/*
 * 2024-11-05
 */
namespace ControlEx {
    public partial class CcPictureBox : PictureBox {
        public CcPictureBox() {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetEmpty() {
            this.Image = null;
        }
    }
}
