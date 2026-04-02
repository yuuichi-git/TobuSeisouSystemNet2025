using System.Xml;
using System.Xml.Serialization;

namespace Vo {

    // ============================================================
    // ★ e-Gov 法令 API（法令本文取得）のルート要素
    //
    //   <law_data_response>
    //       <attached_files_info> … </attached_files_info>
    //       <law_info> … </law_info>
    //       <revision_info> … </revision_info>
    //       <law_full_text> … </law_full_text>
    //   </law_data_response>
    //
    // ============================================================
    [XmlRoot("law_data_response")]
    public class LawDataResponse {

        /// <summary>
        /// 添付ファイル情報（画像・図表など）
        /// </summary>
        [XmlElement("attached_files_info")]
        public AttachedFilesInfo AttachedFilesInfo { get; set; } = new();

        /// <summary>
        /// 法令番号・法令ID・公布日など、法令の基本情報
        /// </summary>
        [XmlElement("law_info")]
        public LawInfo LawInfo { get; set; } = new();

        /// <summary>
        /// 最新の改正情報（施行日・改正法令など）
        /// </summary>
        [XmlElement("revision_info")]
        public RevisionInfo RevisionInfo { get; set; } = new();

        /// <summary>
        /// 法令本文（章・節・款・条・項・号・文）
        /// </summary>
        [XmlElement("law_full_text")]
        public LawFullText LawFullText { get; set; } = new();
    }

    // ============================================================
    // ★ 添付ファイル情報（画像・図表など）
    // ============================================================
    public class AttachedFilesInfo {

        /// <summary>
        /// 添付ファイルを zip 化 → Base64 文字列化したデータ
        /// </summary>
        [XmlElement("image_data")]
        public string ImageData { get; set; } = "";

        /// <summary>
        /// 添付ファイル一覧（画像ファイルのメタ情報）
        /// </summary>
        [XmlArray("attached_files")]
        [XmlArrayItem("attached_file")]
        public List<AttachedFile> AttachedFiles { get; set; } = new();
    }

    public class AttachedFile {

        /// <summary>添付ファイルが属する改正履歴ID</summary>
        [XmlElement("law_revision_id")]
        public string LawRevisionId { get; set; } = "";

        /// <summary>画像ファイルのパス（pict フォルダ内）</summary>
        [XmlElement("src")]
        public string Src { get; set; } = "";

        /// <summary>更新日時（ISO8601）</summary>
        [XmlElement("updated")]
        public string Updated { get; set; } = "";
    }

    // ============================================================
    // ★ 法令基本情報（法令番号・公布日など）
    // ============================================================
    public class LawInfo {

        /// <summary>法令種別（Act / CabinetOrder など）</summary>
        [XmlElement("law_type")]
        public string LawType { get; set; } = "";

        /// <summary>法令ID（例：335AC0000000105）</summary>
        [XmlElement("law_id")]
        public string LawId { get; set; } = "";

        /// <summary>法令番号（例：昭和二十五年法律第五号）</summary>
        [XmlElement("law_num")]
        public string LawNum { get; set; } = "";

        /// <summary>元号（昭和 / 平成 / 令和）</summary>
        [XmlElement("law_num_era")]
        public string LawNumEra { get; set; } = "";

        /// <summary>元号の年（例：25）</summary>
        [XmlElement("law_num_year")]
        public string LawNumYear { get; set; } = "";

        /// <summary>法令種別（Act / CabinetOrder など）</summary>
        [XmlElement("law_num_type")]
        public string LawNumType { get; set; } = "";

        /// <summary>法令番号の通し番号（例：005）</summary>
        [XmlElement("law_num_num")]
        public string LawNumNum { get; set; } = "";

        /// <summary>公布日（YYYY-MM-DD）</summary>
        [XmlElement("promulgation_date")]
        public string PromulgationDate { get; set; } = "";
    }

