/*
 * 2025-11-07
 */
namespace Vo {
    public class CarWorkingDaysVo {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);

        private DateTime _operationDate;                    // 稼働日
        private int _setCode;                               // 配車先コード
        private string _setName;                            // 配車先名
        private int _carCode;                               // 車両コード
        private string _registrationNumber;                 // 車両登録番号
        private string _doorNumber;                         // ドアNo
        private int _classificationCode;                    // 分類コード
        private string _classificationName;                 // 分類名
        private string _disguiseKind2;                      // 車種２
        private int _staffCode;                             // 運転者コード
        private string _staffName;                          // 運転者名
        private string _remarks;                            // 備考(H_VehicleDispatchDetailVo(配車記録ファイル))

        /// <summary>
        /// コンストラクター
        /// </summary>
        public CarWorkingDaysVo() {
            _operationDate = _defaultDateTime;
            _setCode = 0;
            _setName = string.Empty;
            _carCode = 0;
            _registrationNumber = string.Empty;
            _doorNumber = string.Empty;
            _classificationCode = 0;
            _classificationName = string.Empty;
            _disguiseKind2 = string.Empty;
            _staffCode = 0;
            _staffName = string.Empty;
            _remarks = string.Empty;
        }

        /// <summary>
        /// 稼働日
        /// </summary>
        public DateTime OperationDate {
            get => this._operationDate;
            set => this._operationDate = value;
        }
        /// <summary>
        /// 配車先コード
        /// </summary>
        public int SetCode {
            get => this._setCode;
            set => this._setCode = value;
        }
        /// <summary>
        /// 配車先名
        /// </summary>
        public string SetName {
            get => this._setName;
            set => this._setName = value;
        }
        /// <summary>
        /// 車両コード
        /// </summary>
        public int CarCode {
            get => this._carCode;
            set => this._carCode = value;
        }
        /// <summary>
        /// 車両登録番号
        /// </summary>
        public string RegistrationNumber {
            get => this._registrationNumber;
            set => this._registrationNumber = value;
        }
        /// <summary>
        /// ドアNo
        /// </summary>
        public string DoorNumber {
            get => this._doorNumber;
            set => this._doorNumber = value;
        }
        /// <summary>
        /// 分類コード
        /// </summary>
        public int ClassificationCode {
            get => this._classificationCode;
            set => this._classificationCode = value;
        }
        /// <summary>
        /// 分類名
        /// </summary>
        public string ClassificationName {
            get => this._classificationName;
            set => this._classificationName = value;
        }
        /// <summary>
        /// 車種２
        /// </summary>
        public string DisguiseKind2 {
            get => this._disguiseKind2;
            set => this._disguiseKind2 = value;
        }
        /// <summary>
        /// 運転者コード
        /// </summary>
        public int StaffCode {
            get => this._staffCode;
            set => this._staffCode = value;
        }
        /// <summary>
        /// 運転者名
        /// </summary>
        public string StaffName {
            get => this._staffName;
            set => this._staffName = value;
        }
        /// <summary>
        /// 備考(H_VehicleDispatchDetailVo.StaffMemo1)
        /// </summary>
        public string Remarks {
            get => this._remarks;
            set => this._remarks = value;
        }
    }
}
