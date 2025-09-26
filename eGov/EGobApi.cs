/*
 * 2025-08-28
 * e-Gov 法令API
 */


using System.Text.Json.Serialization;

namespace EGov {
    public class EGobApi {

        public async void GetLawData(string lawNum) {
            string endpoint = "/law_data";                                                                  // エンドポイント例: 法令一覧取得
            string baseUrl = "https://laws.e-gov.go.jp/api/2" + endpoint + "/" + lawNum + "";               // APIのベースURL（例: 法令API Version 2）

            using (HttpClient client = new()) {                                                             // HTTPクライアントの初期化
                client.BaseAddress = new Uri(baseUrl);

                try {
                    // GETリクエストの送信
                    HttpResponseMessage httpResponseMessage = await client.GetAsync(baseUrl);

                    // レスポンスの確認
                    if (httpResponseMessage.IsSuccessStatusCode) {
                        string responseData = await httpResponseMessage.Content.ReadAsStringAsync();        // レスポンスデータの読み取り




                    } else {
                        MessageBox.Show($"Error: {httpResponseMessage.StatusCode}");
                    }
                } catch (Exception ex) {
                    MessageBox.Show($"Exception: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// law_data_response
        /// 重複しているプロパティは省略？
        /// </summary>
        public class LawDataResponse {
            /*
             * attached_files_info
             */

            /*
             * law_info
             */
            [JsonPropertyName("law_type")]
            public string LawType {
                get; set;
            }
            [JsonPropertyName("law_id")]
            public string LawId {
                get; set;
            }
            [JsonPropertyName("law_num")]
            public string LawNum {
                get; set;
            }
            [JsonPropertyName("law_num_era")]
            public string LawNumEra {
                get; set;
            }
            [JsonPropertyName("law_num_year")]
            public string LawNumYear {
                get; set;
            }
            [JsonPropertyName("law_num_type")]
            public string LawNumType {
                get; set;
            }
            [JsonPropertyName("law_num_num")]
            public string LawNumNum {
                get; set;
            }
            [JsonPropertyName("promulgation_date")]
            public string PromulgationDate {
                get; set;
            }
            /*
             * revision_info
             */
            [JsonPropertyName("law_revision_id")]
            public string LawRevisionId {
                get; set;
            }
            //[JsonPropertyName("law_type")]
            //public string LawType {
            //    get; set;
            //}
            [JsonPropertyName("law_title")]
            public string LawTitle {
                get; set;
            }
            [JsonPropertyName("law_title_kana")]
            public string LawTitleKana {
                get; set;
            }


        }
    }
}

