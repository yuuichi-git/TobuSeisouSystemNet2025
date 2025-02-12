/*
 * 2025-1-23
 */
namespace Vo {
    public class ClassificationMasterVo {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private int _code;
        private string _name;
        private string _insertPcName;
        private DateTime _insertYmdHms;
        private string _updatePcName;
        private DateTime _updateYmdHms;
        private string _deletePcName;
        private DateTime _deleteYmdHms;
        private bool _deleteFlag;

        public ClassificationMasterVo() {
            _code = 99;
            _name = string.Empty;
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;
        }

        public int Code {
            get => this._code;
            set => this._code = value;
        }
        public string Name {
            get => this._name;
            set => this._name = value;
        }
        public string InsertPcName {
            get => this._insertPcName;
            set => this._insertPcName = value;
        }
        public DateTime InsertYmdHms {
            get => this._insertYmdHms;
            set => this._insertYmdHms = value;
        }
        public string UpdatePcName {
            get => this._updatePcName;
            set => this._updatePcName = value;
        }
        public DateTime UpdateYmdHms {
            get => this._updateYmdHms;
            set => this._updateYmdHms = value;
        }
        public string DeletePcName {
            get => this._deletePcName;
            set => this._deletePcName = value;
        }
        public DateTime DeleteYmdHms {
            get => this._deleteYmdHms;
            set => this._deleteYmdHms = value;
        }
        public bool DeleteFlag {
            get => this._deleteFlag;
            set => this._deleteFlag = value;
        }
    }
}
