/*
 * 2025-10-09
 */
using Newtonsoft.Json;

namespace Vo {
    public class AttachedFilesInfoVo {
        /*
         * Vo
         */
        private string image_data = string.Empty;
        private AttachedFileVo[] attached_files = Array.Empty<AttachedFileVo>();

        /// <summary>
        /// 添付ファイルデータ（添付ファイルをフォルダ名pictに収集し、フォルダ全体をZip形式で圧縮したファイルをBase64でエンコードした文字列）
        /// </summary>
        [JsonProperty("image_data")]
        public string ImageData {
            get => this.image_data;
            set => this.image_data = value;
        }
        /// <summary>
        /// 添付ファイル情報（添付ファイルの情報を格納した配列）
        /// </summary>
        [JsonProperty("attached_files")]
        public AttachedFileVo[] AttachedFiles {                                     // Array形式（子要素あり）
            get => this.attached_files;
            set => this.attached_files = value;
        }
    }
}
