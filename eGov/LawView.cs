/*
 * 2026-03-05
 */
using System.Text;

using Vo;

namespace EGov {
    public partial class LawView : Form {
        private EGobApi _egobApi = new();

        private readonly string _lawName;
        private readonly string _lawArticle;
        private readonly string _lawParagraph;

        private readonly Dictionary<string, string> _dictionaryLawTypeMap = new(){
            { "Constitution",        "憲法" },
            { "Act",                 "法律" },
            { "CabinetOrder",        "政令" },
            { "ImperialOrder",       "勅令" },
            { "MinisterialOrdinance","府省令" },
            { "Rule",                "規則" },
            { "Misc",                "その他" }};
        private static string ToKanjiNumber(string num) {
            return num switch {
                "1" => "一",
                "2" => "二",
                "3" => "三",
                "4" => "四",
                "5" => "五",
                "6" => "六",
                "7" => "七",
                "8" => "八",
                "9" => "九",
                "10" => "十",
                _ => num
            };
        }


        public LawView(string lawName, string lawArticle, string lawParagraph) {
            _lawName = lawName;
            _lawArticle = lawArticle;
            _lawParagraph = lawParagraph;
            /*
             * 
             */
            InitializeComponent();
            this.CcTextBoxLawId.Text = string.Empty;
            this.CcTextBoxLawNum.Text = string.Empty;
            this.CcTextBoxLawType.Text = string.Empty;
            this.CcTextBoxLawTitle.Text = string.Empty;
            this.CcTextBoxLawArticle.Text = string.Empty;

            // フォームロード後に API を叩く
            this.Load += async (_, __) => await LoadLawAsync();
        }

        private async Task LoadLawAsync() {
            // ① KeyWord検索 API で KeywordItem を取得
            KeywordResponse keywordResponse = await _egobApi.GetLawInfoAsync(_lawName);

            if (keywordResponse == null) {
                MessageBox.Show("法令が見つかりませんでした。");
                return;
            }

            // ② law_id を取得
            string? lawId = keywordResponse.Items.FirstOrDefault()?.LawInfo.LawId;

            if (string.IsNullOrWhiteSpace(lawId)) {
                MessageBox.Show("法令IDが取得できませんでした。");
                return;
            }

            // ③ Controlに出力
            this.CcTextBoxLawId.Text = string.Concat(keywordResponse.Items.FirstOrDefault()?.LawInfo.LawId);
            this.CcTextBoxLawNum.Text = string.Concat(keywordResponse.Items.FirstOrDefault()?.LawInfo.LawNum);
            this.CcTextBoxLawType.Text = string.Concat(_dictionaryLawTypeMap[keywordResponse.Items.FirstOrDefault()?.RevisionInfo.LawType]);
            this.CcTextBoxLawTitle.Text = string.Concat(keywordResponse.Items.FirstOrDefault()?.RevisionInfo.LawTitle);
            this.CcTextBoxLawArticle.Text = string.Concat(_lawArticle);

            // ④ 法令APIで法令の内容を取得
            LawDataResponse lawDataResponse = await _egobApi.GetLawDataAsync(lawId);

            if (lawDataResponse?.LawBody == null) {
                MessageBox.Show("法令本文が取得できませんでした。");
                return;
            }

            // ⑤ TreeView に全文構造を表示
            LoadLawTree(lawDataResponse.LawBody);


        }



