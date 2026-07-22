/*
 * 2026-06-17
 */
using System.Text;

using Common;

using Dao;

using Vo;

namespace PaidLeave {
    public partial class PaidLeaveDetail : Form {
        private readonly DateTime _defaultDateTime = new(1900,1,1);
        private readonly DateUtility _dateUtility = new();
        private readonly HolidayUtility _holidayUtility = new();
        private DateTime _mostRecentCommencementDate;
        private Dictionary<DateTime, string> _holidaySet = new ();
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
            /*
             *直近の起算日を取得
             */
            MostRecentCommencementDate = _dateUtility.GetPaidLeaveCommencementDate(_staffMasterVo.PaidLeaveCommencementDate);
            /*
             * 祝祭日を取得し、会社指定休日を追加する
             */
            HolidaySet = _holidayUtility.GetHoliday();
            HolidaySet.Add(new DateTime(DateTime.Now.AddYears(-2).Year, 12, 29), "会社指定休日");
            HolidaySet.Add(new DateTime(DateTime.Now.AddYears(-2).Year, 12, 30), "会社指定休日");
            HolidaySet.Add(new DateTime(DateTime.Now.AddYears(-2).Year, 12, 31), "会社指定休日");

            HolidaySet.Add(new DateTime(DateTime.Now.AddYears(-1).Year, 1, 2), "会社指定休日");
            HolidaySet.Add(new DateTime(DateTime.Now.AddYears(-1).Year, 1, 3), "会社指定休日");
            HolidaySet.Add(new DateTime(DateTime.Now.AddYears(-1).Year, 12, 29), "会社指定休日");
            HolidaySet.Add(new DateTime(DateTime.Now.AddYears(-1).Year, 12, 30), "会社指定休日");
            HolidaySet.Add(new DateTime(DateTime.Now.AddYears(-1).Year, 12, 31), "会社指定休日");

            HolidaySet.Add(new DateTime(DateTime.Now.Year, 1, 2), "会社指定休日");
            HolidaySet.Add(new DateTime(DateTime.Now.Year, 1, 3), "会社指定休日");
            HolidaySet.Add(new DateTime(DateTime.Now.Year, 12, 29), "会社指定休日");
            HolidaySet.Add(new DateTime(DateTime.Now.Year, 12, 30), "会社指定休日");
            HolidaySet.Add(new DateTime(DateTime.Now.Year, 12, 31), "会社指定休日");

            HolidaySet.Add(new DateTime(DateTime.Now.AddYears(1).Year, 1, 2), "会社指定休日");
            HolidaySet.Add(new DateTime(DateTime.Now.AddYears(1).Year, 1, 3), "会社指定休日");
            HolidaySet.Add(new DateTime(DateTime.Now.AddYears(1).Year, 12, 29), "会社指定休日");
            HolidaySet.Add(new DateTime(DateTime.Now.AddYears(1).Year, 12, 30), "会社指定休日");
            HolidaySet.Add(new DateTime(DateTime.Now.AddYears(1).Year, 12, 31), "会社指定休日");
            /*
             * 休日一覧を表示
             */
            StringBuilder stringBuilder = new ();
            foreach(var holiday in HolidaySet.Where(x => x.Key >= DateTime.Today.AddYears(-2)).OrderBy(x => x.Key))
                stringBuilder.AppendFormat("{0:yyyy/MM/dd}：{1}\r\n", holiday.Key, holiday.Value);
            this.CcTextBox1.Text = stringBuilder.ToString();

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

            /*
             * 各パラメータを取得
             */
            int 休日日数半年間 = _holidayUtility.GetWorkingDays(MostRecentCommencementDate.AddMonths(-6).Date,
                                                              MostRecentCommencementDate.AddDays(-1).Date,
                                                              HolidaySet);
            int 休日日数１年間 = _holidayUtility.GetWorkingDays(MostRecentCommencementDate.AddYears(-1).Date,
                                                              MostRecentCommencementDate.AddDays(-1).Date,
                                                              HolidaySet);
            int 所定労働日数半年間 = 183 - 休日日数半年間;
            int 所定労働日数１年間 = 365 - 休日日数１年間;

