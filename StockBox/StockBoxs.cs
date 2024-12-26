/*
 * 2024-11-24
 */
using ControlEx;

using Dao;

using Vo;

namespace StockBox {
    public partial class StockBoxs : Form {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        /*
         * 参照
         */
        private Board _board;
        private StockBoxPanel _stockBoxPanel;
        /*
         * Dao
         */
        private readonly SetMasterDao _setMasterDao;
        private readonly CarMasterDao _carMasterDao;
        private readonly StaffMasterDao _staffMasterDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        private List<SetMasterVo> _listSetMasterVoForMasterData;
        private List<CarMasterVo> _listCarMasterVoForMasterData;
        private List<StaffMasterVo> _listStaffMasterVoForMasterData;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="board"></param>
        public StockBoxs(ConnectionVo connectionVo, Board board) {
            /*
             * 参照
             */
            _board = board;
            /*
             * Dao
             */
            _setMasterDao = new(connectionVo);
            _carMasterDao = new(connectionVo);
            _staffMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            _listSetMasterVoForMasterData = _setMasterDao.SelectAllSetMaster();
            _listCarMasterVoForMasterData = _carMasterDao.SelectAllHCarMaster();
            _listStaffMasterVoForMasterData = _staffMasterDao.SelectAllStaffMaster(null, null, null, false);
            /*
             * InitializeControl
             */
            InitializeComponent();
            /*
             * Form
             */
            this.Opacity = 0.9;
            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemHelp"
            };
            MenuStripEx1.ChangeEnable(listString);
            /*
             * StockBoxPanelBase
             */
            _stockBoxPanel = new();

