/*
 * 2024-11-05
 */
namespace Vo {
    /// <summary>
    /// 契約書マスター
    /// </summary>
    public class EmploymentAgreementVo {
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);

        private int _staffCode;
        private int _contractExpirationPeriod;
        private bool _experienceFlag;
        private DateTime _experienceStartDate;
        private DateTime _experienceEndDate;
        private string _experienceMemo;
        private byte[] _experiencePicture;
        private List<ContractExpirationPartTimeJobVo> _listContractExpirationPartTimeJobVo;
        private List<ContractExpirationLongJobVo> _listContractExpirationLongJobVo;
        private List<ContractExpirationShortJobVo> _listContractExpirationShortJobVo;
        private List<WrittenPledgeVo> _listWrittenPledgeVo;
        private List<LossWrittenPledgeVo> _listLossWrittenPledgeVo;
        private List<ContractExpirationNoticeVo> _listContractExpirationNoticeVo;
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
        public EmploymentAgreementVo() {
            _staffCode = 0;
            _contractExpirationPeriod = 0;
            _experienceFlag = false;
            _experienceStartDate = _defaultDateTime;
            _experienceEndDate = _defaultDateTime;
            _experienceMemo = string.Empty;
            _experiencePicture = Array.Empty<byte>();
            _listContractExpirationPartTimeJobVo = new();
            _listContractExpirationLongJobVo = new();
            _listContractExpirationShortJobVo = new();
            _listWrittenPledgeVo = new();
            _listLossWrittenPledgeVo = new();
            _listContractExpirationNoticeVo = new();
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
        /// 契約期間
        /// </summary>
        public int ContractExpirationPeriod {
            get => _contractExpirationPeriod;
            set => _contractExpirationPeriod = value;
        }
        /// <summary>
        /// 体験入社フラグ
        /// true:体験入社済 false:未登録
        /// </summary>
        public bool ExperienceFlag {
            get => _experienceFlag;
            set => _experienceFlag = value;
        }
        /// <summary>
        /// 体験入社開始日
        /// </summary>
        public DateTime ExperienceStartDate {
            get => _experienceStartDate;
            set => _experienceStartDate = value;
        }
        /// <summary>
        /// 体験入社終了日
        /// </summary>
        public DateTime ExperienceEndDate {
            get => this._experienceEndDate;
            set => this._experienceEndDate = value;
        }
        /// <summary>
        /// 体験入社メモ
        /// </summary>
        public string ExperienceMemo {
            get => this._experienceMemo;
            set => this._experienceMemo = value;
        }
        /// <summary>
        /// 体験入社画像
        /// </summary>
        public byte[] ExperiencePicture {
            get => this._experiencePicture;
            set => this._experiencePicture = value;
        }
        /// <summary>
        /// アルバイト用リスト
        /// </summary>
        public List<ContractExpirationPartTimeJobVo> ListContractExpirationPartTimeJobVo {
            get => this._listContractExpirationPartTimeJobVo;
            set => this._listContractExpirationPartTimeJobVo = value;
        }
        /// <summary>
        /// 長期雇用リスト
        /// </summary>
        public List<ContractExpirationLongJobVo> ListContractExpirationLongJobVo {
            get => this._listContractExpirationLongJobVo;
            set => this._listContractExpirationLongJobVo = value;
        }
        /// <summary>
        /// 短期雇用リスト
        /// </summary>
        public List<ContractExpirationShortJobVo> ListContractExpirationShortJobVo {
            get => this._listContractExpirationShortJobVo;
            set => this._listContractExpirationShortJobVo = value;
        }
        /// <summary>
        /// 誓約書
        /// </summary>
        public List<WrittenPledgeVo> ListWrittenPledgeVo {
            get => this._listWrittenPledgeVo;
            set => this._listWrittenPledgeVo = value;
        }
        /// <summary>
        /// 失墜行為
        /// </summary>
        public List<LossWrittenPledgeVo> ListLossWrittenPledgeVo {
            get => this._listLossWrittenPledgeVo;
            set => this._listLossWrittenPledgeVo = value;
        }
        /// <summary>
        /// 契約満了通知
        /// </summary>
        public List<ContractExpirationNoticeVo> ListContractExpirationNoticeVo {
            get => this._listContractExpirationNoticeVo;
            set => this._listContractExpirationNoticeVo = value;
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
