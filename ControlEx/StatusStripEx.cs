/*
 * 2024-09-23
 */
namespace ControlEx {
    public partial class StatusStripEx : StatusStrip {
        /// <summary>
        /// コンストラクター
        /// </summary>
        public StatusStripEx() {
            /* 
             * ダブルバッファリングを有効にする
             */
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            /*
             * Initialize
             */
            InitializeComponent();
            this.Dock = DockStyle.Bottom;
            this.CreateItem();
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        private void CreateItem() {
            /*
             * 
             */
            ToolStripStatusLabel toolStripStatusLabel = new("Status：");
            toolStripStatusLabel.Name = "ToolStripStatusLabel";
            this.Items.Add(toolStripStatusLabel);
            /*
             * 
             */
            ToolStripStatusLabel toolStripStatusLabelDetail = new("ToolStripStatusLabelDetail");
            toolStripStatusLabelDetail.Name = "ToolStripStatusLabelDetail";
            this.Items.Add(toolStripStatusLabelDetail);
        }
    }
}
