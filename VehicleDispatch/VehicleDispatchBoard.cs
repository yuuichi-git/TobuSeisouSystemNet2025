/*
 * 2024/10/03
 */
using System.Diagnostics;

using Common;

using ControlEx;

using Dao;

using RollCall;

using StockBox;

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
            this.DateTimePickerExOperationDate.SetToday();
            // ControlButtonを初期化
            this.ButtonExStockBoxOpen.SetTextDirectionVertical = "Stock-Boxs";
            this.AddBoard();
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "InitializeSuccess";
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
                        stockBoxs = new(_connectionVo);
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
                case SetLabel setLabel:
                    _contextMenuStripExOpendControl = setLabel;
                    break;
                case CarLabel carLabel:
                    _contextMenuStripExOpendControl = carLabel;
                    break;
                case StaffLabel staffLabel:
                    _contextMenuStripExOpendControl = staffLabel;
                    break;
            }
            // SetControl
            // SetLabel
            // CarLabel
            // StaffLabel
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
                 * SetControl
                 * 
                 */
                case "ToolStripMenuItemSetControlSingle": // SetControl Single
                    MessageBox.Show("ToolStripMenuItemSetControlSingle");
                    break;
                case "ToolStripMenuItemSetControlDouble": // SetControl Double
                    MessageBox.Show("ToolStripMenuItemSetControlDouble");
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
                    MessageBox.Show("ToolStripMenuItemDriversReport");
                    break;
                /*
                 * 稼働・休車
                 */
                case "ToolStripMenuItemSetOperationTrue": // 稼働
                    try {
                        ((SetLabel)_contextMenuStripExOpendControl).OperationFlag = true;
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).SetControlRelocation(); // プロパティの再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "OperationFlagを変更しました。"));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "ToolStripMenuItemSetOperationFalse": // 休車
                    try {
                        ((SetLabel)_contextMenuStripExOpendControl).OperationFlag = false;
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).SetControlRelocation(); // プロパティの再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "OperationFlagを変更しました。"));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                /*
                 * 管理地
                 */
                case "ToolStripMenuItemSetWarehouseAdachi": // 0:該当なし 1:足立 2:三郷
                    try {
                        ((SetLabel)_contextMenuStripExOpendControl).ManagedSpaceCode = 1;
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).SetControlRelocation(); // プロパティの再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "配車先管理を足立に変更しました。"));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "ToolStripMenuItemSetWarehouseMisato": // 0:該当なし 1:足立 2:三郷
                    try {
                        ((SetLabel)_contextMenuStripExOpendControl).ManagedSpaceCode = 2;
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).SetControlRelocation(); // プロパティの再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "配車先管理を三郷に変更しました。"));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                /*
                 * 10:雇上 11:区契 12:臨時 20:清掃工場 30:社内 50:一般 51:社用車 99:指定なし
                 */
                case "ToolStripMenuItemClassificationYOUJYOU": // 雇上
                    try {
                        ((SetLabel)_contextMenuStripExOpendControl).ClassificationCode = 10;
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).SetControlRelocation(); // プロパティの再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "雇上契約に変更しました。"));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "ToolStripMenuItemClassificationKUKEI": // 区契
                    try {
                        ((SetLabel)_contextMenuStripExOpendControl).ClassificationCode = 11;
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).SetControlRelocation(); // プロパティの再構築
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
                        ((SetLabel)_contextMenuStripExOpendControl).AddWorkerFlag = true;
                        ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).SetControlRelocation(); // プロパティの再構築
                        _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                        this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "作付に変更しました。"));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "ToolStripMenuItemAddWorkerFalse": // 作業員なし
                    ((SetLabel)_contextMenuStripExOpendControl).AddWorkerFlag = false;
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).SetControlRelocation(); // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "作無に変更しました。"));
                    break;
                /*
                 * 0:指定なし 1:早番 2:遅番
                 */
                case "ToolStripMenuItemShiftFirst": // 早番
                    ((SetLabel)_contextMenuStripExOpendControl).ShiftCode = 1;
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).SetControlRelocation(); // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "先番に変更しました。"));
                    break;
                case "ToolStripMenuItemShiftLater": // 遅番
                    ((SetLabel)_contextMenuStripExOpendControl).ShiftCode = 2;
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).SetControlRelocation(); // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "後番に変更しました。"));
                    break;
                case "ToolStripMenuItemShiftNone": // 番手解除
                    ((SetLabel)_contextMenuStripExOpendControl).ShiftCode = 0;
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).SetControlRelocation(); // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "番手を解除しました。"));
                    break;
                /*
                 * true:待機 false:通常
                 */
                case "ToolStripMenuItemStandByTrue": // 待機
                    ((SetLabel)_contextMenuStripExOpendControl).StandByFlag = true;
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).SetControlRelocation(); // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "待機ありに変更しました。"));
                    break;
                case "ToolStripMenuItemStandByFalse": // 通常
                    ((SetLabel)_contextMenuStripExOpendControl).StandByFlag = false;
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).SetControlRelocation(); // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "待機なしに変更しました。"));
                    break;
                /*
                 * true:連絡事項あり false:連絡事項なし
                 */
                case "ToolStripMenuItemContactInformationTrue": //
                    ((SetLabel)_contextMenuStripExOpendControl).ContactInfomationFlag = true;
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).SetControlRelocation(); // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "連絡事項ありに変更しました。"));
                    break;
                case "ToolStripMenuItemContactInformationFalse": //
                    ((SetLabel)_contextMenuStripExOpendControl).ContactInfomationFlag = false;
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).SetControlRelocation(); // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "連絡事項なしに変更しました。"));
                    break;
                /*
                 * メモを作成・編集する
                 */
                case "ToolStripMenuItemSetMemo": //
                    MessageBox.Show("ToolStripMenuItemSetMemo");
                    break;
                /*
                 * 代車・代番のFAX確認
                 */
                case "ToolStripMenuItemFaxInformationTrue": //
                    ((SetLabel)_contextMenuStripExOpendControl).FaxTransmissionFlag = true;
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).SetControlRelocation(); // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "FAX送信予約を設定しました。"));
                    break;
                case "ToolStripMenuItemFaxInformationFalse": //
                    ((SetLabel)_contextMenuStripExOpendControl).FaxTransmissionFlag = false;
                    ((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).SetControlRelocation(); // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)((SetLabel)_contextMenuStripExOpendControl).ParentControl).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(string.Concat(((SetLabel)_contextMenuStripExOpendControl).SetMasterVo.SetName, "FAX送信予約を解除しました。"));
                    break;
                /*
                 * FAXを送信する
                 */
                case "ToolStripMenuItemCreateFax": //
                    MessageBox.Show("ToolStripMenuItemCreateFax");
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
                 * 
                 * ---------- StaffLabel ----------
                 * 
                 */

                default:
                    MessageBox.Show("ToolStripMenuItem_Click");
                    break;
            }
        }

        /// <summary>
        /// ObjectがDropされると発生します。
        /// </summary>
        /// <param name="sender">DropされたSetControlが入っている</param>
        /// <param name="e"></param>
        private void OnDragDrop(object sender, DragEventArgs e) {
            switch (((SetControl)sender).DragParentControl) {
                case SetControl dragParentControl:
                    /*
                     * H_SetControl上のセルの位置を取得する
                     */
                    Point clientPoint = ((SetControl)sender).PointToClient(new Point(e.X, e.Y));
                    Point cellPoint = new(clientPoint.X / (int)((SetControl)sender).CellWidth, clientPoint.Y / (int)((SetControl)sender).CellHeight);
                    /*
                     * Drop処理
                     * Controlを追加する
                     */
                    switch (((SetControl)sender).DragControl) {
                        case SetLabel setLabel:
                            ((SetControl)sender).Controls.Add(setLabel, cellPoint.X, cellPoint.Y); // SetLabelを移動する
                            /*
                             * Dragデータの処理
                             */
                            try {
                                dragParentControl.SetControlRelocation(); // プロパティの再構築
                                int updateCount = _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(dragParentControl.GetVehicleDispatchDetailVo()); // DragParentControlのVehicleDispatchDetailVoを取得　SQL発行
                                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(updateCount, "UpdateSuccess");
                            } catch (Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                            /*
                             * Dropデータの処理
                             */
                            try {
                                ((SetControl)sender).SetControlRelocation(); // プロパティの再構築
                                int updateCount = _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)sender).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(updateCount, " SetLabel UpdateSuccess");
                            } catch (Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                            break;
                        case CarLabel carLabel:
                            ((SetControl)sender).Controls.Add(carLabel, cellPoint.X, cellPoint.Y); // CarLabelを移動する
                            /*
                             * Dragデータの処理
                             */
                            try {
                                dragParentControl.SetControlRelocation(); // プロパティの再構築
                                int updateCount = _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(dragParentControl.GetVehicleDispatchDetailVo()); // DragParentControlのVehicleDispatchDetailVoを取得　SQL発行
                                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(updateCount, "UpdateSuccess");
                            } catch (Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                            /*
                             * Dropデータの処理
                             */
                            try {
                                ((SetControl)sender).SetControlRelocation(); // プロパティの再構築
                                int updateCount = _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)sender).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(updateCount, " CarLabel UpdateSuccess");
                            } catch (Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                            break;
                        case StaffLabel staffLabel:
                            ((SetControl)sender).Controls.Add(staffLabel, cellPoint.X, cellPoint.Y); // StaffLabelを移動する
                            /*
                             * Dragデータの処理
                             */
                            try {
                                dragParentControl.SetControlRelocation(); // プロパティの再構築
                                int updateCount = _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(dragParentControl.GetVehicleDispatchDetailVo()); // DragParentControlのVehicleDispatchDetailVoを取得　SQL発行
                                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(updateCount, "UpdateSuccess");
                            } catch (Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                            /*
                             * Dropデータの処理
                             */
                            try {
                                ((SetControl)sender).SetControlRelocation(); // プロパティの再構築
                                int updateCount = _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)sender).GetVehicleDispatchDetailVo()); // thisのVehicleDispatchDetailVoを取得　SQL発行
                                this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(updateCount, " StaffLabel UpdateSuccess");
                            } catch (Exception exception) {
                                MessageBox.Show(exception.Message);
                            }
                            break;
                    }
                    Debug.WriteLine(string.Concat(dragParentControl.CellNumber, " → ", ((SetControl)sender).CellNumber, "の", cellPoint.X, ",", cellPoint.Y));
                    break;
                case StockBoxPanel stockBoxPanel:
                    MessageBox.Show("StockBoxtPanel");
                    break;
            }
        }

        /// <summary>
        /// オブジェクトがコントロールの境界内にドラッグされると一度だけ発生します。
        /// </summary>
        /// <param name="sender">これを呼出したSetControlが入っている</param>
        /// <param name="e"></param>
        private void OnDragEnter(object sender, DragEventArgs e) {

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
                                ((SetControl)staffLabel.ParentControl).SetControlRelocation(); // プロパティの再構築
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
                            ((SetControl)staffLabel.ParentControl).SetControlRelocation(); // プロパティの再構築
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
                    ((SetControl)setLabel.ParentControl).SetControlRelocation(); // プロパティの再構築
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
                    case SetLabel control:
                        control.DoDragDrop(sender, DragDropEffects.Move);
                        this.DragParentControl = (SetControl)control.Parent; // Dragラベルが格納されているSetControlを退避する
                        break;
                    case CarLabel control:
                        control.DoDragDrop(sender, DragDropEffects.Move);
                        this.DragParentControl = (SetControl)control.Parent; // Dragラベルが格納されているSetControlを退避する
                        break;
                    case StaffLabel control:
                        control.DoDragDrop(sender, DragDropEffects.Move);
                        this.DragParentControl = (SetControl)control.Parent; // Dragラベルが格納されているSetControlを退避する
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
