/*
 * 2024-10-10
 */
using Vo;

namespace ControlEx {

    public partial class SetControl : TableLayoutPanel {
        /*
         * デリゲート
         */
        public event EventHandler SetControl_ContextMenuStrip_Opened = delegate { };
        public event EventHandler SetControl_ToolStripMenuItem_Click = delegate { };
        public event DragEventHandler SetControl_OnDragDrop = delegate { };
        public event DragEventHandler SetControl_OnDragEnter = delegate { };
        public event DragEventHandler SetControl_OnDragOver = delegate { };
        public event MouseEventHandler SetControl_OnMouseClick = delegate { };
        public event MouseEventHandler SetControl_OnMouseDoubleClick = delegate { };
        public event MouseEventHandler SetControl_OnMouseDown = delegate { };
        public event EventHandler SetControl_OnMouseEnter = delegate { };
        public event EventHandler SetControl_OnMouseLeave = delegate { };
        public event MouseEventHandler SetControl_OnMouseMove = delegate { };
        public event MouseEventHandler SetControl_OnMouseUp = delegate { };

        private int _cellNumber;
        private SetLabel? _deployedSetLabel;
        private CarLabel? _deployedCarLabel;
        private StaffLabel? _deployedStaffLabel0;
        private StaffLabel? _deployedStaffLabel1;
        private StaffLabel? _deployedStaffLabel2;
        private StaffLabel? _deployedStaffLabel3;

        private object _dragParentControl;
        private object _dragControl;

        /*
         * Vo
         */
        private VehicleDispatchDetailVo _vehicleDispatchDetailVo;
        /*
         * Cellのサイズ
         */
        private const float _cellWidth = 74;
        private const float _cellHeight = 120;
        /*
         * Cellの数
         */
        private const int _columnCount = 1; // Columnの数
        private const int _rowCount = 4; // Rowの数

