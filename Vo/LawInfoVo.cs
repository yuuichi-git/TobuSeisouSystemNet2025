/*
 * 2025-10-09
 */
using Newtonsoft.Json;

namespace Vo {
    public class LawInfoVo {
        private string law_type = string.Empty;
        private string law_id = string.Empty;
        private string law_num = string.Empty;
        private string law_num_era = string.Empty;
        private int law_num_year = 0;
        private string law_num_type = string.Empty;
        private string law_num_num = string.Empty;
        private string promulgation_date = string.Empty;

        /// <summary>
        /// 法令種別
        /// </summary>
        [JsonProperty("law_type")]
        public string LawType {
            get => this.law_type;
            set => this.law_type = value;
        }
        /// <summary>
        /// 法令ID
        /// </summary>
        [JsonProperty("law_id")]
        public string LawId {
            get => this.law_id;
            set => this.law_id = value;
        }
        /// <summary>
        /// 法令番号
        /// </summary>
        [JsonProperty("law_num")]
        public string LawNum {
            get => this.law_num;
            set => this.law_num = value;
        }
        /// <summary>
        /// 法令番号の元号
        /// </summary>
        [JsonProperty("law_num_era")]
        public string LawNumEra {
            get => this.law_num_era;
            set => this.law_num_era = value;
        }
        /// <summary>
        /// 法令番号の年
        /// </summary>
        [JsonProperty("law_num_year")]
        public int LawNumYear {
            get => this.law_num_year;
            set => this.law_num_year = value;
        }
        /// <summary>
        /// 法令番号の法令種別
        /// </summary>
        [JsonProperty("law_num_type")]
        public string LawNumType {
            get => this.law_num_type;
            set => this.law_num_type = value;
        }
        /// <summary>
        /// 法令番号の号数
        /// </summary>
        [JsonProperty("law_num_num")]
        public string LawNumNum {
            get => this.law_num_num;
            set => this.law_num_num = value;
        }
        /// <summary>
        /// 公布日
        /// </summary>
        [JsonProperty("promulgation_date")]
        public string PromulgationDate {
            get => this.promulgation_date;
            set => this.promulgation_date = value;
        }
    }
}
