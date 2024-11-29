/*
 * 2024/10/03
 */
using System.Diagnostics;

using ControlEx;

using Dao;

using Vo;

namespace VehicleDispatch {
    public partial class VehicleDispatchBoard : Form {
        /*
         * プロパティ
         */
        private SetControl _dragParentSetControl;
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
            DateTimePickerExOperationDate.SetToday();
            this.AddBoard();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEx_Click(object sender, EventArgs e) {
            switch (((ButtonEx)sender).Name) {
                case "ButtonExUpdate":
                    try {
                        AddControls(_vehicleDispatchDetailDao.SelectAllVehicleDispatchDetail(DateTimePickerExOperationDate.GetDate()));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
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
            for (int y = 0; y < _board.RowAllNumber; y++) {
                switch (y) {
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
        /// ConvertStaffMasterVo
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
        private StaffMasterVo GetStaffMasterVo(int staffCode) {
            StaffMasterVo staffMasterVo = _listStaffMasterVo.Find(x => x.StaffCode == staffCode);
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
        private class SetControVo {
            SetLabel? _setLabel = null;
            CarLabel? _carLabel = null;
            StaffLabel? _staffLabel0 = null;
            StaffLabel? _staffLabel1 = null;
            StaffLabel? _staffLabel2 = null;
            StaffLabel? _staffLabel3 = null;

            public SetLabel? SetLabel {
                get => this._setLabel;
                set => this._setLabel = value;
            }
            public CarLabel? CarLabel {
                get => this._carLabel;
                set => this._carLabel = value;
            }
            public StaffLabel? StaffLabel0 {
                get => this._staffLabel0;
                set => this._staffLabel0 = value;
            }
            public StaffLabel? StaffLabel1 {
                get => this._staffLabel1;
                set => this._staffLabel1 = value;
            }
            public StaffLabel? StaffLabel2 {
                get => this._staffLabel2;
                set => this._staffLabel2 = value;
            }
            public StaffLabel? StaffLabel3 {
                get => this._staffLabel3;
                set => this._staffLabel3 = value;
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

        /*
         * Event処理
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStripEx_Opened(object sender, EventArgs e) {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        /// <summary>
        /// ObjectがDropされると発生します。
        /// </summary>
        /// <param name="sender">DropされたSetControlが入っている</param>
        /// <param name="e"></param>
        private void OnDragDrop(object sender, DragEventArgs e) {
            Debug.WriteLine("VehicleDispatchBoard DragDrop");

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
                            ((SetControl)sender).Controls.Add((SetLabel)((SetControl)sender).DragControl, cellPoint.X, cellPoint.Y);
                            break;
                        case CarLabel carLabel:
                            ((SetControl)sender).Controls.Add((CarLabel)((SetControl)sender).DragControl, cellPoint.X, cellPoint.Y);
                            break;
                        case StaffLabel staffLabel:
                            ((SetControl)sender).Controls.Add((StaffLabel)((SetControl)sender).DragControl, cellPoint.X, cellPoint.Y);
                            break;
                    }

                    Debug.WriteLine(string.Concat(cellPoint.X, "  ", cellPoint.Y));
                    Debug.WriteLine(string.Concat(dragParentControl.CellNumber, " → ", ((SetControl)sender).CellNumber, "の", cellPoint.X, ",", cellPoint.Y));
                    break;
                case StockBoxtPanel stockBoxPanel:
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseClick(object sender, MouseEventArgs e) {
            switch (sender) {
                case SetLabel:
                    break;
                case CarLabel:
                    break;
                case StaffLabel:
                    /*
                     * 出庫時点呼
                     */
                    if (((StaffLabel)sender).RollCallFlag) {
                        DialogResult dialogResult = MessageBox.Show("出庫点呼を未実施に戻しますか？", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        switch (dialogResult) {
                            case DialogResult.OK:
                                ((StaffLabel)sender).RollCallFlag = false;
                                break;
                            case DialogResult.Cancel:
                                ((StaffLabel)sender).RollCallFlag = true;
                                break;
                        }
                    } else {
                        ((StaffLabel)sender).RollCallFlag = true;
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
                case SetLabel:
                    MessageBox.Show("帰庫点呼記録を実装する");
                    break;
                case CarLabel:
                    break;
                case StaffLabel:
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
                        this.DragParentSetControl = (SetControl)control.Parent; // Dragラベルが格納されているSetControlを退避する
                        break;
                    case CarLabel control:
                        control.DoDragDrop(sender, DragDropEffects.Move);
                        this.DragParentSetControl = (SetControl)control.Parent; // Dragラベルが格納されているSetControlを退避する
                        break;
                    case StaffLabel control:
                        control.DoDragDrop(sender, DragDropEffects.Move);
                        this.DragParentSetControl = (SetControl)control.Parent; // Dragラベルが格納されているSetControlを退避する
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
        public SetControl DragParentSetControl {
            get => this._dragParentSetControl;
            set => this._dragParentSetControl = value;
        }
    }
}
