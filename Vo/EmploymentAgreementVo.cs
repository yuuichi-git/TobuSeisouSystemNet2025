/*
 * 2024-11-12
 */
namespace Vo {
    public class EmploymentAgreementVo {
        private DateTime _defaultDateTime = new(1900, 01, 01);
        private int _staffCode; //従事者コード
        private string _baseLocation; // 勤務地
        private int _occupation; //雇用形態
        private int _contractExpirationPeriod; //更新期間
        private string _contractExpirationPeriodString; //更新期間
        private string _payDetail; //給与区分
        private int _pay; //給与
        private string _travelCostDetail; //交通費区分
        private int _travelCost; //交通費
        private int _jobDescription; //従事すべき業務の内容
        private string _workTime; //勤務時間
        private string _breakTime; //休憩時間
        private bool _checkFlag; //組合に押印提出中
        private bool _koyouFlag; // 雇用保険の適用
        private bool _syakaiFlag; // 社会保険の適用
        private string _salaryRaise; // 昇給
        private string _bonusSummerText; // 夏ボーナス
        private int _bonusSummerPay; // 夏ボーナス金額
        private string _bonusWinterText; // 冬ボーナス
        private int _bonusWinterPay; // 冬ボーナス金額
        private string _bonusDetailText; // 但し書き
        private string _insertPcName;
        private DateTime _insertYmdHms;
        private string _updatePcName;
        private DateTime _updateYmdHms;
        private string _deletePcName;
        private DateTime _deleteYmdHms;
        private bool _deleteFlag;

        public EmploymentAgreementVo() {
            _staffCode = 0;
            _baseLocation = string.Empty;
            _occupation = 0;
            _contractExpirationPeriod = 0;
            _contractExpirationPeriodString = string.Empty;
            _payDetail = string.Empty;
            _pay = 0;
            _travelCostDetail = string.Empty;
            _travelCost = 0;
            _jobDescription = 99;
            _workTime = string.Empty;
            _breakTime = string.Empty;
            _checkFlag = false;
            _koyouFlag = false;
            _syakaiFlag = false;
            _salaryRaise = string.Empty;
            _bonusSummerText = string.Empty;
            _bonusSummerPay = 0;
            _bonusWinterText = string.Empty;
            _bonusWinterPay = 0;
            _bonusDetailText = string.Empty;
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;
        }

        /// <summary>
        /// 従事者コード
        /// </summary>
        public int StaffCode {
            get => this._staffCode;
            set => this._staffCode = value;
        }
        /// <summary>
        /// 勤務地
        /// </summary>
        public string BaseLocation {
            get => this._baseLocation;
            set => this._baseLocation = value;
        }
        /// <summary>
        /// 雇用形態
        /// </summary>
        public int Occupation {
            get => this._occupation;
            set => this._occupation = value;
        }
        /// <summary>
        /// 更新期間
        /// </summary>
        public int ContractExpirationPeriod {
            get => this._contractExpirationPeriod;
            set => this._contractExpirationPeriod = value;
        }
        /// <summary>
        /// 契約期間文字
        /// </summary>
        public string ContractExpirationPeriodString {
            get => this._contractExpirationPeriodString;
            set => this._contractExpirationPeriodString = value;
        }
        /// <summary>
        /// 給与区分
        /// </summary>
        public string PayDetail {
            get => this._payDetail;
            set => this._payDetail = value;
        }
        /// <summary>
        /// 給与
        /// </summary>
        public int Pay {
            get => this._pay;
            set => this._pay = value;
        }
        /// <summary>
        /// 交通費区分
        /// </summary>
        public string TravelCostDetail {
            get => this._travelCostDetail;
            set => this._travelCostDetail = value;
        }
        /// <summary>
        /// 交通費
        /// </summary>
        public int TravelCost {
            get => this._travelCost;
            set => this._travelCost = value;
        }
        /// <summary>
        /// 従事すべき業務の内容
        /// </summary>
        public int JobDescription {
            get => this._jobDescription;
            set => this._jobDescription = value;
        }
        /// <summary>
        /// 勤務時間
        /// </summary>
        public string WorkTime {
            get => this._workTime;
            set => this._workTime = value;
        }
        /// <summary>
        /// 休憩時間
        /// </summary>
        public string BreakTime {
            get => this._breakTime;
            set => this._breakTime = value;
        }
        /// <summary>
        /// 組合に押印提出中
        /// true:提出中
        /// false:未提出
        /// </summary>
        public bool CheckFlag {
            get => this._checkFlag;
            set => this._checkFlag = value;
        }
        /// <summary>
        /// 雇用保険の適用
        /// true:適用あり
        /// false:適用なし
        /// </summary>
        public bool KoyouFlag {
            get => this._koyouFlag;
            set => this._koyouFlag = value;
        }
        /// <summary>
        /// 社会保険の適用
        /// true:適用あり
        /// false:適用なし
        /// </summary>
        public bool SyakaiFlag {
            get => this._syakaiFlag;
            set => this._syakaiFlag = value;
        }
        /// <summary>
        /// 昇給の有無
        /// </summary>
        public string SalaryRaise {
            get => this._salaryRaise;
            set => this._salaryRaise = value;
        }
        /// <summary>
        /// 夏賞与の有無
        /// </summary>
        public string BonusSummerText {
            get => this._bonusSummerText;
            set => this._bonusSummerText = value;
        }
        /// <summary>
        /// 夏賞与の金額
        /// </summary>
        public int BonusSummerPay {
            get => this._bonusSummerPay;
            set => this._bonusSummerPay = value;
        }
        /// <summary>
        /// 冬賞与の有無
        /// </summary>
        public string BonusWinterText {
            get => this._bonusWinterText;
            set => this._bonusWinterText = value;
        }
        /// <summary>
        /// 冬賞与の金額
        /// </summary>
        public int BonusWinterPay {
            get => this._bonusWinterPay;
            set => this._bonusWinterPay = value;
        }
        /// <summary>
        /// 但し、会社の業績により支給しない場合もある。
        /// </summary>
        public string BonusDetailText {
            get => this._bonusDetailText;
            set => this._bonusDetailText = value;
        }
        public string InsertPcName {
            get => this._insertPcName;
            set => this._insertPcName = value;
        }
        public DateTime InsertYmdHms {
            get => this._insertYmdHms;
            set => this._insertYmdHms = value;
        }
        public string UpdatePcName {
            get => this._updatePcName;
            set => this._updatePcName = value;
        }
        public DateTime UpdateYmdHms {
            get => this._updateYmdHms;
            set => this._updateYmdHms = value;
        }
        public string DeletePcName {
            get => this._deletePcName;
            set => this._deletePcName = value;
        }
        public DateTime DeleteYmdHms {
            get => this._deleteYmdHms;
            set => this._deleteYmdHms = value;
        }
        public bool DeleteFlag {
            get => this._deleteFlag;
            set => this._deleteFlag = value;
        }

    }
}
