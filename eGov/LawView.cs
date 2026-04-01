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

        private readonly Dictionary<string, string> _dictionaryLawType = new()
        {
            { "Constitution",        "憲法" },
            { "Act",                 "法律" },
            { "CabinetOrder",        "政令" },
            { "ImperialOrder",       "勅令" },
            { "MinisterialOrdinance","府省令" },
            { "Rule",                "規則" },
            { "Misc",                "その他" }
        };

        private readonly string _lawTitle;
        private readonly string _lawNum;
        private readonly string? _lawArticle;
        private readonly string? _lawParagraph;
        private readonly string? _lawItem;

        // ============================================================
        // ★ コンストラクタ
        // ============================================================
        public LawView(string lawTitle, string lawNum, string? lawArticle = null, string? lawParagraph = null, string? lawItem = null) {
            _lawTitle = lawTitle;
            _lawNum = LawNumberConverter.ConvertLawNotation(lawNum);
            _lawArticle = lawArticle;
            _lawParagraph = lawParagraph;
            _lawItem = lawItem;

            InitializeComponent();

            CcTextBoxLawId.Text = "";
            CcTextBoxLawNum.Text = "";
            CcTextBoxLawArticle.Text = "";
            CcTextBoxLawParagraph.Text = "";
            CcTextBoxLawType.Text = "";
            CcTextBoxLawTitle.Text = "";
            CcTextBoxLawItem.Text = "";

            CcTreeView1.AfterSelect += CcTreeView1_AfterSelect;
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

            CcTextBoxLawId.Text = lawDataResponse.LawInfo.LawId;
            CcTextBoxLawNum.Text = lawDataResponse.LawInfo.LawNum;
            CcTextBoxLawArticle.Text = _lawArticle ?? "";
            CcTextBoxLawParagraph.Text = _lawParagraph ?? "";
            CcTextBoxLawItem.Text = _lawItem ?? "";
            CcTextBoxLawType.Text = _dictionaryLawType[lawDataResponse.LawInfo.LawType];
            CcTextBoxLawTitle.Text = lawDataResponse.RevisionInfo.LawTitle;

            InitializeTreeView(lawDataResponse);
            JumpToInitialPosition();
        }

        // ============================================================
        // ★ TreeView 初期化
        // ============================================================
        private void InitializeTreeView(LawDataResponse lawDataResponse) {
            CcTreeView1.BeginUpdate();
            CcTreeView1.Nodes.Clear();

            TreeNode root = new TreeNode(lawDataResponse.RevisionInfo.LawTitle) {
                Tag = lawDataResponse
            };
            CcTreeView1.Nodes.Add(root);

            var law = lawDataResponse.LawFullText.Law;
            var body = law.LawBody;

            // 本文
            TreeNode mainNode = new TreeNode("本文") {
                Tag = body.MainProvision
            };
            root.Nodes.Add(mainNode);

            AddMainProvision(mainNode, body.MainProvision);

            // 附則
            if (body.SupplProvision != null) {
                string supplLabel = string.IsNullOrEmpty(body.SupplProvision.SupplProvisionLabel)
                    ? "附則"
                    : body.SupplProvision.SupplProvisionLabel;

                TreeNode supplNode = new TreeNode(supplLabel) {
                    Tag = body.SupplProvision
                };
                root.Nodes.Add(supplNode);

                AddArticles(supplNode, body.SupplProvision.Articles);
            }

            root.Expand();
            CcTreeView1.EndUpdate();
        }

        // ============================================================
        // ★ AddMainProvision（章・節・款・条を追加）
        // ============================================================
        private void AddMainProvision(TreeNode parent, MainProvision main) {
            foreach (var chapter in main.Chapters) {
                var chapterNode = CreateNode(
                    $"第{ToKanjiFlexible(chapter.Num)}章 {chapter.ChapterTitle}",
                    chapter
                );
                parent.Nodes.Add(chapterNode);

                foreach (var section in chapter.Sections) {
                    var sectionNode = CreateNode(
                        $"第{ToKanjiFlexible(section.Num)}節 {section.SectionTitle}",
                        section
                    );
                    chapterNode.Nodes.Add(sectionNode);

                    foreach (var subsection in section.Subsections) {
                        var subsectionNode = CreateNode(
                            $"第{ToKanjiFlexible(subsection.Num)}款 {subsection.SubsectionTitle}",
                            subsection
                        );
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

            foreach (var article in articles) {
                // ============================================================
                // ★ 条タイトルの揺れ吸収
                //
                // ArticleTitle が空の場合は「第○条」を生成する。
                // 附則の場合は prefix を付けて「附則 第○条」にする。
                // ============================================================
                string articleTitle = string.IsNullOrEmpty(article.ArticleTitle)
                                        ? $"{prefix}第{ToKanjiFlexible(article.Num)}条"
                                        : $"{prefix}{article.ArticleTitle}";

                var articleNode = CreateNode(articleTitle, article);

                // ★ 正規化キーを設定（117_2_2 → 117-2-2）
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
        // ★ 太字・色付け（章・節・款・条・項・号）
        // ============================================================
        private void ApplyFormatting() {
            // 章・節・款・条・項・号を漢数字の揺れゼロで強調
            ColorizeAndBold(@"^\s*第[一二三四五六七八九十百千・]+章");
            ColorizeAndBold(@"^\s*第[一二三四五六七八九十百千・]+節");
            ColorizeAndBold(@"^\s*第[一二三四五六七八九十百千・]+款");
            ColorizeAndBold(@"^\s*第[一二三四五六七八九十百千・]+条(の[一二三四五六七八九十百千]+)*");
            ColorizeAndBold(@"^\s*[一二三四五六七八九十百千]+項");
            ColorizeAndBold(@"^\s*[一二三四五六七八九十百千]+号");
        }

        private void ColorizeAndBold(string pattern) {
            var matches = Regex.Matches(
                CcRichTextBox1.Text,
                pattern,
                RegexOptions.Multiline
            );

            foreach (Match m in matches) {
                CcRichTextBox1.Select(m.Index, m.Length);
                CcRichTextBox1.SelectionFont = new Font(CcRichTextBox1.Font, FontStyle.Bold);
                CcRichTextBox1.SelectionColor = Color.Blue;
            }

            CcRichTextBox1.Select(0, 0);
        }

        private string BuildChapterText(Chapter chapter) {
            var sb = new StringBuilder();

            // 章タイトル
            sb.AppendLine($"第{ToKanjiFlexible(chapter.Num)}章 {chapter.ChapterTitle}");

            // 章内の節 → 条 → 項 → 号 をすべて展開
            foreach (var section in chapter.Sections)
                sb.AppendLine(BuildSectionText(section));

            // 章直下の条も展開
            foreach (var article in chapter.Articles)
                sb.AppendLine(BuildArticleText(article));

            return sb.ToString();
        }

        private string BuildSectionText(Section section) {
            var sb = new StringBuilder();

            sb.AppendLine($"第{ToKanjiFlexible(section.Num)}節 {section.SectionTitle}");

            foreach (var subsection in section.Subsections)
                sb.AppendLine(BuildSubsectionText(subsection));

            foreach (var article in section.Articles)
                sb.AppendLine(BuildArticleText(article));

            return sb.ToString();
        }

        private string BuildSubsectionText(Subsection subsection) {
            var sb = new StringBuilder();

            sb.AppendLine($"第{ToKanjiFlexible(subsection.Num)}款 {subsection.SubsectionTitle}");

            foreach (var article in subsection.Articles)
                sb.AppendLine(BuildArticleText(article));

            return sb.ToString();
        }

        private string BuildArticleText(Article article) {
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(article.ArticleTitle))
                sb.AppendLine(article.ArticleTitle);
            else
                sb.AppendLine($"第{ToKanjiFlexible(article.Num)}条");

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
            SelectAndHighlight(articleNode);

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

        private bool IsArticleNode(TreeNode node) =>
            node.Text.Contains("条") && !node.Text.Contains("項") && !node.Text.Contains("号");

        private bool IsParagraphNode(TreeNode node) =>
            node.Text.Contains("項");

        private bool IsItemNode(TreeNode node) =>
            node.Text.Contains("号");

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

            // ============================================================
            // ★ NormalizeArticle（内部検索用）
            // ============================================================
            public static string NormalizeArticle(string text) {
                if (string.IsNullOrWhiteSpace(text))
                    return text;

                var s = text.Trim();

                // 「第」を外す
                if (s.StartsWith("第"))
                    s = s.Substring(1);

                // ① 「条の」パターン
                var idx = s.IndexOf("条の", StringComparison.Ordinal);
                if (idx >= 0) {
                    var left = s.Substring(0, idx);
                    var right = s.Substring(idx + "条の".Length);
                    return $"{KanjiOrNumberToInt(left)}-{KanjiOrNumberToInt(right)}";
                }

                // ② アンダースコア（50_2 → 50-2）
                if (s.Contains('_')) {
                    var parts = s.Split('_');
                    return $"{KanjiOrNumberToInt(parts[0])}-{KanjiOrNumberToInt(parts[1])}";
                }

                // ③ ハイフン（50-2）
                if (s.Contains('-')) {
                    var parts = s.Split('-');
                    return $"{KanjiOrNumberToInt(parts[0])}-{KanjiOrNumberToInt(parts[1])}";
                }

                // ④ 単独の条番号
                return KanjiOrNumberToInt(s).ToString();
            }

            public static string NormalizeParagraph(string text) {
                text = text.Replace("項", "");
                return KanjiOrNumberToInt(text).ToString();
            }

            public static string NormalizeItem(string text) {
                text = text.Replace("号", "");
                return KanjiOrNumberToInt(text).ToString();
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
