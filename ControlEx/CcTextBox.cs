/*
 * 2024-11-05
 */
namespace ControlEx {
    public partial class CcTextBox : TextBox {
        public CcTextBox() {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        public void SetEmpty() {
            this.Text = string.Empty;
        }
    }
}
