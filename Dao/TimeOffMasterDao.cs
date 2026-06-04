/*
 * 2026-05-30
 */
using System.Data;
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class TimeOffMasterDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="connectionVo"></param>
        public TimeOffMasterDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>true:該当レコードあり false:該当レコードなし</returns>
        public bool ExistenceTimeOffMaster(DateTime date, int staffCode) {
            using SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT CASE WHEN EXISTS (SELECT 1 FROM H_TimeOffMaster " +
                                     "                         WHERE Date = @Date " +
                                     "                           AND StaffCode = @StaffCode) " +
                                     "THEN 1 ELSE 0 END";
            sqlCommand.Parameters.Add("@Date", SqlDbType.Date).Value = date.Date;
            sqlCommand.Parameters.Add("@StaffCode", SqlDbType.Int).Value = staffCode;

            try {
                return (int)sqlCommand.ExecuteScalar() != 0 ? true : false;
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public List<TimeOffMasterVo> SelectAllTimeOffMaster(DateTime date) {
            List<TimeOffMasterVo> listTimeOffMasterVo = new();

            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT Date," +
                                     "       Code," +
                                     "       StaffCode," +
                                     "       Remarks," +
                                     "       InsertPcName," +
                                     "       InsertYmdHms," +
                                     "       UpdatePcName," +
                                     "       UpdateYmdHms," +
                                     "       DeletePcName," +
                                     "       DeleteYmdHms," +
                                     "       DeleteFlag " +
                                     "FROM   H_TimeOffMaster " +
                                     "WHERE  Date      = @Date " +
                                     "  AND  DeleteFlag = 0";

            sqlCommand.Parameters.Add("@Date", SqlDbType.Date).Value = date;

            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    TimeOffMasterVo timeOffMasterVo = new();
                    timeOffMasterVo.Date = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["Date"]);
                    timeOffMasterVo.Code = _defaultValue.GetDefaultValue<int>(sqlDataReader["Code"]);
                    timeOffMasterVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    timeOffMasterVo.Remarks = _defaultValue.GetDefaultValue<string>(sqlDataReader["Remarks"]);
                    timeOffMasterVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    timeOffMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    timeOffMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    timeOffMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    timeOffMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    timeOffMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    timeOffMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listTimeOffMasterVo.Add(timeOffMasterVo);
                }
            }
            return listTimeOffMasterVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="code"></param>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public TimeOffMasterVo? SelectOneTimeOffMaster(DateTime date, int code, int staffCode) {
            TimeOffMasterVo? timeOffMasterVo = null;

            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT Date," +
                                     "       Code," +
                                     "       StaffCode," +
                                     "       Remarks," +
                                     "       InsertPcName," +
                                     "       InsertYmdHms," +
                                     "       UpdatePcName," +
                                     "       UpdateYmdHms," +
                                     "       DeletePcName," +
                                     "       DeleteYmdHms," +
                                     "       DeleteFlag " +
                                     "FROM   H_TimeOffMaster " +
                                     "WHERE  Date      = @Date " +
                                     "  AND  Code       = @Code " +
                                     "  AND  StaffCode  = @StaffCode " +
                                     "  AND  DeleteFlag = 0";

            sqlCommand.Parameters.Add("@Date", SqlDbType.Date).Value = date;
            sqlCommand.Parameters.Add("@Code", SqlDbType.Int).Value = code;
            sqlCommand.Parameters.Add("@StaffCode", SqlDbType.Int).Value = staffCode;

            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    timeOffMasterVo = new();
                    timeOffMasterVo.Date = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["Date"]);
                    timeOffMasterVo.Code = _defaultValue.GetDefaultValue<int>(sqlDataReader["Code"]);
                    timeOffMasterVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    timeOffMasterVo.Remarks = _defaultValue.GetDefaultValue<string>(sqlDataReader["Remarks"]);
                    timeOffMasterVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    timeOffMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    timeOffMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    timeOffMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    timeOffMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    timeOffMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    timeOffMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                }
            }
            return timeOffMasterVo;
        }

        /// <summary>
        /// InsertOneTimeOffMaster
        /// </summary>
        /// <param name="timeOffMasterVo"></param>
        /// <returns></returns>
        public int InsertOneTimeOffMaster(DateTime date, int code, int staffCode) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_TimeOffMaster(Date," +
                                     "                            Code," +
                                     "                            StaffCode," +
                                     "                            Remarks," +
                                     "                            InsertPcName," +
                                     "                            InsertYmdHms," +
                                     "                            UpdatePcName," +
                                     "                            UpdateYmdHms," +
                                     "                            DeletePcName," +
                                     "                            DeleteYmdHms," +
                                     "                            DeleteFlag) " +
                                     "VALUES ('" + _defaultValue.GetDefaultValue<DateTime>(date).Date + "'," +
                                              "" + _defaultValue.GetDefaultValue<int>(code) + "," +
                                              "" + _defaultValue.GetDefaultValue<int>(staffCode) + "," +
                                             "'" + string.Empty + "'," +
                                             "'" + Environment.MachineName + "'," +
                                             "'" + DateTime.Now + "'," +
                                             "'" + string.Empty + "'," +
                                             "'" + _defaultDateTime + "'," +
                                             "'" + string.Empty + "'," +
                                             "'" + _defaultDateTime + "'," +
                                             "'False'" +
                                             ");";
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 対象レコードを更新する
        /// </summary>
        /// <param name="date"></param>
        /// <param name="code"></param>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public int UpdateOneTimeOffMaster(DateTime date, int code, int staffCode) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_TimeOffMaster " +
                                     "SET Code = " + _defaultValue.GetDefaultValue<int>(code) + "," +
                                     "    UpdatePcName = '" + Environment.MachineName + "'," +
                                     "    UpdateYmdHms = '" + DateTime.Now + "' " +
                                     "WHERE Date = '" + date.ToString("yyyy-MM-dd") + "' AND StaffCode = " + staffCode;
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 対象レコードを更新する
        /// </summary>
        /// <param name="date"></param>
        /// <param name="code"></param>
        /// <param name="staffCode"></param>
        /// <param name="remarks"></param>
        /// <returns></returns>
        public int UpdateOneRemarks(DateTime date, int code, int staffCode, string remarks) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_TimeOffMaster " +
                                     "SET Remarks      = @Remarks, " +
                                     "    UpdatePcName = @UpdatePcName, " +
                                     "    UpdateYmdHms = @UpdateYmdHms " +
                                     "WHERE Date       = @Date " +
                                     "  AND Code       = @Code " +
                                     "  AND StaffCode  = @StaffCode";

            sqlCommand.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = remarks;
            sqlCommand.Parameters.Add("@UpdatePcName", SqlDbType.NVarChar).Value = Environment.MachineName;
            sqlCommand.Parameters.Add("@UpdateYmdHms", SqlDbType.DateTime).Value = DateTime.Now;

            sqlCommand.Parameters.Add("@Date", SqlDbType.Date).Value = date;
            sqlCommand.Parameters.Add("@Code", SqlDbType.Int).Value = code;
            sqlCommand.Parameters.Add("@StaffCode", SqlDbType.Int).Value = staffCode;

            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 対象レコードを削除する
        /// </summary>
        /// <param name="setCode"></param>
        /// <param name="operationDate"></param>
        /// <param name="lastRollCallYmdHms"></param>
        /// <returns></returns>
        public int DeleteOneTimeOffMaster(DateTime date, int staffCode) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM H_TimeOffMaster " +
                                     "       WHERE Date = @Date " +
                                     "         AND StaffCode = @StaffCode";
            sqlCommand.Parameters.Add("@Date", SqlDbType.Date).Value = date.Date;
            sqlCommand.Parameters.Add("@StaffCode", SqlDbType.Int).Value = staffCode;
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }
    }
}
