/*
 * 2024-11-12
 */
using System.Data;
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class ContractExpirationDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;

        public ContractExpirationDao(ConnectionVo connectionVo) {
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
        public bool ExistenceContractExpiration(int staffCode) {
            int count;
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(StaffCode) " +
                                     "FROM H_ContractExpiration " +
                                     "WHERE StaffCode = " + staffCode;
            try {
                count = (int)sqlCommand.ExecuteScalar();
            } catch {
                throw;
            }
            return count != 0 ? true : false;
        }

        /// <summary>
        /// true:該当レコードあり 
        /// false:該当レコードなし
        /// </summary>
        /// <param name="code"></param>
        /// <param name="staffCode"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public bool ExistenceContractExpiration(int code, int staffCode, DateTime startDate, DateTime endDate) {
            int count;
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(StaffCode) " +
                                     "FROM H_ContractExpiration " +
                                     "WHERE Code = " + code + " " +
                                       "AND StaffCode = " + staffCode + " " +
                                       "AND StartDate = '" + startDate + "' " +
                                       "AND EndDate = '" + endDate + "'";
            try {
                count = (int)sqlCommand.ExecuteScalar();
            } catch {
                throw;
            }
            return count != 0 ? true : false;
        }

        public List<ContractExpirationVo> SelectAllContractExpiration() {
            List<ContractExpirationVo> listContractExpirationVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT Code," +
                                            "StaffCode," +
                                            "StartDate," +
                                            "EndDate," +
                                            "Memo," +
                                            //"Picture," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_ContractExpiration " +
                                     "ORDER BY StartDate DESC, EndDate DESC";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    ContractExpirationVo contractExpirationVo = new();
                    contractExpirationVo.Code = _defaultValue.GetDefaultValue<int>(sqlDataReader["Code"]);
                    contractExpirationVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    contractExpirationVo.StartDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StartDate"]);
                    contractExpirationVo.EndDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["EndDate"]);
                    contractExpirationVo.Memo = _defaultValue.GetDefaultValue<string>(sqlDataReader["Memo"]);
                    //contractExpirationVo.Picture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture"]);
                    contractExpirationVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    contractExpirationVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    contractExpirationVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    contractExpirationVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    contractExpirationVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    contractExpirationVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    contractExpirationVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listContractExpirationVo.Add(contractExpirationVo);
                }
            }
            return listContractExpirationVo;
        }

        public List<ContractExpirationVo> SelectAllContractExpirationP() {
            List<ContractExpirationVo> listContractExpirationVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT Code," +
                                            "StaffCode," +
                                            "StartDate," +
                                            "EndDate," +
                                            "Memo," +
                                            "Picture," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_ContractExpiration";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    ContractExpirationVo contractExpirationVo = new();
                    contractExpirationVo.Code = _defaultValue.GetDefaultValue<int>(sqlDataReader["Code"]);
                    contractExpirationVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    contractExpirationVo.StartDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StartDate"]);
                    contractExpirationVo.EndDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["EndDate"]);
                    contractExpirationVo.Memo = _defaultValue.GetDefaultValue<string>(sqlDataReader["Memo"]);
                    contractExpirationVo.Picture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture"]);
                    contractExpirationVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    contractExpirationVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    contractExpirationVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    contractExpirationVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    contractExpirationVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    contractExpirationVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    contractExpirationVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listContractExpirationVo.Add(contractExpirationVo);
                }
            }
            return listContractExpirationVo;
        }

        public List<ContractExpirationVo> SelectOneContractExpirationP(int staffCode) {
            List<ContractExpirationVo> listContractExpirationVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT Code," +
                                            "StaffCode," +
                                            "StartDate," +
                                            "EndDate," +
                                            "Memo," +
                                            "Picture," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_ContractExpiration " +
                                     "WHERE StaffCode = " + staffCode + "";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    ContractExpirationVo contractExpirationVo = new();
                    contractExpirationVo.Code = _defaultValue.GetDefaultValue<int>(sqlDataReader["Code"]);
                    contractExpirationVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    contractExpirationVo.StartDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StartDate"]);
                    contractExpirationVo.EndDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["EndDate"]);
                    contractExpirationVo.Memo = _defaultValue.GetDefaultValue<string>(sqlDataReader["Memo"]);
                    contractExpirationVo.Picture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture"]);
                    contractExpirationVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    contractExpirationVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    contractExpirationVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    contractExpirationVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    contractExpirationVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    contractExpirationVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    contractExpirationVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listContractExpirationVo.Add(contractExpirationVo);
                }
            }
            return listContractExpirationVo;
        }

        public int InsertOneContractExpiration(ContractExpirationVo contractExpirationVo) {
            var sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_ContractExpiration(Code," +
                                                                      "StaffCode," +
                                                                      "StartDate," +
                                                                      "EndDate," +
                                                                      "Memo," +
                                                                      "Picture," +
                                                                      "InsertPcName," +
                                                                      "InsertYmdHms," +
                                                                      "UpdatePcName," +
                                                                      "UpdateYmdHms," +
                                                                      "DeletePcName," +
                                                                      "DeleteYmdHms," +
                                                                      "DeleteFlag) " +
                                     "VALUES (" + contractExpirationVo.Code + "," +
                                             "" + contractExpirationVo.StaffCode + "," +
                                            "'" + contractExpirationVo.StartDate + "'," +
                                            "'" + contractExpirationVo.EndDate + "'," +
                                            "'" + contractExpirationVo.Memo + "'," +
                                            "@Picture," +
                                            "'" + Environment.MachineName + "'," +
                                            "'" + DateTime.Now + "'," +
                                            "'" + string.Empty + "'," +
                                            "'" + _defaultDateTime + "'," +
                                            "'" + string.Empty + "'," +
                                            "'" + _defaultDateTime + "'," +
                                             "'false'" +
                                             ");";
            try {
                sqlCommand.Parameters.Add("@Picture", SqlDbType.Image, contractExpirationVo.Picture.Length).Value = contractExpirationVo.Picture;
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        public int UpdateOneContractExpiration(ContractExpirationVo contractExpirationVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_ContractExpiration " +
                                     "SET Code = " + contractExpirationVo.Code + "," +
                                         "StaffCode = " + contractExpirationVo.StaffCode + "," +
                                         "StartDate = '" + contractExpirationVo.StartDate + "'," +
                                         "EndDate = '" + contractExpirationVo.EndDate + "'," +
                                         "Memo = '" + contractExpirationVo.Memo + "'," +
                                         "Picture = @Picture," +
                                         "UpdatePcName = '" + Environment.MachineName + "'," +
                                         "UpdateYmdHms = '" + DateTime.Now + "' " +
                                     "WHERE Code = " + contractExpirationVo.Code + " " +
                                     "AND StaffCode = " + contractExpirationVo.StaffCode + " " +
                                     "AND StartDate = '" + contractExpirationVo.StartDate + "' " +
                                     "AND EndDate = '" + contractExpirationVo.EndDate + "'";
            try {
                sqlCommand.Parameters.Add("@Picture", SqlDbType.Image, contractExpirationVo.Picture.Length).Value = contractExpirationVo.Picture;
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }
    }
}
