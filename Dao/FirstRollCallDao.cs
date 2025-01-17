/*
 * 2024-02-20
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class FirstRollCallDao {
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
        public FirstRollCallDao(ConnectionVo connectionVo) {
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
        public bool ExistenceFirstRollCallVo(DateTime dateTime) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(OperationDate) " +
                                     "FROM H_FirstRollCall " +
                                     "WHERE OperationDate = '" + dateTime.ToString("yyyy-MM-dd") + "'";
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
        /// <returns>存在する:H_FirstRollCallVo 存在しない:NULL</returns>
        public FirstRollCallVo? SelectOneFirstRollCallVo(DateTime dateTime) {
            FirstRollCallVo firstRollCallVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT OperationDate," +
                                            "RollCallName1," +
                                            "RollCallName2," +
                                            "RollCallName3," +
                                            "RollCallName4," +
                                            "RollCallName5," +
                                            "Weather," +
                                            "Instruction1," +
                                            "Instruction2," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_FirstRollCall " +
                                     "WHERE OperationDate = '" + dateTime.ToString("yyyy-MM-dd") + "'";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    firstRollCallVo.OperationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["OperationDate"]);
                    firstRollCallVo.RollCallName1 = _defaultValue.GetDefaultValue<string>(sqlDataReader["RollCallName1"]);
                    firstRollCallVo.RollCallName2 = _defaultValue.GetDefaultValue<string>(sqlDataReader["RollCallName2"]);
                    firstRollCallVo.RollCallName3 = _defaultValue.GetDefaultValue<string>(sqlDataReader["RollCallName3"]);
                    firstRollCallVo.RollCallName4 = _defaultValue.GetDefaultValue<string>(sqlDataReader["RollCallName4"]);
                    firstRollCallVo.RollCallName5 = _defaultValue.GetDefaultValue<string>(sqlDataReader["RollCallName5"]);
                    firstRollCallVo.Weather = _defaultValue.GetDefaultValue<string>(sqlDataReader["Weather"]);
                    firstRollCallVo.Instruction1 = _defaultValue.GetDefaultValue<string>(sqlDataReader["Instruction1"]);
                    firstRollCallVo.Instruction2 = _defaultValue.GetDefaultValue<string>(sqlDataReader["Instruction2"]);
                    firstRollCallVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    firstRollCallVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    firstRollCallVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    firstRollCallVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    firstRollCallVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    firstRollCallVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    firstRollCallVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                }
            }
            return firstRollCallVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRollCallVo"></param>
        public void InsertOneFirstRollCallVo(FirstRollCallVo firstRollCallVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_FirstRollCall(OperationDate," +
                                                                 "RollCallName1," +
                                                                 "RollCallName2," +
                                                                 "RollCallName3," +
                                                                 "RollCallName4," +
                                                                 "RollCallName5," +
                                                                 "Weather," +
                                                                 "Instruction1," +
                                                                 "Instruction2," +
                                                                 "InsertPcName," +
                                                                 "InsertYmdHms," +
                                                                 "UpdatePcName," +
                                                                 "UpdateYmdHms," +
                                                                 "DeletePcName," +
                                                                 "DeleteYmdHms," +
                                                                 "DeleteFlag) " +
                                     "VALUES ('" + _defaultValue.GetDefaultValue<DateTime>(firstRollCallVo.OperationDate) + "'," +
                                             "'" + _defaultValue.GetDefaultValue<string>(firstRollCallVo.RollCallName1) + "'," +
                                             "'" + _defaultValue.GetDefaultValue<string>(firstRollCallVo.RollCallName2) + "'," +
                                             "'" + _defaultValue.GetDefaultValue<string>(firstRollCallVo.RollCallName3) + "'," +
                                             "'" + _defaultValue.GetDefaultValue<string>(firstRollCallVo.RollCallName4) + "'," +
                                             "'" + _defaultValue.GetDefaultValue<string>(firstRollCallVo.RollCallName5) + "'," +
                                             "'" + _defaultValue.GetDefaultValue<string>(firstRollCallVo.Weather) + "'," +
                                             "'" + _defaultValue.GetDefaultValue<string>(firstRollCallVo.Instruction1) + "'," +
                                             "'" + _defaultValue.GetDefaultValue<string>(firstRollCallVo.Instruction2) + "'," +
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
        /// 
        /// </summary>
        /// <param name="firstRollCallVo"></param>
        public void UpdateOneFirstRollCallVo(FirstRollCallVo firstRollCallVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_FirstRollCall " +
                                     "SET OperationDate = '" + _defaultValue.GetDefaultValue<DateTime>(firstRollCallVo.OperationDate) + "'," +
                                         "RollCallName1 = '" + _defaultValue.GetDefaultValue<string>(firstRollCallVo.RollCallName1) + "'," +
                                         "RollCallName2 = '" + _defaultValue.GetDefaultValue<string>(firstRollCallVo.RollCallName2) + "'," +
                                         "RollCallName3 = '" + _defaultValue.GetDefaultValue<string>(firstRollCallVo.RollCallName3) + "'," +
                                         "RollCallName4 = '" + _defaultValue.GetDefaultValue<string>(firstRollCallVo.RollCallName4) + "'," +
                                         "RollCallName5 = '" + _defaultValue.GetDefaultValue<string>(firstRollCallVo.RollCallName5) + "'," +
                                         "Weather = '" + _defaultValue.GetDefaultValue<string>(firstRollCallVo.Weather) + "'," +
                                         "Instruction1 = '" + _defaultValue.GetDefaultValue<string>(firstRollCallVo.Instruction1) + "'," +
                                         "Instruction2 = '" + _defaultValue.GetDefaultValue<string>(firstRollCallVo.Instruction2) + "'," +
                                         "UpdatePcName = '" + Environment.MachineName + "'," +
                                         "UpdateYmdHms = '" + DateTime.Now + "' " +
                                     "WHERE OperationDate = '" + firstRollCallVo.OperationDate.ToString("yyyy-MM-dd") + "'";
            try {
                sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }
    }
}
