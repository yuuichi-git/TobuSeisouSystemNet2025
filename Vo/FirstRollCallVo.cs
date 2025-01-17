/*
 * 2024-02-19
 */
namespace Vo {
    public class FirstRollCallVo {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private DateTime _operationDate;
        private string _rollCallName1;
        private string _rollCallName2;
        private string _rollCallName3;
        private string _rollCallName4;
        private string _rollCallName5;
        private string _weather;
        private string _instruction1;
        private string _instruction2;
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
        public FirstRollCallVo() {
            _operationDate = _defaultDateTime;
            _rollCallName1 = string.Empty;
            _rollCallName2 = string.Empty;
            _rollCallName3 = string.Empty;
            _rollCallName4 = string.Empty;
            _rollCallName5 = string.Empty;
            _weather = string.Empty;
            _instruction1 = string.Empty;
            _instruction2 = string.Empty;
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;
        }

        /// <summary>
        /// 点呼年月日
        /// </summary>
        public DateTime OperationDate {
            get => _operationDate;
            set => _operationDate = value;
        }
        /// <summary>
        /// 出庫時点呼者１
        /// </summary>
        public string RollCallName1 {
            get => _rollCallName1;
            set => _rollCallName1 = value;
        }
        /// <summary>
        /// 出庫時点呼者２
        /// </summary>
        public string RollCallName2 {
            get => _rollCallName2;
            set => _rollCallName2 = value;
        }
        /// <summary>
        /// 帰庫時点呼者１
        /// </summary>
        public string RollCallName3 {
            get => _rollCallName3;
            set => _rollCallName3 = value;
        }
        /// <summary>
        /// 帰庫時点呼者２
        /// </summary>
        public string RollCallName4 {
            get => _rollCallName4;
            set => _rollCallName4 = value;
        }
        /// <summary>
        /// 三郷車庫点呼者
        /// </summary>
        public string RollCallName5 {
            get => _rollCallName5;
            set => _rollCallName5 = value;
        }
        /// <summary>
        /// 天候
        /// </summary>
        public string Weather {
            get => _weather;
            set => _weather = value;
        }
        /// <summary>
        /// 周知事項１
        /// </summary>
        public string Instruction1 {
            get => _instruction1;
            set => _instruction1 = value;
        }
        /// <summary>
        /// 周知事項２
        /// </summary>
        public string Instruction2 {
            get => _instruction2;
            set => _instruction2 = value;
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
