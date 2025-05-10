/*
 * 2025-04-30
 */
using Dao;

using Vo;

namespace Accounting {
    public partial class AccountingParttimeList : Form {
        /*
         * Dao
         */
        private readonly SetMasterDao _setMasterDao;
        private readonly CarMasterDao _carMasterDao;
        private readonly StaffMasterDao _staffMasterDao;
        private readonly VehicleDispatchDetailDao _vehicleDispatchDetailDao;
        /*
         * Vo
         */
        private readonly List<SetMasterVo> _listSetMasterVo;
        private readonly List<CarMasterVo> _listCarMasterVo;
        private readonly List<StaffMasterVo> _listStaffMasterVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public AccountingParttimeList(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _setMasterDao = new(connectionVo);
            _carMasterDao = new(connectionVo);
            _staffMasterDao = new(connectionVo);
            _vehicleDispatchDetailDao = new(connectionVo);
            /*
             * Vo
             */
            _listSetMasterVo = _setMasterDao.SelectAllSetMaster();
            _listCarMasterVo = _carMasterDao.SelectAllCarMaster();
            _listStaffMasterVo = _staffMasterDao.SelectAllStaffMaster(null, null, null, false);
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
            this.MenuStripEx1.ChangeEnable(listString);
            this.DateTimePickerExOperationDate.SetValueJp(DateTime.Now.Date);
            this.InitializeSheetViewList();
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// HButtonExUpdate_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            this.InitializeSheetViewList();
            this.SetSheetViewList(_vehicleDispatchDetailDao.SelectAllVehicleDispatchDetail(DateTimePickerExOperationDate.GetValue().Date));
        }


        string _operationName = string.Empty;
        private void SetSheetViewList(List<VehicleDispatchDetailVo> listVehicleDispatchDetailVo) {
            int startRow = 3;
            int startCol = 1;

            // 日付
            SheetViewList.Cells["E2"].Text = this.DateTimePickerExOperationDate.GetValueJp();

            foreach (StaffMasterVo staffMasterVo in _listStaffMasterVo.FindAll(x => x.Belongs == 12 && x.VehicleDispatchTarget == true && x.RetirementFlag == false).OrderBy(x => x.EmploymentDate)) {
                SheetViewList.Cells[startRow, startCol].Text = staffMasterVo.DisplayName;
                VehicleDispatchDetailVo vehicleDispatchDetailVo = listVehicleDispatchDetailVo.Find(x => (x.StaffCode1 == staffMasterVo.StaffCode ||
                                                                                                         x.StaffCode2 == staffMasterVo.StaffCode ||
                                                                                                         x.StaffCode3 == staffMasterVo.StaffCode ||
                                                                                                         x.StaffCode4 == staffMasterVo.StaffCode) &&
                                                                                                         x.OperationDate == this.DateTimePickerExOperationDate.GetValue().Date);
                /*
                 * 配車先が設定されてなくてStaffLabelExだけ置いてある場合処理をしない
                 * ”vehicleDispatchDetailVo.Set_code > 0” → この部分
                 */
                if (vehicleDispatchDetailVo != null && vehicleDispatchDetailVo.SetCode > 0) {
                    SheetViewList.Cells[startRow, startCol + 1].Text = "出勤";
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
                    SheetViewList.Cells[startRow, startCol + 2].Text = string.Concat(_operationName, _listSetMasterVo.Find(x => x.SetCode == vehicleDispatchDetailVo.SetCode).SetName);
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
                        SheetViewList.Cells[startRow, startCol + 3].Text = carKidName;
                    }
                    /*
                     * 出勤地
                     */
                    if (vehicleDispatchDetailVo.StaffCode1 == staffMasterVo.StaffCode) {
                        SheetViewList.Cells[startRow, startCol + 4].Text = vehicleDispatchDetailVo.CarGarageCode == 1 ? "本社" : "三郷";
                    } else {
                        SheetViewList.Cells[startRow, startCol + 4].Text = "本社";
                    }
                }
                startRow++;
            }
            this.ToolStripStatusLabelDetail.Text = string.Concat(this.DateTimePickerExOperationDate.GetValueJp(), "のデータを更新しました。");
        }

        /// <summary>
        /// ToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                // A4で印刷する
                case "ToolStripMenuItemPrintA4":
                    //アクティブシート印刷します
                    SpreadList.PrintSheet(SheetViewList);
                    break;
                // アプリケーションを終了する
                case "ToolStripMenuItemExit":
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// InitializeSheetViewList
        /// </summary>
        private void InitializeSheetViewList() {
            SheetViewList.Cells["E2"].Text = string.Empty;
            // 指定範囲のデータをクリア
            SheetViewList.ClearRange(3, 1, 40, 5, true);
        }
    }
}
