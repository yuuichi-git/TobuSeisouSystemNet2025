/*
 * 2025-10-18
 * LawFullTextの要素１
 */
using Newtonsoft.Json;

namespace Vo {
    public class LawFullTextElementVo {
        /*
         * Vo
         */
        private dynamic childrenTokens;
        private int count = 0;
        private dynamic first;
        private bool hasValues;
        private bool isReadOnly;
        private dynamic last;
        private dynamic next;
        private dynamic parent;
        private string path;
        private dynamic previous;
        private dynamic root;
        private string type;

        [JsonProperty("childrenTokens")]
        public dynamic ChildrenTokens {
            get => this.childrenTokens;
            set => this.childrenTokens = value;
        }
        [JsonProperty("Count")]
        public int Count {
            get => this.count;
            set => this.count = value;
        }
        [JsonProperty("First")]
        public dynamic First {
            get => this.first;
            set => this.first = value;
        }
        [JsonProperty("HasValues")]
        public bool HasValues {
            get => this.hasValues;
            set => this.hasValues = value;
        }
        [JsonProperty("IsReadOnly")]
        public bool IsReadOnly {
            get => this.isReadOnly;
            set => this.isReadOnly = value;
        }
        [JsonProperty("Last")]
        public dynamic Last {
            get => this.last;
            set => this.last = value;
        }
        [JsonProperty("Next")]
        public dynamic Next {
            get => this.next;
            set => this.next = value;
        }
        [JsonProperty("Parent")]
        public dynamic Parent {
            get => this.parent;
            set => this.parent = value;
        }
        [JsonProperty("Path")]
        public string Path {
            get => this.path;
            set => this.path = value;
        }
        [JsonProperty("Previous")]
        public dynamic Previous {
            get => this.previous;
            set => this.previous = value;
        }
        [JsonProperty("Root")]
        public dynamic Root {
            get => this.root;
            set => this.root = value;
        }
        [JsonProperty("Type")]
        public string Type {
            get => this.type;
            set => this.type = value;
        }

    }
}
