/*
 * 2024-11-12
 */
namespace Vo {
    public class ContractExpirationVo {
        private DateTime _defaultDateTime = new(1900, 01, 01);
        private int _code;
        private int _staffCode;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _memo;
        private byte[] _picture;
        private string _insertPcName;
        private DateTime _insertYmdHms;
        private string _updatePcName;
        private DateTime _updateYmdHms;
        private string _deletePcName;
        private DateTime _deleteYmdHms;
        private bool _deleteFlag;

        public ContractExpirationVo() {
            _code = 0;
            _staffCode = 0;
            _startDate = _defaultDateTime;
            _endDate = _defaultDateTime;
            _memo = string.Empty;
            _picture = Array.Empty<byte>();
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;
        }

        /// <summary>
        /// 契約書識別コード
        /// 20:継続アルバイト契約
        /// 21:体験アルバイト契約
        /// 10:長期雇用契約
        /// 11:短期雇用契約
        /// 30:誓約書
        /// 40:失墜行為確認書
        /// 50:満了一カ月前通知
        /// </summary>
        public int Code {
            get => this._code;
            set => this._code = value;
        }
        /// <summary>
        /// 従事者コード
        /// </summary>
        public int StaffCode {
            get => this._staffCode;
            set => this._staffCode = value;
        }
        /// <summary>
        /// 契約開始日
        /// </summary>
        public DateTime StartDate {
            get => this._startDate;
            set => this._startDate = value;
        }
        /// <summary>
        /// 契約終了日
        /// </summary>
        public DateTime EndDate {
            get => this._endDate;
            set => this._endDate = value;
        }
        /// <summary>
        /// メモ
        /// </summary>
        public string Memo {
            get => this._memo;
            set => this._memo = value;
        }
        /// <summary>
        /// 契約書画像
        /// </summary>
        public byte[] Picture {
            get => this._picture;
            set => this._picture = value;
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
