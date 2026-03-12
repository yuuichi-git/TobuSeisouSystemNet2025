/*
 * string url = $"https://laws.e-gov.go.jp/api/2/keyword?keyword={Uri.EscapeDataString(lawName)}&response_format=xml"; 大麻草の栽培の規制に関する法律
 * string url = $"https://laws.e-gov.go.jp/api/2/law_data/{lawId}?response_format=xml"; 322AC0000000067
 */
using System.Xml.Linq;

using Vo;

namespace EGov {

    public class EGobApi {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public EGobApi() {
            _httpClient = new HttpClient();
        }

        /*
         * 
         * keyword
         * 
         */
        public async Task<KeywordResponse> GetLawInfoAsync(string lawName) {
            string url = $"https://laws.e-gov.go.jp/api/2/keyword?keyword={Uri.EscapeDataString(lawName)}&response_format=xml";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string xml = await response.Content.ReadAsStringAsync();
            var doc = XDocument.Parse(xml);

            var root = doc.Root!;
            KeywordResponse keywordResponse = new();

            // ---- メタ情報 ----
            keywordResponse.TotalCount = (int?)root.Element("total_count") ?? 0;
            keywordResponse.SentenceCount = (int?)root.Element("sentence_count") ?? 0;
            keywordResponse.NextOffset = (int?)root.Element("next_offset") ?? 0;

            List<KeywordItem> allItems = new();

            // ---- items ----
            var itemsElem = root.Element("items");
            if (itemsElem != null) {
                foreach (var itemElem in itemsElem.Elements("item")) {
                    var item = new KeywordItem();

                    // ---- law_info ----
                    var lawInfoElem = itemElem.Element("law_info");
                    if (lawInfoElem != null) {
                        item.LawInfo = new KeywordLawInfo {
                            LawType = (string?)lawInfoElem.Element("law_type") ?? "",
                            LawId = (string?)lawInfoElem.Element("law_id") ?? "",
                            LawNum = (string?)lawInfoElem.Element("law_num") ?? "",
                            LawNumEra = (string?)lawInfoElem.Element("law_num_era") ?? "",
                            LawNumYear = (int?)lawInfoElem.Element("law_num_year") ?? 0,
                            LawNumType = (string?)lawInfoElem.Element("law_num_type") ?? "",
                            LawNumNum = (string?)lawInfoElem.Element("law_num_num") ?? "",
                            PromulgationDate = ParseDate(lawInfoElem.Element("promulgation_date")?.Value)
                        };
                    }

                    // ---- revision_info ----
                    var revElem = itemElem.Element("revision_info");
                    if (revElem != null) {
                        item.RevisionInfo = new KeywordRevisionInfo {
                            LawRevisionId = (string?)revElem.Element("law_revision_id") ?? "",
                            LawType = (string?)revElem.Element("law_type") ?? "",
                            LawTitle = (string?)revElem.Element("law_title") ?? "",
                            LawTitleKana = (string?)revElem.Element("law_title_kana") ?? "",
                            Abbrev = (string?)revElem.Element("abbrev") ?? "",
                            Category = (string?)revElem.Element("category") ?? "",
                            Updated = ParseDate(revElem.Element("updated")?.Value),
                            AmendmentPromulgateDate = ParseDate(revElem.Element("amendment_promulgate_date")?.Value),
                            AmendmentEnforcementDate = ParseDate(revElem.Element("amendment_enforcement_date")?.Value),
                            AmendmentEnforcementComment = (string?)revElem.Element("amendment_enforcement_comment") ?? "",
                            AmendmentScheduledEnforcementDate = (string?)revElem.Element("amendment_scheduled_enforcement_date") ?? "",
                            AmendmentLawId = (string?)revElem.Element("amendment_law_id") ?? "",
                            AmendmentLawTitle = (string?)revElem.Element("amendment_law_title") ?? "",
                            AmendmentLawTitleKana = (string?)revElem.Element("amendment_law_title_kana") ?? "",
                            AmendmentLawNum = (string?)revElem.Element("amendment_law_num") ?? "",
                            AmendmentType = (int?)revElem.Element("amendment_type") ?? 0,
                            RepealStatus = (string?)revElem.Element("repeal_status") ?? "",
                            RepealDate = (string?)revElem.Element("repeal_date") ?? "",
                            RemainInForce = (bool?)revElem.Element("remain_in_force") ?? false,
                            Mission = (string?)revElem.Element("mission") ?? "",
                            CurrentRevisionStatus = (string?)revElem.Element("current_revision_status") ?? ""
                        };
                    }

                    // ---- sentences ----
                    var sentencesElem = itemElem.Element("sentences");
                    if (sentencesElem != null) {
                        foreach (var sElem in sentencesElem.Elements("sentence")) {
                            item.Sentences.Add(new KeywordSentence {
                                Position = (string?)sElem.Element("position") ?? "",
                                Text = (string?)sElem.Element("text") ?? ""
                            });
                        }
                    }

                    allItems.Add(item);
                }
            }


            // ---- ★ 完全一致フィルタをここで適用 ★ ----
            keywordResponse.Items = allItems.Where(i => i.RevisionInfo?.LawTitle == lawName ||
                                                   i.LawInfo?.LawNum == lawName ||     // 法令番号で一致したい場合
                                                   i.LawInfo?.LawId == lawName         // law_id で一致したい場合
                                                   ).ToList();
            return keywordResponse;
        }


