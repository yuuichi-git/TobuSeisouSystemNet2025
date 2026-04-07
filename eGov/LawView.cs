using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

using Common;

using Vo;

namespace EGov {
    public partial class LawView : Form {
        // ============================================================
        // ★ 定数・フィールド
        // ============================================================
        private const string INDENT1 = "    ";
        private const string INDENT2 = "        ";

        private readonly string _lawTitle;
        private readonly string _lawNum;
        private readonly string? _lawArticle;
        private readonly string? _lawParagraph;
        private readonly string? _lawItem;

        private readonly Dictionary<string, string> _dictionaryLawType = new(){{ "Constitution",        "憲法" },
                                                                               { "Act",                 "法律" },
                                                                               { "CabinetOrder",        "政令" },
                                                                               { "ImperialOrder",       "勅令" },
                                                                               { "MinisterialOrdinance","府省令" },
                                                                               { "Rule",                "規則" },
                                                                               { "Misc",                "その他" }};

        // ============================================================
        // ★ コンストラクタ
        // ============================================================
        public LawView(string lawTitle, string lawNum, string? lawArticle = null, string? lawParagraph = null, string? lawItem = null) {
            _lawTitle = lawTitle;
            _lawNum = LawNumberConverter.ConvertLawNotation(lawNum);
            _lawArticle = lawArticle;
            _lawParagraph = lawParagraph;
            _lawItem = lawItem;

            /*
             * InitializeControl
             */
            InitializeComponent();

            this.CcTextBoxLawId.Text = "";
            this.CcTextBoxLawNum.Text = "";
            this.CcTextBoxLawArticle.Text = "";
            this.CcTextBoxLawParagraph.Text = "";
            this.CcTextBoxLawType.Text = "";
            this.CcTextBoxLawTitle.Text = "";
            this.CcTextBoxLawItem.Text = "";

            this.CcTreeView1.AfterSelect += CcTreeView1_AfterSelect;
        }

        // ============================================================
        // ★ 初期化（API → TreeView）
        // ============================================================
        public async Task InitializeAsync() {
            XDocument? xDocument = await LawApiClient.GetLawDataXmlByLawNumAsync(_lawNum);
            if (xDocument == null || xDocument.Root == null) {
                Debug.WriteLine("GetLawDataAsync: xDocument or Root is null");
                return;
            }

            var lawDataResponse = new LawParser().Parse(xDocument);

            this.CcTextBoxLawId.Text = lawDataResponse.LawInfo.LawId;
            this.CcTextBoxLawNum.Text = lawDataResponse.LawInfo.LawNum;
            this.CcTextBoxLawArticle.Text = _lawArticle ?? "";
            this.CcTextBoxLawParagraph.Text = _lawParagraph ?? "";
            this.CcTextBoxLawItem.Text = _lawItem ?? "";
            this.CcTextBoxLawType.Text = _dictionaryLawType[lawDataResponse.LawInfo.LawType];
            this.CcTextBoxLawTitle.Text = lawDataResponse.RevisionInfo.LawTitle;

            this.InitializeTreeView(lawDataResponse);
            this.JumpToInitialPosition();
        }

        // ============================================================
        // ★ TreeView 初期化
        // ============================================================
        private void InitializeTreeView(LawDataResponse lawDataResponse) {
            this.CcTreeView1.BeginUpdate();
            this.CcTreeView1.Nodes.Clear();

            /*
             * RootNodeを作成
             */
            TreeNode rootNode = new(lawDataResponse.RevisionInfo.LawTitle);
            rootNode.Tag = lawDataResponse;
            this.CcTreeView1.Nodes.Add(rootNode);

            var law = lawDataResponse.LawFullText.Law;
            var body = law.LawBody;

            // 本文
            TreeNode mainNode = new("本文");
            mainNode.Tag = body.MainProvision;
            rootNode.Nodes.Add(mainNode);

            AddMainProvision(mainNode, body.MainProvision);

            // 附則
            if (body.SupplProvision != null) {
                string supplLabel = string.IsNullOrEmpty(body.SupplProvision.SupplProvisionLabel)
                    ? "附則"
                    : body.SupplProvision.SupplProvisionLabel;

                TreeNode supplNode = new(supplLabel);
                supplNode.Tag = body.SupplProvision;
                rootNode.Nodes.Add(supplNode);

                AddArticles(supplNode, body.SupplProvision.Articles);
            }

            rootNode.Expand();
            this.CcTreeView1.EndUpdate();
        }

