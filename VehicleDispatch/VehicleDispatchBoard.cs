﻿/*
 * 2024/10/03
 */
using System.Diagnostics;

using CollectionWeight;

using Common;

using ControlEx;

using Dao;

using DriversReport;

using License;

using RollCall;

using Staff;

using StockBox;

using Substitute;

using Vo;

namespace VehicleDispatch {
    public partial class VehicleDispatchBoard : Form {
        /*
         * インスタンス作成
         */
        private readonly ScreenForm _screenForm = new();
        /*
         * プロパティ
         */
        private SetControl _dragParentControl;
        /*
         * Dao
         */
        private SetMasterDao _setMasterDao;
        private CarMasterDao _carMasterDao;
        private StaffMasterDao _staffMasterDao;
        private VehicleDispatchDetailDao _vehicleDispatchDetailDao;
        private LicenseMasterDao _licenseMasterDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;
        private List<SetMasterVo> _listSetMasterVo;
        private List<CarMasterVo> _listCarMasterVo;
        private List<StaffMasterVo> _listStaffMasterVo;

        private Board _board;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public VehicleDispatchBoard(ConnectionVo connectionVo) {
            /*
             * Dao
             */
            _setMasterDao = new(connectionVo);
            _carMasterDao = new(connectionVo);
            _staffMasterDao = new(connectionVo);
            _vehicleDispatchDetailDao = new(connectionVo);
            _licenseMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            _listSetMasterVo = _setMasterDao.SelectAllSetMaster();
            _listCarMasterVo = _carMasterDao.SelectAllHCarMaster();
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
                "ToolStripMenuItemEdit",
                "ToolStripMenuItemUpdateTaitou",
                "ToolStripMenuItemHelp"
            };
            MenuStripEx1.ChangeEnable(listString);

            this.DateTimePickerExOperationDate.SetToday();
            // ControlButtonを初期化
            this.ButtonExStockBoxOpen.SetTextDirectionVertical = "Stock-Boxs";
            this.AddBoard();
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "InitializeSuccess";
            /*
             * Eventを登録する
             */
            MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 配車用ボードを作成
        /// </summary>
        private void AddBoard() {
            _board = new();
            /*
             * Eventを登録
             */
            _board.Board_ContextMenuStrip_Opened += ContextMenuStripEx_Opened;
            _board.Board_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
            _board.Board_OnMouseClick += OnMouseClick;
            _board.Board_OnMouseDoubleClick += OnMouseDoubleClick;
            _board.Board_OnMouseDown += OnMouseDown;
            _board.Board_OnMouseEnter += OnMouseEnter;
            _board.Board_OnMouseLeave += OnMouseLeave;
            _board.Board_OnMouseMove += OnMouseMove;
            _board.Board_OnMouseUp += OnMouseUp;

            _board.Board_OnDragDrop += OnDragDrop;
            _board.Board_OnDragEnter += OnDragEnter;
            _board.Board_OnDragOver += OnDragOver;

            TableLayoutPanelExBase.Controls.Add(_board, 1, 2);
        }

