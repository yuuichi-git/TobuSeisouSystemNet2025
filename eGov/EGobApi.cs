using System.Xml.Linq;

using Vo;

namespace EGov {
    public class EGovApi {
        private readonly HttpClient _client;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="client"></param>
        public EGovApi(HttpClient client = null) {
            _client = client ?? new HttpClient();
        }

        /// <summary>
        /// キーワード検索APIを実行し、LawItem のリストを返す
        /// </summary>
        public async Task<List<LawItem>> SearchAsync(string keyword) {
            /*
             * API V2 のエンドポイント
             */
            string url = $"https://laws.e-gov.go.jp/api/2/keyword?keyword={Uri.EscapeDataString(keyword)}&response_format=xml";

            var xml = await _client.GetStringAsync(url);
            var doc = XDocument.Parse(xml);

            List<LawItem> items = new();

            foreach (var item in doc.Descendants("item")) {
                LawItem lawItem = new();

                // --- law_info ---
                var lawInfo = item.Element("law_info");
                if (lawInfo != null) {
                    lawItem.LawInfo = new LawInfo {
                        LawType = (string)lawInfo.Element("law_type"),
                        LawId = (string)lawInfo.Element("law_id"),
                        LawNum = (string)lawInfo.Element("law_num"),
                        LawNumEra = (string)lawInfo.Element("law_num_era"),
                        LawNumYear = (string)lawInfo.Element("law_num_year"),
                        LawNumType = (string)lawInfo.Element("law_num_type"),
                        LawNumNum = (string)lawInfo.Element("law_num_num"),
                        PromulgationDate = ParseDate(lawInfo.Element("promulgation_date")?.Value)
                    };
                }

                // --- revision_info ---
                var rev = item.Element("revision_info");
                if (rev != null) {
                    lawItem.RevisionInfo = new RevisionInfo {
                        LawRevisionId = (string)rev.Element("law_revision_id"),
                        LawType = (string)rev.Element("law_type"),
                        LawTitle = (string)rev.Element("law_title"),
                        LawTitleKana = (string)rev.Element("law_title_kana"),
                        Abbrev = (string)rev.Element("abbrev"),
                        Category = (string)rev.Element("category"),
                        Updated = ParseDate(rev.Element("updated")?.Value),
                        AmendmentPromulgateDate = ParseDate(rev.Element("amendment_promulgate_date")?.Value),
                        AmendmentEnforcementDate = ParseDate(rev.Element("amendment_enforcement_date")?.Value),
                        AmendmentEnforcementComment = (string)rev.Element("amendment_enforcement_comment"),
                        AmendmentScheduledEnforcementDate = ParseDate(rev.Element("amendment_scheduled_enforcement_date")?.Value),
                        AmendmentLawId = (string)rev.Element("amendment_law_id"),
                        AmendmentLawTitle = (string)rev.Element("amendment_law_title"),
                        AmendmentLawTitleKana = (string)rev.Element("amendment_law_title_kana"),
                        AmendmentLawNum = (string)rev.Element("amendment_law_num"),
                        AmendmentType = (string)rev.Element("amendment_type"),
                        RepealStatus = (string)rev.Element("repeal_status"),
                        RepealDate = ParseDate(rev.Element("repeal_date")?.Value),
                        RemainInForce = ParseBool(rev.Element("remain_in_force")?.Value),
                        Mission = (string)rev.Element("mission"),
                        CurrentRevisionStatus = (string)rev.Element("current_revision_status")
                    };
                }

                // --- sentences ---
                foreach (var s in item.Descendants("sentence")) {
                    lawItem.Sentences.Add(new Sentence {
                        Position = (string)s.Element("position"),
                        Text = (string)s.Element("text")
                    });
                }

                items.Add(lawItem);
            }

            return items;
        }

        /// <summary>
        /// 法令名から最適一致の LawItem を返す
        /// </summary>
        public async Task<LawItem> FindBestMatchAsync(string lawTitle) {
            var list = await SearchAsync(lawTitle);

            if (list.Count == 0)
                return null;

            return SelectBestMatch(lawTitle, list);
        }

        /// <summary>
        /// 最適一致（完全一致 → 前方一致 → 部分一致 → 類似度）
        /// </summary>
        public LawItem SelectBestMatch(string input, List<LawItem> items) {
            // 完全一致
            var exact = items.FirstOrDefault(i => i.RevisionInfo?.LawTitle == input);
            if (exact != null)
                return exact;

            // 前方一致
            var starts = items.FirstOrDefault(i => i.RevisionInfo?.LawTitle?.StartsWith(input) == true);
            if (starts != null)
                return starts;

            // 部分一致
            var contains = items.FirstOrDefault(i => i.RevisionInfo?.LawTitle?.Contains(input) == true);
            if (contains != null)
                return contains;

            // 類似度
            return items
                .OrderBy(i => StringSimilarity.LevenshteinDistance(i.RevisionInfo?.LawTitle ?? "", input))
                .FirstOrDefault();
        }

        // --- Utility ---

        private static DateTime? ParseDate(string s) {
            if (DateTime.TryParse(s, out var dt))
                return dt;
            return null;
        }

        private static bool? ParseBool(string s) {
            if (bool.TryParse(s, out var b))
                return b;
            return null;
        }

        public static class StringSimilarity {
            public static int LevenshteinDistance(string s, string t) {
                if (string.IsNullOrEmpty(s))
                    return t?.Length ?? 0;
                if (string.IsNullOrEmpty(t))
                    return s.Length;

                var dp = new int[s.Length + 1, t.Length + 1];

                for (int i = 0; i <= s.Length; i++)
                    dp[i, 0] = i;
                for (int j = 0; j <= t.Length; j++)
                    dp[0, j] = j;

                for (int i = 1; i <= s.Length; i++) {
                    for (int j = 1; j <= t.Length; j++) {
                        int cost = s[i - 1] == t[j - 1] ? 0 : 1;

                        dp[i, j] = Math.Min(
                            Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1),
                            dp[i - 1, j - 1] + cost
                        );
                    }
                }

                return dp[s.Length, t.Length];
            }
        }
    }
}