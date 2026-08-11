/*
 * 2024-05-22
 */
namespace Vo {
    public class CertificationFileVo {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);

        private int _staffCode;
        private int _certificationCode;
        private int _markCode;
        private bool _image1Flag;
        private byte[] _image1;
        private bool _image2Flag;
        private byte[] _image2;
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
        public CertificationFileVo() {
            _staffCode = 0;
            _certificationCode = 0;
            _markCode = 0;
            _image1Flag = false;
            _image1 = Array.Empty<byte>();
            _image2Flag = false;
            _image2 = Array.Empty<byte>();
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;
        }

        /// <summary>
        /// 従事者コード
        /// </summary>
        public int StaffCode {
            get => _staffCode;
            set => _staffCode = value;
        }
        /// <summary>
        /// 資格コード
        /// </summary>
        public int CertificationCode {
            get => _certificationCode;
            set => _certificationCode = value;
        }
        /// <summary>
        /// <summary>
        /// 〇印の種類　0→◎,1→○,2→●
        /// </summary>
        public int MarkCode {
            get => _markCode;
            set => _markCode = value;
        }
        /// <summary>
        /// Image1の存在の有無
        /// True:画像あり False:画像なし
        /// </summary>
        public bool Image1Flag {
            get => this._image1Flag;
            set => this._image1Flag = value;
        }
        public byte[] Image1 {
            get => _image1;
            set => _image1 = value;
        }
        /// <summary>
        /// Image2の存在の有無
        /// True:画像あり False:画像なし
        /// </summary>
        public bool Image2Flag {
            get => this._image2Flag;
            set => this._image2Flag = value;
        }
        public byte[] Image2 {
            get => _image2;
            set => _image2 = value;
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
