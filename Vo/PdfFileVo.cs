/*
 * 2026-07-12
 */
namespace Vo {
    public class PdfFileVo {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private string _id;
        private byte[] _pdfImage;
        private string _insertPcName;
        private DateTime _insertYmdHms;
        private string _updatePcName;
        private DateTime _updateYmdHms;
        private string _deletePcName;
        private DateTime _deleteYmdHms;
        private bool _deleteFlag;

        public PdfFileVo() {
            _id = string.Empty;
            _pdfImage = Array.Empty<byte>();
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;
        }

        /// <summary>
        /// Guidを使用した一意の識別子
        /// </summary>
        public string Id {
            get {
                return _id;
            }

            set {
                _id = value;
            }
        }
        /// <summary>
        /// PDFファイルのバイナリデータ
        /// </summary>
        public byte[] PdfImage {
            get {
                return _pdfImage;
            }

            set {
                _pdfImage = value;
            }
        }

        public string InsertPcName {
            get {
                return _insertPcName;
            }

            set {
                _insertPcName = value;
            }
        }

        public DateTime InsertYmdHms {
            get {
                return _insertYmdHms;
            }

            set {
                _insertYmdHms = value;
            }
        }

        public string UpdatePcName {
            get {
                return _updatePcName;
            }

            set {
                _updatePcName = value;
            }
        }

        public DateTime UpdateYmdHms {
            get {
                return _updateYmdHms;
            }

            set {
                _updateYmdHms = value;
            }
        }

        public string DeletePcName {
            get {
                return _deletePcName;
            }

            set {
                _deletePcName = value;
            }
        }

        public DateTime DeleteYmdHms {
            get {
                return _deleteYmdHms;
            }

            set {
                _deleteYmdHms = value;
            }
        }

        public bool DeleteFlag {
            get {
                return _deleteFlag;
            }

            set {
                _deleteFlag = value;
            }
        }
    }
}