        /// <summary>
        /// 本則
        /// AddMainProvision（章・節・款・条を追加）
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="main"></param>
        private void AddMainProvision(TreeNode parent, MainProvision main) {
            foreach (var chapter in main.Chapters) {
                var chapterTitle = $"{chapter.ChapterTitle}";
                var chapterNode = CreateNode(chapterTitle, chapter);
                parent.Nodes.Add(chapterNode);

                foreach (var section in chapter.Sections) {
                    var sectionTitle = $"{section.SectionTitle}";
                    var sectionNode = CreateNode(sectionTitle, section);
                    chapterNode.Nodes.Add(sectionNode);

                    foreach (var subsection in section.Subsections) {
                        var subsectionTitle = $"{subsection.SubsectionTitle}";
                        var subsectionNode = CreateNode(subsectionTitle, subsection);
                        sectionNode.Nodes.Add(subsectionNode);

                        AddArticles(subsectionNode, subsection.Articles);
                    }

                    AddArticles(sectionNode, section.Articles);
                }

                AddArticles(chapterNode, chapter.Articles);
            }

            AddArticles(parent, main.Articles);
        }

        // ============================================================
        // ★ AddArticles（条 → 項 → 号）
        // ============================================================
        private void AddArticles(TreeNode parent, List<Article> articles) {
            if (articles == null)
                return;

            // ============================================================
            // ★ 附則専用プレフィックス
            //
            // e-Gov の SupplProvision（附則）は本則とは別体系の条番号を持つ。
            // そのため TreeView 上で本則の条と混ざるとユーザーが混乱する。
            //
            // 対策：
            //   - parent.Tag が SupplProvision のときだけ
            //       「附則 第○条」
            //     というプレフィックスを付けて区別する。
            //
            // 例：
            //   本則：第十五条
            //   附則：附則 第十五条
            //
            // これにより UI が圧倒的に分かりやすくなる。
            // ============================================================
            string prefix = parent.Tag is SupplProvision ? "附則 " : "";

            // ============================================================
            // 条（Article）の追加
            // ============================================================
            foreach (var article in articles) {
                // ★ 条タイトルの揺れ吸収 + Caption 表示
                string baseTitle = string.IsNullOrEmpty(article.ArticleTitle)
                    ? $"{prefix}第{ToKanjiFlexible(article.Num)}条"
                    : $"{prefix}{article.ArticleTitle}";

                // ★ ArticleCaption（例：〈点呼等〉）を右側に付ける
                string caption = string.IsNullOrWhiteSpace(article.ArticleCaption)
                    ? ""
                    : $"{article.ArticleCaption}";

                string articleTitle = baseTitle + caption;

                var articleNode = CreateNode(articleTitle, article);
                articleNode.Name = NormalizeArticleLoose(article.Num);
                parent.Nodes.Add(articleNode);

                // ============================================================
                // ★ 項（Paragraph）の追加
                //
                // ParagraphNum は揺れが激しい：
                //   - ""（空）
                //   - "1" / "2"（数字だけ）
                //   - "一項" / "第一項"（漢数字＋項）
                //
                // 空 or 数字だけ → Num を漢数字化して「項」を付ける。
                // ============================================================
                foreach (var para in article.Paragraphs) {
                    string paraTitle;

                    if (string.IsNullOrWhiteSpace(para.ParagraphNum) ||
                        Regex.IsMatch(para.ParagraphNum, @"^\d+$")) {
                        paraTitle = $"{ToKanjiFlexible(para.Num)}項";
                    } else {
                        paraTitle = para.ParagraphNum;
                    }

                    var paraNode = CreateNode(paraTitle, para);
                    paraNode.Name = NormalizeParagraphLoose(para.Num);       // ★追加
                    articleNode.Nodes.Add(paraNode);

                    // ============================================================
                    // ★ Sentence（本文）の追加
                    // ============================================================
                    foreach (var s in para.ParagraphSentence.Sentences)
                        paraNode.Nodes.Add(CreateNode(s.Text, s));

                    // ============================================================
                    // ★ 号（Item）の追加 — 本文と完全統一
                    // ============================================================
                    foreach (var item in para.Items) {

                        // ★ 右側と同じロジックで号タイトルを生成
                        string itemTitle = ToKanjiFlexible(item.Num, isItem: true);

                        var itemNode = CreateNode(itemTitle, item);
                        itemNode.Name = NormalizeItemLoose(item.Num);            // ★追加
                        paraNode.Nodes.Add(itemNode);

                        foreach (var s in item.ItemSentence.Sentences)
                            itemNode.Nodes.Add(CreateNode(s.Text, s));
                    }
                }
            }
        }

