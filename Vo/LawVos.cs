namespace Vo {
    /*
     * -----------------------------------------
     * NodeType（enum）
     * -----------------------------------------
     */
    public enum LawNodeType {
        LawBody,
        LawTitle,

        // Container nodes
        Part,           // 編
        Chapter,        // 章
        Section,        // 節
        Subsection,     // 款
        Division,       // 目

        // Main structure
        Article,        // 条
        Paragraph,      // 項
        Item,           // 号
        Sentence,       // 文

        // Special structures
        SupplProvision, // 附則
        AppdxNote,      // 付録注
        TableStruct,
        Table,
        TableRow,
        TableColumn
    }

    /*
     * -----------------------------------------
     * ILawNode（統一ノード）
     * -----------------------------------------
     */
    public interface ILawNode {
        LawNodeType NodeType { get; }
        string? Title { get; }     // 表示名（第○条、第1章など）
        string? Caption { get; }   // 条名・章名など
        string? Text { get; }      // Sentence の本文
        List<ILawNode> Children { get; }
    }

    /*
     * -----------------------------------------
     * LawFullText（法令本文APIのルート）
     * -----------------------------------------
     */
    public sealed class LawFullText {
        public string Era { get; set; } = "";
        public string Lang { get; set; } = "";
        public string LawType { get; set; } = "";
        public string Num { get; set; } = "";
        public string PromulgateDay { get; set; } = "";
        public string PromulgateMonth { get; set; } = "";
        public string Year { get; set; } = "";
        public string LawNum { get; set; } = "";
        public ILawNode LawBody { get; set; } = default!;
    }

    /*
     * -----------------------------------------
     * ILawNode 実装クラス（完全版）
     * -----------------------------------------
     */

    // ルート
    public sealed class LawBodyNode : ILawNode {
        public LawNodeType NodeType => LawNodeType.LawBody;
        public string? Title => null;
        public string? Caption => null;
        public string? Text => null;
        public List<ILawNode> Children { get; set; } = new();
    }

    // 法令タイトル
    public sealed class LawTitleNode : ILawNode {
        public LawNodeType NodeType => LawNodeType.LawTitle;
        public string? Title { get; set; }
        public string? Caption => null;
        public string? Text => null;
        public List<ILawNode> Children { get; } = new();
    }

    /*
     * -----------------------------------------
     * Container Nodes（編・章・節・款・目）
     * -----------------------------------------
     */
    public sealed class PartNode : ILawNode {
        public LawNodeType NodeType => LawNodeType.Part;
        public string? Title { get; set; }     // 第○編
        public string? Caption { get; set; }   // 編名
        public string? Text => null;
        public List<ILawNode> Children { get; set; } = new();
    }

    public sealed class ChapterNode : ILawNode {
        public LawNodeType NodeType => LawNodeType.Chapter;
        public string? Title { get; set; }     // 第○章
        public string? Caption { get; set; }   // 章名
        public string? Text => null;
        public List<ILawNode> Children { get; set; } = new();
    }

    public sealed class SectionNode : ILawNode {
        public LawNodeType NodeType => LawNodeType.Section;
        public string? Title { get; set; }     // 第○節
        public string? Caption { get; set; }   // 節名
        public string? Text => null;
        public List<ILawNode> Children { get; set; } = new();
    }

    public sealed class SubsectionNode : ILawNode {
        public LawNodeType NodeType => LawNodeType.Subsection;
        public string? Title { get; set; }     // 第○款
        public string? Caption { get; set; }   // 款名
        public string? Text => null;
        public List<ILawNode> Children { get; set; } = new();
    }

    public sealed class DivisionNode : ILawNode {
        public LawNodeType NodeType => LawNodeType.Division;
        public string? Title { get; set; }     // 第○目
        public string? Caption { get; set; }   // 目名
        public string? Text => null;
        public List<ILawNode> Children { get; set; } = new();
    }

    /*
     * -----------------------------------------
     * Article / Paragraph / Item / Sentence
     * -----------------------------------------
     */
    public sealed class ArticleNode : ILawNode {
        public LawNodeType NodeType => LawNodeType.Article;
        public string? Title { get; set; }     // 第○条
        public string? Caption { get; set; }   // 条名
        public string? Text => null;
        public List<ILawNode> Children { get; set; } = new();
    }

    public sealed class ParagraphNode : ILawNode {
        public LawNodeType NodeType => LawNodeType.Paragraph;
        public string? Title { get; set; }     // ○項
        public string? Caption { get; set; }   // 項名（あれば）
        public string? Text => null;
        public List<ILawNode> Children { get; set; } = new();
    }

    public sealed class ItemNode : ILawNode {
        public LawNodeType NodeType => LawNodeType.Item;
        public string? Title { get; set; }     // ○号
        public string? Caption { get; set; }
        public string? Text => null;
        public List<ILawNode> Children { get; set; } = new();
    }

    public sealed class SentenceNode : ILawNode {
        public LawNodeType NodeType => LawNodeType.Sentence;
        public string? Title => null;
        public string? Caption => null;
        public string? Text { get; set; }      // 文の本文
        public List<ILawNode> Children { get; } = new();
    }

    /*
     * -----------------------------------------
     * Special Nodes（附則・付録・表）
     * -----------------------------------------
     */
    public sealed class SupplProvisionNode : ILawNode {
        public LawNodeType NodeType => LawNodeType.SupplProvision;
        public string? Title { get; set; }     // 附則
        public string? Caption => null;
        public string? Text => null;
        public List<ILawNode> Children { get; set; } = new();
    }

    public sealed class AppdxNoteNode : ILawNode {
        public LawNodeType NodeType => LawNodeType.AppdxNote;
        public string? Title { get; set; }
        public string? Caption { get; set; }
        public string? Text => null;
        public List<ILawNode> Children { get; set; } = new();
    }

    public sealed class TableStructNode : ILawNode {
        public LawNodeType NodeType => LawNodeType.TableStruct;
        public string? Title { get; set; }
        public string? Caption => null;
        public string? Text => null;
        public List<ILawNode> Children { get; set; } = new();
    }

    public sealed class TableNode : ILawNode {
        public LawNodeType NodeType => LawNodeType.Table;
        public string? Title => null;
        public string? Caption => null;
        public string? Text => null;
        public List<ILawNode> Children { get; set; } = new();
    }

    public sealed class TableRowNode : ILawNode {
        public LawNodeType NodeType => LawNodeType.TableRow;
        public string? Title => null;
        public string? Caption => null;
        public string? Text => null;
        public List<ILawNode> Children { get; set; } = new();
    }

    public sealed class TableColumnNode : ILawNode {
        public LawNodeType NodeType => LawNodeType.TableColumn;
        public string? Title => null;
        public string? Caption => null;
        public string? Text => null;
        public List<ILawNode> Children { get; set; } = new();
    }






    /*
     * -----------------------------------------
     * KeyWord検索API の VO
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
        public string ChapterNum { get; set; } = "";
        public string ChapterTitle { get; set; } = "";
        public List<LawArticle> Articles { get; set; } = new();
    }

    /// <summary>
    /// 節
    /// </summary>
    public class LawSection {
        public string SectionNum { get; set; } = "";
        public string SectionTitle { get; set; } = "";
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
        public string ArticleNum { get; set; } = "";
        public string ArticleTitle { get; set; } = "";

        public List<LawParagraph> Paragraphs { get; set; } = new();
    }

    /// <summary>
    /// 項
    /// </summary>
    public class LawParagraph {
        public string ParagraphNum { get; set; } = "";
        public string Text { get; set; } = "";

        public List<LawItem> Items { get; set; } = new();
    }

    /// <summary>
    /// 号
    /// </summary>
    public class LawItem {
        public string ItemNum { get; set; } = "";
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