/*
 * 2024-11-10
 */
using Common;

using Dao;

using Vo;

namespace EmploymentAgreement {
    public partial class EmploymentAgreementPaper : Form {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        /*
         * インスタンス作成
         */
        private readonly DateUtility _date = new();
        /*
         * Dao
         */
        private BelongsMasterDao _belongsMasterDao;
        private JobDescriptionMasterDao _jobDescriptionMasterDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;
        private EmploymentAgreementVo _employmentAgreementVo;
        private StaffMasterVo _staffMasterVo;
        /*
         * Dictionary
         */
        private readonly Dictionary<int, string> _dictionaryBelongs = new();
        private readonly Dictionary<int, string> _dictionaryJobDescription = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="staffMasterVo"></param>
        /// <param name="employmentAgreementVo"></param>
        public EmploymentAgreementPaper(ConnectionVo connectionVo, int code, StaffMasterVo staffMasterVo, EmploymentAgreementVo employmentAgreementVo) {
            /*
             * Dao
             */
            _belongsMasterDao = new(connectionVo);
            _jobDescriptionMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            _staffMasterVo = staffMasterVo;
            _employmentAgreementVo = employmentAgreementVo;
            /*
             * Dictionary
             */
            foreach (BelongsMasterVo belongsMasterVo in _belongsMasterDao.SelectAllBelongsMaster())
                _dictionaryBelongs.Add(belongsMasterVo.Code, belongsMasterVo.Name);
            foreach (JobDescriptionMasterVo jobDescriptionMasterVo in _jobDescriptionMasterDao.SelectAllJobDescriptionMaster())
                _dictionaryJobDescription.Add(jobDescriptionMasterVo.Code, jobDescriptionMasterVo.Name);
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.ComboBoxExBaseAddress.Text = _employmentAgreementVo.BaseLocation;
            this.LabelExCurrentAddress.Text = _staffMasterVo.CurrentAddress;

            /// <summary>
            /// 契約書識別コード
            /// 10:長期雇用契約
            /// 11:短期雇用契約
            /// 20:継続アルバイト契約
            /// 21:体験アルバイト契約
            /// 22:嘱託雇用契約社員
            /// 23:パートタイマー
            /// 30:誓約書
            /// 40:失墜行為確認書
            /// 50:満了一カ月前通知
            switch (code) {
                case 10:

                    break;
                case 11:

                    break;
                case 20:
                    this.SpreadList.ActiveSheetIndex = 1;
                    this.PutContractExpirationPartTimeJob();
                    break;
                case 21:
                    this.SpreadList.ActiveSheetIndex = 0;
                    this.PutExpirationJob();
                    break;
                case 22:
                    this.SpreadList.ActiveSheetIndex = 2;
                    this.PutContractExpirationPartTimeEmployee();
                    break;
                case 23:
                    this.SpreadList.ActiveSheetIndex = 3;
                    this.PutContractExpirationPartTimer();
                    break;
                case 30:

                    break;
                case 40:

                    break;
                case 50:

                    break;

            }


        }


