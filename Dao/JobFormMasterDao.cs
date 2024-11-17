/*
 * 2024-11-15
 */
using System.Data.SqlClient;

using Common;
using Vo;

namespace Dao {
    public class JobFormMasterDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;

        public JobFormMasterDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        public List<JobFormMasterVo> SelectAllJobFormMaster() {
            List<JobFormMasterVo> listJobFormMasterVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT Code," +
                                            "Name," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_BelongsMaster " +
                                     "ORDER BY Code ASC";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    JobFormMasterVo jobFormMasterVo = new();
                    jobFormMasterVo.Code = _defaultValue.GetDefaultValue<int>(sqlDataReader["Code"]);
                    jobFormMasterVo.Name = _defaultValue.GetDefaultValue<string>(sqlDataReader["Name"]);
                    jobFormMasterVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    jobFormMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    jobFormMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    jobFormMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    jobFormMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    jobFormMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    jobFormMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listJobFormMasterVo.Add(jobFormMasterVo);
                }
            }
            return listJobFormMasterVo;
        }
    }
}
