/*
 * 2024-09-24
 */
namespace ControlEx {
    public partial class ButtonEx : Button {
        /*
         * Fontの定義
         */
        private readonly Font _drawFontStaffLabel = new("メイリオ", 14, FontStyle.Regular, GraphicsUnit.Pixel);
        /*
         * 
         * プロパティ
         * 
         */
        private string _textDirectionVertical = string.Empty;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            /*
             * 文字(氏名)を描画
             */
            StringFormat stringFormat = new();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            stringFormat.LineAlignment = StringAlignment.Center;
            pe.Graphics.DrawString(_textDirectionVertical, _drawFontStaffLabel, Brushes.Black, new Rectangle(0, 0, this.Width, this.Height), stringFormat);
        }

        /// <summary>
        /// TextDirectionVertical
        /// 縦書きのテキスト
        /// </summary>
        public string SetTextDirectionVertical {
            get => _textDirectionVertical;
            set => _textDirectionVertical = value;
        }
    }
}
