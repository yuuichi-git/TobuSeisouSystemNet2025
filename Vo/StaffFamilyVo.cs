/*
 * 2023-10-31
 * 家族構成ファイル
 */
namespace Vo {
    public class StaffFamilyVo {
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);

        private int _staffCode;
        private string _familyName;
        private DateTime _familyBirthDay;
        private string _familyRelationship;
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
        public StaffFamilyVo() {
            _staffCode = 0;
            _familyName = string.Empty;
            _familyBirthDay = _defaultDateTime;
            _familyRelationship = string.Empty;
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
        /// 家族氏名
        /// </summary>
        public string FamilyName {
            get => _familyName;
            set => _familyName = value;
        }
        /// <summary>
        /// 生年月日
        /// </summary>
        public DateTime FamilyBirthDay {
            get => _familyBirthDay;
            set => _familyBirthDay = value;
        }
        /// <summary>
        /// 従業員との関係
        /// </summary>
        public string FamilyRelationship {
            get => _familyRelationship;
            set => _familyRelationship = value;
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
