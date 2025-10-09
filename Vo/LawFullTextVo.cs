/*
 * 2025-10-09
 */
using Newtonsoft.Json;

namespace Vo {
    public class LawFullTextVo {
        private string tag = string.Empty;
        private dynamic attr;
        private dynamic children;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("tag")]
        public dynamic Tag {
            get => this.tag;
            set => this.tag = value;
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("attr")]
        public dynamic Attr {
            get => this.attr;
            set => this.attr = value;
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("children")]
        public dynamic Children {
            get => this.children;
            set => this.children = value;
        }
    }
}
