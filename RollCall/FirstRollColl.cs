/*
 * 2025-1-14
 */
using Common;

using ControlEx;

using Dao;

using FarPoint.Excel;
using FarPoint.Win.Spread;

using GrapeCity.Spreadsheet;

using Vo;

namespace RollCall {
    public partial class FirstRollColl : Form {
        /// <summary>
        /// Rowのスタート位置
        /// </summary>
        private int _startRow = 4;
        /// <summary>
        /// Columnのスタート位置
        /// </summary>
        readonly Dictionary<int, int> _dictionaryColNumber = new() { { 0, 0 }, { 1, 26 } };
        /// <summary>
        /// Rowの最大数
        /// Sheetも調整してね！
        /// これ以上Rowを追加すると会計システムが読取らない(確か99まで)
        /// </summary>
        readonly int _rowMax = 79;
        /// <summary>
        /// 配車先の別名
        /// </summary>
        private readonly Dictionary<int, string> _dictionaryWordCode = new() { { 13101, "千代田区" },
                                                                               { 13102, "中央区" },
                                                                               { 13103, "港区" },
                                                                               { 13104, "新宿区" },
                                                                               { 13105, "文京区" },
                                                                               { 13106, "台東区" },
                                                                               { 13107, "墨田区" },
                                                                               { 13108, "江東区" },
                                                                               { 13109, "品川区" },
                                                                               { 13110, "目黒区" },
                                                                               { 13111, "大田区" },
                                                                               { 13112, "世田谷区" },
                                                                               { 13113, "渋谷区" },
                                                                               { 13114, "中野区" },
                                                                               { 13115, "杉並区" },
                                                                               { 13116, "豊島区" },
                                                                               { 13117, "北区" },
                                                                               { 13118, "荒川区" },
                                                                               { 13119, "板橋区" },
                                                                               { 13120, "練馬区" },
                                                                               { 13121, "足立区" },
                                                                               { 13122, "葛飾区" },
                                                                               { 13123, "江戸川区" } };
        // 所属に対応したカラー
        private readonly Dictionary<string, System.Drawing.Color> _dictionaryBelongsColor = new() { { "", System.Drawing.Color.White },
                                                                                                    { "新", System.Drawing.Color.White},
                                                                                                    { "新作", System.Drawing.Color.White},
                                                                                                    { "自", System.Drawing.Color.White},
                                                                                                    { "自作", System.Drawing.Color.White},
                                                                                                    { "バ", System.Drawing.Color.Wheat},
                                                                                                    { "バ作", System.Drawing.Color.LightBlue},
                                                                                                    { "派", System.Drawing.Color.White},
                                                                                                    { "派作", System.Drawing.Color.MistyRose} };
        /*
         * Dao
         */
        private readonly SetMasterDao _setMasterDao;
        private readonly CarMasterDao _carMasterDao;
        private readonly StaffMasterDao _staffMasterDao;
        private readonly BelongsMasterDao _belongsMasterDao;
        private readonly JobFormMasterDao _jobFormMasterDao;
        private readonly OccupationMasterDao _occupationMasterDao;
        private readonly FirstRollCallDao _firstRollCallDao;
        private readonly VehicleDispatchDetailDao _vehicleDispatchDetailDao;
        private readonly FareMasterDao _fareMasterDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;
        private FareMasterVo _fareMasterVo;
        private List<SetMasterVo> _listSetMasterVo;
        private List<CarMasterVo> _listCarMasterVo;
        private List<StaffMasterVo> _listStaffMasterVo;
        private List<FareMasterVo> _listFareMasterVo;
        /*
         * Dictionary
         */
        private readonly Dictionary<int, string> _dictionaryBelongs = new();
        private readonly Dictionary<int, string> _dictionaryOccupation = new();
        private readonly Dictionary<int, string> _dictionaryJobForm = new();

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public FirstRollColl(ConnectionVo connectionVo) {
            /*
             * Dao
             */
            _setMasterDao = new(connectionVo);
            _carMasterDao = new(connectionVo);
            _staffMasterDao = new(connectionVo);
            _belongsMasterDao = new(connectionVo);
            _jobFormMasterDao = new(connectionVo);
            _occupationMasterDao = new(connectionVo);
            _firstRollCallDao = new(connectionVo);
            _vehicleDispatchDetailDao = new(connectionVo);
            _fareMasterDao = new(connectionVo);
            ;
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            _listSetMasterVo = _setMasterDao.SelectAllSetMaster();
            _listCarMasterVo = _carMasterDao.SelectAllCarMaster();
            _listStaffMasterVo = _staffMasterDao.SelectAllStaffMaster(null, null, null, false);
            _listFareMasterVo = _fareMasterDao.SelectAllFareMasterVo();
            /*
             * Dictionary
             */
            foreach (BelongsMasterVo belongsMasterVo in _belongsMasterDao.SelectAllBelongsMaster())
                _dictionaryBelongs.Add(belongsMasterVo.Code, belongsMasterVo.Name);
            foreach (OccupationMasterVo occupationMasterVo in _occupationMasterDao.SelectAllOccupationMaster())
                _dictionaryOccupation.Add(occupationMasterVo.Code, occupationMasterVo.Name);
            foreach (JobFormMasterVo jobFormMasterVo in _jobFormMasterDao.SelectAllJobFormMaster())
                _dictionaryJobForm.Add(jobFormMasterVo.Code, jobFormMasterVo.Name);

            InitializeComponent();
            this.InitializeSheetViewFirstRollCall();
            this.InitializeSheetViewPartTimeStaff(this.SheetViewPartTimeStaff);
            this.InitializeSheetViewFullStaff(this.SheetViewFullStaff);
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
        private void ButtonEx_Click(object sender, EventArgs e) {
            switch (((CcButton)sender).Name) {
                case "ButtonExUpdate":
                    try {
                        /*
                         * 
                         * ①配車表作成
                         * 点呼執行者が選択されているかの確認
                         * 
                         */
                        if (this.ComboBoxEx1.Text == string.Empty || this.ComboBoxEx2.Text == string.Empty || this.ComboBoxEx3.Text == string.Empty || this.ComboBoxEx4.Text == string.Empty || this.ComboBoxEx5.Text == string.Empty) {
                            MessageBox.Show("点呼執行者を選択して下さい", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        /*
                         * 天候が選択されているかを確認
                         */
                        if (this.ComboBoxExWeather.Text == string.Empty) {
                            MessageBox.Show("天候を選択して下さい", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        /*
                         * 指示事項が
                         */
                        if (this.ComboBoxEx7.Text.Length < 10) {
                            MessageBox.Show("指示事項(10文字以上)を入力して下さい", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        /*
                         * 再更新できないようにする
                         */
                        this.DateTimePickerExOperationDate.Enabled = false;
                        this.groupBoxEx1.Enabled = false;
                        this.groupBoxEx2.Enabled = false;
                        this.CheckBoxEx1.Enabled = false;
                        ((Button)sender).Enabled = false;
                        /*
                         * H_FirstRollCallVo書換え
                         */
                        try {
                            if (_firstRollCallDao.ExistenceFirstRollCallVo(this.DateTimePickerExOperationDate.GetValue().Date)) {
                                _firstRollCallDao.UpdateOneFirstRollCallVo(this.SetFirstRollCallVo());
                            } else {
                                _firstRollCallDao.InsertOneFirstRollCallVo(this.SetFirstRollCallVo());
                            }
                        } catch (Exception exception) {
                            MessageBox.Show(exception.Message);
                        }
                        this.PutSheetViewFirstRollCall();
                        /*
                         * 
                         * ②アルバイト出勤表作成
                         * 
                         */
                        this.PutSheetViewPartTime(_vehicleDispatchDetailDao.SelectAllVehicleDispatchDetail(this.DateTimePickerExOperationDate.GetValue().Date));
                        /*
                         * 
                         * ③全従事者出勤表作成
                         * 
                         */
                        this.PutSheetViewFullStaff(_vehicleDispatchDetailDao.SelectAllVehicleDispatchDetail(this.DateTimePickerExOperationDate.GetValue().Date));
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
                case "ToolStripMenuItemExportExcel":
                    //xls形式ファイルをエクスポートします
                    string fileName = string.Concat("配車当日", DateTime.Now.ToString("MM月dd日"), "作成");
                    SpreadFirstRollCall.SaveExcel(new DirectryUtility().GetExcelDesktopPassXlsx(fileName), ExcelSaveFlags.UseOOXMLFormat);
                    MessageBox.Show("デスクトップへエクスポートしました", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "ToolStripMenuItemExit":
                    Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private FirstRollCallVo SetFirstRollCallVo() {
            FirstRollCallVo firstRollCallVo = new();
            firstRollCallVo.OperationDate = this.DateTimePickerExOperationDate.GetValue();
            firstRollCallVo.RollCallName1 = this.ComboBoxEx1.Text;
            firstRollCallVo.RollCallName2 = this.ComboBoxEx2.Text;
            firstRollCallVo.RollCallName3 = this.ComboBoxEx3.Text;
            firstRollCallVo.RollCallName4 = this.ComboBoxEx4.Text;
            firstRollCallVo.RollCallName5 = this.ComboBoxEx5.Text;
            firstRollCallVo.Weather = this.ComboBoxExWeather.Text;
            firstRollCallVo.Instruction1 = this.ComboBoxEx7.Text;
            firstRollCallVo.Instruction2 = this.ComboBoxEx8.Text;
            return firstRollCallVo;
        }

        /// <summary>
        /// 配車表を初期化
        /// </summary>
        private void InitializeSheetViewFirstRollCall() {
            this.DateTimePickerExOperationDate.SetValueJp(DateTime.Today);          // 日付を初期化
            this.ComboBoxEx1.Text = string.Empty;                                   // 本社(出庫１)
            this.ComboBoxEx2.Text = string.Empty;                                   // 本社(出庫２)
            this.ComboBoxEx3.Text = string.Empty;                                   // 本社(帰庫１)
            this.ComboBoxEx4.Text = string.Empty;                                   // 本社(帰庫２)
            this.ComboBoxEx5.Text = string.Empty;                                   // 三郷
            this.ComboBoxExWeather.Text = string.Empty;                             // 天気
            this.ComboBoxEx7.Text = string.Empty;
            this.ComboBoxEx8.Text = string.Empty;
            this.SpreadFirstRollCall.TabStripPolicy = TabStripPolicy.Always;        // SPREAD初期化
            this.SpreadFirstRollCall.StatusBarVisible = true;
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;     // StatusStrip
        }

        /// <summary>
        /// アルバイト出勤表を初期化
        /// </summary>
        private void InitializeSheetViewPartTimeStaff(SheetView sheetView) {
            this.SpreadFirstRollCall.AllowDragDrop = false; // DrugDropを禁止する
            this.SpreadFirstRollCall.PaintSelectionHeader = false; // ヘッダの選択状態をしない
            this.SpreadFirstRollCall.TabStrip.DefaultSheetTab.Font = new System.Drawing.Font("Yu Gothic UI", 9);
            sheetView.GrayAreaBackColor = System.Drawing.Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.Flat);
            sheetView.Cells["E2"].Text = string.Empty;
            sheetView.ClearRange(3, 1, 40, 5, true); // 指定範囲のデータをクリア
        }

        /// <summary>
        /// 全従事者出勤表を初期化
        /// </summary>
        /// <param name="sheetView"></param>
        private void InitializeSheetViewFullStaff(SheetView sheetView) {
            this.SpreadFirstRollCall.AllowDragDrop = false; // DrugDropを禁止する
            this.SpreadFirstRollCall.PaintSelectionHeader = false; // ヘッダの選択状態をしない
            this.SpreadFirstRollCall.TabStrip.DefaultSheetTab.Font = new System.Drawing.Font("Yu Gothic UI", 9);
            sheetView.ColumnHeader.Rows[0].Height = 30; // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = System.Drawing.Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.Flat);
            sheetView.RowHeader.Columns[0].Font = new System.Drawing.Font("Yu Gothic UI", 9); // 行ヘッダのFont
            sheetView.RowHeader.Columns[0].Width = 50; // 行ヘッダの幅を変更します
            sheetView.RemoveRows(0, sheetView.Rows.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        private void PutSheetViewFirstRollCall() {
            // インナークラス　選択行等を保持
            EntryCellPosition entryCellPosition = new();
            int blockRowCount;
            // 非活性化
            this.SpreadFirstRollCall.SuspendLayout();
            // 配車日時
            SheetViewFirstRollCall.Cells[0, 0].Text = this.DateTimePickerExOperationDate.GetValueJp();
            // 天候
            SheetViewFirstRollCall.Cells[0, 12].Text = this.ComboBoxExWeather.Text;
            /*
             * 解析１
             */
            List<VehicleDispatchDetailVo> listVehicleDispatchDetailVo = _vehicleDispatchDetailDao.SelectAllVehicleDispatchDetail(this.DateTimePickerExOperationDate.GetValue());
            foreach (FareMasterVo fareMasterVo in _listFareMasterVo.OrderBy(x => x.Code)) {
                blockRowCount = 0;
                foreach (VehicleDispatchDetailVo vehicleDispatchDetailVo in listVehicleDispatchDetailVo.OrderBy(x => x.CellNumber)) {
                    /*
                     * 配車表に表示する条件
                     * SetCode > 0
                     */
                    if (vehicleDispatchDetailVo.SetCode > 0 && _listSetMasterVo.Find(x => x.SetCode == vehicleDispatchDetailVo.SetCode).FareCode == fareMasterVo.Code) {
                        /*
                         * 区分セルを作成する
                         */
                        if (blockRowCount == 0)
                            CreateSpan(GetNextCellPosition(), fareMasterVo.Name);
                        entryCellPosition = GetNextCellPosition();
                        /*
                         * 列が”AA"に変わった場合はBlockNameを挿入する
                         */
                        if (entryCellPosition != null && entryCellPosition.Row == _startRow && entryCellPosition.Col == _dictionaryColNumber[1]) {
                            CreateSpan(entryCellPosition, fareMasterVo.Name);
                            entryCellPosition.Row++;
                        }
                        /*
                         * セルへ出力する
                         */
                        if (entryCellPosition is not null) {
                            CreateSetRow(entryCellPosition, vehicleDispatchDetailVo);
                            CreateCarRow(entryCellPosition, vehicleDispatchDetailVo);
                            CreateOperator1Row(entryCellPosition, vehicleDispatchDetailVo);
                            CreateOperator2Row(entryCellPosition, vehicleDispatchDetailVo);
                            CreateOperator3Row(entryCellPosition, vehicleDispatchDetailVo);
                            CreateOperator4Row(entryCellPosition, vehicleDispatchDetailVo);
                        } else {
                            MessageBox.Show("配車表の行数が不足しています。システム管理者へ報告して下さい。", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        }
                        blockRowCount++;
                    }
                }
            }
            this.SpreadFirstRollCall.ResumeLayout(true);
        }

        /// <summary>
        /// 配車先の作成
        /// </summary>
        /// <param name="entryCellPosition"></param>
        /// <param name="vehicleDispatchDetailVo"></param>
        private void CreateSetRow(EntryCellPosition entryCellPosition, VehicleDispatchDetailVo vehicleDispatchDetailVo) {
            string setName1;
            string setName2;
            /*
             * 組がセットされていなければ何もしない
             */
            if (vehicleDispatchDetailVo.SetCode > 0) {
                /*
                 * 区契の場合の表示は”〇〇区”とするため、条件分岐する
                 */
                if (_listSetMasterVo.Find(x => x.SetCode == vehicleDispatchDetailVo.SetCode).ClassificationCode != 11) {

                    setName1 = _listSetMasterVo.Find(x => x.SetCode == vehicleDispatchDetailVo.SetCode).SetName1;
                    setName2 = _listSetMasterVo.Find(x => x.SetCode == vehicleDispatchDetailVo.SetCode).SetName2;
                } else {
                    /*
                     * 配車先が区契の場合、dictionaryWordCodeを参照する
                     */
                    setName1 = _dictionaryWordCode[_listSetMasterVo.Find(x => x.SetCode == vehicleDispatchDetailVo.SetCode).WordCode];
                    setName2 = _listSetMasterVo.Find(x => x.SetCode == vehicleDispatchDetailVo.SetCode).SetName2;
                }
                /*
                 * setName1
                 */
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col].ForeColor = vehicleDispatchDetailVo.OperationFlag ? System.Drawing.Color.Black : System.Drawing.Color.Red;
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col].Text = setName1;
                /*
                 * setName2
                 * 北区軽粗大・資源(１３１１７０６)は金曜日だけ資源車になって、作業員の料金が変わる。だから印を付ける
                 */
                switch (vehicleDispatchDetailVo.SetCode) {
                    case 1311706: // 北区軽粗大・資源
                        SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 1].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                        SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 1].ForeColor = vehicleDispatchDetailVo.OperationFlag ? System.Drawing.Color.Black : System.Drawing.Color.Red;
                        if (this.DateTimePickerExOperationDate.GetValue().DayOfWeek != DayOfWeek.Friday) {
                            SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 1].Text = setName2;
                        } else {
                            SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 1].BackColor = System.Drawing.Color.Yellow;
                            SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 1].Text = "資源";
                        }
                        break;
                    default:
                        SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 1].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                        SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 1].ForeColor = vehicleDispatchDetailVo.OperationFlag ? System.Drawing.Color.Black : System.Drawing.Color.Red;
                        SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 1].Text = setName2;
                        break;
                }
                /*
                 * 指示事項・その他連絡事項
                 */
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 22].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 22].Text = vehicleDispatchDetailVo.SetMemo;
            }
        }

        /// <summary>
        /// 車両の作成
        /// </summary>
        /// <param name="entryCellPosition"></param>
        /// <param name="vehicleDispatchDetailVo"></param>
        private void CreateCarRow(EntryCellPosition entryCellPosition, VehicleDispatchDetailVo vehicleDispatchDetailVo) {
            string doorNumber = string.Empty;
            string registrationNumber = string.Empty;
            /*
             * 組がセットされていなければ何もしない
             */
            if (vehicleDispatchDetailVo.SetCode > 0 && vehicleDispatchDetailVo.CarCode > 0) {
                doorNumber = _listCarMasterVo.Find(x => x.CarCode == vehicleDispatchDetailVo.CarCode).DoorNumber.ToString();
                registrationNumber = string.Concat(_listCarMasterVo.Find(x => x.CarCode == vehicleDispatchDetailVo.CarCode).RegistrationNumber3, _listCarMasterVo.Find(x => x.CarCode == vehicleDispatchDetailVo.CarCode).RegistrationNumber4);
                /*
                 * 本番のドアナンバー
                 */
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 2].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 2].ForeColor = vehicleDispatchDetailVo.OperationFlag ? System.Drawing.Color.Black : System.Drawing.Color.Red;
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 2].Text = doorNumber;
                /*
                 * 本番の車番
                 */
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 3].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 3].ForeColor = vehicleDispatchDetailVo.OperationFlag ? System.Drawing.Color.Black : System.Drawing.Color.Red;
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 3].Text = registrationNumber;
            }
        }

        /// <summary>
        /// 従事者１（運転手）
        /// </summary>
        /// <param name="entryCellPosition"></param>
        /// <param name="vehicleDispatchDetailVo"></param>
        private void CreateOperator1Row(EntryCellPosition entryCellPosition, VehicleDispatchDetailVo vehicleDispatchDetailVo) {
            RichText displayName; // 表示名
            string belongs = string.Empty; // 所属
            /*
             * 組がセットされていなければ何もしない
             */
            if (vehicleDispatchDetailVo.SetCode > 0 && vehicleDispatchDetailVo.StaffCode1 > 0) {
                displayName = new RichText(_listStaffMasterVo.Find(x => x.StaffCode == vehicleDispatchDetailVo.StaffCode1).DisplayName);
                switch (_listStaffMasterVo.Find(x => x.StaffCode == vehicleDispatchDetailVo.StaffCode1).Belongs) {
                    case 10:
                    case 11:
                        belongs = string.Empty;
                        break;
                    case 12:
                        belongs = "バ";
                        break;
                    case 13:
                        belongs = "派";
                        break;
                    case 14:
                    case 15:
                        belongs = string.Empty;
                        break;
                    case 22:
                        switch (_listStaffMasterVo.Find(x => x.StaffCode == vehicleDispatchDetailVo.StaffCode1).JobForm) {
                            case 20:
                            case 21:
                                belongs = "新";
                                break;
                            case 22:
                            case 23:
                                belongs = "自";
                                break;
                        }
                        break;
                }
                /*
                 * "作"を追加するかどうか
                 * ここは基本運転手の欄だから”作”はいらないと思われるけど
                 */
                if (vehicleDispatchDetailVo.StaffOccupation1 == 11) {
                    belongs = string.Concat(belongs, "作");
                }
                /*
                 * 表示名
                 */
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 8].BackColor = _dictionaryBelongsColor[belongs];
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 8].CellType = new FarPoint.Win.Spread.CellType.RichTextCellType();
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 8].Value = GetWorkStaffName(vehicleDispatchDetailVo.SetCode,
                                                                                                                        vehicleDispatchDetailVo.StaffOccupation1,
                                                                                                                        _listStaffMasterVo.Find(x => x.StaffCode == vehicleDispatchDetailVo.StaffCode1));
                //SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 8].Value = displayName;
                /*
                 * 所属
                 */
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 9].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 9].Text = belongs;
                /*
                 * 出庫地
                 * 運転手は配車先の管理地を出勤地とする
                 */
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 10].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 10].Text = vehicleDispatchDetailVo.ManagedSpaceCode == 2 ? "三" : "";
                /*
                 * 指定された配車先の点呼を除外する
                 */
                switch (vehicleDispatchDetailVo.SetCode) {
                    case 1312109: // 本社事務所
                    case 1312110: // 三郷事務所
                    case 1312111: // 整備本社
                    case 1312112: // 整備三郷
                    case 1312118: // 浄化槽
                    case 1312132: // 浄化槽(品川)
                    case 1312135: // 初任診断
                    case 1312136: // 適齢診断
                    case 1312137: // 初任研修(東環保)
                    case 1312138: // 整備管理講習
                    case 1312139: // 運行管理講習
                    case 1312140: // 当日無断で欠勤
                    case 1312141: // 当日朝電で欠勤
                    case 1312145: // 有給休暇
                    case 1312150: // 組合員欠勤
                    case 1312160: // バイト欠勤
                        return;
                }
                /*
                 * 点呼方法
                 */
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 14].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 14].Text = "対面";
                /*
                 * 点呼時刻
                 */
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 16].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 16].Text = vehicleDispatchDetailVo.StaffRollCallFlag1 ? vehicleDispatchDetailVo.StaffRollCallYmdHms1.ToString("HH:mm") : "未点呼";
                /*
                 * 免許・健康・車両・飲酒・検知器
                 */
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 17].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 17].Text = vehicleDispatchDetailVo.StaffRollCallFlag1 ? "✓" : "";
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 18].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 18].Text = vehicleDispatchDetailVo.StaffRollCallFlag1 ? "✓" : "";
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 19].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 19].Text = vehicleDispatchDetailVo.StaffRollCallFlag1 ? "✓" : "";
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 20].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 20].Text = vehicleDispatchDetailVo.StaffRollCallFlag1 ? "✓" : "";
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 21].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 21].Text = vehicleDispatchDetailVo.StaffRollCallFlag1 ? "有" : "";
                /*
                 * 周知事項
                 */

                /*
                 * 点呼執行者
                 */
                switch (vehicleDispatchDetailVo.CarGarageCode) {
                    case 1:
                        /*
                         * 点呼時刻の秒数が偶数なら”点呼執行者本社１”、奇数なら”点呼執行者本社２”を選択する
                         */
                        int second = vehicleDispatchDetailVo.StaffRollCallYmdHms1.Second; //秒（0～59）
                        SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 23].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                        if (vehicleDispatchDetailVo.StaffRollCallFlag1)
                            SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 23].Text = (second % 2 == 0) ? this.ComboBoxEx1.Text : this.ComboBoxEx2.Text;
                        break;
                    case 2:
                        /*
                         * ”点呼執行者三郷”を選択する
                         */
                        SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 23].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                        if (vehicleDispatchDetailVo.StaffRollCallFlag1)
                            SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 23].Text = this.ComboBoxEx5.Text;
                        break;
                }
            }
        }

        /// <summary>
        /// 従事者２（運転手・作業員）
        /// </summary>
        /// <param name="entryCellPosition"></param>
        /// <param name="vehicleDispatchDetailVo"></param>
        private void CreateOperator2Row(EntryCellPosition entryCellPosition, VehicleDispatchDetailVo vehicleDispatchDetailVo) {
            string belongs = string.Empty; // 所属
            /*
             * 組がセットされていなければ何もしない
             */
            if (vehicleDispatchDetailVo.SetCode > 0 && vehicleDispatchDetailVo.StaffCode2 > 0) {
                switch (_listStaffMasterVo.Find(x => x.StaffCode == vehicleDispatchDetailVo.StaffCode2).Belongs) {
                    case 10:
                    case 11:
                        belongs = string.Empty;
                        break;
                    case 12:
                        belongs = "バ";
                        break;
                    case 13:
                        belongs = "派";
                        break;
                    case 14:
                    case 15:
                        belongs = string.Empty;
                        break;
                    case 22:
                        switch (_listStaffMasterVo.Find(x => x.StaffCode == vehicleDispatchDetailVo.StaffCode2).JobForm) {
                            case 20:
                            case 21:
                                belongs = "新";
                                break;
                            case 22:
                            case 23:
                                belongs = "自";
                                break;
                        }
                        break;
                }
                /*
                 * "作"を追加するかどうか
                 */
                if (vehicleDispatchDetailVo.StaffOccupation2 == 11) {
                    belongs = string.Concat(belongs, "作");
                }
                /*
                 * 表示名
                 */
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 11].BackColor = _dictionaryBelongsColor[belongs];
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 11].CellType = new FarPoint.Win.Spread.CellType.RichTextCellType();
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 11].Value = GetWorkStaffName(vehicleDispatchDetailVo.SetCode,
                                                                                                                         vehicleDispatchDetailVo.StaffOccupation2,
                                                                                                                         _listStaffMasterVo.Find(x => x.StaffCode == vehicleDispatchDetailVo.StaffCode2));
                /*
                 * 所属
                 */
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 12].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 12].Text = belongs;
                /*
                 * 出庫地
                 */
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 13].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col + 13].Text = string.Empty; // 基本的に作業員が三郷出庫はない(大Gの相乗りは例外)
            }
        }

        /// <summary>
        /// 従事者３（運転手・作業員）
        /// </summary>
        /// <param name="entryCellPosition"></param>
        /// <param name="vehicleDispatchDetailVo"></param>
        private void CreateOperator3Row(EntryCellPosition entryCellPosition, VehicleDispatchDetailVo vehicleDispatchDetailVo) {
            string belongs = string.Empty; // 所属
            /*
             * 組がセットされていなければ何もしない
             */
            if (vehicleDispatchDetailVo.SetCode > 0 && vehicleDispatchDetailVo.StaffCode3 > 0) {
                switch (_listStaffMasterVo.Find(x => x.StaffCode == vehicleDispatchDetailVo.StaffCode3).Belongs) {
                    case 10:
                    case 11:
                        belongs = string.Empty;
                        break;
                    case 12:
                        belongs = "バ";
                        break;
                    case 13:
                        belongs = "派";
                        break;
                    case 14:
                    case 15:
                        belongs = string.Empty;
                        break;
                    case 22:
                        switch (_listStaffMasterVo.Find(x => x.StaffCode == vehicleDispatchDetailVo.StaffCode3).JobForm) {
                            case 20:
                            case 21:
                                belongs = "新";
                                break;
                            case 22:
                            case 23:
                                belongs = "自";
                                break;
                        }
                        break;
                }
                /*
                 * "作"を追加するかどうか
                 */
                if (vehicleDispatchDetailVo.StaffOccupation3 == 11) {
                    belongs = string.Concat(belongs, "作");
                }
                /*
                 * 表示名
                 */
                SheetViewFirstRollCall.Cells[entryCellPosition.Row + 1, entryCellPosition.Col + 11].BackColor = _dictionaryBelongsColor[belongs];
                SheetViewFirstRollCall.Cells[entryCellPosition.Row + 1, entryCellPosition.Col + 11].CellType = new FarPoint.Win.Spread.CellType.RichTextCellType();
                SheetViewFirstRollCall.Cells[entryCellPosition.Row + 1, entryCellPosition.Col + 11].Value = GetWorkStaffName(vehicleDispatchDetailVo.SetCode,
                                                                                                                             vehicleDispatchDetailVo.StaffOccupation3,
                                                                                                                             _listStaffMasterVo.Find(x => x.StaffCode == vehicleDispatchDetailVo.StaffCode3));
                /*
                 * 所属
                 */
                SheetViewFirstRollCall.Cells[entryCellPosition.Row + 1, entryCellPosition.Col + 12].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                SheetViewFirstRollCall.Cells[entryCellPosition.Row + 1, entryCellPosition.Col + 12].Text = belongs;
                /*
                 * 出庫地
                 */
                SheetViewFirstRollCall.Cells[entryCellPosition.Row + 1, entryCellPosition.Col + 13].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                SheetViewFirstRollCall.Cells[entryCellPosition.Row + 1, entryCellPosition.Col + 13].Text = string.Empty; // 基本的に作業員が三郷出庫はない(大Gの相乗りは例外)
            }
        }

        /// <summary>
        /// 従事者４（運転手・作業員）
        /// </summary>
        /// <param name="entryCellPosition"></param>
        /// <param name="vehicleDispatchDetailVo"></param>
        private void CreateOperator4Row(EntryCellPosition entryCellPosition, VehicleDispatchDetailVo vehicleDispatchDetailVo) {
            string belongs = string.Empty; // 所属
            /*
             * 組がセットされていなければ何もしない
             */
            if (vehicleDispatchDetailVo.SetCode > 0 && vehicleDispatchDetailVo.StaffCode4 > 0) {
                switch (_listStaffMasterVo.Find(x => x.StaffCode == vehicleDispatchDetailVo.StaffCode4).Belongs) {
                    case 10:
                    case 11:
                        belongs = string.Empty;
                        break;
                    case 12:
                        belongs = "バ";
                        break;
                    case 13:
                        belongs = "派";
                        break;
                    case 14:
                    case 15:
                        belongs = string.Empty;
                        break;
                    case 22:
                        switch (_listStaffMasterVo.Find(x => x.StaffCode == vehicleDispatchDetailVo.StaffCode4).JobForm) {
                            case 20:
                            case 21:
                                belongs = "新";
                                break;
                            case 22:
                            case 23:
                                belongs = "自";
                                break;
                        }
                        break;
                }
                /*
                 * "作"を追加するかどうか
                 */
                if (vehicleDispatchDetailVo.StaffOccupation4 == 11) {
                    belongs = string.Concat(belongs, "作");
                }
                /*
                 * 表示名
                 */
                SheetViewFirstRollCall.Cells[entryCellPosition.Row + 1, entryCellPosition.Col + 8].BackColor = _dictionaryBelongsColor[belongs];
                SheetViewFirstRollCall.Cells[entryCellPosition.Row + 1, entryCellPosition.Col + 8].CellType = new FarPoint.Win.Spread.CellType.RichTextCellType();
                SheetViewFirstRollCall.Cells[entryCellPosition.Row + 1, entryCellPosition.Col + 8].Value = GetWorkStaffName(vehicleDispatchDetailVo.SetCode,
                                                                                                                            vehicleDispatchDetailVo.StaffOccupation4,
                                                                                                                            _listStaffMasterVo.Find(x => x.StaffCode == vehicleDispatchDetailVo.StaffCode4));
                /*
                 * 所属
                 */
                SheetViewFirstRollCall.Cells[entryCellPosition.Row + 1, entryCellPosition.Col + 9].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                SheetViewFirstRollCall.Cells[entryCellPosition.Row + 1, entryCellPosition.Col + 9].Text = belongs;
                /*
                 * 出庫地
                 */
                SheetViewFirstRollCall.Cells[entryCellPosition.Row + 1, entryCellPosition.Col + 10].Font = new System.Drawing.Font("Yu Gothic UI", 9);
                SheetViewFirstRollCall.Cells[entryCellPosition.Row + 1, entryCellPosition.Col + 10].Text = string.Empty; // 基本的に作業員が三郷出庫はない(大Gの相乗りは例外)
            }
        }

        /// <summary>
        /// GetWorkStaffName
        /// ”作業員”を加えるかどうか
        /// </summary>
        /// <returns></returns>
        private string GetWorkStaffName(int setCode, int occupation, StaffMasterVo staffMasterVo) {
            string rtfText = string.Empty;
            string displayName = string.Empty;
            /*
             * 2023-02-26
             * operator_occupationの値によって”作業員"を追加する処理
             */
            switch (occupation) {
                // 10:運転手 11:作業員 20:事務職　99:指定なし
                case 11:
                    displayName = string.Concat("作業員", staffMasterVo.DisplayName);
                    /*
                     * リッチテキスト文字列の作成
                     */
                    using (RichTextBox richTextBox = new()) {
                        richTextBox.Text = displayName;
                        richTextBox.SelectionStart = 0;
                        richTextBox.SelectionLength = 3;
                        richTextBox.SelectionColor = System.Drawing.Color.Gray;
                        richTextBox.SelectionFont = new System.Drawing.Font("Yu Gothic UI", 6);
                        rtfText = richTextBox.Rtf;
                    }
                    break;
                default:
                    rtfText = string.Concat("", staffMasterVo.DisplayName);
                    break;
            }
            return rtfText;
        }

        /// <summary>
        /// CreateSpan
        /// 運賃区分欄用のセル結合処理
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="blockName"></param>
        private void CreateSpan(EntryCellPosition entryCellPosition, string blockName) {
            // セルを結合する
            this.SheetViewFirstRollCall.AddSpanCell(entryCellPosition.Row, entryCellPosition.Col, 1, 24);
            this.SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col].BackColor = System.Drawing.Color.LightGreen;
            this.SheetViewFirstRollCall.Cells[entryCellPosition.Row, entryCellPosition.Col].Text = blockName;
        }

        /// <summary>
        /// GetNextCellPosition
        /// 次に挿入するRowを特定する
        /// </summary>
        /// <returns></returns>
        private EntryCellPosition GetNextCellPosition() {
            EntryCellPosition entryCellPosition = new();
            for (int colPosition = 0; colPosition <= 1; colPosition++) { // 0:A列 1:AA列
                for (int row = _startRow; row <= _rowMax - 1; row++) {
                    if (this.SheetViewFirstRollCall.Cells[row, _dictionaryColNumber[colPosition] + 0].Text == "" &&  // 運賃コード又は配車先の位置
                        this.SheetViewFirstRollCall.Cells[row, _dictionaryColNumber[colPosition] + 8].Text == "" &&  // 運転手の位置
                        this.SheetViewFirstRollCall.Cells[row, _dictionaryColNumber[colPosition] + 11].Text == "" &&  // 作業員2の位置
                        this.SheetViewFirstRollCall.Cells[row + 1, _dictionaryColNumber[colPosition] + 11].Text == "") { // 作業員3の位置
                        entryCellPosition.Row = row;
                        entryCellPosition.Col = _dictionaryColNumber[colPosition];
                        entryCellPosition.RemainingRows = _rowMax - row;
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat("Row:", entryCellPosition.Row, " Col:", entryCellPosition.Col, " 残り", entryCellPosition.RemainingRows);
                        return entryCellPosition;
                    }
                }
            }
            /*
             * Null:行に空きが無い
             */
            return null;
        }

        /// <summary>
        /// EntryCellPosition
        /// Rowの状態を保持する
        /// </summary>
        private class EntryCellPosition {
            int _row;
            int _col;
            int _remainingRows;

            public EntryCellPosition() {
                _row = 0;
                _col = 0;
            }
            /// <summary>
            /// 挿入可能な位置を保持
            /// </summary>
            public int Row {
                get => _row;
                set => _row = value;
            }
            /// <summary>
            /// 挿入可能な位置を保持
            /// </summary>
            public int Col {
                get => _col;
                set => _col = value;
            }
            /// <summary>
            /// 残りの行数
            /// </summary>
            public int RemainingRows {
                get => _remainingRows;
                set => _remainingRows = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxEx1_CheckedChanged(object sender, EventArgs e) {
            if (((CheckBox)sender).Checked) {
                if (_firstRollCallDao.ExistenceFirstRollCallVo(this.DateTimePickerExOperationDate.GetValue())) {
                    /*
                     * レコードを表示
                     */
                    //try {

                    //} catch (Exception exception) {
                    //    MessageBox.Show(exception.);
                    //}
                    FirstRollCallVo firstRollCallVo = _firstRollCallDao.SelectOneFirstRollCallVo(this.DateTimePickerExOperationDate.GetValue());
                    this.ComboBoxEx1.Text = firstRollCallVo.RollCallName1;
                    this.ComboBoxEx2.Text = firstRollCallVo.RollCallName2;
                    this.ComboBoxEx3.Text = firstRollCallVo.RollCallName3;
                    this.ComboBoxEx4.Text = firstRollCallVo.RollCallName4;
                    this.ComboBoxEx5.Text = firstRollCallVo.RollCallName5;
                    this.ComboBoxExWeather.Text = firstRollCallVo.Weather;
                    this.ComboBoxEx7.Text = firstRollCallVo.Instruction1;
                    this.ComboBoxEx8.Text = firstRollCallVo.Instruction2;
                } else {
                    MessageBox.Show("指定日の点呼記録はありません。", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            } else {
                this.ComboBoxEx1.Text = string.Empty;
                this.ComboBoxEx2.Text = string.Empty;
                this.ComboBoxEx3.Text = string.Empty;
                this.ComboBoxEx4.Text = string.Empty;
                this.ComboBoxEx5.Text = string.Empty;
                this.ComboBoxExWeather.Text = string.Empty;
                this.ComboBoxEx7.Text = string.Empty;
                this.ComboBoxEx8.Text = string.Empty;
            }
        }

        /*
         * 
         * アルバイト出勤表
         * 
         */
        private void PutSheetViewPartTime(List<VehicleDispatchDetailVo> listVehicleDispatchDetailVo) {
            int startRow = 3;
            int startCol = 1;
            string _operationName = string.Empty;

            // 日付
            this.SheetViewPartTimeStaff.Cells["E2"].Text = this.DateTimePickerExOperationDate.GetValueJp();

            foreach (StaffMasterVo staffMasterVo in _listStaffMasterVo.FindAll(x => x.Belongs == 12 && x.VehicleDispatchTarget == true && x.RetirementFlag == false).OrderBy(x => x.EmploymentDate)) {
                this.SheetViewPartTimeStaff.Cells[startRow, startCol].Text = staffMasterVo.DisplayName;
                VehicleDispatchDetailVo? vehicleDispatchDetailVo = listVehicleDispatchDetailVo.Find(x => (x.StaffCode1 == staffMasterVo.StaffCode ||
                                                                                                          x.StaffCode2 == staffMasterVo.StaffCode ||
                                                                                                          x.StaffCode3 == staffMasterVo.StaffCode ||
                                                                                                          x.StaffCode4 == staffMasterVo.StaffCode) &&
                                                                                                          x.OperationDate == this.DateTimePickerExOperationDate.GetValue().Date);
                /*
                 * 配車先が設定されてなくてStaffLabelExだけ置いてある場合処理をしない
                 * ”vehicleDispatchDetailVo.Set_code > 0” → この部分
                 */
                if (vehicleDispatchDetailVo != null && vehicleDispatchDetailVo.SetCode > 0) {
                    switch (vehicleDispatchDetailVo.SetCode) {
                        case 1312140: // 当日朝電・無断
                        case 1312141:
                            this.SheetViewPartTimeStaff.Cells[startRow, startCol + 1].Text = "欠勤";
                            break;
                        default:
                            this.SheetViewPartTimeStaff.Cells[startRow, startCol + 1].Text = "出勤";
                            break;
                    }
                    /*
                     * 除外を設定
                     * ①整備本社は全て【運転手】にする（真由美さん依頼）
                     */
                    switch (vehicleDispatchDetailVo.SetCode) {
                        case 1312111: // 整備本社
                            _operationName = "【運転手】";
                            break;
                        default:
                            _operationName = vehicleDispatchDetailVo.StaffCode1 == staffMasterVo.StaffCode ? "【運転手】" : "【作業員】";
                            break;
                    }
                    this.SheetViewPartTimeStaff.Cells[startRow, startCol + 2].Text = string.Concat(_operationName, _listSetMasterVo.Find(x => x.SetCode == vehicleDispatchDetailVo.SetCode).SetName);
                    /*
                     * 車種
                     */
                    CarMasterVo carMasterVo = _listCarMasterVo.Find(x => x.CarCode == vehicleDispatchDetailVo.CarCode);
                    if (carMasterVo != null && vehicleDispatchDetailVo.StaffCode1 == staffMasterVo.StaffCode) {
                        var carKidName = "";
                        switch (carMasterVo.CarKindCode) {
                            case 10:
                                carKidName = "軽自動車";
                                break;
                            case 11:
                                carKidName = "小型";
                                break;
                            case 12:
                                carKidName = "普通";
                                break;
                        }
                        this.SheetViewPartTimeStaff.Cells[startRow, startCol + 3].Text = carKidName;
                    }
                    /*
                     * 出勤地
                     */
                    if (vehicleDispatchDetailVo.StaffCode1 == staffMasterVo.StaffCode) {
                        this.SheetViewPartTimeStaff.Cells[startRow, startCol + 4].Text = vehicleDispatchDetailVo.CarGarageCode == 1 ? "本社" : "三郷";
                    } else {
                        this.SheetViewPartTimeStaff.Cells[startRow, startCol + 4].Text = "本社";
                    }
                }
                startRow++;
            }
        }

        /*
         * 
         * 全出勤者出勤表
         * 
         */
        private void PutSheetViewFullStaff(List<VehicleDispatchDetailVo> listVehicleDispatchDetailVo) {
            this.SpreadFirstRollCall.SuspendLayout();                                                                                                                                   // 非活性化
            int spreadListTopRow = SpreadFirstRollCall.GetViewportTopRow(0);                                                                                                            // 先頭行（列）インデックスを取得
            if (this.SheetViewFullStaff.Rows.Count > 0)                                                                                                                                 // Rowを削除する
                this.SheetViewFullStaff.RemoveRows(0, this.SheetViewFullStaff.Rows.Count);

            int i = 0;
            List<AccountingFulltimeVo> listAccountingFulltimeVo;
            foreach (VehicleDispatchDetailVo vehicleDispatchDetailVo in listVehicleDispatchDetailVo.FindAll(x => x.OperationFlag == true && x.VehicleDispatchFlag == true)) {
                listAccountingFulltimeVo = new();
                if (vehicleDispatchDetailVo.StaffCode1 != 0) {
                    AccountingFulltimeVo accountingFulltimeVo = new();
                    accountingFulltimeVo.StaffCode = vehicleDispatchDetailVo.StaffCode1;
                    accountingFulltimeVo.Occupation = vehicleDispatchDetailVo.StaffOccupation1;
                    listAccountingFulltimeVo.Add(accountingFulltimeVo);
                }
                if (vehicleDispatchDetailVo.StaffCode2 != 0) {
                    AccountingFulltimeVo accountingFulltimeVo = new();
                    accountingFulltimeVo.StaffCode = vehicleDispatchDetailVo.StaffCode2;
                    accountingFulltimeVo.Occupation = vehicleDispatchDetailVo.StaffOccupation2;
                    listAccountingFulltimeVo.Add(accountingFulltimeVo);
                }
                if (vehicleDispatchDetailVo.StaffCode3 != 0) {
                    AccountingFulltimeVo accountingFulltimeVo = new();
                    accountingFulltimeVo.StaffCode = vehicleDispatchDetailVo.StaffCode3;
                    accountingFulltimeVo.Occupation = vehicleDispatchDetailVo.StaffOccupation3;
                    listAccountingFulltimeVo.Add(accountingFulltimeVo);
                }
                if (vehicleDispatchDetailVo.StaffCode4 != 0) {
                    AccountingFulltimeVo accountingFulltimeVo = new();
                    accountingFulltimeVo.StaffCode = vehicleDispatchDetailVo.StaffCode4;
                    accountingFulltimeVo.Occupation = vehicleDispatchDetailVo.StaffOccupation4;
                    listAccountingFulltimeVo.Add(accountingFulltimeVo);
                }

                foreach (AccountingFulltimeVo accountingFulltimeVo in listAccountingFulltimeVo) {
                    this.SheetViewFullStaff.Rows.Add(i, 1);
                    this.SheetViewFullStaff.RowHeader.Columns[0].Label = (i + 1).ToString();                                                                                            // Rowヘッダ
                    this.SheetViewFullStaff.Rows[i].Height = 20;                                                                                                                        // Rowの高さ
                    this.SheetViewFullStaff.Rows[i].Resizable = false;                                                                                                                  // RowのResizableを禁止

                    this.SheetViewFullStaff.Cells[i, 0].Value = _listStaffMasterVo.Find(x => x.StaffCode == accountingFulltimeVo.StaffCode).UnionCode;
                    this.SheetViewFullStaff.Cells[i, 1].Text = string.Concat("【", _dictionaryOccupation[accountingFulltimeVo.Occupation], "】", _listStaffMasterVo.Find(x => x.StaffCode == accountingFulltimeVo.StaffCode).DisplayName);
                    if (vehicleDispatchDetailVo.CarCode > 0) {
                        this.SheetViewFullStaff.Cells[i, 3].Text = _listCarMasterVo.Find(x => x.CarCode == vehicleDispatchDetailVo.CarCode).DisguiseKind2;
                    }
                    this.SheetViewFullStaff.Cells[i, 4].Text = _listSetMasterVo.Find(x => x.SetCode == vehicleDispatchDetailVo.SetCode).SetName;

                    switch (vehicleDispatchDetailVo.ManagedSpaceCode) {
                        case 0:
                            break;
                        case 1:
                            this.SheetViewFullStaff.Cells[i, 5].Text = "本社";
                            break;
                        case 2:
                            this.SheetViewFullStaff.Cells[i, 5].Text = "三郷";
                            break;
                    }
                    i++;
                }
                listAccountingFulltimeVo = null;
            }
            this.SpreadFirstRollCall.SetViewportTopRow(0, spreadListTopRow);                                                                                                                          // 先頭行（列）インデックスをセット
            this.SpreadFirstRollCall.ResumeLayout();                                                                                                                                                  // 活性化
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(" ", i, " 件");
        }

        /// <summary>
        /// インナークラス(Vo)
        /// </summary>
        private class AccountingFulltimeVo {
            int _staffCode = 0;
            int _occupation = 0;

            public int StaffCode {
                get => this._staffCode;
                set => this._staffCode = value;
            }
            public int Occupation {
                get => this._occupation;
                set => this._occupation = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstRollColl_FormClosing(object sender, FormClosingEventArgs e) {
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
