using System.Text.RegularExpressions;
using System.Xml.Linq;

using Vo;

using static EGov.LawView;

namespace EGov {

    public interface IParser<TIn, TOut> {
        TOut Parse(TIn input);
    }

    public sealed class LawParser : IParser<XDocument, LawDataResponse> {
        public LawDataResponse Parse(XDocument doc) {
            var root = doc.Root;
            if (root == null)
                return new LawDataResponse();

            var result = new LawDataResponse {
                AttachedFilesInfo = new AttachedFilesParser().Parse(root.Element("attached_files_info")),
                LawInfo = new LawInfoParser().Parse(root.Element("law_info")),
                RevisionInfo = new RevisionInfoParser().Parse(root.Element("revision_info")),
                LawFullText = new LawBodyParser().Parse(root.Element("law_full_text"))
            };

            return result;
        }
    }

    public sealed class AttachedFilesParser : IParser<XElement?, AttachedFilesInfo> {
        public AttachedFilesInfo Parse(XElement? node) {
            var info = new AttachedFilesInfo();
            if (node == null)
                return info;

            info.ImageData = ElementNormalizer.Get(node, "image_data");

            var attachedFilesNode = node.Element("attached_files");
            if (attachedFilesNode != null) {
                foreach (var af in attachedFilesNode.Elements("attached_file")) {
                    var file = new AttachedFile {
                        LawRevisionId = ElementNormalizer.Get(af, "law_revision_id"),
                        Src = ElementNormalizer.Get(af, "src"),
                        Updated = ElementNormalizer.Get(af, "updated")
                    };
                    info.AttachedFiles.Add(file);
                }
            }

            return info;
        }
    }

    public sealed class LawInfoParser : IParser<XElement?, LawInfo> {
        public LawInfo Parse(XElement? node) {
            var info = new LawInfo();
            if (node == null)
                return info;

            info.LawType = ElementNormalizer.Get(node, "law_type");
            info.LawId = ElementNormalizer.Get(node, "law_id");
            info.LawNum = ElementNormalizer.Get(node, "law_num");
            info.LawNumEra = ElementNormalizer.Get(node, "law_num_era");
            info.LawNumYear = ElementNormalizer.Get(node, "law_num_year");
            info.LawNumType = ElementNormalizer.Get(node, "law_num_type");
            info.LawNumNum = ElementNormalizer.Get(node, "law_num_num");
            info.PromulgationDate = ElementNormalizer.Get(node, "promulgation_date");

            return info;
        }
    }

    public sealed class RevisionInfoParser : IParser<XElement?, RevisionInfo> {
        public RevisionInfo Parse(XElement? node) {
            var info = new RevisionInfo();
            if (node == null)
                return info;

            info.LawRevisionId = ElementNormalizer.Get(node, "law_revision_id");
            info.LawType = ElementNormalizer.Get(node, "law_type");
            info.LawTitle = ElementNormalizer.Get(node, "law_title");
            info.LawTitleKana = ElementNormalizer.Get(node, "law_title_kana");
            info.Abbrev = ElementNormalizer.Get(node, "abbrev");
            info.Category = ElementNormalizer.Get(node, "category");
            info.Updated = ElementNormalizer.Get(node, "updated");
            info.AmendmentPromulgateDate = ElementNormalizer.Get(node, "amendment_promulgate_date");
            info.AmendmentEnforcementDate = ElementNormalizer.Get(node, "amendment_enforcement_date");
            info.AmendmentEnforcementComment = ElementNormalizer.Get(node, "amendment_enforcement_comment");
            info.AmendmentScheduledEnforcementDate = ElementNormalizer.Get(node, "amendment_scheduled_enforcement_date");
            info.AmendmentLawId = ElementNormalizer.Get(node, "amendment_law_id");
            info.AmendmentLawTitle = ElementNormalizer.Get(node, "amendment_law_title");
            info.AmendmentLawTitleKana = ElementNormalizer.Get(node, "amendment_law_title_kana");
            info.AmendmentLawNum = ElementNormalizer.Get(node, "amendment_law_num");
            info.AmendmentType = ElementNormalizer.Get(node, "amendment_type");
            info.RepealStatus = ElementNormalizer.Get(node, "repeal_status");
            info.RepealDate = ElementNormalizer.Get(node, "repeal_date");
            info.RemainInForce = ElementNormalizer.Get(node, "remain_in_force");
            info.Mission = ElementNormalizer.Get(node, "mission");
            info.CurrentRevisionStatus = ElementNormalizer.Get(node, "current_revision_status");

            return info;
        }
    }

