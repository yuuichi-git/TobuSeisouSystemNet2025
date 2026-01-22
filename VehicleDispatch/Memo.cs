/*
 * 2024-1-2
 */
using ControlEx;

using Dao;

using Vo;

namespace VehicleDispatch {
    public partial class Memo : Form {
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);
        private Control _control;
        /*
         * Dao
         */
        private VehicleDispatchDetailDao _vehicleDispatchDetailDao;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Memo(ConnectionVo connectionVo, Control control) {
            _control = control;
            /*
             * Dao
             */
            _vehicleDispatchDetailDao = new(connectionVo);
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.InitializeControl();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            switch (_control) {
                case SetLabel setLabel:
                    setLabel.Memo = this.TextBoxExMemo.Text;
                    setLabel.MemoFlag = this.TextBoxExMemo.Text.Length > 0 ? true : false;                                                          // SetLabelのフラグをセット(ControlのViewを変化させる)
                    ((SetControl)setLabel.ParentControl).SetMemo = this.TextBoxExMemo.Text;
                    ((SetControl)setLabel.ParentControl).SetMemoFlag = this.TextBoxExMemo.Text.Length > 0 ? true : false;                           // SetControlのフラグをセット
                    ((SetControl)setLabel.ParentControl).DefaultRelocation();                                                                       // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)setLabel.ParentControl).GetVehicleDispatchDetailVo());    // thisのVehicleDispatchDetailVoを取得　SQL発行
                    break;
                case CarLabel carLabel:
                    carLabel.Memo = this.TextBoxExMemo.Text;
                    carLabel.MemoFlag = this.TextBoxExMemo.Text.Length > 0 ? true : false;                                                          // SetControlのフラグをセット
                    ((SetControl)carLabel.ParentControl).CarMemo = this.TextBoxExMemo.Text;
                    ((SetControl)carLabel.ParentControl).CarMemoFlag = this.TextBoxExMemo.Text.Length > 0 ? true : false;                           // SetControlのフラグをセット
                    ((SetControl)carLabel.ParentControl).DefaultRelocation();                                                                       // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)carLabel.ParentControl).GetVehicleDispatchDetailVo());    // thisのVehicleDispatchDetailVoを取得　SQL発行
                    break;
                case StaffLabel staffLabel:
                    staffLabel.Memo = this.TextBoxExMemo.Text;
                    staffLabel.MemoFlag = this.TextBoxExMemo.Text.Length > 0 ? true : false;
                    switch (this.GetStaffNumber(staffLabel)) {
                        case 0: // Staff1
                            ((SetControl)staffLabel.ParentControl).StaffMemo1 = this.TextBoxExMemo.Text;
                            ((SetControl)staffLabel.ParentControl).StaffMemoFlag1 = this.TextBoxExMemo.Text.Length > 0 ? true : false;              // SetControlのフラグをセット
                            break;
                        case 1: // Staff2
                            ((SetControl)staffLabel.ParentControl).StaffMemo2 = this.TextBoxExMemo.Text;
                            ((SetControl)staffLabel.ParentControl).StaffMemoFlag2 = this.TextBoxExMemo.Text.Length > 0 ? true : false;              // SetControlのフラグをセット
                            break;
                        case 2: // Staff3
                            ((SetControl)staffLabel.ParentControl).StaffMemo3 = this.TextBoxExMemo.Text;
                            ((SetControl)staffLabel.ParentControl).StaffMemoFlag3 = this.TextBoxExMemo.Text.Length > 0 ? true : false;              // SetControlのフラグをセット
                            break;
                        case 3: // Staff4
                            ((SetControl)staffLabel.ParentControl).StaffMemo4 = this.TextBoxExMemo.Text;
                            ((SetControl)staffLabel.ParentControl).StaffMemoFlag4 = this.TextBoxExMemo.Text.Length > 0 ? true : false;              // SetControlのフラグをセット
                            break;
                    }
                    ((SetControl)staffLabel.ParentControl).DefaultRelocation();                                                                     // プロパティの再構築
                    _vehicleDispatchDetailDao.UpdateOneVehicleDispatchDetail(((SetControl)staffLabel.ParentControl).GetVehicleDispatchDetailVo());  // thisのVehicleDispatchDetailVoを取得　SQL発行
                    break;
            }
            this.Close();
        }

