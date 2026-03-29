using System.Xml;
using System.Xml.Serialization;

namespace Vo {
    // -----------------------------
    // root
    // -----------------------------
    [XmlRoot("law_data_response")]
    public class LawDataResponse {
        [XmlElement("attached_files_info")]
        public AttachedFilesInfo AttachedFilesInfo { get; set; } = new();

        [XmlElement("law_info")]
        public LawInfo LawInfo { get; set; } = new();

        [XmlElement("revision_info")]
        public RevisionInfo RevisionInfo { get; set; } = new();

        [XmlElement("law_full_text")]
        public LawFullText LawFullText { get; set; } = new();
    }

    // -----------------------------
    // attached_files_info
    // -----------------------------
    public class AttachedFilesInfo {
        [XmlElement("image_data")]
        public string ImageData { get; set; } = "";

        [XmlArray("attached_files")]
        [XmlArrayItem("attached_file")]
        public List<AttachedFile> AttachedFiles { get; set; } = new();
    }

    public class AttachedFile {
        [XmlElement("law_revision_id")]
        public string LawRevisionId { get; set; } = "";

        [XmlElement("src")]
        public string Src { get; set; } = "";

        [XmlElement("updated")]
        public string Updated { get; set; } = "";
    }

    // -----------------------------
    // law_info
    // -----------------------------
    public class LawInfo {
        [XmlElement("law_type")]
        public string LawType { get; set; } = "";

        [XmlElement("law_id")]
        public string LawId { get; set; } = "";

        [XmlElement("law_num")]
        public string LawNum { get; set; } = "";

        [XmlElement("law_num_era")]
        public string LawNumEra { get; set; } = "";

        [XmlElement("law_num_year")]
        public string LawNumYear { get; set; } = "";

        [XmlElement("law_num_type")]
        public string LawNumType { get; set; } = "";

        [XmlElement("law_num_num")]
        public string LawNumNum { get; set; } = "";

        [XmlElement("promulgation_date")]
        public string PromulgationDate { get; set; } = "";
    }

    // -----------------------------
    // revision_info
    // -----------------------------
    public class RevisionInfo {
        [XmlElement("law_revision_id")]
        public string LawRevisionId { get; set; } = "";

        [XmlElement("law_type")]
        public string LawType { get; set; } = "";

        [XmlElement("law_title")]
        public string LawTitle { get; set; } = "";

        [XmlElement("law_title_kana")]
        public string LawTitleKana { get; set; } = "";

        [XmlElement("abbrev")]
        public string Abbrev { get; set; } = "";

        [XmlElement("category")]
        public string Category { get; set; } = "";

        [XmlElement("updated")]
        public string Updated { get; set; } = "";

        [XmlElement("amendment_promulgate_date")]
        public string AmendmentPromulgateDate { get; set; } = "";

        [XmlElement("amendment_enforcement_date")]
        public string AmendmentEnforcementDate { get; set; } = "";

        [XmlElement("amendment_enforcement_comment")]
        public string AmendmentEnforcementComment { get; set; } = "";

        [XmlElement("amendment_scheduled_enforcement_date")]
        public string AmendmentScheduledEnforcementDate { get; set; } = "";

        [XmlElement("amendment_law_id")]
        public string AmendmentLawId { get; set; } = "";

        [XmlElement("amendment_law_title")]
        public string AmendmentLawTitle { get; set; } = "";

        [XmlElement("amendment_law_title_kana")]
        public string AmendmentLawTitleKana { get; set; } = "";

        [XmlElement("amendment_law_num")]
        public string AmendmentLawNum { get; set; } = "";

        [XmlElement("amendment_type")]
        public string AmendmentType { get; set; } = "";

        [XmlElement("repeal_status")]
        public string RepealStatus { get; set; } = "";

        [XmlElement("repeal_date")]
        public string RepealDate { get; set; } = "";

        [XmlElement("remain_in_force")]
        public string RemainInForce { get; set; } = "";

        [XmlElement("mission")]
        public string Mission { get; set; } = "";

        [XmlElement("current_revision_status")]
        public string CurrentRevisionStatus { get; set; } = "";
    }

    // -----------------------------
    // law_full_text
    // -----------------------------
    public class LawFullText {
        [XmlElement("Law")]
        public Law Law { get; set; } = new();
    }

    // -----------------------------
    // Law
    // -----------------------------
    public class Law {
        [XmlAttribute("Year")]
        public string Year { get; set; } = "";

        [XmlAttribute("PromulgateMonth")]
        public string PromulgateMonth { get; set; } = "";

        [XmlAttribute("PromulgateDay")]
        public string PromulgateDay { get; set; } = "";

        [XmlAttribute("Num")]
        public string Num { get; set; } = "";

        [XmlAttribute("LawType")]
        public string LawType { get; set; } = "";

        [XmlAttribute("Lang")]
        public string Lang { get; set; } = "";

        [XmlAttribute("Era")]
        public string Era { get; set; } = "";

        [XmlElement("LawNum")]
        public string LawNum { get; set; } = "";

        [XmlElement("LawBody")]
        public LawBody LawBody { get; set; } = new();
    }

    public class LawBody {
        [XmlElement("LawTitle")]
        public LawTitle LawTitle { get; set; } = new();

