/*
 * 2024-02-19 
 */
namespace Vo {
    public class LastRollCallVo {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private int _setCode;
        private DateTime _operationDate;
        private DateTime _firstRollCallHms;
        private int _lastPlantCount;
        private string _lastPlantName;
        private DateTime _lastPlantHms;
        private DateTime _lastRollCallHms;
        private decimal _firstOdoMeter;
        private decimal _lastOdoMeter;
        private decimal _oilAmount;
        private string _insertPcName;
        private DateTime _insertYmsHms;
        private string _updatePcName;
        private DateTime _updateYmdHms;
        private string _deletePcName;
        private DateTime _deleteYmsHms;
        private bool _deleteFlag;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public LastRollCallVo() {
            _setCode = 0;
            _operationDate = _defaultDateTime;
            _firstRollCallHms = _defaultDateTime;
            _lastPlantCount = 0;
            _lastPlantName = string.Empty;
            _lastPlantHms = _defaultDateTime;
            _lastRollCallHms = _defaultDateTime;
            _firstOdoMeter = 0;
            _lastOdoMeter = 0;
            _oilAmount = 0;
            _insertPcName = string.Empty;
            _insertYmsHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmsHms = _defaultDateTime;
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
        /// 点呼日
        /// </summary>
        public DateTime OperationDate {
            get => _operationDate;
            set => _operationDate = value;
        }
        /// <summary>
        /// 出庫点呼日時
        /// </summary>
        public DateTime FirstRollCallYmdHms {
            get => _firstRollCallHms;
            set => _firstRollCallHms = value;
        }
        /// <summary>
        /// 運搬回数
        /// </summary>
        public int LastPlantCount {
            get => _lastPlantCount;
            set => _lastPlantCount = value;
        }
        /// <summary>
        /// 最終運搬場所
        /// </summary>
        public string LastPlantName {
            get => _lastPlantName;
            set => _lastPlantName = value;
        }
        /// <summary>
        /// 最終運搬日時
        /// </summary>
        public DateTime LastPlantYmdHms {
            get => _lastPlantHms;
            set => _lastPlantHms = value;
        }
        /// <summary>
        /// 帰社日時
        /// </summary>
        public DateTime LastRollCallYmdHms {
            get => _lastRollCallHms;
            set => _lastRollCallHms = value;
        }
        /// <summary>
        /// 出庫時メーター
        /// </summary>
        public decimal FirstOdoMeter {
            get => _firstOdoMeter;
            set => _firstOdoMeter = value;
        }
        /// <summary>
        /// 帰庫時メーター
        /// </summary>
        public decimal LastOdoMeter {
            get => _lastOdoMeter;
            set => _lastOdoMeter = value;
        }
        /// <summary>
        /// 給油量
        /// </summary>
        public decimal OilAmount {
            get => _oilAmount;
            set => _oilAmount = value;
        }
        public string InsertPcName {
            get => _insertPcName;
            set => _insertPcName = value;
        }
        public DateTime InsertYmdHms {
            get => _insertYmsHms;
            set => _insertYmsHms = value;
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
            get => _deleteYmsHms;
            set => _deleteYmsHms = value;
        }
        public bool DeleteFlag {
            get => _deleteFlag;
            set => _deleteFlag = value;
        }
    }
}
