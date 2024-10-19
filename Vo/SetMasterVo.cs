/*
 * 配車先の基本情報
 * 13101～13123までが23区コード
 */
namespace Vo {
    /*
     * DeepCopyで使用
     */
    [Serializable] // ←DeepCopyする場合には必要
    public class SetMasterVo {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);

        private int _setCode;
        private int _wordCode;
        private string _setName;
        private string _setName1;
        private string _setName2;
        private int _fareCode;
        private int _managedSpaceCode;
        private int _classificationCode;
        private int _contactMethod;
        private int _numberOfPeople;
        private bool _spareOfPeople;
        private string _workingDays;
        private bool _fiveLap;
        private bool _moveFlag;
        private string _remarks;
        private string _telephoneNumber;
        private string _faxNumber;
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
        public SetMasterVo() {
            _setCode = 0;
            _wordCode = 0;
            _setName = string.Empty;
            _setName1 = string.Empty;
            _setName2 = string.Empty;
            _fareCode = 0;
            _managedSpaceCode = 0;
            _classificationCode = 0;
            _contactMethod = 0;
            _numberOfPeople = 0;
            _spareOfPeople = false;
            _workingDays = string.Empty;
            _fiveLap = true;
            _moveFlag = true;
            _remarks = string.Empty;
            _telephoneNumber = string.Empty;
            _faxNumber = string.Empty;
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;


        }

        /// <summary>
        /// 組番号
        /// </summary>
        public int SetCode {
            get => _setCode;
            set => _setCode = value;
        }
        /// <summary>
        /// 市区町村コード
        /// </summary>
        public int WordCode {
            get => _wordCode;
            set => _wordCode = value;
        }
        /// <summary>
        /// 組名
        /// </summary>
        public string SetName {
            get => _setName;
            set => _setName = value;
        }
        /// <summary>
        /// 組名 略称1
        /// Label表示の1行目
        /// </summary>
        public string SetName1 {
            get => _setName1;
            set => _setName1 = value;
        }
        /// <summary>
        /// 組名 略称2
        /// Label表示の2行目
        /// </summary>
        public string SetName2 {
            get => _setName2;
            set => _setName2 = value;
        }
        /// <summary>
        /// 運賃コード
        /// </summary>
        public int FareCode {
            get => _fareCode;
            set => _fareCode = value;
        }
        /// <summary>
        /// 車庫地
        /// 0:該当なし 1:足立 2:三郷
        /// </summary>
        public int ManagedSpaceCode {
            get => _managedSpaceCode;
            set => _managedSpaceCode = value;
        }
        /// <summary>
        /// 分類コード
        /// 10:雇上 11:区契 12:臨時 20:清掃工場 30:社内 50:一般 51:社用車 99:指定なし
        /// </summary>
        public int ClassificationCode {
            get => _classificationCode;
            set => _classificationCode = value;
        }
        /// <summary>
        /// 代番連絡方法
        /// 10:電話 11:Fax 12:しない 13:TEL/FAX
        /// </summary>
        public int ContactMethod {
            get => _contactMethod;
            set => _contactMethod = value;
        }
        /// <summary>
        /// 配車基本人数
        /// 入力例:1～4
        /// </summary>
        public int NumberOfPeople {
            get => _numberOfPeople;
            set => _numberOfPeople = value;
        }
        /// <summary>
        /// スペアを付けられる配車先がどうか
        /// true:予備人員を付けられる false:予備人員を付けられない
        /// </summary>
        public bool SpareOfPeople {
            get => _spareOfPeople;
            set => _spareOfPeople = value;
        }
        /// <summary>
        /// 稼働日
        /// 入力例:"月火水木金土日"
        /// </summary>
        public string WorkingDays {
            get => _workingDays;
            set => _workingDays = value;
        }
        /// <summary>
        /// 第五週の稼働
        /// true:稼働 false:休車
        /// </summary>
        public bool FiveLap {
            get => _fiveLap;
            set => _fiveLap = value;
        }
        /// <summary>
        /// 移動フラグ
        /// true:移動できる false:移動できない
        /// </summary>
        public bool MoveFlag {
            get => _moveFlag;
            set => _moveFlag = value;
        }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks {
            get => _remarks;
            set => _remarks = value;
        }
        /// <summary>
        /// 電話番号
        /// </summary>
        public string TelephoneNumber {
            get => _telephoneNumber;
            set => _telephoneNumber = value;
        }
        /// <summary>
        /// Fax番号
        /// </summary>
        public string FaxNumber {
            get => _faxNumber;
            set => _faxNumber = value;
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
