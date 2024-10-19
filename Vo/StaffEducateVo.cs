/*
 * 2023-10-31
 * 社内教育の実施状況
 */
namespace Vo {
    public class StaffEducateVo {
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);

        private int _staffCode;
        private DateTime _educateDate;
        private string _educateName;
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
        public StaffEducateVo() {
            _staffCode = 0;
            _educateDate = _defaultDateTime;
            _educateName = string.Empty;
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
        /// 教育を受けた年月日
        /// </summary>
        public DateTime EducateDate {
            get => _educateDate;
            set => _educateDate = value;
        }
        /// <summary>
        /// 教育名称
        /// </summary>
        public string EducateName {
            get => _educateName;
            set => _educateName = value;
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
