/*
 * 2025-08-28
 * e-Gov 法令API
 */
using System.Text.Json;
using System.Web;

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
            //string baseUrl = "https://laws.e-gov.go.jp/api/2" + endPoint + "/" + lawNum + "";                                       // APIのベースURL（例: 法令API Version 2）
            string baseUrl = "https://laws.e-gov.go.jp/api/2" + endPoint + "/" + lawNum + 
                                                                "?elm=" + HttpUtility.UrlEncode("MainProvision-Paragraph[1]") +
                                                                "&" + HttpUtility.UrlEncode("Chapter[2]") +
                                                                "&" + HttpUtility.UrlEncode("Section[6]");

            using (HttpClient client = new()) {                                                                                     // HTTPクライアントの初期化
                try {
                    HttpResponseMessage httpResponseMessage = await client.GetAsync(baseUrl);                                       // GETリクエストの送信
                    if (httpResponseMessage.IsSuccessStatusCode) {
                        string responseData = await httpResponseMessage.Content.ReadAsStringAsync();                                // レスポンスの内容を非同期で文字列として取得
                        LawDataResponseVo? lawDataResponseVo = JsonConvert.DeserializeObject<LawDataResponseVo>(responseData);





                        MessageBox.Show("");
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
        private class LawDataResponseVo {
            /*
             * Vo
             */
            private AttachedFilesInfoVo attachedFilesInfo = new();
            private LawInfoVo lawInfo = new();
            private RevisionInfoVo revisionInfo = new();
            private LawFullTextVo lawFullText = new();

            [JsonProperty("attached_files_info")]
            public AttachedFilesInfoVo AttachedFilesInfo {
                get => this.attachedFilesInfo;
                set => this.attachedFilesInfo = value;
            }
            [JsonProperty("law_info")]
            public LawInfoVo LawInfo {
                get => this.lawInfo;
                set => this.lawInfo = value;
            }
            [JsonProperty("revision_info")]
            public RevisionInfoVo RevisionInfo {
                get => this.revisionInfo;
                set => this.revisionInfo = value;
            }
            [JsonProperty("law_full_text")]
            public LawFullTextVo LawFullText {
                get => this.lawFullText;
                set => this.lawFullText = value;
            }
        }
    }
}

