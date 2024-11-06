/*
 * 2024-11-05
 */
namespace Vo {
    /// <summary>
    /// 誓約書管理テーブル
    /// </summary>
    public class WrittenPledgeVo {
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);

        private int _staffCode;
        private DateTime _contractExpirationStartDate;
        private DateTime _contractExpirationEndDate;
        private string _memo;
        private byte[] _picture;
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
        public WrittenPledgeVo() {
            _staffCode = 0;
            _contractExpirationStartDate = _defaultDateTime;
            _contractExpirationEndDate = _defaultDateTime;
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
        /// 従事者コード
        /// </summary>
        public int StaffCode {
            get => this._staffCode;
            set => this._staffCode = value;
        }
        /// <summary>
        /// 契約開始日
        /// </summary>
        public DateTime ContractExpirationStartDate {
            get => this._contractExpirationStartDate;
            set => this._contractExpirationStartDate = value;
        }
        /// <summary>
        /// 契約終了日
        /// </summary>
        public DateTime ContractExpirationEndDate {
            get => this._contractExpirationEndDate;
            set => this._contractExpirationEndDate = value;
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