        [XmlElement("MainProvision")]
        public MainProvision MainProvision { get; set; } = new();

        [XmlElement("SupplProvision")]
        public SupplProvision SupplProvision { get; set; } = new();

        [XmlAnyElement]
        public XmlElement[]? OtherElements { get; set; }
    }

    public class LawTitle {
        [XmlAttribute("Kana")]
        public string Kana { get; set; } = "";

        [XmlAttribute("AbbrevKana")]
        public string AbbrevKana { get; set; } = "";

        [XmlAttribute("Abbrev")]
        public string Abbrev { get; set; } = "";

        [XmlText]
        public string Text { get; set; } = "";
    }

    // -----------------------------
    // MainProvision
    // -----------------------------
    public class MainProvision {
        [XmlElement("Chapter")]
        public List<Chapter> Chapters { get; set; } = new();

        [XmlElement("Article")]
        public List<Article> Articles { get; set; } = new();
    }

    // -----------------------------
    // Chapter（章）
    // -----------------------------
    public class Chapter {
        [XmlAttribute("Num")]
        public string Num { get; set; } = "";

        [XmlElement("ChapterTitle")]
        public string ChapterTitle { get; set; } = "";

        [XmlElement("Section")]
        public List<Section> Sections { get; set; } = new();

        // ★ 追加：章直下に款が来る可能性に備える
        [XmlElement("Subsection")]
        public List<Subsection> Subsections { get; set; } = new();

        [XmlElement("Article")]
        public List<Article> Articles { get; set; } = new();
    }

    // -----------------------------
    // Section（節）
    // -----------------------------
    public class Section {
        [XmlAttribute("Num")]
        public string Num { get; set; } = "";

        [XmlElement("SectionTitle")]
        public string SectionTitle { get; set; } = "";

        // ★ 追加：節の下に款
        [XmlElement("Subsection")]
        public List<Subsection> Subsections { get; set; } = new();

        // ★ 節直下に条が来る場合もある
        [XmlElement("Article")]
        public List<Article> Articles { get; set; } = new();
    }

    // -----------------------------
    // Subsection（款）
    // -----------------------------
    public class Subsection {
        [XmlAttribute("Num")]
        public string Num { get; set; } = "";

        [XmlElement("SubsectionTitle")]
        public string SubsectionTitle { get; set; } = "";

        [XmlElement("Article")]
        public List<Article> Articles { get; set; } = new();
    }

    // -----------------------------
    // Article（条）
    // -----------------------------
    public class Article {
        [XmlAttribute("Num")]
        public string Num { get; set; } = "";

        [XmlAttribute("Delete")]
        public string Delete { get; set; } = "";

        [XmlAttribute("Hide")]
        public string Hide { get; set; } = "";

        [XmlElement("ArticleCaption")]
        public string ArticleCaption { get; set; } = "";

        [XmlElement("ArticleTitle")]
        public string ArticleTitle { get; set; } = "";

        [XmlElement("Paragraph")]
        public List<Paragraph> Paragraphs { get; set; } = new();
    }

    // -----------------------------
    // Paragraph（項）
    // -----------------------------
    public class Paragraph {
        [XmlAttribute("Num")]
        public string Num { get; set; } = "";

        [XmlAttribute("Hide")]
        public string Hide { get; set; } = "";

        [XmlAttribute("OldStyle")]
        public string OldStyle { get; set; } = "";

        [XmlElement("ParagraphCaption")]
        public string ParagraphCaption { get; set; } = "";

        [XmlElement("ParagraphNum")]
        public string ParagraphNum { get; set; } = "";

        [XmlElement("ParagraphSentence")]
        public ParagraphSentence ParagraphSentence { get; set; } = new();

        [XmlElement("Item")]
        public List<Item> Items { get; set; } = new();
    }

    public class ParagraphSentence {
        [XmlElement("Sentence")]
        public List<Sentence> Sentences { get; set; } = new();
    }

    // -----------------------------
    // Sentence（文）
    // -----------------------------
    public class Sentence {
        [XmlAttribute("Num")]
        public string Num { get; set; } = "";

        [XmlAttribute("WritingMode")]
        public string WritingMode { get; set; } = "";

        [XmlAttribute("Function")]
        public string Function { get; set; } = "";

        [XmlAnyElement]
        public XmlElement[]? AnyElements { get; set; }

        [XmlText]
        public string Text { get; set; } = "";
    }

    // -----------------------------
    // Item（号）
    // -----------------------------
    public class Item {
        [XmlAttribute("Num")]
        public string Num { get; set; } = "";

        [XmlElement("ItemTitle")]
        public string ItemTitle { get; set; } = "";

        [XmlElement("ItemSentence")]
        public ItemSentence ItemSentence { get; set; } = new();
    }

    public class ItemSentence {
        [XmlElement("Sentence")]
        public List<Sentence> Sentences { get; set; } = new();
    }

    // -----------------------------
    // SupplProvision（附則）
    // -----------------------------
    public class SupplProvision {
        [XmlElement("SupplProvisionLabel")]
        public string SupplProvisionLabel { get; set; } = "";

        [XmlElement("Article")]
        public List<Article> Articles { get; set; } = new();
    }
}
