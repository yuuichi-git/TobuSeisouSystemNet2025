/*
 * 2023-12-31
 */
namespace Vo {
    public class VehicleDispatchDetailVo {
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);

        private int _cellNumber;
        private DateTime _operationDate;
        private bool _operationFlag;
        private bool _vehicleDispatchFlag;
        private bool _purposeFlag;
        private int _setCode;
        private int _managedSpaceCode;
        private int _classificationCode;
        private bool _lastRollCallFlag;
        private DateTime _lastRollCallYmdHms;
        private bool _setMemoFlag;
        private string _setMemo;
        private int _shiftCode;
        private bool _standByFlag;
        private bool _addWorkerFlag;
        private bool _contactInfomationFlag;
        private bool _faxTransmissionFlag;
        private int _carCode;
        private int _carGarageCode;
        private bool _carProxyFlag;
        private bool _carMemoFlag;
        private string _carMemo;
        private int _targetStaffNumber;
        private int _staffCode1;
        private int _staffOccupation1;
        private bool _staffProxyFlag1;
        private bool _staffRollCallFlag1;
        private DateTime _staffRollCallYmdHms1;
        private bool _staffMemoFlag1;
        private string _staffMemo1;
        private int _staffCode2;
        private int _staffOccupation2;
        private bool _staffProxyFlag2;
        private bool _staffRollCallFlag2;
        private DateTime _staffRollCallYmdHms2;
        private bool _staffMemoFlag2;
        private string _staffMemo2;
        private int _staffCode3;
        private int _staffOccupation3;
        private bool _staffProxyFlag3;
        private bool _staffRollCallFlag3;
        private DateTime _staffRollCallYmdHms3;
        private bool _staffMemoFlag3;
        private string _staffMemo3;
        private int _staffCode4;
        private int _staffOccupation4;
        private bool _staffProxyFlag4;
        private bool _staffRollCallFlag4;
        private DateTime _staffRollCallYmdHms4;
        private bool _staffMemoFlag4;
        private string _staffMemo4;
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
        public VehicleDispatchDetailVo() {
            _cellNumber = 0;
            _operationDate = _defaultDateTime;
            _operationFlag = false;
            _vehicleDispatchFlag = false;
            _purposeFlag = false;
            _setCode = 0;
            _managedSpaceCode = 0;
            _classificationCode = 99;
            _lastRollCallFlag = false;
            _lastRollCallYmdHms = _defaultDateTime;
            _setMemoFlag = false;
            _setMemo = string.Empty;
            _shiftCode = 0;
            _standByFlag = false;
            _addWorkerFlag = false;
            _contactInfomationFlag = false;
            _faxTransmissionFlag = false;
            _carCode = 0;
            _carGarageCode = 0;
            _carProxyFlag = false;
            _carMemoFlag = false;
            _carMemo = string.Empty;
            _targetStaffNumber = 99;
            _staffCode1 = 0;
            _staffOccupation1 = 99;
            _staffProxyFlag1 = false;
            _staffRollCallFlag1 = false;
            _staffRollCallYmdHms1 = _defaultDateTime;
            _staffMemoFlag1 = false;
            _staffMemo1 = string.Empty;
            _staffCode2 = 0;
            _staffOccupation2 = 99;
            _staffProxyFlag2 = false;
            _staffRollCallFlag2 = false;
            _staffRollCallYmdHms2 = _defaultDateTime;
            _staffMemoFlag2 = false;
            _staffMemo2 = string.Empty;
            _staffCode3 = 0;
            _staffOccupation3 = 99;
            _staffProxyFlag3 = false;
            _staffRollCallFlag3 = false;
            _staffRollCallYmdHms3 = _defaultDateTime;
            _staffMemoFlag3 = false;
            _staffMemo3 = string.Empty;
            _staffCode4 = 0;
            _staffOccupation4 = 99;
            _staffProxyFlag4 = false;
            _staffRollCallFlag4 = false;
            _staffRollCallYmdHms4 = _defaultDateTime;
            _staffMemoFlag4 = false;
            _staffMemo4 = string.Empty;
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;
        }