    // ============================================================
    // ★ 最新の改正情報（施行日・改正法令など）
    // ============================================================
    public class RevisionInfo {

        /// <summary>改正履歴ID（法令ID + 日付 + 改正法ID）</summary>
        [XmlElement("law_revision_id")]
        public string LawRevisionId { get; set; } = "";

        /// <summary>改正後の法令種別</summary>
        [XmlElement("law_type")]
        public string LawType { get; set; } = "";

        /// <summary>改正後の法令タイトル</summary>
        [XmlElement("law_title")]
        public string LawTitle { get; set; } = "";

        /// <summary>法令タイトルの読み（かな）</summary>
        [XmlElement("law_title_kana")]
        public string LawTitleKana { get; set; } = "";

        /// <summary>略称</summary>
        [XmlElement("abbrev")]
        public string Abbrev { get; set; } = "";

        /// <summary>分類（行政組織など）</summary>
        [XmlElement("category")]
        public string Category { get; set; } = "";

        /// <summary>データ更新日時</summary>
        [XmlElement("updated")]
        public string Updated { get; set; } = "";

        /// <summary>改正法令の公布日</summary>
        [XmlElement("amendment_promulgate_date")]
        public string AmendmentPromulgateDate { get; set; } = "";

        /// <summary>改正法令の施行日</summary>
        [XmlElement("amendment_enforcement_date")]
        public string AmendmentEnforcementDate { get; set; } = "";

        /// <summary>施行日の備考（例：一部施行）</summary>
        [XmlElement("amendment_enforcement_comment")]
        public string AmendmentEnforcementComment { get; set; } = "";

        /// <summary>予定施行日</summary>
        [XmlElement("amendment_scheduled_enforcement_date")]
        public string AmendmentScheduledEnforcementDate { get; set; } = "";

        /// <summary>改正法令の法令ID</summary>
        [XmlElement("amendment_law_id")]
        public string AmendmentLawId { get; set; } = "";

        /// <summary>改正法令のタイトル</summary>
        [XmlElement("amendment_law_title")]
        public string AmendmentLawTitle { get; set; } = "";

        /// <summary>改正法令タイトルの読み</summary>
        [XmlElement("amendment_law_title_kana")]
        public string AmendmentLawTitleKana { get; set; } = "";

        /// <summary>改正法令番号</summary>
        [XmlElement("amendment_law_num")]
        public string AmendmentLawNum { get; set; } = "";

        /// <summary>改正種別（1:全部改正 / 3:新規制定 など）</summary>
        [XmlElement("amendment_type")]
        public string AmendmentType { get; set; } = "";

        /// <summary>廃止・失効などの状態</summary>
        [XmlElement("repeal_status")]
        public string RepealStatus { get; set; } = "";

        /// <summary>廃止日</summary>
        [XmlElement("repeal_date")]
        public string RepealDate { get; set; } = "";

        /// <summary>現行法かどうか（true = 現行）</summary>
        [XmlElement("remain_in_force")]
        public string RemainInForce { get; set; } = "";

        /// <summary>New（新規制定）/ Partial（一部改正）</summary>
        [XmlElement("mission")]
        public string Mission { get; set; } = "";

        /// <summary>現在の改正状態</summary>
        [XmlElement("current_revision_status")]
        public string CurrentRevisionStatus { get; set; } = "";
    }

    // ============================================================
    // ★ 法令本文（Law → LawBody → Chapter/Section/Subsection/Article…）
    // ============================================================
    public class LawFullText {

        /// <summary>法令本文のルート要素</summary>
        [XmlElement("Law")]
        public Law Law { get; set; } = new();
    }

    public class Law {

        /// <summary>元号の年（例：25）</summary>
        [XmlAttribute("Year")]
        public string Year { get; set; } = "";

        /// <summary>公布月</summary>
        [XmlAttribute("PromulgateMonth")]
        public string PromulgateMonth { get; set; } = "";

