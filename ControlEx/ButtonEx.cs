/*
 * 2024-09-24
 */
namespace ControlEx {
    public partial class ButtonEx : Button {
        /// <summary>
        /// コンストラクター
        /// </summary>
        public ButtonEx() {
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