        // ============================================================
        // ★ Node 生成ヘルパー
        // ============================================================
        private TreeNode CreateNode(string text, object tag) {
            return new TreeNode(text) { Tag = tag };
        }

        // ============================================================
        // ★ AfterSelect（本文表示）
        // ============================================================
        private void CcTreeView1_AfterSelect(object? sender, TreeViewEventArgs e) {
            var node = e.Node;

            // ============================================================
            // ★ 構造ノード（章・節・款・条・項・号）は自動展開
            //
            // Text に「章」「節」「条」などが含まれるかどうかではなく、
            // Tag の型（Chapter / Section / Article / Paragraph / Item）で判定する。
            //
            // これにより：
            //   - 本文中の「条」「項」「号」で誤爆しない
            //   - 構造とロジックが 1:1 で対応し、未来の自分が読んでも一瞬で理解できる
            // ============================================================
            if (node?.Tag is Chapter or Section or Subsection or Article or Paragraph or Item) {
                node.Expand();
            }

            if (node?.Tag == null) {
                CcRichTextBox1.Text = "";
                return;
            }

            string text = node.Tag switch {
                Chapter chapter => BuildChapterText(chapter),
                Section section => BuildSectionText(section),
                Subsection subsection => BuildSubsectionText(subsection),
                Article article => BuildArticleText(article),
                Paragraph para => BuildParagraphText(para),
                Item item => BuildItemText(item),
                Sentence sentence => sentence.Text,
                _ => ""
            };

            CcRichTextBox1.Font = new Font("Meiryo", 9);
            CcRichTextBox1.Text = text;

            ApplyFormatting();
        }

        // ============================================================
        // ★ 太字・色付け・フォントサイズ変更（章・節・款・条・項・号）
        // ------------------------------------------------------------
        // RichTextBox 内のテキストに対して、正規表現で見出しを検出し、
        // その部分だけフォントサイズ・太字・色を変更する。
        // 
        // ・章・節・款 → 最も大きく（11pt）
        // ・条 → 少し大きく（10pt）
        // ・項・号 → 標準より少し強調（10pt）
        //
        // RegexOptions.Multiline を使うことで、行頭 ^ が各行に適用される。
        // ============================================================
        private void ApplyFormatting() {
            // 章（例：第七章）
            ColorizeBoldAndResize(@"^\s*第[一二三四五六七八九十百千・]+章", 11);

            // 節（例：第一節）
            ColorizeBoldAndResize(@"^\s*第[一二三四五六七八九十百千・]+節", 11);

            // 款（例：第一款）
            ColorizeBoldAndResize(@"^\s*第[一二三四五六七八九十百千・]+款", 11);

            // 条（例：第七条、七条の二）
            ColorizeBoldAndResize(@"^\s*第[一二三四五六七八九十百千・]+条(の[一二三四五六七八九十百千]+)*", 10);

            // 項（例：一項、二項）
            ColorizeBoldAndResize(@"^\s*[一二三四五六七八九十百千]+項", 10);

            // 号（例：一号、二号）
            ColorizeBoldAndResize(@"^\s*[一二三四五六七八九十百千]+号", 10);
        }

        // ============================================================
        // ★ ColorizeBoldAndResize
        // ------------------------------------------------------------
        // 指定した正規表現にマッチした部分だけ、
        // ・フォントサイズ変更
        // ・太字
        // ・青色
        // にするユーティリティメソッド。
        //
        // RichTextBox は部分的なフォント変更が可能なので、
        // Select() → SelectionFont / SelectionColor を使って実現する。
        // ============================================================
        private void ColorizeBoldAndResize(string pattern, float fontSize) {
            var matches = Regex.Matches(
                CcRichTextBox1.Text,
                pattern,
                RegexOptions.Multiline
            );

            foreach (Match m in matches) {
                // 対象範囲を選択
                CcRichTextBox1.Select(m.Index, m.Length);

                // フォントサイズと太字を適用
                CcRichTextBox1.SelectionFont = new Font(
                    CcRichTextBox1.Font.FontFamily,  // 現在のフォントファミリを維持
                    fontSize,                        // 指定サイズに変更
                    FontStyle.Bold                   // 太字
                );

                // 色を青に変更
                CcRichTextBox1.SelectionColor = Color.Blue;
            }

            // 選択状態を解除（カーソルを先頭に戻す）
            CcRichTextBox1.Select(0, 0);
        }

