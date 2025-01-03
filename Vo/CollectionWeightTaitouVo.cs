/*
 * 2024-03-16
 */
namespace Vo {
    public class CollectionWeightTaitouVo {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);

        private DateTime _operationDate;
        private int _weight1Total;
        private int _weight2Total;
        private int _weight3Total;
        private int _weight4Total;
        private int _weight5Total;
        private int _weight6Total;
        private int _weight7Total;
        private int _weight8Total;
        private int _weight9Total;
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
        public CollectionWeightTaitouVo() {
            _operationDate = _defaultDateTime;
            _weight1Total = 0;
            _weight2Total = 0;
            _weight3Total = 0;
            _weight4Total = 0;
            _weight5Total = 0;
            _weight6Total = 0;
            _weight7Total = 0;
            _weight8Total = 0;
            _weight9Total = 0;
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;
        }

        /// <summary>
        /// 稼働日
        /// </summary>
        public DateTime OperationDate {
            get => _operationDate;
            set => _operationDate = value;
        }
        /// <summary>
        /// 東武１組
        /// </summary>
        public int Weight1Total {
            get => _weight1Total;
            set => _weight1Total = value;
        }
        /// <summary>
        /// 東武２組
        /// </summary>
        public int Weight2Total {
            get => _weight2Total;
            set => _weight2Total = value;
        }
        /// <summary>
        /// 東武４組
        /// </summary>
        public int Weight3Total {
            get => _weight3Total;
            set => _weight3Total = value;
        }
        /// <summary>
        /// 東武臨時１
        /// </summary>
        public int Weight4Total {
            get => _weight4Total;
            set => _weight4Total = value;
        }
        /// <summary>
        /// 東武臨時２
        /// </summary>
        public int Weight5Total {
            get => _weight5Total;
            set => _weight5Total = value;
        }
        /// <summary>
        /// 三東３組
        /// </summary>
        public int Weight6Total {
            get => _weight6Total;
            set => _weight6Total = value;
        }
        /// <summary>
        /// 三東軽
        /// </summary>
        public int Weight7Total {
            get => _weight7Total;
            set => _weight7Total = value;
        }
        /// <summary>
        /// 臨時１
        /// </summary>
        public int Weight8Total {
            get => this._weight8Total;
            set => this._weight8Total = value;
        }
        /// <summary>
        /// 臨時２
        /// </summary>
        public int Weight9Total {
            get => this._weight9Total;
            set => this._weight9Total = value;
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