        /// <summary>公布日</summary>
        [XmlAttribute("PromulgateDay")]
        public string PromulgateDay { get; set; } = "";

        /// <summary>法令番号（数字部分）</summary>
        [XmlAttribute("Num")]
        public string Num { get; set; } = "";

        /// <summary>法令種別（Act など）</summary>
        [XmlAttribute("LawType")]
        public string LawType { get; set; } = "";

        /// <summary>言語（ja）</summary>
        [XmlAttribute("Lang")]
        public string Lang { get; set; } = "";

        /// <summary>元号（昭和 / 平成 / 令和）</summary>
        [XmlAttribute("Era")]
        public string Era { get; set; } = "";

        /// <summary>法令番号（例：昭和二十五年法律第五号）</summary>
        [XmlElement("LawNum")]
        public string LawNum { get; set; } = "";

        /// <summary>法令本文（章・節・款・条…）</summary>
        [XmlElement("LawBody")]
        public LawBody LawBody { get; set; } = new();
    }

    /// <summary>
    /// ★ 法令タイトル（例：道路交通法）
    ///
    ///   <LawTitle Kana="どうろこうつうほう"
    ///             AbbrevKana="こうつうほう"
    ///             Abbrev="交通法">
    ///       道路交通法
    ///   </LawTitle>
    ///
    /// ・Text … 実際の法令名（本文）
    /// ・Kana … 読み仮名（かな）
    /// ・Abbrev … 略称
    /// ・AbbrevKana … 略称の読み
    ///
    /// e-Gov XML の LawBody の先頭に必ず存在し、
    /// 法令ビューアのタイトル表示に使われる重要な要素。
    /// </summary>
    /// <summary>法令タイトル（例：道路交通法）</summary>
    public class LawTitle {

        /// <summary>法令タイトルの読み（かな）</summary>
        [XmlAttribute("Kana")]
        public string Kana { get; set; } = "";

        /// <summary>法令タイトルの略称の読み（かな）</summary>
        [XmlAttribute("AbbrevKana")]
        public string AbbrevKana { get; set; } = "";

        /// <summary>法令タイトルの略称</summary>
        [XmlAttribute("Abbrev")]
        public string Abbrev { get; set; } = "";

        /// <summary>法令タイトルの本文（例：道路交通法）</summary>
        [XmlText]
        public string Text { get; set; } = "";
    }

    public class LawBody {

        /// <summary>法令タイトル（例：道路交通法）</summary>
        [XmlElement("LawTitle")]
        public LawTitle LawTitle { get; set; } = new();

        /// <summary>本則（章・節・款・条）</summary>
        [XmlElement("MainProvision")]
        public MainProvision MainProvision { get; set; } = new();

        /// <summary>附則</summary>
        [XmlElement("SupplProvision")]
        public SupplProvision SupplProvision { get; set; } = new();

        /// <summary>その他の要素（未対応のタグを吸収）</summary>
        [XmlAnyElement]
        public XmlElement[]? OtherElements { get; set; }
    }

    // ============================================================
    // ★ 本則（MainProvision）
    //
    //   章（Chapter）
    //   ├─ 節（Section）
    //   │    ├─ 款（Subsection）
    //   │    └─ 条（Article）
    //   └─ 条（Article）
    //
    //   という構造を持つ。
    // ============================================================
    public class MainProvision {

        /// <summary>
        /// 章（Chapter）の一覧
        /// </summary>
        [XmlElement("Chapter")]
        public List<Chapter> Chapters { get; set; } = new();

        /// <summary>
        /// 章を持たない法令のために、MainProvision 直下にも条が来る可能性がある
        /// </summary>
        [XmlElement("Article")]
        public List<Article> Articles { get; set; } = new();
    }

    // ============================================================
    // ★ 章（Chapter）
    //
    //   <Chapter Num="1">
    //       <ChapterTitle>総則</ChapterTitle>
    //       <Section> … </Section>
    //       <Subsection> … </Subsection>
    //       <Article> … </Article>
    //   </Chapter>
    //
    // ============================================================
    public class Chapter {

