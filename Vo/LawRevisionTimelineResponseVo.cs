using System.Xml.Serialization;

namespace Vo {

    // ============================================================
    // ★ 改正履歴 API のルート要素
    //
    //   <law_revisions_response>
    //       <law_info> … </law_info>
    //       <revisions>
    //           <revision> … </revision>
    //           <revision> … </revision>
    //       </revisions>
    //   </law_revisions_response>
    //
    // ============================================================
    [XmlRoot("law_revisions_response")]
    public class RevisionTimelineResponse {

        /// <summary>
        /// 改正履歴に依存しない法令情報（法令番号・公布日など）
        /// </summary>
        [XmlElement("law_info")]
        public RevisionLawInfo LawInfo { get; set; } = new();

        /// <summary>
        /// 改正履歴一覧（新しい順）
        /// </summary>
        [XmlArray("revisions")]
        [XmlArrayItem("revision")]
        public List<RevisionTimelineEntry> Revisions { get; set; } = new();
    }

    // ============================================================
    // ★ law_info（改正履歴 API 専用）
    //
    //   改正履歴に依存しない基本情報。
    //   LawDataResponse の law_info と似ているが別物。
    // ============================================================
    public class RevisionLawInfo {

        /// <summary>法令種別（Act / CabinetOrder など）</summary>
        [XmlElement("law_type")]
        public string LawType { get; set; } = "";

        /// <summary>法令ID（例：503AC0000000036）</summary>
        [XmlElement("law_id")]
        public string LawId { get; set; } = "";

        /// <summary>法令番号（例：令和三年法律第三十六号）</summary>
        [XmlElement("law_num")]
        public string LawNum { get; set; } = "";

        /// <summary>元号（Reiwa / Heisei など）</summary>
        [XmlElement("law_num_era")]
        public string LawNumEra { get; set; } = "";

        /// <summary>元号の年（例：3）</summary>
        [XmlElement("law_num_year")]
        public int LawNumYear { get; set; }

        /// <summary>法令種別（Act / CabinetOrder など）</summary>
        [XmlElement("law_num_type")]
        public string LawNumType { get; set; } = "";

        /// <summary>法令番号の通し番号（例：036）</summary>
        [XmlElement("law_num_num")]
        public string LawNumNum { get; set; } = "";

        /// <summary>公布日（YYYY-MM-DD）</summary>
        [XmlElement("promulgation_date")]
        public string PromulgationDate { get; set; } = "";
    }

    // ============================================================
    // ★ revision（改正履歴 1 件分）
    //
    //   施行日・公布日・改正法番号・改正法タイトルなど、
    //   タイムライン表示に必要な情報がすべて入っている。
    // ============================================================
    public class RevisionTimelineEntry {

        /// <summary>
        /// 改正履歴ID（例：503AC0000000036_20240607_506AC0000000046）
        /// </summary>
        [XmlElement("law_revision_id")]
        public string LawRevisionId { get; set; } = "";

        /// <summary>改正後の法令種別（Act など）</summary>
        [XmlElement("law_type")]
        public string LawType { get; set; } = "";

        /// <summary>改正後の法令名（例：デジタル庁設置法）</summary>
        [XmlElement("law_title")]
        public string LawTitle { get; set; } = "";

        /// <summary>改正法令の公布日（YYYY-MM-DD）</summary>
        [XmlElement("amendment_promulgate_date")]
        public string AmendmentPromulgateDate { get; set; } = "";

        /// <summary>改正法令の施行日（YYYY-MM-DD）</summary>
        [XmlElement("amendment_enforcement_date")]
        public string AmendmentEnforcementDate { get; set; } = "";

        /// <summary>施行日の備考（例：一部施行など）</summary>
        [XmlElement("amendment_enforcement_comment")]
        public string AmendmentEnforcementComment { get; set; } = "";

        /// <summary>改正法令の法令ID</summary>
        [XmlElement("amendment_law_id")]
        public string AmendmentLawId { get; set; } = "";

        /// <summary>改正法令のタイトル</summary>
        [XmlElement("amendment_law_title")]
        public string AmendmentLawTitle { get; set; } = "";

        /// <summary>改正法令の法令番号（例：令和六年法律第四十六号）</summary>
        [XmlElement("amendment_law_num")]
        public string AmendmentLawNum { get; set; } = "";

        /// <summary>
        /// 改正種別（1:全部改正 / 3:新規制定 / 8:廃止 など）
        /// </summary>
        [XmlElement("amendment_type")]
        public string AmendmentType { get; set; } = "";

        /// <summary>現行法かどうか（true = 現行、false = 旧法）</summary>
        [XmlElement("remain_in_force")]
        public bool RemainInForce { get; set; }

        /// <summary>廃止・失効などの状態（None / Repeal / Expire など）</summary>
        [XmlElement("repeal_status")]
        public string RepealStatus { get; set; } = "";

        /// <summary>廃止日（YYYY-MM-DD）</summary>
        [XmlElement("repeal_date")]
        public string RepealDate { get; set; } = "";

        /// <summary>New（新規制定）/ Partial（一部改正）</summary>
        [XmlElement("mission")]
        public string Mission { get; set; } = "";

        /// <summary>
        /// 現在の改正状態（CurrentEnforced / PreviousEnforced / UnEnforced / Repeal）
        /// </summary>
        [XmlElement("current_revision_status")]
        public string CurrentRevisionStatus { get; set; } = "";
    }
}
