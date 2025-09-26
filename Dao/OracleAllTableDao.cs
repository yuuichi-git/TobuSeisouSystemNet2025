/*
 * 2025-08-14
 */
using Common;

using Oracle.ManagedDataAccess.Client;

using Vo;

namespace Dao {
    public class OracleAllTableDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public OracleAllTableDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> GetUserTables() {
            List<string> listTableName = new();
            OracleCommand oracleCommand = _connectionVo.OracleConnection.CreateCommand();
            oracleCommand.CommandText = "SELECT * " +
                                        "FROM USER_TABLES " +
                                        "ORDER BY TABLE_NAME";
            using (OracleDataReader oracleDataReader = oracleCommand.ExecuteReader()) {
                while (oracleDataReader.Read() == true) {
                    string _tableName = string.Empty;
                    _tableName = _defaultValue.GetDefaultValue<string>(oracleDataReader["TABLE_NAME"]);
                    listTableName.Add(_tableName);
                }
            }
            return listTableName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ownerName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<string> GetColumns(string ownerName, string tableName) {
            List<string> listTableName = new();
            OracleCommand oracleCommand = _connectionVo.OracleConnection.CreateCommand();
            oracleCommand.CommandText = "SELECT COLUMN_NAME " +
                                        "FROM ALL_TAB_COLUMNS " +
                                        "WHERE OWNER = '" + ownerName + "' AND TABLE_NAME = '" + tableName + "' " +
                                        "ORDER BY COLUMN_ID";
            using (OracleDataReader oracleDataReader = oracleCommand.ExecuteReader()) {
                while (oracleDataReader.Read() == true) {
                    string _tableName = string.Empty;
                    _tableName = _defaultValue.GetDefaultValue<string>(oracleDataReader["COLUMN_NAME"]);
                    listTableName.Add(_tableName);
                }
            }
            return listTableName;
        }
    }
}
