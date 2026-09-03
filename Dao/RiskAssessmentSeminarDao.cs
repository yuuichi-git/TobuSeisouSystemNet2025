/*
 * 2026-08-31
 */
using System.Data;
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class RiskAssessmentSeminarDao {
        private DateTime _defaultDatetime = new(1900, 01, 01);
        private DefaultValue _defaultValue = new();
        private DateUtility _dateUtility = new();
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
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

        /// <summary>
        /// SelectRiskAssessmentSeminarListVo
        /// 画面表示に必要なデータを取得する
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<RiskAssessmentSeminarListVo> SelectRiskAssessmentSeminarList(DateTime startDate, DateTime endDate) {
            /*
             * 短期を含めるかどうかのSQLを作成
             * Belongs 10:役員 11:社員 12:アルバイト 13:派遣 20:新運転 21:自運労
             * JobForm 10:長期雇用 11:手帳 12:アルバイト 99:指定なし
             */

            List<RiskAssessmentSeminarListVo> listRiskAssessmentSeminarListVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT H_BelongsMaster.Code AS BelongsCode," +
                                            "H_BelongsMaster.Name AS BelongsName," +
                                            "H_JobFormMaster.Code AS JobFormCode," +
                                            "H_JobFormMaster.Name AS JobFormName," +
                                            "H_OccupationMaster.Code AS OccupationCode," +
                                            "H_OccupationMaster.Name AS OccupationName," +
                                            "H_StaffMaster.UnionCode," +
                                            "H_StaffMaster.StaffCode," +
                                            "H_StaffMaster.Name AS StaffName," +
                                            "H_StaffMaster.EmploymentDate," +
                                            "(SELECT StudentsFlag FROM H_RiskAssessmentSeminar WHERE H_StaffMaster.StaffCode = H_RiskAssessmentSeminar.StaffCode " +
                                                                                          "AND H_RiskAssessmentSeminar.StudentsCode = 0 " +
                                                                                          "AND H_RiskAssessmentSeminar.StudentsDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "') AS Students01Flag," +
                                            "(SELECT StudentsFlag FROM H_RiskAssessmentSeminar WHERE H_StaffMaster.StaffCode = H_RiskAssessmentSeminar.StaffCode " +
                                                                                          "AND H_RiskAssessmentSeminar.StudentsCode = 1 " +
                                                                                          "AND H_RiskAssessmentSeminar.StudentsDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "') AS Students02Flag," +
                                            "(SELECT StudentsFlag FROM H_RiskAssessmentSeminar WHERE H_StaffMaster.StaffCode = H_RiskAssessmentSeminar.StaffCode " +
                                                                                          "AND H_RiskAssessmentSeminar.StudentsCode = 2 " +
                                                                                          "AND H_RiskAssessmentSeminar.StudentsDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "') AS Students03Flag " +
                                     "FROM H_StaffMaster " +
                                     "LEFT OUTER JOIN H_OccupationMaster ON H_StaffMaster.Occupation = H_OccupationMaster.Code " +
                                     "LEFT OUTER JOIN H_JobFormMaster ON H_StaffMaster.JobForm = H_JobFormMaster.Code " +
                                     "LEFT OUTER JOIN H_BelongsMaster ON H_StaffMaster.Belongs = H_BelongsMaster.Code " +
                                     "WHERE H_StaffMaster.RiskAssessmentFlag = 'true' AND H_StaffMaster.RetirementFlag = 'false' " +
                                     "ORDER BY H_StaffMaster.NameKana ASC";
            using(SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while(sqlDataReader.Read() == true) {
                    RiskAssessmentSeminarListVo riskAssessmentSeminarListVo = new();
                    riskAssessmentSeminarListVo.Belongs = _defaultValue.GetDefaultValue<int>(sqlDataReader["BelongsCode"]);
                    riskAssessmentSeminarListVo.BelongsName = _defaultValue.GetDefaultValue<string>(sqlDataReader["BelongsName"]);
                    riskAssessmentSeminarListVo.JobForm = _defaultValue.GetDefaultValue<int>(sqlDataReader["JobFormCode"]);
                    riskAssessmentSeminarListVo.JobFormName = _defaultValue.GetDefaultValue<string>(sqlDataReader["JobFormName"]);
                    riskAssessmentSeminarListVo.OccupationCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["OccupationCode"]);
                    riskAssessmentSeminarListVo.OccupationName = _defaultValue.GetDefaultValue<string>(sqlDataReader["OccupationName"]);
                    riskAssessmentSeminarListVo.UnionCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["UnionCode"]);
                    riskAssessmentSeminarListVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    riskAssessmentSeminarListVo.StaffName = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffName"]);
                    riskAssessmentSeminarListVo.EmploymentDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["EmploymentDate"]);
                    riskAssessmentSeminarListVo.Students01Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Students01Flag"]);
                    riskAssessmentSeminarListVo.Students02Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Students02Flag"]);
                    riskAssessmentSeminarListVo.Students03Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Students03Flag"]);
                    listRiskAssessmentSeminarListVo.Add(riskAssessmentSeminarListVo);
                }
                return listRiskAssessmentSeminarListVo;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fiscalYear"></param>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public List<RiskAssessmentSeminarVo>? SelectRiskAssessmentSeminar(int fiscalYear, int staffCode) {
            List<RiskAssessmentSeminarVo> listRiskAssessmentSeminarVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT Id," +
                                            "StudentsDate," +
                                            "StudentsCode," +
                                            "StudentsFlag," +
                                            "StaffCode," +
                                            "StaffSign," +
                                            "SignNumber," +
                                            "Memo," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_RiskAssessmentSeminar " +
                                     "WHERE (StudentsDate BETWEEN '" + _dateUtility.GetFiscalYearStartDate(fiscalYear) + "' AND '" + _dateUtility.GetFiscalYearEndDate(fiscalYear) + "') " +
                                     "AND StaffCode = " + staffCode;
            using(SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while(sqlDataReader.Read() == true) {
                    RiskAssessmentSeminarVo riskAssessmentSeminarVo = new();
                    riskAssessmentSeminarVo.Id = _defaultValue.GetDefaultValue<string>(sqlDataReader["Id"]);
                    riskAssessmentSeminarVo.StudentsDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StudentsDate"]);
                    riskAssessmentSeminarVo.StudentsCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StudentsCode"]);
                    riskAssessmentSeminarVo.StudentsFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StudentsFlag"]);
                    riskAssessmentSeminarVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    riskAssessmentSeminarVo.StaffSign = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["StaffSign"]);
                    riskAssessmentSeminarVo.SignNumber = _defaultValue.GetDefaultValue<int>(sqlDataReader["SignNumber"]);
                    riskAssessmentSeminarVo.Memo = _defaultValue.GetDefaultValue<string>(sqlDataReader["Memo"]);
                    riskAssessmentSeminarVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    riskAssessmentSeminarVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    riskAssessmentSeminarVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    riskAssessmentSeminarVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    riskAssessmentSeminarVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    riskAssessmentSeminarVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    riskAssessmentSeminarVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listRiskAssessmentSeminarVo.Add(riskAssessmentSeminarVo);
                }
                return listRiskAssessmentSeminarVo;
            }
        }

        /// <summary>
        /// InsertOneRiskAssessmentSeminar
        /// </summary>
        /// <param name="riskAssessmentSeminarVo"></param>
        /// <returns></returns>
        public int InsertOneRiskAssessmentSeminar(RiskAssessmentSeminarVo riskAssessmentSeminarVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_RiskAssessmentSeminar(Id," +
                                                                         "StudentsDate," +
                                                                         "StudentsCode," +
                                                                         "StudentsFlag," +
                                                                         "StaffCode," +
                                                                         "StaffSign," +
                                                                         "SignNumber," +
                                                                         "Memo," +
                                                                         "InsertPcName," +
                                                                         "InsertYmdHms," +
                                                                         "UpdatePcName," +
                                                                         "UpdateYmdHms," +
                                                                         "DeletePcName," +
                                                                         "DeleteYmdHms," +
                                                                         "DeleteFlag) " +
                                     "VALUES ('" + riskAssessmentSeminarVo.Id + "'," +
                                             "'" + riskAssessmentSeminarVo.StudentsDate + "'," +
                                              "" + riskAssessmentSeminarVo.StudentsCode + "," +
                                             "'" + riskAssessmentSeminarVo.StudentsFlag + "'," +
                                              "" + riskAssessmentSeminarVo.StaffCode + "," +
                                             "@Picture," +
                                              "" + riskAssessmentSeminarVo.SignNumber + "," +
                                             "'" + riskAssessmentSeminarVo.Memo + "'," +
                                             "'" + riskAssessmentSeminarVo.InsertPcName + "'," +
                                             "'" + riskAssessmentSeminarVo.InsertYmdHms + "'," +
                                             "'" + riskAssessmentSeminarVo.UpdatePcName + "'," +
                                             "'" + riskAssessmentSeminarVo.UpdateYmdHms + "'," +
                                             "'" + riskAssessmentSeminarVo.DeletePcName + "'," +
                                             "'" + riskAssessmentSeminarVo.DeleteYmdHms + "'," +
                                             "'" + riskAssessmentSeminarVo.DeleteFlag + "'" +
                                             ");";
            if(riskAssessmentSeminarVo.StaffSign is not null)
                sqlCommand.Parameters.Add("@Picture", SqlDbType.Image, riskAssessmentSeminarVo.StaffSign.Length).Value = riskAssessmentSeminarVo.StaffSign;
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// UpdateOneRiskAssessmentSeminar
        /// </summary>
        /// <param name="beforeRiskAssessmentSeminarVo"></param>
        /// <param name="afterRiskAssessmentSeminarVo"></param>
        /// <returns></returns>
        public int UpdateOneRiskAssessmentSeminar(RiskAssessmentSeminarVo beforeRiskAssessmentSeminarVo, RiskAssessmentSeminarVo afterRiskAssessmentSeminarVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_RiskAssessmentSeminar " +
                                        "SET Id           = '" + _defaultValue.GetDefaultValue<string>(beforeRiskAssessmentSeminarVo.Id) + "'," +
                                            "StudentsDate = '" + _defaultValue.GetDefaultValue<DateTime>(afterRiskAssessmentSeminarVo.StudentsDate) + "'," +
                                            "StudentsCode = " + _defaultValue.GetDefaultValue<int>(afterRiskAssessmentSeminarVo.StudentsCode) + "," +
                                            "StudentsFlag = '" + _defaultValue.GetDefaultValue<bool>(afterRiskAssessmentSeminarVo.StudentsFlag) + "'," +
                                            "StaffCode    = " + _defaultValue.GetDefaultValue<int>(afterRiskAssessmentSeminarVo.StaffCode) + "," +
                                            "StaffSign    = @Picture," +
                                            "SignNumber   = " + _defaultValue.GetDefaultValue<int>(afterRiskAssessmentSeminarVo.SignNumber) + "," +
                                            "Memo         = '" + _defaultValue.GetDefaultValue<string>(afterRiskAssessmentSeminarVo.Memo) + "'," +
                                            "InsertPcName = '" + _defaultValue.GetDefaultValue<string>(afterRiskAssessmentSeminarVo.InsertPcName) + "'," +
                                            "InsertYmdHms = '" + _defaultValue.GetDefaultValue<DateTime>(afterRiskAssessmentSeminarVo.InsertYmdHms) + "'," +
                                            "UpdatePcName = '" + _defaultValue.GetDefaultValue<string>(afterRiskAssessmentSeminarVo.UpdatePcName) + "'," +
                                            "UpdateYmdHms = '" + _defaultValue.GetDefaultValue<DateTime>(afterRiskAssessmentSeminarVo.UpdateYmdHms) + "'," +
                                            "DeletePcName = '" + _defaultValue.GetDefaultValue<string>(afterRiskAssessmentSeminarVo.DeletePcName) + "'," +
                                            "DeleteYmdHms = '" + _defaultValue.GetDefaultValue<DateTime>(afterRiskAssessmentSeminarVo.DeleteYmdHms) + "'," +
                                            "DeleteFlag   = '" + _defaultValue.GetDefaultValue<bool>(afterRiskAssessmentSeminarVo.DeleteFlag) + "' " +
                                     "WHERE Id = '" + beforeRiskAssessmentSeminarVo.Id + "'";
            if(afterRiskAssessmentSeminarVo.StaffSign is not null)
                sqlCommand.Parameters.Add("@Picture", SqlDbType.Image, afterRiskAssessmentSeminarVo.StaffSign.Length).Value = afterRiskAssessmentSeminarVo.StaffSign;
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 1件のリスクアセスメント研修受講情報を削除する
        /// ※物理削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteOneRiskAssessmentSeminar(string id) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM H_RiskAssessmentSeminar " +
                                     "WHERE Id = '" + id + "'";
            try {
                return sqlCommand.ExecuteNonQuery();
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
