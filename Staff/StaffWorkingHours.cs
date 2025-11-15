/*
 * 2025-2-1
 */
using Common;

using ControlEx;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace Staff {
    public partial class StaffWorkingHours : Form {
        /*
         * SPREADのColumnの番号
         */
        private const int colOperationDate = 1;
        private const int colSetName = 2;
        private const int colFirstRollCallTime = 3;
        private const int colBreakTime = 4;
        private const int colLastRollCallTime = 5;
        private const int colWorkingTime = 6;
        /*
         * インターネットから祝日のデータを取得
         */
        private Dictionary<DateTime, string> _dictionaryHoliday = new HolidayUtility().GetHoliday();
        /*
         * インスタンス作成
         */
        private readonly DateUtility _dateUtility = new();
        /*
         * Dao
         */
        private readonly StaffMasterDao _staffMasterDao;
        private readonly StaffWorkingHoursDao _staffWorkingHoursDao;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public StaffWorkingHours(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _staffMasterDao = new(connectionVo);
            _staffWorkingHoursDao = new(connectionVo);
            /*
             * InitializeControl
             */
            InitializeComponent();
            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemPrint",
                "ToolStripMenuItemPrintA4",
                "ToolStripMenuItemHelp"
            };
            MenuStripEx1.ChangeEnable(listString);
            /*
             * 配車日を設定
             */
            this.DateTimePickerExOperationDate1.SetValue(_dateUtility.GetBeginOfMonth(DateTime.Now));
            this.DateTimePickerExOperationDate2.SetValue(_dateUtility.GetEndOfMonth(DateTime.Now));
            this.InitializeComboBoxExStaffDisplayName();
            this.InitializeSheetView(this.SheetViewList);
            /*
             * Eventを登録する
             */
            MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEx_Click(object sender, EventArgs e) {
            switch (((ButtonEx)sender).Name) {
                case "ButtonExUpdate":
                    if (this.ComboBoxExStaffDisplayName.SelectedIndex == -1) {
                        MessageBox.Show("従事者を選択して下さい", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    try {
                        this.SetSheetView(this.SheetViewList, _staffWorkingHoursDao.SelectAllStaffWorkingHoursVo(this.DateTimePickerExOperationDate1.GetDate(),
                                                                                                                 this.DateTimePickerExOperationDate2.GetDate(),
                                                                                                                 ((HComboBoxExSelectNameVo)this.ComboBoxExStaffDisplayName.SelectedItem).StaffMasterVo.StaffCode));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemPrintA4":
                    SpreadList.PrintSheet(SheetViewList);
                    break;
                case "ToolStripMenuItemExit":
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        private void InitializeSheetView(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false;                  // DrugDropを禁止する
            this.SpreadList.PaintSelectionHeader = false;           // ヘッダの選択状態をしない
            this.SpreadList.TabStripPolicy = TabStripPolicy.Never;  // Tab非表示
            sheetView.ClearRange(0, 1, 35, 6, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <param name="listStaffWorkingHoursVo"></param>
        private void SetSheetView(SheetView sheetView, List<StaffWorkingHoursVo> listStaffWorkingHoursVo) {
            this.SpreadList.SuspendLayout();                // Spread 非活性化
            sheetView.ColumnHeader.Cells[0, 0].Text = string.Concat(this.ComboBoxExStaffDisplayName.Text, " さんの労働時間集計表 (雇上・区契・臨時・工場)");
            this.InitializeSheetView(this.SheetViewList);   // Spread 初期化

            int startRow = (int)this.DateTimePickerExOperationDate1.GetDate().DayOfWeek;    // 曜日番号を取得
            for (int row = 0; row < _dateUtility.GetEndOfMonth(this.DateTimePickerExOperationDate2.GetDate()).Day; row++) {
                sheetView.Rows[startRow + row].ForeColor = Color.Black;                     // default色の設定
                /*
                 * 日付を処理
                 */
                DateTime targetOperationDate = _dateUtility.GetBeginOfMonth(this.DateTimePickerExOperationDate1.GetDate()).AddDays(row).Date;
                if (targetOperationDate.DayOfWeek == DayOfWeek.Sunday) {
                    sheetView.Cells[startRow + row, colOperationDate].ForeColor = Color.Red;
                    sheetView.Cells[startRow + row, colOperationDate].Text = string.Concat(targetOperationDate.ToString("yyyy年MM月dd日(dddd)"));
                } else if (_dictionaryHoliday.ContainsKey(targetOperationDate)) {
                    sheetView.Cells[startRow + row, colOperationDate].ForeColor = Color.Red;
                    sheetView.Cells[startRow + row, colOperationDate].Text = string.Concat(targetOperationDate.ToString("yyyy年MM月dd日"), "(", _dictionaryHoliday[targetOperationDate], ")");
                } else {
                    sheetView.Cells[startRow + row, colOperationDate].Text = targetOperationDate.ToString("yyyy年MM月dd日(dddd)");
                }
                /*
                 * 詳細を処理
                 */
                StaffWorkingHoursVo? staffWorkingHoursVo = listStaffWorkingHoursVo.Find(x => x.OperationDate.Date == targetOperationDate && x.SetName != "");
                if (staffWorkingHoursVo is not null && staffWorkingHoursVo.SetName != "当日無断で欠勤" && staffWorkingHoursVo.SetName != "当日朝電で欠勤") {
                    TimeSpan firstTimeSpan = new(staffWorkingHoursVo.FirstRollCallTime.Hour, staffWorkingHoursVo.FirstRollCallTime.Minute, 0);
                    TimeSpan lastTimeSpan = new(staffWorkingHoursVo.LastRollCallTime.Hour, staffWorkingHoursVo.LastRollCallTime.Minute, 0);
                    TimeSpan totalTimeSpan = lastTimeSpan - firstTimeSpan - new TimeSpan(1, 0, 0);
                    sheetView.Cells[startRow + row, colSetName].Text = staffWorkingHoursVo.SetName;
                    sheetView.Cells[startRow + row, colFirstRollCallTime].Value = firstTimeSpan;
                    sheetView.Cells[startRow + row, colBreakTime].Value = new TimeSpan(1, 0, 0);
                    sheetView.Cells[startRow + row, colLastRollCallTime].Value = lastTimeSpan;
                    sheetView.Cells[startRow + row, colWorkingTime].Value = totalTimeSpan;
                } else {
                    sheetView.Rows[startRow + row].ForeColor = Color.Gray;
                    sheetView.Cells[startRow + row, colSetName].Text = "休み";
                }
            }
            /*
             * １週間毎の合計
             */


            this.SpreadList.ResumeLayout();     // Spread 活性化
        }

        /// <summary>
        /// ComboBoxExStaffDisplayNameを初期化
        /// </summary>
        private void InitializeComboBoxExStaffDisplayName() {
            ComboBoxExStaffDisplayName.Items.Clear();
            //List<HComboBoxExSelectNameVo> listComboBoxSelectNameVo = new();
            foreach (StaffMasterVo staffMasterVo in _staffMasterDao.SelectAllStaffMaster(new List<int> { 11, 12, 14, 15, 22 },          // 社員・アルバイト・嘱託雇用契約社員・パートタイマー・労供
                                                                                         new List<int> { 20, 22, 99 },                  // 労供長期・労供短期・指定なし
                                                                                         new List<int> { 10, 11, 12, 13, 20, 99 },      // 運転手・作業員・自転車駐輪場・リサイクルセンター・事務員・指定なし
                                                                                         false))
                ComboBoxExStaffDisplayName.Items.Add(new HComboBoxExSelectNameVo(staffMasterVo.Name, staffMasterVo));
            ComboBoxExStaffDisplayName.DisplayMember = "Name";
        }

        /// <summary>
        /// インナークラス
        /// </summary>
        private class HComboBoxExSelectNameVo {
            private string _name;
            private StaffMasterVo _staffMasterVo;

            // プロパティをコンストラクタでセット
            public HComboBoxExSelectNameVo(string name, StaffMasterVo staffMasterVo) {
                _name = name;
                _staffMasterVo = staffMasterVo;
            }
            public string Name {
                get => _name;
                set => _name = value;
            }
            public StaffMasterVo StaffMasterVo {
                get => _staffMasterVo;
                set => _staffMasterVo = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePickerExOperationDate1_ValueChanged(object sender, EventArgs e) {
            if (((DateTimePickerEx)sender).Value > this.DateTimePickerExOperationDate2.GetValue()) {
                this.DateTimePickerExOperationDate2.SetValueJp(_dateUtility.GetEndOfMonth(((DateTimePickerEx)sender).GetValue()));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePickerExOperationDate2_ValueChanged(object sender, EventArgs e) {
            if (((DateTimePickerEx)sender).Value < this.DateTimePickerExOperationDate1.GetValue()) {
                this.DateTimePickerExOperationDate1.SetValueJp(_dateUtility.GetBeginOfMonth(((DateTimePickerEx)sender).GetValue()));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StaffWorkingHours_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult dialogResult = MessageBox.Show("アプリケーションを終了します。よろしいですか？", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            switch (dialogResult) {
                case DialogResult.OK:
                    e.Cancel = false;
                    Dispose();
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }
    }
}
