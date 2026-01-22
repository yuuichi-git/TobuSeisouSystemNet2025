/*
 * 2026-01-22
 * 時刻入力用 MaskedTextBox カスタムコントロール CcTime
 */
namespace ControlEx {
    public partial class CcTime : MaskedTextBox {
        public CcTime() {
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.ImeMode = ImeMode.Off;                                                                     // IME をオフに設定
            this.Mask = "00:00";                                                                            // マスクを "00:00" に設定
            this.PromptChar = '_';                                                                          // プロンプト文字をアンダースコアに設定
            this.TextAlign = HorizontalAlignment.Right;                                                     // 右寄せ
            this.RejectInputOnFirstFailure = true;                                                          // 入力不正時にそれ以降の入力を拒否
            this.ValidatingType = typeof(DateTime);                                                         // DateTime 型での検証を行う
            /*
             * イベントハンドラを追加
             */
            this.TypeValidationCompleted += CcTime_TypeValidationCompleted;
        }

        /// <summary>
        /// TypeValidationCompleted イベントハンドラ
        /// </summary>
        private void CcTime_TypeValidationCompleted(object sender, TypeValidationEventArgs e) {
            /*
             * this.ValidatingType = typeof(DateTime)に対しての検証結果が e に格納される
             */
            if (!e.IsValidInput) {                                                                          // IsValidInput(true/false)
                MessageBox.Show("正しい時間を入力してください（例：23:59）", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
            /*
             * DateTime に変換できた場合
             */
            DateTime dt = (DateTime)e.ReturnValue;
            if (dt.Hour > 23 || dt.Minute > 59) {                                                           // 追加の独自チェック（例：24時間制の範囲チェック）
                MessageBox.Show("時間の範囲が正しくありません", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Enter:
                    SendKeys.Send("{TAB}");
                    break;
                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e) {
            /*
             * 全選択
             */
            SendKeys.Send("{HOME}");
            SendKeys.Send("+{END}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs paintEventArgs) {
            base.OnPaint(paintEventArgs);
        }
    }
}
