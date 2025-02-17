/*
 * 2024-10-01
 */
namespace ControlEx {
    public partial class ComboBoxEx : ComboBox {
        /// <summary>
        /// コンストラクター
        /// </summary>
        public ComboBoxEx() {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Enter:
                    SendKeys.Send("{TAB}");
                    break;
            }
        }

        /// <summary>
        /// クリア
        /// </summary>
        public void Clear() {
            this.SelectedIndex = -1;
            this.Text = string.Empty;
        }
    }
}
