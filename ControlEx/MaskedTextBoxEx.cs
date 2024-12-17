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

        protected override void OnGotFocus(EventArgs e) {
            /*
             * 全選択
             */
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");

        }
    }
}
