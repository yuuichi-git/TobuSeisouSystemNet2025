/*
 * 2024-10-09
 */
using System.Globalization;

namespace ControlEx {
    public partial class DateTimePickerEx : DateTimePicker {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly CultureInfo _cultureInfo = new("ja-JP");
        private bool _cultureFlag = false;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public DateTimePickerEx() {
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
            /*
             * Event
             */
            this.ValueChanged += DateTimePickerEx_ValueChanged;
        }

        /// <summary>
        /// OnPaint
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        /// <summary>
        /// DateTimePickerEx_ValueChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePickerEx_ValueChanged(object sender, EventArgs e) {
            switch (_cultureFlag) {
                case true:
                    this.CustomFormat = this.Value.ToString(" ggyy年MM月dd日(dddd)", _cultureInfo);
                    break;
                case false:
                    this.CustomFormat = this.Value.ToString(" yyyy年MM月dd日(dddd)");
                    break;
            }
            this.Refresh();
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            /*
             * 
             */
            if (e.KeyCode == Keys.Enter) {
                SendKeys.Send("{TAB}");
            }
            /*
             * 
             */
            if (e.KeyCode == Keys.Escape) {
                this.CustomFormat = " ";
                this.Refresh();
            }
            /*
             * 
             */
            if (e.KeyCode == Keys.A && ModifierKeys == Keys.Control) {
                CultureFlag = false;
                this.CustomFormat = this.Value.ToString(" yyyy年MM月dd日(dddd)");
                this.Value = DateTime.Now.Date;
                this.Refresh();
            }
            /*
             * 
             */
            if (e.KeyCode == Keys.J && ModifierKeys == Keys.Control) {
                CultureFlag = true;
                this.CustomFormat = this.Value.ToString(" ggyy年MM月dd日(dddd)", _cultureInfo);
                this.Value = DateTime.Now.Date;
                this.Refresh();
            }
        }

        /// <summary>
        /// 今日の日付をセット
        /// </summary>
        public void SetToday() {
            this.Value = DateTime.Today;
            this.Refresh();
        }

        /// <summary>
        /// クリアする
        /// </summary>
        public void SetClear() {
            this.CustomFormat = " ";
            this.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetEmpty() {
            this.CustomFormat = " ";
            this.Value = _defaultDateTime;
            this.Refresh();
        }

        /// <summary>
        /// 西暦で表示
        /// </summary>
        /// <param name="dateTime"></param>
        public void SetValue(DateTime dateTime) {
            this.Value = dateTime;
            this.Refresh();
        }

        /// <summary>
        /// 和暦で設定
        /// 1900-01-01の場合はブランクを表示
        /// </summary>
        /// <param name="dateTime"></param>
        public void SetValueJp(DateTime dateTime) {
            if (dateTime.Date != _defaultDateTime.Date) {
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
        /// CultureFlag
        /// true:和暦 false:西暦
        /// </summary>
        public bool CultureFlag {
            get => this._cultureFlag;
            set => this._cultureFlag = value;
        }

        /// <summary>
        /// GetValueJp
        /// </summary>
        /// <returns>和暦を返す</returns>
        public string GetValueJp() {
            return this.Value.ToString(" ggy年M月d日(dddd)", _cultureInfo);
        }
    }
}