        private string BuildChapterText(Chapter chapter) {
            var sb = new StringBuilder();

            // ============================================================
            // ★ 章タイトルの出力
            //
            // ChapterTitle は e-Gov API の <ChapterTitle> に対応。
            // 例：総則、目的、定義 など。
            //
            // ※ TreeView 側では「第○章 ○○」と番号付きで表示しているが、
            //    本文側では Title のみを表示する設計。
            //    （必要なら「第○章」を付けることも可能）
            // ============================================================
            string title = $"{chapter.ChapterTitle}";
            sb.AppendLine(title);

            // 章内の節をすべて展開
            foreach (var section in chapter.Sections)
                sb.AppendLine(BuildSectionText(section));

            // 章直下に条がある場合（節なし構造）
            foreach (var article in chapter.Articles)
                sb.AppendLine(BuildArticleText(article));

            return sb.ToString();
        }

        private string BuildSectionText(Section section) {
            var sb = new StringBuilder();

            // ============================================================
            // ★ 節タイトルの出力
            //
            // SectionTitle は <SectionTitle> に対応。
            // 例：定義、基本事項、手続 など。
            //
            // 本文側では番号（第○節）は付けず、Title のみを表示する方針。
            // ============================================================
            string title = $"{section.SectionTitle}";
            sb.AppendLine(title);

            // 節内の款を展開
            foreach (var subsection in section.Subsections)
                sb.AppendLine(BuildSubsectionText(subsection));

            // 節直下の条を展開
            foreach (var article in section.Articles)
                sb.AppendLine(BuildArticleText(article));

            return sb.ToString();
        }

        private string BuildSubsectionText(Subsection subsection) {
            var sb = new StringBuilder();

            // ============================================================
            // ★ 款タイトルの出力
            //
            // SubsectionTitle は <SubsectionTitle> に対応。
            // 例：基本原則、特例、補足規定 など。
            //
            // 本文側では番号（第○款）は付けず、Title のみを表示する方針。
            // ============================================================
            string title = $"{subsection.SubsectionTitle}";
            sb.AppendLine(title);

            // 款直下の条を展開
            foreach (var article in subsection.Articles)
                sb.AppendLine(BuildArticleText(article));

            return sb.ToString();
        }

        private string BuildArticleText(Article article) {
            var sb = new StringBuilder();

            // ============================================================
            // ★ 条タイトルの出力（Caption も含む）
            //
            // ArticleTitle が存在する場合：
            //   <ArticleTitle>第七条</ArticleTitle>
            //
            // ArticleTitle が空の場合：
            //   第7条 → 第七条（ToKanjiFlexible）
            //
            // ArticleCaption（例：点呼等）は Title の右側に付ける。
            //   例：第七条（点呼等）
            //
            // TreeView と本文側の表示を完全に統一するための仕様。
            // ============================================================
            string title = !string.IsNullOrEmpty(article.ArticleTitle)
                ? article.ArticleTitle
                : $"第{ToKanjiFlexible(article.Num)}条";

            if (!string.IsNullOrWhiteSpace(article.ArticleCaption))
                title += $"{article.ArticleCaption}";

            sb.AppendLine(title);

            // 条内の項を展開
            foreach (var para in article.Paragraphs)
                sb.AppendLine(BuildParagraphText(para));

            return sb.ToString();
        }