        /// <summary>
        /// 配車表№
        /// 0～199の番号
        /// </summary>
        public int CellNumber {
            get => _cellNumber;
            set => _cellNumber = value;
        }
        /// <summary>
        /// 稼働日
        /// </summary>
        public DateTime OperationDate {
            get => _operationDate;
            set => _operationDate = value;
        }
        /// <summary>
        /// 稼働フラグ
        /// true:稼働日 false:休車
        /// </summary>
        public bool OperationFlag {
            get => _operationFlag;
            set => _operationFlag = value;
        }
        /// <summary>
        /// 配車フラグ
        /// true:配車されている false:配車されていない
        /// </summary>
        public bool VehicleDispatchFlag {
            get => _vehicleDispatchFlag;
            set => _vehicleDispatchFlag = value;
        }
        /// <summary>
        /// H_SetControlの形状
        /// true:2列 false:1列
        /// </summary>
        public bool PurposeFlag {
            get => _purposeFlag;
            set => _purposeFlag = value;
        }
        /// <summary>
        /// 配車先コード
        /// </summary>
        public int SetCode {
            get => _setCode;
            set => _setCode = value;
        }
        /// <summary>
        /// 管理地
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
        /// 帰庫点呼フラグ
        /// true:帰庫点呼記録済 false:未点呼
        /// </summary>
        public bool LastRollCallFlag {
            get => _lastRollCallFlag;
            set => _lastRollCallFlag = value;
        }
        /// <summary>
        /// 帰庫点呼日時
        /// </summary>
        public DateTime LastRollCallYmdHms {
            get => _lastRollCallYmdHms;
            set => _lastRollCallYmdHms = value;
        }
        /// <summary>
        /// メモフラグ
        /// true:メモが存在する false:メモが存在しない
        /// </summary>
        public bool SetMemoFlag {
            get => _setMemoFlag;
            set => _setMemoFlag = value;
        }
        /// <summary>
        /// メモ
        /// </summary>
        public string SetMemo {
            get => _setMemo;
            set => _setMemo = value;
        }
        /// <summary>
        /// 番手コード
        /// 0:指定なし 1:早番 2:遅番
        /// </summary>
        public int ShiftCode {
            get => _shiftCode;
            set => _shiftCode = value;
        }
        /// <summary>
        /// 待機フラグ
        /// true:待機 false:通常
        /// </summary>
        public bool StandByFlag {
            get => _standByFlag;
            set => _standByFlag = value;
        }
        /// <summary>
        /// 作業員付きフラグ
        /// true:作業員付き false:作業員なし
        /// </summary>
        public bool AddWorkerFlag {
            get => _addWorkerFlag;
            set => _addWorkerFlag = value;
        }
        /// <summary>
        /// 連絡事項印フラグ
        /// true:連絡事項あり false:連絡事項なし
        /// </summary>
        public bool ContactInfomationFlag {
            get => _contactInfomationFlag;
            set => _contactInfomationFlag = value;
        }
        /// <summary>
        /// FAX送信フラグ
        /// true:Fax送信 false:なし
        /// </summary>
        public bool FaxTransmissionFlag {
            get => _faxTransmissionFlag;
            set => _faxTransmissionFlag = value;
        }
        /// <summary>
        /// 車両コード
        /// </summary>
        public int CarCode {
            get => _carCode;
            set => _carCode = value;
        }
        /// <summary>
        /// 車庫地コード
        /// 0:該当なし 1:本社 2:三郷
        /// </summary>
        public int CarGarageCode {
            get => _carGarageCode;
            set => _carGarageCode = value;
        }
        /// <summary>
        /// 代車フラグ
        /// true:代車 false:本番車
        /// </summary>
        public bool CarProxyFlag {
            get => _carProxyFlag;
            set => _carProxyFlag = value;
        }
        /// <summary>
        /// メモフラグ
        /// true:メモが存在する false:メモが存在しない
        /// </summary>
        public bool CarMemoFlag {
            get => _carMemoFlag;
            set => _carMemoFlag = value;
        }
        /// <summary>
        /// メモ
        /// </summary>
        public string CarMemo {
            get => _carMemo;
            set => _carMemo = value;
        }
        /// <summary>
        /// TargetStaffの番号
        /// 0:運転手 1:作業員１ 2:作業員２ 3:作業員３
        /// </summary>
        public int TargetStaffNumber {
            get => _targetStaffNumber;
            set => _targetStaffNumber = value;
        }
        /// <summary>
        /// 従事者コード1
        /// </summary>
        public int StaffCode1 {
            get => _staffCode1;
            set => _staffCode1 = value;
        }
        /// <summary>
        /// 職種コード1
        /// 10:運転手 11:作業員 12:自転車駐輪場 13:リサイクルセンター 20:事務職 99:指定なし
        /// </summary>
        public int StaffOccupation1 {
            get => _staffOccupation1;
            set => _staffOccupation1 = value;
        }
        /// <summary>
        /// 代番フラグ1
        /// true:代番 false:本番
        /// </summary>
        public bool StaffProxyFlag1 {
            get => _staffProxyFlag1;
            set => _staffProxyFlag1 = value;
        }
        /// <summary>
        /// 点呼フラグ1
        /// true:点呼実施 false:点呼未実施
        /// </summary>
        public bool StaffRollCallFlag1 {
            get => _staffRollCallFlag1;
            set => _staffRollCallFlag1 = value;
        }
        /// <summary>
        /// 点呼日時1
        /// </summary>
        public DateTime StaffRollCallYmdHms1 {
            get => _staffRollCallYmdHms1;
            set => _staffRollCallYmdHms1 = value;
        }
        /// <summary>
        /// メモフラグ1
        /// true:メモが存在する false:メモが存在しない
        /// </summary>
        public bool StaffMemoFlag1 {
            get => _staffMemoFlag1;
            set => _staffMemoFlag1 = value;
        }
        /// <summary>
        /// メモ1
        /// </summary>
        public string StaffMemo1 {
            get => _staffMemo1;
            set => _staffMemo1 = value;
        }
        /// <summary>
        /// 従事者コード2
        /// </summary>
        public int StaffCode2 {
            get => _staffCode2;
            set => _staffCode2 = value;
        }
        /// <summary>
        /// 職種コード2
        /// 10:運転手 11:作業員 12:自転車駐輪場 13:リサイクルセンター 20:事務職 99:指定なし
        /// </summary>
        public int StaffOccupation2 {
            get => _staffOccupation2;
            set => _staffOccupation2 = value;
        }
        /// <summary>
        /// 代番フラグ2
        /// true:代番 false:本番
        /// </summary>
        public bool StaffProxyFlag2 {
            get => _staffProxyFlag2;
            set => _staffProxyFlag2 = value;
        }
        /// <summary>
        /// 点呼フラグ2
        /// true:点呼実施 false:点呼未実施
        /// </summary>
        public bool StaffRollCallFlag2 {
            get => _staffRollCallFlag2;
            set => _staffRollCallFlag2 = value;
        }
        /// <summary>
        /// 点呼日時2
        /// </summary>
        public DateTime StaffRollCallYmdHms2 {
            get => _staffRollCallYmdHms2;
            set => _staffRollCallYmdHms2 = value;
        }
        /// <summary>
        /// メモフラグ2
        /// true:メモが存在する false:メモが存在しない
        /// </summary>
        public bool StaffMemoFlag2 {
            get => _staffMemoFlag2;
            set => _staffMemoFlag2 = value;
        }
        /// <summary>
        /// メモ2
        /// </summary>
        public string StaffMemo2 {
            get => _staffMemo2;
            set => _staffMemo2 = value;
        }
        /// <summary>
        /// 従事者コード3
        /// </summary>
        public int StaffCode3 {
            get => _staffCode3;
            set => _staffCode3 = value;
        }
        /// <summary>
        /// 職種コード3
        /// 10:運転手 11:作業員 12:自転車駐輪場 13:リサイクルセンター 20:事務職 99:指定なし
        /// </summary>
        public int StaffOccupation3 {
            get => _staffOccupation3;
            set => _staffOccupation3 = value;
        }
        /// <summary>
        /// 代番フラグ3
        /// true:代番 false:本番
        /// </summary>
        public bool StaffProxyFlag3 {
            get => _staffProxyFlag3;
            set => _staffProxyFlag3 = value;
        }
        /// <summary>
        /// 点呼フラグ3
        /// true:点呼実施 false:点呼未実施
        /// </summary>
        public bool StaffRollCallFlag3 {
            get => _staffRollCallFlag3;
            set => _staffRollCallFlag3 = value;
        }
        /// <summary>
        /// 点呼日時3
        /// </summary>
        public DateTime StaffRollCallYmdHms3 {
            get => _staffRollCallYmdHms3;
            set => _staffRollCallYmdHms3 = value;
        }
        /// <summary>
        /// メモフラグ3
        /// true:メモが存在する false:メモが存在しない
        /// </summary>
        public bool StaffMemoFlag3 {
            get => _staffMemoFlag3;
            set => _staffMemoFlag3 = value;
        }
        /// <summary>
        /// メモ3
        /// </summary>
        public string StaffMemo3 {
            get => _staffMemo3;
            set => _staffMemo3 = value;
        }
        /// <summary>
        /// 従事者コード4
        /// </summary>
        public int StaffCode4 {
            get => _staffCode4;
            set => _staffCode4 = value;
        }
        /// <summary>
        /// 職種コード4
        /// 10:運転手 11:作業員 12:自転車駐輪場 13:リサイクルセンター 20:事務職 99:指定なし
        /// </summary>
        public int StaffOccupation4 {
            get => _staffOccupation4;
            set => _staffOccupation4 = value;
        }
        /// <summary>
        /// 代番フラグ4
        /// true:代番 false:本番
        /// </summary>
        public bool StaffProxyFlag4 {
            get => _staffProxyFlag4;
            set => _staffProxyFlag4 = value;
        }
        /// <summary>
        /// 点呼フラグ4
        /// true:点呼実施 false:点呼未実施
        /// </summary>
        public bool StaffRollCallFlag4 {
            get => _staffRollCallFlag4;
            set => _staffRollCallFlag4 = value;
        }
        /// <summary>
        /// 点呼日時4
        /// </summary>
        public DateTime StaffRollCallYmdHms4 {
            get => _staffRollCallYmdHms4;
            set => _staffRollCallYmdHms4 = value;
        }
        /// <summary>
        /// メモフラグ4
        /// true:メモが存在する false:メモが存在しない
        /// </summary>
        public bool StaffMemoFlag4 {
            get => _staffMemoFlag4;
            set => _staffMemoFlag4 = value;
        }
        /// <summary>
        /// メモ4
        /// </summary>
        public string StaffMemo4 {
            get => _staffMemo4;
            set => _staffMemo4 = value;
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
