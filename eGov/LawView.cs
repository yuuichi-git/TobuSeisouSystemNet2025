/*
 * 2026-03-05
 * 注意点：法令IDは枝番が存在する。だから法令番号で指定する。
 */
using System.Diagnostics;
using System.Text;
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
            LawDataResponse lawDataResponse = await this.GetLawDataAsync(_lawNum);
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

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lawNum">法令番号</param>
        /// <returns>Task<LawDataResponse></returns>
        private async Task<LawDataResponse> GetLawDataAsync(string lawNum) {
            XDocument? xDocument = await LawApiClient.GetLawDataXmlByLawNumAsync(lawNum);
            if (xDocument == null || xDocument.Root == null) {
                Debug.WriteLine("GetLawDataAsync: xDocument or Root is null");
                return null;
            }

            try {
                LawDataResponse lawDataResponse = MapFromXDocument(xDocument);
                if (lawDataResponse == null)
                    Debug.WriteLine("MapFromXDocument returned null");
                return lawDataResponse;
            } catch (Exception ex) {
                Debug.WriteLine($"GetLawDataAsync: MapFromXDocument threw: {ex}");
                return null;
            }
        }

        private void CcTreeView1_AfterSelect(object? sender, TreeViewEventArgs e) {



        }


        // -----------------------------
        //
        // XDocument → LawDataResponse のマッピング
        //
        // -----------------------------
        private LawDataResponse MapFromXDocument(XDocument xDocument) {
            LawDataResponse lawDataResponse = new();
            if (xDocument?.Root == null)
                return null;

            var root = xDocument.Root;

            // attached_files_info
            var attached = root.Element("attached_files_info");
            if (attached != null) {
                var attachedFilesInfo = new AttachedFilesInfo {
                    ImageData = (string?)attached.Element("image_data") ?? string.Empty
                };

                var attachedFilesNode = attached.Element("attached_files");
                if (attachedFilesNode != null) {
                    foreach (var af in attachedFilesNode.Elements("attached_file")) {
                        attachedFilesInfo.AttachedFiles.Add(new AttachedFile {
                            LawRevisionId = (string?)af.Element("law_revision_id") ?? string.Empty,
                            Src = (string?)af.Element("src") ?? string.Empty,
                            Updated = (string?)af.Element("updated") ?? string.Empty
                        });
                    }
                }

                lawDataResponse.AttachedFilesInfo = attachedFilesInfo;
            }

            // law_info
            var lawInfoNode = root.Element("law_info");
            if (lawInfoNode != null) {
                lawDataResponse.LawInfo = new LawInfo {
                    LawType = (string?)lawInfoNode.Element("law_type") ?? string.Empty,
                    LawId = (string?)lawInfoNode.Element("law_id") ?? string.Empty,
                    LawNum = (string?)lawInfoNode.Element("law_num") ?? string.Empty,
                    LawNumEra = (string?)lawInfoNode.Element("law_num_era") ?? string.Empty,
                    LawNumYear = (string?)lawInfoNode.Element("law_num_year") ?? string.Empty,
                    LawNumType = (string?)lawInfoNode.Element("law_num_type") ?? string.Empty,
                    LawNumNum = (string?)lawInfoNode.Element("law_num_num") ?? string.Empty,
                    PromulgationDate = (string?)lawInfoNode.Element("promulgation_date") ?? string.Empty
                };
            }

            // revision_info
            var revNode = root.Element("revision_info");
            if (revNode != null) {
                lawDataResponse.RevisionInfo = new RevisionInfo {
                    LawRevisionId = (string?)revNode.Element("law_revision_id") ?? string.Empty,
                    LawType = (string?)revNode.Element("law_type") ?? string.Empty,
                    LawTitle = (string?)revNode.Element("law_title") ?? string.Empty,
                    LawTitleKana = (string?)revNode.Element("law_title_kana") ?? string.Empty,
                    Abbrev = (string?)revNode.Element("abbrev") ?? string.Empty,
                    Category = (string?)revNode.Element("category") ?? string.Empty,
                    Updated = (string?)revNode.Element("updated") ?? string.Empty,
                    AmendmentPromulgateDate = (string?)revNode.Element("amendment_promulgate_date") ?? string.Empty,
                    AmendmentEnforcementDate = (string?)revNode.Element("amendment_enforcement_date") ?? string.Empty,
                    AmendmentEnforcementComment = (string?)revNode.Element("amendment_enforcement_comment") ?? string.Empty,
                    AmendmentScheduledEnforcementDate = (string?)revNode.Element("amendment_scheduled_enforcement_date") ?? string.Empty,
                    AmendmentLawId = (string?)revNode.Element("amendment_law_id") ?? string.Empty,
                    AmendmentLawTitle = (string?)revNode.Element("amendment_law_title") ?? string.Empty,
                    AmendmentLawTitleKana = (string?)revNode.Element("amendment_law_title_kana") ?? string.Empty,
                    AmendmentLawNum = (string?)revNode.Element("amendment_law_num") ?? string.Empty,
                    AmendmentType = (string?)revNode.Element("amendment_type") ?? string.Empty,
                    RepealStatus = (string?)revNode.Element("repeal_status") ?? string.Empty,
                    RepealDate = (string?)revNode.Element("repeal_date") ?? string.Empty,
                    RemainInForce = (string?)revNode.Element("remain_in_force") ?? string.Empty,
                    Mission = (string?)revNode.Element("mission") ?? string.Empty,
                    CurrentRevisionStatus = (string?)revNode.Element("current_revision_status") ?? string.Empty
                };
            }

            // law_full_text -> Law
            var fullTextNode = root.Element("law_full_text")?.Element("Law");
            if (fullTextNode != null) {
                var law = new Law {
                    Era = (string?)fullTextNode.Attribute("Era") ?? string.Empty,
                    Lang = (string?)fullTextNode.Attribute("Lang") ?? string.Empty,
                    LawType = (string?)fullTextNode.Attribute("LawType") ?? string.Empty,
                    Num = (string?)fullTextNode.Attribute("Num") ?? string.Empty,
                    PromulgateDay = (string?)fullTextNode.Attribute("PromulgateDay") ?? string.Empty,
                    PromulgateMonth = (string?)fullTextNode.Attribute("PromulgateMonth") ?? string.Empty,
                    Year = (string?)fullTextNode.Attribute("Year") ?? string.Empty,
                    LawNum = (string?)fullTextNode.Element("LawNum") ?? string.Empty
                };

                var body = fullTextNode.Element("LawBody");
                if (body != null) {
                    var titleNode = body.Element("LawTitle");
                    if (titleNode != null) {
                        law.LawBody = new LawBody {
                            LawTitle = new LawTitle {
                                Abbrev = (string?)titleNode.Attribute("Abbrev") ?? string.Empty,
                                AbbrevKana = (string?)titleNode.Attribute("AbbrevKana") ?? string.Empty,
                                Kana = (string?)titleNode.Attribute("Kana") ?? string.Empty,
                                Text = titleNode.Value ?? string.Empty
                            }
                        };
                    }

                    // MainProvision -> Articles
                    var main = body.Element("MainProvision");
                    if (main != null) {
                        var mp = new MainProvision();
                        foreach (var artNode in main.Elements("Article")) {
                            var art = new Article {
                                Num = (string?)artNode.Attribute("Num") ?? string.Empty,
                                Delete = (string?)artNode.Attribute("Delete") ?? string.Empty,
                                Hide = (string?)artNode.Attribute("Hide") ?? string.Empty,
                                ArticleCaption = (string?)artNode.Element("ArticleCaption") ?? string.Empty,
                                ArticleTitle = (string?)artNode.Element("ArticleTitle") ?? string.Empty
                            };

                            foreach (var pNode in artNode.Elements("Paragraph")) {
                                var para = new Paragraph {
                                    Num = (string?)pNode.Attribute("Num") ?? string.Empty,
                                    Hide = (string?)pNode.Attribute("Hide") ?? string.Empty,
                                    ParagraphCaption = (string?)pNode.Element("ParagraphCaption") ?? string.Empty,
                                    ParagraphNum = (string?)pNode.Element("ParagraphNum") ?? string.Empty
                                };

                                var ps = pNode.Element("ParagraphSentence");
                                if (ps != null) {
                                    var pSentence = new ParagraphSentence();
                                    foreach (var sNode in ps.Elements("Sentence")) {
                                        var sent = new Sentence {
                                            Num = (string?)sNode.Attribute("Num") ?? string.Empty,
                                            WritingMode = (string?)sNode.Attribute("WritingMode") ?? string.Empty,
                                            Function = (string?)sNode.Attribute("Function") ?? string.Empty,
                                            Text = ExtractMixedText(sNode)
                                        };
                                        pSentence.Sentences.Add(sent);
                                    }
                                    para.ParagraphSentence = pSentence;
                                }

                                // Items
                                foreach (var itemNode in pNode.Elements("Item")) {
                                    var item = new Item {
                                        Num = (string?)itemNode.Attribute("Num") ?? string.Empty,
                                        ItemTitle = (string?)itemNode.Element("ItemTitle") ?? string.Empty
                                    };

                                    var isNode = itemNode.Element("ItemSentence");
                                    if (isNode != null) {
                                        var isent = new ItemSentence();
                                        foreach (var sNode in isNode.Elements("Sentence")) {
                                            isent.Sentences.Add(new Sentence {
                                                Num = (string?)sNode.Attribute("Num") ?? string.Empty,
                                                WritingMode = (string?)sNode.Attribute("WritingMode") ?? string.Empty,
                                                Function = (string?)sNode.Attribute("Function") ?? string.Empty,
                                                Text = ExtractMixedText(sNode)
                                            });
                                        }
                                        item.ItemSentence = isent;
                                    }

                                    para.Items.Add(item);
                                }

                                art.Paragraphs.Add(para);
                            }

                            mp.Articles.Add(art);
                        }

                        law.LawBody.MainProvision = mp;
                    }

                    // SupplProvision
                    var suppl = body.Element("SupplProvision");
                    if (suppl != null) {
                        var sp = new SupplProvision {
                            SupplProvisionLabel = (string?)suppl.Element("SupplProvisionLabel") ?? string.Empty
                        };

                        foreach (var aNode in suppl.Elements("Article")) {
                            var a = new Article {
                                Num = (string?)aNode.Attribute("Num") ?? string.Empty,
                                ArticleCaption = (string?)aNode.Element("ArticleCaption") ?? string.Empty,
                                ArticleTitle = (string?)aNode.Element("ArticleTitle") ?? string.Empty
                            };

                            foreach (var pNode in aNode.Elements("Paragraph")) {
                                var p = new Paragraph {
                                    Num = (string?)pNode.Attribute("Num") ?? string.Empty,
                                    ParagraphCaption = (string?)pNode.Element("ParagraphCaption") ?? string.Empty,
                                    ParagraphNum = (string?)pNode.Element("ParagraphNum") ?? string.Empty
                                };

                                var ps = pNode.Element("ParagraphSentence");
                                if (ps != null) {
                                    var pSentence = new ParagraphSentence();
                                    foreach (var sNode in ps.Elements("Sentence")) {
                                        pSentence.Sentences.Add(new Sentence {
                                            Num = (string?)sNode.Attribute("Num") ?? string.Empty,
                                            WritingMode = (string?)sNode.Attribute("WritingMode") ?? string.Empty,
                                            Function = (string?)sNode.Attribute("Function") ?? string.Empty,
                                            Text = ExtractMixedText(sNode)
                                        });
                                    }
                                    p.ParagraphSentence = pSentence;
                                }

                                a.Paragraphs.Add(p);
                            }

                            sp.Articles.Add(a);
                        }

                        law.LawBody.SupplProvision = sp;
                    }
                }

                lawDataResponse.LawFullText = new LawFullText { Law = law };
            }

            return lawDataResponse;
        }

        private string ExtractMixedText(XElement sentenceElement) {
            if (sentenceElement == null)
                return string.Empty;

            StringBuilder stringBuilder = new();
            foreach (XNode xNode in sentenceElement.Nodes()) {
                if (xNode is XText txt) {
                    stringBuilder.Append(txt.Value);
                } else if (xNode is XElement el) {
                    if (string.Equals(el.Name.LocalName, "Ruby", StringComparison.OrdinalIgnoreCase)) {
                        // Ruby の本文テキスト（ルビ以外のテキスト）と Rt を括弧で付ける例
                        var rt = (string?)el.Element("Rt") ?? string.Empty;
                        // Ruby の直下のテキストノードを取得
                        var baseText = string.Concat(el.Nodes().OfType<XText>().Select(t => t.Value)).Trim();
                        if (!string.IsNullOrEmpty(baseText)) {
                            stringBuilder.Append(baseText);
                            if (!string.IsNullOrEmpty(rt))
                                stringBuilder.Append($"({rt})");
                        } else {
                            stringBuilder.Append(el.Value);
                        }
                    } else {
                        stringBuilder.Append(el.Value);
                    }
                }
            }
            return stringBuilder.ToString().Trim();
        }
    }
}