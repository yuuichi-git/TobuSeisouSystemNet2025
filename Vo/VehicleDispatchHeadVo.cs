/*
 * 2023-11-06
 */
namespace Vo {
    public class VehicleDispatchHeadVo {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);

        private int _cellNumber;
        private bool _vehicleDispatchFlag;
        private bool _purpose;
        private int _setCode;
        private int _financialYear;
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
        public VehicleDispatchHeadVo() {
            _cellNumber = 0;
            _vehicleDispatchFlag = false;
            _purpose = false;
            _setCode = 0;
            _financialYear = 0;
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;
        }

        /// <summary>
        /// 配車板上のCellの位置
        /// </summary>
        public int CellNumber {
            get => _cellNumber;
            set => _cellNumber = value;
        }
        /// <summary>
        /// 配車フラグ
        /// true:配車の割当てが有る false:割当てが無い
        /// </summary>
        public bool VehicleDispatchFlag {
            get => _vehicleDispatchFlag;
            set => _vehicleDispatchFlag = value;
        }
        /// <summary>
        /// SetControlの形状
        /// true:２列 false:１列
        /// </summary>
        public bool Purpose {
            get => _purpose;
            set => _purpose = value;
        }
        /// <summary>
        /// 配車先コード
        /// </summary>
        public int SetCode {
            get => _setCode;
            set => _setCode = value;
        }
        /// <summary>
        /// 配車年度
        /// </summary>
        public int FinancialYear {
            get => _financialYear;
            set => _financialYear = value;
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
