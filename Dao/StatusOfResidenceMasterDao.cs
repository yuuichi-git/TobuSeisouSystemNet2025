/*
 * 2024-04-10
 */
using System.Data;
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class StatusOfResidenceMasterDao {
        private readonly DefaultValue _defaultValue = new();
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public StatusOfResidenceMasterDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// ExistenceHStatusOfResidenceMaster
        /// true:存在する false:存在しない
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public bool ExistenceHStatusOfResidenceMaster(int staffCode) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT TOP 1 StaffCode FROM H_StatusOfResidenceMaster WHERE StaffCode = " + staffCode + "";
            return sqlCommand.ExecuteScalar() is not null ? true : false;
        }

        /// <summary>
        /// SelectOneStatusOfResidenceMasterP
        /// Picture有
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public StatusOfResidenceMasterVo SelectOneStatusOfResidenceMasterP(int staffCode) {
            StatusOfResidenceMasterVo statusOfResidenceMasterVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "StaffNameKana," +
                                            "StaffName," +
                                            "BirthDate," +
                                            "Gender," +
                                            "Nationality," +
                                            "Address," +
                                            "StatusOfResidence," +
                                            "WorkLimit," +
                                            "PeriodDate," +
                                            "DeadlineDate," +
                                            "PictureHead," +
                                            "PictureTail," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_StatusOfResidenceMaster " +
                                     "WHERE StaffCode = " + staffCode;
            using (var sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    statusOfResidenceMasterVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    statusOfResidenceMasterVo.StaffNameKana = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffNameKana"]);
                    statusOfResidenceMasterVo.StaffName = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffName"]);
                    statusOfResidenceMasterVo.BirthDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["BirthDate"]);
                    statusOfResidenceMasterVo.Gender = _defaultValue.GetDefaultValue<string>(sqlDataReader["Gender"]);
                    statusOfResidenceMasterVo.Nationality = _defaultValue.GetDefaultValue<string>(sqlDataReader["Nationality"]);
                    statusOfResidenceMasterVo.Address = _defaultValue.GetDefaultValue<string>(sqlDataReader["Address"]);
                    statusOfResidenceMasterVo.StatusOfResidence = _defaultValue.GetDefaultValue<string>(sqlDataReader["StatusOfResidence"]);
                    statusOfResidenceMasterVo.WorkLimit = _defaultValue.GetDefaultValue<string>(sqlDataReader["WorkLimit"]);
                    statusOfResidenceMasterVo.PeriodDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["PeriodDate"]);
                    statusOfResidenceMasterVo.DeadlineDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeadlineDate"]);
                    statusOfResidenceMasterVo.PictureHead = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["PictureHead"]);
                    statusOfResidenceMasterVo.PictureTail = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["PictureTail"]);
                    statusOfResidenceMasterVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    statusOfResidenceMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    statusOfResidenceMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    statusOfResidenceMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    statusOfResidenceMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    statusOfResidenceMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    statusOfResidenceMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                }
            }
            return statusOfResidenceMasterVo;
        }

        /// <summary>
        /// SelectAllHStatusOfResidenceMaster
        /// Picture無
        /// </summary>
        /// <returns></returns>
        public List<StatusOfResidenceMasterVo> SelectAllStatusOfResidenceMaster() {
            List<StatusOfResidenceMasterVo> listStatusOfResidenceMasterVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "StaffNameKana," +
                                            "StaffName," +
                                            "BirthDate," +
                                            "Gender," +
                                            "Nationality," +
                                            "Address," +
                                            "StatusOfResidence," +
                                            "WorkLimit," +
                                            "PeriodDate," +
                                            "DeadlineDate," +
                                            //"PictureHead," +
                                            //"PictureTail," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_StatusOfResidenceMaster";
            using (var sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    StatusOfResidenceMasterVo statusOfResidenceMasterVo = new();
                    statusOfResidenceMasterVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    statusOfResidenceMasterVo.StaffNameKana = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffNameKana"]);
                    statusOfResidenceMasterVo.StaffName = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffName"]);
                    statusOfResidenceMasterVo.BirthDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["BirthDate"]);
                    statusOfResidenceMasterVo.Gender = _defaultValue.GetDefaultValue<string>(sqlDataReader["Gender"]);
                    statusOfResidenceMasterVo.Nationality = _defaultValue.GetDefaultValue<string>(sqlDataReader["Nationality"]);
                    statusOfResidenceMasterVo.Address = _defaultValue.GetDefaultValue<string>(sqlDataReader["Address"]);
                    statusOfResidenceMasterVo.StatusOfResidence = _defaultValue.GetDefaultValue<string>(sqlDataReader["StatusOfResidence"]);
                    statusOfResidenceMasterVo.WorkLimit = _defaultValue.GetDefaultValue<string>(sqlDataReader["WorkLimit"]);
                    statusOfResidenceMasterVo.PeriodDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["PeriodDate"]);
                    statusOfResidenceMasterVo.DeadlineDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeadlineDate"]);
                    //hStatusOfResidenceMasterVo.PictureHead = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["PictureHead"]);
                    //hStatusOfResidenceMasterVo.PictureTail = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["PictureTail"]);
                    statusOfResidenceMasterVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    statusOfResidenceMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    statusOfResidenceMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    statusOfResidenceMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    statusOfResidenceMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    statusOfResidenceMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    statusOfResidenceMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listStatusOfResidenceMasterVo.Add(statusOfResidenceMasterVo);
                }
            }
            return listStatusOfResidenceMasterVo;
        }

        /// <summary>
        /// SelectAllHStatusOfResidenceMasterP
        /// Picture有
        /// </summary>
        /// <returns></returns>
        public List<StatusOfResidenceMasterVo> SelectAllStatusOfResidenceMasterP() {
            List<StatusOfResidenceMasterVo> listStatusOfResidenceMasterVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "StaffNameKana," +
                                            "StaffName," +
                                            "BirthDate," +
                                            "Gender," +
                                            "Nationality," +
                                            "Address," +
                                            "StatusOfResidence," +
                                            "WorkLimit," +
                                            "PeriodDate," +
                                            "DeadlineDate," +
                                            "PictureHead," +
                                            "PictureTail," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_StatusOfResidenceMaster";
            using (var sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    StatusOfResidenceMasterVo statusOfResidenceMasterVo = new();
                    statusOfResidenceMasterVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    statusOfResidenceMasterVo.StaffNameKana = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffNameKana"]);
                    statusOfResidenceMasterVo.StaffName = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffName"]);
                    statusOfResidenceMasterVo.BirthDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["BirthDate"]);
                    statusOfResidenceMasterVo.Gender = _defaultValue.GetDefaultValue<string>(sqlDataReader["Gender"]);
                    statusOfResidenceMasterVo.Nationality = _defaultValue.GetDefaultValue<string>(sqlDataReader["Nationality"]);
                    statusOfResidenceMasterVo.Address = _defaultValue.GetDefaultValue<string>(sqlDataReader["Address"]);
                    statusOfResidenceMasterVo.StatusOfResidence = _defaultValue.GetDefaultValue<string>(sqlDataReader["StatusOfResidence"]);
                    statusOfResidenceMasterVo.WorkLimit = _defaultValue.GetDefaultValue<string>(sqlDataReader["WorkLimit"]);
                    statusOfResidenceMasterVo.PeriodDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["PeriodDate"]);
                    statusOfResidenceMasterVo.DeadlineDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeadlineDate"]);
                    statusOfResidenceMasterVo.PictureHead = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["PictureHead"]);
                    statusOfResidenceMasterVo.PictureTail = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["PictureTail"]);
                    statusOfResidenceMasterVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    statusOfResidenceMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    statusOfResidenceMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    statusOfResidenceMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    statusOfResidenceMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    statusOfResidenceMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    statusOfResidenceMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listStatusOfResidenceMasterVo.Add(statusOfResidenceMasterVo);
                }
            }
            return listStatusOfResidenceMasterVo;
        }

        /// <summary>
        /// InsertOneStatusOfHStatusOfResidenceMaster
        /// </summary>
        /// <param name="statusOfResidenceMasterVo"></param>
        /// <returns></returns>
        public int InsertOneStatusOfResidenceMaster(StatusOfResidenceMasterVo statusOfResidenceMasterVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_StatusOfResidenceMaster(StaffCode," +
                                                                           "StaffNameKana," +
                                                                           "StaffName," +
                                                                           "BirthDate," +
                                                                           "Gender," +
                                                                           "Nationality," +
                                                                           "Address," +
                                                                           "StatusOfResidence," +
                                                                           "WorkLimit," +
                                                                           "PeriodDate," +
                                                                           "DeadlineDate," +
                                                                           "PictureHead," +
                                                                           "PictureTail," +
                                                                           "InsertPcName," +
                                                                           "InsertYmdHms," +
                                                                           "UpdatePcName," +
                                                                           "UpdateYmdHms," +
                                                                           "DeletePcName," +
                                                                           "DeleteYmdHms," +
                                                                           "DeleteFlag) " +
                                     "VALUES (" + statusOfResidenceMasterVo.StaffCode + "," +
                                            "'" + statusOfResidenceMasterVo.StaffNameKana + "'," +
                                            "'" + statusOfResidenceMasterVo.StaffName + "'," +
                                            "'" + statusOfResidenceMasterVo.BirthDate + "'," +
                                            "'" + statusOfResidenceMasterVo.Gender + "'," +
                                            "'" + statusOfResidenceMasterVo.Nationality + "'," +
                                            "'" + statusOfResidenceMasterVo.Address + "'," +
                                            "'" + statusOfResidenceMasterVo.StatusOfResidence + "'," +
                                            "'" + statusOfResidenceMasterVo.WorkLimit + "'," +
                                            "'" + statusOfResidenceMasterVo.PeriodDate + "'," +
                                            "'" + statusOfResidenceMasterVo.DeadlineDate + "'," +
                                            "@memberPictureHead," +
                                            "@memberPictureTail," +
                                            "'" + Environment.MachineName + "'," +
                                            "'" + DateTime.Now + "'," +
                                            "'" + string.Empty + "'," +
                                            "'" + _defaultDateTime + "'," +
                                            "'" + string.Empty + "'," +
                                            "'" + _defaultDateTime + "'," +
                                            "'false'" +
                                            ");";
            try {
                sqlCommand.Parameters.Add("@memberPictureHead", SqlDbType.Image, statusOfResidenceMasterVo.PictureHead.Length).Value = statusOfResidenceMasterVo.PictureHead;
                sqlCommand.Parameters.Add("@memberPictureTail", SqlDbType.Image, statusOfResidenceMasterVo.PictureTail.Length).Value = statusOfResidenceMasterVo.PictureTail;
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// UpdateOneHStatusOfResidenceMaster
        /// </summary>
        /// <param name="statusOfResidenceMasterVo"></param>
        /// <returns></returns>
        public int UpdateOneStatusOfResidenceMaster(StatusOfResidenceMasterVo statusOfResidenceMasterVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_StatusOfResidenceMaster " +
                                     "SET StaffCode = " + statusOfResidenceMasterVo.StaffCode + "," +
                                         "StaffNameKana = '" + statusOfResidenceMasterVo.StaffNameKana + "'," +
                                         "StaffName = '" + statusOfResidenceMasterVo.StaffName + "'," +
                                         "BirthDate = '" + statusOfResidenceMasterVo.BirthDate + "'," +
                                         "Gender = '" + statusOfResidenceMasterVo.Gender + "'," +
                                         "Nationality = '" + statusOfResidenceMasterVo.Nationality + "'," +
                                         "Address = '" + statusOfResidenceMasterVo.Address + "'," +
                                         "StatusOfResidence = '" + statusOfResidenceMasterVo.StatusOfResidence + "'," +
                                         "WorkLimit = '" + statusOfResidenceMasterVo.WorkLimit + "'," +
                                         "PeriodDate = '" + statusOfResidenceMasterVo.PeriodDate + "'," +
                                         "DeadlineDate = '" + statusOfResidenceMasterVo.DeadlineDate + "'," +
                                         "PictureHead = @memberPictureHead," +
                                         "PictureTail = @memberPictureTail," +
                                         "UpdatePcName = '" + Environment.MachineName + "'," +
                                         "UpdateYmdHms = '" + DateTime.Now + "' " +
                                     "WHERE StaffCode = " + statusOfResidenceMasterVo.StaffCode;
            try {
                sqlCommand.Parameters.Add("@memberPictureHead", SqlDbType.Image, statusOfResidenceMasterVo.PictureHead.Length).Value = statusOfResidenceMasterVo.PictureHead;
                sqlCommand.Parameters.Add("@memberPictureTail", SqlDbType.Image, statusOfResidenceMasterVo.PictureTail.Length).Value = statusOfResidenceMasterVo.PictureTail;
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// DeleteOneHStatusOfResidenceMaster
        /// </summary>
        /// <param name="staffCode"></param>
        public void DeleteOneStatusOfResidenceMaster(int staffCode) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_StatusOfResidenceMaster " +
                                     "SET DeletePcName = '" + Environment.MachineName + "'," +
                                         "DeleteYmdHms = '" + DateTime.Now + "'," +
                                         "DeleteFlag = 'true' " +
                                     "WHERE StaffCode = " + staffCode;
            try {
                sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }


    }
}
