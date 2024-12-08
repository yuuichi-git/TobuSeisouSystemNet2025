/*
 * 2024-11-12
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class EmploymentAgreementDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;

        public EmploymentAgreementDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// true:該当レコードあり 
        /// false:該当レコードなし
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public bool ExistenceEmploymentAgreement(int staffCode) {
            int count;
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(StaffCode) " +
                                     "FROM H_EmploymentAgreement " +
                                     "WHERE StaffCode = " + staffCode;
            try {
                count = (int)sqlCommand.ExecuteScalar();
            } catch {
                throw;
            }
            return count != 0 ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EmploymentAgreementVo> SelectAllEmploymentAgreement() {
            List<EmploymentAgreementVo> listEmploymentAgreementVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "BaseLocation," +
                                            "Occupation," +
                                            "ContractExpirationPeriod," +
                                            "ContractExpirationPeriodString," +
                                            "PayDetail," +
                                            "Pay," +
                                            "TravelCostDetail," +
                                            "TravelCost," +
                                            "JobDescription," +
                                            "WorkTime," +
                                            "BreakTime," +
                                            "CheckFlag," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_EmploymentAgreement";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    EmploymentAgreementVo employmentAgreementVo = new();
                    employmentAgreementVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    employmentAgreementVo.BaseLocation = _defaultValue.GetDefaultValue<string>(sqlDataReader["BaseLocation"]);
                    employmentAgreementVo.Occupation = _defaultValue.GetDefaultValue<int>(sqlDataReader["Occupation"]);
                    employmentAgreementVo.ContractExpirationPeriod = _defaultValue.GetDefaultValue<int>(sqlDataReader["ContractExpirationPeriod"]);
                    employmentAgreementVo.ContractExpirationPeriodString = _defaultValue.GetDefaultValue<string>(sqlDataReader["ContractExpirationPeriodString"]);
                    employmentAgreementVo.PayDetail = _defaultValue.GetDefaultValue<string>(sqlDataReader["PayDetail"]);
                    employmentAgreementVo.Pay = _defaultValue.GetDefaultValue<int>(sqlDataReader["Pay"]);
                    employmentAgreementVo.TravelCostDetail = _defaultValue.GetDefaultValue<string>(sqlDataReader["TravelCostDetail"]);
                    employmentAgreementVo.TravelCost = _defaultValue.GetDefaultValue<int>(sqlDataReader["TravelCost"]);
                    employmentAgreementVo.JobDescription = _defaultValue.GetDefaultValue<int>(sqlDataReader["JobDescription"]);
                    employmentAgreementVo.WorkTime = _defaultValue.GetDefaultValue<string>(sqlDataReader["WorkTime"]);
                    employmentAgreementVo.BreakTime = _defaultValue.GetDefaultValue<string>(sqlDataReader["BreakTime"]);
                    employmentAgreementVo.CheckFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["CheckFlag"]);
                    employmentAgreementVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    employmentAgreementVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    employmentAgreementVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    employmentAgreementVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    employmentAgreementVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    employmentAgreementVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    employmentAgreementVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listEmploymentAgreementVo.Add(employmentAgreementVo);
                }
            }
            return listEmploymentAgreementVo;
        }

        /// <summary>
        /// 給与区分
        /// </summary>
        /// <returns></returns>
        public List<string> SelectGroupPayDetail() {
            List<string> listGroupPayDetail = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT PayDetail " +
                                     "FROM H_EmploymentAgreement " +
                                     "GROUP BY PayDetail";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    string payDetail = _defaultValue.GetDefaultValue<string>(sqlDataReader["PayDetail"]);
                    listGroupPayDetail.Add(payDetail);
                }
            }
            return listGroupPayDetail;
        }

        /// <summary>
        /// InsertOneEmploymentAgreement
        /// </summary>
        /// <param name="employmentAgreementVo"></param>
        public int InsertOneEmploymentAgreement(EmploymentAgreementVo employmentAgreementVo) {
            var sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_EmploymentAgreement(StaffCode," +
                                                                       "BaseLocation," +
                                                                       "Occupation," +
                                                                       "ContractExpirationPeriod," +
                                                                       "ContractExpirationPeriodString," +
                                                                       "PayDetail," +
                                                                       "Pay," +
                                                                       "TravelCostDetail," +
                                                                       "TravelCost," +
                                                                       "JobDescription," +
                                                                       "WorkTime," +
                                                                       "BreakTime," +
                                                                       "CheckFlag," +
                                                                       "InsertPcName," +
                                                                       "InsertYmdHms," +
                                                                       "UpdatePcName," +
                                                                       "UpdateYmdHms," +
                                                                       "DeletePcName," +
                                                                       "DeleteYmdHms," +
                                                                       "DeleteFlag) " +
                                     "VALUES (" + employmentAgreementVo.StaffCode + "," +
                                            "'" + employmentAgreementVo.BaseLocation + "'," +
                                             "" + employmentAgreementVo.Occupation + "," +
                                             "" + employmentAgreementVo.ContractExpirationPeriod + "," +
                                            "'" + employmentAgreementVo.ContractExpirationPeriodString + "'," +
                                            "'" + employmentAgreementVo.PayDetail + "'," +
                                             "" + employmentAgreementVo.Pay + "," +
                                            "'" + employmentAgreementVo.TravelCostDetail + "'," +
                                             "" + employmentAgreementVo.TravelCost + "," +
                                             "" + employmentAgreementVo.JobDescription + "," +
                                            "'" + employmentAgreementVo.WorkTime + "'," +
                                            "'" + employmentAgreementVo.BreakTime + "'," +
                                            "'" + employmentAgreementVo.CheckFlag + "'," +
                                            "'" + Environment.MachineName + "'," +
                                            "'" + DateTime.Now + "'," +
                                            "'" + string.Empty + "'," +
                                            "'" + _defaultDateTime + "'," +
                                            "'" + string.Empty + "'," +
                                            "'" + _defaultDateTime + "'," +
                                             "'false'" +
                                             ");";
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employmentAgreementVo"></param>
        /// <returns></returns>
        public int UpdateOneEmploymentAgreement(EmploymentAgreementVo employmentAgreementVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_EmploymentAgreement " +
                                     "SET StaffCode = " + employmentAgreementVo.StaffCode + "," +
                                         "BaseLocation = '" + employmentAgreementVo.BaseLocation + "'," +
                                         "Occupation = " + employmentAgreementVo.Occupation + "," +
                                         "ContractExpirationPeriod = " + employmentAgreementVo.ContractExpirationPeriod + "," +
                                         "ContractExpirationPeriodString = '" + employmentAgreementVo.ContractExpirationPeriodString + "'," +
                                         "PayDetail = '" + employmentAgreementVo.PayDetail + "'," +
                                         "Pay = " + employmentAgreementVo.Pay + "," +
                                         "TravelCostDetail = '" + employmentAgreementVo.TravelCostDetail + "'," +
                                         "TravelCost = " + employmentAgreementVo.TravelCost + "," +
                                         "JobDescription = " + employmentAgreementVo.JobDescription + "," +
                                         "WorkTime = '" + employmentAgreementVo.WorkTime + "'," +
                                         "BreakTime = '" + employmentAgreementVo.BreakTime + "'," +
                                         "CheckFlag = '" + employmentAgreementVo.CheckFlag + "'," +
                                         "UpdatePcName = '" + Environment.MachineName + "'," +
                                         "UpdateYmdHms = '" + DateTime.Now + "' " +
                                     "WHERE StaffCode = " + employmentAgreementVo.StaffCode;
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

    }
}
