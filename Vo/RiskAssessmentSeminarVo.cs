/*
 * 2026-08-31
 */
namespace Vo {
    public class RiskAssessmentSeminarVo {
        private string _id;
        private DateTime _studentsDate;
        private int _studentsCode;
        private bool _studentsFlag;
        private int _staffCode;
        private byte[] _staffSign;
        private int _signNumber;
        private string _memo;
        private string _insertPcName;
        private DateTime _insertYmdHms;
        private string _updatePcName;
        private DateTime _updateYmdHms;
        private string _deletePcName;
        private DateTime _deleteYmdHms;
        private bool _deleteFlag;

        private DateTime _defaultDatetime = new(1900, 01, 01);

        /// <summary>
        /// コンストラクター
        /// </summary>
        public RiskAssessmentSeminarVo() {
            _id = string.Empty;
            _studentsDate = _defaultDatetime;
            _studentsCode = 0;
            _studentsFlag = false;
            _staffCode = 0;
            _staffSign = Array.Empty<byte>();
            _signNumber = 0;
            _memo = string.Empty;
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDatetime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDatetime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDatetime;
            _deleteFlag = false;
        }

        /// <summary>
        /// ID
        /// </summary>
        public string Id {
            get {
                return _id;
            }
            set {
                _id = value;
            }
        }
        /// <summary>
        /// 受講日
        /// </summary>
        public DateTime StudentsDate {
            get {
                return _studentsDate;
            }
            set {
                _studentsDate = value;
            }
        }
        /// <summary>
        /// 受講コード(１～３回)
        /// </summary>
        public int StudentsCode {
            get {
                return _studentsCode;
            }
            set {
                _studentsCode = value;
            }
        }

        /// <summary>
        /// 受講フラグ
        /// </summary>
        public bool StudentsFlag {
            get {
                return _studentsFlag;
            }
            set {
                _studentsFlag = value;
            }
        }
        /// <summary>
        /// 従事者コード
        /// </summary>
        public int StaffCode {
            get {
                return _staffCode;
            }
            set {
                _staffCode = value;
            }
        }
        /// <summary>
        /// 受講サイン
        /// </summary>
        public byte[] StaffSign {
            get {
                return _staffSign;
            }
            set {
                _staffSign = value;
            }
        }
        /// <summary>
        /// サイン番号
        /// </summary>
        public int SignNumber {
            get {
                return _signNumber;
            }
            set {
                _signNumber = value;
            }
        }
        /// <summary>
        /// メモ
        /// </summary>
        public string Memo {
            get {
                return _memo;
            }
            set {
                _memo = value;
            }
        }

        public string InsertPcName {
            get {
                return _insertPcName;
            }
            set {
                _insertPcName = value;
            }
        }

        public DateTime InsertYmdHms {
            get {
                return _insertYmdHms;
            }
            set {
                _insertYmdHms = value;
            }
        }

        public string UpdatePcName {
            get {
                return _updatePcName;
            }
            set {
                _updatePcName = value;
            }
        }

        public DateTime UpdateYmdHms {
            get {
                return _updateYmdHms;
            }
            set {
                _updateYmdHms = value;
            }
        }

        public string DeletePcName {
            get {
                return _deletePcName;
            }
            set {
                _deletePcName = value;
            }
        }

        public DateTime DeleteYmdHms {
            get {
                return _deleteYmdHms;
            }
            set {
                _deleteYmdHms = value;
            }
        }

        public bool DeleteFlag {
            get {
                return _deleteFlag;
            }
            set {
                _deleteFlag = value;
            }
        }
    }
}