        /// <summary>章番号（例：1, 2, 3）</summary>
        [XmlAttribute("Num")]
        public string Num { get; set; } = "";

        /// <summary>章タイトル（例：総則）</summary>
        [XmlElement("ChapterTitle")]
        public string ChapterTitle { get; set; } = "";

        /// <summary>節（Section）の一覧</summary>
        [XmlElement("Section")]
        public List<Section> Sections { get; set; } = new();

        /// <summary>章直下に款（Subsection）が来る場合に対応</summary>
        [XmlElement("Subsection")]
        public List<Subsection> Subsections { get; set; } = new();

        /// <summary>章直下に条が来る場合に対応</summary>
        [XmlElement("Article")]
        public List<Article> Articles { get; set; } = new();
    }

    // ============================================================
    // ★ 節（Section）
    //
    //   <Section Num="1">
    //       <SectionTitle>罰則</SectionTitle>
    //       <Subsection> … </Subsection>
    //       <Article> … </Article>
    //   </Section>
    //
    // ============================================================
    public class Section {

        /// <summary>節番号</summary>
        [XmlAttribute("Num")]
        public string Num { get; set; } = "";

        /// <summary>節タイトル</summary>
        [XmlElement("SectionTitle")]
        public string SectionTitle { get; set; } = "";

        /// <summary>節の下に款が来る場合に対応</summary>
        [XmlElement("Subsection")]
        public List<Subsection> Subsections { get; set; } = new();

        /// <summary>節直下に条が来る場合に対応</summary>
        [XmlElement("Article")]
        public List<Article> Articles { get; set; } = new();
    }

    // ============================================================
    // ★ 款（Subsection）
    //
    //   <Subsection Num="1">
    //       <SubsectionTitle>○○○</SubsectionTitle>
    //       <Article> … </Article>
    //   </Subsection>
    //
    // ============================================================
    public class Subsection {

        /// <summary>款番号</summary>
        [XmlAttribute("Num")]
        public string Num { get; set; } = "";

        /// <summary>款タイトル</summary>
        [XmlElement("SubsectionTitle")]
        public string SubsectionTitle { get; set; } = "";

        /// <summary>款の下にある条の一覧</summary>
        [XmlElement("Article")]
        public List<Article> Articles { get; set; } = new();
    }

    // ============================================================
    // ★ 条（Article）
    //
    //   <Article Num="117_2_2" Delete="false" Hide="false">
    //       <ArticleCaption> … </ArticleCaption>
    //       <ArticleTitle>第百十七条の二の二</ArticleTitle>
    //       <Paragraph> … </Paragraph>
    //   </Article>
    //
    // ============================================================
    public class Article {

        /// <summary>条番号（枝番含む：117_2_2 など）</summary>
        [XmlAttribute("Num")]
        public string Num { get; set; } = "";

        /// <summary>削除された条かどうか</summary>
        [XmlAttribute("Delete")]
        public string Delete { get; set; } = "";

        /// <summary>非表示指定（Hide="true"）</summary>
        [XmlAttribute("Hide")]
        public string Hide { get; set; } = "";

        /// <summary>条の見出し（存在しないことも多い）</summary>
        [XmlElement("ArticleCaption")]
        public string ArticleCaption { get; set; } = "";

        /// <summary>条タイトル（例：第百十七条の二の二）</summary>
        [XmlElement("ArticleTitle")]
        public string ArticleTitle { get; set; } = "";

        /// <summary>項（Paragraph）の一覧</summary>
        [XmlElement("Paragraph")]
        public List<Paragraph> Paragraphs { get; set; } = new();
    }

    // ============================================================
    // ★ 項（Paragraph）
    //
    //   <Paragraph Num="1">
    //       <ParagraphNum>第一項</ParagraphNum>
    //       <ParagraphSentence> … </ParagraphSentence>
    //       <Item> … </Item>
    //   </Paragraph>
    //
    // ============================================================
    public class Paragraph {

