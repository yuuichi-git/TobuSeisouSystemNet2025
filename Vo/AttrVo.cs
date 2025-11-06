/*
 * 2025-10-09
 */
using Newtonsoft.Json;

namespace Vo {
    public class AttrVo {
        /*
         * Vo
         */
        private string era = string.Empty;
        private string lang = string.Empty;
        private string lawType = string.Empty;
        private string num = string.Empty;
        private string year = string.Empty;
        private string promulgateMonthDay = string.Empty;
        private string promulgateDayOfWeek = string.Empty;

        [JsonProperty("era")]
        public string Era {
            get => this.era;
            set => this.era = value;
        }
        [JsonProperty("lang")]
        public string Lang {
            get => this.lang;
            set => this.lang = value;
        }
        [JsonProperty("lawType")]
        public string LawType {
            get => this.lawType;
            set => this.lawType = value;
        }
        [JsonProperty("num")]
        public string Num {
            get => this.num;
            set => this.num = value;
        }
        [JsonProperty("year")]
        public string Year {
            get => this.year;
            set => this.year = value;
        }
        [JsonProperty("promulgateMonthDay")]
        public string PromulgateMonthDay {
            get => this.promulgateMonthDay;
            set => this.promulgateMonthDay = value;
        }
        [JsonProperty("promulgateDayOfWeek")]
        public string PromulgateDayOfWeek {
            get => this.promulgateDayOfWeek;
            set => this.promulgateDayOfWeek = value;
        }
    }
}