        /// <summary>
        /// タイムスタンプ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExTimeStamp_Click(object sender, EventArgs e) {
            if (this.TextBoxExMemo.Text.Length > 0) {
                this.TextBoxExMemo.Text += DateTime.Now.ToString(Environment.NewLine + "yyyy年MM月dd日 HH時mm分ss秒：");
            } else {
                this.TextBoxExMemo.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH時mm分ss秒：");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeControl() {
            VehicleDispatchDetailVo vehicleDispatchDetailVo = _vehicleDispatchDetailDao.SelectOneVehicleDispatchDetail(GetCellNumber(_control), GetOperationDate(_control));
            switch (_control) {
                case SetLabel setLabel:
                    if (vehicleDispatchDetailVo.SetMemoFlag)
                        this.TextBoxExMemo.Text = vehicleDispatchDetailVo.SetMemo;
                    break;
                case CarLabel carLabel:
                    if (vehicleDispatchDetailVo.CarMemoFlag)
                        this.TextBoxExMemo.Text = vehicleDispatchDetailVo.CarMemo;
                    break;
                case StaffLabel staffLabel:
                    switch (GetStaffNumber(staffLabel)) {
                        case 0:
                            if (vehicleDispatchDetailVo.StaffMemoFlag1)
                                this.TextBoxExMemo.Text = vehicleDispatchDetailVo.StaffMemo1;
                            break;
                        case 1:
                            if (vehicleDispatchDetailVo.StaffMemoFlag2)
                                this.TextBoxExMemo.Text = vehicleDispatchDetailVo.StaffMemo2;
                            break;
                        case 2:
                            if (vehicleDispatchDetailVo.StaffMemoFlag3)
                                this.TextBoxExMemo.Text = vehicleDispatchDetailVo.StaffMemo3;
                            break;
                        case 3:
                            if (vehicleDispatchDetailVo.StaffMemoFlag4)
                                this.TextBoxExMemo.Text = vehicleDispatchDetailVo.StaffMemo4;
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private int GetCellNumber(Control control) {
            int cellNumber = 999;
            switch (_control) {
                case SetLabel setLabel:
                    cellNumber = ((SetControl)setLabel.ParentControl).CellNumber;
                    break;
                case CarLabel carLabel:
                    cellNumber = ((SetControl)carLabel.ParentControl).CellNumber;
                    break;
                case StaffLabel staffLabel:
                    cellNumber = ((SetControl)staffLabel.ParentControl).CellNumber;
                    break;
            }
            return cellNumber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private DateTime GetOperationDate(Control control) {
            DateTime operationDate = _defaultDateTime;
            switch (_control) {
                case SetLabel setLabel:
                    operationDate = ((SetControl)setLabel.ParentControl).OperationDate;
                    break;
                case CarLabel carLabel:
                    operationDate = ((SetControl)carLabel.ParentControl).OperationDate;
                    break;
                case StaffLabel staffLabel:
                    operationDate = ((SetControl)staffLabel.ParentControl).OperationDate;
                    break;
            }
            return operationDate;
        }

        /// <summary>
        /// StaffLabelがSetControlに配置されている位置を取得する
        /// </summary>
        /// <param name="staffLabel"></param>
        /// <returns>CellNumber</returns>
        private int GetStaffNumber(StaffLabel staffLabel) {
            TableLayoutPanelCellPosition tableLayoutPanelCellPosition = ((SetControl)staffLabel.ParentControl).GetCellPosition(staffLabel);
            return tableLayoutPanelCellPosition.Column * 2 + (tableLayoutPanelCellPosition.Row - 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Memo_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
