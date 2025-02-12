namespace Vo {
    /*
     * DeepCopyで使用
     */
    [Serializable] // ←DeepCopyする場合には必要
    public class CarMasterVo {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);

        private int _carCode;
        private int _classificationCode;
        private string _registrationNumber;
        private string _registrationNumber1;
        private string _registrationNumber2;
        private string _registrationNumber3;
        private string _registrationNumber4;
        private int _garageCode;
        private int _doorNumber;
        private DateTime _registrationDate;
        private DateTime _firstRegistrationDate;
        private int _carKindCode;
        private string _disguiseKind1;
        private string _disguiseKind2;
        private string _disguiseKind3;
        private string _carUse;
        private int _otherCode;
        private int _shapeCode;
        private int _manufacturerCode;
        private decimal _capacity;
        private decimal _maximumLoadCapacity;
        private decimal _vehicleWeight;
        private decimal _totalVehicleWeight;
        private string _vehicleNumber;
        private decimal _length;
        private decimal _width;
        private decimal _height;
        private decimal _ffAxisWeight;
        private decimal _frAxisWeight;
        private decimal _rfAxisWeight;
        private decimal _rrAxisWeight;
        private string _version;
        private string _motorVersion;
        private decimal _totalDisplacement;
        private string _typesOfFuel;
        private string _versionDesignateNumber;
        private string _categoryDistinguishNumber;
        private string _ownerName;
        private string _ownerAddress;
        private string _userName;
        private string _userAddress;
        private string _baseAddress;
        private DateTime _expirationDate;
        private string _remarks;
        private byte[] _mainPicture; // 2024-08-09 追加
        private byte[] _subPicture; // 2024-08-09 名前変更
        private bool _emergencyVehicleFlag; // 2024-08-13
        private DateTime _emergencyVehicleDate; // 2024-08-13
        private string _insertPcName;
        private DateTime _insertYmdHms;
        private string _updatePcName;
        private DateTime _updateYmdHms;
        private string _deletePcName;
        private DateTime _deleteYmdHms;
        private bool _deleteFlag;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CarMasterVo() {
            _carCode = 0;
            _classificationCode = default;
            _registrationNumber = string.Empty;
            _registrationNumber1 = string.Empty;
            _registrationNumber2 = string.Empty;
            _registrationNumber3 = string.Empty;
            _registrationNumber4 = string.Empty;
            _garageCode = default;
            _doorNumber = default;
            _registrationDate = _defaultDateTime;
            _firstRegistrationDate = _defaultDateTime;
            _carKindCode = default;
            _disguiseKind1 = string.Empty;
            _disguiseKind2 = string.Empty;
            _disguiseKind3 = string.Empty;
            _carUse = string.Empty;
            _otherCode = default;
            _shapeCode = default;
            _manufacturerCode = default;
            _capacity = default;
            _maximumLoadCapacity = default;
            _vehicleWeight = default;
            _totalVehicleWeight = default;
            _vehicleNumber = string.Empty;
            _length = default;
            _width = default;
            _height = default;
            _ffAxisWeight = default;
            _frAxisWeight = default;
            _rfAxisWeight = default;
            _rrAxisWeight = default;
            _version = string.Empty;
            _motorVersion = string.Empty;
            _totalDisplacement = default;
            _typesOfFuel = string.Empty;
            _versionDesignateNumber = string.Empty;
            _categoryDistinguishNumber = string.Empty;
            _ownerName = string.Empty;
            _ownerAddress = string.Empty;
            _userName = string.Empty;
            _userAddress = string.Empty;
            _baseAddress = string.Empty;
            _expirationDate = _defaultDateTime;
            _remarks = string.Empty;
            _mainPicture = Array.Empty<byte>();
            _subPicture = Array.Empty<byte>();
            _emergencyVehicleFlag = false;
            _emergencyVehicleDate = _defaultDateTime;
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;
        }

        /// <summary>
        /// 車両コード
        /// </summary>
        public int CarCode {
            get => _carCode;
            set => _carCode = value;
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
        /// 自動車登録番号又は車両番号
        /// </summary>
        public string RegistrationNumber {
            get => _registrationNumber;
            set => _registrationNumber = value;
        }
        /// <summary>
        /// 自動車登録番号又は車両番号1
        /// </summary>
        public string RegistrationNumber1 {
            get => _registrationNumber1;
            set => _registrationNumber1 = value;
        }
        /// <summary>
        /// 自動車登録番号又は車両番号2
        /// </summary>
        public string RegistrationNumber2 {
            get => _registrationNumber2;
            set => _registrationNumber2 = value;
        }
        /// <summary>
        /// 自動車登録番号又は車両番号3
        /// </summary>
        public string RegistrationNumber3 {
            get => _registrationNumber3;
            set => _registrationNumber3 = value;
        }
        /// <summary>
        /// 自動車登録番号又は車両番号4
        /// </summary>
        public string RegistrationNumber4 {
            get => _registrationNumber4;
            set => _registrationNumber4 = value;
        }
        /// <summary>
        /// 車庫地
        /// 1:足立 2:三郷
        /// </summary>
        public int GarageCode {
            get => _garageCode;
            set => _garageCode = value;
        }
        /// <summary>
        /// ドア番号
        /// "78-1"等の文字で表すドア番は"781"等と記載する
        /// </summary>
        public int DoorNumber {
            get => _doorNumber;
            set => _doorNumber = value;
        }
        /// <summary>
        /// 登録年月日/交付年月日
        /// </summary>
        public DateTime RegistrationDate {
            get => _registrationDate;
            set => _registrationDate = value;
        }
        /// <summary>
        /// 初年度登録年月
        /// </summary>
        public DateTime FirstRegistrationDate {
            get => _firstRegistrationDate;
            set => _firstRegistrationDate = value;
        }
        /// <summary>
        /// 自動車の種別
        /// 10:軽自動車 11:小型 12:普通
        /// </summary>
        public int CarKindCode {
            get => _carKindCode;
            set => _carKindCode = value;
        }
        /// <summary>
        /// 仮装の種類1(配車での名称)
        /// </summary>
        public string DisguiseKind1 {
            get => _disguiseKind1;
            set => _disguiseKind1 = value;
        }
        /// <summary>
        /// 仮装の種類2(事故報告書での名称)
        /// </summary>
        public string DisguiseKind2 {
            get => _disguiseKind2;
            set => _disguiseKind2 = value;
        }
        /// <summary>
        /// 仮装の種類3(整備での名称)
        /// </summary>
        public string DisguiseKind3 {
            get => _disguiseKind3;
            set => _disguiseKind3 = value;
        }
        /// <summary>
        /// 用途
        /// </summary>
        public string CarUse {
            get => _carUse;
            set => _carUse = value;
        }
        /// <summary>
        /// 自家用・事業用の別コード
        /// 10:事業用 11:自家用
        /// </summary>
        public int OtherCode {
            get => _otherCode;
            set => _otherCode = value;
        }
        /// <summary>
        /// 車体の形状コード
        /// 10:キャブオーバー 11:塵芥車 12:ダンプ 13:コンテナ専用 14:脱着装置付コンテナ専用車 15:粉粒体運搬車 16:糞尿車 17:清掃車
        /// </summary>
        public int ShapeCode {
            get => _shapeCode;
            set => _shapeCode = value;
        }
        /// <summary>
        /// 車両メーカーコード
        /// 10:いすゞ 11:日産 12:ダイハツ 13:日野 14:スバル
        /// </summary>
        public int ManufacturerCode {
            get => _manufacturerCode;
            set => _manufacturerCode = value;
        }
        /// <summary>
        /// 乗車定員
        /// </summary>
        public decimal Capacity {
            get => _capacity;
            set => _capacity = value;
        }
        /// <summary>
        /// 最大積載量
        /// </summary>
        public decimal MaximumLoadCapacity {
            get => _maximumLoadCapacity;
            set => _maximumLoadCapacity = value;
        }
        /// <summary>
        /// 車両重量
        /// </summary>
        public decimal VehicleWeight {
            get => _vehicleWeight;
            set => _vehicleWeight = value;
        }
        /// <summary>
        /// 車両総重量
        /// </summary>
        public decimal TotalVehicleWeight {
            get => _totalVehicleWeight;
            set => _totalVehicleWeight = value;
        }
        /// <summary>
        /// 車台番号
        /// </summary>
        public string VehicleNumber {
            get => _vehicleNumber;
            set => _vehicleNumber = value;
        }
        /// <summary>
        /// 長さ
        /// </summary>
        public decimal Length {
            get => _length;
            set => _length = value;
        }
        /// <summary>
        /// 幅
        /// </summary>
        public decimal Width {
            get => _width;
            set => _width = value;
        }
        /// <summary>
        /// 高さ
        /// </summary>
        public decimal Height {
            get => _height;
            set => _height = value;
        }
        /// <summary>
        /// 前前軸重
        /// </summary>
        public decimal FfAxisWeight {
            get => _ffAxisWeight;
            set => _ffAxisWeight = value;
        }
        /// <summary>
        /// 前後軸重
        /// </summary>
        public decimal FrAxisWeight {
            get => _frAxisWeight;
            set => _frAxisWeight = value;
        }
        /// <summary>
        /// 後前軸重
        /// </summary>
        public decimal RfAxisWeight {
            get => _rfAxisWeight;
            set => _rfAxisWeight = value;
        }
        /// <summary>
        /// 後後軸重
        /// </summary>
        public decimal RrAxisWeight {
            get => _rrAxisWeight;
            set => _rrAxisWeight = value;
        }
        /// <summary>
        /// 型式
        /// </summary>
        public string Version {
            get => _version;
            set => _version = value;
        }
        /// <summary>
        /// 原動機の型式
        /// </summary>
        public string MotorVersion {
            get => _motorVersion;
            set => _motorVersion = value;
        }
        /// <summary>
        /// 総排気量又は定格出力
        /// </summary>
        public decimal TotalDisplacement {
            get => _totalDisplacement;
            set => _totalDisplacement = value;
        }
        /// <summary>
        /// 燃料の種類
        /// </summary>
        public string TypesOfFuel {
            get => _typesOfFuel;
            set => _typesOfFuel = value;
        }
        /// <summary>
        /// 型式指定番号
        /// </summary>
        public string VersionDesignateNumber {
            get => _versionDesignateNumber;
            set => _versionDesignateNumber = value;
        }
        /// <summary>
        /// 類別区分番号
        /// </summary>
        public string CategoryDistinguishNumber {
            get => _categoryDistinguishNumber;
            set => _categoryDistinguishNumber = value;
        }
        /// <summary>
        /// 所有者の氏名又は名称
        /// </summary>
        public string OwnerName {
            get => _ownerName;
            set => _ownerName = value;
        }
        /// <summary>
        /// 所有者の住所
        /// </summary>
        public string OwnerAddress {
            get => _ownerAddress;
            set => _ownerAddress = value;
        }
        /// <summary>
        /// 使用者の氏名又は名称
        /// </summary>
        public string UserName {
            get => _userName;
            set => _userName = value;
        }
        /// <summary>
        /// 使用者の住所
        /// </summary>
        public string UserAddress {
            get => _userAddress;
            set => _userAddress = value;
        }
        /// <summary>
        /// 使用の本拠の位置
        /// </summary>
        public string BaseAddress {
            get => _baseAddress;
            set => _baseAddress = value;
        }
        /// <summary>
        /// 有効期限の満了する日
        /// </summary>
        public DateTime ExpirationDate {
            get => _expirationDate;
            set => _expirationDate = value;
        }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks {
            get => _remarks;
            set => _remarks = value;
        }
        /// <summary>
        /// 車検証画像(車検証)
        /// </summary>
        public byte[] MainPicture {
            get => _mainPicture;
            set => _mainPicture = value;
        }
        /// <summary>
        /// 車検証画像(自動車検査証記録事項)
        /// </summary>
        public byte[] SubPicture {
            get => _subPicture;
            set => _subPicture = value;
        }
        /// <summary>
        /// 緊急車両登録
        /// true:登録済 false:未登録
        /// </summary>
        public bool EmergencyVehicleFlag {
            get => _emergencyVehicleFlag;
            set => _emergencyVehicleFlag = value;
        }
        /// <summary>
        /// 緊急車両登録期限
        /// </summary>
        public DateTime EmergencyVehicleDate {
            get => _emergencyVehicleDate;
            set => _emergencyVehicleDate = value;
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
