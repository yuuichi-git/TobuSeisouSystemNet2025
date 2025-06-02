/*
 * 2024-05-21
 */
namespace Vo {
    public class CertificationMasterVo {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);

        private int _certificationCode;
        private string _certificationName;
        private string _certificationDisplayName;
        private bool _displayFlag;
        private int _certificationType;
        private int _numberOfAppointments;
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
        public CertificationMasterVo() {
            _certificationCode = 0;
            _certificationName = string.Empty;
            _certificationDisplayName = string.Empty;
            _displayFlag = false;
            _certificationType = 0;
            _numberOfAppointments = 0;
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;
        }

        /// <summary>
        /// 資格コード
        /// 100の位はCertification_typeの数字と同じ
        /// </summary>
        public int CertificationCode {
            get => _certificationCode;
            set => _certificationCode = value;
        }
        /// <summary>
        /// 資格名
        /// </summary>
        public string CertificationName {
            get => _certificationName;
            set => _certificationName = value;
        }
        /// <summary>
        /// 資格表示名
        /// </summary>
        public string CertificationDisplayName {
            get => _certificationDisplayName;
            set => _certificationDisplayName = value;
        }
        /// <summary>
        /// 画面表示フラグ
        /// True:画面に表示する　False:画面に表示しない
        /// </summary>
        public bool DisplayFlag {
            get => _displayFlag;
            set => _displayFlag = value;
        }
        /// <summary>
        /// 資格の分類
        /// 1:資格等 2:作業経験の有無等
        /// </summary>
        public int CertificationType {
            get => _certificationType;
            set => _certificationType = value;
        }
        /// <summary>
        /// 資格取得予定数
        /// </summary>
        public int NumberOfAppointments {
            get => _numberOfAppointments;
            set => _numberOfAppointments = value;
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
