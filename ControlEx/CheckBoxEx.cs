/*
 * 2024-11-04
 */
namespace ControlEx {
    public partial class CheckBoxEx : CheckBox {
        public CheckBoxEx() {
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
    }
}