        private StockBoxs? stockBoxs = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEx_Click(object sender, EventArgs e) {
            switch (((ButtonEx)sender).Name) {
                case "ButtonExUpdate":
                    try {
                        this.AddControls(_vehicleDispatchDetailDao.SelectAllVehicleDispatchDetail(DateTimePickerExOperationDate.GetDate()));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "ButtonExStockBoxOpen":
                    if (stockBoxs is null || stockBoxs.IsDisposed) {
                        stockBoxs = new(_connectionVo, _board);
                        _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), stockBoxs);
                        stockBoxs.Show(this);
                    } else {
                        MessageBox.Show("このプログラム（Stock-Boxs）は、既に起動しています。多重起動は禁止されています。", "多重起動メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listVehicleDispatchDetailVo"></param>
        private void AddControls(List<VehicleDispatchDetailVo> listVehicleDispatchDetailVo) {
            // Board上のSetControlをDisposeする
            _board.RemoveControls();
            /*
             * 全てのCellを捜査してSetControlを追加する
             */
            int _cellNumber = 0; // 0～199
            for (int row = 0; row < _board.RowAllNumber; row++) {
                switch (row) {
                    case 0 or 2 or 4 or 6: // DetailCell
                        break;
                    case 1 or 3 or 5 or 7: // SetControlCell
                        for (int x = 0; x < _board.ColumnCount; x++) {
                            VehicleDispatchDetailVo vehicleDispatchDetailVo = listVehicleDispatchDetailVo.Find(x => x.CellNumber == _cellNumber);
                            if (vehicleDispatchDetailVo is not null) {
                                _board.AddSetControl(_cellNumber, vehicleDispatchDetailVo,
                                                     _listSetMasterVo.Find(x => x.SetCode == vehicleDispatchDetailVo.SetCode),
                                                     _listCarMasterVo.Find(x => x.CarCode == vehicleDispatchDetailVo.CarCode),
                                                     ConvertStaffMasterVo(vehicleDispatchDetailVo));
                            }
                            _cellNumber++;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// VehicleDispatchDetailVoをList<StaffMasterVo>に変換する
        /// </summary>
        /// <param name="vehicleDispatchDetailVo"></param>
        /// <returns></returns>
        private List<StaffMasterVo> ConvertStaffMasterVo(VehicleDispatchDetailVo vehicleDispatchDetailVo) {
            List<StaffMasterVo> listStaffMasterVo = new();
            listStaffMasterVo.Add(GetStaffMasterVo(vehicleDispatchDetailVo.StaffCode1));
            listStaffMasterVo.Add(GetStaffMasterVo(vehicleDispatchDetailVo.StaffCode2));
            listStaffMasterVo.Add(GetStaffMasterVo(vehicleDispatchDetailVo.StaffCode3));
            listStaffMasterVo.Add(GetStaffMasterVo(vehicleDispatchDetailVo.StaffCode4));
            return listStaffMasterVo;
        }

        /// <summary>
        /// GetStaffMasterVo
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        private StaffMasterVo? GetStaffMasterVo(int staffCode) {
            StaffMasterVo? staffMasterVo = _listStaffMasterVo.Find(x => x.StaffCode == staffCode);
            if (staffMasterVo is not null) {
                // 検索で見つかったVoを返す
                return staffMasterVo;
            } else {
                // StaffCodeがゼロ(存在しない)を返す
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VehicleDispatchBoard_FormClosing(object sender, FormClosingEventArgs e) {
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

        /// <summary>
        /// ContextMenuStripEx_Openedの際に設定される
        /// </summary>
        private Control _contextMenuStripExOpendControl;
        /*
         * Event処理
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStripEx_Opened(object sender, EventArgs e) {
            switch (((ContextMenuStrip)sender).SourceControl) {
                case SetControl setControl:
                    _contextMenuStripExOpendControl = setControl;
                    break;
                case SetLabel setLabel:
                    _contextMenuStripExOpendControl = setLabel;
                    break;
                case CarLabel carLabel:
                    _contextMenuStripExOpendControl = carLabel;
                    break;
                case StaffLabel staffLabel:
                    staffLabel.toolStripMenuItem01.Enabled = _licenseMasterDao.ExistenceLicenseMaster(staffLabel.StaffMasterVo.StaffCode);
                    _contextMenuStripExOpendControl = staffLabel;
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">ToolStripMenuItem</param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                /*
                 * 
                 * Form(MenuStrip)
                 * 
                 */
                case "ToolStripMenuItemExit":
                    MessageBox.Show("ToolStripMenuItemExit");
                    break;
                case "ToolStripMenuItemUpdateTaitou":
                    CollectionWeightTaitou collectionWeightTaitou = new(_connectionVo, this.DateTimePickerExOperationDate.GetDate());
                    _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), collectionWeightTaitou);
                    collectionWeightTaitou.ShowDialog(this);
                    break;
                /*
                 * 
                 * SetControl
                 * 
                 */
                case "ToolStripMenuItemSetControlSingle": // SetControl Single
                    int cellNumberSingle = ((SetControl)_contextMenuStripExOpendControl).CellNumber;
                    int endCellNumberSingle = 0;
                    switch (cellNumberSingle) {
                        case int i when i >= 0 && i <= 43:
                            endCellNumberSingle = 43;
                            break;
                        case int i when i >= 50 && i <= 93:
                            endCellNumberSingle = 93;
                            break;
                        case int i when i >= 100 && i <= 143:
                            endCellNumberSingle = 143;
                            break;
                        case int i when i >= 150 && i <= 193:
                            endCellNumberSingle = 193;
                            break;
                    }
                    this.SetControlChengeSingle(cellNumberSingle, endCellNumberSingle);
                    break;
                case "ToolStripMenuItemSetControlDouble": // SetControl Double
                    int cellNumberDouble = ((SetControl)_contextMenuStripExOpendControl).CellNumber;
                    int endCellNumberDouble = 0;
                    switch (cellNumberDouble) {
                        case int i when i >= 0 && i <= 43:
                            endCellNumberDouble = 43;
                            break;
                        case int i when i >= 50 && i <= 93:
                            endCellNumberDouble = 93;
                            break;
                        case int i when i >= 100 && i <= 143:
                            endCellNumberDouble = 143;
                            break;
                        case int i when i >= 150 && i <= 193:
                            endCellNumberDouble = 193;
                            break;
                    }
                    this.SetControlChengeDouble(cellNumberDouble, endCellNumberDouble);
                    break;
                /*
                 * 
                 * ---------- SetLabel ----------
                 * 
                 */
                /*
                 * 配車先の情報
                 */
                case "ToolStripMenuItemSetDetail": //
                    MessageBox.Show("ToolStripMenuItemSetDetail");
                    break;
                /*
                 * 日報印刷
                 */
                case "ToolStripMenuItemDriversReport": //
                    try {
                        DriversReportPaper driversReportPaper = new(_connectionVo, (SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl);
                        _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), driversReportPaper);
                        driversReportPaper.ShowDialog(this);
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                /*
                 * 稼働・休車
                 */
                case "ToolStripMenuItemSetOperationTrue": // 稼働
                    try {
                        ((SetLabel)_contextMenuStripExOpendControl).OperationFlag = true;                                       // SetLabelのフラグをセット(ControlのViewを変化させる)
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).OperationFlag = true;           // SetControlのフラグをセット
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).VehicleDispatchFlag = true;     // SetControlのフラグをセット
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();            // SetControlの構成を再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "ToolStripMenuItemSetOperationFalse": // 休車
                    try {
                        ((SetLabel)_contextMenuStripExOpendControl).OperationFlag = false;                                      // SetLabelのフラグをセット(ControlのViewを変化させる)
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).OperationFlag = false;          // SetControlのフラグをセット
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).VehicleDispatchFlag = false;    // SetControlのフラグをセット
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();            // SetControlの構成を再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                /*
                 * 管理地
                 */
                case "ToolStripMenuItemSetWarehouseAdachi": // 0:該当なし 1:足立 2:三郷
                    try {
                        ((SetLabel)_contextMenuStripExOpendControl).ManagedSpaceCode = 1;                                       // SetLabelのフラグをセット(ControlのViewを変化させる)
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).ManagedSpaceCode = 1;           // SetControlのフラグをセット
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();            // SetControlの構成を再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "配車先管理を足立に変更しました。");
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "ToolStripMenuItemSetWarehouseMisato": // 0:該当なし 1:足立 2:三郷
                    try {
                        ((SetLabel)_contextMenuStripExOpendControl).ManagedSpaceCode = 2;                                       // SetLabelのフラグをセット(ControlのViewを変化させる)
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).ManagedSpaceCode = 2;           // SetControlのフラグをセット
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();            // プロパティの再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "配車先管理を三郷に変更しました。");
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                /*
                 * 10:雇上 11:区契 12:臨時 20:清掃工場 30:社内 50:一般 51:社用車 99:指定なし
                 */
                case "ToolStripMenuItemClassificationYOUJYOU": // 雇上
                    try {
                        ((SetLabel)_contextMenuStripExOpendControl).ClassificationCode = 10;                                    // SetLabelのフラグをセット(ControlのViewを変化させる)
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).ClassificationCode = 10;        // SetControlのフラグをセット
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();            // プロパティの再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "雇上契約に変更しました。"));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "ToolStripMenuItemClassificationKUKEI": // 区契
                    try {
                        ((SetLabel)_contextMenuStripExOpendControl).ClassificationCode = 11;                                    // SetLabelのフラグをセット(ControlのViewを変化させる)
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).ClassificationCode = 11;        // SetControlのフラグをセット
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();            // プロパティの再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "区契約に変更しました。"));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                /*
                 * true:作業員付き false:作業員なし
                 */
                case "ToolStripMenuItemAddWorkerTrue": // 作業員付き
                    try {
                        ((SetLabel)_contextMenuStripExOpendControl).AddWorkerFlag = true;                                       // SetLabelのフラグをセット(ControlのViewを変化させる)
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).AddWorkerFlag = true;           // SetControlのフラグをセット
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();            // プロパティの再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "作付に変更しました。"));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "ToolStripMenuItemAddWorkerFalse": // 作業員なし
                    ((SetLabel)_contextMenuStripExOpendControl).AddWorkerFlag = false;                                          // SetLabelのフラグをセット(ControlのViewを変化させる)
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).AddWorkerFlag = false;              // SetControlのフラグをセット
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();                // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "作無に変更しました。"));
                    break;
                /*
                 * 0:指定なし 1:早番 2:遅番
                 */
                case "ToolStripMenuItemShiftFirst": // 早番
                    ((SetLabel)_contextMenuStripExOpendControl).ShiftCode = 1;                                                  // SetLabelのフラグをセット(ControlのViewを変化させる)
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).ShiftCode = 1;                      // SetControlのフラグをセット
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();                // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "先番に変更しました。"));
                    break;
                case "ToolStripMenuItemShiftLater": // 遅番
                    ((SetLabel)_contextMenuStripExOpendControl).ShiftCode = 2;                                                  // SetLabelのフラグをセット(ControlのViewを変化させる)
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).ShiftCode = 2;                      // SetControlのフラグをセット
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();                // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "後番に変更しました。"));
                    break;
                case "ToolStripMenuItemShiftNone": // 番手解除
                    ((SetLabel)_contextMenuStripExOpendControl).ShiftCode = 0;                                                  // SetLabelのフラグをセット(ControlのViewを変化させる)
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).ShiftCode = 0;                      // SetControlのフラグをセット
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();                // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "番手を解除しました。"));
                    break;
                /*
                 * true:待機 false:通常
                 */
                case "ToolStripMenuItemStandByTrue": // 待機
                    ((SetLabel)_contextMenuStripExOpendControl).StandByFlag = true;                                             // SetLabelのフラグをセット(ControlのViewを変化させる)
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).StandByFlag = true;                 // SetControlのフラグをセット
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();                // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "待機ありに変更しました。"));
                    break;
                case "ToolStripMenuItemStandByFalse": // 通常
                    ((SetLabel)_contextMenuStripExOpendControl).StandByFlag = false;                                            // SetLabelのフラグをセット(ControlのViewを変化させる)
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).StandByFlag = false;                // SetControlのフラグをセット
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();                // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "待機なしに変更しました。"));
                    break;
                /*
                 * true:連絡事項あり false:連絡事項なし
                 */
                case "ToolStripMenuItemContactInformationTrue": //
                    ((SetLabel)_contextMenuStripExOpendControl).ContactInfomationFlag = true;                                   // SetLabelのフラグをセット(ControlのViewを変化させる)
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).ContactInfomationFlag = true;       // SetControlのフラグをセット
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();                // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "連絡事項ありに変更しました。"));
                    break;
                case "ToolStripMenuItemContactInformationFalse": //
                    ((SetLabel)_contextMenuStripExOpendControl).ContactInfomationFlag = false;                                  // SetLabelのフラグをセット(ControlのViewを変化させる)
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).ContactInfomationFlag = false;      // SetControlのフラグをセット
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();                // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "連絡事項なしに変更しました。"));
                    break;
                /*
                 * メモを作成・編集する
                 */
                case "ToolStripMenuItemSetMemo": //
                    Memo setMemo = new(_connectionVo, _contextMenuStripExOpendControl);
                    _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), setMemo);
                    setMemo.ShowDialog(this);
                    break;
                /*
                 * 代車・代番のFAX確認
                 */
                case "ToolStripMenuItemFaxInformationTrue": //
                    ((SetLabel)_contextMenuStripExOpendControl).FaxTransmissionFlag = true;                                     // SetLabelのフラグをセット(ControlのViewを変化させる)
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).FaxTransmissionFlag = true;         // SetControlのフラグをセット
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();                // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "FAX送信予約を設定しました。"));
                    break;
                case "ToolStripMenuItemFaxInformationFalse": //
                    ((SetLabel)_contextMenuStripExOpendControl).FaxTransmissionFlag = false;                                    // SetLabelのフラグをセット(ControlのViewを変化させる)
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).FaxTransmissionFlag = false;        // SetControlのフラグをセット
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();                // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "FAX送信予約を解除しました。"));
                    break;
                /*
                 * FAXを送信する
                 */
                case "ToolStripMenuItemCreateFax": //
                    SubstituteSheet substituteSheet = new(_connectionVo, (SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl);
                    _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), substituteSheet);
                    substituteSheet.ShowDialog(this);
                    break;
                /*
                 * 削除
                 */
                case "ToolStripMenuItemSetDelete": //
                    MessageBox.Show("ToolStripMenuItemSetDelete");
                    break;
                /*
                 * プロパティ
                 */
                case "ToolStripMenuItemSetProperty": //
                    MessageBox.Show("ToolStripMenuItemSetProperty");
                    break;

                /*
                 * 
                 * ---------- CarLabel ----------
                 * 
                 */
                /*
                 * 管理地
                 */
                case "ToolStripMenuItemCarWarehouseAdachi": // 0:該当なし 1:足立 2:三郷
                    try {
                        ((CarLabel)_contextMenuStripExOpendControl).CarGarageCode = 1;
                        ((SetControl)((CarLabel)_contextMenuStripExOpendControl).ParentControl).CarGarageCode = 1;              // SetControlのフラグをセット
                        ((SetControl)((CarLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();            // プロパティの再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((CarLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((CarLabel)_contextMenuStripExOpendControl).CarMasterVo.RegistrationNumber, "の管理車庫を足立に変更しました。"));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "ToolStripMenuItemCarWarehouseMisato": // 0:該当なし 1:足立 2:三郷
                    try {
                        ((CarLabel)_contextMenuStripExOpendControl).CarGarageCode = 2;
                        ((SetControl)((CarLabel)_contextMenuStripExOpendControl).ParentControl).CarGarageCode = 2;              // SetControlのフラグをセット
                        ((SetControl)((CarLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();            // プロパティの再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((CarLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((CarLabel)_contextMenuStripExOpendControl).CarMasterVo.RegistrationNumber, "の管理車庫を三郷に変更しました。"));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                /*
                 * 代車処理
                 */
                case "ToolStripMenuItemCarProxyTrue": // true:代車 false:なし
                    try {
                        ((CarLabel)_contextMenuStripExOpendControl).ProxyFlag = true;
                        ((SetControl)((CarLabel)_contextMenuStripExOpendControl).ParentControl).CarProxyFlag = true;            // SetControlのフラグをセット
                        ((SetControl)((CarLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();            // プロパティの再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((CarLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((CarLabel)_contextMenuStripExOpendControl).CarMasterVo.RegistrationNumber, "を代車に設定しました。"));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "ToolStripMenuItemCarProxyFalse": // true:代車 false:なし
                    try {
                        ((CarLabel)_contextMenuStripExOpendControl).ProxyFlag = false;
                        ((SetControl)((CarLabel)_contextMenuStripExOpendControl).ParentControl).CarProxyFlag = false;           // SetControlのフラグをセット
                        ((SetControl)((CarLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();            // プロパティの再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((CarLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((CarLabel)_contextMenuStripExOpendControl).CarMasterVo.RegistrationNumber, "の代車を解除しました。"));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                /*
                 * メモを作成・編集する
                 */
                case "ToolStripMenuItemCarMemo": //
                    Memo carMemo = new(_connectionVo, _contextMenuStripExOpendControl);
                    _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), carMemo);
                    carMemo.ShowDialog(this);
                    break;
                /*
                 * 
                 * ---------- StaffLabel ----------
                 * 
                 */
                // 従事者台帳
                case "ToolStripMenuItemStaffPaper":
                    StaffPaper staffPaper = new(_connectionVo, ((StaffLabel)_contextMenuStripExOpendControl).StaffMasterVo.StaffCode);
                    _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), staffPaper);
                    staffPaper.ShowDialog(this);
                    break;
                case "ToolStripMenuItemStaffLicense":
                    LicenseCard licenseCard = new(_connectionVo, ((StaffLabel)_contextMenuStripExOpendControl).StaffMasterVo.StaffCode);
                    _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), licenseCard);
                    licenseCard.ShowDialog(this);
                    break;
                // 代番
                case "ToolStripMenuItemStaffProxyTrue":
                    ((StaffLabel)_contextMenuStripExOpendControl).ProxyFlag = true;
                    try {
                        switch (((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).GetStaffNumber((StaffLabel)_contextMenuStripExOpendControl)) {
                            case 0:
                                ((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).StaffProxyFlag1 = true;   // SetControlのフラグをセット
                                break;
                            case 1:
                                ((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).StaffProxyFlag2 = true;   // SetControlのフラグをセット
                                break;
                            case 2:
                                ((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).StaffProxyFlag3 = true;   // SetControlのフラグをセット
                                break;
                            case 3:
                                ((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).StaffProxyFlag4 = true;   // SetControlのフラグをセット
                                break;
                        }
                        ((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();              // プロパティの再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((StaffLabel)_contextMenuStripExOpendControl).StaffMasterVo.DisplayName, "を代番に設定しました。"));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "ToolStripMenuItemStaffProxyFalse":
                    ((StaffLabel)_contextMenuStripExOpendControl).ProxyFlag = false;
                    try {
                        switch (((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).GetStaffNumber((StaffLabel)_contextMenuStripExOpendControl)) {
                            case 0:
                                ((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).StaffProxyFlag1 = false;   // SetControlのフラグをセット
                                break;
                            case 1:
                                ((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).StaffProxyFlag2 = false;   // SetControlのフラグをセット
                                break;
                            case 2:
                                ((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).StaffProxyFlag3 = false;   // SetControlのフラグをセット
                                break;
                            case 3:
                                ((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).StaffProxyFlag4 = false;   // SetControlのフラグをセット
                                break;
                        }
                        ((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();              // プロパティの再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((StaffLabel)_contextMenuStripExOpendControl).StaffMasterVo.DisplayName, "の代番を解除しました。"));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                // 職種
                case "ToolStripMenuItemStaffOccupation10":
                    ((StaffLabel)_contextMenuStripExOpendControl).OccupationCode = 10;
                    try {
                        switch (((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).GetStaffNumber((StaffLabel)_contextMenuStripExOpendControl)) {
                            case 0:
                                ((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).StaffOccupation1 = 10;    // SetControlのフラグをセット
                                break;
                            case 1:
                                ((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).StaffOccupation2 = 10;    // SetControlのフラグをセット
                                break;
                            case 2:
                                ((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).StaffOccupation3 = 10;    // SetControlのフラグをセット
                                break;
                            case 3:
                                ((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).StaffOccupation4 = 10;    // SetControlのフラグをセット
                                break;
                        }
                        ((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();              // プロパティの再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((StaffLabel)_contextMenuStripExOpendControl).StaffMasterVo.DisplayName, "を運転手として登録しました。"));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "ToolStripMenuItemStaffOccupation11":
                    ((StaffLabel)_contextMenuStripExOpendControl).OccupationCode = 11;
                    try {
                        switch (((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).GetStaffNumber((StaffLabel)_contextMenuStripExOpendControl)) {
                            case 0:
                                ((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).StaffOccupation1 = 11;    // SetControlのフラグをセット
                                break;
                            case 1:
                                ((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).StaffOccupation2 = 11;    // SetControlのフラグをセット
                                break;
                            case 2:
                                ((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).StaffOccupation3 = 11;    // SetControlのフラグをセット
                                break;
                            case 3:
                                ((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).StaffOccupation4 = 11;    // SetControlのフラグをセット
                                break;
                        }
                        ((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).DefaultRelocation();              // プロパティの再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((StaffLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((StaffLabel)_contextMenuStripExOpendControl).StaffMasterVo.DisplayName, "を作業員として登録しました。"));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                // メモを作成・編集する
                case "ToolStripMenuItemStaffMemo": //
                    Memo staffMemo = new(_connectionVo, _contextMenuStripExOpendControl);
                    _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), staffMemo);
                    staffMemo.ShowDialog(this);
                    break;
                default:
                    MessageBox.Show("ToolStripMenuItem_Click");
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellNumber">選択されたCellNumber</param>
        /// <param name="endCellNumber">１段目:43 2段目:93 3段目:143 4段目:193</param>
        private void SetControlChengeSingle(int cellNumber, int endCellNumber) {
            try {
                // ①対象SetControlのプロパティを変更する(Singleに変更する)
                VehicleDispatchDetailVo vehicleDispatchDetailVo = this._vehicleDispatchDetailDao.SelectOneVehicleDispatchDetail(cellNumber, this.DateTimePickerExOperationDate.GetDate());
                vehicleDispatchDetailVo.PurposeFlag = false;
                this._vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(cellNumber, vehicleDispatchDetailVo);
                Debug.WriteLine(string.Concat("CellNumber = ", cellNumber, " のPurposeFlagを'False'に変更しました"));

                // ②各CellNumberをインクリメントする
                for (int i = cellNumber + 2; i <= endCellNumber; i++) { // DoubleをSingleに変えるんだから+2からスタートする
                    if (_vehicleDispatchDetailDao.ExistenceEmploymentAgreement(i, this.DateTimePickerExOperationDate.GetDate())) { // この先にDoubleがあったらCellNumberがズレるから存在確認が必要
                        vehicleDispatchDetailVo = _vehicleDispatchDetailDao.SelectOneVehicleDispatchDetail(i, this.DateTimePickerExOperationDate.GetDate());
                        vehicleDispatchDetailVo.CellNumber--;
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(i, vehicleDispatchDetailVo);
                        Debug.WriteLine(string.Concat("CellNumber = ", cellNumber, " をCellNumber = ", vehicleDispatchDetailVo.CellNumber, " に変更しました"));
                    } else {
                        continue;
                    }
                }

                // ③末尾に空のVehicleDispatchDetailVoを追加する
                VehicleDispatchDetailVo defaultVehicleDispatchDetailVo = new();
                defaultVehicleDispatchDetailVo.CellNumber = endCellNumber;
                defaultVehicleDispatchDetailVo.OperationDate = this.DateTimePickerExOperationDate.GetDate();
                _vehicleDispatchDetailDao.InsertOneVehicleDispatchDetail(defaultVehicleDispatchDetailVo);
                Debug.WriteLine(string.Concat("CellNumber = ", defaultVehicleDispatchDetailVo.CellNumber, " に空のレコードををINSERTしました"));
            } catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellNumber">選択されたCellNumber</param>
        /// <param name="endCellNumber">１段目:43 2段目:93 3段目:143 4段目:193</param>
        private void SetControlChengeDouble(int cellNumber, int endCellNumber) {
            try {
                // ①CellNumber93を削除する
                VehicleDispatchDetailVo defaultVehicleDispatchDetailVo = new();
                if (_vehicleDispatchDetailDao.DeleteOneVehicleDispatchDetail(endCellNumber, this.DateTimePickerExOperationDate.GetDate()) == 0) {
                    MessageBox.Show("CellNumber93番を削除できません。処理を中断します。");
                    return;
                }

                // ②各CellNumberをデクリメントする
                for (int i = endCellNumber - 1; i >= cellNumber + 1; i--) {
                    if (_vehicleDispatchDetailDao.ExistenceEmploymentAgreement(i, this.DateTimePickerExOperationDate.GetDate())) { // この先にDoubleがあったらCellNumberが飛ぶから存在確認が必要
                        defaultVehicleDispatchDetailVo = _vehicleDispatchDetailDao.SelectOneVehicleDispatchDetail(i, this.DateTimePickerExOperationDate.GetDate());
                        defaultVehicleDispatchDetailVo.CellNumber++;
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(i, defaultVehicleDispatchDetailVo);
                    } else {
                        continue;
                    }
                }

                // ③対象SetControlのプロパティを変更する(Doubleに変更する)
                VehicleDispatchDetailVo targetVehicleDispatchDetailVo = _vehicleDispatchDetailDao.SelectOneVehicleDispatchDetail(cellNumber, this.DateTimePickerExOperationDate.GetDate());
                targetVehicleDispatchDetailVo.CellNumber = cellNumber;
                targetVehicleDispatchDetailVo.PurposeFlag = true;
                _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(cellNumber, targetVehicleDispatchDetailVo);
                Debug.WriteLine(string.Concat("CellNumber = ", cellNumber, " のPurposeFlagを'True'に変更しました"));
            } catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// SetControlの形状をDoubleに変更する
        /// </summary>
        private void SetControlChengeDouble() {
            switch (((SetControl)_contextMenuStripExOpendControl).CellNumber) {
                case int cellNumber when cellNumber >= 0 && cellNumber < 43:

                    break;
                case int targetCellNumber when targetCellNumber >= 50 && targetCellNumber < 93:
                    try {
                        // ①CellNumber93を削除する
                        VehicleDispatchDetailVo blankVehicleDispatchDetailVo = new();
                        if (_vehicleDispatchDetailDao.DeleteOneVehicleDispatchDetail(93, this.DateTimePickerExOperationDate.GetDate()) == 0) {
                            MessageBox.Show("CellNumber93番を削除できません。処理を中断します。");
                            break;
                        }

                        // ②各CellNumberをデクリメントする
                        for (int i = 92; i >= targetCellNumber + 1; i--) {
                            if (_vehicleDispatchDetailDao.ExistenceEmploymentAgreement(i, this.DateTimePickerExOperationDate.GetDate())) { // この先にDoubleがあったらCellNumberが飛ぶから存在確認が必要
                                blankVehicleDispatchDetailVo = _vehicleDispatchDetailDao.SelectOneVehicleDispatchDetail(i, this.DateTimePickerExOperationDate.GetDate());
                                blankVehicleDispatchDetailVo.CellNumber++;
                                _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(i, blankVehicleDispatchDetailVo);
                            } else {
                                continue;
                            }
                        }

                        // ③対象SetControlのプロパティを変更する(Doubleに変更する)
                        VehicleDispatchDetailVo targetVehicleDispatchDetailVo = _vehicleDispatchDetailDao.SelectOneVehicleDispatchDetail(targetCellNumber, this.DateTimePickerExOperationDate.GetDate());
                        targetVehicleDispatchDetailVo.CellNumber = targetCellNumber;
                        targetVehicleDispatchDetailVo.PurposeFlag = true;
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(targetCellNumber, targetVehicleDispatchDetailVo);
                        Debug.WriteLine(string.Concat("CellNumber=", targetCellNumber, "のPurposeFlagをTrueに変更しました"));
                    } catch (Exception e) {
                        MessageBox.Show(e.Message);
                    }
                    break;
                case int cellNumber when cellNumber >= 100 && cellNumber < 143:

                    break;
                case int cellNumber when cellNumber >= 150 && cellNumber < 193:

                    break;
            }
        }

        /// <summary>
        /// DragされているControl
        /// </summary>
        private Control _dragEnterObject;
        /// <summary>
        /// オブジェクトがコントロールの境界内にドラッグされると一度だけ発生します。
        /// </summary>
        /// <param name="sender">これを呼出したSetControlが入っている</param>
        /// <param name="e"></param>
        private void OnDragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(typeof(SetLabel))) {
                _dragEnterObject = (SetLabel)e.Data.GetData(typeof(SetLabel));
            } else if (e.Data.GetDataPresent(typeof(CarLabel))) {
                _dragEnterObject = (CarLabel)e.Data.GetData(typeof(CarLabel));
            } else if (e.Data.GetDataPresent(typeof(StaffLabel))) {
                _dragEnterObject = (StaffLabel)e.Data.GetData(typeof(StaffLabel));
            }
        }

        /// <summary>
        /// ドラッグ アンド ドロップ操作中にマウス カーソルがコントロールの境界内を移動したときに発生します。
        /// Copy  :データがドロップ先にコピーされようとしている状態
        /// Move  :データがドロップ先に移動されようとしている状態
        /// Scroll:データによってドロップ先でスクロールが開始されようとしている状態、あるいは現在スクロール中である状態
        /// All   :上の3つを組み合わせたもの
        /// Link  :データのリンクがドロップ先に作成されようとしている状態
        ///  None  :いかなるデータもドロップ先が受け付けようとしない状態
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDragOver(object sender, DragEventArgs e) {

        }

        /// <summary>
        /// ObjectがDropされると発生します。
        /// </summary>
        /// <param name="sender">DropされたSetControlが入っている</param>
        /// <param name="e"></param>
        private void OnDragDrop(object sender, DragEventArgs e) {
            /*
             * SetControl上のセルの位置を取得する
             */
            Point clientPoint = ((SetControl)sender).PointToClient(new Point(e.X, e.Y));
            Point cellPoint = new(clientPoint.X / (int)((SetControl)sender).CellWidth, clientPoint.Y / (int)((SetControl)sender).CellHeight);
            /*
             * DropされたObjectのParentによって処理を分岐する
             */
            switch (((SetControl)sender).DragParentControl) {
                case SetControl dragParentControl:
                    /*
                     * Drop処理
                     * Controlを追加する
                     */
                    switch (((SetControl)sender).DragControl) {
                        case SetLabel setLabel:
                            ((SetControl)sender).Controls.Add(setLabel, cellPoint.X, cellPoint.Y);                                                              // SetLabelを移動する
                            /*
                             * Drag側のSetControlの処理
                             */
                            try {
                                dragParentControl.SetControlRelocation();                                                                                       // プロパティの再構築
                                int updateCount = _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(dragParentControl.GetVehicleDispatchDetailVo());     // DragParentControlのVehicleDispatchDetailVoを取得　SQL発行
                                dragParentControl.EventDel(setLabel);                                                                                           // Eventを削除
                            } catch (Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                            /*
                             * Dropデータの処理
                             */
                            try {
                                ((SetControl)sender).SetControlRelocation((SetControl)sender);                                                                  // プロパティの再構築
                                int updateCount = _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)sender).GetVehicleDispatchDetailVo());  // thisのVehicleDispatchDetailVoを取得　SQL発行
                                ((SetControl)sender).EventAdd(setLabel);                                                                                        // Eventを追加
                            } catch (Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                            break;
                        case CarLabel carLabel:
                            ((SetControl)sender).Controls.Add(carLabel, cellPoint.X, cellPoint.Y);                                                              // CarLabelを移動する
                            /*
                             * Drag側のSetControlの処理
                             */
                            try {
                                dragParentControl.DefaultRelocation();                                                                                          // プロパティの再構築
                                int updateCount = _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(dragParentControl.GetVehicleDispatchDetailVo());     // DragParentControlのVehicleDispatchDetailVoを取得　SQL発行
                                dragParentControl.EventDel(carLabel);                                                                                           // Eventを削除
                                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(updateCount, "UpdateSuccess");
                            } catch (Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                            /*
                             * Dropデータの処理
                             */
                            try {
                                ((SetControl)sender).DefaultRelocation();                                                                                       // プロパティの再構築
                                int updateCount = _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)sender).GetVehicleDispatchDetailVo());  // thisのVehicleDispatchDetailVoを取得　SQL発行
                                ((SetControl)sender).EventAdd(carLabel);                                                                                        // Eventを追加
                                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(updateCount, " CarLabel UpdateSuccess");
                            } catch (Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                            break;
                        case StaffLabel staffLabel:
                            ((SetControl)sender).Controls.Add(staffLabel, cellPoint.X, cellPoint.Y);                                                            // StaffLabelを移動する
                            /*
                             * Drag側のSetControlの処理
                             */
                            try {
                                dragParentControl.DefaultRelocation();                                                                                          // プロパティの再構築
                                int updateCount = _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(dragParentControl.GetVehicleDispatchDetailVo());     // DragParentControlのVehicleDispatchDetailVoを取得　SQL発行
                                dragParentControl.EventDel(staffLabel);                                                                                         // Eventを削除
                                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(updateCount, "UpdateSuccess");
                            } catch (Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                            /*
                             * Dropデータの処理
                             */
                            try {
                                ((SetControl)sender).DefaultRelocation();                                                                                       // プロパティの再構築
                                int updateCount = _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)sender).GetVehicleDispatchDetailVo());  // thisのVehicleDispatchDetailVoを取得　SQL発行
                                ((SetControl)sender).EventAdd(staffLabel);                                                                                      // Eventを追加
                                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(updateCount, " StaffLabel UpdateSuccess");
                            } catch (Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                            break;
                    }
                    Debug.WriteLine(string.Concat(dragParentControl.CellNumber, " → ", ((SetControl)sender).CellNumber, "の", cellPoint.X, ",", cellPoint.Y));
                    break;
                case StockBoxPanel stockBoxPanel:
                    switch (this._dragEnterObject) {
                        case SetLabel setLabel:
                            ((SetControl)sender).Controls.Add(setLabel, cellPoint.X, cellPoint.Y);                                                              // SetLabelを移動する
                            ((SetControl)setLabel.ParentControl).OperationFlag = true;                                                                          // SetControlのフラグをセット
                            ((SetControl)setLabel.ParentControl).VehicleDispatchFlag = true;                                                                    // SetControlのフラグをセット
                            ((SetControl)setLabel.ParentControl).PurposeFlag = false;                                                                           // SetControlのフラグをセット
                            /*
                             * Dropデータの処理
                             */
                            try {
                                ((SetControl)sender).DefaultRelocation();                                                                                       // プロパティの再構築
                                int updateCount = _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)sender).GetVehicleDispatchDetailVo());  // thisのVehicleDispatchDetailVoを取得　SQL発行
                                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(updateCount, " SetLabel UpdateSuccess");
                            } catch (Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                            break;
                        case CarLabel carLabel:
                            ((SetControl)sender).Controls.Add(carLabel, cellPoint.X, cellPoint.Y);                                                              // CarLabelを移動する
                            /*
                             * Dropデータの処理
                             */
                            try {
                                ((SetControl)sender).DefaultRelocation();                                                                                       // プロパティの再構築
                                int updateCount = _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)sender).GetVehicleDispatchDetailVo());  // thisのVehicleDispatchDetailVoを取得　SQL発行
                                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(updateCount, " CarLabel UpdateSuccess");
                            } catch (Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                            break;
                        case StaffLabel staffLabel:
                            ((SetControl)sender).Controls.Add(staffLabel, cellPoint.X, cellPoint.Y);                                                            // StaffLabelを移動する
                            /*
                             * Dropデータの処理
                             */
                            try {
                                ((SetControl)sender).DefaultRelocation();                                                                                       // プロパティの再構築
                                int updateCount = _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)sender).GetVehicleDispatchDetailVo());  // thisのVehicleDispatchDetailVoを取得　SQL発行
                                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(updateCount, " StaffLabel UpdateSuccess");
                            } catch (Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// Ctrl + 左クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseClick(object sender, MouseEventArgs e) {
            switch (sender) {
                case SetLabel setLabel:
                    MessageBox.Show("SetLabel-OnMouseClick");
                    break;
                case CarLabel carLabel:
                    MessageBox.Show("CarLabel-OnMouseClick");
                    break;
                case StaffLabel staffLabel:
                    /*
                     * 出庫時点呼
                     */
                    if (staffLabel.RollCallFlag) {
                        DialogResult dialogResult = MessageBox.Show(string.Concat(staffLabel.StaffMasterVo.DisplayName, "の出庫点呼を未実施に戻しますか？"), "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.OK) {
                            try {
                                staffLabel.RollCallFlag = false;
                                ((SetControl)staffLabel.ParentControl).DefaultRelocation(); // プロパティの再構築
                                _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)staffLabel.ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(staffLabel.StaffMasterVo.DisplayName, "の点呼を解除しました。"));
                            } catch (Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                        } else {
                            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat("操作をキャンセルしました。");
                        }
                    } else {
                        try {
                            staffLabel.RollCallFlag = true;
                            ((SetControl)staffLabel.ParentControl).DefaultRelocation(); // プロパティの再構築
                            _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)staffLabel.ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(staffLabel.StaffMasterVo.DisplayName, "の点呼を記録しました。"));
                        } catch (Exception exception) {
                            MessageBox.Show(exception.Message);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDoubleClick(object sender, MouseEventArgs e) {
            switch (sender) {
                case SetLabel setLabel:
                    /*
                     * 帰庫点呼
                     */
                    ((SetControl)setLabel.ParentControl).DefaultRelocation(); // プロパティの再構築
                    LastRollCall lastRollCall = new(_connectionVo, (SetControl)setLabel.ParentControl);
                    _screenForm.SetPosition(Screen.FromPoint(Cursor.Position), lastRollCall);
                    lastRollCall.ShowDialog(this);
                    Debug.WriteLine("SetLabel-OnMouseDoubleClick");
                    break;
                case CarLabel carLabel:
                    Debug.WriteLine("CarLabel-OnMouseDoubleClick");
                    break;
                case StaffLabel staffLabel:
                    Debug.WriteLine("StaffLabel-OnMouseDoubleClick");
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                switch (sender) {
                    case SetLabel setLabel:
                        this.DragParentControl = (SetControl)setLabel.Parent; // Dragラベルが格納されているSetControlを退避する
                        if (setLabel.SetMasterVo.MoveFlag) {
                            setLabel.DoDragDrop(sender, DragDropEffects.Move);
                        } else {
                            setLabel.DoDragDrop(sender, DragDropEffects.None);
                        }
                        break;
                    case CarLabel carLabel:
                        this.DragParentControl = (SetControl)carLabel.Parent; // Dragラベルが格納されているSetControlを退避する
                        carLabel.DoDragDrop(sender, DragDropEffects.Move);
                        break;
                    case StaffLabel staffLabel:
                        this.DragParentControl = (SetControl)staffLabel.Parent; // Dragラベルが格納されているSetControlを退避する
                        staffLabel.DoDragDrop(sender, DragDropEffects.Move);
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseEnter(object sender, EventArgs e) {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseLeave(object sender, EventArgs e) {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseMove(object sender, MouseEventArgs e) {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseUp(object sender, MouseEventArgs e) {

        }

        /*
         * プロパティ
         */
        /// <summary>
        /// Drag Dropを開始した時のSetControlを格納する
        /// </summary>
        public SetControl DragParentControl {
            get => this._dragParentControl;
            set => this._dragParentControl = value;
        }
    }
}
