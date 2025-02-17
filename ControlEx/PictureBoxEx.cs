/*
 * 2024-11-05
 */
namespace ControlEx {
    public partial class PictureBoxEx : PictureBox {
        public PictureBoxEx() {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear() {
            this.Image = null;
        }
    }
}
