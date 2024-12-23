/*
 * 2024-07-06
 * H_StaffDestinationで使用する
 */
namespace Vo {
    public class StaffDestinationVo {

        private readonly DateTime _defaultDateTime = new(1900, 01, 01);

        private DateTime _operationDate;
        private int _setCode;
        private string _setName;
        private int _staffCode1;
        private string _staffName1;
        private int _staffBelongs1;
        private int _staffJobForm1;
        private int _staffOccupation1;
        private bool _staffProxyFlag1;
        private bool _staffRollCallFlag1;
        private DateTime _staffRollCallYmdHms1;
        private bool _staffMemoFlag1;
        private string _staffMemo1;
        private int _staffCode2;
        private string _staffName2;
        private int _staffBelongs2;
        private int _staffJobForm2;
        private int _staffOccupation2;
        private bool _staffProxyFlag2;
        private bool _staffRollCallFlag2;
        private DateTime _staffRollCallYmdHms2;
        private bool _staffMemoFlag2;
        private string _staffMemo2;
        private int _staffCode3;
        private string _staffName3;
        private int _staffBelongs3;
        private int _staffJobForm3;
        private int _staffOccupation3;
        private bool _staffProxyFlag3;
        private bool _staffRollCallFlag3;
        private DateTime _staffRollCallYmdHms3;
        private bool _staffMemoFlag3;
        private string _staffMemo3;
        private int _staffCode4;
        private string _staffName4;
        private int _staffBelongs4;
        private int _staffJobForm4;
        private int _staffOccupation4;
        private bool _staffProxyFlag4;
        private bool _staffRollCallFlag4;
        private DateTime _staffRollCallYmdHms4;
        private bool _staffMemoFlag4;
        private string _staffMemo4;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public StaffDestinationVo() {
            _operationDate = _defaultDateTime;
            _setCode = 0;
            _setName = string.Empty;
            _staffCode1 = 0;
            _staffBelongs1 = 99;
            _staffJobForm1 = 99;
            _staffOccupation1 = 99;
            _staffProxyFlag1 = false;
            _staffRollCallFlag1 = false;
            _staffRollCallYmdHms1 = _defaultDateTime;
            _staffMemoFlag1 = false;
            _staffMemo1 = string.Empty;
            _staffCode2 = 0;
            _staffBelongs2 = 99;
            _staffJobForm2 = 99;
            _staffOccupation2 = 99;
            _staffProxyFlag2 = false;
            _staffRollCallFlag2 = false;
            _staffRollCallYmdHms2 = _defaultDateTime;
            _staffMemoFlag2 = false;
            _staffMemo2 = string.Empty;
            _staffCode3 = 0;
            _staffBelongs3 = 99;
            _staffJobForm3 = 99;
            _staffOccupation3 = 99;
            _staffProxyFlag3 = false;
            _staffRollCallFlag3 = false;
            _staffRollCallYmdHms3 = _defaultDateTime;
            _staffMemoFlag3 = false;
            _staffMemo3 = string.Empty;
            _staffCode4 = 0;
            _staffBelongs4 = 99;
            _staffJobForm4 = 99;
            _staffOccupation4 = 99;
            _staffProxyFlag4 = false;
            _staffRollCallFlag4 = false;
            _staffRollCallYmdHms4 = _defaultDateTime;
            _staffMemoFlag4 = false;
            _staffMemo4 = string.Empty;
        }

        /// <summary>
        /// 稼働日
        /// </summary>
        public DateTime OperationDate {
            get => _operationDate;
            set => _operationDate = value;
        }
        /// <summary>
        /// 
        /// </summary>
        public int SetCode {
            get => this._setCode;
            set => this._setCode = value;
        }
        /// <summary>
        /// 配車先名
        /// </summary>
        public string SetName {
            get => _setName;
            set => _setName = value;
        }
        /// <summary>
        /// 従事者コード1
        /// </summary>
        public int StaffCode1 {
            get => _staffCode1;
            set => _staffCode1 = value;
        }
        public string StaffName1 {
            get => this._staffName1;
            set => this._staffName1 = value;
        }
        public int StaffBelongs1 {
            get => this._staffBelongs1;
            set => this._staffBelongs1 = value;
        }
        public int StaffJobForm1 {
            get => this._staffJobForm1;
            set => this._staffJobForm1 = value;
        }
        /// <summary>
        /// 職種コード1
        /// 10:運転手 11:作業員 20:事務職 99:指定なし
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
        public string StaffName2 {
            get => this._staffName2;
            set => this._staffName2 = value;
        }
        public int StaffBelongs2 {
            get => this._staffBelongs2;
            set => this._staffBelongs2 = value;
        }
        public int StaffJobForm2 {
            get => this._staffJobForm2;
            set => this._staffJobForm2 = value;
        }
        /// <summary>
        /// 職種コード2
        /// 10:運転手 11:作業員 20:事務職 99:指定なし
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
        public string StaffName3 {
            get => this._staffName3;
            set => this._staffName3 = value;
        }
        public int StaffBelongs3 {
            get => this._staffBelongs3;
            set => this._staffBelongs3 = value;
        }
        public int StaffJobForm3 {
            get => this._staffJobForm3;
            set => this._staffJobForm3 = value;
        }
        /// <summary>
        /// 職種コード3
        /// 10:運転手 11:作業員 20:事務職 99:指定なし
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
        public string StaffName4 {
            get => this._staffName4;
            set => this._staffName4 = value;
        }
        public int StaffBelongs4 {
            get => this._staffBelongs4;
            set => this._staffBelongs4 = value;
        }
        public int StaffJobForm4 {
            get => this._staffJobForm4;
            set => this._staffJobForm4 = value;
        }
        /// <summary>
        /// 職種コード4
        /// 10:運転手 11:作業員 20:事務職 99:指定なし
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
        
    }
}