        /// <summary>
        /// 体験アルバイト契約
        /// </summary>
        private void PutExpirationJob() {
            // 【使用者】事業場所在地
            this.SheetView体験期間契約.Cells[5, 6].Text = ComboBoxExBaseAddress.Text;
            // 【労働者】住所
            this.SheetView体験期間契約.Cells[5, 23].Text = _staffMasterVo.CurrentAddress;
            // 【労働者】フリガナ
            this.SheetView体験期間契約.Cells[7, 23].Text = _staffMasterVo.OtherNameKana;
            // 【労働者】生年月日
            this.SheetView体験期間契約.Cells[10, 23].Text = string.Concat(StaffMasterVo.BirthDate.ToString("yyyy年MM月dd日"), " (", _date.GetAge(_staffMasterVo.BirthDate), "歳)");
            // 雇用形態
            this.SheetView体験期間契約.Cells[11, 8].Text = _dictionaryBelongs[_staffMasterVo.Belongs];
            // 契約期間
            this.SheetView体験期間契約.Cells[12, 9].Text = _employmentAgreementVo.ContractExpirationPeriodString;
            // 従事すべき業務の内容
            this.SheetView体験期間契約.Cells[20, 9].Text = _dictionaryJobDescription[_employmentAgreementVo.JobDescription];
            // 始業・就業の時刻
            this.SheetView体験期間契約.Cells[22, 9].Text = _employmentAgreementVo.WorkTime;
            // 休憩時間
            this.SheetView体験期間契約.Cells[24, 9].Text = _employmentAgreementVo.BreakTime;
            // 給料区分
            this.SheetView体験期間契約.Cells[28, 9].Text = _employmentAgreementVo.PayDetail;
            // 給料金額
            this.SheetView体験期間契約.Cells[28, 30].Value = _employmentAgreementVo.Pay != 0 ? _employmentAgreementVo.Pay : "---";
            // 交通費
            this.SheetView体験期間契約.Cells[29, 30].Value = _employmentAgreementVo.TravelCost;
        }

        /// <summary>
        /// 長期アルバイト契約
        /// </summary>
        private void PutContractExpirationPartTimeJob() {
            // 【使用者】事業場所在地
            this.SheetViewアルバイト契約.Cells[5, 6].Text = ComboBoxExBaseAddress.Text;
            // 【労働者】住所
            this.SheetViewアルバイト契約.Cells[5, 23].Text = _staffMasterVo.CurrentAddress;
            // 【労働者】フリガナ
            this.SheetViewアルバイト契約.Cells[7, 23].Text = _staffMasterVo.OtherNameKana;
            // 【労働者】生年月日
            this.SheetViewアルバイト契約.Cells[10, 23].Text = string.Concat(StaffMasterVo.BirthDate.ToString("yyyy年MM月dd日"), " (", _date.GetAge(_staffMasterVo.BirthDate), "歳)");
            // 雇用形態
            this.SheetViewアルバイト契約.Cells[11, 8].Text = _dictionaryBelongs[_staffMasterVo.Belongs];
            // 契約期間
            this.SheetViewアルバイト契約.Cells[12, 9].Text = _employmentAgreementVo.ContractExpirationPeriodString;
            // 従事すべき業務の内容
            this.SheetViewアルバイト契約.Cells[21, 9].Text = _dictionaryJobDescription[_employmentAgreementVo.JobDescription];
            // 始業・就業の時刻
            this.SheetViewアルバイト契約.Cells[23, 9].Text = _employmentAgreementVo.WorkTime;
            // 休憩時間
            this.SheetViewアルバイト契約.Cells[25, 9].Text = _employmentAgreementVo.BreakTime;
            // 給料区分
            this.SheetViewアルバイト契約.Cells[29, 9].Text = _employmentAgreementVo.PayDetail;
            // 給料金額
            this.SheetViewアルバイト契約.Cells[29, 30].Value = _employmentAgreementVo.Pay != 0 ? _employmentAgreementVo.Pay : "---";
            // 交通費
            this.SheetViewアルバイト契約.Cells[30, 30].Value = _employmentAgreementVo.TravelCost;
        }

