/*
 * 2024-09-23
 */
namespace ControlEx {
    public partial class CcTabControl : TabControl {
        /// <summary>
        /// コンストラクター
        /// </summary>
        public CcTabControl() {
            /*
             * Initialize
             */
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        /// <summary>
        /// 指定したタブをアクティブにする
        /// WinForms の TabPage.Visible は存在するけれど、TabControl はその値を UI 表示に反映しないという仕様になっている。だから、Visible=false にしても タブヘッダーは消えない。
        /// Visible を使うのではなく、TabPages コレクションから外す必要がある。
        /// </summary>
        /// <param name="tabName"></param>
        public void SetActiveTabPages(string tabName) {
            if (string.IsNullOrWhiteSpace(tabName))
                return;

            // 一旦すべてのタブを退避
            var allPages = this.TabPages.Cast<TabPage>().ToList();

            // TabControl から全削除
            this.TabPages.Clear();

            // 指定されたタブだけ再追加
            var target = allPages.FirstOrDefault(p => p.Name == tabName);
            if (target != null) {
                this.TabPages.Add(target);
            }

            // レイアウト更新＋再描画
            this.PerformLayout();
            this.Refresh();
        }

    }
}
