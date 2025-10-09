/*
 * 2025-10-09
 */
using Newtonsoft.Json;

namespace Vo {
    public class AttachedFileVo {
        private string law_revision_id = string.Empty;
        private string src = string.Empty;
        private string updated = string.Empty;

        /// <summary>
        /// 法令ID
        /// </summary>
        [JsonProperty("law_revision_id")]
        public string LawRevisionId {
            get => this.law_revision_id;
            set => this.law_revision_id = value;
        }
        /// <summary>
        /// 法令XML中のFig要素のsrc属性
        /// </summary>
        [JsonProperty("src")]
        public string Src {
            get => this.src;
            set => this.src = value;
        }
        /// <summary>
        /// 正誤等による更新日時
        /// </summary>
        [JsonProperty("updated")]
        public string Updated {
            get => this.updated;
            set => this.updated = value;
        }
    }
}