        /*
         * 
         * law_data
         * string lawId, string lawArticle = null, string lawParagraph = null
         * 
         */
        public async Task<LawDataResponse> GetLawDataAsync(string lawId, string lawArticle = null, string lawParagraph = null) {
            if (string.IsNullOrWhiteSpace(lawId))
                throw new ArgumentException("lawId is required.", nameof(lawId));

            string url = $"https://laws.e-gov.go.jp/api/2/law_data/{lawId}?response_format=xml";

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string xml = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(xml))
                throw new Exception("API returned empty XML.");

            XDocument doc = XDocument.Parse(xml);
            XElement root = doc.Root!; // <law_data_response>

            LawDataResponse result = new LawDataResponse();

            // -------------------------
            // law_info → LawDataResponse
            // -------------------------
            XElement? lawInfo = root.Element("law_info");
            if (lawInfo != null) {
                result.LawNum = (string?)lawInfo.Element("law_num") ?? "";
                result.LawTitle = (string?)root.Element("revision_info")?.Element("law_title") ?? "";
                result.LawTitleKana = (string?)root.Element("revision_info")?.Element("law_title_kana") ?? "";

                result.PromulgationDate = ParseDate(lawInfo.Element("promulgation_date")?.Value);
            }

            // -------------------------
            // law_full_text → LawBody
            // -------------------------
            XElement? fullText = root.Element("law_full_text");
            if (fullText != null) {
                XElement? lawElem = fullText.Element("Law");
                if (lawElem != null) {
                    XElement? bodyElem = lawElem.Element("LawBody");
                    if (bodyElem != null) {
                        LawBody body = new();
                        result.LawBody = body;

                        // ★ LawBody の中身をすべてパース（Chapter / Article / Paragraph / Item / Sentence）
                        this.ParseLawBody(bodyElem, body);
                    }
                }
            }

            return result;

        }














        private DateTime? ParseDate(string? value) {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            if (DateTime.TryParse(value, out var dt))
                return dt;

            return null;
        }

        /*
         * 
         * パーサー
         * 
         */

        private void ParseLawBody(XElement bodyElem, LawBody body) {
            // -------------------------
            // 前文（LawTitle の後にある場合もある）
            // -------------------------
            XElement? prefaceElem = bodyElem.Element("Preface");
            if (prefaceElem != null) {
                body.Preface = new LawPreface {
                    Text = (string?)prefaceElem.Value ?? ""
                };
            }

            // -------------------------
            // MainProvision（本文）
            // -------------------------
            XElement? mainProvision = bodyElem.Element("MainProvision");
            if (mainProvision != null) {
                foreach (XElement chapterElem in mainProvision.Elements("Chapter")) {
                    LawChapter chapter = ParseChapter(chapterElem);
                    body.Chapters.Add(chapter);
                }

                // 附則
                XElement? supplElem = mainProvision.Element("SupplProvision");
                if (supplElem != null) {
                    body.SupplProvision = ParseSupplProvision(supplElem);
                }
            }
        }

