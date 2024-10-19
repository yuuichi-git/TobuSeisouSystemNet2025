/*
 * 2023-10-31
 * 交通違反他ファイル
 */
namespace Vo {
    public class StaffCarViolateVo {
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);

        private int _staffCode;
        private DateTime _carViolateDate;
        private string _carViolateContent;
        private string _carViolatePlace;
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
        public StaffCarViolateVo() {
            _staffCode = 0;
            _carViolateDate = _defaultDateTime;
            _carViolateContent = string.Empty;
            _carViolatePlace = string.Empty;
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;
        }

        /// <summary>
        /// 従業員コード
        /// </summary>
        public int StaffCode {
            get => _staffCode;
            set => _staffCode = value;
        }
        /// <summary>
        /// 違反年月日
        /// </summary>
        public DateTime CarViolateDate {
            get => _carViolateDate;
            set => _carViolateDate = value;
        }
        /// <summary>
        /// 違反名
        /// </summary>
        public string CarViolateContent {
            get => _carViolateContent;
            set => _carViolateContent = value;
        }
        /// <summary>
        /// 違反住所他
        /// </summary>
        public string CarViolatePlace {
            get => _carViolatePlace;
            set => _carViolatePlace = value;
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
