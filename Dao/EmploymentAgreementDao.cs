/*
 * 2024-11-05
 */
using System.Data;
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class EmploymentAgreementDao {
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Dao
         */
        private ContractExpirationPartTimeJobDao _contractExpirationPartTimeJobDao;
        private ContractExpirationLongJobDao _contractExpirationLongJobDao;
        private ContractExpirationShortJobDao _contractExpirationShortJobDao;
        private WrittenPledgeDao _writtenPledgeDao;
        private LossWrittenPledgeDao _lossWrittenPledgeDao;
        private ContractExpirationNoticeDao _contractExpirationNoticeDao;
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        public EmploymentAgreementDao(ConnectionVo connectionVo) {
            /*
             * Dao
             */
            _contractExpirationPartTimeJobDao = new(connectionVo);
            _contractExpirationLongJobDao = new(connectionVo);
            _contractExpirationShortJobDao = new(connectionVo);
            _writtenPledgeDao = new(connectionVo);
            _lossWrittenPledgeDao = new(connectionVo);
            _contractExpirationNoticeDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// レコードの存在チェック
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns>true:あり false:なし</returns>
        public bool CheckRecord(int staffCode) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(StaffCode) " +
                                     "FROM H_EmploymentAgreement " +
                                     "WHERE  StaffCode = " + staffCode + "";
            try {
                return (int)sqlCommand.ExecuteScalar() > 0 ? true : false;
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public EmploymentAgreementVo SelectOneEmploymentAgreement(int staffCode) {
            EmploymentAgreementVo employmentAgreement = null;
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "ContractExpirationPeriod," +
                                            "ExperienceFlag," +
                                            "ExperienceStartDate," +
                                            "ExperienceEndDate," +
                                            "ExperienceMemo," +
                                            "ExperiencePicture," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_EmploymentAgreement " +
                                     "WHERE StaffCode = '" + staffCode + "'";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    employmentAgreement = new();
                    employmentAgreement.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    employmentAgreement.ContractExpirationPeriod = _defaultValue.GetDefaultValue<int>(sqlDataReader["ContractExpirationPeriod"]);
                    employmentAgreement.ExperienceFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["ExperienceFlag"]);
                    employmentAgreement.ExperienceStartDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ExperienceStartDate"]);
                    employmentAgreement.ExperienceEndDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ExperienceEndDate"]);
                    employmentAgreement.ExperienceMemo = _defaultValue.GetDefaultValue<string>(sqlDataReader["ExperienceMemo"]);
                    employmentAgreement.ExperiencePicture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["ExperiencePicture"]);
                    employmentAgreement.ListContractExpirationPartTimeJobVo = _contractExpirationPartTimeJobDao.SelectOneContractExpirationPartTimeJob(staffCode);
                    employmentAgreement.ListContractExpirationLongJobVo = _contractExpirationLongJobDao.SelectOneContractExpirationLongJob(staffCode);
                    employmentAgreement.ListContractExpirationShortJobVo = _contractExpirationShortJobDao.SelectOneContractExpirationShortJob(staffCode);
                    employmentAgreement.ListWrittenPledgeVo = _writtenPledgeDao.SelectOneWrittenPledge(staffCode);
                    employmentAgreement.ListLossWrittenPledgeVo = _lossWrittenPledgeDao.SelectOneLossWrittenPledge(staffCode);
                    employmentAgreement.ListContractExpirationNoticeVo = _contractExpirationNoticeDao.SelectOneContractExpirationNotice(staffCode);
                    employmentAgreement.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    employmentAgreement.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    employmentAgreement.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    employmentAgreement.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    employmentAgreement.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    employmentAgreement.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    employmentAgreement.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                }
            }
            return employmentAgreement;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employmentAgreementVo"></param>
        /// <returns></returns>
        public int InsertOneEmploymentAgreement(EmploymentAgreementVo employmentAgreementVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_EmploymentAgreement(StaffCode," +
                                                                       "ContractExpirationPeriod," +
                                                                       "ExperienceFlag," +
                                                                       "ExperienceStartDate," +
                                                                       "ExperienceEndDate," +
                                                                       "ExperienceMemo," +
                                                                       "ExperiencePicture," +
                                                                       "InsertPcName," +
                                                                       "InsertYmdHms," +
                                                                       "UpdatePcName," +
                                                                       "UpdateYmdHms," +
                                                                       "DeletePcName," +
                                                                       "DeleteYmdHms," +
                                                                       "DeleteFlag) " +
                                     "VALUES ('" + employmentAgreementVo.StaffCode + "'," +
                                              "" + employmentAgreementVo.ContractExpirationPeriod + "," +
                                             "'" + employmentAgreementVo.ExperienceFlag + "'," +
                                             "'" + employmentAgreementVo.ExperienceStartDate + "'," +
                                             "'" + employmentAgreementVo.ExperienceEndDate + "'," +
                                             "'" + employmentAgreementVo.ExperienceMemo + "'," +
                                             "@picture," +
                                             "'" + employmentAgreementVo.InsertPcName + "'," +
                                             "'" + employmentAgreementVo.InsertYmdHms + "'," +
                                             "'" + employmentAgreementVo.UpdatePcName + "'," +
                                             "'" + _defaultDateTime + "'," +
                                             "'" + string.Empty + "'," +
                                             "'" + _defaultDateTime + "'," +
                                             "'" + string.Empty + "'" +
                                             ");";
            if (employmentAgreementVo.ExperiencePicture is not null)
                sqlCommand.Parameters.Add("@picture", SqlDbType.Image, employmentAgreementVo.ExperiencePicture.Length).Value = employmentAgreementVo.ExperiencePicture;
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employmentAgreementVo"></param>
        public void UpdateOneEmploymentAgreement(EmploymentAgreementVo employmentAgreementVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_EmploymentAgreement " +
                                     "SET StaffCode = " + employmentAgreementVo.StaffCode + "," +
                                         "ContractExpirationPeriod = " + employmentAgreementVo.ContractExpirationPeriod + "," +
                                         "UpdatePcName = '" + employmentAgreementVo.UpdatePcName + "'," +
                                         "UpdateYmdHms = '" + employmentAgreementVo.UpdateYmdHms + "' " +
                                     "WHERE StaffCode = " + employmentAgreementVo.StaffCode;
            try {
                sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }
    }
}
