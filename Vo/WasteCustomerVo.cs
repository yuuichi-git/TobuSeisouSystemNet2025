/*
 * 2025-06-16
 */
namespace Vo {
    public class WasteCustomerVo {
        private DateTime _defaultDateTime = new(1900, 01, 01);
        private int _id;
        private DateTime _concludedDate;
        private string _concludedDetail;
        private string _transportationCompany;
        private string _emissionCompanyKana;
        private string _emissionCompanyName;
        private string _postNumber;
        private string _address;
        private string _telephoneNumber;
        private string _faxNumber;
        private string _emissionPlaceName;
        private string _emissionPlaceAddress;
        private decimal _unitPriceFlammable;
        private decimal _unitPriceCollection;
        private decimal _unitPriceDisposal;
        private decimal _unitPriceResources;
        private decimal _unitPriceTransportationCosts;
        private decimal _unitPriceManifestCosts;
        private decimal _unitPriceOtherCosts;
        private decimal _unitPriceBulkyTransportationCosts;
        private decimal _unitPriceBulkyDisposal;
        private string _remarks;
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
        public WasteCustomerVo() {
            _id = 0;
            _concludedDate = _defaultDateTime;
            _concludedDetail = string.Empty;
            _transportationCompany = string.Empty;
            _emissionCompanyKana = string.Empty;
            _emissionCompanyName = string.Empty;
            _postNumber = string.Empty;
            _address = string.Empty;
            _telephoneNumber = string.Empty;
            _faxNumber = string.Empty;
            _emissionPlaceName = string.Empty;
            _emissionPlaceAddress = string.Empty;
            _unitPriceFlammable = 0;
            _unitPriceCollection = 0;
            _unitPriceDisposal = 0;
            _unitPriceResources = 0;
            _unitPriceTransportationCosts = 0;
            _unitPriceManifestCosts = 0;
            _unitPriceOtherCosts = 0;
            _unitPriceBulkyTransportationCosts = 0;
            _unitPriceBulkyDisposal = 0;
            _remarks = string.Empty;
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;
        }

        /// <summary>
        /// 一意のId
        /// </summary>
        public int Id {
            get => this._id;
            set => this._id = value;
        }
        /// <summary>
        /// 契約締結日
        /// </summary>
        public DateTime ConcludedDate {
            get => this._concludedDate;
            set => this._concludedDate = value;
        }
        /// <summary>
        /// 契約内容
        /// </summary>
        public string ConcludedDetail {
            get => this._concludedDetail;
            set => this._concludedDetail = value;
        }
        /// <summary>
        /// 運搬会社
        /// </summary>
        public string TransportationCompany {
            get => this._transportationCompany;
            set => this._transportationCompany = value;
        }
        /// <summary>
        /// 排出事業者フリガナ
        /// </summary>
        public string EmissionCompanyKana {
            get => this._emissionCompanyKana;
            set => this._emissionCompanyKana = value;
        }
        /// <summary>
        /// 排出事業者名称
        /// </summary>
        public string EmissionCompanyName {
            get => this._emissionCompanyName;
            set => this._emissionCompanyName = value;
        }
        /// <summary>
        /// 排出事業者郵便番号
        /// </summary>
        public string PostNumber {
            get => this._postNumber;
            set => this._postNumber = value;
        }
        /// <summary>
        /// 排出事業者住所
        /// </summary>
        public string Address {
            get => this._address;
            set => this._address = value;
        }
        /// <summary>
        /// 排出事業者電話番号
        /// </summary>
        public string TelephoneNumber {
            get => this._telephoneNumber;
            set => this._telephoneNumber = value;
        }
        /// <summary>
        /// 排出事業者FAX番号
        /// </summary>
        public string FaxNumber {
            get => this._faxNumber;
            set => this._faxNumber = value;
        }
        /// <summary>
        /// 排出事業所名称
        /// </summary>
        public string EmissionPlaceName {
            get => this._emissionPlaceName;
            set => this._emissionPlaceName = value;
        }
        /// <summary>
        /// 排出事業所住所
        /// </summary>
        public string EmissionPlaceAddress {
            get => this._emissionPlaceAddress;
            set => this._emissionPlaceAddress = value;
        }
        /// <summary>
        /// 単価　可燃
        /// </summary>
        public decimal UnitPriceFlammable {
            get => this._unitPriceFlammable;
            set => this._unitPriceFlammable = value;
        }
        /// <summary>
        /// 単価　不燃収集
        /// </summary>
        public decimal UnitPriceCollection {
            get => this._unitPriceCollection;
            set => this._unitPriceCollection = value;
        }
        /// <summary>
        /// 単価　不燃処分
        /// </summary>
        public decimal UnitPriceDisposal {
            get => this._unitPriceDisposal;
            set => this._unitPriceDisposal = value;
        }
        /// <summary>
        /// 単価　資源
        /// </summary>
        public decimal UnitPriceResources {
            get => this._unitPriceResources;
            set => this._unitPriceResources = value;
        }
        /// <summary>
        /// 単価　運搬費
        /// </summary>
        public decimal UnitPriceTransportationCosts {
            get => this._unitPriceTransportationCosts;
            set => this._unitPriceTransportationCosts = value;
        }
        /// <summary>
        /// 単価　マニフェスト費用
        /// </summary>
        public decimal UnitPriceManifestCosts {
            get => this._unitPriceManifestCosts;
            set => this._unitPriceManifestCosts = value;
        }
        /// <summary>
        /// 単価　その他費用
        /// </summary>
        public decimal UnitPriceOtherCosts {
            get => this._unitPriceOtherCosts;
            set => this._unitPriceOtherCosts = value;
        }
        /// <summary>
        /// 単価　粗大運搬費
        /// </summary>
        public decimal UnitPriceBulkyTransportationCosts {
            get => this._unitPriceBulkyTransportationCosts;
            set => this._unitPriceBulkyTransportationCosts = value;
        }
        /// <summary>
        /// 単価　粗大処分費
        /// </summary>
        public decimal UnitPriceBulkyDisposal {
            get => this._unitPriceBulkyDisposal;
            set => this._unitPriceBulkyDisposal = value;
        }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks {
            get => this._remarks;
            set => this._remarks = value;
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
