/*
 * 2024-10-10
 */
using Vo;

namespace ControlEx {

    public partial class SetControl : TableLayoutPanel {
        private SetControl _thisLabel;
        private SetLabel _deployedSetLabel;
        private CarLabel _deployedCarLabel;
        private StaffLabel _deployedStaffLabel0;
        private StaffLabel _deployedStaffLabel1;
        private StaffLabel _deployedStaffLabel2;
        private StaffLabel _deployedStaffLabel3;

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
        /// Constructor
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
                    this.Size = new Size((int)_cellWidth * _columnCount * 2, (int)_cellHeight * _rowCount);
                    /*
                     * Column作成
                     */
                    this.ColumnCount = _columnCount + 1;
                    this.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _cellWidth));
                    this.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _cellWidth));
                    break;
                case false: // １列
                    // Size
                    this.Size = new Size((int)_cellWidth * _columnCount, (int)_cellHeight * _rowCount);
                    /*
                     * Column作成
                     */
                    this.ColumnCount = _columnCount;
                    this.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _cellWidth));
                    break;
            }
            /*
             * Row作成
             */
            this.RowCount = _rowCount;
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, _cellHeight));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, _cellHeight));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, _cellHeight));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, _cellHeight));
            // 自分自身の参照を退避
            ThisLabel = this;
        }

        /// <summary>
        /// OnPaint
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
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
        /// 
        /// </summary>
        /// <param name="cellNumber"></param>
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
            setLabel.MemoFlag = VehicleDispatchDetailVo.SetMemoFlag;
            setLabel.OperationFlag = VehicleDispatchDetailVo.OperationFlag;
            setLabel.ShiftCode = VehicleDispatchDetailVo.ShiftCode;
            setLabel.StandByFlag = VehicleDispatchDetailVo.StandByFlag;
            setLabel.TelCallingFlag = (setMasterVo.ContactMethod == 10 || setMasterVo.ContactMethod == 13);
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
            carLabel.MemoFlag = VehicleDispatchDetailVo.CarMemoFlag;
            carLabel.ClassificationCode = VehicleDispatchDetailVo.ClassificationCode;
            carLabel.ManagedSpaceCode = VehicleDispatchDetailVo.ManagedSpaceCode;
            carLabel.ProxyFlag = VehicleDispatchDetailVo.CarProxyFlag;
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
                        staffLabel.ProxyFlag = VehicleDispatchDetailVo.StaffProxyFlag1;
                        staffLabel.RollCallFlag = VehicleDispatchDetailVo.StaffRollCallFlag1;
                        break;
                    case 1:
                        staffLabel.Memo = VehicleDispatchDetailVo.StaffMemo2;
                        staffLabel.MemoFlag = VehicleDispatchDetailVo.StaffMemoFlag2;
                        staffLabel.OccupationCode = GetOccupationCode(1);
                        staffLabel.ProxyFlag = VehicleDispatchDetailVo.StaffProxyFlag2;
                        staffLabel.RollCallFlag = VehicleDispatchDetailVo.StaffRollCallFlag2;
                        break;
                    case 2:
                        staffLabel.Memo = VehicleDispatchDetailVo.StaffMemo3;
                        staffLabel.MemoFlag = VehicleDispatchDetailVo.StaffMemoFlag3;
                        staffLabel.OccupationCode = GetOccupationCode(2);
                        staffLabel.ProxyFlag = VehicleDispatchDetailVo.StaffProxyFlag3;
                        staffLabel.RollCallFlag = VehicleDispatchDetailVo.StaffRollCallFlag3;
                        break;
                    case 3:
                        staffLabel.Memo = VehicleDispatchDetailVo.StaffMemo4;
                        staffLabel.MemoFlag = VehicleDispatchDetailVo.StaffMemoFlag4;
                        staffLabel.OccupationCode = GetOccupationCode(3);
                        staffLabel.ProxyFlag = VehicleDispatchDetailVo.StaffProxyFlag4;
                        staffLabel.RollCallFlag = VehicleDispatchDetailVo.StaffRollCallFlag4;
                        break;
                }
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

        /*
         * Eventを渡す
         */
        public event MouseEventHandler Event_SetControl_OnMouseDown = delegate { };
        public event EventHandler Event_SetControl_OnMouseEnter = delegate { };
        public event EventHandler Event_SetControl_OnMouseLeave = delegate { };
        public event MouseEventHandler Event_SetControl_OnMouseMove = delegate { };
        public event MouseEventHandler Event_SetControl_OnMouseUp = delegate { };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e) {
            Event_SetControl_OnMouseDown.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e) {
            Event_SetControl_OnMouseEnter.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e) {
            Event_SetControl_OnMouseLeave.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e) {
            Event_SetControl_OnMouseMove.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e) {
            Event_SetControl_OnMouseUp.Invoke(this, e);
        }

        /*
         * プロパティー
         */
        /// <summary>
        /// 自分自身の参照を保持
        /// </summary>
        public SetControl ThisLabel {
            get => this._thisLabel;
            set => this._thisLabel = value;
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
        public SetLabel DeployedSetLabel {
            get => this._deployedSetLabel;
            set => this._deployedSetLabel = value;
        }
        /// <summary>
        /// 配置されているCarLabelの参照を保持
        /// </summary>
        public CarLabel DeployedCarLabel {
            get => this._deployedCarLabel;
            set => this._deployedCarLabel = value;
        }
        /// <summary>
        /// 配置されているStaffLabel 0の参照を保持
        /// </summary>
        public StaffLabel DeployedStaffLabel0 {
            get => this._deployedStaffLabel0;
            set => this._deployedStaffLabel0 = value;
        }
        /// <summary>
        /// 配置されているStaffLabel 1の参照を保持
        /// </summary>
        public StaffLabel DeployedStaffLabel1 {
            get => this._deployedStaffLabel1;
            set => this._deployedStaffLabel1 = value;
        }
        /// <summary>
        /// 配置されているStaffLabel 2の参照を保持
        /// </summary>
        public StaffLabel DeployedStaffLabel2 {
            get => this._deployedStaffLabel2;
            set => this._deployedStaffLabel2 = value;
        }
        /// <summary>
        /// 配置されているStaffLabel 3の参照を保持
        /// </summary>
        public StaffLabel DeployedStaffLabel3 {
            get => this._deployedStaffLabel3;
            set => this._deployedStaffLabel3 = value;
        }
    }
}

