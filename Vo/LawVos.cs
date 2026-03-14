namespace Vo {
    public enum LawStatus {
        Active,             // 有効
        NotYetEnforced,     // 未施行
        Repealed            // 廃止
    }

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
        public List<LawRevisionInfo> Revisions { get; set; } = new();

    }

    // ★ law_data 用の改正情報 VO（最低限版）
    public sealed class LawRevisionInfo {
        public string AmendmentLawId { get; set; } = "";          // 改正法の LawId
        public string AmendmentLawNum { get; set; } = "";         // 改正法の法令番号
        public string AmendmentLawTitle { get; set; } = "";       // 改正法の題名
        public DateTime? AmendmentPromulgateDate { get; set; }    // 改正法の公布日
        public DateTime? AmendmentEnforcementDate { get; set; }   // 改正の施行日
        public string AmendmentEnforcementComment { get; set; } = ""; // 経過措置など
        public int AmendmentType { get; set; }                    // 改正種別（全部改正・一部改正など）
    }

    /// <summary>
    /// 本文全体
    /// </summary>
    public class LawBody {
        public List<LawPart> Parts { get; } = new();
        public List<LawChapter> Chapters { get; } = new();
        public List<LawSection> Sections { get; } = new();
        public List<LawSubsection> Subsections { get; } = new();
        public List<LawDivision> Divisions { get; } = new();
        public List<LawArticle> Articles { get; } = new();

        public LawSupplProvision? SupplProvision { get; set; }
        public LawPreface? Preface { get; set; }
    }

    public class LawPart : ILawNode {
        public string DisplayTitle => PartTitle;
        public string PartNum { get; set; } = "";
        public string PartTitle { get; set; } = "";

        public List<LawChapter> Chapters { get; } = new();
        public List<LawArticle> Articles { get; } = new();

        public string NodeType => "Part";
        public IReadOnlyList<ILawNode> Children =>
            Chapters.Cast<ILawNode>()
                    .Concat(Articles)
                    .ToList();
        public LawStatus Status { get; set; } = LawStatus.Active;
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
    public class LawChapter : ILawNode {
        public string DisplayTitle => ChapterTitle ?? "";
        public string? ChapterNum { get; set; }
        public string? ChapterTitle { get; set; }

        public List<LawSection> Sections { get; set; } = new();
        public List<LawArticle> Articles { get; set; } = new();

        public string NodeType => "Chapter";
        public IReadOnlyList<ILawNode> Children => Sections.Cast<ILawNode>().Concat(Articles).ToList();
        public LawStatus Status { get; set; } = LawStatus.Active;
    }


    /// <summary>
    /// 節
    /// </summary>
    public class LawSection : ILawNode {
        public string DisplayTitle => SectionTitle ?? "";
        public string? SectionNum { get; set; }
        public string? SectionTitle { get; set; }

        public List<LawArticle> Articles { get; set; } = new();

        public string NodeType => "Section";
        public IReadOnlyList<ILawNode> Children => Articles.Cast<ILawNode>().ToList();
        public LawStatus Status { get; set; } = LawStatus.Active;
    }


    /// <summary>
    /// 款
    /// </summary>
    public class LawSubsection : ILawNode {
        public string DisplayTitle => SubsectionTitle;
        public string SubsectionNum { get; set; } = "";
        public string SubsectionTitle { get; set; } = "";
        public List<LawArticle> Articles { get; set; } = new();

        public string NodeType => "Subsection";
        public IReadOnlyList<ILawNode> Children => Articles.Cast<ILawNode>().ToList();
        public LawStatus Status { get; set; } = LawStatus.Active;
    }

    /// <summary>
    /// 目
    /// </summary>
    public class LawDivision : ILawNode {
        public string DisplayTitle => DivisionTitle;
        public string DivisionNum { get; set; } = "";
        public string DivisionTitle { get; set; } = "";
        public List<LawArticle> Articles { get; set; } = new();

        public string NodeType => "Division";
        public IReadOnlyList<ILawNode> Children => Articles.Cast<ILawNode>().ToList();
        public LawStatus Status { get; set; } = LawStatus.Active;
    }

    /// <summary>
    /// 条
    /// </summary>
    public class LawArticle : ILawNode {
        public string DisplayTitle {
            get {
                if (string.IsNullOrWhiteSpace(ArticleNum))
                    return ArticleTitle;

                // 見出しが「第◯条」だけの場合（＝見出しなし）
                string? trimmed = ArticleTitle?.Replace("　", "").Trim();
                if (string.IsNullOrWhiteSpace(trimmed) || trimmed == $"第{ArticleNum}条") {
                    return $"第{ArticleNum}条";
                } else {
                    // ★ 見出しがある場合だけ括弧付き
                    return $"第{ArticleNum}条{ArticleCaption}";
                }
            }
        }
        public string ArticleNum { get; set; } = "";
        public string ArticleTitle { get; set; } = "";
        public string ArticleCaption { get; set; } = "";   // ★ 追加

        public List<LawParagraph> Paragraphs { get; set; } = new();

        // ★ デフォルトは有効
        public LawStatus Status { get; set; } = LawStatus.Active;
        public string NodeType => "Article";
        public IReadOnlyList<ILawNode> Children => Paragraphs.Cast<ILawNode>().ToList();
    }

    /// <summary>
    /// 項
    /// </summary>
    public class LawParagraph : ILawNode {
        public string DisplayTitle {
            get {
                if (string.IsNullOrWhiteSpace(ParagraphNum))
                    return "項番号なし";

                return $"{ParagraphNum}項";  // ★ ここで「項」を付ける
            }
        }
        public string? ParagraphNum { get; set; }
        public List<string> Sentences { get; set; } = new();
        public string Text { get; set; } = "";
        public List<LawItem> Items { get; set; } = new();

        public string NodeType => "Paragraph";
        public IReadOnlyList<ILawNode> Children => Items.Cast<ILawNode>().ToList();
        public LawStatus Status { get; set; } = LawStatus.Active;
    }

    /// <summary>
    /// 号
    /// </summary>
    public class LawItem : ILawNode {
        public string DisplayTitle {
            get {
                if (string.IsNullOrWhiteSpace(ItemNum))
                    return "号番号なし";

                return $"{ItemNum}号";
            }
        }
        public string? ItemNum { get; set; }
        public List<string> Sentences { get; set; } = new();
        public string Text { get; set; } = "";

        public string NodeType => "Item";
        public IReadOnlyList<ILawNode> Children => Array.Empty<ILawNode>();
        public LawStatus Status { get; set; } = LawStatus.Active;
    }

    /// <summary>
    /// 附則
    /// </summary>
    public class LawSupplProvision : ILawNode {
        public string DisplayTitle => "附則";
        public List<LawSupplParagraph> Paragraphs { get; set; } = new();
        public string NodeType => "SupplProvision";
        public IReadOnlyList<ILawNode> Children => Paragraphs.Cast<ILawNode>().ToList();
        public LawStatus Status { get; set; } = LawStatus.Active;
    }

    /// <summary>
    /// 附則の項
    /// </summary>
    public class LawSupplParagraph : ILawNode {
        public string DisplayTitle {
            get {
                if (string.IsNullOrWhiteSpace(ParagraphNum))
                    return "本文";

                return $"{ParagraphNum}項";
            }
        }
        public string ParagraphNum { get; set; } = "";
        public string Text { get; set; } = "";
        public string NodeType => "SupplParagraph";
        public IReadOnlyList<ILawNode> Children => Array.Empty<ILawNode>();
        public LawStatus Status { get; set; } = LawStatus.Active;
    }

    public interface ILawNode {
        string DisplayTitle { get; }
        string NodeType { get; }
        IReadOnlyList<ILawNode> Children { get; }
        // ★ 追加：法令の有効性ステータス
        LawStatus Status { get; }
    }

}