        /// <summary>
        /// 嘱託雇用契約社員
        /// </summary>
        private void PutContractExpirationPartTimeEmployee() {
            // 【使用者】事業場所在地
            this.SheetView嘱託雇用契約社員.Cells[5, 6].Text = ComboBoxExBaseAddress.Text;
            // 【労働者】住所
            this.SheetView嘱託雇用契約社員.Cells[5, 23].Text = _staffMasterVo.CurrentAddress;
            // 【労働者】フリガナ
            this.SheetView嘱託雇用契約社員.Cells[7, 23].Text = _staffMasterVo.OtherNameKana;
            // 【労働者】生年月日
            this.SheetView嘱託雇用契約社員.Cells[10, 23].Text = string.Concat(StaffMasterVo.BirthDate.ToString("yyyy年MM月dd日"), " (", _date.GetAge(_staffMasterVo.BirthDate), "歳)");
            // 雇用形態
            this.SheetView嘱託雇用契約社員.Cells[11, 8].Text = _dictionaryBelongs[_staffMasterVo.Belongs];
            // 契約期間
            this.SheetView嘱託雇用契約社員.Cells[12, 9].Text = _employmentAgreementVo.ContractExpirationPeriodString;
            // 従事すべき業務の内容
            this.SheetView嘱託雇用契約社員.Cells[21, 9].Text = _dictionaryJobDescription[_employmentAgreementVo.JobDescription];
            // 始業・就業の時刻
            this.SheetView嘱託雇用契約社員.Cells[23, 9].Text = _employmentAgreementVo.WorkTime;
            // 休憩時間
            this.SheetView嘱託雇用契約社員.Cells[25, 9].Text = _employmentAgreementVo.BreakTime;
            // 給料区分
            this.SheetView嘱託雇用契約社員.Cells[32, 9].Text = _employmentAgreementVo.PayDetail;
            // 給料金額
            this.SheetView嘱託雇用契約社員.Cells[32, 30].Value = _employmentAgreementVo.Pay != 0 ? _employmentAgreementVo.Pay : "---";
            // 交通費
            this.SheetView嘱託雇用契約社員.Cells[35, 30].Value = _employmentAgreementVo.TravelCost != 0 ? _employmentAgreementVo.TravelCost : "---";
        }

        /// <summary>
        /// パートタイマー
        /// </summary>
        private void PutContractExpirationPartTimer() {
            // 【使用者】事業場所在地
            this.SheetViewパートタイマー.Cells[5, 6].Text = ComboBoxExBaseAddress.Text;
            // 【労働者】住所
            this.SheetViewパートタイマー.Cells[5, 23].Text = _staffMasterVo.CurrentAddress;
            // 【労働者】フリガナ
            this.SheetViewパートタイマー.Cells[7, 23].Text = _staffMasterVo.OtherNameKana;
            // 【労働者】生年月日
            this.SheetViewパートタイマー.Cells[10, 23].Text = string.Concat(StaffMasterVo.BirthDate.ToString("yyyy年MM月dd日"), " (", _date.GetAge(_staffMasterVo.BirthDate), "歳)");
            // 雇用形態
            this.SheetViewパートタイマー.Cells[11, 8].Text = _dictionaryBelongs[_staffMasterVo.Belongs];
            // 契約期間
            this.SheetViewパートタイマー.Cells[12, 9].Text = _employmentAgreementVo.ContractExpirationPeriodString;
            // 従事すべき業務の内容
            this.SheetViewパートタイマー.Cells[20, 9].Text = _dictionaryJobDescription[_employmentAgreementVo.JobDescription];
            // 始業・就業の時刻
            this.SheetViewパートタイマー.Cells[22, 9].Text = _employmentAgreementVo.WorkTime;
            // 休憩時間
            this.SheetViewパートタイマー.Cells[24, 9].Text = _employmentAgreementVo.BreakTime;
            // 給料区分
            this.SheetViewパートタイマー.Cells[28, 9].Text = _employmentAgreementVo.PayDetail;
            // 給料金額
            this.SheetViewパートタイマー.Cells[28, 30].Value = _employmentAgreementVo.Pay != 0 ? _employmentAgreementVo.Pay : "---";
        }

        private void ButtonExPrint_Click(object sender, EventArgs e) {
            SpreadList.PrintSheet(SpreadList.ActiveSheet);
        }

        private void ComboBoxExBaseAddress_SelectedIndexChanged(object sender, EventArgs e) {
            // 【使用者】事業場所在地
            this.SpreadList.ActiveSheet.Cells[5, 6].Text = this.ComboBoxExBaseAddress.Text;
        }

        /*
         * Setter Getter
         */
        public StaffMasterVo StaffMasterVo {
            get => this._staffMasterVo;
            set => this._staffMasterVo = value;
        }
    }
}