        private void LoadLawTree(LawBody lawBody) {
            CcTreeView1.BeginUpdate();
            CcTreeView1.Nodes.Clear();

            // -----------------------------------------
            // ① 前文（Preface）
            // -----------------------------------------
            if (lawBody.Preface != null && !string.IsNullOrWhiteSpace(lawBody.Preface.Text)) {
                var prefaceNode = CcTreeView1.Nodes.Add("前文");
                prefaceNode.Nodes.Add(lawBody.Preface.Text);
            }

            // -----------------------------------------
            // ② 章（Chapter）
            // -----------------------------------------
            foreach (var chapter in lawBody.Chapters) {
                string chapterLabel =
                    !string.IsNullOrWhiteSpace(chapter.ChapterTitle)
                    ? chapter.ChapterTitle
                    : $"第{chapter.ChapterNum}章";

                var chapterNode = CcTreeView1.Nodes.Add(chapterLabel);

                // ★ ① 節（Section）
                foreach (var section in chapter.Sections) {
                    string sectionLabel =
                        !string.IsNullOrWhiteSpace(section.SectionTitle)
                        ? section.SectionTitle
                        : $"第{section.SectionNum}節";

                    var sectionNode = chapterNode.Nodes.Add(sectionLabel);

                    // ★ 節の中の条（Article）
                    foreach (var art in section.Articles) {
                        var artNode = sectionNode.Nodes.Add($"{art.ArticleTitle}");
                        artNode.Tag = art;

                        // 項（Paragraph）
                        foreach (var para in art.Paragraphs) {
                            string? rawParaNum = para.ParagraphNum?
                                .Replace("\u3000", "")
                                .Replace("\u00A0", "")
                                .Trim();

                            string paraLabel =
                                string.IsNullOrEmpty(rawParaNum)
                                    ? "本文"
                                    : $"{rawParaNum}項";

                            var paraNode = artNode.Nodes.Add(paraLabel);
                            paraNode.Tag = para;   // ★ 項に Tag

                            // ★ 本文ノードにも Tag を付ける
                            if (!string.IsNullOrWhiteSpace(para.Text)) {
                                var textNode = paraNode.Nodes.Add(para.Text);
                                textNode.Tag = para;   // ★ 本文にも Paragraph を付ける
                            }

                            foreach (var item in para.Items) {
                                string itemLabel = string.IsNullOrWhiteSpace(item.ItemNum)
                                    ? "（号番号なし）"
                                    : $"{ToKanjiNumber(item.ItemNum)}号";

                                var itemNode = paraNode.Nodes.Add(itemLabel);
                                itemNode.Tag = item;

                                if (!string.IsNullOrWhiteSpace(item.Text)) {
                                    var itemTextNode = itemNode.Nodes.Add(item.Text);
                                    itemTextNode.Tag = item;   // ★ 号の本文にも Tag
                                }
                            }
                        }
                    }
                }

                // ★ ② 章直下の条（Article）も対応
                foreach (var art in chapter.Articles) {
                    var artNode = chapterNode.Nodes.Add($"{art.ArticleTitle}");
                    artNode.Tag = art;

                    foreach (var para in art.Paragraphs) {
                        string paraLabel = string.IsNullOrWhiteSpace(para.ParagraphNum)
                            ? "項番号なし"
                            : $"{para.ParagraphNum}項";

                        var paraNode = artNode.Nodes.Add(paraLabel);
                        paraNode.Tag = para;

                        if (!string.IsNullOrWhiteSpace(para.Text)) {
                            var textNode = paraNode.Nodes.Add(para.Text);
                            textNode.Tag = para;
                        }

                        foreach (var item in para.Items) {
                            string itemLabel = string.IsNullOrWhiteSpace(item.ItemNum)
                                ? "号番号なし"
                                : $"{ToKanjiNumber(item.ItemNum)}号";

                            var itemNode = paraNode.Nodes.Add(itemLabel);
                            itemNode.Tag = item;

                            if (!string.IsNullOrWhiteSpace(item.Text)) {
                                var itemTextNode = itemNode.Nodes.Add(item.Text);
                                itemTextNode.Tag = item;   // ★ 必須
                            }

                        }
                    }
                }
            }

            // -----------------------------------------
            // ③ 章に属さない条（Article）
            // -----------------------------------------
            if (lawBody.Articles.Count > 0) {
                var miscNode = CcTreeView1.Nodes.Add("（章に属さない条）");

                foreach (var art in lawBody.Articles) {
                    var artNode = miscNode.Nodes.Add($"{art.ArticleTitle}");
                    artNode.Tag = art;

                    foreach (var para in art.Paragraphs) {
                        string paraLabel = string.IsNullOrWhiteSpace(para.ParagraphNum)
                            ? "項番号なし"
                            : $"{para.ParagraphNum}項";

                        var paraNode = artNode.Nodes.Add(paraLabel);
                        paraNode.Tag = para;

                        if (!string.IsNullOrWhiteSpace(para.Text)) {
                            var textNode = paraNode.Nodes.Add(para.Text);
                            textNode.Tag = para;
                        }


                        foreach (var item in para.Items) {
                            string itemLabel = string.IsNullOrWhiteSpace(item.ItemNum)
                                ? "号番号なし"
                                : $"{item.ItemNum}号";

                            var itemNode = paraNode.Nodes.Add(itemLabel);
                            itemNode.Tag = item;

                            if (!string.IsNullOrWhiteSpace(item.Text)) {
                                var itemTextNode = itemNode.Nodes.Add(item.Text);
                                itemTextNode.Tag = item;   // ★ 必須
                            }
                        }
                    }
                }
            }

            // -----------------------------------------
            // ④ 附則（SupplProvision）
            // -----------------------------------------
            if (lawBody.SupplProvision != null) {
                var supplNode = CcTreeView1.Nodes.Add("附則");

                foreach (var para in lawBody.SupplProvision.Paragraphs) {
                    string paraLabel = string.IsNullOrWhiteSpace(para.ParagraphNum)
                        ? "附則項番号なし"
                        : $"{para.ParagraphNum}項";

                    var paraNode = supplNode.Nodes.Add(paraLabel);

                    if (!string.IsNullOrWhiteSpace(para.Text)) {
                        var textNode = paraNode.Nodes.Add(para.Text);
                        textNode.Tag = para;
                    }

                }
            }

            CcTreeView1.EndUpdate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcTreeView1_AfterSelect(object sender, TreeViewEventArgs e) {
            var vo = e.Node.Tag;

            // ★ 条（Article）
            if (vo is LawArticle art) {
                var sb = new StringBuilder();

                sb.AppendLine(art.ArticleTitle);
                sb.AppendLine();

                foreach (var para1 in art.Paragraphs) {
                    string label = string.IsNullOrWhiteSpace(para1.ParagraphNum)
                        ? "項番号なし"
                        : $"{para1.ParagraphNum}項";

                    sb.AppendLine($"【{label}】");

                    if (!string.IsNullOrWhiteSpace(para1.Text))
                        sb.AppendLine(para1.Text);

                    foreach (var item1 in para1.Items) {
                        string itemLabel = string.IsNullOrWhiteSpace(item1.ItemNum)
                            ? "号番号なし"
                            : $"{item1.ItemNum}号";

                        sb.AppendLine($"  ・{itemLabel}");

                        if (!string.IsNullOrWhiteSpace(item1.Text))
                            sb.AppendLine($"    {item1.Text}");
                    }

                    sb.AppendLine();
                }

                CcRichTextBox1.Text = sb.ToString();
                return;
            }

            // ★ 項（Paragraph）または「項の本文ノード」
            if (vo is LawParagraph para) {
                CcRichTextBox1.Text = para.Text;
                return;
            }

            // ★ 号（Item）または「号の本文ノード」
            if (vo is LawItem item) {
                CcRichTextBox1.Text = item.Text;
                return;
            }

            CcRichTextBox1.Clear();

        }
    }
}