namespace Vo {
    public class CarAccidentMasterVo {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);

        private int _staffCode;
        private DateTime _occurrenceYmdHms;
        private bool _totallingFlag;
        private string _weather;
        private string _accidentKind;
        private string _carStatic;
        private string _occurrenceCause;
        private string _negligence;
        private string _personalInjury;
        private string _propertyAccident1;
        private string _propertyAccident2;
        private string _occurrenceAddress;
        private string _workKind;
        private string _displayName;
        private string _licenseNumber;
        private string _carRegistrationNumber;
        private string _accidentSummary;
        private string _accidentDetail;
        private string _guide;
        private byte[] _picture1;
        private byte[] _picture2;
        private byte[] _picture3;
        private byte[] _picture4;
        private byte[] _picture5;
        private string _insertPcName;
        private DateTime _insertYmdHms;
        private string _updatePcName;
        private DateTime _updateYmdHms;
        private string _deletePcName;
        private DateTime _deleteYmdHms;
        private bool _deleteFlag;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public CarAccidentMasterVo() {
            _staffCode = 0;
            _occurrenceYmdHms = _defaultDateTime;
            _totallingFlag = false;
            _weather = string.Empty;
            _accidentKind = string.Empty;
            _carStatic = string.Empty;
            _occurrenceCause = string.Empty;
            _negligence = string.Empty;
            _personalInjury = string.Empty;
            _propertyAccident1 = string.Empty;
            _propertyAccident2 = string.Empty;
            _occurrenceAddress = string.Empty;
            _workKind = string.Empty;
            _displayName = string.Empty;
            _licenseNumber = string.Empty;
            _carRegistrationNumber = string.Empty;
            _accidentSummary = string.Empty;
            _accidentDetail = string.Empty;
            _guide = string.Empty;
            _picture1 = Array.Empty<byte>();
            _picture2 = Array.Empty<byte>();
            _picture3 = Array.Empty<byte>();
            _picture4 = Array.Empty<byte>();
            _picture5 = Array.Empty<byte>();
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
            get => _staffCode;
            set => _staffCode = value;
        }
        /// <summary>
        /// 事故発生日時
        /// </summary>
        public DateTime OccurrenceYmdHms {
            get => _occurrenceYmdHms;
            set => _occurrenceYmdHms = value;
        }
        /// <summary>
        /// 集計表に反映
        /// True:反映する False:反映しない
        /// </summary>
        public bool TotallingFlag {
            get => _totallingFlag;
            set => _totallingFlag = value;
        }
        /// <summary>
        /// 天候
        /// </summary>
        public string Weather {
            get => _weather;
            set => _weather = value;
        }
        /// <summary>
        /// 事故の種別
        /// </summary>
        public string AccidentKind {
            get => _accidentKind;
            set => _accidentKind = value;
        }
        /// <summary>
        /// 車両の静動
        /// </summary>
        public string CarStatic {
            get => _carStatic;
            set => _carStatic = value;
        }
        /// <summary>
        /// 事故の発生原因
        /// </summary>
        public string OccurrenceCause {
            get => _occurrenceCause;
            set => _occurrenceCause = value;
        }
        /// <summary>
        /// 過失の有無
        /// </summary>
        public string Negligence {
            get => _negligence;
            set => _negligence = value;
        }
        /// <summary>
        /// 人身事故の詳細
        /// </summary>
        public string PersonalInjury {
            get => _personalInjury;
            set => _personalInjury = value;
        }
        /// <summary>
        /// 物件事故の詳細1
        /// </summary>
        public string PropertyAccident1 {
            get => _propertyAccident1;
            set => _propertyAccident1 = value;
        }
        /// <summary>
        /// 物件事故の詳細2
        /// </summary>
        public string PropertyAccident2 {
            get => _propertyAccident2;
            set => _propertyAccident2 = value;
        }
        /// <summary>
        /// 事故の発生場所
        /// </summary>
        public string OccurrenceAddress {
            get => _occurrenceAddress;
            set => _occurrenceAddress = value;
        }
        /// <summary>
        /// 運転者・作業員の別
        /// </summary>
        public string WorkKind {
            get => _workKind;
            set => _workKind = value;
        }
        /// <summary>
        /// 従業員名
        /// </summary>
        public string DisplayName {
            get => _displayName;
            set => _displayName = value;
        }
        /// <summary>
        /// 免許証番号
        /// </summary>
        public string LicenseNumber {
            get => _licenseNumber;
            set => _licenseNumber = value;
        }
        /// <summary>
        /// 車両登録番号
        /// </summary>
        public string CarRegistrationNumber {
            get => _carRegistrationNumber;
            set => _carRegistrationNumber = value;
        }
        /// <summary>
        /// 事故概要
        /// </summary>
        public string AccidentSummary {
            get => _accidentSummary;
            set => _accidentSummary = value;
        }
        /// <summary>
        /// 事故詳細
        /// </summary>
        public string AccidentDetail {
            get => _accidentDetail;
            set => _accidentDetail = value;
        }
        /// <summary>
        /// 事故後の指導
        /// </summary>
        public string Guide {
            get => _guide;
            set => _guide = value;
        }
        public byte[] Picture1 {
            get => _picture1;
            set => _picture1 = value;
        }
        public byte[] Picture2 {
            get => _picture2;
            set => _picture2 = value;
        }
        public byte[] Picture3 {
            get => _picture3;
            set => _picture3 = value;
        }
        public byte[] Picture4 {
            get => _picture4;
            set => _picture4 = value;
        }
        public byte[] Picture5 {
            get => _picture5;
            set => _picture5 = value;
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
    }
}
