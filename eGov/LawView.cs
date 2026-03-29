/*
 * 2026-03-05
 * 注意点：法令IDは枝番が存在する。だから法令番号で指定する。
 */
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

using Common;

using Vo;

namespace EGov {
    public partial class LawView : Form {
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lawTitle"></param>
        /// <param name="lawNum"></param>
        /// <param name="lawArticle"></param>
        /// <param name="lawParagraph"></param>
        public LawView(string lawTitle, string lawNum, string? lawArticle = null, string? lawParagraph = null) {
            _lawTitle = lawTitle;
            _lawNum = LawNumberConverter.ConvertLawNotation(lawNum);
            _lawArticle = lawArticle;
            _lawParagraph = lawParagraph;

            InitializeComponent();

            this.CcTextBoxLawId.Text = string.Empty;
            this.CcTextBoxLawNum.Text = string.Empty;
            this.CcTextBoxLawArticle.Text = string.Empty;
            this.CcTextBoxLawParagraph.Text = string.Empty;
            this.CcTextBoxLawType.Text = string.Empty;
            this.CcTextBoxLawTitle.Text = string.Empty;

            // TreeView の選択イベントを登録（UI スレッドで安全に）
            this.CcTreeView1.AfterSelect += CcTreeView1_AfterSelect;
        }

        /// <summary>
        /// LawListからインスタンス化した後に呼び出す。
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync() {
            XDocument? xDocument = await LawApiClient.GetLawDataXmlByLawNumAsync(_lawNum);
            if (xDocument == null || xDocument.Root == null) {
                Debug.WriteLine("GetLawDataAsync: xDocument or Root is null");
                return;
            }

            var lawDataResponse = new LawParser().Parse(xDocument);

            string lawId = lawDataResponse.LawInfo.LawId;
            string lawNum = lawDataResponse.LawInfo.LawNum;
            string? lawArticle = _lawArticle;
            string? lawParagraph = _lawParagraph;
            string lawTitle = lawDataResponse.RevisionInfo.LawTitle;
            string lawType = _dictionaryLawType[lawDataResponse.LawInfo.LawType];

            this.CcTextBoxLawId.Text = lawId;
            this.CcTextBoxLawNum.Text = lawNum;
            this.CcTextBoxLawArticle.Text = lawArticle ?? string.Empty;
            this.CcTextBoxLawParagraph.Text = lawParagraph ?? string.Empty;
            this.CcTextBoxLawType.Text = lawType;
            this.CcTextBoxLawTitle.Text = lawTitle;

            // TreeView 初期化は lawDataResponse.LawFullText を元に別クラスで組むと綺麗
            InitializeTreeView(lawDataResponse);
            // ★ TreeView 初期化後にジャンプ
            JumpToInitialPosition();
        }

