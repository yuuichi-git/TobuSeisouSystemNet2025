/*
 * 2026-03-08
 * e-Gob API から取得した Article オブジェクトから、すべての Sentence を列挙する拡張メソッド
 */
using Vo;

namespace Extensions {
    public static class ArticleExtensions {
        public static IEnumerable<Sentence> GetAllSentences(this Article article) {
            // 条の直下の文
            foreach (var s in article.Sentences)
                yield return s;

            // 項
            foreach (var p in article.Paragraphs) {
                foreach (var s in p.Sentences)
                    yield return s;

                // 号
                foreach (var i in p.Items) {
                    foreach (var s in i.Sentences)
                        yield return s;

                    // イ・ロ
                    foreach (var s1 in i.Subitem1s) {
                        foreach (var s in s1.Sentences)
                            yield return s;

                        // ハ・ニ
                        foreach (var s2 in s1.Subitem2s) {
                            foreach (var s in s2.Sentences)
                                yield return s;
                        }
                    }
                }
            }
        }
    }

    public static class LawBodyExtensions {
        public static IEnumerable<Article> ArticlesRecursive(this LawBody body) {
            // 章あり
            foreach (var chapter in body.Chapters) {
                foreach (var section in chapter.Sections) {
                    foreach (var subsection in section.Subsections) {
                        foreach (var article in subsection.Articles)
                            yield return article;
                    }

                    foreach (var article in section.Articles)
                        yield return article;
                }

                foreach (var article in chapter.Articles)
                    yield return article;
            }

            // 章なし → 節あり
            foreach (var section in body.Sections) {
                foreach (var subsection in section.Subsections) {
                    foreach (var article in subsection.Articles)
                        yield return article;
                }

                foreach (var article in section.Articles)
                    yield return article;
            }

            // 章も節も無い → 条のみ
            foreach (var article in body.Articles)
                yield return article;
        }
    }
}
