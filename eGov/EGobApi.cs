/*
 * string url = $"https://laws.e-gov.go.jp/api/2/keyword?keyword={Uri.EscapeDataString(lawNam)}&response_format=xml"; 大麻草の栽培の規制に関する法律  昭和23年法律第186号
 * string url = $"https://laws.e-gov.go.jp/api/2/law_data/{lawId}?response_format=xml"; 322AC0000000067
 * string url = $"https://laws.e-gov.go.jp/api/2/law_data/323AC0000000124?article=3;response_format=xml
 * string url = $"https://laws.e-gov.go.jp/api/2/laws?law_num=%E6%98%AD%E5%92%8C%E4%BA%94%E5%8D%81%E5%85%AB%E5%B9%B4%E6%B3%95%E5%BE%8B%E7%AC%AC%E5%9B%9B%E5%8D%81%E4%B8%89%E5%8F%B7
 */
using System.Xml.Linq;

namespace EGov {
    public static class LawApiClient {
        private static readonly HttpClient _httpClient = new HttpClient {
            Timeout = TimeSpan.FromSeconds(20)
        };

        /// <summary>
        /// keyword検索
        /// </summary>
        /// <param name="lawNum"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static async Task<XDocument?> GetLawIdByLawNumAsync(string lawNum) {
            if (string.IsNullOrWhiteSpace(lawNum))
                throw new ArgumentException("lawNum is required.", nameof(lawNum));

            var encoded = Uri.EscapeDataString(lawNum);
            var url = $"https://laws.e-gov.go.jp/api/2/keyword?keyword={encoded}&response_format=xml";

            using var req = new HttpRequestMessage(HttpMethod.Get, url);
            using var resp = await _httpClient.SendAsync(req, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
            resp.EnsureSuccessStatusCode();
            var xml = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);

            return string.IsNullOrWhiteSpace(xml) ? null : XDocument.Parse(xml);
        }

        /// <summary>
        /// law_data検索
        /// </summary>
        /// <param name="lawNum"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static async Task<XDocument?> GetLawDataXmlByLawNumAsync(string lawNum) {
            if (string.IsNullOrWhiteSpace(lawNum))
                throw new ArgumentException("lawNum is required.", nameof(lawNum));

            var encoded = Uri.EscapeDataString(lawNum);
            var url = $"https://laws.e-gov.go.jp/api/2/law_data/{encoded}?response_format=xml";

            using var req = new HttpRequestMessage(HttpMethod.Get, url);
            using var resp = await _httpClient.SendAsync(req, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
            resp.EnsureSuccessStatusCode();
            var xml = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);

            return string.IsNullOrWhiteSpace(xml) ? null : XDocument.Parse(xml);
        }
    }
}
