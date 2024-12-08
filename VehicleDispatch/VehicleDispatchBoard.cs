﻿/*
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

        /*
         * Event処理
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStripEx_Opened(object sender, EventArgs e) {
            // SetControl
            // SetLabel
            // CarLabel
            // StaffLabel
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                /*
                 * SetControl
                 */
                case "ToolStripMenuItemSetControlSingle": // SetControl Single
                    MessageBox.Show("ToolStripMenuItemSetControlSingle");
                    break;
                case "ToolStripMenuItemSetControlDouble": // SetControl Double
                    MessageBox.Show("ToolStripMenuItemSetControlDouble");
                    break;

                /*
                 * SetLabel
                 */

                /*
                 * CarLabel
                 */

                /*
                 * StaffLabel
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
                    /*
                     * 帰庫点呼
                     */
                    MessageBox.Show("帰庫点呼記録を実装する");
                    break;
                case CarLabel carLabel:
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
                case SetLabel:
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
