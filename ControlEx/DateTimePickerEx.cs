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
            this.KeyDown += DateTimePickerEx_KeyDown;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePickerEx_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyData) {
                /*
                 * クリア表示
                 */
                case Keys.Escape:
                    this.CustomFormat = " ";
                    this.Refresh();
                    break;
                /*
                 * 西暦で表示
                 */
                case Keys.Control | Keys.A:
                    CultureFlag = false;
                    this.CustomFormat = this.Value.ToString(" yyyy年MM月dd日(dddd)");
                    this.Refresh();
                    break;
                /*
                 * 和暦で表示
                 */
                case Keys.Control | Keys.J:
                    CultureFlag = true;
                    this.CustomFormat = this.Value.ToString(" ggyy年MM月dd日(dddd)", _cultureInfo);
                    this.Refresh();
                    break;
            }
        }

        /// <summary>
        /// CultureFlag
        /// true:和暦 false:西暦
        /// </summary>
        public bool CultureFlag {
            get => this._cultureFlag;
            set => this._cultureFlag = value;
        }
    }
}
