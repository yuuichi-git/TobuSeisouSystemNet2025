/*
 * 2024-12-11
 */
namespace ControlEx {
    public partial class MaskedTextBoxEx : MaskedTextBox {
        public MaskedTextBoxEx() {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e) {
            /*
             * 全選択
             */
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Enter:
                    SendKeys.Send("{TAB}");
                    break;
                default:
                    base.OnKeyDown(e);
                    break;
            }
        }
    }
}
