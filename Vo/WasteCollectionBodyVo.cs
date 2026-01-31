/*
 * 2026-01-26
 */
namespace Vo {
    public class WasteCollectionBodyVo {
        private DateTime _defaultDateTime = new(1900, 01, 01);
        private string _id;
        private int _numberOfRow;
        private string _itemName;
        private string _itemSize;
        private int _numberOfUnits;
        private decimal _unitPrice;
        private string _others;
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
        public WasteCollectionBodyVo() {
            this._id = string.Empty;
            this._numberOfRow = 0;
            this._itemName = string.Empty;
            this._itemSize = string.Empty;
            this._numberOfUnits = 0;
            this._unitPrice = 0;
            this._others = string.Empty;
            this._insertPcName = string.Empty;
            this._insertYmdHms = this._defaultDateTime;
            this._updatePcName = string.Empty;
            this._updateYmdHms = this._defaultDateTime;
            this._deletePcName = string.Empty;
            this._deleteYmdHms = this._defaultDateTime;
            this._deleteFlag = false;
        }

        /// <summary>
        /// ID
        /// </summary>
        public string Id {
            get => this._id;
            set => this._id = value;
        }
        /// <summary>
        /// データ番号
        /// </summary>
        public int NumberOfRow {
            get => this._numberOfRow;
            set => this._numberOfRow = value;
        }
        /// <summary>
        /// 品名
        /// </summary>
        public string ItemName {
            get => this._itemName;
            set => this._itemName = value;
        }
        /// <summary>
        /// サイズ
        /// </summary>
        public string ItemSize {
            get => this._itemSize;
            set => this._itemSize = value;
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int NumberOfUnits {
            get => this._numberOfUnits;
            set => this._numberOfUnits = value;
        }
        /// <summary>
        /// 単価
        /// </summary>
        public decimal UnitPrice {
            get => this._unitPrice;
            set => this._unitPrice = value;
        }
        /// <summary>
        /// 備考
        /// </summary>
        public string Others {
            get => this._others;
            set => this._others = value;
        }
        public string InsertPcName {
            get => this._insertPcName;
            set => this._insertPcName = value;
        }
        public DateTime InsertYmdHms {
            get => this._insertYmdHms;
            set => this._insertYmdHms = value;
        }
        public string UpdatePcName {
            get => this._updatePcName;
            set => this._updatePcName = value;
        }
        public DateTime UpdateYmdHms {
            get => this._updateYmdHms;
            set => this._updateYmdHms = value;
        }
        public string DeletePcName {
            get => this._deletePcName;
            set => this._deletePcName = value;
        }
        public DateTime DeleteYmdHms {
            get => this._deleteYmdHms;
            set => this._deleteYmdHms = value;
        }
        public bool DeleteFlag {
            get => this._deleteFlag;
            set => this._deleteFlag = value;
        }
    }
}
