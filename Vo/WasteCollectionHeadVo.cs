/*
 * 2026-01-26
 */
namespace Vo {
    public class WasteCollectionHeadVo {
        private DateTime _defaultDateTime = new(1900, 01, 01);
        private int _id;
        private DateTime _officeQuotationDate;
        private int _officeRequestWord;
        private string _officeRequestWordName;
        private string _officeCompanyName;
        private string _officeContactPerson;
        private string _officeAddress;
        private string _officeTelephoneNumber;
        private string _officeCellphoneNumber;
        private string _workSiteLocation;
        private string _workSiteAddress;
        private DateTime _pickupDate;
        private string _remarks;
        private byte[] _mainPicture;
        private byte[] _subPicture;
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
        public WasteCollectionHeadVo() {
            this._id = 0;
            this._officeQuotationDate = this._defaultDateTime;
            this._officeRequestWord = 0;
            this._officeRequestWordName = string.Empty;                                 // H_WordMaster
            this._officeCompanyName = string.Empty;
            this._officeContactPerson = string.Empty;
            this._officeAddress = string.Empty;
            this._officeTelephoneNumber = string.Empty;
            this._officeCellphoneNumber = string.Empty;
            this._workSiteLocation = string.Empty;
            this._workSiteAddress = string.Empty;
            this._pickupDate = this._defaultDateTime;
            this._remarks = string.Empty;
            this._mainPicture = Array.Empty<byte>();
            this._subPicture = Array.Empty<byte>();
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
        public int Id {
            get => this._id;
            set => this._id = value;
        }
        /// <summary>
        /// 見積日
        /// </summary>
        public DateTime OfficeQuotationDate {
            get => this._officeQuotationDate;
            set => this._officeQuotationDate = value;
        }
        /// <summary>
        /// 依頼区(コード)
        /// </summary>
        public int OfficeRequestWord {
            get => this._officeRequestWord;
            set => this._officeRequestWord = value;
        }
        /// <summary>
        /// 依頼区名
        /// </summary>
        public string OfficeRequestWordName {
            get => this._officeRequestWordName;
            set => this._officeRequestWordName = value;
        }
        /// <summary>
        /// 本社　会社名
        /// </summary>
        public string OfficeCompanyName {
            get => this._officeCompanyName;
            set => this._officeCompanyName = value;
        }
        /// <summary>
        /// 本社　担当者
        /// </summary>
        public string OfficeContactPerson {
            get => this._officeContactPerson;
            set => this._officeContactPerson = value;
        }
        /// <summary>
        /// 本社　住所
        /// </summary>
        public string OfficeAddress {
            get => this._officeAddress;
            set => this._officeAddress = value;
        }
        /// <summary>
        /// 本社　連絡先
        /// </summary>
        public string OfficeTelephoneNumber {
            get => this._officeTelephoneNumber;
            set => this._officeTelephoneNumber = value;
        }
        /// <summary>
        /// 本社　携帯番号
        /// </summary>
        public string OfficeCellphoneNumber {
            get => this._officeCellphoneNumber;
            set => this._officeCellphoneNumber = value;
        }
        /// <summary>
        /// 現場　回収場所
        /// </summary>
        public string WorkSiteLocation {
            get => this._workSiteLocation;
            set => this._workSiteLocation = value;
        }
        /// <summary>
        /// 現場　住所
        /// </summary>
        public string WorkSiteAddress {
            get => this._workSiteAddress;
            set => this._workSiteAddress = value;
        }
        /// <summary>
        /// 回収日
        /// </summary>
        public DateTime PickupDate {
            get => this._pickupDate;
            set => this._pickupDate = value;
        }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks {
            get => this._remarks;
            set => this._remarks = value;
        }
        /// <summary>
        /// 写真１
        /// </summary>
        public byte[] MainPicture {
            get => this._mainPicture;
            set => this._mainPicture = value;
        }
        /// <summary>
        /// 写真２
        /// </summary>
        public byte[] SubPicture {
            get => this._subPicture;
            set => this._subPicture = value;
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
