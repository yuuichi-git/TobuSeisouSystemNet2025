/*
 * 2024-05-01
 */
namespace Vo {
    public class LegalTwelveItemVo {
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

        private readonly DateTime _defaultDatetime = new(1900, 01, 01);

        /// <summary>
        /// コンストラクター
        /// </summary>
        public LegalTwelveItemVo() {
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
        /// 受講日
        /// </summary>
        public DateTime StudentsDate {
            get => _studentsDate;
            set => _studentsDate = value;
        }
        /// <summary>
        /// 受講コード(１２項目№)
        /// 0～11
        /// </summary>
        public int StudentsCode {
            get => _studentsCode;
            set => _studentsCode = value;
        }
        /// <summary>
        /// 受講フラグ
        /// </summary>
        public bool StudentsFlag {
            get => _studentsFlag;
            set => _studentsFlag = value;
        }
        /// <summary>
        /// 従事者コード
        /// </summary>
        public int StaffCode {
            get => _staffCode;
            set => _staffCode = value;
        }
        /// <summary>
        /// 受講サイン
        /// </summary>
        public byte[] StaffSign {
            get => _staffSign;
            set => _staffSign = value;
        }
        /// <summary>
        /// サイン番号
        /// 1→1回目のサイン:2→2回目のサイン:3→3回目のサイン
        /// </summary>
        public int SignNumber {
            get => _signNumber;
            set => _signNumber = value;
        }
        /// <summary>
        /// メモ
        /// </summary>
        public string Memo {
            get => _memo;
            set => _memo = value;
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
