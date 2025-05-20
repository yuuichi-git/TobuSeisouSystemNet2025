/*
 * 2025-04-30
 */
using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace Accounting {
    public partial class AccountingFulltimeList : Form {
        /*
         * Dao
         */
        private BelongsMasterDao _belongsMasterDao;
        private JobFormMasterDao _jobFormMasterDao;
        private OccupationMasterDao _occupationMasterDao;
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
        /// <param name="screen"></param>
        public AccountingFulltimeList(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _belongsMasterDao = new(connectionVo);
            _jobFormMasterDao = new(connectionVo);
            _occupationMasterDao = new(connectionVo);
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
             * Dictionary
             */
            foreach (BelongsMasterVo belongsMasterVo in _belongsMasterDao.SelectAllBelongsMaster())
                _dictionaryBelongs.Add(belongsMasterVo.Code, belongsMasterVo.Name);
            foreach (OccupationMasterVo occupationMasterVo in _occupationMasterDao.SelectAllOccupationMaster())
                _dictionaryOccupation.Add(occupationMasterVo.Code, occupationMasterVo.Name);
            foreach (JobFormMasterVo jobFormMasterVo in _jobFormMasterDao.SelectAllJobFormMaster())
                _dictionaryJobForm.Add(jobFormMasterVo.Code, jobFormMasterVo.Name);
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
            this.InitializeSheetViewList(this.SheetViewList);
            this.StatusStripEx.ToolStripStatusLabelDetail.Text = string.Empty;
            /*
             * Eventを登録する
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// ButtonExUpdate_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            this.PutSheetViewList(_vehicleDispatchDetailDao.SelectAllVehicleDispatchDetail(DateTimePickerExOperationDate.GetValue().Date));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listVehicleDispatchDetailVo"></param>
        private void PutSheetViewList(List<VehicleDispatchDetailVo> listVehicleDispatchDetailVo) {
            this.SpreadList.SuspendLayout();                                                                                                                                            // 非活性化
            int spreadListTopRow = SpreadList.GetViewportTopRow(0);                                                                                                                     // 先頭行（列）インデックスを取得
            if (this.SheetViewList.Rows.Count > 0)                                                                                                                                      // Rowを削除する
                this.SheetViewList.RemoveRows(0, this.SheetViewList.Rows.Count);

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
                    this.SheetViewList.Rows.Add(i, 1);
                    this.SheetViewList.RowHeader.Columns[0].Label = (i + 1).ToString();                                                                                                     // Rowヘッダ
                    this.SheetViewList.Rows[i].Height = 20;                                                                                                                                 // Rowの高さ
                    this.SheetViewList.Rows[i].Resizable = false;                                                                                                                           // RowのResizableを禁止

                    this.SheetViewList.Cells[i, 0].Value = _listStaffMasterVo.Find(x => x.StaffCode == accountingFulltimeVo.StaffCode).UnionCode;
                    this.SheetViewList.Cells[i, 1].Text = string.Concat("【", _dictionaryOccupation[accountingFulltimeVo.Occupation], "】", _listStaffMasterVo.Find(x => x.StaffCode == accountingFulltimeVo.StaffCode).DisplayName);
                    if (vehicleDispatchDetailVo.CarCode > 0) {
                        this.SheetViewList.Cells[i, 3].Text = _listCarMasterVo.Find(x => x.CarCode == vehicleDispatchDetailVo.CarCode).DisguiseKind2;
                    }
                    this.SheetViewList.Cells[i, 4].Text = _listSetMasterVo.Find(x => x.SetCode == vehicleDispatchDetailVo.SetCode).SetName;

                    switch (vehicleDispatchDetailVo.ManagedSpaceCode) {
                        case 0:
                            break;
                        case 1:
                            this.SheetViewList.Cells[i, 5].Text = "本社";
                            break;
                        case 2:
                            this.SheetViewList.Cells[i, 5].Text = "三郷";
                            break;
                    }
                    i++;
                }
                listAccountingFulltimeVo = null;
            }
            this.SpreadList.SetViewportTopRow(0, spreadListTopRow);                                                                                                                          // 先頭行（列）インデックスをセット
            this.SheetViewList.SortRows(0, true, true);
            this.SpreadList.ResumeLayout();                                                                                                                                                  // 活性化
            this.StatusStripEx.ToolStripStatusLabelDetail.Text = string.Concat(" ", i, " 件");
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
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private SheetView InitializeSheetViewList(SheetView sheetView) {
            SpreadList.AllowDragDrop = false; // DrugDropを禁止する
            SpreadList.PaintSelectionHeader = false; // ヘッダの選択状態をしない
            SpreadList.TabStripPolicy = TabStripPolicy.Never; // シートタブを非表示
            sheetView.ColumnHeader.Rows[0].Height = 30; // Columnヘッダの高さ
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9); // 行ヘッダのFont
            sheetView.RowHeader.Columns[0].Width = 50; // 行ヘッダの幅を変更します
            sheetView.RemoveRows(0, sheetView.Rows.Count);
            return sheetView;
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
        private void AccountingFulltime_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
