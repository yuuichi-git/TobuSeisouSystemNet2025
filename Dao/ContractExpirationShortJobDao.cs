/*
 * 2024-11-06
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class ContractExpirationShortJobDao {
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        public ContractExpirationShortJobDao(ConnectionVo connectionVo) {
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
        public List<ContractExpirationShortJobVo> SelectOneContractExpirationShortJob(int staffCode) {
            List<ContractExpirationShortJobVo> listContractExpirationShortJobVo = new();
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
                                     "FROM H_ContractExpirationShortJob " +
                                     "WHERE StaffCode = '" + staffCode + "'";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    ContractExpirationShortJobVo contractExpirationShortJobVo = new();
                    contractExpirationShortJobVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    contractExpirationShortJobVo.ContractExpirationStartDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ContractExpirationStartDate"]);
                    contractExpirationShortJobVo.ContractExpirationEndDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ContractExpirationEndDate"]);
                    contractExpirationShortJobVo.Memo = _defaultValue.GetDefaultValue<string>(sqlDataReader["Memo"]);
                    contractExpirationShortJobVo.Picture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture"]);
                    contractExpirationShortJobVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    contractExpirationShortJobVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    contractExpirationShortJobVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    contractExpirationShortJobVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    contractExpirationShortJobVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    contractExpirationShortJobVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    contractExpirationShortJobVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listContractExpirationShortJobVo.Add(contractExpirationShortJobVo);
                }
            }
            return listContractExpirationShortJobVo;
        }
    }
}
