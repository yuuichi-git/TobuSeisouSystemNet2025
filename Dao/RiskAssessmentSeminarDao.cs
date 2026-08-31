/*
 * 2026-08-31
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class RiskAssessmentSeminarDao {
        private DateTime _defaultDatetime = new(1900, 01, 01);
        private DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;

        public RiskAssessmentSeminarDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// レコードが存在するか確認する
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true:存在する, false:存在しない</returns>
        public bool ExistenceRiskAssessmentSeminar(string id) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(Id) " +
                                     "FROM H_RiskAssessmentSeminar " +
                                     "WHERE Id = '" + id + "'";
            try {
                return (int)sqlCommand.ExecuteScalar() > 0 ? true : false;
            } catch {
                throw;
            }
        }






    }

    /// <summary>
    /// リスクアセスメント研修受講情報Vo
    /// </summary>
    public class RiskAssessmentSeminarListVo {
        private string _id;
        private int _belongs;
        private string _belongsName;
        private int _jobForm;
        private int _unionCode;
        private string _jobFormName;
        private int _occupationCode;
        private string _occupationName;
        private int _staffCode;
        private string _staffName;
        private DateTime _employmentDate;
        private bool _students01Flag;
        private bool _students02Flag;
        private bool _students03Flag;

        private readonly DateTime _defaultDatetime = new(1900, 01, 01);

        /// <summary>
        /// コンストラクター
        /// </summary>
        public RiskAssessmentSeminarListVo() {
            _id = string.Empty;
            _belongs = 0;
            _belongsName = string.Empty;
            _jobForm = 0;
            _unionCode = 0;
            _jobFormName = string.Empty;
            _occupationCode = 0;
            _occupationName = string.Empty;
            _staffCode = 0;
            _staffName = string.Empty;
            _employmentDate = _defaultDatetime;
            _students01Flag = false;
            _students02Flag = false;
            _students03Flag = false;
        }

        /// <summary>
        /// ID
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
        public int UnionCode {
            get {
                return _unionCode;
            }

            set {
                _unionCode = value;
            }
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
    }
}
