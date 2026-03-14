/*
 * 2026-03-05
 */
using System.Text;

using ControlEx;

using Vo;

namespace EGov {
    public partial class LawView : Form {
        private EGobApi _egobApi = new();

        private readonly string _lawName;
        private readonly string _lawArticle;
        private readonly string _lawParagraph;

        private readonly Dictionary<string, string> _dictionaryLawTypeMap = new(){
            { "Constitution",        "憲法" },
            { "Act",                 "法律" },
            { "CabinetOrder",        "政令" },
            { "ImperialOrder",       "勅令" },
            { "MinisterialOrdinance","府省令" },
            { "Rule",                "規則" },
            { "Misc",                "その他" }};

        private void ApplyStatusToNodes(ILawNode node, LawStatus status) {
            // ★ 法令全体のステータスを適用
            switch (node) {
                case LawPart p:
                    p.Status = status;
                    break;
                case LawChapter c:
                    c.Status = status;
                    break;
                case LawSection s:
                    s.Status = status;
                    break;
                case LawSubsection ss:
                    ss.Status = status;
                    break;
                case LawDivision d:
                    d.Status = status;
                    break;
                case LawArticle a:
                    // ★ 条文削除の個別判定
                    if (a.Paragraphs.Count == 1 &&
                        a.Paragraphs[0].Text.Trim() == "削除") {
                        a.Status = LawStatus.Repealed;
                    } else {
                        a.Status = status;
                    }
                    break;

                case LawParagraph pa:
                    pa.Status = status;
                    break;

                case LawItem i:
                    i.Status = status;
                    break;

                case LawSupplProvision sp:
                    sp.Status = status;
                    break;

                case LawSupplParagraph spp:
                    spp.Status = status;
                    break;
            }

            foreach (var child in node.Children)
                ApplyStatusToNodes(child, status);
        }

        public LawView(string lawName, string lawArticle, string lawParagraph) {
            _lawName = lawName;
            _lawArticle = lawArticle;
            _lawParagraph = lawParagraph;
            /*
             * 
             */
            InitializeComponent();
            this.CcTextBoxLawId.Text = string.Empty;
            this.CcTextBoxLawNum.Text = string.Empty;
            this.CcTextBoxLawType.Text = string.Empty;
            this.CcTextBoxLawTitle.Text = string.Empty;
            this.CcTextBoxLawArticle.Text = string.Empty;

            // フォームロード後に API を叩く
            this.Load += async (_, __) => await LoadLawAsync();
        }

        private async Task LoadLawAsync() {
            // ① KeyWord検索 API で KeywordItem を取得
            KeywordResponse keywordResponse = await _egobApi.GetLawInfoAsync(_lawName);

            if (keywordResponse == null) {
                MessageBox.Show("法令が見つかりませんでした。");
                return;
            }

            // ② law_id を取得
            string? lawId = keywordResponse.Items.FirstOrDefault()?.LawInfo.LawId;

            if (string.IsNullOrWhiteSpace(lawId)) {
                MessageBox.Show("法令IDが取得できませんでした。");
                return;
            }

            // ③ Controlに出力
            this.CcTextBoxLawId.Text = keywordResponse.Items.FirstOrDefault()?.LawInfo.LawId ?? "";
            this.CcTextBoxLawNum.Text = keywordResponse.Items.FirstOrDefault()?.LawInfo.LawNum ?? "";
            this.CcTextBoxLawType.Text = _dictionaryLawTypeMap[keywordResponse.Items.FirstOrDefault()?.RevisionInfo.LawType];
            this.CcTextBoxLawTitle.Text = keywordResponse.Items.FirstOrDefault()?.RevisionInfo.LawTitle ?? "";
            this.CcTextBoxLawArticle.Text = _lawArticle;

            // ④ 法令APIで法令の内容を取得
            LawDataResponse lawDataResponse = await _egobApi.GetLawDataAsync(lawId);

            if (lawDataResponse?.LawBody == null) {
                MessageBox.Show("法令本文が取得できませんでした。");
                return;
            }

            // ★ ⑤ 改正履歴を LawDataResponse に統合
            lawDataResponse.Revisions = keywordResponse.Items
                .Select(x => new LawRevisionInfo {
                    AmendmentLawId = x.RevisionInfo.AmendmentLawId,
                    AmendmentLawNum = x.RevisionInfo.AmendmentLawNum,
                    AmendmentLawTitle = x.RevisionInfo.AmendmentLawTitle,
                    AmendmentPromulgateDate = x.RevisionInfo.AmendmentPromulgateDate,
                    AmendmentEnforcementDate = x.RevisionInfo.AmendmentEnforcementDate,
                    AmendmentEnforcementComment = x.RevisionInfo.AmendmentEnforcementComment,
                    AmendmentType = x.RevisionInfo.AmendmentType
                })
                .Where(r => !string.IsNullOrWhiteSpace(r.AmendmentLawNum))
                .ToList();

            // ★ ⑥ 法令全体のステータスを判定
            var status = LawStatusEvaluator.Evaluate(lawDataResponse);

            // ★ ⑦ 全ノードにステータスを適用（LawBody は ILawNode ではないので注意）
            if (lawDataResponse.LawBody != null) {
                foreach (var part in lawDataResponse.LawBody.Parts)
                    ApplyStatusToNodes(part, status);

                foreach (var chapter in lawDataResponse.LawBody.Chapters)
                    ApplyStatusToNodes(chapter, status);

                foreach (var art in lawDataResponse.LawBody.Articles)
                    ApplyStatusToNodes(art, status);

                if (lawDataResponse.LawBody.SupplProvision != null)
                    ApplyStatusToNodes(lawDataResponse.LawBody.SupplProvision, status);
            }

            // ⑧ TreeView に全文構造を表示
            this.LoadLawTree(lawDataResponse.LawBody);

            // ⑨ 改正履歴ノードを TreeView に追加
            this.AddRevisionTree(lawDataResponse.Revisions);

            // 指定条文まで自動展開
            this.ExpandToTarget(CcTreeView1, _lawArticle, _lawParagraph);
        }

