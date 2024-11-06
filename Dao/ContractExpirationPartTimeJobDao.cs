/*
 * 2024-11-06
 */
using System.Data;
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class ContractExpirationPartTimeJobDao {
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        public ContractExpirationPartTimeJobDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public List<ContractExpirationPartTimeJobVo> SelectOneContractExpirationPartTimeJob(int staffCode) {
            List<ContractExpirationPartTimeJobVo> listContractExpirationPartTimeJobVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "ContractExpirationStartDate," +
                                            "ContractExpirationEndDate," +
                                            "Memo," +
                                            "Picture," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_ContractExpirationPartTimeJob " +
                                     "WHERE StaffCode = '" + staffCode + "'";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    ContractExpirationPartTimeJobVo contractExpirationPartTimeJobVo = new();
                    contractExpirationPartTimeJobVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    contractExpirationPartTimeJobVo.ContractExpirationStartDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ContractExpirationStartDate"]);
                    contractExpirationPartTimeJobVo.ContractExpirationEndDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ContractExpirationEndDate"]);
                    contractExpirationPartTimeJobVo.Memo = _defaultValue.GetDefaultValue<string>(sqlDataReader["Memo"]);
                    contractExpirationPartTimeJobVo.Picture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture"]);
                    contractExpirationPartTimeJobVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    contractExpirationPartTimeJobVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    contractExpirationPartTimeJobVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    contractExpirationPartTimeJobVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    contractExpirationPartTimeJobVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    contractExpirationPartTimeJobVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    contractExpirationPartTimeJobVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listContractExpirationPartTimeJobVo.Add(contractExpirationPartTimeJobVo);
                }
            }
            return listContractExpirationPartTimeJobVo;
        }

        public int InsertOneContractExpirationPartTimeJob(ContractExpirationPartTimeJobVo contractExpirationPartTimeJobVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO ContractExpirationPartTimeJob(StaffCode," +
                                                                               "ContractExpirationStartDate," +
                                                                               "ContractExpirationEndDate," +
                                                                               "Memo," +
                                                                               "Picture," +
                                                                               "InsertPcName," +
                                                                               "InsertYmdHms," +
                                                                               "UpdatePcName," +
                                                                               "UpdateYmdHms," +
                                                                               "DeletePcName," +
                                                                               "DeleteYmdHms," +
                                                                               "DeleteFlag) " +
                                     "VALUES ('" + contractExpirationPartTimeJobVo.StaffCode + "'," +
                                              "" + contractExpirationPartTimeJobVo.ContractExpirationStartDate + "," +
                                             "'" + contractExpirationPartTimeJobVo.ContractExpirationEndDate + "'," +
                                              "" + contractExpirationPartTimeJobVo.Memo + "," +
                                             "@picture," +
                                             "'" + contractExpirationPartTimeJobVo.InsertPcName + "'," +
                                             "'" + contractExpirationPartTimeJobVo.InsertYmdHms + "'," +
                                             "'" + contractExpirationPartTimeJobVo.UpdatePcName + "'," +
                                             "'" + contractExpirationPartTimeJobVo.UpdateYmdHms + "'," +
                                             "'" + contractExpirationPartTimeJobVo.DeletePcName + "'," +
                                             "'" + contractExpirationPartTimeJobVo.DeleteYmdHms + "'," +
                                             "'" + contractExpirationPartTimeJobVo.DeleteFlag + "'" +
                                             ");";
            if (contractExpirationPartTimeJobVo.Picture is not null)
                sqlCommand.Parameters.Add("@picture", SqlDbType.Image, contractExpirationPartTimeJobVo.Picture.Length).Value = contractExpirationPartTimeJobVo.Picture;
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }
    }
}