        /// <summary>項番号（1, 2, 3…）</summary>
        [XmlAttribute("Num")]
        public string Num { get; set; } = "";

        /// <summary>非表示指定</summary>
        [XmlAttribute("Hide")]
        public string Hide { get; set; } = "";

        /// <summary>旧様式（OldStyle="true"）</summary>
        [XmlAttribute("OldStyle")]
        public string OldStyle { get; set; } = "";

        /// <summary>項の見出し（存在しないことが多い）</summary>
        [XmlElement("ParagraphCaption")]
        public string ParagraphCaption { get; set; } = "";

        /// <summary>項番号の表記（例：一項 / 第一項 / 1）</summary>
        [XmlElement("ParagraphNum")]
        public string ParagraphNum { get; set; } = "";

        /// <summary>本文（Sentence の集合）</summary>
        [XmlElement("ParagraphSentence")]
        public ParagraphSentence ParagraphSentence { get; set; } = new();

        /// <summary>号（Item）の一覧</summary>
        [XmlElement("Item")]
        public List<Item> Items { get; set; } = new();
    }

    public class ParagraphSentence {
        [XmlElement("Sentence")]
        public List<Sentence> Sentences { get; set; } = new();
    }

    // ============================================================
    // ★ 文（Sentence）
    //
    //   <Sentence Num="1" WritingMode="vertical">
    //       自動車等を運転したときは…
    //   </Sentence>
    //
    // ============================================================
    public class Sentence {

        /// <summary>文番号</summary>
        [XmlAttribute("Num")]
        public string Num { get; set; } = "";

        /// <summary>縦書き・横書き（vertical / horizontal）</summary>
        [XmlAttribute("WritingMode")]
        public string WritingMode { get; set; } = "";

        /// <summary>機能属性（Function="…") ※稀に使用される</summary>
        [XmlAttribute("Function")]
        public string Function { get; set; } = "";

        /// <summary>Sentence 内に未知のタグがある場合に吸収</summary>
        [XmlAnyElement]
        public XmlElement[]? AnyElements { get; set; }

        /// <summary>文の本文テキスト</summary>
        [XmlText]
        public string Text { get; set; } = "";
    }

    // ============================================================
    // ★ 号（Item）
    //
    //   <Item Num="1">
    //       <ItemTitle> … </ItemTitle>
    //       <ItemSentence> … </ItemSentence>
    //   </Item>
    //
    // ============================================================
    public class Item {

        /// <summary>号番号（1, 2, 3…）</summary>
        [XmlAttribute("Num")]
        public string Num { get; set; } = "";

        /// <summary>号タイトル（通常は空）</summary>
        [XmlElement("ItemTitle")]
        public string ItemTitle { get; set; } = "";

        /// <summary>号の本文（Sentence の集合）</summary>
        [XmlElement("ItemSentence")]
        public ItemSentence ItemSentence { get; set; } = new();
    }

    public class ItemSentence {

        /// <summary>号の本文（Sentence の一覧）</summary>
        [XmlElement("Sentence")]
        public List<Sentence> Sentences { get; set; } = new();
    }

    // ============================================================
    // ★ 附則（SupplProvision）
    //
    //   <SupplProvision>
    //       <SupplProvisionLabel>附則</SupplProvisionLabel>
    //       <Article> … </Article>
    //   </SupplProvision>
    //
    // ============================================================
    public class SupplProvision {

        /// <summary>附則のラベル（例：附則 / 附則（令和○年法律○号））</summary>
        [XmlElement("SupplProvisionLabel")]
        public string SupplProvisionLabel { get; set; } = "";

        /// <summary>附則内の条一覧</summary>
        [XmlElement("Article")]
        public List<Article> Articles { get; set; } = new();
    }
}