        // ============================================================
        // 項の表示
        // ============================================================
        private string BuildParagraphText(Paragraph para) {
            var sb = new StringBuilder();

            string title;

            // ============================================================
            // ★ ParagraphNum の揺れ吸収ロジック
            //
            // e-Gov API の ParagraphNum は揺れている：
            //   - ""（空）
            //   - "1" / "2" / "3"（数字だけ）
            //   - "一項" / "第一項"（すでに漢数字＋項）
            //
            // TreeView と本文の表示を揃えるため、
            //   1) 空 or 数字だけ → Num を漢数字化して「項」を付ける
            //   2) それ以外（例：一項 / 第一項）→ そのまま使う
            // ============================================================
            if (string.IsNullOrWhiteSpace(para.ParagraphNum) ||
                Regex.IsMatch(para.ParagraphNum, @"^\d+$")) {
                // Num は揺れゼロ（1,2,3…）なので ToKanjiFlexible で漢数字化
                title = $"{ToKanjiFlexible(para.Num)}項";
            } else {
                // すでに「一項」「第一項」などが入っている場合はこちらを優先
                title = para.ParagraphNum;
            }

            // 項タイトルを出力
            sb.AppendLine(INDENT1 + title);

            // ============================================================
            // ★ Sentence（本文）を出力
            // ============================================================
            foreach (var s in para.ParagraphSentence.Sentences)
                sb.AppendLine(INDENT1 + s.Text);

            // ============================================================
            // ★ 号（Item）を出力
            // ============================================================
            foreach (var item in para.Items)
                sb.AppendLine(BuildItemText(item));

            return sb.ToString();
        }


        // ============================================================
        // 号の表示（枝番にも完全対応）
        // ============================================================
        private string BuildItemText(Item item) {
            var sb = new StringBuilder();

            // ★ 号モードで漢数字化（五号 / 五号の四）
            string title = ToKanjiFlexible(item.Num, isItem: true);

            sb.AppendLine(INDENT2 + title);

            foreach (var s in item.ItemSentence.Sentences)
                sb.AppendLine(INDENT2 + s.Text);

            return sb.ToString();
        }

        // ============================================================
        // ★ ジャンプ処理（条 → 項 → 号）
        // ============================================================
        private void JumpToInitialPosition() {

            // ★ 条が指定されていない場合はジャンプしない
            if (_lawArticle == null)
                return;

            // ★ 条へジャンプ
            var articleNode = FindArticleNodeStrict(CcTreeView1.Nodes, _lawArticle);
            if (articleNode == null)
                return;
            // ハイライト＋展開
            SelectAndHighlight(articleNode);

            // ★ 項が指定されていない場合 → 条だけハイライトして終了
            if (_lawParagraph == null)
                return;

            // ★ 項へジャンプ
            var paragraphNode = FindParagraphNodeStrict(articleNode, _lawParagraph);
            if (paragraphNode == null)
                return;
            // ハイライト＋展開
            SelectAndHighlight(paragraphNode);

            // ★ 号が指定されていない場合 → 項だけハイライトして終了
            if (_lawItem == null)
                return;

            // ★ 号へジャンプ
            var itemNode = FindItemNodeStrict(paragraphNode, _lawItem);
            if (itemNode == null)
                return;
            // ハイライト＋展開
            SelectAndHighlight(itemNode);
        }

        private TreeNode? FindArticleNodeStrict(TreeNodeCollection nodes, string target) {
            string normalizedTarget = NormalizeArticleLoose(target);

            foreach (TreeNode node in nodes) {
                // ★ 条ノード判定は Tag でやる
                if (node.Tag is Article) {
                    // ★ Text を正規化するのではなく、Name（＝正規化キー）と比較する
                    if (node.Name == normalizedTarget)
                        return node;
                }

                // 項・号はスキップ（条の下だけ検索）
                if (node.Tag is Paragraph || node.Tag is Item)
                    continue;

                var child = FindArticleNodeStrict(node.Nodes, normalizedTarget);
                if (child != null)
                    return child;
            }

            return null;
        }

        /*
         * 条の枝番パターン（例：50の2、50-2、50_2）に対応するための揺れ吸収ロジック
         */
        private string NormalizeArticleLoose(string text) {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            string s = text.Trim();

            // 「第」を除去
            if (s.StartsWith("第"))
                s = s.Substring(1);

            // 「条」を除去
            s = s.Replace("条", "");

            // 全角数字 → 半角数字
            s = ToHalfWidthDigits(s);

            // 「の」で枝番をすべて分離（例：百十七の二の二）
            if (s.Contains("の")) {
                var parts = s.Split('の');
                var normalizedParts = parts.Select(p => NormalizeNumber(p));
                return string.Join("-", normalizedParts);
            }

            // アンダースコア（117_2_2）
            if (s.Contains("_")) {
                var parts = s.Split('_');
                var normalizedParts = parts.Select(p => NormalizeNumber(p));
                return string.Join("-", normalizedParts);
            }

            // ハイフン（117-2-2）
            if (s.Contains("-")) {
                var parts = s.Split('-');
                var normalizedParts = parts.Select(p => NormalizeNumber(p));
                return string.Join("-", normalizedParts);
            }

            // 単独の条番号
            return NormalizeNumber(s);
        }


