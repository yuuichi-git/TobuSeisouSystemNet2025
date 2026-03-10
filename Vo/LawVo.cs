namespace Vo {
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    // ============================================================
    //  検索結果系
    // ============================================================

    public class LawSearchResultVO {
        public int TotalCount {
            get; set;
        }
        public int SentenceCount {
            get; set;
        }
        public List<LawItemVO> Items { get; set; } = new();
    }

    public class LawItemVO {
        public LawInfoVO LawInfo { get; set; } = new();
        public RevisionInfoVO RevisionInfo { get; set; } = new();
        public List<SentenceVO> Sentences { get; set; } = new();
    }

    public class LawInfoVO {
        public string LawType { get; set; } = string.Empty;
        public string LawId { get; set; } = string.Empty;
        public string LawNum { get; set; } = string.Empty;
        public string LawNumEra { get; set; } = string.Empty;
        public int LawNumYear {
            get; set;
        }
        public string LawNumType { get; set; } = string.Empty;
        public string LawNumNum { get; set; } = string.Empty;
        public string PromulgationDate { get; set; } = string.Empty;
    }

    public class RevisionInfoVO {
        public string LawRevisionId { get; set; } = string.Empty;
        public string LawType { get; set; } = string.Empty;
        public string LawTitle { get; set; } = string.Empty;
        public string LawTitleKana { get; set; } = string.Empty;
        public string Abbrev { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Updated { get; set; } = string.Empty;

        public string AmendmentPromulgateDate { get; set; } = string.Empty;
        public string AmendmentEnforcementDate { get; set; } = string.Empty;
        public string? AmendmentEnforcementComment {
            get; set;
        }
        public string? AmendmentScheduledEnforcementDate {
            get; set;
        }

        public string AmendmentLawId { get; set; } = string.Empty;
        public string AmendmentLawTitle { get; set; } = string.Empty;
        public string? AmendmentLawTitleKana {
            get; set;
        }
        public string AmendmentLawNum { get; set; } = string.Empty;

        public string AmendmentType { get; set; } = string.Empty;
        public string RepealStatus { get; set; } = string.Empty;
        public string? RepealDate {
            get; set;
        }
        public bool RemainInForce {
            get; set;
        }
        public string Mission { get; set; } = string.Empty;
        public string CurrentRevisionStatus { get; set; } = string.Empty;
    }

    public class SentenceVO {
        public string Position { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }

    // ============================================================
    //  法令詳細（全文）
    // ============================================================

    public class LawDetailVO {
        public object? AttachedFilesInfo {
            get; set;
        }
        public LawInfoVO LawInfo { get; set; } = new();
        public RevisionInfoVO RevisionInfo { get; set; } = new();

        /// <summary>
        /// 生の XML 構造
        /// </summary>
        public LawFullTextNodeVO LawFullText { get; set; } = new();

        /// <summary>
        /// TreeView 用に整形済みの構造
        /// </summary>
        public LawViewVo LawViewVo { get; set; } = new();
    }

    /// <summary>
    /// law_full_text の再帰構造
    /// </summary>
    public class LawFullTextNodeVO {
        public string Tag { get; set; } = string.Empty;
        public Dictionary<string, string> Attr { get; set; } = new();
        public List<object> Children { get; set; } = new();
    }

    // ============================================================
    //  TreeView 用の整形済み構造
    // ============================================================
    /// <summary>
    /// ENum
    /// </summary>
    public enum LawNodeType {
        Article, Paragraph, Sentence
    }

    /// <summary>
    /// インターフェース
    /// </summary>
    public interface ILawNode {
        LawNodeType NodeType { get; }
        string DisplayText { get; }
        string NormalizedKey { get; }
    }

    //public class LawViewVo {
    //    public string LawId { get; set; } = string.Empty;
    //    public string LawNum { get; set; } = string.Empty;
    //    public string LawTitle { get; set; } = string.Empty;
    //    public List<ArticleView> Articles { get; set; } = new();
    //}

    public class LawViewVo {
        public string LawId { get; set; } = "";
        public string LawNum { get; set; } = "";
        public string LawTitle { get; set; } = "";

        public List<ChapterView> Chapters { get; set; } = new();   // ★追加
    }


    /// <summary>
    /// 章
    /// </summary>
    public class ChapterView {
        public string ChapterNum { get; set; } = "";
        public string ChapterTitle { get; set; } = "";
        public List<ArticleView> Articles { get; set; } = new();
    }


    public class ArticleView : ILawNode {
        public string ArticleNum { get; set; } = "";
        public string ArticleTitle { get; set; } = "";
        public string NormalizedKey { get; set; } = "";
        public List<ParagraphView> Paragraphs { get; set; } = new();

        public LawNodeType NodeType => LawNodeType.Article;

        public string DisplayText {
            get {
                // ① タイトルが空 → 「第◯条」
                if (string.IsNullOrWhiteSpace(ArticleTitle))
                    return $"第{ArticleNum}条";

                var t = ArticleTitle.Trim();

                // ② 「第一条」「第二条」など → タイトルなし扱い
                if (t == $"第{ArticleNum}条")
                    return $"第{ArticleNum}条";

                // ③ 括弧付き → そのまま
                if (t.StartsWith("（") && t.EndsWith("）"))
                    return $"第{ArticleNum}条{t}";

                // ④ 「第一条　目的」など → 後半だけ抽出
                var parts = t.Split('　', ' ', '\t');
                if (parts.Length >= 2) {
                    var title = parts.Last();
                    return $"第{ArticleNum}条（{title}）";
                }

                // ⑤ その他 → 括弧で包む
                return $"第{ArticleNum}条（{t}）";
            }
        }
    }

    public class ParagraphView : ILawNode {
        public string ParagraphNum { get; set; } = "";
        public string ParagraphTitle { get; set; } = "";   // ★追加
        public string NormalizedKey { get; set; } = "";
        public List<SentenceView> Sentences { get; set; } = new();

        public LawNodeType NodeType => LawNodeType.Paragraph;

        public string DisplayText {
            get {
                // ① 項番号なし
                if (string.IsNullOrWhiteSpace(ParagraphNum)) {
                    if (!string.IsNullOrWhiteSpace(ParagraphTitle))
                        return $"（項番号なし）（{ParagraphTitle}）";

                    return "（項番号なし）";
                }

                // ② タイトルなし
                if (string.IsNullOrWhiteSpace(ParagraphTitle))
                    return $"{ParagraphNum}項";

                // ③ タイトルあり
                return $"{ParagraphNum}項（{ParagraphTitle}）";
            }
        }
    }


    public class SentenceView : ILawNode {
        public string SentenceNum { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string NormalizedKey { get; set; } = string.Empty;
        public LawNodeType NodeType => LawNodeType.Sentence;
        public string DisplayText => string.IsNullOrEmpty(SentenceNum) ? Text : $"{SentenceNum}　{Text}";
    }


    /*
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     */
    public static class LawViewBuilder {
        public static LawViewVo Build(LawDetailVO detail) {
            var view = new LawViewVo {
                LawId = detail.LawInfo.LawId,
                LawNum = detail.LawInfo.LawNum,
                LawTitle = detail.RevisionInfo.LawTitle
            };

            var law = FindChild(detail.LawFullText, "Law");
            var body = FindChild(law, "LawBody");
            var main = FindChild(body, "MainProvision");

            // ★ Chapter をトップに
            foreach (var chapterNode in FindChildren(main, "Chapter")) {
                var chapter = new ChapterView {
                    ChapterNum = GetAttr(chapterNode, "Num"),
                    ChapterTitle = GetInnerText(FindChild(chapterNode, "ChapterTitle"))
                };

                // Chapter 配下の Article
                foreach (var artNode in FindChildren(chapterNode, "Article")) {
                    var article = BuildArticle(artNode);
                    if (article != null)
                        chapter.Articles.Add(article);
                }

                view.Chapters.Add(chapter);
            }

            return view;
        }


        private static ArticleView? BuildArticle(LawFullTextNodeVO articleNode) {
            var articleNum = GetAttr(articleNode, "Num");
            if (string.IsNullOrEmpty(articleNum))
                return null;

            // ★ ArticleTitle を取得（子ノード）
            var titleNode = FindChild(articleNode, "ArticleTitle");
            var rawTitle = GetInnerText(titleNode);   // "第一条" など

            var article = new ArticleView {
                ArticleNum = articleNum,
                ArticleTitle = rawTitle,   // ★ ここにセット
                NormalizedKey = $"art:{articleNum}"
            };

            // Paragraph の処理はそのまま
            foreach (var paraNode in FindChildren(articleNode, "Paragraph")) {
                var para = BuildParagraph(paraNode, articleNum);
                if (para != null)
                    article.Paragraphs.Add(para);
            }

            return article;
        }


        private static ParagraphView? BuildParagraph(LawFullTextNodeVO paraNode, string articleNum) {
            var paraNum = GetAttr(paraNode, "Num");

            // ★ ParagraphTitle を取得（あれば）
            var titleNode = FindChild(paraNode, "ParagraphTitle");
            var paraTitle = GetInnerText(titleNode);

            // ★ ParagraphTitle が無い場合 → 最初の Sentence から自動抽出（精度強化版）
            if (string.IsNullOrWhiteSpace(paraTitle)) {
                var firstSentenceNode =
                    FindChildrenTag(paraNode, "ParagraphSentence")
                        .SelectMany(ps => FindChildrenTag(ps, "Sentence"))
                        .FirstOrDefault();

                var text = GetInnerText(firstSentenceNode)?.Trim() ?? "";

                if (!string.IsNullOrEmpty(text)) {
                    // ① 「この法律で「◯◯」とは」 → ◯◯の定義
                    var m = Regex.Match(text, "この法律で「(.+?)」とは");
                    if (m.Success) {
                        paraTitle = $"{m.Groups[1].Value}の定義";
                    }
                    // ② 拒否事由
                    else if (text.Contains("次の各号のいずれかに該当する者")) {
                        paraTitle = "拒否事由";
                    }
                    // ③ 届出義務
                    else if (text.Contains("届け出なければならない")) {
                        paraTitle = "届出義務";
                    }
                    // ④ 免許の付与
                    else if (text.Contains("免許を与えるとき")) {
                        paraTitle = "免許の付与";
                    }
                    // ⑤ 免許証の交付
                    else if (text.Contains("免許証を交付")) {
                        paraTitle = "免許証の交付";
                    }
                    // ⑥ fallback：句点まで
                    else {
                        var cutoff = text.IndexOf('。');
                        if (cutoff > 0)
                            paraTitle = text.Substring(0, cutoff);
                        else
                            paraTitle = text.Length > 20 ? text.Substring(0, 20) + "…" : text;
                    }
                }
            }


            var paragraph = new ParagraphView {
                ParagraphNum = paraNum == "1" ? "" : paraNum,
                ParagraphTitle = paraTitle,   // ★ 自動抽出済みタイトルをセット
                NormalizedKey = $"art:{articleNum}:para:{paraNum}"
            };

            // ① 通常の ParagraphSentence → Sentence
            foreach (var paraSentence in FindChildrenTag(paraNode, "ParagraphSentence")) {
                foreach (var sentenceNode in FindChildrenTag(paraSentence, "Sentence")) {
                    var s = BuildSentence(sentenceNode, articleNum, paraNum, null);
                    if (s != null)
                        paragraph.Sentences.Add(s);
                }
            }

            // ② 各号（Item）も SentenceView に落とし込む
            foreach (var itemNode in FindChildrenTag(paraNode, "Item")) {
                var itemTitleNode = FindChildTag(itemNode, "ItemTitle");
                var itemLabel = itemTitleNode != null ? GetInnerText(itemTitleNode) : "";

                foreach (var itemSentence in FindChildrenTag(itemNode, "ItemSentence")) {
                    foreach (var sentenceNode in FindChildrenTag(itemSentence, "Sentence")) {
                        var s = BuildSentence(sentenceNode, articleNum, paraNum, itemLabel);
                        if (s != null)
                            paragraph.Sentences.Add(s);
                    }
                }
            }

            if (paragraph.Sentences.Count == 0)
                return null;

            return paragraph;
        }


        private static SentenceView? BuildSentence(
            LawFullTextNodeVO sentenceNode,
            string articleNum,
            string paraNum,
            string? itemLabel) {
            var num = GetAttr(sentenceNode, "Num"); // "1" など
            var text = GetInnerText(sentenceNode);

            if (string.IsNullOrEmpty(text))
                return null;

            var sentence = new SentenceView {
                SentenceNum = itemLabel ?? num, // 号がある場合は「一」「二」などを優先
                Text = text,
                NormalizedKey = itemLabel == null
                    ? $"art:{articleNum}:para:{paraNum}:sent:{num}"
                    : $"art:{articleNum}:para:{paraNum}:item:{itemLabel}"
            };

            return sentence;
        }

        // ====== 汎用ヘルパ ======

        private static LawFullTextNodeVO? FindChild(LawFullTextNodeVO? node, string tag) {
            if (node == null)
                return null;

            return node.Children
                       .OfType<LawFullTextNodeVO>()
                       .FirstOrDefault(c => c.Tag == tag);
        }

        // 子ノードから一致するものをすべて返す
        private static IEnumerable<LawFullTextNodeVO> FindChildren(LawFullTextNodeVO? node, string tag) {
            if (node == null)
                return Enumerable.Empty<LawFullTextNodeVO>();

            return node.Children
                       .OfType<LawFullTextNodeVO>()
                       .Where(c => c.Tag == tag);
        }


        // 属性取得（なければ空文字）
        private static string GetAttr(LawFullTextNodeVO node, string name) {
            return node.Attr.TryGetValue(name, out var v) ? v : "";
        }


        private static LawFullTextNodeVO? FindChildTag(LawFullTextNodeVO node, string tag)
            => node.Children
                   .OfType<LawFullTextNodeVO>()
                   .FirstOrDefault(c => c.Tag == tag);

        private static IEnumerable<LawFullTextNodeVO> FindChildrenTag(LawFullTextNodeVO node, string tag)
            => node.Children
                   .OfType<LawFullTextNodeVO>()
                   .Where(c => c.Tag == tag);

        // ノード内のテキストを再帰的に全部つなげる
        private static string GetInnerText(LawFullTextNodeVO? node) {
            if (node == null)
                return "";

            var sb = new System.Text.StringBuilder();
            CollectText(node, sb);
            return sb.ToString().Trim();
        }


        // 再帰的に文字列を集める
        private static void CollectText(LawFullTextNodeVO node, System.Text.StringBuilder sb) {
            foreach (var child in node.Children) {
                switch (child) {
                    case string s:
                        sb.Append(s);
                        break;

                    case LawFullTextNodeVO c:
                        CollectText(c, sb);
                        break;
                }
            }
        }

    }


}