/*
 * 2026-05-30
 */
namespace Vo {
    public class TimeOffMasterVo {
        private DateTime _defaultDateTime = new(1900, 01, 01);

        private DateTime _date;
        private int _code;
        private int _staffCode;
        private string _remarks;
        private string _insertPcName;
        private DateTime _insertYmdHms;
        private string _updatePcName;
        private DateTime _updateYmdHms;
        private string _deletePcName;
        private DateTime _deleteYmdHms;
        private bool _deleteFlag;

        public TimeOffMasterVo() {
            _date = _defaultDateTime;
            _code = 0;
            _staffCode = 0;
            _remarks = string.Empty;
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;
        }

        /// <summary>
        /// 休暇日
        /// </summary>
        public DateTime Date { get => this._date; set => this._date = value; }
        /// <summary>
        /// 休暇Code
        /// </summary>
        public int Code { get => this._code; set => this._code = value; }
        /// <summary>
        /// StaffCode
        /// </summary>
        public int StaffCode { get => this._staffCode; set => this._staffCode = value; }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get => this._remarks; set => this._remarks = value; }
        public string InsertPcName { get => this._insertPcName; set => this._insertPcName = value; }

        public DateTime InsertYmdHms { get => this._insertYmdHms; set => this._insertYmdHms = value; }
        public string UpdatePcName { get => this._updatePcName; set => this._updatePcName = value; }
        public DateTime UpdateYmdHms { get => this._updateYmdHms; set => this._updateYmdHms = value; }
        public string DeletePcName { get => this._deletePcName; set => this._deletePcName = value; }
        public DateTime DeleteYmdHms { get => this._deleteYmdHms; set => this._deleteYmdHms = value; }
        public bool DeleteFlag { get => this._deleteFlag; set => this._deleteFlag = value; }
    }
}
