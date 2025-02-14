/*
 * 2024-11-06
 */
namespace ControlEx {
    public partial class NumericUpDownEx : NumericUpDown {
        /// <summary>
        /// コンストラクター
        /// </summary>
        public NumericUpDownEx() {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pe"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
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