            // 半年間の勤務日数を計算
            int numberOfWorkingHalfDays = _vehicleDispatchDetailDao.GetCountVehicleDispatchDetail(MostRecentCommencementDate.AddMonths(-6).Date,
                                                                                                  MostRecentCommencementDate.AddDays(-1).Date,
                                                                                                  _staffMasterVo.StaffCode);
            // 半年間の勤務日数を表示
            this.CcGroupBox半年間.Text = string.Concat("半年間：", 
                                                      MostRecentCommencementDate.AddMonths(-6).Date.ToString("yyyy/MM/dd"), "～",
                                                      MostRecentCommencementDate.AddDays(-1).Date.ToString("yyyy/MM/dd"), "まで");
            this.CcLabelNumberOfWorkingHalfDays.Text = string.Concat("勤務日数：", numberOfWorkingHalfDays, "日出勤");
            // 半年間の所定労働日数と休日・祭日・指定休日の合計を表示
            this.CcLabel所定労働日数半年間.Text = string.Concat("所定労働日数（日曜日/祝祭日/会社指定日を除く）：", 所定労働日数半年間, "日");
            this.CcLabel休日日数半年間.Text = string.Concat("休日・祭日・指定休日の合計：", 休日日数半年間, "日");

            // 半年間の稼働率を計算
            this.CcLabel出勤率半年間.Text = string.Concat("出勤率 = 出勤した日数(", numberOfWorkingHalfDays, ")÷ 所定労働日数(", 所定労働日数半年間, ") = ",
                                                        CalculateAttendanceRate(numberOfWorkingHalfDays, 所定労働日数半年間).ToString("P2"));





            // １年間の勤務日数を計算
            int numberOfWorkingFullDays = _vehicleDispatchDetailDao.GetCountVehicleDispatchDetail(MostRecentCommencementDate.AddYears(-1).Date,
                                                                                                  MostRecentCommencementDate.AddDays(-1).Date,
                                                                                                  _staffMasterVo.StaffCode);
            // １年間の勤務日数を表示
            this.CcGroupBox１年間.Text = string.Concat("１年間：", 
                                                      MostRecentCommencementDate.AddYears(-1).Date.ToString("yyyy/MM/dd"), "～",
                                                      MostRecentCommencementDate.AddDays(-1).Date.ToString("yyyy/MM/dd"), "まで");
            this.CcLabelNumberOfWorkingFullDays.Text = string.Concat("勤務日数：", numberOfWorkingFullDays, "日出勤");
            // １年間の所定労働日数と休日・祭日・指定休日の合計を表示
            this.CcLabel所定労働日数１年間.Text = string.Concat("所定労働日数（日曜日/祝祭日/会社指定日を除く）：", 所定労働日数１年間, "日");
            this.CcLabel休日日数１年間.Text = string.Concat("休日・祭日・指定休日の合計：", 休日日数１年間, "日");

            // １年間の稼働率を計算
            this.CcLabel出勤率１年間.Text = string.Concat("出勤率 = 出勤した日数(", numberOfWorkingFullDays, ")÷ 所定労働日数(", 所定労働日数１年間, ") = ",
                                                        CalculateAttendanceRate(numberOfWorkingFullDays, 所定労働日数１年間).ToString("P2"));

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
        /// <param name="workedDays"></param>
        /// <param name="scheduledDays"></param>
        /// <returns></returns>
        public double CalculateAttendanceRate(int workedDays, int scheduledDays) {
            if(scheduledDays == 0)
                return 0d;
            return (double)workedDays / (double)scheduledDays;
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

        // 祝祭日を格納
        public Dictionary<DateTime, string> HolidaySet {
            get {
                return _holidaySet;
            }
            set {
                _holidaySet = value;
            }
        }
    }
}
