/*
 * 2025-10-18
 */
using Newtonsoft.Json;

namespace Vo {
    public class LawFullTextTopElementVo {
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
