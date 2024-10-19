/*
 * 2024-10-10
 */
using Vo;

namespace ControlEx {
    public partial class SetControl : TableLayoutPanel {
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
            this.Size = new Size((int)_cellWidth * _columnCount, (int)_cellHeight * _rowCount);
            /*
             * Column作成
             */
            this.ColumnCount = _columnCount;
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, _cellWidth));
            /*
             * Row作成
             */
            this.RowCount = _rowCount;
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, _cellHeight));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, _cellHeight));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, _cellHeight));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, _cellHeight));
            /*
             * Event作成
             */
            this.MouseDown += SetControl_MouseDown;
            this.MouseEnter += SetControl_MouseEnter;
            this.MouseLeave += SetControl_MouseLeave;
            this.MouseMove += SetControl_MouseMove;
            this.MouseUp += SetControl_MouseUp;
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
            setLabel.SetMemoFlag = VehicleDispatchDetailVo.SetMemoFlag;
            setLabel.ShiftCode = VehicleDispatchDetailVo.ShiftCode;
            setLabel.StandByFlag = VehicleDispatchDetailVo.StandByFlag;
            setLabel.TelCallingFlag = (setMasterVo.ContactMethod == 10 || setMasterVo.ContactMethod == 13);
            this.Controls.Add(setLabel, 0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellNumber"></param>
        /// <param name="carMasterVo"></param>
        public void AddCarLabel(CarMasterVo carMasterVo) {
            if (carMasterVo is null)
                return;
            CarLabel carLabel = new(carMasterVo);

            this.Controls.Add(carLabel, 0, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellNumber"></param>
        /// <param name="listStaffMasterVo"></param>
        public void AddStaffLabel(List<StaffMasterVo> listStaffMasterVo) {
            if (listStaffMasterVo is null)
                return;

        }

        /*
         * Eventを渡す
         */
        public event MouseEventHandler Event_SetControl_MouseDown = delegate { };
        public event EventHandler Event_SetControl_MouseEnter = delegate { };
        public event EventHandler Event_SetControl_MouseLeave = delegate { };
        public event MouseEventHandler Event_SetControl_MouseMove = delegate { };
        public event MouseEventHandler Event_SetControl_MouseUp = delegate { };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetControl_MouseDown(object sender, MouseEventArgs e) {
            Event_SetControl_MouseDown.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetControl_MouseEnter(object sender, EventArgs e) {
            Event_SetControl_MouseEnter.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetControl_MouseLeave(object sender, EventArgs e) {
            Event_SetControl_MouseLeave.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetControl_MouseMove(object sender, MouseEventArgs e) {
            Event_SetControl_MouseMove.Invoke(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetControl_MouseUp(object sender, MouseEventArgs e) {
            Event_SetControl_MouseUp.Invoke(sender, e);
        }

        /*
         * プロパティー
         */
        public VehicleDispatchDetailVo VehicleDispatchDetailVo {
            get => this._vehicleDispatchDetailVo;
            set => this._vehicleDispatchDetailVo = value;
        }
    }
}

