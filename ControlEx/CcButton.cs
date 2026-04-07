/*
 * 2024-09-24
 */
namespace CcControl {
    public partial class CcButton : Button {
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
        public CcButton() {
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

            // 初期 ForeColor を明示（必要なら変更）
            this.ForeColor = SystemColors.ControlText;

            // Enabled の初期状態に合わせて色を設定
            UpdateForeColor();
        }

        /// <summary>
        /// Enabled の変化を検知して ForeColor を切り替える
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnabledChanged(EventArgs e) {
            base.OnEnabledChanged(e);
            UpdateForeColor();
            this.Invalidate(); // 再描画
        }

        /// <summary>
        /// ForeColor を Enabled に応じて更新する
        /// </summary>
        private void UpdateForeColor() {
            // 要求は Gray にすることなので Color.Gray を使用
            this.ForeColor = this.Enabled ? SystemColors.ControlText : Color.Gray;
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
            using (var stringFormat = new StringFormat()) {
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                stringFormat.LineAlignment = StringAlignment.Center;

                // ForeColor を使って描画する（Brush を破棄する）
                using (var brush = new SolidBrush(this.ForeColor)) {
                    pe.Graphics.DrawString(_textDirectionVertical, _drawFontStaffLabel, brush, new Rectangle(0, 0, this.Width, this.Height), stringFormat);
                }
            }
        }

        /// <summary>
        /// TextDirectionVertical
        /// 縦書きのテキスト
        /// </summary>
        public string SetTextDirectionVertical {
            get => _textDirectionVertical;
            set {
                _textDirectionVertical = value;
                this.Invalidate(); // テキストが変わったら再描画
            }
        }
    }
}
