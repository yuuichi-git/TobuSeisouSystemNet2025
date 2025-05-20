/*
 * 2024-04-27
 */
namespace Vo {
    public class LegalTwelveItemListVo {
        private readonly DateTime _defaultDatetime = new(1900, 01, 01);
        private int _belongs;
        private string _belongsName;
        private int _jobForm;
        private string _jobFormName;
        private int _occupationCode;
        private string _occupationName;
        private int _staffCode;
        private string _staffName;
        private DateTime _employmentDate;
        private bool _students01Flag;
        private bool _students02Flag;
        private bool _students03Flag;
        private bool _students04Flag;
        private bool _students05Flag;
        private bool _students06Flag;
        private bool _students07Flag;
        private bool _students08Flag;
        private bool _students09Flag;
        private bool _students10Flag;
        private bool _students11Flag;
        private bool _students12Flag;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public LegalTwelveItemListVo() {
            _belongs = 0;
            _belongsName = string.Empty;
            _jobForm = 0;
            _jobFormName = string.Empty;
            _occupationCode = 0;
            _occupationName = string.Empty;
            _staffCode = 0;
            _staffName = string.Empty;
            _employmentDate = _defaultDatetime;
            _students01Flag = false;
            _students02Flag = false;
            _students03Flag = false;
            _students04Flag = false;
            _students05Flag = false;
            _students06Flag = false;
            _students07Flag = false;
            _students08Flag = false;
            _students09Flag = false;
            _students10Flag = false;
            _students11Flag = false;
            _students12Flag = false;
        }

        /// <summary>
        /// 所属
        /// 10:役員 11:社員 12:アルバイト 13:派遣 20:新運転 21:自運労 99:指定なし
        /// </summary>
        public int Belongs {
            get => _belongs;
            set => _belongs = value;
        }
        /// <summary>
        /// 所属名
        /// 10:役員 11:社員 12:アルバイト 13:派遣 20:新運転 21:自運労 99:指定なし
        /// </summary>
        public string BelongsName {
            get => _belongsName;
            set => _belongsName = value;
        }
        /// <summary>
        /// 雇用形態
        /// 10:長期雇用 11:手帳 12:アルバイト 99:指定なし
        /// </summary>
        public int JobForm {
            get => _jobForm;
            set => _jobForm = value;
        }
        /// <summary>
        /// 雇用形態名
        /// 10:長期雇用 11:手帳 12:アルバイト 99:指定なし
        /// </summary>
        public string JobFormName {
            get => _jobFormName;
            set => _jobFormName = value;
        }
        /// <summary>
        /// 職種
        /// 10:運転手 11:作業員 20:事務職 99:指定なし
        /// </summary>
        public int OccupationCode {
            get => _occupationCode;
            set => _occupationCode = value;
        }
        /// <summary>
        /// 職種名
        /// 10:運転手 11:作業員 20:事務職 99:指定なし
        /// </summary>
        public string OccupationName {
            get => _occupationName;
            set => _occupationName = value;
        }
        public int StaffCode {
            get => _staffCode;
            set => _staffCode = value;
        }
        public string StaffName {
            get => _staffName;
            set => _staffName = value;
        }
        /// <summary>
        /// 雇用年月日
        /// </summary>
        public DateTime EmploymentDate {
            get => _employmentDate;
            set => _employmentDate = value;
        }
        /// <summary>
        /// 項目受講フラグ
        /// </summary>
        public bool Students01Flag {
            get => _students01Flag;
            set => _students01Flag = value;
        }
        /// <summary>
        /// 項目受講フラグ
        /// </summary>
        public bool Students02Flag {
            get => _students02Flag;
            set => _students02Flag = value;
        }
        /// <summary>
        /// 項目受講フラグ
        /// </summary>
        public bool Students03Flag {
            get => _students03Flag;
            set => _students03Flag = value;
        }
        /// <summary>
        /// 項目受講フラグ
        /// </summary>
        public bool Students04Flag {
            get => _students04Flag;
            set => _students04Flag = value;
        }
        /// <summary>
        /// 項目受講フラグ
        /// </summary>
        public bool Students05Flag {
            get => _students05Flag;
            set => _students05Flag = value;
        }
        /// <summary>
        /// 項目受講フラグ
        /// </summary>
        public bool Students06Flag {
            get => _students06Flag;
            set => _students06Flag = value;
        }
        /// <summary>
        /// 項目受講フラグ
        /// </summary>
        public bool Students07Flag {
            get => _students07Flag;
            set => _students07Flag = value;
        }
        /// <summary>
        /// 項目受講フラグ
        /// </summary>
        public bool Students08Flag {
            get => _students08Flag;
            set => _students08Flag = value;
        }
        /// <summary>
        /// 項目受講フラグ
        /// </summary>
        public bool Students09Flag {
            get => _students09Flag;
            set => _students09Flag = value;
        }
        /// <summary>
        /// 項目受講フラグ
        /// </summary>
        public bool Students10Flag {
            get => _students10Flag;
            set => _students10Flag = value;
        }
        /// <summary>
        /// 項目受講フラグ
        /// </summary>
        public bool Students11Flag {
            get => _students11Flag;
            set => _students11Flag = value;
        }
        /// <summary>
        /// 項目受講フラグ
        /// </summary>
        public bool Students12Flag {
            get => _students12Flag;
            set => _students12Flag = value;
        }
    }
}
