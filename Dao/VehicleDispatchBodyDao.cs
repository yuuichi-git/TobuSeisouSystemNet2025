/*
 * 2024-02-24
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class VehicleDispatchBodyDao {
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
        public VehicleDispatchBodyDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// ExistenceHVehicleDispatchBodyVo
        /// true:該当レコードあり false:該当レコードなし
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public bool ExistenceHVehicleDispatchBodyVo(int setCode, string dayOfWeek, int financialYear) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(SetCode) " +
                                     "FROM H_VehicleDispatchBody " +
                                     "WHERE SetCode = " + setCode + " AND DayOfWeek = '" + dayOfWeek + "' AND  FinancialYear = " + financialYear + "";
            try {
                return (int)sqlCommand.ExecuteScalar() != 0 ? true : false;
            } catch {
                throw;
            }
        }

        /// <summary>
        /// SelectOneHVehicleDispatchBody
        /// </summary>
        /// <param name="setCode"></param>
        /// <param name="operationDate"></param>
        /// <returns></returns>
        public VehicleDispatchBodyVo SelectOneVehicleDispatchBody(int setCode, DateTime operationDate, int financialYear) {
            VehicleDispatchBodyVo vehicleDispatchBodyVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT SetCode," +
                                            "DayOfWeek," +
                                            "CarCode," +
                                            "StaffCode1," +
                                            "StaffCode2," +
                                            "StaffCode3," +
                                            "StaffCode4," +
                                            "FinancialYear," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_VehicleDispatchBody " +
                                     "WHERE SetCode = " + setCode + " AND DayOfWeek = '" + operationDate.ToString("ddd") + "' AND FinancialYear = " + financialYear + "";
            using (var sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    vehicleDispatchBodyVo.SetCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["SetCode"]);
                    vehicleDispatchBodyVo.DayOfWeek = _defaultValue.GetDefaultValue<string>(sqlDataReader["DayOfWeek"]);
                    vehicleDispatchBodyVo.CarCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["CarCode"]);
                    vehicleDispatchBodyVo.StaffCode1 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode1"]);
                    vehicleDispatchBodyVo.StaffCode2 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode2"]);
                    vehicleDispatchBodyVo.StaffCode3 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode3"]);
                    vehicleDispatchBodyVo.StaffCode4 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode4"]);
                    vehicleDispatchBodyVo.FinancialYear = _defaultValue.GetDefaultValue<int>(sqlDataReader["FinancialYear"]);
                    vehicleDispatchBodyVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    vehicleDispatchBodyVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    vehicleDispatchBodyVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    vehicleDispatchBodyVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    vehicleDispatchBodyVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    vehicleDispatchBodyVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    vehicleDispatchBodyVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                }
            }
            return vehicleDispatchBodyVo;

        }

        /// <summary>
        /// InsertOneHVehicleDispatchBodyVo
        /// </summary>
        /// <param name="vehicleDispatchBodyVo"></param>
        public void InsertOneHVehicleDispatchBodyVo(VehicleDispatchBodyVo vehicleDispatchBodyVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_VehicleDispatchBody(SetCode," +
                                                                       "DayOfWeek," +
                                                                       "CarCode," +
                                                                       "StaffCode1," +
                                                                       "StaffCode2," +
                                                                       "StaffCode3," +
                                                                       "StaffCode4," +
                                                                       "FinancialYear," +
                                                                       "InsertPcName," +
                                                                       "InsertYmdHms," +
                                                                       "UpdatePcName," +
                                                                       "UpdateYmdHms," +
                                                                       "DeletePcName," +
                                                                       "DeleteYmdHms," +
                                                                       "DeleteFlag) " +
                                     "VALUES (" + _defaultValue.GetDefaultValue<int>(vehicleDispatchBodyVo.SetCode) + "," +
                                            "'" + _defaultValue.GetDefaultValue<string>(vehicleDispatchBodyVo.DayOfWeek) + "'," +
                                             "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchBodyVo.CarCode) + "," +
                                             "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchBodyVo.StaffCode1) + "," +
                                             "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchBodyVo.StaffCode2) + "," +
                                             "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchBodyVo.StaffCode3) + "," +
                                             "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchBodyVo.StaffCode4) + "," +
                                             "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchBodyVo.FinancialYear) + "," +
                                            "'" + Environment.MachineName + "'," +
                                            "'" + DateTime.Now + "'," +
                                          "''," +
                                            "'" + _defaultDateTime + "'," +
                                          "''," +
                                            "'" + _defaultDateTime + "'," +
                                            "'False'" +
                                            ");";
            try {
                sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// UpdateOneHVehicleDispatchBodyVo
        /// </summary>
        /// <param name="vehicleDispatchBodyVo"></param>
        public void UpdateOneHVehicleDispatchBodyVo(VehicleDispatchBodyVo vehicleDispatchBodyVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_VehicleDispatchBody " +
                                     "SET SetCode = " + _defaultValue.GetDefaultValue<int>(vehicleDispatchBodyVo.SetCode) + "," +
                                         "DayOfWeek = '" + _defaultValue.GetDefaultValue<string>(vehicleDispatchBodyVo.DayOfWeek) + "'," +
                                         "CarCode = " + _defaultValue.GetDefaultValue<int>(vehicleDispatchBodyVo.CarCode) + "," +
                                         "StaffCode1 = " + _defaultValue.GetDefaultValue<int>(vehicleDispatchBodyVo.StaffCode1) + "," +
                                         "StaffCode2 = " + _defaultValue.GetDefaultValue<int>(vehicleDispatchBodyVo.StaffCode2) + "," +
                                         "StaffCode3 = " + _defaultValue.GetDefaultValue<int>(vehicleDispatchBodyVo.StaffCode3) + "," +
                                         "StaffCode4 = " + _defaultValue.GetDefaultValue<int>(vehicleDispatchBodyVo.StaffCode4) + "," +
                                         "FinancialYear = " + _defaultValue.GetDefaultValue<int>(vehicleDispatchBodyVo.FinancialYear) + "," +
                                         "UpdatePcName = '" + Environment.MachineName + "'," +
                                         "UpdateYmdHms = '" + DateTime.Now + "' " +
                                     "WHERE SetCode = " + vehicleDispatchBodyVo.SetCode + " AND DayOfWeek = '" + vehicleDispatchBodyVo.DayOfWeek + "' AND FinancialYear = " + vehicleDispatchBodyVo.FinancialYear + "";
            try {
                sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }
    }
}
