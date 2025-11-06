/*
 * 2025-10-11
 */
using Newtonsoft.Json;

namespace Vo {
    public class ChildrenVo {
        /*
         * Vo
         */
        private string tag = string.Empty;
        private AttrVo attr = new();
        private dynamic children;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("tag")]
        public string Tag {
            get => this.tag;
            set => this.tag = value;
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("attr")]
        public AttrVo Attr {
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
