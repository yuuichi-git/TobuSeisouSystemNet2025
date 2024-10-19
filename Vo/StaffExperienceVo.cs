/*
 * 2023-10-31
 * 自動車経験ファイル
 */
namespace Vo {
    public class StaffExperienceVo {
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);

        private int _staffCode;
        private string _experienceKind;
        private string _experienceLoad;
        private string _experienceDuration;
        private string _experienceNote;
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
        public StaffExperienceVo() {
            _staffCode = 0;
            _experienceKind = string.Empty;
            _experienceLoad = string.Empty;
            _experienceDuration = string.Empty;
            _experienceNote = string.Empty;
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
        /// 過去に運転経験のある自動車の種類
        /// </summary>
        public string ExperienceKind {
            get => _experienceKind;
            set => _experienceKind = value;
        }
        /// <summary>
        /// 過去に運転経験のある自動車の積載量
        /// </summary>
        public string ExperienceLoad {
            get => _experienceLoad;
            set => _experienceLoad = value;
        }
        /// <summary>
        /// 過去に運転経験のある自動車の経験期間
        /// </summary>
        public string ExperienceDuration {
            get => _experienceDuration;
            set => _experienceDuration = value;
        }
        /// <summary>
        /// 過去に運転経験のある自動車の備考
        /// </summary>
        public string ExperienceNote {
            get => _experienceNote;
            set => _experienceNote = value;
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
