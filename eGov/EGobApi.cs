/*
 * 2025-08-28
 * e-Gov 法令API
 */
using System.Text;
using System.Text.Json;

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
            string endPoint = "/law_data";                                                                  // エンドポイント例: 法令一覧取得
            string baseUrl = "https://laws.e-gov.go.jp/api/2" + endPoint + "/" + lawNum + "";               // APIのベースURL（例: 法令API Version 2）

            using (HttpClient client = new()) {                                                             // HTTPクライアントの初期化
                

                try {
                    //client.DefaultRequestHeaders.Add("Accept", "application/json");                         // 必要に応じてヘッダーを設定
                    //client.BaseAddress = new Uri(baseUrl);
                    HttpResponseMessage httpResponseMessage = await client.GetAsync(baseUrl);               // GETリクエストの送信

                    if (httpResponseMessage.IsSuccessStatusCode) {
                        string responseData = await httpResponseMessage.Content.ReadAsStringAsync();        // レスポンスの内容を非同期で文字列として取得
                        LawDataResponseVo? lawData = JsonConvert.DeserializeObject<LawDataResponseVo>(responseData);

                        



                        Console.WriteLine(responseData);
                    } else {
                        MessageBox.Show($"Error: {httpResponseMessage.StatusCode}");
                    }
                } catch (Exception ex) {
                    MessageBox.Show($"Exception: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        class LawDataResponseVo {
            [JsonProperty("attached_files_info")]
            AttachedFilesInfoVo AttachedFilesInfo {
                get; set;
            }
            [JsonProperty("law_info")]
            LawInfoVo LawInfo {
                get; set;
            }
            [JsonProperty("revision_info")]
            RevisionInfoVo RevisionInfo {
                get; set;
            }
            [JsonProperty("law_full_text")]
            LawFullTextVo LawFullText {
                get; set;
            }
        }

    }
}

