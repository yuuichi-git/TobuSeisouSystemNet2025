/*
 * 2025-04-30
 */
using Dao;

using Vo;

namespace Accounting {
    public partial class AccountingParttimeList : Form {
        /*
         * Dao
         */
        private readonly SetMasterDao _setMasterDao;
        private readonly CarMasterDao _carMasterDao;
        private readonly StaffMasterDao _staffMasterDao;
        private readonly VehicleDispatchDetailDao _vehicleDispatchDetailDao;
        /*
         * Vo
         */
        private readonly List<SetMasterVo> _listSetMasterVo;
        private readonly List<CarMasterVo> _listCarMasterVo;
        private readonly List<StaffMasterVo> _listStaffMasterVo;

        /// <summary>
        /// �R���X�g���N�^�[
        /// </summary>
        /// <param name="connectionVo"></param>
        public AccountingParttimeList(ConnectionVo connectionVo, Screen screen) {
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
            _listSetMasterVo = _setMasterDao.SelectAllSetMaster();
            _listCarMasterVo = _carMasterDao.SelectAllCarMaster();
            _listStaffMasterVo = _staffMasterDao.SelectAllStaffMaster(null, null, null, false);
            /*
             * InitializeControl
             */
            InitializeComponent();
            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemPrint",
                "ToolStripMenuItemPrintA4",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);
            this.DateTimePickerExOperationDate.SetValueJp(DateTime.Now.Date);
            this.InitializeSheetViewList();
            /*
             * Event��o�^����
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// HButtonExUpdate_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            this.InitializeSheetViewList();
            this.SetSheetViewList(_vehicleDispatchDetailDao.SelectAllVehicleDispatchDetail(DateTimePickerExOperationDate.GetValue().Date));
        }


        string _operationName = string.Empty;
        private void SetSheetViewList(List<VehicleDispatchDetailVo> listVehicleDispatchDetailVo) {
            int startRow = 3;
            int startCol = 1;

            // ���t
            SheetViewList.Cells["E2"].Text = this.DateTimePickerExOperationDate.GetValueJp();

            foreach (StaffMasterVo staffMasterVo in _listStaffMasterVo.FindAll(x => x.Belongs == 12 && x.VehicleDispatchTarget == true && x.RetirementFlag == false).OrderBy(x => x.EmploymentDate)) {
                SheetViewList.Cells[startRow, startCol].Text = staffMasterVo.DisplayName;
                VehicleDispatchDetailVo vehicleDispatchDetailVo = listVehicleDispatchDetailVo.Find(x => (x.StaffCode1 == staffMasterVo.StaffCode ||
                                                                                                         x.StaffCode2 == staffMasterVo.StaffCode ||
                                                                                                         x.StaffCode3 == staffMasterVo.StaffCode ||
                                                                                                         x.StaffCode4 == staffMasterVo.StaffCode) &&
                                                                                                         x.OperationDate == this.DateTimePickerExOperationDate.GetValue().Date);
                /*
                 * �z�Ԑ悪�ݒ肳��ĂȂ���StaffLabelEx�����u���Ă���ꍇ���������Ȃ�
                 * �hvehicleDispatchDetailVo.Set_code > 0�h �� ���̕���
                 */
                if (vehicleDispatchDetailVo != null && vehicleDispatchDetailVo.SetCode > 0) {
                    SheetViewList.Cells[startRow, startCol + 1].Text = "�o��";
                    /*
                     * ���O��ݒ�
                     * �@�����{�Ђ͑S�āy�^�]��z�ɂ���i�^�R������˗��j
                     */
                    switch (vehicleDispatchDetailVo.SetCode) {
                        case 1312111: // �����{��
                            _operationName = "�y�^�]��z";
                            break;
                        default:
                            _operationName = vehicleDispatchDetailVo.StaffCode1 == staffMasterVo.StaffCode ? "�y�^�]��z" : "�y��ƈ��z";
                            break;
                    }
                    SheetViewList.Cells[startRow, startCol + 2].Text = string.Concat(_operationName, _listSetMasterVo.Find(x => x.SetCode == vehicleDispatchDetailVo.SetCode).SetName);
                    /*
                     * �Ԏ�
                     */
                    CarMasterVo carMasterVo = _listCarMasterVo.Find(x => x.CarCode == vehicleDispatchDetailVo.CarCode);
                    if (carMasterVo != null && vehicleDispatchDetailVo.StaffCode1 == staffMasterVo.StaffCode) {
                        var carKidName = "";
                        switch (carMasterVo.CarKindCode) {
                            case 10:
                                carKidName = "�y������";
                                break;
                            case 11:
                                carKidName = "���^";
                                break;
                            case 12:
                                carKidName = "����";
                                break;
                        }
                        SheetViewList.Cells[startRow, startCol + 3].Text = carKidName;
                    }
                    /*
                     * �o�Βn
                     */
                    if (vehicleDispatchDetailVo.StaffCode1 == staffMasterVo.StaffCode) {
                        SheetViewList.Cells[startRow, startCol + 4].Text = vehicleDispatchDetailVo.CarGarageCode == 1 ? "�{��" : "�O��";
                    } else {
                        SheetViewList.Cells[startRow, startCol + 4].Text = "�{��";
                    }
                }
                startRow++;
            }
            this.ToolStripStatusLabelDetail.Text = string.Concat(this.DateTimePickerExOperationDate.GetValueJp(), "�̃f�[�^���X�V���܂����B");
        }

        /// <summary>
        /// ToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                // A4�ň������
                case "ToolStripMenuItemPrintA4":
                    //�A�N�e�B�u�V�[�g������܂�
                    SpreadList.PrintSheet(SheetViewList);
                    break;
                // �A�v���P�[�V�������I������
                case "ToolStripMenuItemExit":
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// InitializeSheetViewList
        /// </summary>
        private void InitializeSheetViewList() {
            SheetViewList.Cells["E2"].Text = string.Empty;
            // �w��͈͂̃f�[�^���N���A
            SheetViewList.ClearRange(3, 1, 40, 5, true);
        }
    }
}
