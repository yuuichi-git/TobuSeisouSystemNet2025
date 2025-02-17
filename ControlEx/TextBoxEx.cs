/*
 * 2024-11-05
 */
namespace ControlEx {
    public partial class TextBoxEx : TextBox {
        public TextBoxEx() {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        public void ClearEmpty() {
            this.Text = string.Empty;
        }
    }
}
