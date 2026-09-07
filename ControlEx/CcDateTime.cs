/*
 * 2024-10-09
 */
using System.ComponentModel;
using System.Globalization;

namespace CcControl {
    public partial class CcDateTime : DateTimePicker {
        /// <summary>
        /// デフォルトの日付
        /// </summary>
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        /// <summary>
        /// 日付形式情報
        /// </summary>
        private readonly CultureInfo _cultureInfo = new("ja-JP");
        /// <summary>
        /// CultureFlag
        /// true:和暦 false:西暦
        /// </summary>
        private bool _cultureFlag = true;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public CcDateTime() {
            /*
             * InitializeControl
             */
            InitializeComponent();
            /*
             * 和暦設定
             */
            _cultureInfo.DateTimeFormat.Calendar = new JapaneseCalendar();
            this.Format = DateTimePickerFormat.Custom;
            this.CustomFormat = _defaultDateTime.ToString(" ggyy年MM月dd日(dddd)", _cultureInfo);
            this.Value = _defaultDateTime;
            this.Refresh();
        }

        /// <summary>
        /// キーが押されたときの処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e) {
            /*
             * EnterキーでTABキーを送信するbase.KeyDown(e);
             */
            if(e.KeyCode == Keys.Enter) {
                SendKeys.Send("{TAB}");
            }
            /*
             * Escapeキーでデフォルト日付に戻す
             */
            if(e.KeyCode == Keys.Escape) {
                this.Value = _defaultDateTime;
                this.CustomFormat = " ";
                this.Refresh();
            }
            /*
             * Ctrl+Aで西暦表示に切り替える
             */
            if(e.KeyCode == Keys.A && ModifierKeys == Keys.Control) {
                this.CultureFlag = false;
                this.CustomFormat = this.Value.ToString(" yyyy年MM月dd日(dddd)");
                this.Value = DateTime.Now.Date;
                this.Refresh();
            }
            /*
             * Ctrl+Jで和暦表示に切り替える
             */
            if(e.KeyCode == Keys.J && ModifierKeys == Keys.Control) {
                this.CultureFlag = true;
                this.CustomFormat = this.Value.ToString(" ggyy年MM月dd日(dddd)", _cultureInfo);
                this.Value = DateTime.Now.Date;
                this.Refresh();
            }
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        /// <summary>
        /// 値が変更されたときの処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnValueChanged(EventArgs e) {
            switch(_cultureFlag) {
                case true:
                    this.CustomFormat = this.Value.ToString(" ggyy年MM月dd日(dddd)", _cultureInfo);
                    break;
                case false:
                    this.CustomFormat = this.Value.ToString(" yyyy年MM月dd日(dddd)");
                    break;
            }
            this.Refresh();
        }

        /// <summary>
        /// 日付を取得
        /// </summary>
        /// <returns>日付＋0:00:00時</returns>
        public DateTime GetDate() {
            return this.Value.Date;
        }

        /// <summary>
        /// Valueを取得
        /// </summary>
        /// <returns></returns>
        public DateTime GetValue() {
            return this.Value;
        }

        /// <summary>
        /// GetValueJp
        /// </summary>
        /// <returns>和暦を返す</returns>
        public string GetValueJp() {
            return this.Value.ToString(" ggy年M月d日(dddd)", _cultureInfo);
        }

        /// <summary>
        /// クリアする
        /// </summary>
        public void SetClear() {
            this.CustomFormat = " ";
            this.Refresh();
        }

        /// <summary>
        /// 空にする
        /// </summary>
        public void SetEmpty() {
            this.Value = _defaultDateTime;
            this.CustomFormat = " ";
            this.Refresh();
        }

        /// <summary>
        /// 今日の日付をセット
        /// </summary>
        public void SetToday() {
            this.Value = DateTime.Today;
            this.Refresh();
        }

        /// <summary>
        /// 西暦で表示
        /// </summary>
        /// <param name="dateTime"></param>
        public void SetValue(DateTime dateTime) {
            if(dateTime.Date != _defaultDateTime.Date) {
                this.CustomFormat = this.Value.ToString(" yyyy年MM月dd日(dddd)");
                this.Value = dateTime;
                this.Refresh();
            } else {
                this.CustomFormat = " ";
                this.Value = _defaultDateTime;
                this.Refresh();
            }
        }

        /// <summary>
        /// 和暦で設定
        /// 1900-01-01の場合はブランクを表示
        /// </summary>
        /// <param name="dateTime"></param>
        public void SetValueJp(DateTime dateTime) {
            if(dateTime.Date != _defaultDateTime.Date) {
                this.CustomFormat = dateTime.ToString(" ggyy年MM月dd日(dddd)", _cultureInfo);
                this.Value = dateTime;
                this.Refresh();
            } else {
                this.CustomFormat = " ";
                this.Value = _defaultDateTime;
                this.Refresh();
            }
        }

        /// <summary>
        /// Emptyかどうか
        /// </summary>
        /// <returns></returns>
        public bool TestEmpty() {
            if(this.CustomFormat == " " && this.Value == _defaultDateTime) {
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// CultureFlag
        /// true:和暦 false:西暦
        /// </summary>
        [Category("RisSoft")]
        [Browsable(false)]
        [Description("和暦(true)／西暦(false) を切り替えます")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CultureFlag {
            get => this._cultureFlag;
            set => this._cultureFlag = value;
        }
    }
}
