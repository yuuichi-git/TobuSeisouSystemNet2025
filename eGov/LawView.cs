/*
 * 2026-03-05
 */
using System.Diagnostics;

using ControlEx;

using Vo;

namespace EGov {
    public partial class LawView : Form {
        /*
         * Dictionary
         */
        private readonly Dictionary<string, string> _dictionaryLawTypeMap = new(){{ "Constitution",        "憲法" },
                                                                                  { "Act",                 "法律" },
                                                                                  { "CabinetOrder",        "政令" },
                                                                                  { "ImperialOrder",       "勅令" },
                                                                                  { "MinisterialOrdinance","府省令" },
                                                                                  { "Rule",                "規則" },
                                                                                  { "Misc",                "その他" }};
        /*
         * インスタンス
         */
        private EGovApi eGobApi = new();

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="lawTitle"></param>
        /// <param name="article"></param>
        /// <param name="paragraph"></param>
        public LawView(string lawTitle, string? article = null, string? paragraph = null) {
            /*
             * Initialize
             */
            InitializeComponent();
            // 非同期で初期化
            _ = InitializeAsync(lawTitle, article, paragraph);
        }

        private async Task InitializeAsync(string lawTitle, string? article = null, string? paragraph = null) {
            LawItemVO? lawItemVO = null;
            LawDetailVO? lawDetailVO = null;

            /*
             * ① keyword検索
             */
            try {
                var lawSearchResultVO = await eGobApi.GetLawByKeywordAsync(lawTitle);

                if (lawSearchResultVO == null || lawSearchResultVO.Items.Count == 0)
                    throw new Exception("検索結果が空です。");

                // 完全一致優先（将来の仕様変更に備えて複数件対応）
                lawItemVO = lawSearchResultVO.Items
                    .FirstOrDefault(x => x.RevisionInfo.LawTitle == lawTitle)
                    ?? lawSearchResultVO.Items[0];
            } catch (Exception ex) {
                MessageBox.Show(
                    $"法令情報の取得に失敗しました: {ex.Message}",
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            /*
             * ② law_data検索
             */
            try {
                lawDetailVO = await eGobApi.GetLawDetailAsync(lawItemVO.LawInfo.LawId);

                if (lawDetailVO == null)
                    throw new Exception("法令本文データが取得できませんでした。");
            } catch (Exception ex) {
                MessageBox.Show(
                    $"法令本文の取得に失敗しました: {ex.Message}",
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            /*
             * ③ UI 更新（lawDetailVO が取得できてから）
             */
            this.CcLabelLawType.Text = "種別：" + _dictionaryLawTypeMap[lawItemVO.LawInfo.LawType];
            this.CcLabelLawTitle.Text = "法令名：" + lawItemVO.RevisionInfo.LawTitle;
            this.CcLabelLawArticle.Text = "条№：" + article;

            Debug.WriteLine("【法令情報】");
            Debug.WriteLine($"法令ID: {lawItemVO.LawInfo.LawId}");
            Debug.WriteLine($"法令番号: {lawItemVO.LawInfo.LawNum}");
            Debug.WriteLine($"法令名: {lawItemVO.RevisionInfo.LawTitle}");
            Debug.WriteLine($"カテゴリ: {lawItemVO.RevisionInfo.Category}");
            Debug.WriteLine($"改正日: {lawItemVO.RevisionInfo.Updated}");

            /* 
             * ④ lawDetailVO → TreeView 展開用の正規化処理へ
             */
            // LawFullTextNodeVO → LawViewVo へ正規化
            lawDetailVO.LawViewVo = LawViewBuilder.Build(lawDetailVO);

            // TreeView に展開
            this.BuildTreeFromView(lawDetailVO.LawViewVo);

            // TreeView に展開（閉じた状態）
            this.BuildTreeFromView(lawDetailVO.LawViewVo);

            // ★ 指定された条・項だけ開く
            OpenSpecifiedNode(article, paragraph);

        }

        private void BuildTreeFromView(LawViewVo view) {
            CcTreeView1.Nodes.Clear();

            foreach (var chapter in view.Chapters) {
                var chapterNode = new TreeNode(chapter.ChapterTitle) {
                    Tag = chapter
                };

                foreach (var article in chapter.Articles) {
                    var artNode = new TreeNode(article.DisplayText) {
                        Tag = article
                    };

                    foreach (var para in article.Paragraphs) {
                        var paraNode = new TreeNode(para.DisplayText) {
                            Tag = para
                        };

                        foreach (var sent in para.Sentences) {
                            var sentNode = new TreeNode(sent.DisplayText) {
                                Tag = sent
                            };

                            // ★ 絶対に Expand しない
                            paraNode.Nodes.Add(sentNode);
                        }

                        // ★ 絶対に Expand しない
                        artNode.Nodes.Add(paraNode);
                    }

                    // ★ 絶対に Expand しない
                    chapterNode.Nodes.Add(artNode);
                }

                // ★ 絶対に Expand しない
                CcTreeView1.Nodes.Add(chapterNode);
            }

            // ★ ここに ExpandAll() を絶対に書かない
        }


        private void OpenSpecifiedNode(string? article, string? paragraph) {
            if (string.IsNullOrWhiteSpace(article))
                return;

            foreach (TreeNode chapterNode in CcTreeView1.Nodes) {
                foreach (TreeNode articleNode in chapterNode.Nodes) {
                    if (articleNode.Tag is ArticleView av &&
                        av.ArticleNum == article) {
                        chapterNode.Expand();
                        articleNode.Expand();

                        if (!string.IsNullOrWhiteSpace(paragraph)) {
                            foreach (TreeNode paraNode in articleNode.Nodes) {
                                if (paraNode.Tag is ParagraphView pv &&
                                    (pv.ParagraphNum == paragraph ||
                                     (pv.ParagraphNum == "" && paragraph == "1"))) {
                                    paraNode.Expand();
                                    CcTreeView1.SelectedNode = paraNode;
                                    paraNode.EnsureVisible();
                                    return;
                                }
                            }
                        }

                        CcTreeView1.SelectedNode = articleNode;
                        articleNode.EnsureVisible();
                        return;
                    }
                }
            }
        }

    }
}