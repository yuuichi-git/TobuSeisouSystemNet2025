/*
 * 2024-05-22
 */
namespace Vo {
    public class CertificationFileVo {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);

        private int _staffCode;
        private int _certificationCode;
        private int _markCode;
        private byte[] _picture1;
        private byte[] _picture2;
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
        public CertificationFileVo() {
            _staffCode = 0;
            _certificationCode = 0;
            _markCode = 0;
            _picture1 = Array.Empty<byte>();
            _picture2 = Array.Empty<byte>();
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
        /// 資格コード
        /// </summary>
        public int CertificationCode {
            get => _certificationCode;
            set => _certificationCode = value;
        }
        /// <summary>
        /// <summary>
        /// 〇印の種類　0→◎,1→○,2→●
        /// </summary>
        public int MarkCode {
            get => _markCode;
            set => _markCode = value;
        }
        public byte[] Picture1 {
            get => _picture1;
            set => _picture1 = value;
        }
        public byte[] Picture2 {
            get => _picture2;
            set => _picture2 = value;
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
