/*
 * 2024-02-19
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {

    public class LastRollCallDao {
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        private readonly DateUtility _dateUtility = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public LastRollCallDao(ConnectionVo connectionVo) {
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
        public bool ExistenceLastRollCall(int setCode, DateTime operationDate, DateTime lastRollCallYmdHms) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(LastRollCallYmdHms) " +
                                     "FROM H_LastRollCall " +
                                     "WHERE SetCode = " + setCode + " AND OperationDate = '" + operationDate.ToString("yyyy-MM-dd") + "' AND LastRollCallYmdHms = '" + lastRollCallYmdHms.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            try {
                return (int)sqlCommand.ExecuteScalar() != 0 ? true : false;
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public LastRollCallVo? SelectOneLastRollCall(int setCode, DateTime operationDate, DateTime lastRollCallYmdHms) {
            LastRollCallVo? hLastRollCallVo = null;
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT H_LastRollCall.SetCode," +
                                            "H_LastRollCall.OperationDate," +
                                            "H_VehicleDispatchDetail.StaffRollCallYmdHms1," +
                                            "H_LastRollCall.LastPlantCount," +
                                            "H_LastRollCall.LastPlantName," +
                                            "H_LastRollCall.LastPlantYmdHms," +
                                            "H_LastRollCall.LastRollCallYmdHms," +
                                            "H_LastRollCall.FirstOdoMeter," +
                                            "H_LastRollCall.LastOdoMeter," +
                                            "H_LastRollCall.OilAmount " +
                                     "FROM H_LastRollCall " +
                                     "LEFT OUTER JOIN H_VehicleDispatchDetail ON H_LastRollCall.OperationDate = H_VehicleDispatchDetail.OperationDate " +
                                                                            "AND H_LastRollCall.SetCode = H_VehicleDispatchDetail.SetCode " +
                                     "WHERE H_LastRollCall.SetCode = " + setCode + " AND H_LastRollCall.OperationDate = '" + operationDate.ToString("yyyy-MM-dd") + "' AND H_LastRollCall.LastRollCallYmdHms = '" + lastRollCallYmdHms.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            using (var sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    hLastRollCallVo = new();
                    hLastRollCallVo.SetCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["SetCode"]);
                    hLastRollCallVo.OperationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["OperationDate"]);
                    hLastRollCallVo.FirstRollCallYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StaffRollCallYmdHms1"]);
                    hLastRollCallVo.LastPlantCount = _defaultValue.GetDefaultValue<int>(sqlDataReader["LastPlantCount"]);
                    hLastRollCallVo.LastPlantName = _defaultValue.GetDefaultValue<string>(sqlDataReader["LastPlantName"]);
                    hLastRollCallVo.LastPlantYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["LastPlantYmdHms"]);
                    hLastRollCallVo.LastRollCallYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["LastRollCallYmdHms"]);
                    hLastRollCallVo.FirstOdoMeter = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["FirstOdoMeter"]);
                    hLastRollCallVo.LastOdoMeter = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["LastOdoMeter"]);
                    hLastRollCallVo.OilAmount = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["OilAmount"]);
                }
            }
            return hLastRollCallVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lastRollCallVo"></param>
        /// <returns></returns>
        public int InsertOneLastRollCall(LastRollCallVo lastRollCallVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_LastRollCall(SetCode," +
                                                                "OperationDate," +
                                                                "FirstRollCallYmdHms," +
                                                                "LastPlantCount," +
                                                                "LastPlantName," +
                                                                "LastPlantYmdHms," +
                                                                "LastRollCallYmdHms," +
                                                                "FirstOdoMeter," +
                                                                "LastOdoMeter," +
                                                                "OilAmount," +
                                                                "InsertPcName," +
                                                                "InsertYmdHms," +
                                                                "UpdatePcName," +
                                                                "UpdateYmdHms," +
                                                                "DeletePcName," +
                                                                "DeleteYmdHms," +
                                                                "DeleteFlag) " +
                                     "VALUES (" + _defaultValue.GetDefaultValue<int>(lastRollCallVo.SetCode) + "," +
                                            "'" + _defaultValue.GetDefaultValue<DateTime>(lastRollCallVo.OperationDate) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<DateTime>(lastRollCallVo.FirstRollCallYmdHms) + "'," +
                                             "" + _defaultValue.GetDefaultValue<int>(lastRollCallVo.LastPlantCount) + "," +
                                            "'" + _defaultValue.GetDefaultValue<string>(lastRollCallVo.LastPlantName) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<DateTime>(lastRollCallVo.LastPlantYmdHms) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<DateTime>(lastRollCallVo.LastRollCallYmdHms) + "'," +
                                             "" + _defaultValue.GetDefaultValue<decimal>(lastRollCallVo.FirstOdoMeter) + "," +
                                             "" + _defaultValue.GetDefaultValue<decimal>(lastRollCallVo.LastOdoMeter) + "," +
                                             "" + _defaultValue.GetDefaultValue<decimal>(lastRollCallVo.OilAmount) + "," +
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
        /// 
        /// </summary>
        /// <param name="lastRollCallVo"></param>
        public int UpdateOneLastRollCall(int setCode, DateTime operationDate, DateTime lastRollCallYmdHms, LastRollCallVo lastRollCallVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_LastRollCall " +
                                     "SET SetCode = " + _defaultValue.GetDefaultValue<int>(lastRollCallVo.SetCode) + "," +
                                         "OperationDate = '" + _defaultValue.GetDefaultValue<DateTime>(lastRollCallVo.OperationDate) + "'," +
                                         "FirstRollCallYmdHms = '" + _defaultValue.GetDefaultValue<DateTime>(lastRollCallVo.FirstRollCallYmdHms) + "'," +
                                         "LastPlantCount = " + _defaultValue.GetDefaultValue<int>(lastRollCallVo.LastPlantCount) + "," +
                                         "LastPlantName = '" + _defaultValue.GetDefaultValue<string>(lastRollCallVo.LastPlantName) + "'," +
                                         "LastPlantYmdHms = '" + _defaultValue.GetDefaultValue<DateTime>(lastRollCallVo.LastPlantYmdHms) + "'," +
                                         "LastRollCallYmdHms = '" + _defaultValue.GetDefaultValue<DateTime>(lastRollCallVo.LastRollCallYmdHms) + "'," +
                                         "FirstOdoMeter = " + _defaultValue.GetDefaultValue<decimal>(lastRollCallVo.FirstOdoMeter) + "," +
                                         "LastOdoMeter = " + _defaultValue.GetDefaultValue<decimal>(lastRollCallVo.LastOdoMeter) + "," +
                                         "OilAmount = " + _defaultValue.GetDefaultValue<decimal>(lastRollCallVo.OilAmount) + "," +
                                         "UpdatePcName = '" + Environment.MachineName + "'," +
                                         "UpdateYmdHms = '" + DateTime.Now + "' " +
                                     "WHERE SetCode = " + setCode + " AND OperationDate = '" + operationDate.ToString("yyyy-MM-dd") + "' AND LastRollCallYmdHms = '" + lastRollCallYmdHms.ToString("yyyy-MM-dd HH:mm:ss") + "'";
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
        public int DeleteOneLastRollCall(int setCode, DateTime operationDate, DateTime lastRollCallYmdHms) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM H_LastRollCall " +
                                     "WHERE SetCode = " + setCode + " AND OperationDate = '" + operationDate.ToString("yyyy-MM-dd") + "' AND LastRollCallYmdHms = '" + lastRollCallYmdHms.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }
    }
}
