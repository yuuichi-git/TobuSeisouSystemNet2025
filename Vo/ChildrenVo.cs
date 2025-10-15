/*
 * 2025-10-11
 */
using Newtonsoft.Json;

namespace Vo {
    public class ChildrenVo {
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

        //public class ChildrenArrayVo {
        //    private dynamic childrenTokens;
        //    private int count = 0;
        //    private dynamic first;
        //    private bool hasValues;
        //    private bool isReadOnly;
        //    private dynamic last;
        //    private dynamic next;
        //    private dynamic parent;
        //    private string path;
        //    private dynamic previous;
        //    private dynamic root;
        //    private string type;

        //    [JsonProperty("childrenTokens")]
        //    public dynamic ChildrenTokens {
        //        get => this.childrenTokens;
        //        set => this.childrenTokens = value;
        //    }
        //    [JsonProperty("count")]
        //    public int Count {
        //        get => this.count;
        //        set => this.count = value;
        //    }
        //    [JsonProperty("first")]
        //    public dynamic First {
        //        get => this.first;
        //        set => this.first = value;
        //    }
        //    [JsonProperty("hasValues")]
        //    public bool HasValues {
        //        get => this.hasValues;
        //        set => this.hasValues = value;
        //    }
        //    [JsonProperty("isReadOnly")]
        //    public bool IsReadOnly {
        //        get => this.isReadOnly;
        //        set => this.isReadOnly = value;
        //    }
        //    [JsonProperty("last")]
        //    public dynamic Last {
        //        get => this.last;
        //        set => this.last = value;
        //    }
        //    [JsonProperty("next")]
        //    public dynamic Next {
        //        get => this.next;
        //        set => this.next = value;
        //    }
        //    [JsonProperty("parent")]
        //    public dynamic Parent {
        //        get => this.parent;
        //        set => this.parent = value;
        //    }
        //    [JsonProperty("path")]
        //    public string Path {
        //        get => this.path;
        //        set => this.path = value;
        //    }
        //    [JsonProperty("previous")]
        //    public dynamic Previous {
        //        get => this.previous;
        //        set => this.previous = value;
        //    }
        //    [JsonProperty("root")]
        //    public dynamic Root {
        //        get => this.root;
        //        set => this.root = value;
        //    }
        //    [JsonProperty("type")]
        //    public string Type {
        //        get => this.type;
        //        set => this.type = value;
        //    }
        //}

    }
}
