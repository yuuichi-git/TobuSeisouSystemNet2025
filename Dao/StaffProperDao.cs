/*
 * 2024-02-03
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class StaffProperDao {
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
        public StaffProperDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// GetSyoninProperDate
        /// 初任診断の受診日を取得する
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns>初任診断の受診日を返す。存在しない場合はstring.Emptyを返す</returns>
        public DateTime GetSyoninProperDate(int staffCode) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT TOP 1 ProperDate FROM H_StaffProperMaster WHERE StaffCode = " + staffCode + " AND ProperKind = '初任診断' ORDER BY ProperDate DESC";
            var data = sqlCommand.ExecuteScalar();
            if (data is not null) {
                return (DateTime)sqlCommand.ExecuteScalar();
            } else {
                return _defaultDateTime;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public string GetTekireiProperDate(int staffCode) {
            TimeSpan timeSpan;
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT TOP 1 ProperDate FROM H_StaffProperMaster WHERE StaffCode = " + staffCode + " AND ProperKind = '適齢診断' ORDER BY ProperDate DESC";
            var data = sqlCommand.ExecuteScalar();
            if (data is not null) {
                timeSpan = ((DateTime)sqlCommand.ExecuteScalar()).AddYears(3) - DateTime.Now.Date;
                return string.Concat(timeSpan.Days, "日後");
            } else {
                return string.Empty;
            }
        }

        /// <summary>
        /// SelectOneHStaffProperMaster
        /// </summary>
        /// <returns></returns>
        public List<StaffProperVo> SelectOneStaffProperMaster(int staffCode) {
            List<StaffProperVo> listStaffProperVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "ProperKind," +
                                            "ProperDate," +
                                            "ProperNote," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_StaffProperMaster " +
                                     "WHERE StaffCode = " + staffCode + "";
            using (var sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    StaffProperVo staffProperVo = new();
                    staffProperVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    staffProperVo.ProperKind = _defaultValue.GetDefaultValue<string>(sqlDataReader["ProperKind"]);
                    staffProperVo.ProperDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ProperDate"]);
                    staffProperVo.ProperNote = _defaultValue.GetDefaultValue<string>(sqlDataReader["ProperNote"]);
                    staffProperVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    staffProperVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    staffProperVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    staffProperVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    staffProperVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    staffProperVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listStaffProperVo.Add(staffProperVo);
                }
            }
            return listStaffProperVo;
        }

        /// <summary>
        /// InsertOneHStaffProperMaster
        /// </summary>
        /// <param name="staffProperVo"></param>
        public void InsertOneStaffProperMaster(StaffProperVo staffProperVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_StaffProperMaster(StaffCode," +
                                                                     "ProperKind," +
                                                                     "ProperDate," +
                                                                     "ProperNote," +
                                                                     "InsertPcName," +
                                                                     "InsertYmdHms," +
                                                                     "UpdatePcName," +
                                                                     "UpdateYmdHms," +
                                                                     "DeletePcName," +
                                                                     "DeleteYmdHms," +
                                                                     "DeleteFlag) " +
                                     "VALUES (" + _defaultValue.GetDefaultValue<int>(staffProperVo.StaffCode) + "," +
                                            "'" + _defaultValue.GetDefaultValue<string>(staffProperVo.ProperKind) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<DateTime>(staffProperVo.ProperDate) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<string>(staffProperVo.ProperNote) + "'," +
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
