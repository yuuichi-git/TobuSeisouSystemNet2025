/*
 * 2025-08-28
 * e-Gov 法令API
 */
using Newtonsoft.Json;

using Vo;

namespace EGov {
    public class EGobApi {
        /// <summary>
        /// コンストラクター
        /// </summary>
        public EGobApi() {
        }

        public async void GetLawData(string lawNum) {
            string endPoint = "/law_data";                                                                                          // エンドポイント例: 法令一覧取得
            string baseUrl = "https://laws.e-gov.go.jp/api/2" + endPoint + "/" + lawNum;
            //string baseUrl = "https://laws.e-gov.go.jp/api/2" + endPoint + "/" + lawNum +
            //                                                    "?elm=" + HttpUtility.UrlEncode("MainProvision[1]") +
            //                                                        "&" + HttpUtility.UrlEncode("Article[33_2]");


            using (HttpClient client = new()) {                                                                                     // HTTPクライアントの初期化
                try {
                    HttpResponseMessage httpResponseMessage = await client.GetAsync(baseUrl);                                       // GETリクエストの送信
                    if (httpResponseMessage.IsSuccessStatusCode) {
                        string responseData = await httpResponseMessage.Content.ReadAsStringAsync();                                // レスポンスの内容を非同期で文字列として取得
                        LawFullTextTopElementVo? lawFullTextTopElementVo = JsonConvert.DeserializeObject<LawFullTextTopElementVo>(responseData);







                        MessageBox.Show("");
                    } else {
                        MessageBox.Show($"Error: {httpResponseMessage.StatusCode}");
                    }
                } catch (Exception ex) {
                    MessageBox.Show($"Exception: {ex.Message}");
                }
            }
        }
    }
}

