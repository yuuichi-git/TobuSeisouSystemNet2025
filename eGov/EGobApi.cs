/*
 * string url = $"https://laws.e-gov.go.jp/api/2/keyword?keyword={Uri.EscapeDataString(lawName)}&response_format=xml"; 大麻草の栽培の規制に関する法律
 * string url = $"https://laws.e-gov.go.jp/api/2/law_data/{lawId}?response_format=xml"; 322AC0000000067
 */
using System.Xml.Linq;

using Vo;

namespace EGov {
    public class EGovApi {
        private readonly HttpClient _httpClient = new();

        public EGovApi() {

        }

        /// <summary>
        /// keyword
        /// https://laws.e-gov.go.jp/api/2/keyword?keyword={lawName}&response_format=xml から法令を検索し、完全一致するものを返す
        /// </summary>
        /// <param name="lawName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<LawSearchResultVO> GetLawByKeywordAsync(string lawName) {
            string url = $"https://laws.e-gov.go.jp/api/2/keyword?keyword={Uri.EscapeDataString(lawName)}&response_format=xml";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string xml = await response.Content.ReadAsStringAsync();
            var doc = XDocument.Parse(xml);

            var items = doc.Root?
                .Element("items")?
                .Elements("item")
                .ToList();

            if (items == null || items.Count == 0)
                throw new Exception($"法令が見つかりません: {lawName}");

            // ✔ 完全一致を探す
            var exact = items.FirstOrDefault(item => {
                var title = item.Element("revision_info")?
                                .Element("law_title")?.Value;
                return title == lawName;
            });

            if (exact == null)
                throw new Exception($"完全一致する法令が見つかりません: {lawName}");

            // ✔ LawSearchResultVO に詰める
            var result = new LawSearchResultVO {
                TotalCount = 1,
                SentenceCount = 0
            };

            var lawItem = new LawItemVO {
                LawInfo = new LawInfoVO {
                    LawType = exact.Element("law_info")?.Element("law_type")?.Value ?? "",
                    LawId = exact.Element("law_info")?.Element("law_id")?.Value ?? "",
                    LawNum = exact.Element("law_info")?.Element("law_num")?.Value ?? "",
                    LawNumEra = exact.Element("law_info")?.Element("law_num_era")?.Value ?? "",
                    LawNumYear = int.TryParse(exact.Element("law_info")?.Element("law_num_year")?.Value, out var y) ? y : 0,
                    LawNumType = exact.Element("law_info")?.Element("law_num_type")?.Value ?? "",
                    LawNumNum = exact.Element("law_info")?.Element("law_num_num")?.Value ?? "",
                    PromulgationDate = exact.Element("law_info")?.Element("promulgation_date")?.Value ?? ""
                },
                RevisionInfo = new RevisionInfoVO {
                    LawRevisionId = exact.Element("revision_info")?.Element("law_revision_id")?.Value ?? "",
                    LawType = exact.Element("revision_info")?.Element("law_type")?.Value ?? "",
                    LawTitle = exact.Element("revision_info")?.Element("law_title")?.Value ?? "",
                    LawTitleKana = exact.Element("revision_info")?.Element("law_title_kana")?.Value ?? "",
                    Abbrev = exact.Element("revision_info")?.Element("abbrev")?.Value ?? "",
                    Category = exact.Element("revision_info")?.Element("category")?.Value ?? "",
                    Updated = exact.Element("revision_info")?.Element("updated")?.Value ?? "",
                    AmendmentPromulgateDate = exact.Element("revision_info")?.Element("amendment_promulgate_date")?.Value ?? "",
                    AmendmentEnforcementDate = exact.Element("revision_info")?.Element("amendment_enforcement_date")?.Value ?? "",
                    AmendmentEnforcementComment = exact.Element("revision_info")?.Element("amendment_enforcement_comment")?.Value,
                    AmendmentScheduledEnforcementDate = exact.Element("revision_info")?.Element("amendment_scheduled_enforcement_date")?.Value,
                    AmendmentLawId = exact.Element("revision_info")?.Element("amendment_law_id")?.Value ?? "",
                    AmendmentLawTitle = exact.Element("revision_info")?.Element("amendment_law_title")?.Value ?? "",
                    AmendmentLawTitleKana = exact.Element("revision_info")?.Element("amendment_law_title_kana")?.Value,
                    AmendmentLawNum = exact.Element("revision_info")?.Element("amendment_law_num")?.Value ?? "",
                    AmendmentType = exact.Element("revision_info")?.Element("amendment_type")?.Value ?? "",
                    RepealStatus = exact.Element("revision_info")?.Element("repeal_status")?.Value ?? "",
                    RepealDate = exact.Element("revision_info")?.Element("repeal_date")?.Value,
                    RemainInForce = bool.TryParse(exact.Element("revision_info")?.Element("remain_in_force")?.Value, out var b) && b,
                    Mission = exact.Element("revision_info")?.Element("mission")?.Value ?? "",
                    CurrentRevisionStatus = exact.Element("revision_info")?.Element("current_revision_status")?.Value ?? ""
                }
            };

