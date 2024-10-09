/*
 * 2024-09-23
 */
namespace ControlEx {
    public partial class LabelEx : Label {
        /// <summary>
        /// コンストラクター
        /// </summary>
        public LabelEx() {
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
        }

        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }
    }
}