    public sealed class LawBodyParser : IParser<XElement?, LawFullText> {
        public LawFullText Parse(XElement? node) {
            var full = new LawFullText();
            if (node == null)
                return full;

            var lawNode = node.Element("Law");
            if (lawNode == null)
                return full;

            var law = new Law {
                Year = AttributeNormalizer.Get(lawNode, "Year"),
                PromulgateMonth = AttributeNormalizer.Get(lawNode, "PromulgateMonth"),
                PromulgateDay = AttributeNormalizer.Get(lawNode, "PromulgateDay"),
                Num = AttributeNormalizer.Get(lawNode, "Num"),
                LawType = AttributeNormalizer.Get(lawNode, "LawType"),
                Lang = AttributeNormalizer.Get(lawNode, "Lang"),
                Era = AttributeNormalizer.Get(lawNode, "Era"),
                LawNum = ElementNormalizer.Get(lawNode, "LawNum")
            };

            var body = lawNode.Element("LawBody");
            if (body != null) {
                var lawBody = new LawBody();

                var titleNode = body.Element("LawTitle");
                if (titleNode != null) {
                    lawBody.LawTitle = new LawTitle {
                        Kana = AttributeNormalizer.Get(titleNode, "Kana"),
                        AbbrevKana = AttributeNormalizer.Get(titleNode, "AbbrevKana"),
                        Abbrev = AttributeNormalizer.Get(titleNode, "Abbrev"),
                        Text = titleNode.Value ?? string.Empty
                    };
                }

                var mainNode = body.Element("MainProvision");
                if (mainNode != null)
                    lawBody.MainProvision = new MainProvisionParser().Parse(mainNode);

                var supplNode = body.Element("SupplProvision");
                if (supplNode != null)
                    lawBody.SupplProvision = new SupplProvisionParser().Parse(supplNode);

                law.LawBody = lawBody;
            }

            full.Law = law;
            return full;
        }
    }

