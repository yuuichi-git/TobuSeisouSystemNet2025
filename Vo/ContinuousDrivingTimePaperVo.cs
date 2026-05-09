/*
 * 2026-05-08
 */
namespace Vo {
    public class ContinuousDrivingTimePaperVo {
        private DateTime _operationDate = new(1900, 01, 01);
        private string _staffDisplayName = string.Empty;
        private string _jobForm = string.Empty;
        private string _setName = string.Empty;
        private string _carRegistrationNumber = string.Empty;
        private string _firstLollCallHms = string.Empty;
        private string _lastLollCallHms = string.Empty;
        private TimeSpan _continuosDrivingTime = TimeSpan.Zero;
        private string _remarks = string.Empty;

        /// <summary>
        /// 運行日
        /// </summary>
        public DateTime OperationDate {
            get => _operationDate;
            set => _operationDate = value;
        }
        /// <summary>
        /// 運転者名
        /// </summary>
        public string StaffDisplayName {
            get => _staffDisplayName;
            set => _staffDisplayName = value;
        }
        /// <summary>
        /// 雇用形態
        /// </summary>
        public string JobForm {
            get => _jobForm;
            set => _jobForm = value;
        }
        /// <summary>
        /// 配車先
        /// </summary>
        public string SetName {
            get => _setName;
            set => _setName = value;
        }
        /// <summary>
        /// 車両番号
        /// </summary>
        public string CarRegistrationNumber {
            get => _carRegistrationNumber;
            set => _carRegistrationNumber = value;
        }
        /// <summary>
        /// 運行開始時刻
        /// </summary>
        public string FirstLollCallHms {
            get => _firstLollCallHms;
            set => _firstLollCallHms = value;
        }
        /// <summary>
        /// 運行終了時刻
        /// </summary>
        public string LastLollCallHms {
            get => _lastLollCallHms;
            set => _lastLollCallHms = value;
        }
        /// <summary>
        /// 連続運転時間
        /// </summary>
        public TimeSpan ContinuosDrivingTime {
            get => _continuosDrivingTime;
            set => _continuosDrivingTime = value;
        }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks {
            get => _remarks;
            set => _remarks = value;
        }
    }
}
