/*
 * 2024-04-10
 */
namespace Vo {
    public class StatusOfResidenceMasterVo {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);

        private int _staffCode;
        private string _staffNameKana;
        private string _staffName;
        private DateTime _birthDate;
        private string _gender;
        private string _nationality;
        private string _address;
        private string _statusOfResidence;
        private string _workLimit;
        private DateTime _periodDate;
        private DateTime _deadlineDate;
        private byte[] _pictureHead;
        private byte[] _pictureTail;
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
        public StatusOfResidenceMasterVo() {
            _staffCode = 0;
            _staffNameKana = string.Empty;
            _staffName = string.Empty;
            _birthDate = _defaultDateTime;
            _gender = string.Empty;
            _nationality = string.Empty;
            _address = string.Empty;
            _statusOfResidence = string.Empty;
            _workLimit = string.Empty;
            _periodDate = _defaultDateTime;
            _deadlineDate = _defaultDateTime;
            _pictureHead = Array.Empty<byte>();
            _pictureTail = Array.Empty<byte>();
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
        /// カナ
        /// </summary>
        public string StaffNameKana {
            get => _staffNameKana;
            set => _staffNameKana = value;
        }
        /// <summary>
        /// 氏名
        /// </summary>
        public string StaffName {
            get => _staffName;
            set => _staffName = value;
        }
        /// <summary>
        /// 生年月日
        /// </summary>
        public DateTime BirthDate {
            get => _birthDate;
            set => _birthDate = value;
        }
        /// <summary>
        /// 性別
        /// </summary>
        public string Gender {
            get => _gender;
            set => _gender = value;
        }
        /// <summary>
        /// 国籍・地域
        /// </summary>
        public string Nationality {
            get => _nationality;
            set => _nationality = value;
        }
        /// <summary>
        /// 住居地
        /// </summary>
        public string Address {
            get => _address;
            set => _address = value;
        }
        /// <summary>
        /// 在留資格
        /// </summary>
        public string StatusOfResidence {
            get => _statusOfResidence;
            set => _statusOfResidence = value;
        }
        /// <summary>
        /// 就労制限の有無
        /// </summary>
        public string WorkLimit {
            get => _workLimit;
            set => _workLimit = value;
        }
        /// <summary>
        /// 在留期間
        /// </summary>
        public DateTime PeriodDate {
            get => _periodDate;
            set => _periodDate = value;
        }
        /// <summary>
        /// 有効期限
        /// </summary>
        public DateTime DeadlineDate {
            get => _deadlineDate;
            set => _deadlineDate = value;
        }
        public byte[] PictureHead {
            get => _pictureHead;
            set => _pictureHead = value;
        }
        public byte[] PictureTail {
            get => _pictureTail;
            set => _pictureTail = value;
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