    public sealed class MainProvisionParser : IParser<XElement, MainProvision> {
        public MainProvision Parse(XElement node) {
            var mp = new MainProvision();

            // -----------------------------
            // 1. Chapter（章）
            // -----------------------------
            foreach (var chapterNode in node.Elements("Chapter")) {
                var chapter = new Chapter {
                    Num = AttributeNormalizer.Get(chapterNode, "Num"),
                    ChapterTitle = ElementNormalizer.Get(chapterNode, "ChapterTitle")
                };

                // -----------------------------
                // 1-1. Section（節）
                // -----------------------------
                foreach (var sectionNode in chapterNode.Elements("Section")) {
                    var section = new Section {
                        Num = AttributeNormalizer.Get(sectionNode, "Num"),
                        SectionTitle = ElementNormalizer.Get(sectionNode, "SectionTitle")
                    };

                    // -----------------------------
                    // 1-1-1. Subsection（款）★ 新規追加
                    // -----------------------------
                    foreach (var subNode in sectionNode.Elements("Subsection")) {
                        var subsection = new Subsection {
                            Num = AttributeNormalizer.Get(subNode, "Num"),
                            SubsectionTitle = ElementNormalizer.Get(subNode, "SubsectionTitle")
                        };

                        // 款の下の条
                        foreach (var artNode in subNode.Elements("Article")) {
                            var article = new ArticleParser().Parse(artNode);
                            subsection.Articles.Add(article);
                        }

                        section.Subsections.Add(subsection);
                    }

                    // -----------------------------
                    // 1-1-2. 節直下の Article（条）
                    // -----------------------------
                    foreach (var artNode in sectionNode.Elements("Article")) {
                        var article = new ArticleParser().Parse(artNode);
                        section.Articles.Add(article);
                    }

                    chapter.Sections.Add(section);
                }

                // -----------------------------
                // 1-2. Chapter 直下の Subsection（款）★ 任意対応
                // -----------------------------
                foreach (var subNode in chapterNode.Elements("Subsection")) {
                    var subsection = new Subsection {
                        Num = AttributeNormalizer.Get(subNode, "Num"),
                        SubsectionTitle = ElementNormalizer.Get(subNode, "SubsectionTitle")
                    };

                    foreach (var artNode in subNode.Elements("Article")) {
                        var article = new ArticleParser().Parse(artNode);
                        subsection.Articles.Add(article);
                    }

                    chapter.Subsections.Add(subsection);
                }

                // -----------------------------
                // 1-3. Chapter 直下の Article（条）
                // -----------------------------
                foreach (var artNode in chapterNode.Elements("Article")) {
                    var article = new ArticleParser().Parse(artNode);
                    chapter.Articles.Add(article);
                }

                mp.Chapters.Add(chapter);
            }

            // -----------------------------
            // 2. MainProvision 直下の Article（章なし法令）
            // -----------------------------
            foreach (var artNode in node.Elements("Article")) {
                var article = new ArticleParser().Parse(artNode);
                mp.Articles.Add(article);
            }

            return mp;
        }
    }

    public sealed class SupplProvisionParser : IParser<XElement, SupplProvision> {
        public SupplProvision Parse(XElement node) {
            var sp = new SupplProvision {
                SupplProvisionLabel = ElementNormalizer.Get(node, "SupplProvisionLabel")
            };

            foreach (var aNode in node.Elements("Article")) {
                var article = new ArticleParser().Parse(aNode);
                sp.Articles.Add(article);
            }

            return sp;
        }
    }

    public sealed class ArticleParser : IParser<XElement, Article> {
        public Article Parse(XElement node) {
            var article = new Article {
                Num = AttributeNormalizer.Get(node, "Num"),
                Delete = AttributeNormalizer.Get(node, "Delete"),
                Hide = AttributeNormalizer.Get(node, "Hide"),
                ArticleCaption = ElementNormalizer.Get(node, "ArticleCaption"),
                ArticleTitle = ElementNormalizer.Get(node, "ArticleTitle")
            };

            // ★ Num の揺れを完全吸収（ここが最重要）
            article.Num = NormalizeArticleNum(article.Num, article.ArticleTitle);

            // ★ Paragraph パース
            foreach (var pNode in node.Elements("Paragraph")) {
                var para = new ParagraphParser().Parse(pNode);
                article.Paragraphs.Add(para);
            }

            return article;
        }

        // ============================================================
        // ★ Num 抽出ロジック（壊れない・揺れゼロ）
        // ============================================================
        private string NormalizeArticleNum(string? rawNum, string? articleTitle) {
            // 1. XML の Num 属性がある場合（最優先）
            if (!string.IsNullOrWhiteSpace(rawNum)) {
                // 50_2 → 50-2
                if (rawNum.Contains("_"))
                    return rawNum.Replace("_", "-");

                return rawNum;
            }

            // 2. Num 属性が無い場合 → ArticleTitle から抽出
            if (string.IsNullOrWhiteSpace(articleTitle))
                return "";

            // 例：第五十条の二
            var m = Regex.Match(articleTitle, @"第(.+?)条(?:の(.+))?");
            if (!m.Success)
                return "";

            var left = m.Groups[1].Value;   // 五十
            var right = m.Groups[2].Value;  // 二（存在しない場合もある）

            if (string.IsNullOrWhiteSpace(right)) {
                // 第五十条 → 50
                return NumberConverter.KanjiOrNumberToInt(left).ToString();
            } else {
                // 第五十条の二 → 50-2
                return $"{NumberConverter.KanjiOrNumberToInt(left)}-{NumberConverter.KanjiOrNumberToInt(right)}";
            }
        }
    }

