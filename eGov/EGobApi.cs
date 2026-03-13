/*
 * string url = $"https://laws.e-gov.go.jp/api/2/keyword?keyword={Uri.EscapeDataString(lawName)}&response_format=xml"; 大麻草の栽培の規制に関する法律
 * string url = $"https://laws.e-gov.go.jp/api/2/law_data/{lawId}?response_format=xml"; 322AC0000000067
 */
using System.Globalization;
using System.Xml.Linq;

using Vo;

namespace EGov {

    public class EGobApi {
        private readonly HttpClient _httpClient = new();

        /*
         * 
         * keyword検索
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
         * law_data検索
         * 
         */
        public async Task<LawDataResponse> GetLawDataAsync(string lawId, string? lawArticle = null, string? lawParagraph = null) {
            if (string.IsNullOrWhiteSpace(lawId))
                throw new ArgumentException("lawId is required.", nameof(lawId));

            string url = $"https://laws.e-gov.go.jp/api/2/law_data/{lawId}?response_format=xml";

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string xml = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(xml))
                throw new Exception("API returned empty XML.");

            // ★ ここが重要：LawBody を探す
            var doc = XDocument.Parse(xml);

            var lawBodyElem = doc
                .Descendants()
                .FirstOrDefault(e => e.Name.LocalName == "LawBody");

            if (lawBodyElem == null)
                throw new Exception("LawBody element not found in XML.");

            // ★ LawBody 専用の入口でパース
            var lawDataResponse = LawDataConverter.FromLawBody(lawBodyElem);

            return lawDataResponse;

        }

        private DateTime? ParseDate(string? value) {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            if (DateTime.TryParse(value, out var dt))
                return dt;

            return null;
        }






        public static class LawDataConverter {
            // ============================================================
            // 入口：LawBody を直接パースする
            // ============================================================
            public static LawDataResponse FromLawBody(XElement lawBodyElem) {
                if (lawBodyElem == null)
                    throw new ArgumentNullException(nameof(lawBodyElem));

                var res = new LawDataResponse();

                // -----------------------------
                // LawTitle（本文タイトル）
                // -----------------------------
                var titleElem = lawBodyElem.Elements()
                    .FirstOrDefault(e => e.Name.LocalName == "LawTitle");

                if (titleElem != null) {
                    res.LawTitle = titleElem.Value.Trim();
                    res.LawTitleKana = (string?)titleElem.Attribute("Kana") ?? "";
                }

                // -----------------------------
                // LawNum（法律番号）
                // LawBody の親（Law）にある
                // -----------------------------
                var lawElem = lawBodyElem.Parent;
                if (lawElem != null) {
                    var lawNumElem = lawElem.Elements()
                        .FirstOrDefault(e => e.Name.LocalName == "LawNum");

                    if (lawNumElem != null)
                        res.LawNum = lawNumElem.Value.Trim();
                }

                // -----------------------------
                // PromulgationDate（公布日）
                // Law の属性 Year / PromulgateMonth / PromulgateDay
                // -----------------------------
                if (lawElem != null) {
                    var year = (string?)lawElem.Attribute("Year");
                    var month = (string?)lawElem.Attribute("PromulgateMonth");
                    var day = (string?)lawElem.Attribute("PromulgateDay");

                    if (int.TryParse(year, out var y) &&
                        int.TryParse(month, out var m) &&
                        int.TryParse(day, out var d)) {
                        // 昭和23 → 1948 などの元号変換は後で必要なら追加
                        res.PromulgationDate = new DateTime(y + 1925, m, d);
                    }
                }

                // -----------------------------
                // 本文パース
                // -----------------------------
                res.LawBody = ParseLawBody(lawBodyElem);

                return res;
            }

            // ============================================================
            // LawBody のパース
            // ============================================================
            private static LawBody ParseLawBody(XElement lawBodyElem) {
                var body = new LawBody();

                // -----------------------------
                // Preface（前文）
                // -----------------------------
                var prefaceElem = lawBodyElem.Elements()
                    .FirstOrDefault(e => e.Name.LocalName == "Preface");

                if (prefaceElem != null) {
                    body.Preface = new LawPreface {
                        Text = ExtractAllText(prefaceElem)
                    };
                }

                // -----------------------------
                // MainProvision（本文）
                // -----------------------------
                var main = lawBodyElem.Elements()
                    .FirstOrDefault(e => e.Name.LocalName == "MainProvision");

                if (main != null) {
                    foreach (var child in main.Elements()) {
                        switch (child.Name.LocalName) {
                            case "Chapter":
                                body.Chapters.Add(ParseChapter(child));
                                break;

                            case "Section":
                                body.Sections.Add(ParseSection(child));
                                break;

                            case "Subsection":
                                body.Subsections.Add(ParseSubsection(child));
                                break;

                            case "Division":
                                body.Divisions.Add(ParseDivision(child));
                                break;

                            case "Article":
                                body.Articles.Add(ParseArticle(child));
                                break;
                        }
                    }
                }

                // -----------------------------
                // SupplProvision（附則）
                // -----------------------------
                var suppl = lawBodyElem.Elements()
                    .FirstOrDefault(e => e.Name.LocalName == "SupplProvision");

                if (suppl != null) {
                    body.SupplProvision = ParseSupplProvision(suppl);
                }

                return body;
            }

