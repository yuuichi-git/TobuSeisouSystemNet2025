/*
 * 2024-10-10
 */
using System.Diagnostics;

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

        private readonly DateTime _defaultDateTime = new(1900, 01, 01);

        private Control _deployedSetLabel;
        private Control _deployedCarLabel;
        private Control _deployedStaffLabel1;
        private Control _deployedStaffLabel2;
        private Control _deployedStaffLabel3;
        private Control _deployedStaffLabel4;

        private object _dragParentControl;
        private object _dragControl;

        private VehicleDispatchDetailVo _vehicleDispatchDetailVo;

        /*
         * 
         * VehicleDispatchDetailVo
         * 
         */
        private int _cellNumber;
        private DateTime _operationDate;
        private bool _operationFlag;
        private bool _vehicleDispatchFlag;
        private bool _purposeFlag;
        private int _setCode;
        private int _managedSpaceCode;
        private int _classificationCode;
        private bool _lastRollCallFlag;
        private DateTime _lastRollCallYmdHms;
        private bool _setMemoFlag;
        private string _setMemo;
        private int _shiftCode;
        private bool _standByFlag;
        private bool _addWorkerFlag;
        private bool _contactInfomationFlag;
        private bool _faxTransmissionFlag;
        private int _carCode;
        private int _carGarageCode;
        private bool _carProxyFlag;
        private bool _carMemoFlag;
        private string _carMemo;
        private int _targetStaffNumber;
        private int _staffCode1;
        private int _staffOccupation1;
        private bool _staffProxyFlag1;
        private bool _staffRollCallFlag1;
        private DateTime _staffRollCallYmdHms1;
        private bool _staffMemoFlag1;
        private string _staffMemo1;
        private int _staffCode2;
        private int _staffOccupation2;
        private bool _staffProxyFlag2;
        private bool _staffRollCallFlag2;
        private DateTime _staffRollCallYmdHms2;
        private bool _staffMemoFlag2;
        private string _staffMemo2;
        private int _staffCode3;
        private int _staffOccupation3;
        private bool _staffProxyFlag3;
        private bool _staffRollCallFlag3;
        private DateTime _staffRollCallYmdHms3;
        private bool _staffMemoFlag3;
        private string _staffMemo3;
        private int _staffCode4;
        private int _staffOccupation4;
        private bool _staffProxyFlag4;
        private bool _staffRollCallFlag4;
        private DateTime _staffRollCallYmdHms4;
        private bool _staffMemoFlag4;
        private string _staffMemo4;
        private string _insertPcName;
        private DateTime _insertYmdHms;
        private string _updatePcName;
        private DateTime _updateYmdHms;
        private string _deletePcName;
        private DateTime _deleteYmdHms;
        private bool _deleteFlag;

        /*
         * SetMasterVoのNumberOfPeopleを退避
         */
        private int _numberOfPeople;
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
        /*
         * 
         */
        private bool _oldOnCursorFlag = false;
        private bool _newOnCursorFlag = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vehicleDispatchDetailVo"></param>
        public SetControl(VehicleDispatchDetailVo vehicleDispatchDetailVo) {
            _vehicleDispatchDetailVo = vehicleDispatchDetailVo;
            /*
             * プロパティに値をセットする
             */
            this.SetAllProperty(vehicleDispatchDetailVo);
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
            switch (this.PurposeFlag) {
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
            // ContextMenuStripを初期化
            this.CreateContextMenuStrip();
            /*
             * Event
             */
            this.MouseDown += OnMouseDown; // 画面スクロールに使う
            this.MouseMove += OnMouseMove; // 画面スクロールに使う
            this.MouseUp += OnMouseUp; // 画面スクロールに使う
            this.MouseEnter += OnMouseEnter;
            this.MouseLeave += OnMouseLeave;
        }

        /*
         * ContextMenuStrip
         */
        ContextMenuStrip contextMenuStrip = new();
        ToolStripMenuItem toolStripMenuItem00 = new("SetControlの形状");
        ToolStripMenuItem toolStripMenuItem00_0 = new("Single-SetControl"); // 子アイテム１
        ToolStripMenuItem toolStripMenuItem00_1 = new("Double-SetControl"); // 子アイテム２
        /// <summary>
        /// CreateContextMenuStrip
        /// </summary>
        private void CreateContextMenuStrip() {
            //ContextMenuStrip contextMenuStrip = new();
            contextMenuStrip.Name = "ContextMenuStripSetControl";
            contextMenuStrip.Opened += ContextMenuStrip_Opened;
            this.ContextMenuStrip = contextMenuStrip;

            //ToolStripMenuItem toolStripMenuItem00 = new("SetControlの形状");
            toolStripMenuItem00.Name = "ToolStripMenuItemCarVerification";
            toolStripMenuItem00.Click += ToolStripMenuItem_Click;
            contextMenuStrip.Items.Add(toolStripMenuItem00);
            //ToolStripMenuItem toolStripMenuItem00_0 = new("Single-SetControl"); // 子アイテム１
            toolStripMenuItem00_0.Name = "ToolStripMenuItemSetControlSingle";
            toolStripMenuItem00_0.Click += ToolStripMenuItem_Click;
            toolStripMenuItem00.DropDownItems.Add(toolStripMenuItem00_0);
            contextMenuStrip.Items.Add(toolStripMenuItem00);
            //ToolStripMenuItem toolStripMenuItem00_1 = new("Double-SetControl"); // 子アイテム２
            toolStripMenuItem00_1.Name = "ToolStripMenuItemSetControlDouble";
            toolStripMenuItem00_1.Click += ToolStripMenuItem_Click;
            toolStripMenuItem00.DropDownItems.Add(toolStripMenuItem00_1);
            contextMenuStrip.Items.Add(toolStripMenuItem00);
            /*
             * スペーサー
             */
            contextMenuStrip.Items.Add(new ToolStripSeparator());
            /*
             * プロパティ
             */
            ToolStripMenuItem toolStripMenuItem01 = new("プロパティ");
            toolStripMenuItem01.Name = "ToolStripMenuItemSetControlProperty";
            toolStripMenuItem01.Click += ToolStripMenuItem_Click;
            contextMenuStrip.Items.Add(toolStripMenuItem01);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setMasterVo"></param>
        public void AddSetLabel(SetMasterVo setMasterVo) {
            if (setMasterVo is null)
                return;
            SetLabel setLabel = new(setMasterVo);
            setLabel.ParentControl = this;

            setLabel.AddWorkerFlag = this.AddWorkerFlag;
            setLabel.ClassificationCode = this.ClassificationCode;
            setLabel.ContactInfomationFlag = this.ContactInfomationFlag;
            setLabel.LastRollCallFlag = this.LastRollCallFlag;
            setLabel.LastRollCallYmdHms = this.LastRollCallYmdHms;
            setLabel.ManagedSpaceCode = this.ManagedSpaceCode;
            setLabel.Memo = this.SetMemo;
            setLabel.MemoFlag = this.SetMemoFlag;
            setLabel.OperationFlag = this.OperationFlag;
            setLabel.ShiftCode = this.ShiftCode;
            setLabel.StandByFlag = this.StandByFlag;
            setLabel.TelCallingFlag = false; // 電話連絡(2024-12-11の時点では、電話連絡確認機能は利用していない
            setLabel.FaxTransmissionFlag = this.FaxTransmissionFlag;
            // Eventを登録
            setLabel.SetLabel_ContextMenuStrip_Opened += ContextMenuStrip_Opened;
            setLabel.SetLabel_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
            setLabel.SetLabel_OnMouseClick += OnMouseClick;
            setLabel.SetLabel_OnMouseDoubleClick += OnMouseDoubleClick;
            setLabel.SetLabel_OnMouseDown += OnMouseDown;
            setLabel.SetLabel_OnMouseEnter += OnMouseEnter;
            setLabel.SetLabel_OnMouseLeave += OnMouseLeave;
            setLabel.MouseMove += OnMouseMove;
            setLabel.MouseUp += OnMouseUp;
            this.Controls.Add(setLabel, 0, 0);
            // 参照を退避
            this.DeployedSetLabel = setLabel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="carMasterVo"></param>
        public void AddCarLabel(CarMasterVo carMasterVo) {
            if (carMasterVo is null)
                return;
            CarLabel carLabel = new(carMasterVo);
            carLabel.ParentControl = this;

            carLabel.CarGarageCode = this.CarGarageCode;
            carLabel.ClassificationCode = carMasterVo.ClassificationCode;               // CarMasterの値を使用する
            carLabel.EmergencyVehicleFlag = carMasterVo.EmergencyVehicleFlag;           // CarMasterの値を使用する
            carLabel.Memo = this.CarMemo;
            carLabel.MemoFlag = this.CarMemoFlag;
            carLabel.ProxyFlag = this.CarProxyFlag;

            /*
             * Eventを登録
             */
            carLabel.CarLabel_ContextMenuStrip_Opened += ContextMenuStrip_Opened;
            carLabel.CarLabel_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
            carLabel.CarLabel_OnMouseClick += OnMouseClick;
            carLabel.CarLabel_OnMouseDoubleClick += OnMouseDoubleClick;
            carLabel.CarLabel_OnMouseDown += OnMouseDown;
            carLabel.CarLabel_OnMouseEnter += OnMouseEnter;
            carLabel.CarLabel_OnMouseLeave += OnMouseLeave;
            carLabel.MouseMove += OnMouseMove;
            carLabel.MouseUp += OnMouseUp;
            this.Controls.Add(carLabel, 0, 1);
            /*
             * 参照を退避
             */
            this.DeployedCarLabel = carLabel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listStaffMasterVo"></param>
        public void AddStaffLabels(List<StaffMasterVo> listStaffMasterVo) {
            if (listStaffMasterVo is null)
                return;
            switch (this.PurposeFlag) {
                case true: // ２列
                    this.DeployedStaffLabel1 = this.AddStaffLabel(0, listStaffMasterVo[0]);
                    this.DeployedStaffLabel2 = this.AddStaffLabel(1, listStaffMasterVo[1]);
                    this.DeployedStaffLabel3 = this.AddStaffLabel(2, listStaffMasterVo[2]);
                    this.DeployedStaffLabel4 = this.AddStaffLabel(3, listStaffMasterVo[3]);
                    break;
                case false: // １列
                    this.DeployedStaffLabel1 = this.AddStaffLabel(0, listStaffMasterVo[0]);
                    this.DeployedStaffLabel2 = this.AddStaffLabel(1, listStaffMasterVo[1]);
                    this.DeployedStaffLabel3 = null;
                    this.DeployedStaffLabel4 = null;
                    break;
            }
        }

        /// <summary>
        /// SetControlの指定したCellにStaffLabelを作成
        /// </summary>
        /// <param name="number">0～3</param>
        /// <param name="staffMasterVo"></param>
        /// <returns></returns>
        private StaffLabel? AddStaffLabel(int number, StaffMasterVo staffMasterVo) {
            if (staffMasterVo is not null) {
                StaffLabel staffLabel = new(staffMasterVo);
                staffLabel.ParentControl = this;
                switch (number) {
                    case 0:
                        staffLabel.Memo = this.StaffMemo1;
                        staffLabel.MemoFlag = this.StaffMemoFlag1;
                        staffLabel.OccupationCode = this.StaffOccupation1;//GetOccupationCode(0);

                        staffLabel.ProxyFlag = this.StaffProxyFlag1;
                        staffLabel.RollCallFlag = this.StaffRollCallFlag1;
                        staffLabel.RollCallYmdHms = this.StaffRollCallYmdHms1;
                        break;
                    case 1:
                        staffLabel.Memo = this.StaffMemo2;
                        staffLabel.MemoFlag = this.StaffMemoFlag2;
                        staffLabel.OccupationCode = this.StaffOccupation2;//GetOccupationCode(1);
                        staffLabel.ProxyFlag = this.StaffProxyFlag2;
                        staffLabel.RollCallFlag = this.StaffRollCallFlag2;
                        staffLabel.RollCallYmdHms = this.StaffRollCallYmdHms2;
                        break;
                    case 2:
                        staffLabel.Memo = this.StaffMemo3;
                        staffLabel.MemoFlag = this.StaffMemoFlag3;
                        staffLabel.OccupationCode = this.StaffOccupation3;//GetOccupationCode(2);
                        staffLabel.ProxyFlag = this.StaffProxyFlag3;
                        staffLabel.RollCallFlag = this.StaffRollCallFlag3;
                        staffLabel.RollCallYmdHms = this.StaffRollCallYmdHms3;
                        break;
                    case 3:
                        staffLabel.Memo = this.StaffMemo4;
                        staffLabel.MemoFlag = this.StaffMemoFlag4;
                        staffLabel.OccupationCode = this.StaffOccupation4;//GetOccupationCode(3);
                        staffLabel.ProxyFlag = this.StaffProxyFlag4;
                        staffLabel.RollCallFlag = this.StaffRollCallFlag4;
                        staffLabel.RollCallYmdHms = this.StaffRollCallYmdHms4;
                        break;
                }
                // Eventを登録
                staffLabel.StaffLabel_ContextMenuStrip_Opened += ContextMenuStrip_Opened;
                staffLabel.StaffLabel_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
                staffLabel.StaffLabel_OnMouseClick += OnMouseClick;
                staffLabel.StaffLabel_OnMouseDoubleClick += OnMouseDoubleClick;
                staffLabel.StaffLabel_OnMouseDown += OnMouseDown;
                staffLabel.StaffLabel_OnMouseEnter += OnMouseEnter;
                staffLabel.StaffLabel_OnMouseLeave += OnMouseLeave;
                staffLabel.MouseMove += OnMouseMove;
                staffLabel.MouseUp += OnMouseUp;
                this.Controls.Add(staffLabel, number <= 1 ? 0 : 1, number % 2 == 0 ? 2 : 3);
                return staffLabel;
            } else {
                return null;
            }
        }

        /// <summary>
        /// StaffLabelがSetControlに配置されている位置を取得する
        /// </summary>
        /// <param name="staffLabel"></param>
        /// <returns>CellNumber</returns>
        public int GetStaffNumber(StaffLabel staffLabel) {
            TableLayoutPanelCellPosition tableLayoutPanelCellPosition = ((SetControl)staffLabel.ParentControl).GetCellPosition(staffLabel);
            return tableLayoutPanelCellPosition.Column * 2 + (tableLayoutPanelCellPosition.Row - 2);
        }

        /// <summary>
        /// 条件によって”作”をつけるかどうかを決定する
        /// </summary>
        /// <param name="number">0～3</param>
        /// <returns></returns>
        private int GetOccupationCode(int number) {
            switch (this.ClassificationCode) {
                case 10 or 11 or 12:
                    if (number == 0) {
                        return 10;
                    } else {
                        return this.ClassificationCode;
                    }
                default:
                    return this.ClassificationCode;
            }
        }

        /// <summary>
        /// プロパティの操作はせずに再配置のみ実施する
        /// </summary>
        public void DefaultRelocation() {
            this.CellNumber = this.CellNumber;
            this.OperationDate = this.OperationDate;
            this.OperationFlag = this.OperationFlag;
            this.VehicleDispatchFlag = this.VehicleDispatchFlag;
            this.PurposeFlag = this.PurposeFlag;
            /*
             * SetLabel/CarLabel/StaffLabelの各プロパティをセットする
             */
            switch (this.PurposeFlag) {
                case true:
                    this.DeployedSetLabel = this.GetTableLayoutChildControl(0, 0);
                    this.DeployedCarLabel = this.GetTableLayoutChildControl(0, 1);
                    this.DeployedStaffLabel1 = this.GetTableLayoutChildControl(0, 2);
                    this.DeployedStaffLabel2 = this.GetTableLayoutChildControl(0, 3);
                    this.DeployedStaffLabel3 = this.GetTableLayoutChildControl(1, 2);
                    this.DeployedStaffLabel4 = this.GetTableLayoutChildControl(1, 3);
                    break;
                case false:
                    this.DeployedSetLabel = this.GetTableLayoutChildControl(0, 0);
                    this.DeployedCarLabel = this.GetTableLayoutChildControl(0, 1);
                    this.DeployedStaffLabel1 = this.GetTableLayoutChildControl(0, 2);
                    this.DeployedStaffLabel2 = this.GetTableLayoutChildControl(0, 3);
                    this.DeployedStaffLabel3 = null;
                    this.DeployedStaffLabel4 = null;
                    break;
            }
            this.InsertPcName = Environment.MachineName;
            this.InsertYmdHms = DateTime.Now;
            this.UpdatePcName = Environment.MachineName;
            this.UpdateYmdHms = DateTime.Now;
            this.DeletePcName = Environment.MachineName;
            this.DeleteYmdHms = DateTime.Now;
            this.DeleteFlag = false;
        }

        /// <summary>
        /// Drug側のSetControlを再構築する
        /// LabelがDragされて削除されている状態
        /// </summary>
        /// <param name="setControl">対象のSetControl</param>
        public void SetControlRelocation() {
            this.CellNumber = this.CellNumber;
            this.OperationDate = this.OperationDate;
            this.OperationFlag = false;                 // SetLabelが移動されたのだから、SetLabelは無い。つまり休車扱いになるよね。
            this.VehicleDispatchFlag = false;           // SetLabelが移動されたのだから、SetLabelは無い。つまり配車されていない扱いになるよね。
            this.PurposeFlag = this.PurposeFlag;        // DoubleかSingleかはSetLabelには関係ないのでそのままにしておいてね。あくまでもDouble・Singleの違いはSetControlの問題だよ。
            /*
             * SetLabel/CarLabel/StaffLabelの各プロパティをセットする
             */
            switch (this.PurposeFlag) {
                case true:
                    this.DeployedSetLabel = this.GetTableLayoutChildControl(0, 0);
                    this.DeployedCarLabel = this.GetTableLayoutChildControl(0, 1);
                    this.DeployedStaffLabel1 = this.GetTableLayoutChildControl(0, 2);
                    this.DeployedStaffLabel2 = this.GetTableLayoutChildControl(0, 3);
                    this.DeployedStaffLabel3 = this.GetTableLayoutChildControl(1, 2);
                    this.DeployedStaffLabel4 = this.GetTableLayoutChildControl(1, 3);
                    break;
                case false:
                    this.DeployedSetLabel = this.GetTableLayoutChildControl(0, 0);
                    this.DeployedCarLabel = this.GetTableLayoutChildControl(0, 1);
                    this.DeployedStaffLabel1 = this.GetTableLayoutChildControl(0, 2);
                    this.DeployedStaffLabel2 = this.GetTableLayoutChildControl(0, 3);
                    this.DeployedStaffLabel3 = null;
                    this.DeployedStaffLabel4 = null;
                    break;
            }
            this.InsertPcName = Environment.MachineName;
            this.InsertYmdHms = DateTime.Now;
            this.UpdatePcName = Environment.MachineName;
            this.UpdateYmdHms = DateTime.Now;
            this.DeletePcName = Environment.MachineName;
            this.DeleteYmdHms = DateTime.Now;
            this.DeleteFlag = false;
        }

        /// <summary>
        /// Drop側のSetControlを再構築する
        /// LabelがDropされて追加されている状態
        /// </summary>
        /// <param name="setControl"></param>
        public void SetControlRelocation(SetControl setControl) {
            this.CellNumber = setControl.CellNumber;
            this.OperationDate = this.OperationDate;
            this.OperationFlag = true;                  // SetLabelが移動されたのだから、SetLabelは有る。つまり稼働扱いになるよね。
            this.VehicleDispatchFlag = true;            // SetLabelが移動されたのだから、SetLabelは有る。つまり配車されている扱いになるよね。
            this.PurposeFlag = this.PurposeFlag;        // DoubleかSingleかはSetLabelには関係ないのでそのままにしておいてね。あくまでもDouble・Singleの違いはSetControlの問題だよ。
            /*
             * SetLabel/CarLabel/StaffLabelの各プロパティをセットする
             */
            switch (setControl.PurposeFlag) {
                case true:
                    this.DeployedSetLabel = this.GetTableLayoutChildControl(0, 0);
                    this.DeployedCarLabel = this.GetTableLayoutChildControl(0, 1);
                    this.DeployedStaffLabel1 = this.GetTableLayoutChildControl(0, 2);
                    this.DeployedStaffLabel2 = this.GetTableLayoutChildControl(0, 3);
                    this.DeployedStaffLabel3 = this.GetTableLayoutChildControl(1, 2);
                    this.DeployedStaffLabel4 = this.GetTableLayoutChildControl(1, 3);
                    break;
                case false:
                    this.DeployedSetLabel = this.GetTableLayoutChildControl(0, 0);
                    this.DeployedCarLabel = this.GetTableLayoutChildControl(0, 1);
                    this.DeployedStaffLabel1 = this.GetTableLayoutChildControl(0, 2);
                    this.DeployedStaffLabel2 = this.GetTableLayoutChildControl(0, 3);
                    this.DeployedStaffLabel3 = null;
                    this.DeployedStaffLabel4 = null;
                    break;
            }
            this.InsertPcName = Environment.MachineName;
            this.InsertYmdHms = DateTime.Now;
            this.UpdatePcName = Environment.MachineName;
            this.UpdateYmdHms = DateTime.Now;
            this.DeletePcName = Environment.MachineName;
            this.DeleteYmdHms = DateTime.Now;
            this.DeleteFlag = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private Control? GetTableLayoutChildControl(int row, int column) {
            Control control = this.GetControlFromPosition(row, column);
            if (control is not null) {
                Debug.WriteLine(string.Concat(row, ",", column, "→", control.Name));
            } else {
                Debug.WriteLine(string.Concat(row, ",", column, "→", "null"));
            }
            return control;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void EventAdd(Control control) {
            if (control is null)
                return;
            /*
             * Eventの付け替え
             */
            switch (control) {
                case SetLabel setLabel:
                    setLabel.SetLabel_ContextMenuStrip_Opened += ContextMenuStrip_Opened;
                    setLabel.SetLabel_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
                    setLabel.SetLabel_OnMouseClick += OnMouseClick;
                    setLabel.SetLabel_OnMouseDoubleClick += OnMouseDoubleClick;
                    setLabel.SetLabel_OnMouseDown += OnMouseDown;
                    setLabel.SetLabel_OnMouseEnter += OnMouseEnter;
                    setLabel.SetLabel_OnMouseLeave += OnMouseLeave;
                    setLabel.MouseMove += OnMouseMove;
                    setLabel.MouseUp += OnMouseUp;
                    break;
                case CarLabel carLabel:
                    carLabel.CarLabel_ContextMenuStrip_Opened += ContextMenuStrip_Opened;
                    carLabel.CarLabel_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
                    carLabel.CarLabel_OnMouseClick += OnMouseClick;
                    carLabel.CarLabel_OnMouseDoubleClick += OnMouseDoubleClick;
                    carLabel.CarLabel_OnMouseDown += OnMouseDown;
                    carLabel.CarLabel_OnMouseEnter += OnMouseEnter;
                    carLabel.CarLabel_OnMouseLeave += OnMouseLeave;
                    carLabel.MouseMove += OnMouseMove;
                    carLabel.MouseUp += OnMouseUp;
                    break;
                case StaffLabel staffLabel:
                    staffLabel.StaffLabel_ContextMenuStrip_Opened += ContextMenuStrip_Opened;
                    staffLabel.StaffLabel_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
                    staffLabel.StaffLabel_OnMouseClick += OnMouseClick;
                    staffLabel.StaffLabel_OnMouseDoubleClick += OnMouseDoubleClick;
                    staffLabel.StaffLabel_OnMouseDown += OnMouseDown;
                    staffLabel.StaffLabel_OnMouseEnter += OnMouseEnter;
                    staffLabel.StaffLabel_OnMouseLeave += OnMouseLeave;
                    staffLabel.MouseMove += OnMouseMove;
                    staffLabel.MouseUp += OnMouseUp;
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public void EventDel(Control control) {
            if (control is null)
                return;
            /*
             * Eventの付け替え
             */
            switch (control) {
                case SetLabel setLabel:
                    setLabel.SetLabel_ContextMenuStrip_Opened -= ContextMenuStrip_Opened;
                    setLabel.SetLabel_ToolStripMenuItem_Click -= ToolStripMenuItem_Click;
                    setLabel.SetLabel_OnMouseClick -= OnMouseClick;
                    setLabel.SetLabel_OnMouseDoubleClick -= OnMouseDoubleClick;
                    setLabel.SetLabel_OnMouseDown -= OnMouseDown;
                    setLabel.SetLabel_OnMouseEnter -= OnMouseEnter;
                    setLabel.SetLabel_OnMouseLeave -= OnMouseLeave;
                    setLabel.MouseMove -= OnMouseMove;
                    setLabel.MouseUp -= OnMouseUp;
                    break;
                case CarLabel carLabel:
                    carLabel.CarLabel_ContextMenuStrip_Opened -= ContextMenuStrip_Opened;
                    carLabel.CarLabel_ToolStripMenuItem_Click -= ToolStripMenuItem_Click;
                    carLabel.CarLabel_OnMouseClick -= OnMouseClick;
                    carLabel.CarLabel_OnMouseDoubleClick -= OnMouseDoubleClick;
                    carLabel.CarLabel_OnMouseDown -= OnMouseDown;
                    carLabel.CarLabel_OnMouseEnter -= OnMouseEnter;
                    carLabel.CarLabel_OnMouseLeave -= OnMouseLeave;
                    carLabel.MouseMove -= OnMouseMove;
                    carLabel.MouseUp -= OnMouseUp;
                    break;
                case StaffLabel staffLabel:
                    staffLabel.StaffLabel_ContextMenuStrip_Opened -= ContextMenuStrip_Opened;
                    staffLabel.StaffLabel_ToolStripMenuItem_Click -= ToolStripMenuItem_Click;
                    staffLabel.StaffLabel_OnMouseClick -= OnMouseClick;
                    staffLabel.StaffLabel_OnMouseDoubleClick -= OnMouseDoubleClick;
                    staffLabel.StaffLabel_OnMouseDown -= OnMouseDown;
                    staffLabel.StaffLabel_OnMouseEnter -= OnMouseEnter;
                    staffLabel.StaffLabel_OnMouseLeave -= OnMouseLeave;
                    staffLabel.MouseMove -= OnMouseMove;
                    staffLabel.MouseUp -= OnMouseUp;
                    break;
            }
        }

        /// <summary>
        /// VehicleDispatchDetailVoを作成する
        /// </summary>
        public VehicleDispatchDetailVo GetVehicleDispatchDetailVo() {
            VehicleDispatchDetailVo vehicleDispatchDetailVo = new();
            vehicleDispatchDetailVo.CellNumber = this.CellNumber;
            vehicleDispatchDetailVo.OperationDate = this.OperationDate;
            vehicleDispatchDetailVo.OperationFlag = this.OperationFlag;
            vehicleDispatchDetailVo.VehicleDispatchFlag = this.VehicleDispatchFlag;
            vehicleDispatchDetailVo.PurposeFlag = this.PurposeFlag;
            vehicleDispatchDetailVo.SetCode = this.SetCode;
            vehicleDispatchDetailVo.ManagedSpaceCode = this.ManagedSpaceCode;
            vehicleDispatchDetailVo.ClassificationCode = this.ClassificationCode;
            vehicleDispatchDetailVo.LastRollCallFlag = this.LastRollCallFlag;
            vehicleDispatchDetailVo.LastRollCallYmdHms = this.LastRollCallYmdHms;
            vehicleDispatchDetailVo.SetMemoFlag = this.SetMemoFlag;
            vehicleDispatchDetailVo.SetMemo = this.SetMemo;
            vehicleDispatchDetailVo.ShiftCode = this.ShiftCode;
            vehicleDispatchDetailVo.StandByFlag = this.StandByFlag;
            vehicleDispatchDetailVo.AddWorkerFlag = this.AddWorkerFlag;
            vehicleDispatchDetailVo.ContactInfomationFlag = this.ContactInfomationFlag;
            vehicleDispatchDetailVo.FaxTransmissionFlag = this.FaxTransmissionFlag;
            vehicleDispatchDetailVo.CarCode = this.CarCode;
            vehicleDispatchDetailVo.CarGarageCode = this.CarGarageCode;
            vehicleDispatchDetailVo.CarProxyFlag = this.CarProxyFlag;
            vehicleDispatchDetailVo.CarMemoFlag = this.CarMemoFlag;
            vehicleDispatchDetailVo.CarMemo = this.CarMemo;
            vehicleDispatchDetailVo.TargetStaffNumber = this.TargetStaffNumber;
            vehicleDispatchDetailVo.StaffCode1 = this.StaffCode1;
            vehicleDispatchDetailVo.StaffOccupation1 = this.StaffOccupation1;
            vehicleDispatchDetailVo.StaffProxyFlag1 = this.StaffProxyFlag1;
            vehicleDispatchDetailVo.StaffRollCallFlag1 = this.StaffRollCallFlag1;
            vehicleDispatchDetailVo.StaffRollCallYmdHms1 = this.StaffRollCallYmdHms1;
            vehicleDispatchDetailVo.StaffMemoFlag1 = this.StaffMemoFlag1;
            vehicleDispatchDetailVo.StaffMemo1 = this.StaffMemo1;
            vehicleDispatchDetailVo.StaffCode2 = this.StaffCode2;
            vehicleDispatchDetailVo.StaffOccupation2 = this.StaffOccupation2;
            vehicleDispatchDetailVo.StaffProxyFlag2 = this.StaffProxyFlag2;
            vehicleDispatchDetailVo.StaffRollCallFlag2 = this.StaffRollCallFlag2;
            vehicleDispatchDetailVo.StaffRollCallYmdHms2 = this.StaffRollCallYmdHms2;
            vehicleDispatchDetailVo.StaffMemoFlag2 = this.StaffMemoFlag2;
            vehicleDispatchDetailVo.StaffMemo2 = this.StaffMemo2;
            vehicleDispatchDetailVo.StaffCode3 = this.StaffCode3;
            vehicleDispatchDetailVo.StaffOccupation3 = this.StaffOccupation3;
            vehicleDispatchDetailVo.StaffProxyFlag3 = this.StaffProxyFlag3;
            vehicleDispatchDetailVo.StaffRollCallFlag3 = this.StaffRollCallFlag3;
            vehicleDispatchDetailVo.StaffRollCallYmdHms3 = this.StaffRollCallYmdHms3;
            vehicleDispatchDetailVo.StaffMemoFlag3 = this.StaffMemoFlag3;
            vehicleDispatchDetailVo.StaffMemo3 = this.StaffMemo3;
            vehicleDispatchDetailVo.StaffCode4 = this.StaffCode4;
            vehicleDispatchDetailVo.StaffOccupation4 = this.StaffOccupation4;
            vehicleDispatchDetailVo.StaffProxyFlag4 = this.StaffProxyFlag4;
            vehicleDispatchDetailVo.StaffRollCallFlag4 = this.StaffRollCallFlag4;
            vehicleDispatchDetailVo.StaffRollCallYmdHms4 = this.StaffRollCallYmdHms4;
            vehicleDispatchDetailVo.StaffMemoFlag4 = this.StaffMemoFlag4;
            vehicleDispatchDetailVo.StaffMemo4 = this.StaffMemo4;
            vehicleDispatchDetailVo.InsertPcName = this.InsertPcName;
            vehicleDispatchDetailVo.InsertYmdHms = this.InsertYmdHms;
            vehicleDispatchDetailVo.UpdatePcName = this.UpdatePcName;
            vehicleDispatchDetailVo.UpdateYmdHms = this.UpdateYmdHms;
            vehicleDispatchDetailVo.DeletePcName = this.DeletePcName;
            vehicleDispatchDetailVo.DeleteYmdHms = this.DeleteYmdHms;
            vehicleDispatchDetailVo.DeleteFlag = this.DeleteFlag;
            return vehicleDispatchDetailVo;
        }

        /// <summary>
        /// VehicleDispatchDetailVoをプロパティにセットする
        /// </summary>
        /// <param name="vehicleDispatchDetailVo"></param>
        public void SetAllProperty(VehicleDispatchDetailVo vehicleDispatchDetailVo) {
            this.CellNumber = vehicleDispatchDetailVo.CellNumber;
            this.OperationDate = vehicleDispatchDetailVo.OperationDate;
            this.OperationFlag = vehicleDispatchDetailVo.OperationFlag;
            this.VehicleDispatchFlag = vehicleDispatchDetailVo.VehicleDispatchFlag;
            this.PurposeFlag = vehicleDispatchDetailVo.PurposeFlag;
            this.SetCode = vehicleDispatchDetailVo.SetCode;
            this.ManagedSpaceCode = vehicleDispatchDetailVo.ManagedSpaceCode;
            this.ClassificationCode = vehicleDispatchDetailVo.ClassificationCode;
            this.LastRollCallFlag = vehicleDispatchDetailVo.LastRollCallFlag;
            this.LastRollCallYmdHms = vehicleDispatchDetailVo.LastRollCallYmdHms;
            this.SetMemoFlag = vehicleDispatchDetailVo.SetMemoFlag;
            this.SetMemo = vehicleDispatchDetailVo.SetMemo;
            this.ShiftCode = vehicleDispatchDetailVo.ShiftCode;
            this.StandByFlag = vehicleDispatchDetailVo.StandByFlag;
            this.AddWorkerFlag = vehicleDispatchDetailVo.AddWorkerFlag;
            this.ContactInfomationFlag = vehicleDispatchDetailVo.ContactInfomationFlag;
            this.FaxTransmissionFlag = vehicleDispatchDetailVo.FaxTransmissionFlag;
            this.CarCode = vehicleDispatchDetailVo.CarCode;
            this.CarGarageCode = vehicleDispatchDetailVo.CarGarageCode;
            this.CarProxyFlag = vehicleDispatchDetailVo.CarProxyFlag;
            this.CarMemoFlag = vehicleDispatchDetailVo.CarMemoFlag;
            this.CarMemo = vehicleDispatchDetailVo.CarMemo;
            this.TargetStaffNumber = vehicleDispatchDetailVo.TargetStaffNumber;
            this.StaffCode1 = vehicleDispatchDetailVo.StaffCode1;
            this.StaffOccupation1 = vehicleDispatchDetailVo.StaffOccupation1;
            this.StaffProxyFlag1 = vehicleDispatchDetailVo.StaffProxyFlag1;
            this.StaffRollCallFlag1 = vehicleDispatchDetailVo.StaffRollCallFlag1;
            this.StaffRollCallYmdHms1 = vehicleDispatchDetailVo.StaffRollCallYmdHms1;
            this.StaffMemoFlag1 = vehicleDispatchDetailVo.StaffMemoFlag1;
            this.StaffMemo1 = vehicleDispatchDetailVo.StaffMemo1;
            this.StaffCode2 = vehicleDispatchDetailVo.StaffCode2;
            this.StaffOccupation2 = vehicleDispatchDetailVo.StaffOccupation2;
            this.StaffProxyFlag2 = vehicleDispatchDetailVo.StaffProxyFlag2;
            this.StaffRollCallFlag2 = vehicleDispatchDetailVo.StaffRollCallFlag2;
            this.StaffRollCallYmdHms2 = vehicleDispatchDetailVo.StaffRollCallYmdHms2;
            this.StaffMemoFlag2 = vehicleDispatchDetailVo.StaffMemoFlag2;
            this.StaffMemo2 = vehicleDispatchDetailVo.StaffMemo2;
            this.StaffCode3 = vehicleDispatchDetailVo.StaffCode3;
            this.StaffOccupation3 = vehicleDispatchDetailVo.StaffOccupation3;
            this.StaffProxyFlag3 = vehicleDispatchDetailVo.StaffProxyFlag3;
            this.StaffRollCallFlag3 = vehicleDispatchDetailVo.StaffRollCallFlag3;
            this.StaffRollCallYmdHms3 = vehicleDispatchDetailVo.StaffRollCallYmdHms3;
            this.StaffMemoFlag3 = vehicleDispatchDetailVo.StaffMemoFlag3;
            this.StaffMemo3 = vehicleDispatchDetailVo.StaffMemo3;
            this.StaffCode4 = vehicleDispatchDetailVo.StaffCode4;
            this.StaffOccupation4 = vehicleDispatchDetailVo.StaffOccupation4;
            this.StaffProxyFlag4 = vehicleDispatchDetailVo.StaffProxyFlag4;
            this.StaffRollCallFlag4 = vehicleDispatchDetailVo.StaffRollCallFlag4;
            this.StaffRollCallYmdHms4 = vehicleDispatchDetailVo.StaffRollCallYmdHms4;
            this.StaffMemoFlag4 = vehicleDispatchDetailVo.StaffMemoFlag4;
            this.StaffMemo4 = vehicleDispatchDetailVo.StaffMemo4;
            this.InsertPcName = vehicleDispatchDetailVo.InsertPcName;
            this.InsertYmdHms = vehicleDispatchDetailVo.InsertYmdHms;
            this.UpdatePcName = vehicleDispatchDetailVo.UpdatePcName;
            this.UpdateYmdHms = vehicleDispatchDetailVo.UpdateYmdHms;
            this.DeletePcName = vehicleDispatchDetailVo.DeletePcName;
            this.DeleteYmdHms = vehicleDispatchDetailVo.DeleteYmdHms;
            this.DeleteFlag = vehicleDispatchDetailVo.DeleteFlag;
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
            switch (((ContextMenuStrip)sender).SourceControl) {
                case SetControl setControl:
                    if ((this.CellNumber >= 0 && this.CellNumber <= 43) || (this.CellNumber >= 50 && this.CellNumber <= 93) || (this.CellNumber >= 100 && this.CellNumber <= 143) || (this.CellNumber >= 150 && this.CellNumber <= 193)) {
                        // Single-SetControlに変更する場合、自分自身が"Double-SetControl"でなくてはならない
                        toolStripMenuItem00_0.Enabled = this.PurposeFlag && this.StaffCode3 == 0 && this.StaffCode4 == 0 ? true : false;
                        // Double-SetControlに変更する場合、自分自身が"Single-SetControl"でなくてはならない
                        toolStripMenuItem00_1.Enabled = !this.PurposeFlag ? true : false;
                    } else {
                        toolStripMenuItem00_0.Enabled = false;
                        toolStripMenuItem00_1.Enabled = false;
                    }
                    break;
                case SetLabel setLabel:

                    break;
                case CarLabel carLabel:

                    break;
                case StaffLabel staffLabel:

                    break;
            }
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
            /*
             * Boderを描画する
             */
            if (_vehicleDispatchDetailVo.VehicleDispatchFlag) {
                switch (e.Column) {
                    case 0: // １列目
                        switch (e.Row) {
                            case 2: // StaffLabel(1人目)
                                if (this.NumberOfPeople >= 1)
                                    ControlPaint.DrawBorder(e.Graphics, rectangle, Color.Gray, ButtonBorderStyle.Dotted); // StaffLabel1の枠線
                                break;
                            case 3: // StaffLabel(2人目)
                                if (this.NumberOfPeople >= 2)
                                    ControlPaint.DrawBorder(e.Graphics, rectangle, Color.Gray, ButtonBorderStyle.Dotted); // StaffLabel2の枠線
                                break;
                        }
                        break;
                    case 1: // ２列目
                        switch (e.Row) {
                            case 2: // StaffLabel(3人目)
                                if (this.NumberOfPeople >= 3)
                                    ControlPaint.DrawBorder(e.Graphics, rectangle, Color.Gray, ButtonBorderStyle.Dotted); // StaffLabel3の枠線
                                break;
                            case 3: // StaffLabel(4人目)
                                if (this.NumberOfPeople >= 4)
                                    ControlPaint.DrawBorder(e.Graphics, rectangle, Color.Gray, ButtonBorderStyle.Dotted); // StaffLabel4の枠線
                                break;
                        }
                        break;
                }
            } else {

            }
            /*
             * H_SetControlの外枠を描画する
             */
            if (_oldOnCursorFlag) {
                if (this.PurposeFlag) {
                    e.Graphics.DrawRectangle(new Pen(Color.Gray, 2), new Rectangle(1, 1, 146, 477));
                } else {
                    e.Graphics.DrawRectangle(new Pen(Color.Gray, 2), new Rectangle(1, 1, 71, 477));
                }
            }
        }

        /// <summary>
        /// DragされているControl
        /// </summary>
        private Control _dragEnterObject;
        /// <summary>
        /// オブジェクトがコントロールの境界内にドラッグされると一度だけ発生します。
        /// </summary>
        /// <param name="dragEventArgs"></param>
        protected override void OnDragEnter(DragEventArgs e) {
            if (e.Data.GetDataPresent(typeof(SetLabel))) {
                _dragEnterObject = (SetLabel)e.Data.GetData(typeof(SetLabel));
            } else if (e.Data.GetDataPresent(typeof(CarLabel))) {
                _dragEnterObject = (CarLabel)e.Data.GetData(typeof(CarLabel));
            } else if (e.Data.GetDataPresent(typeof(StaffLabel))) {
                _dragEnterObject = (StaffLabel)e.Data.GetData(typeof(StaffLabel));
            }
            // 処理を渡す
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
            switch (_dragEnterObject) {
                case SetLabel setLabel:
                    e.Effect = (cellPoint.X == 0 && cellPoint.Y == 0 && this.GetControlFromPosition(cellPoint.X, cellPoint.Y) is null) ? DragDropEffects.Move : DragDropEffects.None;
                    break;
                case CarLabel carLabel:
                    e.Effect = (cellPoint.X == 0 && cellPoint.Y == 1 && this.GetControlFromPosition(cellPoint.X, cellPoint.Y) is null) ? DragDropEffects.Move : DragDropEffects.None;
                    break;
                case StaffLabel staffLabel:
                    e.Effect = ((cellPoint.Y == 2 || cellPoint.Y == 3) && this.GetControlFromPosition(cellPoint.X, cellPoint.Y) is null) ? DragDropEffects.Move : DragDropEffects.None;
                    break;
                default:
                    e.Effect = DragDropEffects.None;
                    break;
            }
            // 処理を渡す
            SetControl_OnDragOver.Invoke(this, e);
        }

        /// <summary>
        /// Drag・DropされたControlを退避する処理
        /// </summary>
        /// <param name="dragEventArgs"></param>
        protected override void OnDragDrop(DragEventArgs e) {
            this.DragControl = _dragEnterObject;
            switch (_dragEnterObject) {
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
            }
            // 処理を渡す
            SetControl_OnDragDrop.Invoke(this, e);
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
        private void OnMouseEnter(object sender, EventArgs e) {
            /*
             * Control上にカーソルがある
             */
            _newOnCursorFlag = true;
            if (_oldOnCursorFlag != _newOnCursorFlag) {
                _oldOnCursorFlag = _newOnCursorFlag;
                this.Refresh();
            }
            //
            SetControl_OnMouseEnter.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void OnMouseLeave(object sender, EventArgs e) {
            /*
             * Control上にカーソルがない
             */
            _newOnCursorFlag = false;
            if (_oldOnCursorFlag != _newOnCursorFlag) {
                _oldOnCursorFlag = _newOnCursorFlag;
                this.Refresh();
            }
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
         * 
         * プロパティー
         * 
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
        /// 配置されているSetLabelの参照を保持
        /// </summary>
        public Control DeployedSetLabel {
            get => this._deployedSetLabel;
            set {
                this._deployedSetLabel = value;
                if (this.DeployedSetLabel is not null) {
                    this.SetCode = ((SetLabel)this.DeployedSetLabel).SetMasterVo.SetCode;
                    this.ManagedSpaceCode = ((SetLabel)this.DeployedSetLabel).ManagedSpaceCode;
                    this.ClassificationCode = ((SetLabel)this.DeployedSetLabel).ClassificationCode;
                    this.LastRollCallFlag = ((SetLabel)this.DeployedSetLabel).LastRollCallFlag;
                    this.LastRollCallYmdHms = ((SetLabel)this.DeployedSetLabel).LastRollCallYmdHms;
                    this.SetMemoFlag = ((SetLabel)this.DeployedSetLabel).MemoFlag;
                    this.SetMemo = ((SetLabel)this.DeployedSetLabel).Memo;
                    this.ShiftCode = ((SetLabel)this.DeployedSetLabel).ShiftCode;
                    this.StandByFlag = ((SetLabel)this.DeployedSetLabel).StandByFlag;
                    this.AddWorkerFlag = ((SetLabel)this.DeployedSetLabel).AddWorkerFlag;
                    this.ContactInfomationFlag = ((SetLabel)this.DeployedSetLabel).ContactInfomationFlag;
                    this.FaxTransmissionFlag = ((SetLabel)this.DeployedSetLabel).FaxTransmissionFlag;
                } else {
                    this.SetCode = 0;
                    this.ManagedSpaceCode = 0;
                    this.ClassificationCode = 99;
                    this.LastRollCallFlag = false;
                    this.LastRollCallYmdHms = _defaultDateTime;
                    this.SetMemoFlag = false;
                    this.SetMemo = string.Empty;
                    this.ShiftCode = 0;
                    this.StandByFlag = false;
                    this.AddWorkerFlag = false;
                    this.ContactInfomationFlag = false;
                    this.FaxTransmissionFlag = false;
                }
            }
        }
        /// <summary>
        /// 配置されているCarLabelの参照を保持
        /// </summary>
        public Control DeployedCarLabel {
            get => this._deployedCarLabel;
            set {
                this._deployedCarLabel = value;
                if (this.DeployedCarLabel is not null) {
                    this.CarCode = ((CarLabel)this.DeployedCarLabel).CarMasterVo.CarCode;
                    this.CarGarageCode = ((CarLabel)this.DeployedCarLabel).CarGarageCode;
                    this.CarProxyFlag = ((CarLabel)this.DeployedCarLabel).ProxyFlag;
                    this.CarMemoFlag = ((CarLabel)this.DeployedCarLabel).MemoFlag;
                    this.CarMemo = ((CarLabel)this.DeployedCarLabel).Memo;
                } else {
                    this.CarCode = 0;
                    this.CarGarageCode = 0;
                    this.CarProxyFlag = false;
                    this.CarMemoFlag = false;
                    this.CarMemo = string.Empty;
                }
            }
        }

        /// <summary>
        /// 配置されているStaffLabel 1の参照を保持
        /// </summary>
        public Control DeployedStaffLabel1 {
            get => this._deployedStaffLabel1;
            set {
                this._deployedStaffLabel1 = value;
                if (this.DeployedStaffLabel1 is not null) {
                    this.StaffCode1 = ((StaffLabel)this.DeployedStaffLabel1).StaffMasterVo.StaffCode;
                    this.StaffOccupation1 = ((StaffLabel)this.DeployedStaffLabel1).OccupationCode;
                    this.StaffProxyFlag1 = ((StaffLabel)this.DeployedStaffLabel1).ProxyFlag;
                    this.StaffRollCallFlag1 = ((StaffLabel)this.DeployedStaffLabel1).RollCallFlag;
                    this.StaffRollCallYmdHms1 = ((StaffLabel)this.DeployedStaffLabel1).RollCallYmdHms;
                    this.StaffMemoFlag1 = ((StaffLabel)this.DeployedStaffLabel1).MemoFlag;
                    this.StaffMemo1 = ((StaffLabel)this.DeployedStaffLabel1).Memo;
                } else {
                    this.StaffCode1 = 0;
                    this.StaffOccupation1 = 99;
                    this.StaffProxyFlag1 = false;
                    this.StaffRollCallFlag1 = false;
                    this.StaffRollCallYmdHms1 = _defaultDateTime;
                    this.StaffMemoFlag1 = false;
                    this.StaffMemo1 = string.Empty;
                }
            }
        }
        /// <summary>
        /// 配置されているStaffLabel 2の参照を保持
        /// </summary>
        public Control DeployedStaffLabel2 {
            get => this._deployedStaffLabel2;
            set {
                this._deployedStaffLabel2 = value;
                if (this.DeployedStaffLabel2 is not null) {
                    this.StaffCode2 = ((StaffLabel)this.DeployedStaffLabel2).StaffMasterVo.StaffCode;
                    this.StaffOccupation2 = ((StaffLabel)this.DeployedStaffLabel2).OccupationCode;
                    this.StaffProxyFlag2 = ((StaffLabel)this.DeployedStaffLabel2).ProxyFlag;
                    this.StaffRollCallFlag2 = ((StaffLabel)this.DeployedStaffLabel2).RollCallFlag;
                    this.StaffRollCallYmdHms2 = ((StaffLabel)this.DeployedStaffLabel2).RollCallYmdHms;
                    this.StaffMemoFlag2 = ((StaffLabel)this.DeployedStaffLabel2).MemoFlag;
                    this.StaffMemo2 = ((StaffLabel)this.DeployedStaffLabel2).Memo;
                } else {
                    this.StaffCode2 = 0;
                    this.StaffOccupation2 = 99;
                    this.StaffProxyFlag2 = false;
                    this.StaffRollCallFlag2 = false;
                    this.StaffRollCallYmdHms2 = _defaultDateTime;
                    this.StaffMemoFlag2 = false;
                    this.StaffMemo2 = string.Empty;
                }
            }
        }
        /// <summary>
        /// 配置されているStaffLabel 3の参照を保持
        /// </summary>
        public Control DeployedStaffLabel3 {
            get => this._deployedStaffLabel3;
            set {
                this._deployedStaffLabel3 = value;
                if (this.DeployedStaffLabel3 is not null) {
                    this.StaffCode3 = ((StaffLabel)this.DeployedStaffLabel3).StaffMasterVo.StaffCode;
                    this.StaffOccupation3 = ((StaffLabel)this.DeployedStaffLabel3).OccupationCode;
                    this.StaffProxyFlag3 = ((StaffLabel)this.DeployedStaffLabel3).ProxyFlag;
                    this.StaffRollCallFlag3 = ((StaffLabel)this.DeployedStaffLabel3).RollCallFlag;
                    this.StaffRollCallYmdHms3 = ((StaffLabel)this.DeployedStaffLabel3).RollCallYmdHms;
                    this.StaffMemoFlag3 = ((StaffLabel)this.DeployedStaffLabel3).MemoFlag;
                    this.StaffMemo3 = ((StaffLabel)this.DeployedStaffLabel3).Memo;
                } else {
                    this.StaffCode3 = 0;
                    this.StaffOccupation3 = 99;
                    this.StaffProxyFlag3 = false;
                    this.StaffRollCallFlag3 = false;
                    this.StaffRollCallYmdHms3 = _defaultDateTime;
                    this.StaffMemoFlag3 = false;
                    this.StaffMemo3 = string.Empty;
                }
            }
        }
        /// <summary>
        /// 配置されているStaffLabel 4の参照を保持
        /// </summary>
        public Control DeployedStaffLabel4 {
            get => this._deployedStaffLabel4;
            set {
                this._deployedStaffLabel4 = value;
                if (this.DeployedStaffLabel4 is not null) {
                    this.StaffCode4 = ((StaffLabel)this.DeployedStaffLabel4).StaffMasterVo.StaffCode;
                    this.StaffOccupation4 = ((StaffLabel)this.DeployedStaffLabel4).OccupationCode;
                    this.StaffProxyFlag4 = ((StaffLabel)this.DeployedStaffLabel4).ProxyFlag;
                    this.StaffRollCallFlag4 = ((StaffLabel)this.DeployedStaffLabel4).RollCallFlag;
                    this.StaffRollCallYmdHms4 = ((StaffLabel)this.DeployedStaffLabel4).RollCallYmdHms;
                    this.StaffMemoFlag4 = ((StaffLabel)this.DeployedStaffLabel4).MemoFlag;
                    this.StaffMemo4 = ((StaffLabel)this.DeployedStaffLabel4).Memo;
                } else {
                    this.StaffCode4 = 0;
                    this.StaffOccupation4 = 99;
                    this.StaffProxyFlag4 = false;
                    this.StaffRollCallFlag4 = false;
                    this.StaffRollCallYmdHms4 = _defaultDateTime;
                    this.StaffMemoFlag4 = false;
                    this.StaffMemo4 = string.Empty;
                }
            }
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
            set {
                this._dragControl = value;
            }
        }

        /*
         * 
         * VehicleDispatchDetailVo
         * 
         */
        /// <summary>
        /// 配車表№
        /// 0～199の番号
        /// </summary>
        public int CellNumber {
            get => _cellNumber;
            set => _cellNumber = value;
        }
        /// <summary>
        /// 稼働日
        /// </summary>
        public DateTime OperationDate {
            get => _operationDate;
            set => _operationDate = value;
        }
        /// <summary>
        /// 稼働フラグ
        /// true:稼働日 false:休車
        /// </summary>
        public bool OperationFlag {
            get => _operationFlag;
            set => _operationFlag = value;
        }
        /// <summary>
        /// 配車フラグ
        /// true:配車されている false:配車されていない
        /// </summary>
        public bool VehicleDispatchFlag {
            get => _vehicleDispatchFlag;
            set => _vehicleDispatchFlag = value;
        }
        /// <summary>
        /// H_SetControlの形状
        /// true:2列 false:1列
        /// </summary>
        public bool PurposeFlag {
            get => _purposeFlag;
            set => _purposeFlag = value;
        }
        /// <summary>
        /// 配車先コード
        /// </summary>
        public int SetCode {
            get => _setCode;
            set => _setCode = value;
        }
        /// <summary>
        /// 管理地
        /// 0:該当なし 1:足立 2:三郷
        /// </summary>
        public int ManagedSpaceCode {
            get => _managedSpaceCode;
            set => _managedSpaceCode = value;
        }
        /// <summary>
        /// 分類コード
        /// 10:雇上 11:区契 12:臨時 20:清掃工場 30:社内 50:一般 51:社用車 99:指定なし
        /// </summary>
        public int ClassificationCode {
            get => _classificationCode;
            set => _classificationCode = value;
        }
        /// <summary>
        /// 帰庫点呼フラグ
        /// true:帰庫点呼記録済 false:未点呼
        /// </summary>
        public bool LastRollCallFlag {
            get => this._lastRollCallFlag;
            set {
                this._lastRollCallFlag = value;
                Refresh();
            }
        }
        /// <summary>
        /// 帰庫点呼日時
        /// </summary>
        public DateTime LastRollCallYmdHms {
            get => _lastRollCallYmdHms;
            set => _lastRollCallYmdHms = value;
        }
        /// <summary>
        /// メモフラグ
        /// true:メモが存在する false:メモが存在しない
        /// </summary>
        public bool SetMemoFlag {
            get => _setMemoFlag;
            set => _setMemoFlag = value;
        }
        /// <summary>
        /// メモ
        /// </summary>
        public string SetMemo {
            get => _setMemo;
            set => _setMemo = value;
        }
        /// <summary>
        /// 番手コード
        /// 0:指定なし 1:早番 2:遅番
        /// </summary>
        public int ShiftCode {
            get => _shiftCode;
            set => _shiftCode = value;
        }
        /// <summary>
        /// 待機フラグ
        /// true:待機 false:通常
        /// </summary>
        public bool StandByFlag {
            get => _standByFlag;
            set => _standByFlag = value;
        }
        /// <summary>
        /// 作業員付きフラグ
        /// true:作業員付き false:作業員なし
        /// </summary>
        public bool AddWorkerFlag {
            get => _addWorkerFlag;
            set => _addWorkerFlag = value;
        }
        /// <summary>
        /// 連絡事項印フラグ
        /// true:連絡事項あり false:連絡事項なし
        /// </summary>
        public bool ContactInfomationFlag {
            get => _contactInfomationFlag;
            set => _contactInfomationFlag = value;
        }
        /// <summary>
        /// FAX送信フラグ
        /// true:Fax送信 false:なし
        /// </summary>
        public bool FaxTransmissionFlag {
            get => _faxTransmissionFlag;
            set => _faxTransmissionFlag = value;
        }
        /// <summary>
        /// 車両コード
        /// </summary>
        public int CarCode {
            get => _carCode;
            set => _carCode = value;
        }
        /// <summary>
        /// 車庫地コード
        /// 0:該当なし 1:本社 2:三郷
        /// </summary>
        public int CarGarageCode {
            get => _carGarageCode;
            set => _carGarageCode = value;
        }
        /// <summary>
        /// 代車フラグ
        /// true:代車 false:本番車
        /// </summary>
        public bool CarProxyFlag {
            get => _carProxyFlag;
            set => _carProxyFlag = value;
        }
        /// <summary>
        /// メモフラグ
        /// true:メモが存在する false:メモが存在しない
        /// </summary>
        public bool CarMemoFlag {
            get => _carMemoFlag;
            set => _carMemoFlag = value;
        }
        /// <summary>
        /// メモ
        /// </summary>
        public string CarMemo {
            get => _carMemo;
            set => _carMemo = value;
        }
        /// <summary>
        /// TargetStaffの番号
        /// 0:運転手 1:作業員１ 2:作業員２ 3:作業員３
        /// </summary>
        public int TargetStaffNumber {
            get => _targetStaffNumber;
            set => _targetStaffNumber = value;
        }
        /// <summary>
        /// 従事者コード1
        /// </summary>
        public int StaffCode1 {
            get => _staffCode1;
            set => _staffCode1 = value;
        }
        /// <summary>
        /// 職種コード1
        /// 10:運転手 11:作業員 20:事務職 99:指定なし
        /// </summary>
        public int StaffOccupation1 {
            get => _staffOccupation1;
            set => _staffOccupation1 = value;
        }
        /// <summary>
        /// 代番フラグ1
        /// true:代番 false:本番
        /// </summary>
        public bool StaffProxyFlag1 {
            get => _staffProxyFlag1;
            set => _staffProxyFlag1 = value;
        }
        /// <summary>
        /// 点呼フラグ1
        /// true:点呼実施 false:点呼未実施
        /// </summary>
        public bool StaffRollCallFlag1 {
            get => _staffRollCallFlag1;
            set => _staffRollCallFlag1 = value;
        }
        /// <summary>
        /// 点呼日時1
        /// </summary>
        public DateTime StaffRollCallYmdHms1 {
            get => _staffRollCallYmdHms1;
            set => _staffRollCallYmdHms1 = value;
        }
        /// <summary>
        /// メモフラグ1
        /// true:メモが存在する false:メモが存在しない
        /// </summary>
        public bool StaffMemoFlag1 {
            get => _staffMemoFlag1;
            set => _staffMemoFlag1 = value;
        }
        /// <summary>
        /// メモ1
        /// </summary>
        public string StaffMemo1 {
            get => _staffMemo1;
            set => _staffMemo1 = value;
        }
        /// <summary>
        /// 従事者コード2
        /// </summary>
        public int StaffCode2 {
            get => _staffCode2;
            set => _staffCode2 = value;
        }
        /// <summary>
        /// 職種コード2
        /// 10:運転手 11:作業員 20:事務職 99:指定なし
        /// </summary>
        public int StaffOccupation2 {
            get => _staffOccupation2;
            set => _staffOccupation2 = value;
        }
        /// <summary>
        /// 代番フラグ2
        /// true:代番 false:本番
        /// </summary>
        public bool StaffProxyFlag2 {
            get => _staffProxyFlag2;
            set => _staffProxyFlag2 = value;
        }
        /// <summary>
        /// 点呼フラグ2
        /// true:点呼実施 false:点呼未実施
        /// </summary>
        public bool StaffRollCallFlag2 {
            get => _staffRollCallFlag2;
            set => _staffRollCallFlag2 = value;
        }
        /// <summary>
        /// 点呼日時2
        /// </summary>
        public DateTime StaffRollCallYmdHms2 {
            get => _staffRollCallYmdHms2;
            set => _staffRollCallYmdHms2 = value;
        }
        /// <summary>
        /// メモフラグ2
        /// true:メモが存在する false:メモが存在しない
        /// </summary>
        public bool StaffMemoFlag2 {
            get => _staffMemoFlag2;
            set => _staffMemoFlag2 = value;
        }
        /// <summary>
        /// メモ2
        /// </summary>
        public string StaffMemo2 {
            get => _staffMemo2;
            set => _staffMemo2 = value;
        }
        /// <summary>
        /// 従事者コード3
        /// </summary>
        public int StaffCode3 {
            get => _staffCode3;
            set => _staffCode3 = value;
        }
        /// <summary>
        /// 職種コード3
        /// 10:運転手 11:作業員 20:事務職 99:指定なし
        /// </summary>
        public int StaffOccupation3 {
            get => _staffOccupation3;
            set => _staffOccupation3 = value;
        }
        /// <summary>
        /// 代番フラグ3
        /// true:代番 false:本番
        /// </summary>
        public bool StaffProxyFlag3 {
            get => _staffProxyFlag3;
            set => _staffProxyFlag3 = value;
        }
        /// <summary>
        /// 点呼フラグ3
        /// true:点呼実施 false:点呼未実施
        /// </summary>
        public bool StaffRollCallFlag3 {
            get => _staffRollCallFlag3;
            set => _staffRollCallFlag3 = value;
        }
        /// <summary>
        /// 点呼日時3
        /// </summary>
        public DateTime StaffRollCallYmdHms3 {
            get => _staffRollCallYmdHms3;
            set => _staffRollCallYmdHms3 = value;
        }
        /// <summary>
        /// メモフラグ3
        /// true:メモが存在する false:メモが存在しない
        /// </summary>
        public bool StaffMemoFlag3 {
            get => _staffMemoFlag3;
            set => _staffMemoFlag3 = value;
        }
        /// <summary>
        /// メモ3
        /// </summary>
        public string StaffMemo3 {
            get => _staffMemo3;
            set => _staffMemo3 = value;
        }
        /// <summary>
        /// 従事者コード4
        /// </summary>
        public int StaffCode4 {
            get => _staffCode4;
            set => _staffCode4 = value;
        }
        /// <summary>
        /// 職種コード4
        /// 10:運転手 11:作業員 20:事務職 99:指定なし
        /// </summary>
        public int StaffOccupation4 {
            get => _staffOccupation4;
            set => _staffOccupation4 = value;
        }
        /// <summary>
        /// 代番フラグ4
        /// true:代番 false:本番
        /// </summary>
        public bool StaffProxyFlag4 {
            get => _staffProxyFlag4;
            set => _staffProxyFlag4 = value;
        }
        /// <summary>
        /// 点呼フラグ4
        /// true:点呼実施 false:点呼未実施
        /// </summary>
        public bool StaffRollCallFlag4 {
            get => _staffRollCallFlag4;
            set => _staffRollCallFlag4 = value;
        }
        /// <summary>
        /// 点呼日時4
        /// </summary>
        public DateTime StaffRollCallYmdHms4 {
            get => _staffRollCallYmdHms4;
            set => _staffRollCallYmdHms4 = value;
        }
        /// <summary>
        /// メモフラグ4
        /// true:メモが存在する false:メモが存在しない
        /// </summary>
        public bool StaffMemoFlag4 {
            get => _staffMemoFlag4;
            set => _staffMemoFlag4 = value;
        }
        /// <summary>
        /// メモ4
        /// </summary>
        public string StaffMemo4 {
            get => _staffMemo4;
            set => _staffMemo4 = value;
        }
        public string InsertPcName {
            get => _insertPcName;
            set => _insertPcName = value;
        }
        public DateTime InsertYmdHms {
            get => _insertYmdHms;
            set => _insertYmdHms = value;
        }
        public string UpdatePcName {
            get => _updatePcName;
            set => _updatePcName = value;
        }
        public DateTime UpdateYmdHms {
            get => _updateYmdHms;
            set => _updateYmdHms = value;
        }
        public string DeletePcName {
            get => _deletePcName;
            set => _deletePcName = value;
        }
        public DateTime DeleteYmdHms {
            get => _deleteYmdHms;
            set => _deleteYmdHms = value;
        }
        public bool DeleteFlag {
            get => _deleteFlag;
            set => _deleteFlag = value;
        }
        /// <summary>
        /// SetMasterVoのNumberOfPeopleを退避
        /// </summary>
        public int NumberOfPeople {
            get => this._numberOfPeople;
            set => this._numberOfPeople = value;
        }
    }
}

