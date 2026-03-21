using System.Xml.Serialization;

namespace Vo {

    public class LawKeywordVo {
        [XmlRoot("keyword_response")]
        public class KeywordResponseVO {
            [XmlElement("total_count")]
            public int TotalCount { get; set; } = 0;

            [XmlElement("sentence_count")]
            public int SentenceCount { get; set; } = 0;

            [XmlElement("next_offset")]
            public int NextOffset { get; set; } = 0;

            [XmlArray("items")]
            [XmlArrayItem("item")]
            public List<ItemVO> Items { get; set; } = new();
        }

        public class ItemVO {
            [XmlElement("law_info")]
            public LawInfoVO LawInfo { get; set; } = new();

            [XmlElement("revision_info")]
            public RevisionInfoVO RevisionInfo { get; set; } = new();

            [XmlArray("sentences")]
            [XmlArrayItem("sentence")]
            public List<SentenceVO> Sentences { get; set; } = new();
        }
        /*
         * 
         * LawInfo
         * 
         */
        public class LawInfoVO {
            [XmlElement("law_type")]
            public string LawType { get; set; } = string.Empty;

            [XmlElement("law_id")]
            public string LawId { get; set; } = string.Empty;

            [XmlElement("law_num")]
            public string LawNum { get; set; } = string.Empty;

            [XmlElement("law_num_era")]
            public string LawNumEra { get; set; } = string.Empty;

            [XmlElement("law_num_year")]
            public string LawNumYear { get; set; } = string.Empty;

            [XmlElement("law_num_type")]
            public string LawNumType { get; set; } = string.Empty;

            [XmlElement("law_num_num")]
            public string LawNumNum { get; set; } = string.Empty;

            [XmlElement("promulgation_date")]
            public string PromulgationDate { get; set; } = string.Empty;
        }
        /*
         * 
         * RevisionInfo
         * 
         */
        public class RevisionInfoVO {
            [XmlElement("law_revision_id")]
            public string LawRevisionId { get; set; } = string.Empty;

            [XmlElement("law_type")]
            public string LawType { get; set; } = string.Empty;

            [XmlElement("law_title")]
            public string LawTitle { get; set; } = string.Empty;

            [XmlElement("law_title_kana")]
            public string LawTitleKana { get; set; } = string.Empty;

            [XmlElement("abbrev")]
            public string Abbrev { get; set; } = string.Empty;

            [XmlElement("category")]
            public string Category { get; set; } = string.Empty;

            [XmlElement("updated")]
            public string Updated { get; set; } = string.Empty;

            [XmlElement("amendment_promulgate_date")]
            public string AmendmentPromulgateDate { get; set; } = string.Empty;

            [XmlElement("amendment_enforcement_date")]
            public string AmendmentEnforcementDate { get; set; } = string.Empty;

            [XmlElement("amendment_enforcement_comment")]
            public string AmendmentEnforcementComment { get; set; } = string.Empty;

            [XmlElement("amendment_scheduled_enforcement_date")]
            public string AmendmentScheduledEnforcementDate { get; set; } = string.Empty;

            [XmlElement("amendment_law_id")]
            public string AmendmentLawId { get; set; } = string.Empty;

            [XmlElement("amendment_law_title")]
            public string AmendmentLawTitle { get; set; } = string.Empty;

            [XmlElement("amendment_law_title_kana")]
            public string AmendmentLawTitleKana { get; set; } = string.Empty;

            [XmlElement("amendment_law_num")]
            public string AmendmentLawNum { get; set; } = string.Empty;

            [XmlElement("amendment_type")]
            public string AmendmentType { get; set; } = string.Empty;

            [XmlElement("repeal_status")]
            public string RepealStatus { get; set; } = string.Empty;

            [XmlElement("repeal_date")]
            public string RepealDate { get; set; } = string.Empty;

            [XmlElement("remain_in_force")]
            public bool RemainInForce { get; set; } = false;

            [XmlElement("mission")]
            public string Mission { get; set; } = string.Empty;

            [XmlElement("current_revision_status")]
            public string CurrentRevisionStatus { get; set; } = string.Empty;
        }
        /*
         * 
         * Sentence
         * 
         */
        public class SentenceVO {
            [XmlElement("position")]
            public string Position { get; set; } = string.Empty;

            [XmlElement("text")]
            public string Text { get; set; } = string.Empty;
        }

    }
}