        private string NormalizeNumber(string s) {
            // 数字ならそのまま
            if (int.TryParse(s, out int n))
                return n.ToString();

            // 漢数字 → 数字
            return NumberConverter.KanjiToNumber(s).ToString();
        }

        private TreeNode? FindParagraphNodeStrict(TreeNode articleNode, string targetParagraph) {
            string normalizedTarget = NormalizeParagraphLoose(targetParagraph);

            foreach (TreeNode node in articleNode.Nodes) {
                if (node.Tag is not Paragraph)
                    continue;

                if (node.Name == normalizedTarget)
                    return node;
            }

            return null;
        }

        private TreeNode? FindItemNodeStrict(TreeNode paragraphNode, string targetItem) {
            string normalizedTarget = NormalizeItemLoose(targetItem);

            foreach (TreeNode node in paragraphNode.Nodes) {
                if (node.Tag is not Item)
                    continue;

                if (node.Name == normalizedTarget)
                    return node;
            }

            return null;
        }

        private string NormalizeParagraphLoose(string text) {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            string s = text.Trim();

            // 「項」を除去
            s = s.Replace("項", "");

            // 「第」を除去（例：第一項 → 一）
            if (s.StartsWith("第"))
                s = s.Substring(1);

            // 全角数字 → 半角数字
            s = ToHalfWidthDigits(s);

            // 数字ならそのまま
            if (int.TryParse(s, out int n))
                return n.ToString();

            // 漢数字 → 数字
            return NumberConverter.KanjiToNumber(s).ToString();
        }

        /*
         * 号の枝番パターン（例：2の2、二の三）に対応するための揺れ吸収ロジック
         */
        private string NormalizeItemLoose(string text) {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            string s = text.Trim();

            // 「第」を除去（例：第一号 → 一号）
            if (s.StartsWith("第"))
                s = s.Substring(1);

            // 「号」を除去
            s = s.Replace("号", "");

            // 全角数字 → 半角数字
            s = ToHalfWidthDigits(s);

            // 「の」で枝番を分離（例：二の三）
            if (s.Contains("の")) {
                var parts = s.Split('の');
                return $"{NormalizeNumber(parts[0])}-{NormalizeNumber(parts[1])}";
            }

            // アンダースコア（2_2）
            if (s.Contains("_")) {
                var parts = s.Split('_');
                return $"{NormalizeNumber(parts[0])}-{NormalizeNumber(parts[1])}";
            }

            // ハイフン（2-2）
            if (s.Contains("-")) {
                var parts = s.Split('-');
                return $"{NormalizeNumber(parts[0])}-{NormalizeNumber(parts[1])}";
            }

            // 単独の号番号
            return NormalizeNumber(s);
        }

        private string ToHalfWidthDigits(string s) {
            return s
                .Replace("０", "0")
                .Replace("１", "1")
                .Replace("２", "2")
                .Replace("３", "3")
                .Replace("４", "4")
                .Replace("５", "5")
                .Replace("６", "6")
                .Replace("７", "7")
                .Replace("８", "8")
                .Replace("９", "9");
        }

        // ============================================================
        //
        // ★ NumberConverter（完全リファクタ版）
        //
        // ============================================================
        public static class NumberConverter {
            public static readonly Dictionary<char, int> KanjiMap = new() {
                ['〇'] = 0,
                ['一'] = 1,
                ['二'] = 2,
                ['三'] = 3,
                ['四'] = 4,
                ['五'] = 5,
                ['六'] = 6,
                ['七'] = 7,
                ['八'] = 8,
                ['九'] = 9,
                ['十'] = 10,
                ['百'] = 100,
                ['千'] = 1000
            };

            // ============================================================
            // ★ 漢数字 → アラビア数字（1〜9999対応）
            // ============================================================
            public static int KanjiToNumber(string kanji) {
                int total = 0;
                int current = 0;

                foreach (char c in kanji) {
                    if (!KanjiMap.TryGetValue(c, out int val))
                        continue;

                    if (val >= 10) {
                        if (current == 0)
                            current = 1;
                        current *= val;
                    } else {
                        current += val;
                    }
                }

                total += current;
                return total;
            }

            public static int KanjiOrNumberToInt(string s) {
                if (int.TryParse(s, out var n))
                    return n;

                return KanjiToNumber(s);
            }
        }

