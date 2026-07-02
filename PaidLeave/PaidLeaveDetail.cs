/*
 * 2026-06-17
 */
using Common;

using Dao;

using Vo;

namespace PaidLeave {
    public partial class PaidLeaveDetail : Form {
        private readonly DateTime _defaultDateTime = new(1900,1,1);
        private readonly DateUtility _dateUtility = new();
        DateTime _mostRecentCommencementDate;
        /*
         * Dao
         */
        private readonly VehicleDispatchDetailDao _vehicleDispatchDetailDao;
        private readonly PaidLeaveEntitlementDao _paidLeaveEntitlementDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        private readonly StaffMasterVo _staffMasterVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="staffMasterVo"></param>
        public PaidLeaveDetail(ConnectionVo connectionVo, StaffMasterVo staffMasterVo) {
            /*
             * Dao
             */
            _vehicleDispatchDetailDao = new(connectionVo);
            _paidLeaveEntitlementDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            _staffMasterVo = staffMasterVo;
            /*
             * InitializeControls
             */
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaidLeaveDetail_Load(object sender, EventArgs e) {
            MostRecentCommencementDate = _dateUtility.GetPaidLeaveCommencementDate(_staffMasterVo.PaidLeaveCommencementDate);
            /*
             * InitializeControls　
             */
            // コンストラクター内での処理では無効になるよ
            switch(this.Owner) {
                case PaidLeaveList:
                    this.CcNumericUpDownYearsOfService.Enabled = true;
                    this.CcNumericUpDownGrantedDays.Enabled = true;
                    this.CcButtonUpdate.Enabled = true;
                    break;
                default:
                    this.CcNumericUpDownYearsOfService.Enabled = false;
                    this.CcNumericUpDownGrantedDays.Enabled = false;
                    this.CcButtonUpdate.Enabled = false;
                    break;
            }
            // １年間の勤務日数を計算
            string numberOfWorkingDays = string.Concat(MostRecentCommencementDate.AddYears(-1).Date.ToString("yyyy/MM/dd"), "～", MostRecentCommencementDate.AddDays(-1).Date.ToString("yyyy/MM/dd"), "までのの勤務日数：",
                                                       _vehicleDispatchDetailDao.GetCountVehicleDispatchDetail(MostRecentCommencementDate.AddYears(-1).Date, MostRecentCommencementDate.AddDays(-1).Date, _staffMasterVo.StaffCode), "日出勤");
            this.CcLabelNumberOfWorkingDays.Text = numberOfWorkingDays;
            // 従事者名
            this.CcLabelStaffName.Text = _staffMasterVo.Name;
            // 勤務年数
            this.CcNumericUpDownYearsOfService.Value = _paidLeaveEntitlementDao.GetYearsOfService(_staffMasterVo.StaffCode, MostRecentCommencementDate);
            // 有給基準日
            this.CcLabelPaidLeaveReferenceDate.Text = _staffMasterVo.PaidLeaveReferenceDate.ToString("yyyy/MM/dd");
            // 直近の起算日
            this.CcLabelPaidLeaveCommencementDate.Text = MostRecentCommencementDate.ToString("yyyy/MM/dd");
            // 直近の付与日数を取得
            this.CcNumericUpDownGrantedDays.Value = _paidLeaveEntitlementDao.GetGrantedDays(_staffMasterVo.StaffCode, MostRecentCommencementDate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcButtonUpdate_Click(object sender, EventArgs e) {
            if(_paidLeaveEntitlementDao.ExistenceHPaidLeaveEntitlement(_staffMasterVo.StaffCode, MostRecentCommencementDate.Date)) {
                /*
                 * UPDATE
                 */
                PaidLeaveEntitlementV0 paidLeaveEntitlementV0 = new();
                // 従事者名
                paidLeaveEntitlementV0.StaffCode = _staffMasterVo.StaffCode;
                // 勤続年数
                paidLeaveEntitlementV0.YearsOfService = Convert.ToInt32(this.CcNumericUpDownYearsOfService.Value);
                // 直近の起算日
                paidLeaveEntitlementV0.StartDate = MostRecentCommencementDate.Date;
                // 有給付与日数
                paidLeaveEntitlementV0.GrantedDays = Convert.ToInt32(this.CcNumericUpDownGrantedDays.Value);
                // 備考
                paidLeaveEntitlementV0.Remarks = string.Empty;

                paidLeaveEntitlementV0.UpdatePcName = Environment.MachineName;
                paidLeaveEntitlementV0.UpdateYmdHms = DateTime.Now;

                DialogResult dialogResult = MessageBox.Show($"{_staffMasterVo.DisplayName} の情報を更新します。よろしいですか？","メッセージ",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                switch(dialogResult) {
                    case DialogResult.OK:
                        _paidLeaveEntitlementDao.UpdateOnePaidLeaveEntitlementV0(paidLeaveEntitlementV0);
                        Close();
                        break;
                    case DialogResult.Cancel:
                        break;
                }

            } else {
                /*
                 * INSERT
                 */
                PaidLeaveEntitlementV0 paidLeaveEntitlementV0 = new();
                // 従事者名
                paidLeaveEntitlementV0.StaffCode = _staffMasterVo.StaffCode;
                // 勤続年数
                paidLeaveEntitlementV0.YearsOfService = Convert.ToInt32(this.CcNumericUpDownYearsOfService.Value);
                // 直近の起算日
                paidLeaveEntitlementV0.StartDate = MostRecentCommencementDate.Date;
                // 有給付与日数
                paidLeaveEntitlementV0.GrantedDays = Convert.ToInt32(this.CcNumericUpDownGrantedDays.Value);
                // 備考
                paidLeaveEntitlementV0.Remarks = string.Empty;

                paidLeaveEntitlementV0.InsertPcName = Environment.MachineName;
                paidLeaveEntitlementV0.InsertYmdHms = DateTime.Now;
                paidLeaveEntitlementV0.UpdatePcName = string.Empty;
                paidLeaveEntitlementV0.UpdateYmdHms = _defaultDateTime;
                paidLeaveEntitlementV0.DeletePcName = string.Empty;
                paidLeaveEntitlementV0.DeleteYmdHms = _defaultDateTime;
                paidLeaveEntitlementV0.DeleteFlag = false;
                _paidLeaveEntitlementDao.InsertOnePaidLeaveEntitlementV0(paidLeaveEntitlementV0);

                Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaidLeaveDetail_FormClosing(object sender, FormClosingEventArgs e) {

        }


        /// <summary>
        /// 直近の起算日
        /// </summary>
        public DateTime MostRecentCommencementDate {
            get {
                return _mostRecentCommencementDate;
            }
            set {
                _mostRecentCommencementDate = value;
            }
        }
    }
}
