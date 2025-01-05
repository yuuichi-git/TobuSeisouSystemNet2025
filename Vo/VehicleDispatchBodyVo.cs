/*
 * 2023-11-06
 */
namespace Vo {
    public class VehicleDispatchBodyVo {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);

        private int _setCode;
        private string _dayOfWeek;
        private int _carCode;
        private int _staffCode1;
        private int _staffCode2;
        private int _staffCode3;
        private int _staffCode4;
        private int _FinancialYear;
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
        public VehicleDispatchBodyVo() {
            _setCode = 0;
            _dayOfWeek = string.Empty;
            _carCode = 0;
            _staffCode1 = 0;
            _staffCode2 = 0;
            _staffCode3 = 0;
            _staffCode4 = 0;
            _FinancialYear = 0;
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;
        }

        /// <summary>
        /// 配車先コード
        /// </summary>
        public int SetCode {
            get => _setCode;
            set => _setCode = value;
        }
        /// <summary>
        /// 配車曜日
        /// </summary>
        public string DayOfWeek {
            get => _dayOfWeek;
            set => _dayOfWeek = value;
        }
        /// <summary>
        /// 車両コード
        /// </summary>
        public int CarCode {
            get => _carCode;
            set => _carCode = value;
        }
        /// <summary>
        /// 従事者コード１
        /// </summary>
        public int StaffCode1 {
            get => _staffCode1;
            set => _staffCode1 = value;
        }
        /// <summary>
        /// 従事者コード２
        /// </summary>
        public int StaffCode2 {
            get => _staffCode2;
            set => _staffCode2 = value;
        }
        /// <summary>
        /// 従事者コード３
        /// </summary>
        public int StaffCode3 {
            get => _staffCode3;
            set => _staffCode3 = value;
        }
        /// <summary>
        /// 従事者コード４
        /// </summary>
        public int StaffCode4 {
            get => _staffCode4;
            set => _staffCode4 = value;
        }
        /// <summary>
        /// 会計年度
        /// </summary>
        public int FinancialYear {
            get => _FinancialYear;
            set => _FinancialYear = value;
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
