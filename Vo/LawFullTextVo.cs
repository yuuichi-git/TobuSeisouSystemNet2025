/*
 * 2025-10-09
 */
using Newtonsoft.Json;

namespace Vo {
    public class LawFullTextVo {
        private string tag = string.Empty;
        private AttrVo attr = new();
        private ChildrenVo[] children = Array.Empty<ChildrenVo>();

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
        public ChildrenVo[] Children {                                              // Array形式（子要素あり）
            get => this.children;
            set => this.children = value;
        }
    }
}