    public sealed class ChapterParser : IParser<XElement, Chapter> {
        public Chapter Parse(XElement node) {
            var chapter = new Chapter {
                Num = AttributeNormalizer.Get(node, "Num"),
                ChapterTitle = ElementNormalizer.Get(node, "ChapterTitle")
            };

            chapter.Num = NormalizeChapterNum(chapter.Num, chapter.ChapterTitle);

            foreach (var sNode in node.Elements("Section")) {
                var section = new SectionParser().Parse(sNode);
                chapter.Sections.Add(section);
            }

            foreach (var aNode in node.Elements("Article")) {
                var article = new ArticleParser().Parse(aNode);
                chapter.Articles.Add(article);
            }

            return chapter;
        }

        private string NormalizeChapterNum(string? rawNum, string? title) {
            if (!string.IsNullOrWhiteSpace(rawNum))
                return rawNum;

            if (string.IsNullOrWhiteSpace(title))
                return "";

            // 例：第三章
            var m = Regex.Match(title, @"第(.+?)章");
            if (!m.Success)
                return "";

            return NumberConverter.KanjiOrNumberToInt(m.Groups[1].Value).ToString();
        }
    }

    public sealed class SectionParser : IParser<XElement, Section> {
        public Section Parse(XElement node) {
            var section = new Section {
                Num = AttributeNormalizer.Get(node, "Num"),
                SectionTitle = ElementNormalizer.Get(node, "SectionTitle")
            };

            section.Num = NormalizeSectionNum(section.Num, section.SectionTitle);

            foreach (var ssNode in node.Elements("Subsection")) {
                var subsection = new SubsectionParser().Parse(ssNode);
                section.Subsections.Add(subsection);
            }

            foreach (var aNode in node.Elements("Article")) {
                var article = new ArticleParser().Parse(aNode);
                section.Articles.Add(article);
            }

            return section;
        }

        private string NormalizeSectionNum(string? rawNum, string? title) {
            if (!string.IsNullOrWhiteSpace(rawNum))
                return rawNum;

            if (string.IsNullOrWhiteSpace(title))
                return "";

            // 例：第一節
            var m = Regex.Match(title, @"第(.+?)節");
            if (!m.Success)
                return "";

            return NumberConverter.KanjiOrNumberToInt(m.Groups[1].Value).ToString();
        }
    }

    public sealed class SubsectionParser : IParser<XElement, Subsection> {
        public Subsection Parse(XElement node) {
            var subsection = new Subsection {
                Num = AttributeNormalizer.Get(node, "Num"),
                SubsectionTitle = ElementNormalizer.Get(node, "SubsectionTitle")
            };

            subsection.Num = NormalizeSubsectionNum(subsection.Num, subsection.SubsectionTitle);

            foreach (var aNode in node.Elements("Article")) {
                var article = new ArticleParser().Parse(aNode);
                subsection.Articles.Add(article);
            }

            return subsection;
        }

        private string NormalizeSubsectionNum(string? rawNum, string? title) {
            if (!string.IsNullOrWhiteSpace(rawNum))
                return rawNum;

            if (string.IsNullOrWhiteSpace(title))
                return "";

            // 例：第一款
            var m = Regex.Match(title, @"第(.+?)款");
            if (!m.Success)
                return "";

            return NumberConverter.KanjiOrNumberToInt(m.Groups[1].Value).ToString();
        }
    }

