/*
 * 2024-05-22
 */
using System.Data;
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class CertificationFileDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public CertificationFileDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// ExistenceHCertificationFile
        /// </summary>
        /// <param name="staffCode"></param>
        /// <param name="certificationCode"></param>
        /// <returns>true:該当レコードあり false:該当レコードなし</returns>
        public bool ExistenceHCertificationFile(int staffCode, int certificationCode) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(StaffCode) " +
                                     "FROM H_CertificationFile " +
                                     "WHERE StaffCode = " + staffCode + " AND CertificationCode = " + certificationCode + "";
            try {
                return (int)sqlCommand.ExecuteScalar() != 0 ? true : false;
            } catch {
                throw;
            }
        }

        /// <summary>
        /// SelectAllCertificationFile
        /// Picture無し
        /// </summary>
        /// <returns></returns>
        public List<CertificationFileVo> SelectAllCertificationFile() {
            List<CertificationFileVo> listCertificationFileVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "CertificationCode," +
                                            "MarkCode," +
                                            "Picture1Flag," +
                                            "Picture2Flag," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_CertificationFile";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    CertificationFileVo certificationFileVo = new();
                    certificationFileVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    certificationFileVo.CertificationCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["CertificationCode"]);
                    certificationFileVo.MarkCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["MarkCode"]);
                    certificationFileVo.Picture1Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Picture1Flag"]);
                    certificationFileVo.Picture2Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Picture2Flag"]);
                    certificationFileVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    certificationFileVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    certificationFileVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    certificationFileVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    certificationFileVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    certificationFileVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    certificationFileVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listCertificationFileVo.Add(certificationFileVo);
                }
            }
            return listCertificationFileVo;
        }

        /// <summary>
        /// SelectAllCertificationFileP
        /// Picture有り
        /// </summary>
        /// <returns></returns>
        public List<CertificationFileVo> SelectAllCertificationFileP() {
            List<CertificationFileVo> listCertificationFileVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "CertificationCode," +
                                            "MarkCode," +
                                            "Picture1Flag," +
                                            "Picture1," +
                                            "Picture2Flag," +
                                            "Picture2," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_CertificationFile";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    CertificationFileVo certificationFileVo = new();
                    certificationFileVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    certificationFileVo.CertificationCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["CertificationCode"]);
                    certificationFileVo.MarkCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["MarkCode"]);
                    certificationFileVo.Picture1Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Picture1Flag"]);
                    certificationFileVo.Picture1 = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture1"]);
                    certificationFileVo.Picture2Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Picture2Flag"]);
                    certificationFileVo.Picture2 = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture2"]);
                    certificationFileVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    certificationFileVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    certificationFileVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    certificationFileVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    certificationFileVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    certificationFileVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    certificationFileVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listCertificationFileVo.Add(certificationFileVo);
                }
            }
            return listCertificationFileVo;
        }

        /// <summary>
        /// SelectAllHCertificationFileP
        /// Picture有り
        /// </summary>
        /// <returns></returns>
        public CertificationFileVo SelectOneCertificationFile(int staffCode, int certificationCode) {
            CertificationFileVo certificationFileVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "CertificationCode," +
                                            "MarkCode," +
                                            "Picture1Flag," +
                                            "Picture1," +
                                            "Picture2Flag," +
                                            "Picture2," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_CertificationFile " +
                                     "WHERE StaffCode = " + staffCode + " AND  CertificationCode = " + certificationCode + "";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    certificationFileVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    certificationFileVo.CertificationCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["CertificationCode"]);
                    certificationFileVo.MarkCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["MarkCode"]);
                    certificationFileVo.Picture1Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Picture1Flag"]);
                    certificationFileVo.Picture1 = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture1"]);
                    certificationFileVo.Picture2Flag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Picture2Flag"]);
                    certificationFileVo.Picture2 = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture2"]);
                    certificationFileVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    certificationFileVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    certificationFileVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    certificationFileVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    certificationFileVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    certificationFileVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    certificationFileVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                }
            }
            return certificationFileVo;
        }

        /// <summary>
        /// InsertOneHCertificationFile
        /// </summary>
        /// <param name="certificationFileVo"></param>
        /// <param name="staffCode"></param>
        /// <param name="markCode"></param>
        public void InsertOneCertificationFile(CertificationFileVo certificationFileVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_CertificationFile(StaffCode," +
                                                                     "CertificationCode," +
                                                                     "MarkCode," +
                                                                     "Picture1Flag," +
                                                                     "Picture1," +
                                                                     "Picture2Flag," +
                                                                     "Picture2," +
                                                                     "InsertPcName," +
                                                                     "InsertYmdHms," +
                                                                     "UpdatePcName," +
                                                                     "UpdateYmdHms," +
                                                                     "DeletePcName," +
                                                                     "DeleteYmdHms," +
                                                                     "DeleteFlag) " +
                                     "VALUES (" + certificationFileVo.StaffCode + "," +
                                                  certificationFileVo.CertificationCode + "," +
                                                  certificationFileVo.MarkCode + "," +
                                            "'" + certificationFileVo.Picture1Flag + "'," +
                                                 "@member_picture1," +
                                            "'" + certificationFileVo.Picture2Flag + "'," +
                                                 "@member_picture2," +
                                            "'" + Environment.MachineName + "'," +
                                            "'" + DateTime.Now + "'," +
                                            "'" + string.Empty + "'," +
                                            "'" + _defaultDateTime + "'," +
                                            "'" + string.Empty + "'," +
                                            "'" + _defaultDateTime + "'," +
                                            "'False'" +
                                            ");";
            try {
                if (certificationFileVo.Picture1 is not null)
                    sqlCommand.Parameters.Add("@member_picture1", SqlDbType.Image, certificationFileVo.Picture1.Length).Value = certificationFileVo.Picture1;
                if (certificationFileVo.Picture2 is not null)
                    sqlCommand.Parameters.Add("@member_picture2", SqlDbType.Image, certificationFileVo.Picture2.Length).Value = certificationFileVo.Picture2;
                sqlCommand.ExecuteReader();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// UpdateOneLicenseLedger
        /// </summary>
        /// <param name="certificationFileVo"></param>
        /// <returns></returns>
        public int UpdateOneLicenseLedger(CertificationFileVo certificationFileVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_CertificationFile " +
                                     "SET StaffCode = " + certificationFileVo.StaffCode + "," +
                                         "CertificationCode = " + certificationFileVo.CertificationCode + "," +
                                         "MarkCode = " + certificationFileVo.MarkCode + "," +
                                         "Picture1Flag = '" + certificationFileVo.Picture1Flag + "'," +
                                         "Picture1 = @member_picture1," +
                                         "Picture2Flag = '" + certificationFileVo.Picture2Flag + "'," +
                                         "Picture2 = @member_picture2," +
                                         "UpdatePcName = '" + Environment.MachineName + "'," +
                                         "UpdateYmdHms = '" + DateTime.Now + "' " +
                                     "WHERE StaffCode = " + certificationFileVo.StaffCode + " AND CertificationCode = " + certificationFileVo.CertificationCode + "";
            try {
                sqlCommand.Parameters.Add("@member_picture1", SqlDbType.Image, certificationFileVo.Picture1.Length).Value = certificationFileVo.Picture1;
                sqlCommand.Parameters.Add("@member_picture2", SqlDbType.Image, certificationFileVo.Picture2.Length).Value = certificationFileVo.Picture2;
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }
    }
}