        public static class LawStatusEvaluator {
            public static LawStatus Evaluate(LawDataResponse law) {
                // ① 廃止判定（e-Gov の AmendmentType=9 が廃止）
                if (law.Revisions.Any(r => r.AmendmentType == 9))
                    return LawStatus.Repealed;

                // ② 未施行判定（施行日が未来）
                if (law.EnforcementDate.HasValue &&
                    law.EnforcementDate.Value > DateTime.Today)
                    return LawStatus.NotYetEnforced;

                // ③ 上記以外は有効
                return LawStatus.Active;
            }
        }

        private void AddRevisionTree(List<LawRevisionInfo> revisions) {
            if (revisions == null || revisions.Count == 0)
                return;

            var root = CcTreeView1.Nodes.Add("改正履歴");

            foreach (var rev in revisions) {
                string label =
                    $"{rev.AmendmentLawNum}  " +
                    $"（公布：{rev.AmendmentPromulgateDate?.ToString("yyyy/MM/dd") ?? "不明"}" +
                    $" / 施行：{rev.AmendmentEnforcementDate?.ToString("yyyy/MM/dd") ?? "不明"}）";

                var node = root.Nodes.Add(label);
                node.Tag = rev;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lawBody"></param>
        private void LoadLawTree(LawBody lawBody) {
            CcTreeView1.BeginUpdate();
            CcTreeView1.Nodes.Clear();

            // -----------------------------------------
            // ① 前文（Preface）
            // -----------------------------------------
            if (lawBody.Preface != null && !string.IsNullOrWhiteSpace(lawBody.Preface.Text)) {
                var prefaceNode = CcTreeView1.Nodes.Add("前文");
                prefaceNode.Nodes.Add(lawBody.Preface.Text);
            }

            // -----------------------------------------
            // ② 編（Part）
            // -----------------------------------------
            foreach (var part in lawBody.Parts)
                AddNode(CcTreeView1.Nodes, part);

            // -----------------------------------------
            // ③ Part が無い法令（民法など）
            // -----------------------------------------
            foreach (var chapter in lawBody.Chapters)
                AddNode(CcTreeView1.Nodes, chapter);

            // -----------------------------------------
            // ④ 章に属さない条（Article）
            // -----------------------------------------
            foreach (var art in lawBody.Articles)
                AddNode(CcTreeView1.Nodes, art);

            // -----------------------------------------
            // ⑤ 附則（SupplProvision）
            // -----------------------------------------
            if (lawBody.SupplProvision != null)
                AddNode(CcTreeView1.Nodes, lawBody.SupplProvision);

            CcTreeView1.EndUpdate();
        }

        private void AddNode(TreeNodeCollection parent, ILawNode node) {
            // ノード作成
            var tn = parent.Add(node.DisplayTitle);
            tn.Tag = node;

            // ★ ステータス色反映（統合）
            switch (node.Status) {
                case LawStatus.Active:
                    tn.ForeColor = Color.Black;
                    break;

                case LawStatus.NotYetEnforced:
                    tn.ForeColor = Color.Blue;
                    break;

                case LawStatus.Repealed:
                    tn.ForeColor = Color.Red;
                    break;
            }

            // 子ノードを再帰的に追加
            foreach (var child in node.Children)
                AddNode(tn.Nodes, child);
        }

        private void CcTreeView1_AfterSelect(object sender, TreeViewEventArgs e) {
            if (e.Node?.Tag is not ILawNode node) {
                CcRichTextBox1.Clear();
                return;
            }

            ShowNodeContent(node);
        }

        private void ShowNodeContent(ILawNode node) {
            StringBuilder sb = new();

            // 自分自身のタイトル
            sb.AppendLine(node.DisplayTitle);
            sb.AppendLine();

            // 子ノードを階層に応じて表示
            foreach (var child in node.Children) {
                sb.AppendLine($"{Indent(1)}{child.DisplayTitle}");

                // 条 → 見出し（ArticleCaption）
                if (child is LawArticle art && !string.IsNullOrWhiteSpace(art.ArticleCaption)) {
                    sb.AppendLine($"{Indent(2)}{art.ArticleCaption}");
                }

                // 項 → 本文
                if (child is LawParagraph para && !string.IsNullOrWhiteSpace(para.Text)) {
                    sb.AppendLine($"{Indent(2)}{para.Text}");
                }

                // 号 → 本文
                if (child is LawItem item && !string.IsNullOrWhiteSpace(item.Text)) {
                    sb.AppendLine($"{Indent(2)}{item.Text}");
                }

                sb.AppendLine();
            }

            CcRichTextBox1.Text = sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private string Indent(int level) {
            return new string(' ', level * 2); // 1階層 = 半角2スペース
        }

        /* 
         * TreeViewの展開
         *
         */
        public void ExpandToTarget(CcTreeView tree, string? lawArticle = null, string? lawParagraph = null) {
            if (string.IsNullOrWhiteSpace(lawArticle))
                return;

            var articleList = lawArticle
                .Split(',')
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

            string? paragraphNum = NormalizeParagraphNum(lawParagraph);

            TreeNode? firstMatchedNode = null;

            foreach (var articleNum in articleList) {
                foreach (TreeNode root in tree.Nodes) {
                    var matched = FindAndExpand(root, articleNum, paragraphNum, tree);

                    if (matched != null) {
                        if (firstMatchedNode == null)
                            firstMatchedNode = matched;

                        break;
                    }
                }
            }

            if (firstMatchedNode != null) {
                HighlightNode(tree, firstMatchedNode, paragraphNum);
                tree.SelectedNode = firstMatchedNode;
            }
        }

        private void HighlightNode(TreeView tree, TreeNode target, string? paragraphNum) {
            ResetNodeBackColor(tree.Nodes);

            // ★ 項指定なし → 条だけ黄色
            if (string.IsNullOrWhiteSpace(paragraphNum)) {
                target.BackColor = Color.Yellow;
                return;
            }

            // ★ 項指定あり → 条も黄色
            target.BackColor = Color.Yellow;

            string targetPara = NormalizeParagraphNum(paragraphNum);

            // ★ 項ノードを黄色にする
            foreach (TreeNode child in target.Nodes) {
                if (child.Tag is LawParagraph para) {
                    if (NormalizeParagraphNum(para.ParagraphNum) == targetPara) {
                        child.BackColor = Color.Yellow;
                        return;
                    }
                }
            }
        }

        private void ResetNodeBackColor(TreeNodeCollection nodes) {
            foreach (TreeNode node in nodes) {
                node.BackColor = Color.White; // デフォルト背景色
                ResetNodeBackColor(node.Nodes);
            }
        }

        private TreeNode? FindAndExpand(TreeNode node, string articleNum, string? paragraphNum, TreeView tree) {
            // ★ Article ノードか？
            if (node.Tag is LawArticle art) {
                if (art.ArticleNum == articleNum) {
                    node.Expand();

                    // ★ 項指定なし → 条ノードを返す
                    if (string.IsNullOrWhiteSpace(paragraphNum))
                        return node;

                    string targetPara = NormalizeParagraphNum(paragraphNum);

                    // ★ 項指定あり → 子ノードから探す
                    foreach (TreeNode child in node.Nodes) {
                        if (child.Tag is LawParagraph para) {
                            if (NormalizeParagraphNum(para.ParagraphNum) == targetPara) {
                                child.Expand();
                                return child; // ★ 項ノードを返す
                            }
                        }
                    }

                    // 項が見つからない → 条ノードを返す
                    return node;
                }
            }

            // ★ 子ノードを探索
            foreach (TreeNode child in node.Nodes) {
                var result = FindAndExpand(child, articleNum, paragraphNum, tree);
                if (result != null) {
                    node.Expand();
                    return result;
                }
            }

            return null;
        }

        private string NormalizeParagraphNum(string? raw) {
            if (string.IsNullOrWhiteSpace(raw))
                return "";

            string s = raw;

            // 全角 → 半角
            s = s.Normalize(NormalizationForm.FormKC);

            // 「項」を除去
            s = s.Replace("項", "");

            // 不可視文字除去
            s = s
                .Replace("\u200B", "")
                .Replace("\u200C", "")
                .Replace("\u200D", "")
                .Replace("\uFEFF", "");

            // 漢数字 → アラビア数字
            s = s
                .Replace("一", "1")
                .Replace("二", "2")
                .Replace("三", "3")
                .Replace("四", "4")
                .Replace("五", "5")
                .Replace("六", "6")
                .Replace("七", "7")
                .Replace("八", "8")
                .Replace("九", "9")
                .Replace("十", "10");

            return s.Trim();
        }

    }
}