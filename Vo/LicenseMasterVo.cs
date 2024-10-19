/*
 * 2024-02-07
 */
namespace Vo {
    public class LicenseMasterVo {
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);

        private int _staffCode;
        private string _nameKana;
        private string _name;
        private DateTime _birthDate;
        private string _currentAddress;
        private DateTime _deliveryDate;
        private DateTime _expirationDate;
        private string _licenseCondition;
        private string _licenseNumber;
        private DateTime _getDate1;
        private DateTime _getDate2;
        private DateTime _getDate3;
        private byte[] _pictureHead;
        private byte[] _pictureTail;
        private bool _large;
        private bool _medium;
        private bool _quasiMedium;
        private bool _ordinary;
        private bool _bigSpecial;
        private bool _bigAutoBike;
        private bool _ordinaryAutoBike;
        private bool _smallSpecial;
        private bool _withARaw;
        private bool _bigTwo;
        private bool _mediumTwo;
        private bool _ordinaryTwo;
        private bool _bigSpecialTwo;
        private bool _traction;
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
        public LicenseMasterVo() {
            _staffCode = 0;
            _nameKana = string.Empty;
            _name = string.Empty;
            _birthDate = _defaultDateTime;
            _currentAddress = string.Empty;
            _deliveryDate = _defaultDateTime;
            _expirationDate = _defaultDateTime;
            _licenseCondition = string.Empty;
            _licenseNumber = string.Empty;
            _getDate1 = _defaultDateTime;
            _getDate2 = _defaultDateTime;
            _getDate3 = _defaultDateTime;
            _pictureHead = Array.Empty<byte>();
            _pictureTail = Array.Empty<byte>();
            _large = false;
            _medium = false;
            _quasiMedium = false;
            _ordinary = false;
            _bigSpecial = false;
            _bigAutoBike = false;
            _ordinaryAutoBike = false;
            _smallSpecial = false;
            _withARaw = false;
            _bigTwo = false;
            _mediumTwo = false;
            _ordinaryTwo = false;
            _bigSpecialTwo = false;
            _traction = false;
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;
        }

        /// <summary>
        /// 社員コード
        /// </summary>
        public int StaffCode {
            get => _staffCode;
            set => _staffCode = value;
        }
        /// <summary>
        /// 氏名カナ
        /// </summary>
        public string NameKana {
            get => _nameKana;
            set => _nameKana = value;
        }
        /// <summary>
        /// 氏名
        /// </summary>
        public string Name {
            get => _name;
            set => _name = value;
        }
        /// <summary>
        /// 生年月日
        /// </summary>
        public DateTime BirthDate {
            get => _birthDate;
            set => _birthDate = value;
        }
        /// <summary>
        /// 住所
        /// </summary>
        public string CurrentAddress {
            get => _currentAddress;
            set => _currentAddress = value;
        }
        /// <summary>
        /// 交付年月日
        /// </summary>
        public DateTime DeliveryDate {
            get => _deliveryDate;
            set => _deliveryDate = value;
        }
        /// <summary>
        /// 有効期限
        /// </summary>
        public DateTime ExpirationDate {
            get => _expirationDate;
            set => _expirationDate = value;
        }
        /// <summary>
        /// 条件等
        /// </summary>
        public string LicenseCondition {
            get => _licenseCondition;
            set => _licenseCondition = value;
        }
        /// <summary>
        /// 免許証番号
        /// </summary>
        public string LicenseNumber {
            get => _licenseNumber;
            set => _licenseNumber = value;
        }
        /// <summary>
        /// 二小原取得日
        /// </summary>
        public DateTime GetDate1 {
            get => _getDate1;
            set => _getDate1 = value;
        }
        /// <summary>
        /// 他取得日
        /// </summary>
        public DateTime GetDate2 {
            get => _getDate2;
            set => _getDate2 = value;
        }
        /// <summary>
        /// 二種取得日
        /// </summary>
        public DateTime GetDate3 {
            get => _getDate3;
            set => _getDate3 = value;
        }
        /// <summary>
        /// 写真表
        /// </summary>
        public byte[] PictureHead {
            get => _pictureHead;
            set => _pictureHead = value;
        }
        /// <summary>
        /// 写真裏
        /// </summary>
        public byte[] PictureTail {
            get => _pictureTail;
            set => _pictureTail = value;
        }
        /// <summary>
        /// 大型
        /// </summary>
        public bool Large {
            get => _large;
            set => _large = value;
        }
        /// <summary>
        /// 中型
        /// </summary>
        public bool Medium {
            get => _medium;
            set => _medium = value;
        }
        /// <summary>
        /// 準中型
        /// </summary>
        public bool QuasiMedium {
            get => _quasiMedium;
            set => _quasiMedium = value;
        }
        /// <summary>
        /// 普通
        /// </summary>
        public bool Ordinary {
            get => _ordinary;
            set => _ordinary = value;
        }
        /// <summary>
        /// 大特
        /// </summary>
        public bool BigSpecial {
            get => _bigSpecial;
            set => _bigSpecial = value;
        }
        /// <summary>
        /// 大自二
        /// </summary>
        public bool BigAutoBike {
            get => _bigAutoBike;
            set => _bigAutoBike = value;
        }
        /// <summary>
        /// 普自二
        /// </summary>
        public bool OrdinaryAutoBike {
            get => _ordinaryAutoBike;
            set => _ordinaryAutoBike = value;
        }
        /// <summary>
        /// 小特
        /// </summary>
        public bool SmallSpecial {
            get => _smallSpecial;
            set => _smallSpecial = value;
        }
        /// <summary>
        /// 原付
        /// </summary>
        public bool WithARaw {
            get => _withARaw;
            set => _withARaw = value;
        }
        /// <summary>
        /// 大型二種
        /// </summary>
        public bool BigTwo {
            get => _bigTwo;
            set => _bigTwo = value;
        }
        /// <summary>
        /// 中型二種
        /// </summary>
        public bool MediumTwo {
            get => _mediumTwo;
            set => _mediumTwo = value;
        }
        /// <summary>
        /// 普通二種
        /// </summary>
        public bool OrdinaryTwo {
            get => _ordinaryTwo;
            set => _ordinaryTwo = value;
        }
        /// <summary>
        /// 大特二種
        /// </summary>
        public bool BigSpecialTwo {
            get => _bigSpecialTwo;
            set => _bigSpecialTwo = value;
        }
        /// <summary>
        /// けん引
        /// </summary>
        public bool Traction {
            get => _traction;
            set => _traction = value;
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