            result.Items.Add(lawItem);

            return result;
        }

        /// <summary>
        /// law_data
        /// https://laws.e-gov.go.jp/api/2/law_data/{lawId}?response_format=xml から法令の詳細を取得する
        /// </summary>
        /// <param name="lawId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<LawDetailVO> GetLawDetailAsync(string lawId) {
            string url = $"https://laws.e-gov.go.jp/api/2/law_data/{lawId}?response_format=xml";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string xml = await response.Content.ReadAsStringAsync();
            var doc = XDocument.Parse(xml);

            var root = doc.Root;
            if (root is null)
                throw new Exception("law_data の XML が不正です。");

            LawDetailVO lawDetailVO = new();
            lawDetailVO.AttachedFilesInfo = null;
            lawDetailVO.LawInfo = ParseLawInfo(root.Element("law_info"));
            lawDetailVO.RevisionInfo = ParseRevisionInfo(root.Element("revision_info"));
            lawDetailVO.LawFullText = ParseFullTextNode(root.Element("law_full_text"));

            return lawDetailVO;
        }


        private LawInfoVO ParseLawInfo(XElement? e) {
            if (e == null)
                return new();

            return new LawInfoVO {
                LawType = e.Element("law_type")?.Value ?? "",
                LawId = e.Element("law_id")?.Value ?? "",
                LawNum = e.Element("law_num")?.Value ?? "",
                LawNumEra = e.Element("law_num_era")?.Value ?? "",
                LawNumYear = int.TryParse(e.Element("law_num_year")?.Value, out var y) ? y : 0,
                LawNumType = e.Element("law_num_type")?.Value ?? "",
                LawNumNum = e.Element("law_num_num")?.Value ?? "",
                PromulgationDate = e.Element("promulgation_date")?.Value ?? ""
            };
        }

        private RevisionInfoVO ParseRevisionInfo(XElement? e) {
            if (e == null)
                return new();

            return new RevisionInfoVO {
                LawRevisionId = e.Element("law_revision_id")?.Value ?? "",
                LawType = e.Element("law_type")?.Value ?? "",
                LawTitle = e.Element("law_title")?.Value ?? "",
                LawTitleKana = e.Element("law_title_kana")?.Value ?? "",
                Abbrev = e.Element("abbrev")?.Value ?? "",
                Category = e.Element("category")?.Value ?? "",
                Updated = e.Element("updated")?.Value ?? "",
                AmendmentPromulgateDate = e.Element("amendment_promulgate_date")?.Value ?? "",
                AmendmentEnforcementDate = e.Element("amendment_enforcement_date")?.Value ?? "",
                AmendmentEnforcementComment = e.Element("amendment_enforcement_comment")?.Value,
                AmendmentScheduledEnforcementDate = e.Element("amendment_scheduled_enforcement_date")?.Value,
                AmendmentLawId = e.Element("amendment_law_id")?.Value ?? "",
                AmendmentLawTitle = e.Element("amendment_law_title")?.Value ?? "",
                AmendmentLawTitleKana = e.Element("amendment_law_title_kana")?.Value,
                AmendmentLawNum = e.Element("amendment_law_num")?.Value ?? "",
                AmendmentType = e.Element("amendment_type")?.Value ?? "",
                RepealStatus = e.Element("repeal_status")?.Value ?? "",
                RepealDate = e.Element("repeal_date")?.Value,
                RemainInForce = bool.TryParse(e.Element("remain_in_force")?.Value, out var b) && b,
                Mission = e.Element("mission")?.Value ?? "",
                CurrentRevisionStatus = e.Element("current_revision_status")?.Value ?? ""
            };
        }

        private LawFullTextNodeVO ParseFullTextNode(XElement? e) {
            var node = new LawFullTextNodeVO();

            if (e == null)
                return node;

            node.Tag = e.Name.LocalName;

            // 属性
            foreach (var attr in e.Attributes())
                node.Attr[attr.Name.LocalName] = attr.Value;

            // 子ノード
            foreach (var child in e.Nodes()) {
                switch (child) {
                    case XElement ce:
                        node.Children.Add(ParseFullTextNode(ce));
                        break;

                    case XCData cdata:
                        var cdataValue = cdata.Value.Trim();
                        if (!string.IsNullOrEmpty(cdataValue))
                            node.Children.Add(cdataValue);
                        break;

                    case XText text:
                        var value = text.Value.Trim();
                        if (!string.IsNullOrEmpty(value))
                            node.Children.Add(value);
                        break;

                    case XComment:
                        // コメントは無視
                        break;

                    default:
                        // その他のノード（ProcessingInstruction など）
                        var raw = child.ToString().Trim();
                        if (!string.IsNullOrEmpty(raw))
                            node.Children.Add(raw);
                        break;
                }
            }

            return node;
        }







    }
}
