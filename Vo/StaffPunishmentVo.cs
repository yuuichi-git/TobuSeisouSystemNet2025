/*
 * 2023-10-31
 * 賞罰・譴責ファイル
 */
namespace Vo {
    public class StaffPunishmentVo {
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);

        private int _staffCode;
        private DateTime _punishmentDate;
        private string _punishmentNote;
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
        public StaffPunishmentVo() {
            _staffCode = 0;
            _punishmentDate = _defaultDateTime;
            _punishmentNote = string.Empty;
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
        /// 年月日
        /// </summary>
        public DateTime PunishmentDate {
            get => _punishmentDate;
            set => _punishmentDate = value;
        }
        /// <summary>
        /// 備考
        /// </summary>
        public string PunishmentNote {
            get => _punishmentNote;
            set => _punishmentNote = value;
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
