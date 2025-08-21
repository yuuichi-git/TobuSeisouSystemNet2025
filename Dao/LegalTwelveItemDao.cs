/*
 * 2024-04-27
 */
using System.Data;
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class LegalTwelveItemDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
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
}
