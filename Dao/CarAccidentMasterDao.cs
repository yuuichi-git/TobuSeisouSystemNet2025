/*
 * 2024-02-07
 */
using System.Data;
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class CarAccidentMasterDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        private readonly DateUtility _dateUtility = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public CarAccidentMasterDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// ExistenceHCarAccidentMaster
        /// true:該当レコードあり false:該当レコードなし
        /// </summary>
        /// <param name="staffCode"></param>
        /// <param name="occurrenceYmdHms"></param>
        /// <returns></returns>
        public bool ExistenceCarAccidentMaster(CarAccidentMasterVo carAccidentMasterVo) {
            int count;
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(StaffCode) " +
                                     "FROM H_CarAccidentMaster " +
                                     "WHERE StaffCode = " + carAccidentMasterVo.StaffCode + " AND OccurrenceYmdHms = '" + carAccidentMasterVo.OccurrenceYmdHms.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            try {
                count = (int)sqlCommand.ExecuteScalar();
            } catch {
                throw;
            }
            return count != 0 ? true : false;
        }

        /// <summary>
        /// 年度内の事故件数
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns></returns>c
        public string GetCarAccidentMasterCount(int staffCode) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(StaffCode) " +
                                     "FROM H_CarAccidentMaster " +
                                     "WHERE StaffCode = " + staffCode + "" +
                                       "AND OccurrenceYmdHms BETWEEN '" + _dateUtility.GetFiscalYearStartDate(DateTime.Now.Date) + "' AND '" + _dateUtility.GetFiscalYearEndDate(DateTime.Now.Date) + "' " +
                                       "AND TotallingFlag = 'true'";
            int count = (int)sqlCommand.ExecuteScalar();
            if (count > 0) {
                return string.Concat(sqlCommand.ExecuteScalar().ToString(), "件");
            } else {
                return string.Empty;
            }
        }

        /// <summary>
        /// SelectGroupHCarAccidentMaster
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public List<CarAccidentMasterVo> SelectGroupCarAccidentMaster(int staffCode) {
            List<CarAccidentMasterVo> listCarAccidentMasterVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "OccurrenceYmdHms," +
                                            "TotallingFlag," +
                                            "Weather," +
                                            "AccidentKind," +
                                            "CarStatic," +
                                            "OccurrenceCause," +
                                            "Negligence," +
                                            "PersonalInjury," +
                                            "PropertyAccident1," +
                                            "PropertyAccident2," +
                                            "OccurrenceAddress," +
                                            "WorkKind," +
                                            "DisplayName," +
                                            "LicenseNumber," +
                                            "CarRegistrationNumber," +
                                            "AccidentSummary," +
                                            "AccidentDetail," +
                                            "Guide," +
                                            "Picture1," +
                                            "Picture2," +
                                            "Picture3," +
                                            "Picture4," +
                                            "Picture5," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_CarAccidentMaster " +
                                     "WHERE StaffCode = " + staffCode + "";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    CarAccidentMasterVo carAccidentMasterVo = new();
                    carAccidentMasterVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    carAccidentMasterVo.OccurrenceYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["OccurrenceYmdHms"]);
                    carAccidentMasterVo.TotallingFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["TotallingFlag"]);
                    carAccidentMasterVo.Weather = _defaultValue.GetDefaultValue<string>(sqlDataReader["Weather"]);
                    carAccidentMasterVo.AccidentKind = _defaultValue.GetDefaultValue<string>(sqlDataReader["AccidentKind"]);
                    carAccidentMasterVo.CarStatic = _defaultValue.GetDefaultValue<string>(sqlDataReader["CarStatic"]);
                    carAccidentMasterVo.OccurrenceCause = _defaultValue.GetDefaultValue<string>(sqlDataReader["OccurrenceCause"]);
                    carAccidentMasterVo.Negligence = _defaultValue.GetDefaultValue<string>(sqlDataReader["Negligence"]);
                    carAccidentMasterVo.PersonalInjury = _defaultValue.GetDefaultValue<string>(sqlDataReader["PersonalInjury"]);
                    carAccidentMasterVo.PropertyAccident1 = _defaultValue.GetDefaultValue<string>(sqlDataReader["PropertyAccident1"]);
                    carAccidentMasterVo.PropertyAccident2 = _defaultValue.GetDefaultValue<string>(sqlDataReader["PropertyAccident2"]);
                    carAccidentMasterVo.OccurrenceAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["OccurrenceAddress"]);
                    carAccidentMasterVo.WorkKind = _defaultValue.GetDefaultValue<string>(sqlDataReader["WorkKind"]);
                    carAccidentMasterVo.DisplayName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DisplayName"]);
                    carAccidentMasterVo.LicenseNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["LicenseNumber"]);
                    carAccidentMasterVo.CarRegistrationNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["CarRegistrationNumber"]);
                    carAccidentMasterVo.AccidentSummary = _defaultValue.GetDefaultValue<string>(sqlDataReader["AccidentSummary"]);
                    carAccidentMasterVo.AccidentDetail = _defaultValue.GetDefaultValue<string>(sqlDataReader["AccidentDetail"]);
                    carAccidentMasterVo.Guide = _defaultValue.GetDefaultValue<string>(sqlDataReader["Guide"]);
                    carAccidentMasterVo.Picture1 = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture1"]);
                    carAccidentMasterVo.Picture2 = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture2"]);
                    carAccidentMasterVo.Picture3 = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture3"]);
                    carAccidentMasterVo.Picture4 = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture4"]);
                    carAccidentMasterVo.Picture5 = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture5"]);
                    carAccidentMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    carAccidentMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    carAccidentMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    carAccidentMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    carAccidentMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    carAccidentMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listCarAccidentMasterVo.Add(carAccidentMasterVo);
                }
            }
            return listCarAccidentMasterVo;
        }

        /// <summary>
        /// SelectOneHCarAccidentMaster
        /// Detailで使用
        /// </summary>
        /// <param name="staffCode"></param>
        /// <param name="occurrenceYmdHms"></param>
        /// <returns></returns>
        public CarAccidentMasterVo SelectOneCarAccidentMaster(int staffCode, DateTime occurrenceYmdHms) {
            CarAccidentMasterVo carAccidentMasterVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "OccurrenceYmdHms," +
                                            "TotallingFlag," +
                                            "Weather," +
                                            "AccidentKind," +
                                            "CarStatic," +
                                            "OccurrenceCause," +
                                            "Negligence," +
                                            "PersonalInjury," +
                                            "PropertyAccident1," +
                                            "PropertyAccident2," +
                                            "OccurrenceAddress," +
                                            "WorkKind," +
                                            "DisplayName," +
                                            "LicenseNumber," +
                                            "CarRegistrationNumber," +
                                            "AccidentSummary," +
                                            "AccidentDetail," +
                                            "Guide," +
                                            "Picture1," +
                                            "Picture2," +
                                            "Picture3," +
                                            "Picture4," +
                                            "Picture5," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_CarAccidentMaster " +
                                     "WHERE StaffCode = " + staffCode + " AND OccurrenceYmdHms = '" + occurrenceYmdHms.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            using (var sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    carAccidentMasterVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    carAccidentMasterVo.OccurrenceYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["OccurrenceYmdHms"]);
                    carAccidentMasterVo.TotallingFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["TotallingFlag"]);
                    carAccidentMasterVo.Weather = _defaultValue.GetDefaultValue<string>(sqlDataReader["Weather"]);
                    carAccidentMasterVo.AccidentKind = _defaultValue.GetDefaultValue<string>(sqlDataReader["AccidentKind"]);
                    carAccidentMasterVo.CarStatic = _defaultValue.GetDefaultValue<string>(sqlDataReader["CarStatic"]);
                    carAccidentMasterVo.OccurrenceCause = _defaultValue.GetDefaultValue<string>(sqlDataReader["OccurrenceCause"]);
                    carAccidentMasterVo.Negligence = _defaultValue.GetDefaultValue<string>(sqlDataReader["Negligence"]);
                    carAccidentMasterVo.PersonalInjury = _defaultValue.GetDefaultValue<string>(sqlDataReader["PersonalInjury"]);
                    carAccidentMasterVo.PropertyAccident1 = _defaultValue.GetDefaultValue<string>(sqlDataReader["PropertyAccident1"]);
                    carAccidentMasterVo.PropertyAccident2 = _defaultValue.GetDefaultValue<string>(sqlDataReader["PropertyAccident2"]);
                    carAccidentMasterVo.OccurrenceAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["OccurrenceAddress"]);
                    carAccidentMasterVo.WorkKind = _defaultValue.GetDefaultValue<string>(sqlDataReader["WorkKind"]);
                    carAccidentMasterVo.DisplayName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DisplayName"]);
                    carAccidentMasterVo.LicenseNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["LicenseNumber"]);
                    carAccidentMasterVo.CarRegistrationNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["CarRegistrationNumber"]);
                    carAccidentMasterVo.AccidentSummary = _defaultValue.GetDefaultValue<string>(sqlDataReader["AccidentSummary"]);
                    carAccidentMasterVo.AccidentDetail = _defaultValue.GetDefaultValue<string>(sqlDataReader["AccidentDetail"]);
                    carAccidentMasterVo.Guide = _defaultValue.GetDefaultValue<string>(sqlDataReader["Guide"]);
                    carAccidentMasterVo.Picture1 = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture1"]);
                    carAccidentMasterVo.Picture2 = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture2"]);
                    carAccidentMasterVo.Picture3 = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture3"]);
                    carAccidentMasterVo.Picture4 = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture4"]);
                    carAccidentMasterVo.Picture5 = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture5"]);
                    carAccidentMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    carAccidentMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    carAccidentMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    carAccidentMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    carAccidentMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    carAccidentMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                }
            }
            return carAccidentMasterVo;
        }

        /// <summary>
        /// SelectAllHCarAccidentMaster
        /// </summary>
        /// <param name="dateTime1"></param>
        /// <param name="dateTime2"></param>
        /// <returns></returns>
        public List<CarAccidentMasterVo> SelectAllCarAccidentMaster(DateTime dateTime1, DateTime dateTime2) {
            List<CarAccidentMasterVo> listCarAccidentMasterVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "OccurrenceYmdHms," +
                                            "TotallingFlag," +
                                            "Weather," +
                                            "AccidentKind," +
                                            "CarStatic," +
                                            "OccurrenceCause," +
                                            "Negligence," +
                                            "PersonalInjury," +
                                            "PropertyAccident1," +
                                            "PropertyAccident2," +
                                            "OccurrenceAddress," +
                                            "WorkKind," +
                                            "DisplayName," +
                                            "LicenseNumber," +
                                            "CarRegistrationNumber," +
                                            "AccidentSummary," +
                                            "AccidentDetail," +
                                            "Guide," +
                                            //"Picture1," +
                                            //"Picture2," +
                                            //"Picture3," +
                                            //"Picture4," +
                                            //"Picture5," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_CarAccidentMaster " +
                                     "WHERE (OccurrenceYmdHms BETWEEN '" + dateTime1 + "' AND '" + dateTime2 + "')";
            using (var sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    CarAccidentMasterVo carAccidentMasterVo = new();
                    carAccidentMasterVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    carAccidentMasterVo.OccurrenceYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["OccurrenceYmdHms"]);
                    carAccidentMasterVo.TotallingFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["TotallingFlag"]);
                    carAccidentMasterVo.Weather = _defaultValue.GetDefaultValue<string>(sqlDataReader["Weather"]);
                    carAccidentMasterVo.AccidentKind = _defaultValue.GetDefaultValue<string>(sqlDataReader["AccidentKind"]);
                    carAccidentMasterVo.CarStatic = _defaultValue.GetDefaultValue<string>(sqlDataReader["CarStatic"]);
                    carAccidentMasterVo.OccurrenceCause = _defaultValue.GetDefaultValue<string>(sqlDataReader["OccurrenceCause"]);
                    carAccidentMasterVo.Negligence = _defaultValue.GetDefaultValue<string>(sqlDataReader["Negligence"]);
                    carAccidentMasterVo.PersonalInjury = _defaultValue.GetDefaultValue<string>(sqlDataReader["PersonalInjury"]);
                    carAccidentMasterVo.PropertyAccident1 = _defaultValue.GetDefaultValue<string>(sqlDataReader["PropertyAccident1"]);
                    carAccidentMasterVo.PropertyAccident2 = _defaultValue.GetDefaultValue<string>(sqlDataReader["PropertyAccident2"]);
                    carAccidentMasterVo.OccurrenceAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["OccurrenceAddress"]);
                    carAccidentMasterVo.WorkKind = _defaultValue.GetDefaultValue<string>(sqlDataReader["WorkKind"]);
                    carAccidentMasterVo.DisplayName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DisplayName"]);
                    carAccidentMasterVo.LicenseNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["LicenseNumber"]);
                    carAccidentMasterVo.CarRegistrationNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["CarRegistrationNumber"]);
                    carAccidentMasterVo.AccidentSummary = _defaultValue.GetDefaultValue<string>(sqlDataReader["AccidentSummary"]);
                    carAccidentMasterVo.AccidentDetail = _defaultValue.GetDefaultValue<string>(sqlDataReader["AccidentDetail"]);
                    carAccidentMasterVo.Guide = _defaultValue.GetDefaultValue<string>(sqlDataReader["Guide"]);
                    //hCarAccidentMasterVo.Picture1 = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture1"]);
                    //hCarAccidentMasterVo.Picture2 = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture2"]);
                    //hCarAccidentMasterVo.Picture3 = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture3"]);
                    //hCarAccidentMasterVo.Picture4 = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture4"]);
                    //hCarAccidentMasterVo.Picture5 = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["Picture5"]);
                    carAccidentMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    carAccidentMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    carAccidentMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    carAccidentMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    carAccidentMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    carAccidentMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listCarAccidentMasterVo.Add(carAccidentMasterVo);
                }
            }
            return listCarAccidentMasterVo;
        }

        /// <summary>
        /// InsertOneHCarAccidentMaster
        /// </summary>
        /// <param name="carAccidentMasterVo"></param>
        /// <returns></returns>
        public int InsertOneCarAccidentMaster(CarAccidentMasterVo carAccidentMasterVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_CarAccidentMaster(StaffCode," +
                                                                     "OccurrenceYmdHms," +
                                                                     "TotallingFlag," +
                                                                     "Weather," +
                                                                     "AccidentKind," +
                                                                     "CarStatic," +
                                                                     "OccurrenceCause," +
                                                                     "Negligence," +
                                                                     "PersonalInjury," +
                                                                     "PropertyAccident1," +
                                                                     "PropertyAccident2," +
                                                                     "OccurrenceAddress," +
                                                                     "WorkKind," +
                                                                     "DisplayName," +
                                                                     "LicenseNumber," +
                                                                     "CarRegistrationNumber," +
                                                                     "AccidentSummary," +
                                                                     "AccidentDetail," +
                                                                     "Guide," +
                                                                     "Picture1," +
                                                                     "Picture2," +
                                                                     "Picture3," +
                                                                     "Picture4," +
                                                                     "Picture5," +
                                                                     "InsertPcName," +
                                                                     "InsertYmdHms," +
                                                                     "UpdatePcName," +
                                                                     "UpdateYmdHms," +
                                                                     "DeletePcName," +
                                                                     "DeleteYmdHms," +
                                                                     "DeleteFlag) " +
                                     "VALUES ('" + carAccidentMasterVo.StaffCode + "'," +
                                             "'" + carAccidentMasterVo.OccurrenceYmdHms + "'," +
                                             "'" + carAccidentMasterVo.TotallingFlag + "'," +
                                             "'" + carAccidentMasterVo.Weather + "'," +
                                             "'" + carAccidentMasterVo.AccidentKind + "'," +
                                             "'" + carAccidentMasterVo.CarStatic + "'," +
                                             "'" + carAccidentMasterVo.OccurrenceCause + "'," +
                                             "'" + carAccidentMasterVo.Negligence + "'," +
                                             "'" + carAccidentMasterVo.PersonalInjury + "'," +
                                             "'" + carAccidentMasterVo.PropertyAccident1 + "'," +
                                             "'" + carAccidentMasterVo.PropertyAccident2 + "'," +
                                             "'" + carAccidentMasterVo.OccurrenceAddress + "'," +
                                             "'" + carAccidentMasterVo.WorkKind + "'," +
                                             "'" + carAccidentMasterVo.DisplayName + "'," +
                                             "'" + carAccidentMasterVo.LicenseNumber + "'," +
                                             "'" + carAccidentMasterVo.CarRegistrationNumber + "'," +
                                             "'" + carAccidentMasterVo.AccidentSummary + "'," +
                                             "'" + carAccidentMasterVo.AccidentDetail + "'," +
                                             "'" + carAccidentMasterVo.Guide + "'," +
                                             "@member_picture1," +
                                             "@member_picture2," +
                                             "@member_picture3," +
                                             "@member_picture4," +
                                             "@member_picture5," +
                                             "'" + Environment.MachineName + "'," +
                                             "'" + DateTime.Now + "'," +
                                             "'" + string.Empty + "'," +
                                             "'" + _defaultDateTime + "'," +
                                             "'" + string.Empty + "'," +
                                             "'" + _defaultDateTime + "'," +
                                             "'false'" +
                                             ");";
            try {
                if (carAccidentMasterVo.Picture1 is not null)
                    sqlCommand.Parameters.Add("@member_picture1", SqlDbType.Image, carAccidentMasterVo.Picture1.Length).Value = carAccidentMasterVo.Picture1;
                if (carAccidentMasterVo.Picture2 is not null)
                    sqlCommand.Parameters.Add("@member_picture2", SqlDbType.Image, carAccidentMasterVo.Picture2.Length).Value = carAccidentMasterVo.Picture2;
                if (carAccidentMasterVo.Picture3 is not null)
                    sqlCommand.Parameters.Add("@member_picture3", SqlDbType.Image, carAccidentMasterVo.Picture3.Length).Value = carAccidentMasterVo.Picture3;
                if (carAccidentMasterVo.Picture4 is not null)
                    sqlCommand.Parameters.Add("@member_picture4", SqlDbType.Image, carAccidentMasterVo.Picture4.Length).Value = carAccidentMasterVo.Picture4;
                if (carAccidentMasterVo.Picture5 is not null)
                    sqlCommand.Parameters.Add("@member_picture5", SqlDbType.Image, carAccidentMasterVo.Picture5.Length).Value = carAccidentMasterVo.Picture5;
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// UpdateOneHCarAccidentMaster
        /// </summary>
        /// <param name="carAccidentMasterVo"></param>
        /// <returns></returns>
        public int UpdateOneCarAccidentMaster(CarAccidentMasterVo carAccidentMasterVo) {
            var sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_CarAccidentMaster " +
                                     "SET StaffCode = " + carAccidentMasterVo.StaffCode + "," +
                                         "OccurrenceYmdHms = '" + carAccidentMasterVo.OccurrenceYmdHms + "'," +
                                         "TotallingFlag = '" + carAccidentMasterVo.TotallingFlag + "'," +
                                         "Weather = '" + carAccidentMasterVo.Weather + "'," +
                                         "AccidentKind = '" + carAccidentMasterVo.AccidentKind + "'," +
                                         "CarStatic = '" + carAccidentMasterVo.CarStatic + "'," +
                                         "OccurrenceCause = '" + carAccidentMasterVo.OccurrenceCause + "'," +
                                         "Negligence = '" + carAccidentMasterVo.Negligence + "'," +
                                         "PersonalInjury = '" + carAccidentMasterVo.PersonalInjury + "'," +
                                         "PropertyAccident1 = '" + carAccidentMasterVo.PropertyAccident1 + "'," +
                                         "PropertyAccident2 = '" + carAccidentMasterVo.PropertyAccident2 + "'," +
                                         "OccurrenceAddress = '" + carAccidentMasterVo.OccurrenceAddress + "'," +
                                         "WorkKind = '" + carAccidentMasterVo.WorkKind + "'," +
                                         "DisplayName = '" + carAccidentMasterVo.DisplayName + "'," +
                                         "LicenseNumber = '" + carAccidentMasterVo.LicenseNumber + "'," +
                                         "CarRegistrationNumber = '" + carAccidentMasterVo.CarRegistrationNumber + "'," +
                                         "AccidentSummary = '" + carAccidentMasterVo.AccidentSummary + "'," +
                                         "AccidentDetail = '" + carAccidentMasterVo.AccidentDetail + "'," +
                                         "Guide = '" + carAccidentMasterVo.Guide + "'," +
                                         "Picture1 = @member_picture1," +
                                         "Picture2 = @member_picture2," +
                                         "Picture3 = @member_picture3," +
                                         "Picture4 = @member_picture4," +
                                         "Picture5 = @member_picture5," +
                                         "UpdatePcName = '" + Environment.MachineName + "'," +
                                         "UpdateYmdHms = '" + DateTime.Now + "' " +
                                     "WHERE StaffCode = " + carAccidentMasterVo.StaffCode + " AND OccurrenceYmdHms = '" + carAccidentMasterVo.OccurrenceYmdHms.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            try {
                if (carAccidentMasterVo.Picture1 is not null)
                    sqlCommand.Parameters.Add("@member_picture1", SqlDbType.Image, carAccidentMasterVo.Picture1.Length).Value = carAccidentMasterVo.Picture1;
                if (carAccidentMasterVo.Picture2 is not null)
                    sqlCommand.Parameters.Add("@member_picture2", SqlDbType.Image, carAccidentMasterVo.Picture2.Length).Value = carAccidentMasterVo.Picture2;
                if (carAccidentMasterVo.Picture3 is not null)
                    sqlCommand.Parameters.Add("@member_picture3", SqlDbType.Image, carAccidentMasterVo.Picture3.Length).Value = carAccidentMasterVo.Picture3;
                if (carAccidentMasterVo.Picture4 is not null)
                    sqlCommand.Parameters.Add("@member_picture4", SqlDbType.Image, carAccidentMasterVo.Picture4.Length).Value = carAccidentMasterVo.Picture4;
                if (carAccidentMasterVo.Picture5 is not null)
                    sqlCommand.Parameters.Add("@member_picture5", SqlDbType.Image, carAccidentMasterVo.Picture5.Length).Value = carAccidentMasterVo.Picture5;
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }
    }
}