            // ============================================================
            // Chapter / Section / Subsection / Division / Article
            // ============================================================
            private static LawChapter ParseChapter(XElement elem) {
                var chapter = new LawChapter {
                    ChapterNum = (string?)elem.Attribute("Num") ?? "",
                    ChapterTitle = GetChildValue(elem, "ChapterTitle"),
                };

                // ★ Section を拾う
                foreach (var sec in elem.Elements().Where(e => e.Name.LocalName == "Section"))
                    chapter.Sections.Add(ParseSection(sec));

                // 章直下の Article（ある場合）
                foreach (var art in elem.Elements().Where(e => e.Name.LocalName == "Article"))
                    chapter.Articles.Add(ParseArticle(art));

                return chapter;
            }

            private static LawSection ParseSection(XElement elem) {
                var section = new LawSection {
                    SectionNum = (string?)elem.Attribute("Num") ?? "",
                    SectionTitle = GetChildValue(elem, "SectionTitle"),
                };

                foreach (var art in elem.Elements().Where(e => e.Name.LocalName == "Article"))
                    section.Articles.Add(ParseArticle(art));

                return section;
            }


            private static LawSubsection ParseSubsection(XElement elem) {
                var subsection = new LawSubsection {
                    SubsectionNum = GetChildValue(elem, "SubsectionNum"),
                    SubsectionTitle = GetChildValue(elem, "SubsectionTitle"),
                };

                foreach (var art in elem.Elements().Where(e => e.Name.LocalName == "Article"))
                    subsection.Articles.Add(ParseArticle(art));

                return subsection;
            }

            private static LawDivision ParseDivision(XElement elem) {
                var division = new LawDivision {
                    DivisionNum = GetChildValue(elem, "DivisionNum"),
                    DivisionTitle = GetChildValue(elem, "DivisionTitle"),
                };

                foreach (var art in elem.Elements().Where(e => e.Name.LocalName == "Article"))
                    division.Articles.Add(ParseArticle(art));

                return division;
            }

            private static LawArticle ParseArticle(XElement elem) {
                var article = new LawArticle {
                    ArticleTitle = GetChildValue(elem, "ArticleTitle"),
                };

                // Paragraph を読み込む
                foreach (var paraElem in elem.Elements().Where(e => e.Name.LocalName == "Paragraph"))
                    article.Paragraphs.Add(ParseParagraph(paraElem));

                return article;
            }


            // ============================================================
            // Paragraph / Item
            // ============================================================
            private static LawParagraph ParseParagraph(XElement elem) {
                var para = new LawParagraph {
                    ParagraphNum = GetChildValue(elem, "ParagraphNum"),
                    Text = ExtractParagraphText(elem),
                };

                foreach (var item in elem.Elements().Where(e => e.Name.LocalName == "Item"))
                    para.Items.Add(ParseItem(item));

                return para;
            }

            private static LawItem ParseItem(XElement elem) {
                return new LawItem {
                    ItemNum = GetChildValue(elem, "ItemTitle"),
                    Text = ExtractItemText(elem),
                };
            }

            // ============================================================
            // SupplProvision（附則）
            // ============================================================
            private static LawSupplProvision ParseSupplProvision(XElement elem) {
                var suppl = new LawSupplProvision();

                foreach (var para in elem.Elements().Where(e => e.Name.LocalName == "Paragraph")) {
                    suppl.Paragraphs.Add(new LawSupplParagraph {
                        ParagraphNum = GetChildValue(para, "ParagraphNum"),
                        Text = ExtractParagraphText(para),
                    });
                }

                return suppl;
            }

            // ============================================================
            // テキスト抽出
            // ============================================================
            private static string ExtractParagraphText(XElement elem) {
                var sentences = elem
                    .Descendants()
                    .Where(e => e.Name.LocalName == "Sentence")
                    .Select(e => e.Value)
                    .ToList();

                return string.Join("", sentences);
            }

            private static string ExtractItemText(XElement elem) {
                var sentences = elem
                    .Descendants()
                    .Where(e => e.Name.LocalName == "Sentence")
                    .Select(e => e.Value)
                    .ToList();

                return string.Join("", sentences);
            }

            private static string ExtractAllText(XElement elem) {
                return string.Concat(elem
                    .DescendantNodes()
                    .OfType<XText>()
                    .Select(t => t.Value));
            }

            // ============================================================
            // ユーティリティ
            // ============================================================
            private static string GetChildValue(XElement parent, string localName) {
                var e = parent.Elements().FirstOrDefault(x => x.Name.LocalName == localName);
                return e != null ? e.Value.Trim() : "";
            }
        }



    }
}