            this.TableLayoutPanelExBase.Controls.Add(_stockBoxPanel, 0, 2);

            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "InitializeSuccess";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEx_Click(object sender, EventArgs e) {
            switch (((ButtonEx)sender).Name) {
                case "ButtonExSet":
                    try {
                        this.RemoveControls();
                        this._stockBoxPanel.Controls.AddRange(GetArraySetLabel());
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "ButtonExCar":
                    try {
                        this.RemoveControls();
                        this._stockBoxPanel.Controls.AddRange(GetArrayCarLabel(_board.GetAllCarLabel()));
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "ButtonExFullTime":
                    try {
                        this.RemoveControls();
                        MessageBox.Show("ButtonExFullTime");
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "ButtonExPartTime":
                    try {
                        this.RemoveControls();
                        MessageBox.Show("ButtonExPartTime");
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "ButtonExLongTime":
                    try {
                        this.RemoveControls();
                        MessageBox.Show("ButtonExLongTime");
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "ButtonExShortTime":
                    try {
                        this.RemoveControls();
                        MessageBox.Show("ButtonExShortTime");
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "ButtonExDispatch":
                    try {
                        this.RemoveControls();
                        MessageBox.Show("ButtonExDispatch");
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
            }
        }

        /*
         * 
         * SetLabel
         * 
         */
        /// <summary>
        /// SetLabelを取得する
        /// </summary>
        /// <returns></returns>
        public SetLabel[] GetArraySetLabel() {
            List<SetMasterVo> listSetMasterVo = _listSetMasterVoForMasterData.FindAll(x => x.ClassificationCode > 11).OrderBy(x => x.ClassificationCode).ThenBy(x => x.WordCode).ToList(); // ClassificationCode 10/11 は表示しない(雇上・区契)
            SetLabel[] _arrayControl = new SetLabel[listSetMasterVo.Count];
            int i = 0;
            foreach (SetMasterVo setMasterVo in listSetMasterVo) {
                _arrayControl[i] = GetOneSetLabel(setMasterVo);
                i++;
            }
            return _arrayControl;
        }

        private List<int> CreateSetCodeList(List<SetMasterVo> listSetMasterVo) {
            List<int> list = new();
            foreach (SetMasterVo setMasterVo in listSetMasterVo)
                list.Add(setMasterVo.SetCode);
            return list;
        }

        public SetLabel GetOneSetLabel(SetMasterVo setMasterVo) {
            SetLabel setLabel = new(setMasterVo);
            setLabel.ParentControl = this._stockBoxPanel;
            setLabel.AddWorkerFlag = false;
            setLabel.ClassificationCode = setMasterVo.ClassificationCode;
            setLabel.ContactInfomationFlag = false;
            setLabel.LastRollCallFlag = false;
            setLabel.LastRollCallYmdHms = _defaultDateTime;
            setLabel.ManagedSpaceCode = 1;
            setLabel.Memo = string.Empty;
            setLabel.MemoFlag = false;
            setLabel.OperationFlag = true;
            setLabel.ShiftCode = 0;
            setLabel.StandByFlag = false;
            setLabel.TelCallingFlag = false; // 電話連絡(2024-12-11の時点では、電話連絡確認機能は利用していない
            setLabel.FaxTransmissionFlag = false;
            //// Eventを登録
            //setLabel.SetLabel_ContextMenuStrip_Opened += ContextMenuStrip_Opened;
            //setLabel.SetLabel_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
            //setLabel.SetLabel_OnMouseClick += OnMouseClick;
            //setLabel.SetLabel_OnMouseDoubleClick += OnMouseDoubleClick;
            //setLabel.SetLabel_OnMouseDown += OnMouseDown;
            //setLabel.MouseMove += OnMouseMove;
            //setLabel.MouseUp += OnMouseUp;
            return setLabel;
        }

        /*
         * 
         * CarLabel
         * 
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listCarMasterVo">SetControl上に配置されているCarMasterVoの一覧</param>
        /// <returns></returns>
        public CarLabel[] GetArrayCarLabel(List<CarMasterVo> listCarMasterVo) {
            List<CarMasterVo> newListCarMasterVo = _listCarMasterVoForMasterData.Where(x => !CreateCarCodeList(listCarMasterVo).Contains(x.CarCode)).ToList();
            CarLabel[] _arrayControl = new CarLabel[newListCarMasterVo.Count];
            int i = 0;
            foreach (CarMasterVo carMasterVo in newListCarMasterVo.FindAll(x => x.DeleteFlag == false)) {
                _arrayControl[i] = GetOneCarLabel(carMasterVo);
                i++;
            }
            return _arrayControl;
        }

        private List<int> CreateCarCodeList(List<CarMasterVo> listCarMasterVo) {
            List<int> list = new();
            foreach (CarMasterVo carMasterVo in listCarMasterVo)
                list.Add(carMasterVo.CarCode);
            return list;
        }

        public CarLabel GetOneCarLabel(CarMasterVo carMasterVo) {
            CarLabel carLabel = new(carMasterVo);
            carLabel.ParentControl = this._stockBoxPanel;
            carLabel.ClassificationCode = carMasterVo.ClassificationCode; // 車両登録がされているのでCarMasterVoのデータ
            carLabel.CarGarageCode = carMasterVo.GarageCode;
            carLabel.Memo = string.Empty;
            carLabel.MemoFlag = false;
            carLabel.ProxyFlag = false;
            //// Eventを登録
            //carLabel.CarLabel_ContextMenuStrip_Opened += ContextMenuStrip_Opened;
            //carLabel.CarLabel_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
            //carLabel.CarLabel_OnMouseClick += OnMouseClick;
            //carLabel.CarLabel_OnMouseDoubleClick += OnMouseDoubleClick;
            //carLabel.CarLabel_OnMouseDown += OnMouseDown;
            //carLabel.MouseMove += OnMouseMove;
            //carLabel.MouseUp += OnMouseUp;
            return carLabel;
        }

        /*
         * 
         * StaffLabel
         * 
         */
        public StaffLabel[] GetArrayStaffLabel(List<StaffMasterVo> listStaffMasterVo) {
            List<StaffMasterVo> newListStaffMasterVo = _listStaffMasterVoForMasterData.Where(x => !CreateStaffCodeList(listStaffMasterVo).Contains(x.StaffCode)).ToList();
            StaffLabel[] _arrayControl = new StaffLabel[newListStaffMasterVo.Count];
            int i = 0;
            foreach (StaffMasterVo staffMasterVo in newListStaffMasterVo) {
                _arrayControl[i] = GetOneStaffLabel(staffMasterVo);
                i++;
            }
            return _arrayControl;
        }

        private List<int> CreateStaffCodeList(List<StaffMasterVo> listStaffMasterVo) {
            List<int> list = new();
            foreach (StaffMasterVo staffMasterVo in listStaffMasterVo)
                list.Add(staffMasterVo.StaffCode);
            return list;
        }

        public StaffLabel GetOneStaffLabel(StaffMasterVo staffMasterVo) {
            StaffLabel staffLabel = new(staffMasterVo);
            staffLabel.ParentControl = this._stockBoxPanel;
            staffLabel.OccupationCode = staffMasterVo.Occupation;
            staffLabel.Memo = string.Empty;
            staffLabel.MemoFlag = false;
            staffLabel.ProxyFlag = false;
            staffLabel.RollCallFlag = false;
            staffLabel.RollCallYmdHms = _defaultDateTime;
            //// Eventを登録
            //staffLabel.StaffLabel_ContextMenuStrip_Opened += ContextMenuStrip_Opened;
            //staffLabel.StaffLabel_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
            //staffLabel.StaffLabel_OnMouseClick += OnMouseClick;
            //staffLabel.StaffLabel_OnMouseDoubleClick += OnMouseDoubleClick;
            //staffLabel.StaffLabel_OnMouseDown += OnMouseDown;
            //staffLabel.MouseMove += OnMouseMove;
            //staffLabel.MouseUp += OnMouseUp;
            return staffLabel;
        }

        /// <summary>
        /// 
        /// </summary>
        public void RemoveControls() {
            /*
             * メソッドをClear呼び出してもコントロール ハンドルはメモリから削除されません。 メモリリークを回避するにはメソッドをDispose明示的に呼び出す必要があります。
             * ※後ろから解放している点が重要らしい。
             */
            for (int i = this._stockBoxPanel.Controls.Count - 1; 0 <= i; i--)
                this._stockBoxPanel.Controls[i].Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StockBoxs_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
