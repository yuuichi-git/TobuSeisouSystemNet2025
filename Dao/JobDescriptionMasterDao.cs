/*
 * 2024-11-15
 */
using System.Data.SqlClient;

using Common;
using Vo;

namespace Dao {
    public class JobDescriptionMasterDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;

        public JobDescriptionMasterDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        public List<JobDescriptionMasterVo> SelectAllJobDescriptionMaster() {
            List<JobDescriptionMasterVo> listJobDescriptionMasterVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT Code," +
                                            "Name," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_JobDescriptionMaster " +
                                     "ORDER BY Code ASC";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    JobDescriptionMasterVo jobDescriptionMasterVo = new();
                    jobDescriptionMasterVo.Code = _defaultValue.GetDefaultValue<int>(sqlDataReader["Code"]);
                    jobDescriptionMasterVo.Name = _defaultValue.GetDefaultValue<string>(sqlDataReader["Name"]);
                    jobDescriptionMasterVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    jobDescriptionMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    jobDescriptionMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    jobDescriptionMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    jobDescriptionMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    jobDescriptionMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    jobDescriptionMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listJobDescriptionMasterVo.Add(jobDescriptionMasterVo);
                }
            }
            return listJobDescriptionMasterVo;
        }
    }
}
