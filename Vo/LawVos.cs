namespace Vo {

    /*
     * -----------------------------------------
     * keyword検索API の VO
     * -----------------------------------------
     */

    // ルート
    public sealed class KeywordResponse {
        public int TotalCount { get; set; }
        public int SentenceCount { get; set; }
        public int NextOffset { get; set; }
        public List<KeywordItem> Items { get; set; } = new();
    }

    // 個々の検索ヒット
    public sealed class KeywordItem {
        public KeywordLawInfo LawInfo { get; set; } = new();
        public KeywordRevisionInfo RevisionInfo { get; set; } = new();
        public List<KeywordSentence> Sentences { get; set; } = new();
    }

    // law_info（KeyWord用）
    public sealed class KeywordLawInfo {
        public string LawType { get; set; } = "";
        public string LawId { get; set; } = "";
        public string LawNum { get; set; } = "";
        public string LawNumEra { get; set; } = "";
        public int LawNumYear { get; set; }
        public string LawNumType { get; set; } = "";
        public string LawNumNum { get; set; } = "";
        public DateTime? PromulgationDate { get; set; }
    }

    // revision_info（KeyWord用）
    public sealed class KeywordRevisionInfo {
        public string LawRevisionId { get; set; } = "";
        public string LawType { get; set; } = "";
        public string LawTitle { get; set; } = "";
        public string LawTitleKana { get; set; } = "";
        public string Abbrev { get; set; } = "";
        public string Category { get; set; } = "";
        public DateTime? Updated { get; set; }
        public DateTime? AmendmentPromulgateDate { get; set; }
        public DateTime? AmendmentEnforcementDate { get; set; }
        public string AmendmentEnforcementComment { get; set; } = "";
        public string AmendmentScheduledEnforcementDate { get; set; } = "";
        public string AmendmentLawId { get; set; } = "";
        public string AmendmentLawTitle { get; set; } = "";
        public string AmendmentLawTitleKana { get; set; } = "";
        public string AmendmentLawNum { get; set; } = "";
        public int AmendmentType { get; set; }
        public string RepealStatus { get; set; } = "";
        public string RepealDate { get; set; } = "";
        public bool RemainInForce { get; set; }
        public string Mission { get; set; } = "";
        public string CurrentRevisionStatus { get; set; } = "";
    }

    // sentences
    public sealed class KeywordSentence {
        public string Position { get; set; } = "";
        public string Text { get; set; } = "";
    }

    /*
     * -----------------------------------------
     * law_data検索API の VO
     * -----------------------------------------
     */

    /// <summary>
    /// 最上位
    /// </summary>
    public class LawDataResponse {
        public string LawNum { get; set; } = "";
        public string LawTitle { get; set; } = "";
        public string LawTitleKana { get; set; } = "";
        public DateTime? PromulgationDate { get; set; }
        public DateTime? EnforcementDate { get; set; }

        public LawBody? LawBody { get; set; }
    }

    /// <summary>
    /// 本文全体
    /// </summary>
    public class LawBody {
        public LawPreface? Preface { get; set; }

        public List<LawChapter> Chapters { get; set; } = new();
        public List<LawSection> Sections { get; set; } = new();
        public List<LawSubsection> Subsections { get; set; } = new();
        public List<LawDivision> Divisions { get; set; } = new();

        public List<LawArticle> Articles { get; set; } = new();

        public LawSupplProvision? SupplProvision { get; set; }
    }

    /// <summary>
    /// 前文
    /// </summary>
    public class LawPreface {
        public string Text { get; set; } = "";
    }

    /// <summary>
    /// 章
    /// </summary>
    public class LawChapter {
        public string? ChapterNum { get; set; }
        public string? ChapterTitle { get; set; }

        public List<LawSection> Sections { get; set; } = new();
        public List<LawArticle> Articles { get; set; } = new();
    }

    /// <summary>
    /// 節
    /// </summary>
    public class LawSection {
        public string? SectionNum { get; set; }
        public string? SectionTitle { get; set; }

        public List<LawArticle> Articles { get; set; } = new();
    }

    /// <summary>
    /// 款
    /// </summary>
    public class LawSubsection {
        public string SubsectionNum { get; set; } = "";
        public string SubsectionTitle { get; set; } = "";
        public List<LawArticle> Articles { get; set; } = new();
    }

    /// <summary>
    /// 目
    /// </summary>
    public class LawDivision {
        public string DivisionNum { get; set; } = "";
        public string DivisionTitle { get; set; } = "";
        public List<LawArticle> Articles { get; set; } = new();
    }

    /// <summary>
    /// 条
    /// </summary>
    public class LawArticle {
        public string ArticleTitle { get; set; } = "";
        public List<LawParagraph> Paragraphs { get; set; } = new();
    }

    /// <summary>
    /// 項
    /// </summary>
    public class LawParagraph {
        public string? ParagraphNum { get; set; }
        public List<string> Sentences { get; set; } = new();
        public string Text { get; set; } = "";
        public List<LawItem> Items { get; set; } = new();
    }


    /// <summary>
    /// 号
    /// </summary>
    public class LawItem {
        public string? ItemNum { get; set; }
        public List<string> Sentences { get; set; } = new();
        public string Text { get; set; } = "";
    }


    /// <summary>
    /// 附則
    /// </summary>
    public class LawSupplProvision {
        public List<LawSupplParagraph> Paragraphs { get; set; } = new();
    }

    /// <summary>
    /// 附則の項
    /// </summary>
    public class LawSupplParagraph {
        public string ParagraphNum { get; set; } = "";
        public string Text { get; set; } = "";
    }




}