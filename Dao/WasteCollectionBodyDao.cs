/*
 * 2026-01-26
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class WasteCollectionBodyDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        public WasteCollectionBodyDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// Idに等しいレコードの存在を確認する
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true:該当レコードあり false:該当レコードなし</returns>
        public bool ExistenceWasteCollectionBody(int id, int rowIndex) {
            using SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText =
                "SELECT COUNT(Id) " +
                "FROM H_WasteCollectionBody " +
                "WHERE Id = @Id AND NumberOfRow = @RowIndex";
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@RowIndex", rowIndex);
            object result = sqlCommand.ExecuteScalar();
            return Convert.ToInt32(result) > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<WasteCollectionBodyVo> SelectAllWasteCollectionBody(int id) {
            List<WasteCollectionBodyVo> listWasteCollectionBodyVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT H_WasteCollectionBody.Id," +
                                            "H_WasteCollectionBody.NumberOfRow," +
                                            "H_WasteCollectionBody.ItemName," +
                                            "H_WasteCollectionBody.ItemSize," +
                                            "H_WasteCollectionBody.NumberOfUnits," +
                                            "H_WasteCollectionBody.UnitPrice," +
                                            "H_WasteCollectionBody.Others," +
                                            "H_WasteCollectionBody.InsertPcName," +
                                            "H_WasteCollectionBody.InsertYmdHms," +
                                            "H_WasteCollectionBody.UpdatePcName," +
                                            "H_WasteCollectionBody.UpdateYmdHms," +
                                            "H_WasteCollectionBody.DeletePcName," +
                                            "H_WasteCollectionBody.DeleteYmdHms," +
                                            "H_WasteCollectionBody.DeleteFlag " +
                                     "FROM H_WasteCollectionBody " +
                                     "WHERE H_WasteCollectionBody.Id = " + id + "";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    WasteCollectionBodyVo wasteCollectionBodyVo = new();
                    wasteCollectionBodyVo.Id = _defaultValue.GetDefaultValue<int>(sqlDataReader["Id"]);
                    wasteCollectionBodyVo.NumberOfRow = _defaultValue.GetDefaultValue<int>(sqlDataReader["NumberOfRow"]);
                    wasteCollectionBodyVo.ItemName = _defaultValue.GetDefaultValue<string>(sqlDataReader["ItemName"]);
                    wasteCollectionBodyVo.ItemSize = _defaultValue.GetDefaultValue<string>(sqlDataReader["ItemSize"]);
                    wasteCollectionBodyVo.NumberOfUnits = _defaultValue.GetDefaultValue<int>(sqlDataReader["NumberOfUnits"]);
                    wasteCollectionBodyVo.UnitPrice = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["UnitPrice"]);
                    wasteCollectionBodyVo.Remarks = _defaultValue.GetDefaultValue<string>(sqlDataReader["Others"]);
                    wasteCollectionBodyVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    wasteCollectionBodyVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    wasteCollectionBodyVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    wasteCollectionBodyVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    wasteCollectionBodyVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    wasteCollectionBodyVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    wasteCollectionBodyVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listWasteCollectionBodyVo.Add(wasteCollectionBodyVo);
                }
            }
            return listWasteCollectionBodyVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wasteCollectionBodyVo"></param>
        public void InsertOneWasteCollectionBody(int id, int numberOfRow, WasteCollectionBodyVo wasteCollectionBodyVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_WasteCollectionBody(Id," +
                                                                       "NumberOfRow," +
                                                                       "ItemName," +
                                                                       "ItemSize," +
                                                                       "NumberOfUnits," +
                                                                       "UnitPrice," +
                                                                       "Others," +
                                                                       "InsertPcName," +
                                                                       "InsertYmdHms," +
                                                                       "UpdatePcName," +
                                                                       "UpdateYmdHms," +
                                                                       "DeletePcName," +
                                                                       "DeleteYmdHms," +
                                                                       "DeleteFlag) " +
                                     "VALUES (" + id + "," +
                                             "" + numberOfRow + "," +
                                            "'" + wasteCollectionBodyVo.ItemName + "'," +
                                            "'" + wasteCollectionBodyVo.ItemSize + "'," +
                                            "'" + wasteCollectionBodyVo.NumberOfUnits + "'," +
                                            "'" + wasteCollectionBodyVo.UnitPrice + "'," +
                                            "'" + wasteCollectionBodyVo.Remarks + "'," +
                                            "'" + Environment.MachineName + "'," +
                                            "'" + DateTime.Now + "'," +
                                            "'" + string.Empty + "'," +
                                            "'" + _defaultDateTime + "'," +
                                            "'" + string.Empty + "'," +
                                            "'" + _defaultDateTime + "'," +
                                             "'false'" +
                                             ");";

            sqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numberOfRow"></param>
        /// <param name="wasteCollectionBodyVo"></param>
        public void UpdateOneWasteCollectionBody(int id, int numberOfRow, WasteCollectionBodyVo wasteCollectionBodyVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_WasteCollectionBody " +
                                     "SET ItemName = '" + wasteCollectionBodyVo.ItemName + "'," +
                                         "ItemSize = '" + wasteCollectionBodyVo.ItemSize + "'," +
                                         "NumberOfUnits = " + wasteCollectionBodyVo.NumberOfUnits + "," +
                                         "UnitPrice = " + wasteCollectionBodyVo.UnitPrice + "," +
                                         "Others = '" + wasteCollectionBodyVo.Remarks + "'," +
                                         "UpdatePcName = '" + Environment.MachineName + "'," +
                                         "UpdateYmdHms = '" + DateTime.Now + "' " +
                                     "WHERE Id = " + id + " AND NumberOfRow = " + numberOfRow + "";

            sqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="numberOfRow"></param>
        public void DeleteOneWasteCollectionBody(int id, int numberOfRow) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_WasteCollectionBody " +
                                     "SET DeletePcName = '" + Environment.MachineName + "'," +
                                         "DeleteYmdHms = '" + DateTime.Now + "'," +
                                         "DeleteFlag = 'True' " +
                                     "WHERE Id = " + id + " AND NumberOfRow = " + numberOfRow + "";
            sqlCommand.ExecuteNonQuery();
        }
    }
}
