/*
 * 2024-11-04
 */
namespace CcControl {
    public partial class CcCheckBox : CheckBox {
        public CcCheckBox() {
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
                default:
                    base.OnKeyDown(e);
                    break;
            }
        }
    }
}