    public sealed class ParagraphParser : IParser<XElement, Paragraph> {
        public Paragraph Parse(XElement node) {
            var para = new Paragraph {
                Num = AttributeNormalizer.Get(node, "Num"),
                ParagraphNum = ElementNormalizer.Get(node, "ParagraphNum")
            };

            para.Num = NormalizeParagraphNum(para.Num, para.ParagraphNum);

            foreach (var sNode in node.Elements("ParagraphSentence")) {
                var sentence = new ParagraphSentenceParser().Parse(sNode);
                para.ParagraphSentence = sentence;
            }

            foreach (var iNode in node.Elements("Item")) {
                var item = new ItemParser().Parse(iNode);
                para.Items.Add(item);
            }

            return para;
        }

        private string NormalizeParagraphNum(string? rawNum, string? title) {
            if (!string.IsNullOrWhiteSpace(rawNum))
                return rawNum;

            if (string.IsNullOrWhiteSpace(title))
                return "";

            // 例：２
            return NumberConverter.KanjiOrNumberToInt(title).ToString();
        }
    }

    public sealed class ParagraphSentenceParser : IParser<XElement, ParagraphSentence> {
        public ParagraphSentence Parse(XElement node) {
            var ps = new ParagraphSentence();

            // 1. 直接 Sentence がある場合
            foreach (var sNode in node.Elements("Sentence")) {
                var sent = new SentenceParser().Parse(sNode);
                ps.Sentences.Add(sent);
            }

            // 2. Column の中に Sentence がある場合
            foreach (var col in node.Elements("Column")) {
                foreach (var sNode in col.Elements("Sentence")) {
                    var sent = new SentenceParser().Parse(sNode);
                    ps.Sentences.Add(sent);
                }
            }

            return ps;
        }
    }

    public sealed class ItemParser : IParser<XElement, Item> {
        public Item Parse(XElement node) {
            var item = new Item {
                Num = AttributeNormalizer.Get(node, "Num"),
                ItemTitle = ElementNormalizer.Get(node, "ItemTitle")
            };

            // ★ Num の揺れを完全吸収
            item.Num = NormalizeItemNum(item.Num, item.ItemTitle);

            // ★ ItemSentence パース（Column 対応）
            var isNode = node.Element("ItemSentence");
            if (isNode != null) {
                var isent = new ItemSentence();

                // 1. 直接 Sentence
                foreach (var sNode in isNode.Elements("Sentence")) {
                    var sent = new SentenceParser().Parse(sNode);
                    isent.Sentences.Add(sent);
                }

                // 2. Column の中の Sentence
                foreach (var col in isNode.Elements("Column")) {
                    foreach (var sNode in col.Elements("Sentence")) {
                        var sent = new SentenceParser().Parse(sNode);
                        isent.Sentences.Add(sent);
                    }
                }

                item.ItemSentence = isent;
            }

            return item;
        }

        // ============================================================
        // ★ Num 抽出ロジック（壊れない・揺れゼロ）
        // ============================================================
        private string NormalizeItemNum(string? rawNum, string? title) {
            // 1. XML の Num 属性がある場合（最優先）
            if (!string.IsNullOrWhiteSpace(rawNum))
                return rawNum;

            // 2. Num が無い場合 → ItemTitle から抽出
            if (string.IsNullOrWhiteSpace(title))
                return "";

            // 例：一 → 1
            return NumberConverter.KanjiOrNumberToInt(title).ToString();
        }
    }

    public sealed class SentenceParser : IParser<XElement, Sentence> {
        public Sentence Parse(XElement node) {
            var sentence = new Sentence {
                Num = AttributeNormalizer.Get(node, "Num"),
                WritingMode = AttributeNormalizer.Get(node, "WritingMode"),
                Function = AttributeNormalizer.Get(node, "Function"),
                Text = RubyTextNormalizer.Extract(node)
            };

            return sentence;
        }
    }
}