        /// <summary>
        /// コンストラクター
        /// </summary>
        public SetControl(VehicleDispatchDetailVo vehicleDispatchDetailVo) {
            /*
             * Vo
             */
            _vehicleDispatchDetailVo = vehicleDispatchDetailVo;
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.AllowDrop = true;
            this.Dock = DockStyle.Fill;
            this.Margin = new Padding(0);
            this.Name = "SetControl";
            this.Padding = new Padding(0);
            /*
             * Column作成
             */
            switch (VehicleDispatchDetailVo.PurposeFlag) {
                case true: // ２列
                    // Size
                    this.Size = new Size((int)CellWidth * _columnCount * 2, (int)CellHeight * _rowCount);
                    /*
                     * Column作成
                     */
                    this.ColumnCount = _columnCount + 1;
                    this.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, CellWidth));
                    this.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, CellWidth));
                    break;
                case false: // １列
                    // Size
                    this.Size = new Size((int)CellWidth * _columnCount, (int)CellHeight * _rowCount);
                    /*
                     * Column作成
                     */
                    this.ColumnCount = _columnCount;
                    this.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, CellWidth));
                    break;
            }
            /*
             * Row作成
             */
            this.RowCount = _rowCount;
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, CellHeight));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, CellHeight));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, CellHeight));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, CellHeight));
            /*
             * Event
             */
            this.MouseDown += OnMouseDown; // 画面スクロールに使う
            this.MouseMove += OnMouseMove; // 画面スクロールに使う
            this.MouseUp += OnMouseUp; // 画面スクロールに使う
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setMasterVo"></param>
        public void AddSetLabel(SetMasterVo setMasterVo) {
            if (setMasterVo is null)
                return;
            SetLabel setLabel = new(setMasterVo);
            setLabel.AddWorkerFlag = VehicleDispatchDetailVo.AddWorkerFlag;
            setLabel.ClassificationCode = VehicleDispatchDetailVo.ClassificationCode;
            setLabel.FaxTransmissionFlag = (setMasterVo.ContactMethod == 11 || setMasterVo.ContactMethod == 13);
            setLabel.LastRollCallFlag = VehicleDispatchDetailVo.LastRollCallFlag;
            setLabel.ManagedSpaceCode = VehicleDispatchDetailVo.ManagedSpaceCode;
            setLabel.Memo = VehicleDispatchDetailVo.SetMemo;
            setLabel.MemoFlag = VehicleDispatchDetailVo.SetMemoFlag;
            setLabel.OperationFlag = VehicleDispatchDetailVo.OperationFlag;
            setLabel.ParentControl = this;
            setLabel.ShiftCode = VehicleDispatchDetailVo.ShiftCode;
            setLabel.StandByFlag = VehicleDispatchDetailVo.StandByFlag;
            setLabel.TelCallingFlag = (setMasterVo.ContactMethod == 10 || setMasterVo.ContactMethod == 13);
            // Eventを登録
            setLabel.SetLabel_ContextMenuStrip_Opened += ContextMenuStrip_Opened;
            setLabel.SetLabel_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
            setLabel.SetLabel_OnMouseClick += OnMouseClick;
            setLabel.SetLabel_OnMouseDoubleClick += OnMouseDoubleClick;
            setLabel.SetLabel_OnMouseDown += OnMouseDown;
            setLabel.MouseMove += OnMouseMove;
            setLabel.MouseUp += OnMouseUp;
            this.Controls.Add(setLabel, 0, 0);
            // 参照を退避
            DeployedSetLabel = setLabel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="carMasterVo"></param>
        public void AddCarLabel(CarMasterVo carMasterVo) {
            if (carMasterVo is null)
                return;
            CarLabel carLabel = new(carMasterVo);
            carLabel.Memo = VehicleDispatchDetailVo.CarMemo;
            carLabel.MemoFlag = VehicleDispatchDetailVo.CarMemoFlag;
            carLabel.ClassificationCode = VehicleDispatchDetailVo.ClassificationCode;
            carLabel.ManagedSpaceCode = VehicleDispatchDetailVo.ManagedSpaceCode;
            carLabel.ParentControl = this;
            carLabel.ProxyFlag = VehicleDispatchDetailVo.CarProxyFlag;
            // Eventを登録
            carLabel.CarLabel_ContextMenuStrip_Opened += ContextMenuStrip_Opened;
            carLabel.CarLabel_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
            carLabel.CarLabel_OnMouseClick += OnMouseClick;
            carLabel.CarLabel_OnMouseDoubleClick += OnMouseDoubleClick;
            carLabel.CarLabel_OnMouseDown += OnMouseDown;
            carLabel.MouseMove += OnMouseMove;
            carLabel.MouseUp += OnMouseUp;
            this.Controls.Add(carLabel, 0, 1);
            // 参照を退避
            DeployedCarLabel = carLabel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listStaffMasterVo"></param>
        public void AddStaffLabels(List<StaffMasterVo> listStaffMasterVo) {
            if (listStaffMasterVo is null)
                return;
            switch (VehicleDispatchDetailVo.PurposeFlag) {
                case false: // １列
                    DeployedStaffLabel0 = this.AddStaffLabel(0, listStaffMasterVo[0]);
                    DeployedStaffLabel1 = this.AddStaffLabel(1, listStaffMasterVo[1]);
                    DeployedStaffLabel2 = null;
                    DeployedStaffLabel3 = null;
                    break;
                case true: // ２列
                    DeployedStaffLabel0 = this.AddStaffLabel(0, listStaffMasterVo[0]);
                    DeployedStaffLabel1 = this.AddStaffLabel(1, listStaffMasterVo[1]);
                    DeployedStaffLabel2 = this.AddStaffLabel(2, listStaffMasterVo[2]);
                    DeployedStaffLabel3 = this.AddStaffLabel(3, listStaffMasterVo[3]);
                    break;
            }
        }

        /// <summary>
        /// SetControlの指定したCellにStaffLabelを作成
        /// </summary>
        /// <param name="number">0～3</param>
        /// <param name="staffMasterVo"></param>
        /// <returns></returns>
        private StaffLabel AddStaffLabel(int number, StaffMasterVo staffMasterVo) {
            if (staffMasterVo is not null) {
                StaffLabel staffLabel = new(staffMasterVo);
                switch (number) {
                    case 0:
                        staffLabel.Memo = VehicleDispatchDetailVo.StaffMemo1;
                        staffLabel.MemoFlag = VehicleDispatchDetailVo.StaffMemoFlag1;
                        staffLabel.OccupationCode = GetOccupationCode(0);
                        staffLabel.ParentControl = this;
                        staffLabel.ProxyFlag = VehicleDispatchDetailVo.StaffProxyFlag1;
                        staffLabel.RollCallFlag = VehicleDispatchDetailVo.StaffRollCallFlag1;
                        break;
                    case 1:
                        staffLabel.Memo = VehicleDispatchDetailVo.StaffMemo2;
                        staffLabel.MemoFlag = VehicleDispatchDetailVo.StaffMemoFlag2;
                        staffLabel.OccupationCode = GetOccupationCode(1);
                        staffLabel.ParentControl = this;
                        staffLabel.ProxyFlag = VehicleDispatchDetailVo.StaffProxyFlag2;
                        staffLabel.RollCallFlag = VehicleDispatchDetailVo.StaffRollCallFlag2;
                        break;
                    case 2:
                        staffLabel.Memo = VehicleDispatchDetailVo.StaffMemo3;
                        staffLabel.MemoFlag = VehicleDispatchDetailVo.StaffMemoFlag3;
                        staffLabel.OccupationCode = GetOccupationCode(2);
                        staffLabel.ParentControl = this;
                        staffLabel.ProxyFlag = VehicleDispatchDetailVo.StaffProxyFlag3;
                        staffLabel.RollCallFlag = VehicleDispatchDetailVo.StaffRollCallFlag3;
                        break;
                    case 3:
                        staffLabel.Memo = VehicleDispatchDetailVo.StaffMemo4;
                        staffLabel.MemoFlag = VehicleDispatchDetailVo.StaffMemoFlag4;
                        staffLabel.OccupationCode = GetOccupationCode(3);
                        staffLabel.ParentControl = this;
                        staffLabel.ProxyFlag = VehicleDispatchDetailVo.StaffProxyFlag4;
                        staffLabel.RollCallFlag = VehicleDispatchDetailVo.StaffRollCallFlag4;
                        break;
                }
                // Eventを登録
                staffLabel.StaffLabel_ContextMenuStrip_Opened += ContextMenuStrip_Opened;
                staffLabel.StaffLabel_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
                staffLabel.StaffLabel_OnMouseClick += OnMouseClick;
                staffLabel.StaffLabel_OnMouseDoubleClick += OnMouseDoubleClick;
                staffLabel.StaffLabel_OnMouseDown += OnMouseDown;
                staffLabel.MouseMove += OnMouseMove;
                staffLabel.MouseUp += OnMouseUp;
                this.Controls.Add(staffLabel, number <= 1 ? 0 : 1, number % 2 == 0 ? 2 : 3);
                return staffLabel;
            } else {
                return null;
            }
        }

        /// <summary>
        /// 条件によって”作”をつけるかどうかを決定する
        /// </summary>
        /// <param name="number">0～3</param>
        /// <returns></returns>
        private int GetOccupationCode(int number) {
            switch (VehicleDispatchDetailVo.ClassificationCode) {
                case 10 or 11 or 12:
                    if (number == 0) {
                        return 10;
                    } else {
                        return 11;
                    }
                default:
                    return VehicleDispatchDetailVo.ClassificationCode;
            }
        }

        /// <summary>
        /// プロパティに配置されているobjectをセットする
        /// _deployedSetLabel
        /// _deployedCarLabel
        /// _deployedStaffLabel0
        /// _deployedStaffLabel1
        /// _deployedStaffLabel2
        /// _deployedStaffLabel3
        /// </summary>
        /// <param name="setControl">対象のSetControl</param>
        private void SetControlRelocation(SetControl setControl) {
            if (VehicleDispatchDetailVo.PurposeFlag) {
                DeployedSetLabel = setControl.GetControlFromPosition(0, 0) is not null ? (SetLabel)setControl.GetControlFromPosition(0, 0) : null;
                DeployedCarLabel = setControl.GetControlFromPosition(0, 1) is not null ? (CarLabel)setControl.GetControlFromPosition(0, 1) : null;
                DeployedStaffLabel0 = setControl.GetControlFromPosition(0, 2) is not null ? (StaffLabel)setControl.GetControlFromPosition(0, 2) : null;
                DeployedStaffLabel1 = setControl.GetControlFromPosition(0, 3) is not null ? (StaffLabel)setControl.GetControlFromPosition(0, 3) : null;
                DeployedStaffLabel2 = setControl.GetControlFromPosition(1, 2) is not null ? (StaffLabel)setControl.GetControlFromPosition(1, 2) : null;
                DeployedStaffLabel3 = setControl.GetControlFromPosition(1, 3) is not null ? (StaffLabel)setControl.GetControlFromPosition(1, 3) : null;
            } else {
                DeployedSetLabel = setControl.GetControlFromPosition(0, 0) is not null ? (SetLabel)setControl.GetControlFromPosition(0, 0) : null;
                DeployedCarLabel = setControl.GetControlFromPosition(0, 1) is not null ? (CarLabel)setControl.GetControlFromPosition(0, 1) : null;
                DeployedStaffLabel0 = setControl.GetControlFromPosition(0, 2) is not null ? (StaffLabel)setControl.GetControlFromPosition(0, 2) : null;
                DeployedStaffLabel1 = setControl.GetControlFromPosition(0, 3) is not null ? (StaffLabel)setControl.GetControlFromPosition(0, 3) : null;
                DeployedStaffLabel2 = null;
                DeployedStaffLabel3 = null;
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
        private void ContextMenuStrip_Opened(object sender, EventArgs e) {
            //
            SetControl_ContextMenuStrip_Opened.Invoke(sender, e);
        }

        /// <summary>
        /// SetLabel/CarLabel/StaffLabelからのイベントを集約
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            //
            SetControl_ToolStripMenuItem_Click.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellPaint(TableLayoutCellPaintEventArgs e) {
            /*
             * Boderを描画する
             */
            Rectangle rectangle = e.CellBounds;
            rectangle.Inflate(-1, -1); // 枠のサイズを小さくする
            ControlPaint.DrawBorder(e.Graphics, rectangle, Color.Gray, ButtonBorderStyle.Dotted);
        }

        /// <summary>
        /// Drag・DropされたControlを退避する処理
        /// </summary>
        /// <param name="dragEventArgs"></param>
        protected override void OnDragDrop(DragEventArgs e) {
            /*
             * DragされたControlとParentを格納
             */
            if (e.Data.GetDataPresent(typeof(SetLabel))) {
                DragControl = (SetLabel)e.Data.GetData(typeof(SetLabel));
            } else if (e.Data.GetDataPresent(typeof(CarLabel))) {
                DragControl = (CarLabel)e.Data.GetData(typeof(CarLabel));
            } else if (e.Data.GetDataPresent(typeof(StaffLabel))) {
                DragControl = (StaffLabel)e.Data.GetData(typeof(StaffLabel));
            }
            switch (DragControl) {
                case SetLabel setLabel:
                    this.DragParentControl = setLabel.Parent;
                    setLabel.ParentControl = this;
                    break;
                case CarLabel carLabel:
                    this.DragParentControl = carLabel.Parent;
                    carLabel.ParentControl = this;
                    break;
                case StaffLabel staffLabel:
                    this.DragParentControl = staffLabel.Parent;
                    staffLabel.ParentControl = this;
                    break;
                default:
                    MessageBox.Show("コンテナを登録してください。");
                    break;
            }
            // DragのSetControlRelocationをセット

            // DropのSetControlRelocationをセット

            //
            SetControl_OnDragDrop.Invoke(this, e);
        }

        /// <summary>
        /// オブジェクトがコントロールの境界内にドラッグされると一度だけ発生します。
        /// </summary>
        /// <param name="dragEventArgs"></param>
        protected override void OnDragEnter(DragEventArgs e) {
            //
            SetControl_OnDragEnter.Invoke(this, e);
        }

        /// <summary>
        /// ドラッグ アンド ドロップ操作中にマウス カーソルがコントロールの境界内を移動したときに発生します。
        /// Copy  :データがドロップ先にコピーされようとしている状態
        /// Move  :データがドロップ先に移動されようとしている状態
        /// Scroll:データによってドロップ先でスクロールが開始されようとしている状態、あるいは現在スクロール中である状態
        /// All   :上の3つを組み合わせたもの
        /// Link  :データのリンクがドロップ先に作成されようとしている状態
        /// None  :いかなるデータもドロップ先が受け付けようとしない状態
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDragOver(DragEventArgs e) {
            Point clientPoint = this.PointToClient(new Point(e.X, e.Y));
            Point cellPoint = new(clientPoint.X / (int)CellWidth, clientPoint.Y / (int)CellHeight);
            if (e.Data.GetDataPresent(typeof(SetLabel))) {
                e.Effect = (cellPoint.X == 0 && cellPoint.Y == 0 && this.GetControlFromPosition(cellPoint.X, cellPoint.Y) is null) ? DragDropEffects.Move : DragDropEffects.None;
            } else if (e.Data.GetDataPresent(typeof(CarLabel))) {
                e.Effect = (cellPoint.X == 0 && cellPoint.Y == 1 && this.GetControlFromPosition(cellPoint.X, cellPoint.Y) is null) ? DragDropEffects.Move : DragDropEffects.None;
            } else if (e.Data.GetDataPresent(typeof(StaffLabel))) {
                e.Effect = ((cellPoint.Y == 2 || cellPoint.Y == 3) && this.GetControlFromPosition(cellPoint.X, cellPoint.Y) is null) ? DragDropEffects.Move : DragDropEffects.None;
            } else {
                e.Effect = DragDropEffects.None;
            }
            // 処理を渡す
            SetControl_OnDragOver.Invoke(this, e);
        }

        /// <summary>
        /// SetLabel/CarLabel/StaffLabel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseClick(object sender, MouseEventArgs e) {
            //
            SetControl_OnMouseClick.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDoubleClick(object sender, MouseEventArgs e) {
            //
            SetControl_OnMouseDoubleClick.Invoke(sender, e);
        }

        /// <summary>
        /// スクロールに使用
        /// 配下にある各LabelのEventを受け入れ
        /// </summary>
        /// <param name="e"></param>
        private void OnMouseDown(object sender, MouseEventArgs e) {
            //
            SetControl_OnMouseDown.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e) {
            //
            SetControl_OnMouseEnter.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e) {
            //
            SetControl_OnMouseLeave.Invoke(this, e);
        }

        /// <summary>
        /// スクロールに使用
        /// 配下にある各LabelのEventを受け入れ
        /// </summary>
        /// <param name="e"></param>
        private void OnMouseMove(object sender, MouseEventArgs e) {
            //
            SetControl_OnMouseMove.Invoke(sender, e);
        }

        /// <summary>
        /// スクロールに使用
        /// 配下にある各LabelのEventを受け入れ
        /// </summary>
        /// <param name="e"></param>
        private void OnMouseUp(object sender, MouseEventArgs e) {
            //
            SetControl_OnMouseUp.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        /*
         * プロパティー
         */
        /// <summary>
        /// 
        /// </summary>
        public float CellWidth => _cellWidth;
        /// <summary>
        /// 
        /// </summary>
        public float CellHeight => _cellHeight;
        /// <summary>
        /// CellNumber
        /// </summary>
        public int CellNumber {
            get => this._cellNumber;
            set => this._cellNumber = value;
        }
        /// <summary>
        /// VehicleDispatchDetailVoを格納
        /// </summary>
        public VehicleDispatchDetailVo VehicleDispatchDetailVo {
            get => this._vehicleDispatchDetailVo;
            set => this._vehicleDispatchDetailVo = value;
        }
        /// <summary>
        /// 配置されているSetLabelの参照を保持
        /// </summary>
        public SetLabel? DeployedSetLabel {
            get => this._deployedSetLabel;
            set => this._deployedSetLabel = value;
        }
        /// <summary>
        /// 配置されているCarLabelの参照を保持
        /// </summary>
        public CarLabel? DeployedCarLabel {
            get => this._deployedCarLabel;
            set => this._deployedCarLabel = value;
        }
        /// <summary>
        /// 配置されているStaffLabel 0の参照を保持
        /// </summary>
        public StaffLabel? DeployedStaffLabel0 {
            get => this._deployedStaffLabel0;
            set => this._deployedStaffLabel0 = value;
        }
        /// <summary>
        /// 配置されているStaffLabel 1の参照を保持
        /// </summary>
        public StaffLabel? DeployedStaffLabel1 {
            get => this._deployedStaffLabel1;
            set => this._deployedStaffLabel1 = value;
        }
        /// <summary>
        /// 配置されているStaffLabel 2の参照を保持
        /// </summary>
        public StaffLabel? DeployedStaffLabel2 {
            get => this._deployedStaffLabel2;
            set => this._deployedStaffLabel2 = value;
        }
        /// <summary>
        /// 配置されているStaffLabel 3の参照を保持
        /// </summary>
        public StaffLabel? DeployedStaffLabel3 {
            get => this._deployedStaffLabel3;
            set => this._deployedStaffLabel3 = value;
        }
        /// <summary>
        /// Drag時のParentControlを格納
        /// </summary>
        public object DragParentControl {
            get => this._dragParentControl;
            set => this._dragParentControl = value;
        }
        /// <summary>
        /// DragしたControlを格納
        /// </summary>
        public object DragControl {
            get => this._dragControl;
            set => this._dragControl = value;
        }

    }
}