        /*
         * -----------------------------------------
         * Chapter（章）
         * -----------------------------------------
         */
        private LawChapter ParseChapter(XElement chapterElem) {
            LawChapter chapter = new LawChapter {
                ChapterNum = (string?)chapterElem.Attribute("Num") ?? "",
                ChapterTitle = (string?)chapterElem.Element("ChapterTitle") ?? ""
            };

            foreach (XElement articleElem in chapterElem.Elements("Article")) {
                LawArticle article = ParseArticle(articleElem);
                chapter.Articles.Add(article);
            }

            return chapter;
        }


        /*
         * -----------------------------------------
         * Article（条）
         * -----------------------------------------
         */
        private LawArticle ParseArticle(XElement articleElem) {
            LawArticle article = new LawArticle {
                ArticleNum = (string?)articleElem.Attribute("Num") ?? "",
                ArticleTitle = (string?)articleElem.Element("ArticleTitle") ?? ""
            };

            foreach (XElement paragraphElem in articleElem.Elements("Paragraph")) {
                LawParagraph paragraph = ParseParagraph(paragraphElem);
                article.Paragraphs.Add(paragraph);
            }

            return article;
        }


        /*
         * -----------------------------------------
         * Paragraph（項）
         * -----------------------------------------
         */
        private LawParagraph ParseParagraph(XElement paragraphElem) {
            LawParagraph paragraph = new LawParagraph {
                ParagraphNum = (string?)paragraphElem.Element("ParagraphNum") ?? "",
                Text = "" // Sentence が複数あるのでここでは空
            };

            // ParagraphSentence → Sentence
            XElement? sentenceContainer = paragraphElem.Element("ParagraphSentence");
            if (sentenceContainer != null) {
                foreach (XElement sentenceElem in sentenceContainer.Elements("Sentence")) {
                    string text = (string?)sentenceElem.Value ?? "";
                    paragraph.Items.Add(new LawItem {
                        ItemNum = "", // ParagraphSentence 直下の Sentence は号ではない
                        Text = text
                    });
                }
            }

            // Item（号）
            foreach (XElement itemElem in paragraphElem.Elements("Item")) {
                LawItem item = ParseItem(itemElem);
                paragraph.Items.Add(item);
            }

            return paragraph;
        }


        /*
         * -----------------------------------------
         * Item（号）
         * -----------------------------------------
         */
        private LawItem ParseItem(XElement itemElem) {
            LawItem item = new LawItem {
                ItemNum = (string?)itemElem.Attribute("Num") ?? "",
                Text = ""
            };

            XElement? itemSentence = itemElem.Element("ItemSentence");
            if (itemSentence != null) {
                XElement? sentenceElem = itemSentence.Element("Sentence");
                if (sentenceElem != null) {
                    item.Text = (string?)sentenceElem.Value ?? "";
                }
            }

            return item;
        }

        /*
         * -----------------------------------------
         * SupplProvision（附則）
         * -----------------------------------------
         */
        private LawSupplProvision ParseSupplProvision(XElement supplElem) {
            LawSupplProvision sp = new LawSupplProvision();

            foreach (XElement paraElem in supplElem.Elements("Paragraph")) {
                LawSupplParagraph p = new LawSupplParagraph {
                    ParagraphNum = (string?)paraElem.Element("ParagraphNum") ?? "",
                    Text = (string?)paraElem.Element("ParagraphSentence")?.Element("Sentence")?.Value ?? ""
                };

                sp.Paragraphs.Add(p);
            }

            return sp;
        }





    }
}