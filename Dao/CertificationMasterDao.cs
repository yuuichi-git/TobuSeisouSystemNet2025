/*
 * 2024-05-22
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class CertificationMasterDao {
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public CertificationMasterDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;

        }

        /// <summary>
        /// SelectAllCertificationMaster
        /// </summary>
        /// <returns></returns>
        public List<CertificationMasterVo> SelectAllCertificationMaster() {
            List<CertificationMasterVo> listCertificationMasterVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT CertificationCode," +
                                            "CertificationName," +
                                            "CertificationDisplayName," +
                                            "DisplayFlag," +
                                            "CertificationType," +
                                            "NumberOfAppointments," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_CertificationMaster " +
                                     "WHERE DisplayFlag = 'True' " +
                                       "AND DeleteFlag = 'False' " +
                                       "ORDER BY CertificationCode ASC";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    CertificationMasterVo certificationMasterVo = new();
                    certificationMasterVo.CertificationCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["CertificationCode"]);
                    certificationMasterVo.CertificationName = _defaultValue.GetDefaultValue<string>(sqlDataReader["CertificationName"]);
                    certificationMasterVo.CertificationDisplayName = _defaultValue.GetDefaultValue<string>(sqlDataReader["CertificationDisplayName"]);
                    certificationMasterVo.DisplayFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DisplayFlag"]);
                    certificationMasterVo.CertificationType = _defaultValue.GetDefaultValue<int>(sqlDataReader["CertificationType"]);
                    certificationMasterVo.NumberOfAppointments = _defaultValue.GetDefaultValue<int>(sqlDataReader["NumberOfAppointments"]);
                    certificationMasterVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    certificationMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    certificationMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    certificationMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    certificationMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    certificationMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    certificationMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listCertificationMasterVo.Add(certificationMasterVo);
                }
            }
            return listCertificationMasterVo;
        }
    }
}
