/*
 * 2025-2-1
 */
namespace Vo {
    public class StaffWorkingHoursVo {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private DateTime _operationDate;
        private string _setName;
        private DateTime _firstRollCallTime;
        private DateTime _lastRollCallTime;
        private int _workingMinute;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public StaffWorkingHoursVo() {
            _operationDate = _defaultDateTime;
            _setName = string.Empty;
            _firstRollCallTime = _defaultDateTime;
            _lastRollCallTime = _defaultDateTime;
            _workingMinute = 0;
        }

        /// <summary>
        /// 配車日付
        /// </summary>
        public DateTime OperationDate {
            get => this._operationDate;
            set => this._operationDate = value;
        }
        /// <summary>
        /// 配車先名
        /// </summary>
        public string SetName {
            get => this._setName;
            set => this._setName = value;
        }
        /// <summary>
        /// 出庫点呼日時
        /// </summary>
        public DateTime FirstRollCallTime {
            get => this._firstRollCallTime;
            set => this._firstRollCallTime = value;
        }
        /// <summary>
        /// 帰庫点呼日時
        /// </summary>
        public DateTime LastRollCallTime {
            get => this._lastRollCallTime;
            set => this._lastRollCallTime = value;
        }
        /// <summary>
        /// 労働時間（分）
        /// </summary>
        public int WorkingMinute {
            get => this._workingMinute;
            set => this._workingMinute = value;
        }
    }
}
