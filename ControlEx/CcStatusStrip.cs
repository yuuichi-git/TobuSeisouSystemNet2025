/*
 * 2024-09-23
 */
namespace ControlEx {
    public partial class CcStatusStrip : StatusStrip {
        /*
         * プロパティ
         */
        private ToolStripStatusLabel toolStripStatusLabel = new("Status：");
        private ToolStripStatusLabel toolStripStatusLabelDetail = new("ToolStripStatusLabelDetail");

        /// <summary>
        /// コンストラクター
        /// </summary>
        public CcStatusStrip() {
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
        private void CreateItem() {
            /*
             * 
             */
            toolStripStatusLabel.Name = "ToolStripStatusLabel";
            this.Items.Add(toolStripStatusLabel);
            /*
             * 
             */
            toolStripStatusLabelDetail.Name = "ToolStripStatusLabelDetail";
            this.Items.Add(toolStripStatusLabelDetail);
        }

        /*
         * 
         */
        /// <summary>
        /// 
        /// </summary>
        public ToolStripStatusLabel ToolStripStatusLabel {
            get => this.toolStripStatusLabel;
            set => this.toolStripStatusLabel = value;
        }
        /// <summary>
        /// 
        /// </summary>
        public ToolStripStatusLabel ToolStripStatusLabelDetail {
            get => this.toolStripStatusLabelDetail;
            set => this.toolStripStatusLabelDetail = value;
        }
    }
}
