/*
 * 2025-10-09
 */
using Newtonsoft.Json;

namespace Vo {
    public class RevisionInfoVo {
        private string law_revision_id = string.Empty;
        private string law_type = string.Empty;
        private string law_title = string.Empty;
        private string law_title_kana = string.Empty;
        private string abbrev = string.Empty;
        private string category = string.Empty;
        private string updated = string.Empty;
        private string amendment_promulgate_date = string.Empty;
        private string amendment_enforcement_date = string.Empty;
        private string amendment_enforcement_comment = string.Empty;
        private string amendment_scheduled_enforcement_date = string.Empty;
        private string amendment_law_id = string.Empty;
        private string amendment_law_title = string.Empty;
        private string amendment_law_title_kana = string.Empty;
        private string amendment_law_num = string.Empty;
        private string amendment_type = string.Empty;
        private string repeal_status = string.Empty;
        private string repeal_date = string.Empty;
        private bool remain_in_force = false;
        private string mission = string.Empty;
        private string current_revision_status = string.Empty;

        /// <summary>
        /// 法令履歴ID
        /// </summary>
        [JsonProperty("law_revision_id")]
        public string LawRevisionId {
            get => this.law_revision_id;
            set => this.law_revision_id = value;
        }
        /// <summary>
        /// 法令種別
        /// </summary>
        [JsonProperty("law_type")]
        public string LawType {
            get => this.law_type;
            set => this.law_type = value;
        }
        /// <summary>
        /// 法令名
        /// </summary>
        [JsonProperty("law_title")]
        public string LawTitle {
            get => this.law_title;
            set => this.law_title = value;
        }
        /// <summary>
        /// 法令名読み
        /// </summary>
        [JsonProperty("law_title_kana")]
        public string LawTitleKana {
            get => this.law_title_kana;
            set => this.law_title_kana = value;
        }
        /// <summary>
        /// 法令略称
        /// </summary>
        [JsonProperty("abbrev")]
        public string Abbrev {
            get => this.abbrev;
            set => this.abbrev = value;
        }
        /// <summary>
        /// 法令分野分類
        /// </summary>
        [JsonProperty("category")]
        public string Category {
            get => this.category;
            set => this.category = value;
        }
        /// <summary>
        /// 正誤等による更新日時
        /// </summary>
        [JsonProperty("updated")]
        public string Updated {
            get => this.updated;
            set => this.updated = value;
        }
        /// <summary>
        /// 改正法令公布日
        /// </summary>
        [JsonProperty("amendment_promulgate_date")]
        public string AmendmentPromulgateDate {
            get => this.amendment_promulgate_date;
            set => this.amendment_promulgate_date = value;
        }
        /// <summary>
        /// 改正法令施行期日（この履歴に対応する改正の施行期日）
        /// </summary>
        [JsonProperty("amendment_enforcement_date")]
        public string AmendmentEnforcementDate {
            get => this.amendment_enforcement_date;
            set => this.amendment_enforcement_date = value;
        }
        /// <summary>
        /// 施行期日規定等の参考情報（この履歴に対応する改正の施行期日）
        /// </summary>
        [JsonProperty("amendment_enforcement_comment")]
        public string AmendmentEnforcementComment {
            get => this.amendment_enforcement_comment;
            set => this.amendment_enforcement_comment = value;
        }
        /// <summary>
        /// 擬似的な施行期日（実際の施行期日とは限らない）（この履歴に対応する改正の施行期日）
        /// </summary>
        [JsonProperty("amendment_scheduled_enforcement_date")]
        public string AmendmentScheduledEnforcementDate {
            get => this.amendment_scheduled_enforcement_date;
            set => this.amendment_scheduled_enforcement_date = value;
        }
        /// <summary>
        /// 改正法令の法令ID（この履歴に対応する改正法令）
        /// </summary>
        [JsonProperty("amendment_law_id")]
        public string AmendmentLawId {
            get => this.amendment_law_id;
            set => this.amendment_law_id = value;
        }
        /// <summary>
        /// 改正法令名
        /// </summary>
        [JsonProperty("amendment_law_title")]
        public string AmendmentLawTitle {
            get => this.amendment_law_title;
            set => this.amendment_law_title = value;
        }
        /// <summary>
        /// 改正法令名読み
        /// </summary>
        [JsonProperty("amendment_law_title_kana")]
        public string AmendmentLawTitleKana {
            get => this.amendment_law_title_kana;
            set => this.amendment_law_title_kana = value;
        }
        /// <summary>
        /// 改正法令番号
        /// </summary>
        [JsonProperty("amendment_law_num")]
        public string AmendmentLawNum {
            get => this.amendment_law_num;
            set => this.amendment_law_num = value;
        }
        /// <summary>
        /// 改正種別
        /// </summary>
        [JsonProperty("amendment_type")]
        public string AmendmentType {
            get => this.amendment_type;
            set => this.amendment_type = value;
        }
        /// <summary>
        /// 廃止等の状態
        /// </summary>
        [JsonProperty("repeal_status")]
        public string RepealStatus {
            get => this.repeal_status;
            set => this.repeal_status = value;
        }
        /// <summary>
        /// 廃止日
        /// </summary>
        [JsonProperty("repeal_date")]
        public string RepealDate {
            get => this.repeal_date;
            set => this.repeal_date = value;
        }
        /// <summary>
        /// 廃止後の効力（true:廃止後でも効力を有するもの / false:廃止後に効力を有しないもの）
        /// </summary>
        [JsonProperty("remain_in_force")]
        public bool RemainInForce {
            get => this.remain_in_force;
            set => this.remain_in_force = value;
        }
        /// <summary>
        /// 新規制定又は被改正法令（New）・一部改正法令（Partial）
        /// </summary>
        [JsonProperty("mission")]
        public string Mission {
            get => this.mission;
            set => this.mission = value;
        }
        /// <summary>
        /// 履歴の状態
        /// </summary>
        [JsonProperty("current_revision_status")]
        public string CurrentRevisionStatus {
            get => this.current_revision_status;
            set => this.current_revision_status = value;
        }
    }
}
