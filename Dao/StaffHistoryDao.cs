/*
 * 2024-02-03
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class StaffHistoryDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public StaffHistoryDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// SelectOneHStaffHistoryMaster
        /// </summary>
        /// <returns></returns>
        public List<StaffHistoryVo> SelectOneStaffHistoryMaster(int staffCode) {
            List<StaffHistoryVo> listStaffHistoryVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "HistoryDate," +
                                            "CompanyName," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_StaffHistoryMaster " +
                                     "WHERE StaffCode = " + staffCode + "";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    StaffHistoryVo staffHistoryVo = new();
                    staffHistoryVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    staffHistoryVo.HistoryDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["HistoryDate"]);
                    staffHistoryVo.CompanyName = _defaultValue.GetDefaultValue<string>(sqlDataReader["CompanyName"]);
                    staffHistoryVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    staffHistoryVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    staffHistoryVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    staffHistoryVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    staffHistoryVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    staffHistoryVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    staffHistoryVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listStaffHistoryVo.Add(staffHistoryVo);
                }
            }
            return listStaffHistoryVo;
        }

        /// <summary>
        /// InsertOneHStaffHistoryMaster
        /// </summary>
        /// <param name="staffHistoryVo"></param>
        public void InsertOneStaffHistoryMaster(StaffHistoryVo staffHistoryVo) {
            var sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_StaffHistoryMaster(StaffCode," +
                                                                      "HistoryDate," +
                                                                      "CompanyName," +
                                                                      "InsertPcName," +
                                                                      "InsertYmdHms," +
                                                                      "UpdatePcName," +
                                                                      "UpdateYmdHms," +
                                                                      "DeletePcName," +
                                                                      "DeleteYmdHms," +
                                                                      "DeleteFlag) " +
                                     "VALUES (" + _defaultValue.GetDefaultValue<int>(staffHistoryVo.StaffCode) + "," +
                                             "'" + _defaultValue.GetDefaultValue<DateTime>(staffHistoryVo.HistoryDate) + "'," +
                                             "'" + _defaultValue.GetDefaultValue<string>(staffHistoryVo.CompanyName) + "'," +
                                             "'" + Environment.MachineName + "'," +
                                             "'" + DateTime.Now + "'," +
                                             "'" + "" + "'," +
                                             "'" + _defaultDateTime + "'," +
                                             "'" + "" + "'," +
                                             "'" + _defaultDateTime + "'," +
                                             "'False'" +
                                             ");";
            try {
                sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }
    }
}
