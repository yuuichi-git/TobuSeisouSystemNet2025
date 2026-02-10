/*
 * 2025/05/13
 */
using Common;

using Dao;

using FarPoint.Excel;
using FarPoint.Win.Spread;

using Vo;

namespace RollCall {
    public partial class RollCallRecordSheet : Form {
        /*
         * Dao
         */
        private SetMasterDao _setMasterDao;
        private CarMasterDao _carMasterDao;
        private StaffMasterDao _staffMasterDao;
        private VehicleDispatchDetailDao _vehicleDispatchDetailDao;
        private FirstRollCallDao _firstRollCallDao;
        private LastRollCallDao _lastRollCallDao;
        /*
         * Vo
         */
        private List<SetMasterVo> _listSetMasterVo;
        private List<CarMasterVo> _listCarMasterVo;
        private List<StaffMasterVo> _listStaffMasterVo;
        private List<VehicleDispatchDetailVo> _listVehicleDispatchDetailVo;
        private FirstRollCallVo _firstRollCallVo;
        private LastRollCallVo _lastRollCallVo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public RollCallRecordSheet(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _setMasterDao = new(connectionVo);
            _carMasterDao = new(connectionVo);
            _staffMasterDao = new(connectionVo);
            _vehicleDispatchDetailDao = new(connectionVo);
            _firstRollCallDao = new(connectionVo);
            _lastRollCallDao = new(connectionVo);
            /*
             * Vo
             */
            _listSetMasterVo = _setMasterDao.SelectAllSetMaster();
            _listCarMasterVo = _carMasterDao.SelectAllCarMaster();
            _listStaffMasterVo = _staffMasterDao.SelectAllStaffMaster(null, null, null, null);                                                  // 第四パラメータをNullにすることで、退職者も含めて取得する
            _listVehicleDispatchDetailVo = new();
            _firstRollCallVo = new();
            _lastRollCallVo = new();
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
                "ToolStripMenuItemPrint",
                "ToolStripMenuItemPrintA4",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);
            // 配車日付
            this.DateTimePickerExOperationDate.SetValueJp(DateTime.Now.Date);
            // 車庫地
            this.ComboBoxExManagedSpace.Text = "本社営業所";
            // SPREAD
            this.InitializeSheetView(this.SheetViewList);
            // StatusStrip
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            this.SheetViewList.ClearRange(4, 1, 70, 22, true);                              // SPREADクリア
            int ManagedSpaceCode = this.ComboBoxExManagedSpace.SelectedIndex + 1;           // 0:該当なし 1:本社営業所 2:三郷車庫
            /*
             * FirstRollCallVoを取得
             */
            _firstRollCallVo = _firstRollCallDao.SelectOneFirstRollCallVo(this.DateTimePickerExOperationDate.GetValue());
            if (_firstRollCallVo is null) {
                MessageBox.Show("選択日付の点呼実施者記録が存在しません。処理を終了します。", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.SheetViewList.Cells[1, 1].Text = string.Concat(this.DateTimePickerExOperationDate.GetValueJp(), "  天候：", _firstRollCallVo.Weather, "  ", this.ComboBoxExManagedSpace.Text);

            /*
             * Rowの処理
             */
            int row = 0;
            _listVehicleDispatchDetailVo = _vehicleDispatchDetailDao.SelectAllVehicleDispatchDetail(this.DateTimePickerExOperationDate.GetValue());
            foreach (VehicleDispatchDetailVo vehicleDispatchDetailVo in _listVehicleDispatchDetailVo.FindAll(x => x.OperationFlag == true && x.ManagedSpaceCode == ManagedSpaceCode).OrderBy(x => x.StaffRollCallYmdHms1)) {
                // LastRollCallVoを読込 
                _lastRollCallVo = _lastRollCallDao.SelectOneLastRollCall(vehicleDispatchDetailVo.SetCode, vehicleDispatchDetailVo.OperationDate, vehicleDispatchDetailVo.LastRollCallYmdHms);
                // 第五週の状態
                bool fiveLap = _listSetMasterVo.Find(x => x.SetCode == vehicleDispatchDetailVo.SetCode).FiveLap;
                /*
                 * 第五週が休車対象で、第５週になった場合点呼記録簿から除外する
                 */
                if (vehicleDispatchDetailVo.OperationDate.Day > 28 && fiveLap == false) {
                    /*
                     * 第五週に休車の配車先は、点呼記録簿に記載しない
                     */
                } else {
                    /*
                     * 車両が指定されていないものは、点呼記録簿から除外する
                     */
                    if (vehicleDispatchDetailVo.CarCode > 0) {
                        // 配車先
                        SheetViewList.Cells[row + 4, 1].Text = string.Concat(_listSetMasterVo.Find(x => x.SetCode == vehicleDispatchDetailVo.SetCode).SetName1,
                                                                             _listSetMasterVo.Find(x => x.SetCode == vehicleDispatchDetailVo.SetCode).SetName2);
                        // 自動車登録番号
                        SheetViewList.Cells[row + 4, 2].Text = _listCarMasterVo.Find(x => x.CarCode == vehicleDispatchDetailVo.CarCode).RegistrationNumber;
                        // 運転手
                        SheetViewList.Cells[row + 4, 3].Text = _listStaffMasterVo.Find(x => x.StaffCode == vehicleDispatchDetailVo.StaffCode1).DisplayName;
                        // 点呼方法
                        SheetViewList.Cells[row + 4, 4].Text = "対面";
                        // 点呼時刻
                        SheetViewList.Cells[row + 4, 5].Text = vehicleDispatchDetailVo.StaffRollCallYmdHms1.ToString("H:mm");
                        // 免許の所持
                        SheetViewList.Cells[row + 4, 6].Text = "✓";
                        // 健康状態・睡眠状況
                        SheetViewList.Cells[row + 4, 7].Text = "✓";
                        // 日常の点検
                        SheetViewList.Cells[row + 4, 9].Text = "✓";
                        // 酒気帯びの有無
                        SheetViewList.Cells[row + 4, 10].Text = "✓";
                        // 検知器使用の有無
                        SheetViewList.Cells[row + 4, 11].Text = "有";
                        // 指示事項・その他の事項
                        SheetViewList.Cells[row + 4, 12].Text = string.Concat(_firstRollCallVo.Instruction1, "\r\n\r\n", _firstRollCallVo.Instruction2);
                        // 点呼実施者
                        switch (ManagedSpaceCode) {
                            case 1:
                                int secondStart = vehicleDispatchDetailVo.StaffRollCallYmdHms1.Second; // 秒（0～59）
                                SheetViewList.Cells[row + 4, 13].Text = (secondStart % 2 == 0) ? _firstRollCallVo.RollCallName1 : _firstRollCallVo.RollCallName2;
                                break;
                            case 2:
                                SheetViewList.Cells[row + 4, 13].Text = _firstRollCallVo.RollCallName5;
                                break;
                        }
                        /*
                         * 乗務後点呼
                         */
                        if (vehicleDispatchDetailVo.LastRollCallFlag) {
                            // 最終搬入先
                            SheetViewList.Cells[row + 4, 14].Text = _lastRollCallVo.LastPlantName;
                            // 回数
                            SheetViewList.Cells[row + 4, 15].Text = _lastRollCallVo.LastPlantCount.ToString();
                            // 搬入時刻
                            SheetViewList.Cells[row + 4, 16].Text = _lastRollCallVo.LastPlantYmdHms.ToString("HH:mm");
                            // 帰庫時刻
                            SheetViewList.Cells[row + 4, 17].Text = _lastRollCallVo.LastRollCallYmdHms.ToString("HH:mm");
                            // 点呼方法
                            SheetViewList.Cells[row + 4, 18].Text = "対面";
                            // 酒気帯びの有無
                            SheetViewList.Cells[row + 4, 19].Text = "✓";
                            // 検知器使用の有無
                            SheetViewList.Cells[row + 4, 20].Text = "有";
                            // 自動車、道路及び運行の状況　その他必要な事項
                            SheetViewList.Cells[row + 4, 21].Text = vehicleDispatchDetailVo.SetMemo;
                            // 点呼実施者
                            switch (ManagedSpaceCode) {
                                case 1:
                                    int secondEnd = vehicleDispatchDetailVo.StaffRollCallYmdHms1.Second; // 秒（0～59）
                                    /*
                                     * 秒の数字によって点呼者を帰る
                                     */
                                    switch (secondEnd.ToString("00").Substring(1, 1)) {
                                        case "0":
                                        case "1":
                                        case "2":
                                        case "3":
                                        case "4":
                                            SheetViewList.Cells[row + 4, 22].Text = _firstRollCallVo.RollCallName3;
                                            break;
                                        case "5":
                                        case "6":
                                        case "7":
                                        case "8":
                                        case "9":
                                            SheetViewList.Cells[row + 4, 22].Text = _firstRollCallVo.RollCallName4;
                                            break;
                                    }
                                    break;
                                case 2:
                                    SheetViewList.Cells[row + 4, 22].Text = _firstRollCallVo.RollCallName5;
                                    break;
                            }
                        }
                        row++;
                    }
                }
            }
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
                    string fileName = string.Concat("点呼記録簿", DateTimePickerExOperationDate.GetDate().ToString("MM月dd日"), ComboBoxExManagedSpace.Text, "分");
                    this.SpreadList.SaveExcel(new DirectryUtility().GetExcelDesktopPassXlsx(fileName), ExcelSaveFlags.UseOOXMLFormat);
                    MessageBox.Show("デスクトップへエクスポートしました", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "ToolStripMenuItemPrintA4":
                    this.SpreadList.PrintSheet(SheetViewList);
                    break;
                case "ToolStripMenuItemExit":
                    Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private SheetView InitializeSheetView(SheetView sheetView) {
            SpreadList.AllowDragDrop = false; // DrugDropを禁止する
            SpreadList.PaintSelectionHeader = false; // ヘッダの選択状態をしない
            SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            SpreadList.TabStripPolicy = TabStripPolicy.Never; // シートタブを非表示
            //sheetView.AlternatingRows.Count = 2; // 行スタイルを２行単位とします
            //sheetView.AlternatingRows[0].BackColor = Color.WhiteSmoke; // 1行目の背景色を設定します
            //sheetView.AlternatingRows[1].BackColor = Color.White; // 2行目の背景色を設定します
            //sheetView.ColumnHeader.Rows[0].Height = 22; // Columnヘッダの高さ
            //sheetView.GrayAreaBackColor = Color.White;
            //sheetView.HorizontalGridLine = new GridLine(GridLineType.None);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9); // 行ヘッダのFont
            //sheetView.RowHeader.Columns[0].Width = 48; // 行ヘッダの幅を変更します
            //sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);
            //sheetView.RemoveRows(0, sheetView.Rows.Count);
            return sheetView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RollCallRecordSheet_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult dialogResult = MessageBox.Show("アプリケーションを終了します。よろしいですか？", "メッセージ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
