/*
 * 2024/10/03
 */
using ControlEx;

using Dao;

using Vo;

namespace VehicleDispatch {
    public partial class VehicleDispatchBoard : Form {
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
        /// Constructor
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
            _listStaffMasterVo = _staffMasterDao.SelectAllStaffMaster();
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
                    MessageBox.Show("出庫点呼記録を実装する");
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
    }
}