        // ============================================================
        // ★ NumberToKanjiFlexible（1〜9999対応）
        // ============================================================
        public static string NumberToKanjiFlexible(string s) {
            if (string.IsNullOrWhiteSpace(s))
                return s;

            if (s.All(c => NumberConverter.KanjiMap.ContainsKey(c)))
                return s;

            if (!int.TryParse(s, out int n))
                return s;

            if (n == 0)
                return "〇";

            var sb = new StringBuilder();

            if (n >= 1000) {
                sb.Append(NumberToKanjiFlexible((n / 1000).ToString()));
                sb.Append("千");
                n %= 1000;
            }
            if (n >= 100) {
                sb.Append(NumberToKanjiFlexible((n / 100).ToString()));
                sb.Append("百");
                n %= 100;
            }
            if (n >= 10) {
                if (n / 10 == 1)
                    sb.Append("十");
                else {
                    sb.Append(NumberToKanjiFlexible((n / 10).ToString()));
                    sb.Append("十");
                }
                n %= 10;
            }
            if (n > 0)
                sb.Append("〇一二三四五六七八九"[n]);

            return sb.ToString();
        }

        // ============================================================
        // ★ ToKanjiFlexible（章・節・款・条・項・号すべて対応）
        //    ・用途に応じて「条」「号」を切り替えられる
        //    ・枝番（50-2 / 5-4 / 2-2）すべて吸収
        // ============================================================
        private string ToKanjiFlexible(string raw, bool isItem = false) {
            if (string.IsNullOrWhiteSpace(raw))
                return raw;

            string s = raw.Trim();

            // 0. アンダースコア → ハイフン
            if (s.Contains('_'))
                s = s.Replace('_', '-');

            // --------------------------------------------
            // 1. ハイフン形式（枝番）
            //    ・条 → 第五十条の二
            //    ・号 → 五号の二
            // --------------------------------------------
            if (s.Contains('-')) {
                var parts = s.Split('-');
                if (parts.Length == 2) {

                    string left = NumberToKanjiFlexible(parts[0]);
                    string right = NumberToKanjiFlexible(parts[1]);

                    if (isItem) {
                        // ★ 号モード（五号の二）
                        return $"{left}号の{right}";
                    } else {
                        // ★ 条モード（第五十条の二）
                        return $"第{left}条の{right}";
                    }
                }
            }

            // --------------------------------------------
            // 2. 第〇章 / 第〇節 / 第〇款
            // --------------------------------------------
            var m = Regex.Match(s, @"^第?(.+?)(章|節|款)$");
            if (m.Success) {
                return $"第{NumberToKanjiFlexible(m.Groups[1].Value)}{m.Groups[2].Value}";
            }

            // --------------------------------------------
            // 3. 〇項 / 〇号
            // --------------------------------------------
            m = Regex.Match(s, @"^(.+?)(項|号)$");
            if (m.Success) {
                string num = NumberToKanjiFlexible(m.Groups[1].Value);
                string unit = m.Groups[2].Value;

                // 号モードなら「号」を優先
                if (isItem)
                    return $"{num}号";

                return $"{num}{unit}";
            }

            // --------------------------------------------
            // 4. 純粋な数字
            // --------------------------------------------
            string kanji = NumberToKanjiFlexible(s);

            if (isItem)
                return $"{kanji}号";

            return kanji;
        }

        // ============================================================
        // テキストをハイライトする（ジャンプ先の条・項・号を黄色で強調）
        // ============================================================
        private void SelectAndHighlight(TreeNode node) {
            // TreeView 側
            CcTreeView1.SelectedNode = node;
            node.EnsureVisible();
            // 本文側
            HighlightArticleText(node.Text);
        }

        private void HighlightArticleText(string keyword) {
            if (string.IsNullOrWhiteSpace(keyword))
                return;

            var rtb = CcRichTextBox1;

            // 既存ハイライトをクリア
            int selStart = rtb.SelectionStart;
            int selLength = rtb.SelectionLength;

            rtb.SelectAll();
            rtb.SelectionBackColor = Color.White;

            // キーワード検索
            int index = rtb.Text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase);
            if (index >= 0) {
                rtb.Select(index, keyword.Length);
                rtb.SelectionBackColor = Color.Yellow;
                rtb.ScrollToCaret();
            }

            // 元の選択に戻す
            rtb.Select(selStart, selLength);
        }
    }
}
