/*
 * 2024-04-27
 */
using System.Data;
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class LegalTwelveItemDao {
        private readonly DateUtility _dateUtility = new();
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        private readonly LegalTwelveItemVo _legalTwelveItemVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public LegalTwelveItemDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            _legalTwelveItemVo = new();
        }

        /// <summary>
        /// ExistenceLegalTwelveItem
        /// </summary>
        /// <param name="legalTwelveItemVo">変更前のVo</param>
        /// <returns></returns>
        public bool ExistenceLegalTwelveItem(LegalTwelveItemVo legalTwelveItemVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(StudentsDate) " +
                                     "FROM H_LegalTwelveItem " +
                                     "WHERE (StudentsDate BETWEEN '" + legalTwelveItemVo.StudentsDate + "' AND '" + legalTwelveItemVo.StudentsDate + "') " +
                                     "AND StudentsCode = " + legalTwelveItemVo.StudentsCode + " " +
                                     "AND StaffCode = " + legalTwelveItemVo.StaffCode;
            try {
                return (int)sqlCommand.ExecuteScalar() > 0 ? true : false;
            } catch {
                throw;
            }
        }

        /// <summary>
        /// SelectLegalTwelveItemForm
        /// 画面表示に必要なデータを取得する
        /// </summary>
        /// <returns></returns>
        public List<LegalTwelveItemListVo> SelectLegalTwelveItemListVo(DateTime startDate, DateTime endDate) {
            /*
             * 短期を含めるかどうかのSQLを作成
             * Belongs 10:役員 11:社員 12:アルバイト 13:派遣 20:新運転 21:自運労
             * JobForm 10:長期雇用 11:手帳 12:アルバイト 99:指定なし
             */
            //string allTerm;
            //if (allTermFlag) {
            //    allTerm = "H_StaffMaster.Belongs IN (10,11,12,13,20,21) AND H_StaffMaster.JobForm IN(10,11,12,99) AND H_StaffMaster.Occupation = 10 AND H_StaffMaster.RetirementFlag = 'false' ";
            //} else {
            //    allTerm = "H_StaffMaster.Belongs IN (10,11,12,13,20,21) AND H_StaffMaster.JobForm IN(10,12,99) AND H_StaffMaster.Occupation = 10 AND H_StaffMaster.RetirementFlag = 'false' ";
            //}

            List<LegalTwelveItemListVo> listLegalTwelveItemVo = new();
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
                                            "(SELECT StudentsFlag FROM H_LegalTwelveItem WHERE H_StaffMaster.StaffCode = H_LegalTwelveItem.StaffCode " +
                                                                                          "AND H_LegalTwelveItem.StudentsCode = 0 " +
                                                                                          "AND H_LegalTwelveItem.StudentsDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "') AS Students01Flag," +
                                            "(SELECT StudentsFlag FROM H_LegalTwelveItem WHERE H_StaffMaster.StaffCode = H_LegalTwelveItem.StaffCode " +
                                                                                          "AND H_LegalTwelveItem.StudentsCode = 1 " +
                                                                                          "AND H_LegalTwelveItem.StudentsDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "') AS Students02Flag," +
                                            "(SELECT StudentsFlag FROM H_LegalTwelveItem WHERE H_StaffMaster.StaffCode = H_LegalTwelveItem.StaffCode " +
                                                                                          "AND H_LegalTwelveItem.StudentsCode = 2 " +
                                                                                          "AND H_LegalTwelveItem.StudentsDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "') AS Students03Flag," +
                                            "(SELECT StudentsFlag FROM H_LegalTwelveItem WHERE H_StaffMaster.StaffCode = H_LegalTwelveItem.StaffCode " +
                                                                                          "AND H_LegalTwelveItem.StudentsCode = 3 " +
                                                                                          "AND H_LegalTwelveItem.StudentsDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "') AS Students04Flag," +
                                            "(SELECT StudentsFlag FROM H_LegalTwelveItem WHERE H_StaffMaster.StaffCode = H_LegalTwelveItem.StaffCode " +
                                                                                          "AND H_LegalTwelveItem.StudentsCode = 4 " +
                                                                                          "AND H_LegalTwelveItem.StudentsDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "') AS Students05Flag," +
                                            "(SELECT StudentsFlag FROM H_LegalTwelveItem WHERE H_StaffMaster.StaffCode = H_LegalTwelveItem.StaffCode " +
                                                                                          "AND H_LegalTwelveItem.StudentsCode = 5 " +
                                                                                          "AND H_LegalTwelveItem.StudentsDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "') AS Students06Flag," +
                                            "(SELECT StudentsFlag FROM H_LegalTwelveItem WHERE H_StaffMaster.StaffCode = H_LegalTwelveItem.StaffCode " +
                                                                                          "AND H_LegalTwelveItem.StudentsCode = 6 " +
                                                                                          "AND H_LegalTwelveItem.StudentsDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "') AS Students07Flag," +
                                            "(SELECT StudentsFlag FROM H_LegalTwelveItem WHERE H_StaffMaster.StaffCode = H_LegalTwelveItem.StaffCode " +
                                                                                          "AND H_LegalTwelveItem.StudentsCode = 7 " +
                                                                                          "AND H_LegalTwelveItem.StudentsDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "') AS Students08Flag," +
                                            "(SELECT StudentsFlag FROM H_LegalTwelveItem WHERE H_StaffMaster.StaffCode = H_LegalTwelveItem.StaffCode " +
                                                                                          "AND H_LegalTwelveItem.StudentsCode = 8 " +
                                                                                          "AND H_LegalTwelveItem.StudentsDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "') AS Students09Flag," +
                                            "(SELECT StudentsFlag FROM H_LegalTwelveItem WHERE H_StaffMaster.StaffCode = H_LegalTwelveItem.StaffCode " +
                                                                                          "AND H_LegalTwelveItem.StudentsCode = 9 " +
                                                                                          "AND H_LegalTwelveItem.StudentsDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "') AS Students10Flag," +
                                            "(SELECT StudentsFlag FROM H_LegalTwelveItem WHERE H_StaffMaster.StaffCode = H_LegalTwelveItem.StaffCode " +
                                                                                          "AND H_LegalTwelveItem.StudentsCode = 10 " +
                                                                                          "AND H_LegalTwelveItem.StudentsDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "') AS Students11Flag," +
                                            "(SELECT StudentsFlag FROM H_LegalTwelveItem WHERE H_StaffMaster.StaffCode = H_LegalTwelveItem.StaffCode " +
                                                                                          "AND H_LegalTwelveItem.StudentsCode = 11 " +
                                                                                          "AND H_LegalTwelveItem.StudentsDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "') AS Students12Flag " +
                                     "FROM H_StaffMaster " +
                                     "LEFT OUTER JOIN H_OccupationMaster ON H_StaffMaster.Occupation = H_OccupationMaster.Code " +
                                     "LEFT OUTER JOIN H_JobFormMaster ON H_StaffMaster.JobForm = H_JobFormMaster.Code " +
                                     "LEFT OUTER JOIN H_BelongsMaster ON H_StaffMaster.Belongs = H_BelongsMaster.Code " +
                                     "WHERE H_StaffMaster.LegalTwelveItemFlag = 'true' AND H_StaffMaster.RetirementFlag = 'false' " +
                                     "ORDER BY H_StaffMaster.NameKana ASC";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    LegalTwelveItemListVo legalTwelveItemVo = new();
                    legalTwelveItemVo.Belongs = _defaultValue.GetDefaultValue<int>(sqlDataReader["BelongsCode"]);
                    legalTwelveItemVo.BelongsName = _defaultValue.GetDefaultValue<string>(sqlDataReader["BelongsName"]);
                    legalTwelveItemVo.JobForm = _defaultValue.GetDefaultValue<int>(sqlDataReader["JobFormCode"]);
                    legalTwelveItemVo.JobFormName = _defaultValue.GetDefaultValue<string>(sqlDataReader["JobFormName"]);
                    legalTwelveItemVo.OccupationCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["OccupationCode"]);
                    legalTwelveItemVo.OccupationName = _defaultValue.GetDefaultValue<string>(sqlDataReader["OccupationName"]);
                    legalTwelveItemVo.UnionCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["UnionCode"]);
                    legalTwelveItemVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    legalTwelveItemVo.StaffName = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffName"]);
                    legalTwelveItemVo.EmploymentDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["EmploymentDate"]);
                    legalTwelveItemVo.Students01Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Students01Flag"]);
                    legalTwelveItemVo.Students02Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Students02Flag"]);
                    legalTwelveItemVo.Students03Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Students03Flag"]);
                    legalTwelveItemVo.Students04Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Students04Flag"]);
                    legalTwelveItemVo.Students05Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Students05Flag"]);
                    legalTwelveItemVo.Students06Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Students06Flag"]);
                    legalTwelveItemVo.Students07Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Students07Flag"]);
                    legalTwelveItemVo.Students08Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Students08Flag"]);
                    legalTwelveItemVo.Students09Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Students09Flag"]);
                    legalTwelveItemVo.Students10Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Students10Flag"]);
                    legalTwelveItemVo.Students11Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Students11Flag"]);
                    legalTwelveItemVo.Students12Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Students12Flag"]);
                    listLegalTwelveItemVo.Add(legalTwelveItemVo);
                }
                return listLegalTwelveItemVo;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fiscalYear"></param>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public List<LegalTwelveItemVo> SelectLegalTwelveItemVo(int fiscalYear, int staffCode) {
            List<LegalTwelveItemVo> listLegalTwelveItemVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT StudentsDate," +
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
                                     "FROM H_LegalTwelveItem " +
                                     "WHERE (StudentsDate BETWEEN '" + _dateUtility.GetFiscalYearStartDate(fiscalYear) + "' AND '" + _dateUtility.GetFiscalYearEndDate(fiscalYear) + "') " +
                                     "AND StaffCode = " + staffCode;
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    LegalTwelveItemVo legalTwelveItemVo = new();
                    legalTwelveItemVo.StudentsDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StudentsDate"]);
                    legalTwelveItemVo.StudentsCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StudentsCode"]);
                    legalTwelveItemVo.StudentsFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StudentsFlag"]);
                    legalTwelveItemVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    legalTwelveItemVo.StaffSign = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["StaffSign"]);
                    legalTwelveItemVo.SignNumber = _defaultValue.GetDefaultValue<int>(sqlDataReader["SignNumber"]);
                    legalTwelveItemVo.Memo = _defaultValue.GetDefaultValue<string>(sqlDataReader["Memo"]);
                    legalTwelveItemVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    legalTwelveItemVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    legalTwelveItemVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    legalTwelveItemVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    legalTwelveItemVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    legalTwelveItemVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    legalTwelveItemVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listLegalTwelveItemVo.Add(legalTwelveItemVo);
                }
                return listLegalTwelveItemVo;
            }
        }

        /// <summary>
        /// InsertOneLegalTwelveItem
        /// </summary>
        /// <param name="legalTwelveItemVo"></param>
        /// <returns></returns>
        public int InsertOneLegalTwelveItem(LegalTwelveItemVo legalTwelveItemVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_LegalTwelveItem(StudentsDate," +
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
                                     "VALUES ('" + legalTwelveItemVo.StudentsDate + "'," +
                                              "" + legalTwelveItemVo.StudentsCode + "," +
                                             "'" + legalTwelveItemVo.StudentsFlag + "'," +
                                              "" + legalTwelveItemVo.StaffCode + "," +
                                             "@Picture," +
                                              "" + legalTwelveItemVo.SignNumber + "," +
                                             "'" + legalTwelveItemVo.Memo + "'," +
                                             "'" + legalTwelveItemVo.InsertPcName + "'," +
                                             "'" + legalTwelveItemVo.InsertYmdHms + "'," +
                                             "'" + legalTwelveItemVo.UpdatePcName + "'," +
                                             "'" + legalTwelveItemVo.UpdateYmdHms + "'," +
                                             "'" + legalTwelveItemVo.DeletePcName + "'," +
                                             "'" + legalTwelveItemVo.DeleteYmdHms + "'," +
                                             "'" + legalTwelveItemVo.DeleteFlag + "'" +
                                             ");";
            if (legalTwelveItemVo.StaffSign is not null)
                sqlCommand.Parameters.Add("@Picture", SqlDbType.Image, legalTwelveItemVo.StaffSign.Length).Value = legalTwelveItemVo.StaffSign;
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// UpdateOneLegalTwelveItem
        /// </summary>
        /// <param name="oldHLegalTwelveItemVo"></param>
        /// <param name="newLegalTwelveItemVo"></param>
        /// <returns></returns>
        public int UpdateOneLegalTwelveItem(LegalTwelveItemVo oldLegalTwelveItemVo, LegalTwelveItemVo newLegalTwelveItemVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_LegalTwelveItem " +
                                     "SET StudentsDate = '" + _defaultValue.GetDefaultValue<DateTime>(newLegalTwelveItemVo.StudentsDate) + "'," +
                                         "StudentsCode = " + _defaultValue.GetDefaultValue<int>(newLegalTwelveItemVo.StudentsCode) + "," +
                                         "StudentsFlag = '" + _defaultValue.GetDefaultValue<bool>(newLegalTwelveItemVo.StudentsFlag) + "'," +
                                         "StaffCode = " + _defaultValue.GetDefaultValue<int>(newLegalTwelveItemVo.StaffCode) + "," +
                                         "StaffSign = @Picture," +
                                         "SignNumber = " + _defaultValue.GetDefaultValue<int>(newLegalTwelveItemVo.SignNumber) + "," +
                                         "Memo = '" + _defaultValue.GetDefaultValue<string>(newLegalTwelveItemVo.Memo) + "'," +
                                         "InsertPcName = '" + _defaultValue.GetDefaultValue<string>(newLegalTwelveItemVo.InsertPcName) + "'," +
                                         "InsertYmdHms = '" + _defaultValue.GetDefaultValue<DateTime>(newLegalTwelveItemVo.InsertYmdHms) + "'," +
                                         "UpdatePcName = '" + _defaultValue.GetDefaultValue<string>(newLegalTwelveItemVo.UpdatePcName) + "'," +
                                         "UpdateYmdHms = '" + _defaultValue.GetDefaultValue<DateTime>(newLegalTwelveItemVo.UpdateYmdHms) + "'," +
                                         "DeletePcName = '" + _defaultValue.GetDefaultValue<string>(newLegalTwelveItemVo.DeletePcName) + "'," +
                                         "DeleteYmdHms = '" + _defaultValue.GetDefaultValue<DateTime>(newLegalTwelveItemVo.DeleteYmdHms) + "'," +
                                         "DeleteFlag = '" + _defaultValue.GetDefaultValue<bool>(newLegalTwelveItemVo.DeleteFlag) + "' " +
                                     "WHERE (StudentsDate BETWEEN '" + oldLegalTwelveItemVo.StudentsDate + "' AND '" + oldLegalTwelveItemVo.StudentsDate + "') " +
                                     "AND StudentsCode = " + oldLegalTwelveItemVo.StudentsCode + " " +
                                     "AND StaffCode = " + oldLegalTwelveItemVo.StaffCode;
            if (newLegalTwelveItemVo.StaffSign is not null)
                sqlCommand.Parameters.Add("@Picture", SqlDbType.Image, newLegalTwelveItemVo.StaffSign.Length).Value = newLegalTwelveItemVo.StaffSign;
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// DeleteOneLegalTwelveItem
        /// </summary>
        /// <param name="oldHLegalTwelveItemVo"></param>
        /// <returns></returns>
        public int DeleteOneLegalTwelveItemVo(LegalTwelveItemVo oldLegalTwelveItemVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM H_LegalTwelveItem " +
                                     "WHERE (StudentsDate BETWEEN '" + oldLegalTwelveItemVo.StudentsDate + "' AND '" + oldLegalTwelveItemVo.StudentsDate + "') " +
                                     "AND StudentsCode = " + oldLegalTwelveItemVo.StudentsCode + " " +
                                     "AND StaffCode = " + oldLegalTwelveItemVo.StaffCode;
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }



    }

    /// <summary>
    /// LegalTwelveItemListVo
    /// </summary>
    public class LegalTwelveItemListVo {
        private readonly DateTime _defaultDatetime = new(1900, 01, 01);
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
