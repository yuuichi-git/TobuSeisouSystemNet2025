/*
 * 2023-10-31
 * 適正診断ファイル
 */
namespace Vo {
    public class StaffProperVo {
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);

        private int _staffCode;
        private string _properKind;
        private DateTime _properDate;
        private string _properNote;
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
        public StaffProperVo() {
            _staffCode = 0;
            _properKind = string.Empty;
            _properDate = _defaultDateTime;
            _properNote = string.Empty;
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
        /// 診断の種類
        /// </summary>
        public string ProperKind {
            get => _properKind;
            set => _properKind = value;
        }
        /// <summary>
        /// 診断日
        /// </summary>
        public DateTime ProperDate {
            get => _properDate;
            set => _properDate = value;
        }
        /// <summary>
        /// 備考
        /// </summary>
        public string ProperNote {
            get => _properNote;
            set => _properNote = value;
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
