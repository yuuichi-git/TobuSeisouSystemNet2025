/*
 * 2025-1-11
 */
namespace CcControl {
    public partial class CcFlowLayoutPanel : FlowLayoutPanel {
        /*
         * properties
         */
        private string _displayText = string.Empty;

        /*
         * constructor
         */
        public CcFlowLayoutPanel() {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            using (Font font = new Font("Meiryo", 12, FontStyle.Bold))
            using (Brush brush = new SolidBrush(Color.Black)) {
                StringFormat sf = new StringFormat {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                e.Graphics.DrawString(DisplayText, font, brush,
                                      this.ClientRectangle, sf);
            }
        }

        /*
         * 
         * properties
         * 
         */
        /// <summary>
        /// Control表示するテキストを取得または設定します。
        /// </summary>
        public string DisplayText { get => this._displayText; set => this._displayText = value; }
    }
}
