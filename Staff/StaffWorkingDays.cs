/*
 * 2025-02-12
 */
using Common;

using ControlEx;

using Dao;

using FarPoint.Excel;
using FarPoint.Win.Spread;

using Vo;

namespace Staff {
    public partial class StaffWorkingDays : Form {
        /*
         * SPREADのColumnの番号
         */
        private const int colBelongs = 0;
        private const int colDisplayName = 1;
        private const int colWorkdays = 2;
        private const int colSundays = 3;
        private const int colHolidays = 4;
        /*
         * インターネットから祝日のデータを取得
         */
        private readonly Dictionary<DateTime, string> _dictionaryHoliday = new HolidayUtility().GetHoliday();
        /*
         * インスタンス作成
         */
        private readonly DateUtility _dateUtility = new();
        /*
         * Dao
         */
        private readonly StaffMasterDao _staffMasterDao;
        private readonly VehicleDispatchDetailDao _vehicleDispatchDetailDao;
        private BelongsMasterDao _belongsMasterDao;
        /*
         * Vo
         */
        private List<StaffMasterVo> _listStaffMasterVo;
        /*
         * Dictionary
         */
        private readonly Dictionary<int, string> _dictionaryBelongs = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public StaffWorkingDays(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _staffMasterDao = new(connectionVo);
            _vehicleDispatchDetailDao = new(connectionVo);
            _belongsMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _listStaffMasterVo = _staffMasterDao.SelectAllStaffMaster(new List<int> { 11, 12, 14, 15, 22 },          // 社員・アルバイト・嘱託雇用契約社員・パートタイマー・労供
                                                                      new List<int> { 20, 22, 99 },                  // 労供長期・労供短期・指定なし
                                                                      new List<int> { 10, 11, 12, 13, 20, 99 },      // 運転手・作業員・自転車駐輪場・リサイクルセンター・事務員・指定なし
                                                                      false);
            /*
             * Dictionary
             */
            foreach (BelongsMasterVo belongsMasterVo in _belongsMasterDao.SelectAllBelongsMaster())
                _dictionaryBelongs.Add(belongsMasterVo.Code, belongsMasterVo.Name);
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
                "ToolStripMenuItemExport",
                "ToolStripMenuItemExportExcel",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);
            /*
             * 配車日を設定
             */
            this.DateTimePickerExOperationDate1.SetValue(_dateUtility.GetBeginOfMonth(DateTime.Now));
            this.DateTimePickerExOperationDate2.SetValue(_dateUtility.GetEndOfMonth(DateTime.Now));
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
                    try {
                        this.SetSheetView(this.SheetViewList, _vehicleDispatchDetailDao.SelectAllVehicleDispatchDetail(this.DateTimePickerExOperationDate1.GetDate(), this.DateTimePickerExOperationDate2.GetDate()));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <param name="listVehicleDispatchDetailVo"></param>
        private void SetSheetView(SheetView sheetView, List<VehicleDispatchDetailVo> listVehicleDispatchDetailVo) {
            int rowCount = 0;
            this.SpreadList.SuspendLayout();                            // Spread 非活性化
            sheetView.ColumnHeader.Cells[0, 0].Text = string.Concat(this.DateTimePickerExOperationDate1.GetDate().ToString("yyyy/MM/dd"), "から", this.DateTimePickerExOperationDate2.GetDate().ToString("yyyy/MM/dd"), "までの集計");

            // Rowを削除する
            if (sheetView.Rows.Count > 0)
                sheetView.RemoveRows(0, sheetView.Rows.Count);

            /*
             * 所属・氏名
             */
            foreach (StaffMasterVo staffMasterVo in _listStaffMasterVo.OrderBy(x => x.Belongs).ThenBy(x => x.NameKana)) {
                sheetView.Rows.Add(rowCount, 1);
                sheetView.RowHeader.Columns[0].Label = (rowCount + 1).ToString();                           // Rowヘッダ
                sheetView.Rows[rowCount].Height = 20;                                                       // Rowの高さ
                sheetView.Rows[rowCount].Resizable = false;                                                 // RowのResizableを禁止
                sheetView.Rows[rowCount].Tag = staffMasterVo;                                               // Voを退避

                sheetView.Cells[rowCount, colBelongs].Text = _dictionaryBelongs[staffMasterVo.Belongs];     // 役職又は所属
                sheetView.Cells[rowCount, colDisplayName].Text = staffMasterVo.Name;
                rowCount++;
            }

            for (int rowNumber = 0; rowNumber < sheetView.Rows.Count; rowNumber++) {
                int staffCode = ((StaffMasterVo)sheetView.Rows[rowNumber].Tag).StaffCode;
                int workdaysCount = 0;
                int sundaysCount = 0;
                int holidaysCount = 0;
                foreach (VehicleDispatchDetailVo vehicleDispatchDetailVo in listVehicleDispatchDetailVo.FindAll(x => x.StaffCode1 == staffCode || x.StaffCode2 == staffCode || x.StaffCode3 == staffCode || x.StaffCode4 == staffCode)) {
                    /*
                     * 平日・日曜・休日の回数を調べる
                     */
                    if (vehicleDispatchDetailVo.OperationDate.DayOfWeek == DayOfWeek.Sunday) {
                        sundaysCount++;
                    } else if (_dictionaryHoliday.ContainsKey(vehicleDispatchDetailVo.OperationDate)) {
                        holidaysCount++;
                    } else {
                        workdaysCount++;
                    }
                }
                sheetView.Cells[rowNumber, colWorkdays].Value = workdaysCount;                              // 平日
                sheetView.Cells[rowNumber, colSundays].Value = sundaysCount;                                // 日曜
                sheetView.Cells[rowNumber, colHolidays].Value = holidaysCount;                              // 休日
            }

            this.SpreadList.ResumeLayout();                                                                 // Spread 活性化
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(" ", rowCount, " 件");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemExportExcel":
                    //xls形式ファイルをエクスポートします
                    string fileName = string.Concat("出勤日数表(", DateTimePickerExOperationDate1.GetDate().ToString("yyyy年MM月dd日"), " ～ ", DateTimePickerExOperationDate2.GetDate().ToString("yyyy年MM月dd日"));
                    SpreadList.SaveExcel(new Directry().GetExcelDesktopPassXlsx(fileName), ExcelSaveFlags.UseOOXMLFormat);
                    MessageBox.Show("デスクトップへエクスポートしました", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private SheetView InitializeSheetView(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false; // DrugDropを禁止する
            this.SpreadList.PaintSelectionHeader = false; // ヘッダの選択状態をしない
            this.SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            this.SpreadList.TabStripPolicy = TabStripPolicy.Never; // Tab非表示
            sheetView.AlternatingRows.Count = 2; // 行スタイルを２行単位とします
            sheetView.AlternatingRows[0].BackColor = Color.WhiteSmoke; // 1行目の背景色を設定します
            sheetView.AlternatingRows[1].BackColor = Color.White; // 2行目の背景色を設定します
            sheetView.ColumnHeader.Rows[0].Height = 26; // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.None);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9); // 行ヘッダのFont
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);
            sheetView.RemoveRows(0, sheetView.Rows.Count);
            return sheetView;
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
