/*
 * 2023-10-31
 * 健康診断ファイル
 */
namespace Vo {
    public class StaffMedicalExaminationVo {
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);

        private int _staffCode;
        private DateTime _medicalExaminationDate;
        private string _medicalInstitutionName;
        private string _medicalExaminationNote;
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
        public StaffMedicalExaminationVo() {
            _staffCode = 0;
            _medicalExaminationDate = _defaultDateTime;
            _medicalInstitutionName = string.Empty;
            _medicalExaminationNote = string.Empty;
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
        /// 健診実施日
        /// </summary>
        public DateTime MedicalExaminationDate {
            get => _medicalExaminationDate;
            set => _medicalExaminationDate = value;
        }
        /// <summary>
        /// 受診機関名
        /// </summary>
        public string MedicalInstitutionName {
            get => _medicalInstitutionName;
            set => _medicalInstitutionName = value;
        }
        /// <summary>
        /// 備考
        /// </summary>
        public string MedicalExaminationNote {
            get => _medicalExaminationNote;
            set => _medicalExaminationNote = value;
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