        private void InitializeTreeView(LawDataResponse response) {
            CcTreeView1.BeginUpdate();
            CcTreeView1.Nodes.Clear();

            // ルートノード（法律タイトル）
            string title = response.RevisionInfo.LawTitle;
            TreeNode root = new TreeNode(title) {
                Tag = response
            };
            CcTreeView1.Nodes.Add(root);

            var law = response.LawFullText.Law;
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

        private void AddMainProvision(TreeNode parent, MainProvision main) {
            // Chapter がある場合
            foreach (var chapter in main.Chapters) {
                string chapterTitle = $"第{chapter.Num}章 {chapter.ChapterTitle}";
                TreeNode chapterNode = new TreeNode(chapterTitle) {
                    Tag = chapter
                };
                parent.Nodes.Add(chapterNode);

                // Section（節）
                foreach (var section in chapter.Sections) {
                    string sectionTitle = $"第{section.Num}節 {section.SectionTitle}";
                    TreeNode sectionNode = new TreeNode(sectionTitle) {
                        Tag = section
                    };
                    chapterNode.Nodes.Add(sectionNode);

                    // ★ 追加：Subsection（款）
                    foreach (var subsection in section.Subsections) {
                        string subsectionTitle = $"第{subsection.Num}款 {subsection.SubsectionTitle}";
                        TreeNode subsectionNode = new TreeNode(subsectionTitle) {
                            Tag = subsection
                        };
                        sectionNode.Nodes.Add(subsectionNode);

                        // 款の下の条
                        AddArticles(subsectionNode, subsection.Articles);
                    }

                    // 節直下の条
                    AddArticles(sectionNode, section.Articles);
                }

                // Chapter 直下の条
                AddArticles(chapterNode, chapter.Articles);
            }

            // MainProvision 直下の条（章なし法令）
            AddArticles(parent, main.Articles);
        }

        private void AddArticles(TreeNode parent, List<Article> articles) {
            if (articles == null)
                return;

            foreach (var article in articles) {
                string title = string.IsNullOrEmpty(article.ArticleTitle)
                    ? $"第{article.Num}条"
                    : article.ArticleTitle;

                TreeNode articleNode = new TreeNode(title) {
                    Tag = article
                };
                parent.Nodes.Add(articleNode);

                // Paragraph
                foreach (var para in article.Paragraphs) {
                    string paraTitle = string.IsNullOrEmpty(para.ParagraphNum)
                        ? $"{para.Num}項"
                        : para.ParagraphNum;

                    TreeNode paraNode = new TreeNode(paraTitle) {
                        Tag = para
                    };
                    articleNode.Nodes.Add(paraNode);

                    // ParagraphSentence
                    foreach (var s in para.ParagraphSentence.Sentences) {
                        TreeNode sentenceNode = new TreeNode(s.Text) {
                            Tag = s
                        };
                        paraNode.Nodes.Add(sentenceNode);
                    }

                    // Item
                    foreach (var item in para.Items) {
                        string itemTitle = string.IsNullOrEmpty(item.ItemTitle)
                            ? $"{item.Num}号"
                            : item.ItemTitle;

                        TreeNode itemNode = new TreeNode(itemTitle) {
                            Tag = item
                        };
                        paraNode.Nodes.Add(itemNode);

                        foreach (var s in item.ItemSentence.Sentences) {
                            TreeNode sentenceNode = new TreeNode(s.Text) {
                                Tag = s
                            };
                            itemNode.Nodes.Add(sentenceNode);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// TreeView のノードがクリックされたときに、
        /// ノードの種類（Chapter / Article / Paragraph / Item / Sentence）
        /// に応じて全文を表示する。
        /// </summary>
        private void CcTreeView1_AfterSelect(object? sender, TreeViewEventArgs e) {
            var node = e.Node;

            // ★ 章・節・款をクリックしたら自動展開
            if (node.Text.Contains("章") ||
                node.Text.Contains("節") ||
                node.Text.Contains("款") ||
                node.Text.Contains("条") ||
                node.Text.Contains("項") ||
                node.Text.Contains("号")) {
                node.Expand(); // 自分を展開
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

            // ★ 表示後に太字・色付けを適用
            ApplyFormatting();
        }

        private void ApplyFormatting() {
            // 章
            ColorizeAndBold(@"^\s*第[0-9一二三四五六七八九十百千]+章");

            // 節
            ColorizeAndBold(@"^\s*第[0-9一二三四五六七八九十百千]+節");

            // 款
            ColorizeAndBold(@"^\s*第[0-9一二三四五六七八九十百千]+款");

            // 条（の○ まで対応）
            ColorizeAndBold(@"^\s*第[0-9一二三四五六七八九十百千]+条(の[一二三四五六七八九十百千]+)*");

            // 項
            ColorizeAndBold(@"^\s*[0-9一二三四五六七八九十百千]+項");

            // 号
            ColorizeAndBold(@"^\s*[0-9一二三四五六七八九十百千]+号");
        }

        private void ColorizeAndBold(string pattern) {
            var matches = Regex.Matches(
                CcRichTextBox1.Text,
                pattern,
                RegexOptions.Multiline
            );

            foreach (Match m in matches) {
                CcRichTextBox1.Select(m.Index, m.Length);

                CcRichTextBox1.SelectionFont = new Font(
                    CcRichTextBox1.Font,
                    FontStyle.Bold
                );

                CcRichTextBox1.SelectionColor = Color.Blue;
            }

            CcRichTextBox1.Select(0, 0);
        }

        private string BuildChapterText(Chapter chapter) {
            var sb = new StringBuilder();

            sb.AppendLine($"第{chapter.Num}章 {chapter.ChapterTitle}");
            sb.AppendLine();

            // 節
            foreach (var section in chapter.Sections) {
                sb.AppendLine(BuildSectionText(section));
                sb.AppendLine();
            }

            // 節がない章の条
            foreach (var article in chapter.Articles) {
                sb.AppendLine(BuildArticleText(article));
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private string BuildSectionText(Section section) {
            var sb = new StringBuilder();

            sb.AppendLine($"第{section.Num}節 {section.SectionTitle}");
            sb.AppendLine();

            // 款
            foreach (var subsection in section.Subsections) {
                sb.AppendLine(BuildSubsectionText(subsection));
                sb.AppendLine();
            }

            // 款がない節の条
            foreach (var article in section.Articles) {
                sb.AppendLine(BuildArticleText(article));
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private string BuildSubsectionText(Subsection subsection) {
            var sb = new StringBuilder();

            sb.AppendLine($"第{subsection.Num}款 {subsection.SubsectionTitle}");
            sb.AppendLine();

            foreach (var article in subsection.Articles) {
                sb.AppendLine(BuildArticleText(article));
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private string BuildArticleText(Article article) {
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(article.ArticleTitle))
                sb.AppendLine(article.ArticleTitle);
            else
                sb.AppendLine($"第{article.Num}条");

            sb.AppendLine();

            foreach (var para in article.Paragraphs) {
                sb.AppendLine(BuildParagraphText(para));
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private string BuildParagraphText(Paragraph para) {
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(para.ParagraphNum))
                sb.AppendLine(para.ParagraphNum);
            else
                sb.AppendLine($"{para.Num}項");

            foreach (var s in para.ParagraphSentence.Sentences)
                sb.AppendLine(s.Text);

            foreach (var item in para.Items) {
                sb.AppendLine();
                sb.AppendLine(BuildItemText(item));
            }

            return sb.ToString();
        }

        private string BuildItemText(Item item) {
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(item.ItemTitle))
                sb.AppendLine(item.ItemTitle);
            else
                sb.AppendLine($"{item.Num}号");

            foreach (var s in item.ItemSentence.Sentences)
                sb.AppendLine(s.Text);

            return sb.ToString();
        }

        // ============================================================
        // ★ ジャンプ処理（条 → 項）
        // ============================================================
        private void JumpToInitialPosition() {
            if (_lawArticle == null)
                return;

            // 条を探す
            var articleNode = FindNodeByArticle(CcTreeView1.Nodes, _lawArticle);
            if (articleNode != null) {
                CcTreeView1.SelectedNode = articleNode;
                articleNode.EnsureVisible();

                // 項指定があればさらに探す
                if (_lawParagraph != null) {
                    var paraNode = FindNodeByParagraph(articleNode, _lawParagraph);
                    if (paraNode != null) {
                        CcTreeView1.SelectedNode = paraNode;
                        paraNode.EnsureVisible();
                    }
                }
            }
        }

        // ============================================================
        // ★ 条ノード検索（漢数字 → アラビア数字変換して比較）
        // ============================================================
        private TreeNode? FindNodeByArticle(TreeNodeCollection nodes, string target) {
            foreach (TreeNode node in nodes) {
                string normalized = NormalizeArticleText(node.Text);

                if (normalized == target)
                    return node;

                var found = FindNodeByArticle(node.Nodes, target);
                if (found != null)
                    return found;
            }
            return null;
        }

        // ============================================================
        // ★ 項ノード検索（"三項" → "3"）
        // ============================================================
        private TreeNode? FindNodeByParagraph(TreeNode articleNode, string target) {
            foreach (TreeNode node in articleNode.Nodes) {
                string normalized = NormalizeParagraphText(node.Text);

                if (normalized == target)
                    return node;
            }
            return null;
        }

        // ============================================================
        // ★ 正規化（条：第二十三条の二 → "23-2"）
        // ============================================================
        private string NormalizeArticleText(string text) {
            text = text.Replace("第", "").Replace("条", "");

            var parts = text.Split('の');
            List<string> nums = new();

            foreach (var p in parts)
                nums.Add(KanjiToNumber(p).ToString());

            return string.Join("-", nums);
        }

        // ============================================================
        // ★ 正規化（項：三項 → "3"）
        // ============================================================
        private string NormalizeParagraphText(string text) {
            // 例： "1項" → "1"
            text = text.Replace("項", "");

            // すでにアラビア数字ならそのまま
            if (int.TryParse(text, out _))
                return text;

            // 漢数字なら変換
            return KanjiToNumber(text).ToString();
        }

        // ============================================================
        // ★ 正規化（号：一号 → "1"）
        // ============================================================
        private string NormalizeItemText(string text) {
            text = text.Replace("号", "");
            return KanjiToNumber(text).ToString();
        }

        // ============================================================
        // ★ 漢数字 → アラビア数字変換
        // ============================================================
        private static readonly Dictionary<char, int> KanjiMap = new() {
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

        private int KanjiToNumber(string kanji) {
            int total = 0;
            int current = 0;

            foreach (char c in kanji) {
                if (KanjiMap.TryGetValue(c, out int val)) {
                    if (val >= 10) {
                        if (current == 0)
                            current = 1;
                        current *= val;
                    } else {
                        current += val;
                    }
                }
            }

            total += current;
            return total;
        }
    }
}