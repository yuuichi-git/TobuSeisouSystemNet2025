/*
 * 2026-06-07
 */
using Common;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace Toukanpo {
    public partial class WorkPerformanceSurveyForm : Form {
        private DateUtility _dateUtility = new();
        /*
         * Dao
         */
        private StaffMasterDao _staffMasterDao;
        private readonly BelongsMasterDao _belongsMasterDao;
        private VehicleDispatchDetailDao _vehicleDispatchDetailDao;
        /*
         * Vo
         */
        private List<StaffMasterVo> _listStaffMasterVo;
        private List<VehicleDispatchDetailVo> _listVehicleDispatchDetailVo;
        /*
         * Dictionary
         */
        private readonly Dictionary<int, string> _dictionaryBelongs = new();

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public WorkPerformanceSurveyForm(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _staffMasterDao = new(connectionVo);
            _belongsMasterDao = new(connectionVo);
            _vehicleDispatchDetailDao = new(connectionVo);
            /*
             * Dictionary
             */
            foreach (BelongsMasterVo belongsMasterVo in _belongsMasterDao.SelectAllBelongsMaster())
                _dictionaryBelongs.Add(belongsMasterVo.Code, belongsMasterVo.Name);

            InitializeComponent();
            /*
             * MenuStrip
             */
            List<string> listString = new() {"ToolStripMenuItemFile",
                                             "ToolStripMenuItemExit",
                                             "ToolStripMenuItemHelp"
            };
            this.CcMenuStrip1.ChangeEnable(listString);
            this.InitializeControls();
            this.InitializeSheetView(this.SheetViewList);
            this.SetSheetView(this.SheetViewList);
            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "Initialize Success";
            /*
             * Eventを登録する
             */
            this.CcMenuStrip1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcButtonUpdate_Click(object sender, EventArgs e) {
            this.SetSheetView(this.SheetViewList);

            DateTime startDateTime = new((int)CcNumericUpDownYear.Value, (int)CcNumericUpDownMonth.Value, 1);
            DateTime endDateTime = _dateUtility.GetEndOfMonth(startDateTime);
            _listStaffMasterVo = _staffMasterDao.SelectAllStaffMaster(null, null, null, null);
            _listVehicleDispatchDetailVo = _vehicleDispatchDetailDao.SelectAllVehicleDispatchDetail(startDateTime, endDateTime);

            List<WorkPerformanceSurveyFormVo> listWorkPerformanceSurveyFormVo = new();

            /*
             * ①Staffを抽出する
             */
            foreach (VehicleDispatchDetailVo vehicleDispatchDetailVo in _listVehicleDispatchDetailVo) {
                /*
                 * 抽出する条件
                 * SetCode > 0
                 */
                if (vehicleDispatchDetailVo.SetCode > 0) {
                    /*
                     * VehicleDispatchDetailVoからStaffを取得し、Voにセットする
                     */
                    for (int i = 0; i < 4; i++) {

                        switch (i) {
                            case 0:
                                if (vehicleDispatchDetailVo.StaffCode1 != 0) {
                                    WorkPerformanceSurveyFormVo workPerformanceSurveyFormVo0 = new();
                                    workPerformanceSurveyFormVo0.Number = string.Empty;
                                    workPerformanceSurveyFormVo0.StaffCode = _listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode1).StaffCode;
                                    workPerformanceSurveyFormVo0.StaffName = _listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode1).Name;
                                    workPerformanceSurveyFormVo0.BelongsCode = _listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode1).Belongs;
                                    workPerformanceSurveyFormVo0.BelongsName = _dictionaryBelongs[workPerformanceSurveyFormVo0.BelongsCode];
                                    workPerformanceSurveyFormVo0.GenderCode = _listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode1).Gender == "男" ? "h" : "f";
                                    workPerformanceSurveyFormVo0.GenderName = _listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode1).Gender;
                                    workPerformanceSurveyFormVo0.Age = _dateUtility.GetAge(_listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode1).BirthDate).ToString();
                                    workPerformanceSurveyFormVo0.OperationDate = vehicleDispatchDetailVo.OperationDate;
                                    workPerformanceSurveyFormVo0.ClassificationName = vehicleDispatchDetailVo.ClassificationCode == 10 ? "雇上" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 11 ? "区契" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 12 ? "臨時" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 20 ? "清掃工場" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 30 ? "社内" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 40 ? "水物" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 50 ? "一般" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 51 ? "社用車" : "指定なし";
                                    workPerformanceSurveyFormVo0.Driver = true;
                                    listWorkPerformanceSurveyFormVo.Add(workPerformanceSurveyFormVo0);
                                }

                                break;
                            case 1:
                                if (vehicleDispatchDetailVo.StaffCode2 != 0) {
                                    WorkPerformanceSurveyFormVo workPerformanceSurveyFormVo1 = new();
                                    workPerformanceSurveyFormVo1.Number = string.Empty;
                                    workPerformanceSurveyFormVo1.StaffCode = _listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode2).StaffCode;
                                    workPerformanceSurveyFormVo1.StaffName = _listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode2).Name;
                                    workPerformanceSurveyFormVo1.BelongsCode = _listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode2).Belongs;
                                    workPerformanceSurveyFormVo1.BelongsName = _dictionaryBelongs[workPerformanceSurveyFormVo1.BelongsCode];
                                    workPerformanceSurveyFormVo1.GenderCode = _listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode2).Gender == "男" ? "h" : "f";
                                    workPerformanceSurveyFormVo1.GenderName = _listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode2).Gender;
                                    workPerformanceSurveyFormVo1.Age = _dateUtility.GetAge(_listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode2).BirthDate).ToString();
                                    workPerformanceSurveyFormVo1.OperationDate = vehicleDispatchDetailVo.OperationDate;
                                    workPerformanceSurveyFormVo1.ClassificationName = vehicleDispatchDetailVo.ClassificationCode == 10 ? "雇上" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 11 ? "区契" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 12 ? "臨時" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 20 ? "清掃工場" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 30 ? "社内" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 40 ? "水物" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 50 ? "一般" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 51 ? "社用車" : "指定なし";
                                    workPerformanceSurveyFormVo1.Driver = false;
                                    listWorkPerformanceSurveyFormVo.Add(workPerformanceSurveyFormVo1);
                                }

                                break;
                            case 2:
                                if (vehicleDispatchDetailVo.StaffCode3 != 0) {
                                    WorkPerformanceSurveyFormVo workPerformanceSurveyFormVo2 = new();
                                    workPerformanceSurveyFormVo2.Number = string.Empty;
                                    workPerformanceSurveyFormVo2.StaffCode = _listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode3).StaffCode;
                                    workPerformanceSurveyFormVo2.StaffName = _listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode3).Name;
                                    workPerformanceSurveyFormVo2.BelongsCode = _listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode3).Belongs;
                                    workPerformanceSurveyFormVo2.BelongsName = _dictionaryBelongs[workPerformanceSurveyFormVo2.BelongsCode];
                                    workPerformanceSurveyFormVo2.GenderCode = _listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode3).Gender == "男" ? "h" : "f";
                                    workPerformanceSurveyFormVo2.GenderName = _listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode3).Gender;
                                    workPerformanceSurveyFormVo2.Age = _dateUtility.GetAge(_listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode3).BirthDate).ToString();
                                    workPerformanceSurveyFormVo2.OperationDate = vehicleDispatchDetailVo.OperationDate;
                                    workPerformanceSurveyFormVo2.ClassificationName = vehicleDispatchDetailVo.ClassificationCode == 10 ? "雇上" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 11 ? "区契" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 12 ? "臨時" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 20 ? "清掃工場" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 30 ? "社内" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 40 ? "水物" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 50 ? "一般" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 51 ? "社用車" : "指定なし";
                                    workPerformanceSurveyFormVo2.Driver = false;
                                    listWorkPerformanceSurveyFormVo.Add(workPerformanceSurveyFormVo2);
                                }

                                break;
                            case 3:
                                if (vehicleDispatchDetailVo.StaffCode4 != 0) {
                                    WorkPerformanceSurveyFormVo workPerformanceSurveyFormVo3 = new();
                                    workPerformanceSurveyFormVo3.Number = string.Empty;
                                    workPerformanceSurveyFormVo3.StaffCode = _listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode4).StaffCode;
                                    workPerformanceSurveyFormVo3.StaffName = _listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode4).Name;
                                    workPerformanceSurveyFormVo3.BelongsCode = _listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode4).Belongs;
                                    workPerformanceSurveyFormVo3.BelongsName = _dictionaryBelongs[workPerformanceSurveyFormVo3.BelongsCode];
                                    workPerformanceSurveyFormVo3.GenderCode = _listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode4).Gender == "男" ? "h" : "f";
                                    workPerformanceSurveyFormVo3.GenderName = _listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode4).Gender;
                                    workPerformanceSurveyFormVo3.Age = _dateUtility.GetAge(_listStaffMasterVo.Find(staff => staff.StaffCode == vehicleDispatchDetailVo.StaffCode4).BirthDate).ToString();
                                    workPerformanceSurveyFormVo3.OperationDate = vehicleDispatchDetailVo.OperationDate;
                                    workPerformanceSurveyFormVo3.ClassificationName = vehicleDispatchDetailVo.ClassificationCode == 10 ? "雇上" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 11 ? "区契" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 12 ? "臨時" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 20 ? "清掃工場" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 30 ? "社内" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 40 ? "水物" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 50 ? "一般" :
                                                                                      vehicleDispatchDetailVo.ClassificationCode == 51 ? "社用車" : "指定なし";
                                    workPerformanceSurveyFormVo3.Driver = false;
                                    listWorkPerformanceSurveyFormVo.Add(workPerformanceSurveyFormVo3);
                                }
                                break;
                        }
                    }
                }
            }



            /*
             * ②VoをSheetViewにセットする
             */
            int rowCount = 0;
            List<IGrouping<string, WorkPerformanceSurveyFormVo>> groupedListWorkPerformanceSurveyFormVo = listWorkPerformanceSurveyFormVo.GroupBy(x => x.StaffCode.ToString()).ToList();
            foreach (IGrouping<string, WorkPerformanceSurveyFormVo> groupWorkPerformanceSurveyFormVo in groupedListWorkPerformanceSurveyFormVo) {
                //string staffName = groupWorkPerformanceSurveyFormVo.Key;   // グループのキー（= StaffCode）
                SheetViewList.Rows.Add(rowCount, 1);
                SheetViewList.Rows[rowCount].Font = new Font("Yu Gothic UI", 9);                                                            // RowのFont
                SheetViewList.RowHeader.Columns[0].Label = (rowCount + 1).ToString();                                                       // Rowヘッダ
                SheetViewList.Rows[rowCount].Height = 20;                                                                                   // Rowの高さ
                SheetViewList.Rows[rowCount].Resizable = false;                                                                             // RowのResizableを禁止

                foreach (WorkPerformanceSurveyFormVo workPerformanceSurveyFormVo in groupWorkPerformanceSurveyFormVo.OrderBy(x => x.OperationDate)) {
                    SheetViewList.Cells[rowCount, 0].Text = (rowCount + 1).ToString();                                                      // №
                    SheetViewList.Cells[rowCount, 1].Text = workPerformanceSurveyFormVo.StaffName;                                          // 氏名
                    switch (workPerformanceSurveyFormVo.BelongsCode) {
                        case 10:
                        case 11:
                            SheetViewList.Cells[rowCount, 2].Text = "u";                                                                    // 所属コード
                            SheetViewList.Cells[rowCount, 3].Text = "正規社員";                                                             // 所属名
                            break;
                        case 12:
                            SheetViewList.Cells[rowCount, 2].Text = "y";                                                                    // 所属コード
                            SheetViewList.Cells[rowCount, 3].Text = "アルバイト";                                                           // 所属名
                            break;
                        case 13:
                            SheetViewList.Cells[rowCount, 2].Text = "w";                                                                    // 所属コード
                            SheetViewList.Cells[rowCount, 3].Text = "派遣社員";                                                             // 所属名
                            break;
                        case 14:
                            SheetViewList.Cells[rowCount, 2].Text = "v";                                                                    // 所属コード
                            SheetViewList.Cells[rowCount, 3].Text = "嘱託社員";                                                             // 所属名
                            break;
                        case 15:
                            SheetViewList.Cells[rowCount, 2].Text = "y";                                                                    // 所属コード
                            SheetViewList.Cells[rowCount, 3].Text = "アルバイト";                                                           // 所属名
                            break;
                        case 22:
                            SheetViewList.Cells[rowCount, 2].Text = "z";                                                                    // 所属コード
                            SheetViewList.Cells[rowCount, 3].Text = "労供";                                                                 // 所属名
                            break;
                        case 99:
                            SheetViewList.Cells[rowCount, 2].Text = "●";
                            SheetViewList.Cells[rowCount, 3].Text = "●";
                            break;
                    }
                    switch (workPerformanceSurveyFormVo.GenderName) {
                        case "男性":
                            SheetViewList.Cells[rowCount, 4].Text = "h";
                            SheetViewList.Cells[rowCount, 5].Text = "男性";
                            break;
                        case "女性":
                            SheetViewList.Cells[rowCount, 4].Text = "f";
                            SheetViewList.Cells[rowCount, 5].Text = "女性";
                            break;
                    }
                    SheetViewList.Cells[rowCount, 6].Text = workPerformanceSurveyFormVo.Age;                                                // 年齢

                    for (int day = 1; day <= 31; day++) {
                        if (workPerformanceSurveyFormVo.OperationDate.Day == day) {
                            if ((workPerformanceSurveyFormVo.ClassificationName == "雇上" || workPerformanceSurveyFormVo.ClassificationName == "臨時" || workPerformanceSurveyFormVo.ClassificationName == "清掃工場") && workPerformanceSurveyFormVo.Driver)             // 区分が「雇上」で運転手の場合
                                SheetViewList.Cells[rowCount, 6 + day].Text = "a";
                            if ((workPerformanceSurveyFormVo.ClassificationName == "雇上" || workPerformanceSurveyFormVo.ClassificationName == "臨時" || workPerformanceSurveyFormVo.ClassificationName == "清掃工場") && !workPerformanceSurveyFormVo.Driver)            // 区分が「雇上」で運転手以外の場合
                                SheetViewList.Cells[rowCount, 6 + day].Text = "b";
                            if (workPerformanceSurveyFormVo.ClassificationName == "区契" && workPerformanceSurveyFormVo.Driver)             // 区分が「区契」で運転手の場合
                                SheetViewList.Cells[rowCount, 6 + day].Text = "c";
                            if (workPerformanceSurveyFormVo.ClassificationName == "区契" && !workPerformanceSurveyFormVo.Driver)            // 区分が「区契」で運転手以外の場合
                                SheetViewList.Cells[rowCount, 6 + day].Text = "d";
                            if ((workPerformanceSurveyFormVo.ClassificationName == "一般" || workPerformanceSurveyFormVo.ClassificationName == "水物" || workPerformanceSurveyFormVo.ClassificationName == "社内") && workPerformanceSurveyFormVo.Driver)             // 区分が「一般・水物」で運転手の場合
                                SheetViewList.Cells[rowCount, 6 + day].Text = "e";
                            if ((workPerformanceSurveyFormVo.ClassificationName == "一般" || workPerformanceSurveyFormVo.ClassificationName == "水物" || workPerformanceSurveyFormVo.ClassificationName == "社内") && !workPerformanceSurveyFormVo.Driver)            // 区分が「一般・水物」で運転手以外の場合
                                SheetViewList.Cells[rowCount, 6 + day].Text = "f";
                        }

                        SheetViewList.Cells[rowCount, 38].Text = groupWorkPerformanceSurveyFormVo.Count().ToString();                       // 勤務日数
                    }


                }
                rowCount++;
            }
            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = rowCount.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemExit":
                    this.Close();
                    break;

            }
        }

        private void SetSheetView(SheetView sheetView) {
            for (int i = 0; i < DateTime.DaysInMonth((int)CcNumericUpDownYear.Value, (int)CcNumericUpDownMonth.Value); i++) {
                sheetView.ColumnHeader.Cells[0, 7 + i].Text = (i + 1).ToString();
                sheetView.ColumnHeader.Cells[1, 7 + i].Text = new DateTime((int)CcNumericUpDownYear.Value, (int)CcNumericUpDownMonth.Value, i + 1).ToString("ddd");
            }
        }

        private void InitializeControls() {
            this.CcNumericUpDownYear.Value = _dateUtility.GetFiscalYear();                                                                  // 現在の年度を設定する
            this.CcNumericUpDownMonth.Value = 4;                                                                                            // 月は4月を初期値とする
        }

        /// <summary>
        /// InitializeSheetView
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns>SheetView</returns>
        private SheetView InitializeSheetView(SheetView sheetView) {
            SpreadList.AllowDragDrop = false;                                                                                               // DrugDropを禁止する
            SpreadList.PaintSelectionHeader = false;                                                                                        // ヘッダの選択状態をしない
            SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            SpreadList.TabStripPolicy = TabStripPolicy.Never;                                                                               // タブを非表示にします
            sheetView.AlternatingRows.Count = 2;                                                                                            // 行スタイルを２行単位とします
            sheetView.AlternatingRows[0].BackColor = Color.WhiteSmoke;                                                                      // 1行目の背景色を設定します
            sheetView.AlternatingRows[1].BackColor = Color.White;                                                                           // 2行目の背景色を設定します
            sheetView.ColumnHeader.Rows[0].Height = 26;                                                                                     // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.None);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9);                                                              // 行ヘッダのFont
            sheetView.RowHeader.Columns[0].Width = 48;                                                                                      // 行ヘッダの幅を変更します
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);
            sheetView.RemoveRows(0, sheetView.Rows.Count);

            for (int i = 0; i < 31; i++) {
                sheetView.ColumnHeader.Cells[0, 7 + i].Text = " ";                                                                          // 日付
                sheetView.ColumnHeader.Cells[1, 7 + i].Text = " ";                                                                          // 曜日(スペースを挿入しないとColumns文字が表示されるよ)
            }

            return sheetView;
        }

        /*
         * ------------------------------------------------
         * WorkPerformanceSurveyFormVo
         * ------------------------------------------------
         */
        public class WorkPerformanceSurveyFormVo {
            private string _number = string.Empty;                                          // №
            private int _staffCode = 0;                                                     // 社員コード
            private string _staffName = string.Empty;                                       // 氏名
            private int _belongsCode = 99;                                                  // 所属コード(10:役員 11:社員 12:アルバイト 13:派遣 14:嘱託雇用契約社員 15:パートタイマー 22:労供 99:指定なし)
            private string _belongsName = string.Empty;                                     // 所属名
            private string _genderCode = string.Empty;                                      // 性別コード
            private string _genderName = string.Empty;                                      // 性別名
            private string _age = string.Empty;                                             // 年齢
            private DateTime _operationDate = new(1900, 1, 1);                              // 対象日
            private string _classificationName = string.Empty;                              // 区分名(10:雇上 11:区契 12:臨時 20:清掃工場 30:社内 50:一般 51:社用車 99:指定なし)
            private bool _driver = false;                                                   // true:運転手、false:運転手以外

            public WorkPerformanceSurveyFormVo() {

            }

            /// <summary>
            /// №
            /// </summary>
            public string Number { get => this._number; set => this._number = value; }
            /// <summary>
            /// 社員コード
            /// </summary>
            public int StaffCode { get => this._staffCode; set => this._staffCode = value; }
            /// <summary>
            /// 氏名
            /// </summary>
            public string StaffName { get => this._staffName; set => this._staffName = value; }
            /// <summary>
            /// 所属コード(10:役員 11:社員 12:アルバイト 13:派遣 14:嘱託雇用契約社員 15:パートタイマー 22:労供 99:指定なし)
            /// </summary>
            public int BelongsCode { get => this._belongsCode; set => this._belongsCode = value; }
            /// <summary>
            /// 所属コード(10:役員 11:社員 12:アルバイト 13:派遣 14:嘱託雇用契約社員 15:パートタイマー 22:労供 99:指定なし)
            /// </summary>
            public string BelongsName { get => this._belongsName; set => this._belongsName = value; }
            /// <summary>
            /// 性別コード
            /// </summary>
            public string GenderCode { get => this._genderCode; set => this._genderCode = value; }
            /// <summary>
            /// 性別名
            /// </summary>
            public string GenderName { get => this._genderName; set => this._genderName = value; }
            /// <summary>
            /// 年齢
            /// </summary>
            public string Age { get => this._age; set => this._age = value; }
            /// <summary>
            /// 対象日
            /// </summary>
            public DateTime OperationDate { get => this._operationDate; set => this._operationDate = value; }
            /// <summary>
            /// 区分名(10:雇上 11:区契 12:臨時 20:清掃工場 30:社内 50:一般 51:社用車 99:指定なし)
            /// </summary>
            public string ClassificationName { get => this._classificationName; set => this._classificationName = value; }
            /// <summary>
            /// true:運転手、false:運転手以外
            /// </summary>
            public bool Driver { get => this._driver; set => this._driver = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkPerformanceSurveyForm_FormClosing(object sender, FormClosingEventArgs e) {
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
