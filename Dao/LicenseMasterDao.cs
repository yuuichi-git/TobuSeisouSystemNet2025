/*
 * 2024-02-07
 */
using System.Data;
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class LicenseMasterDao {
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
        public LicenseMasterDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// レコードの有無
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns>true:あり false:なし</returns>
        public bool ExistenceLicenseMaster(int staffCode) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT TOP 1 StaffCode FROM H_LicenseMaster WHERE StaffCode = " + staffCode + "";
            return sqlCommand.ExecuteScalar() is not null ? true : false;
        }

        /// <summary>
        /// GetExpirationDate
        /// 有効期限を取得する
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns>有効期限を返す。存在しない場合はstring.Emptyを返す</returns>
        public string? GetExpirationDate(int staffCode) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT TOP 1 ExpirationDate FROM H_LicenseMaster WHERE StaffCode = " + staffCode + "";
            var data = sqlCommand.ExecuteScalar();
            if (data is not null) {
                return sqlCommand.ExecuteScalar().ToString();
            } else {
                return string.Empty;
            }
        }

        /// <summary>
        /// SelectHLicenseMasterForStaffDetail
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public LicenseMasterVo SelectOneLicenseMaster(int staffCode) {
            LicenseMasterVo licenseMasterVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "NameKana," +
                                            "Name," +
                                            "BirthDate," +
                                            "CurrentAddress," +
                                            "DeliveryDate," +
                                            "ExpirationDate," +
                                            "LicenseCondition," +
                                            "LicenseNumber," +
                                            "GetDate1," +
                                            "GetDate2," +
                                            "GetDate3," +
                                            "PictureHead," +
                                            "PictureTail," +
                                            "Large," +
                                            "Medium," +
                                            "QuasiMedium," +
                                            "Ordinary," +
                                            "BigSpecial," +
                                            "BigAutoBike," +
                                            "OrdinaryAutoBike," +
                                            "SmallSpecial," +
                                            "WithARaw," +
                                            "BigTwo," +
                                            "MediumTwo," +
                                            "OrdinaryTwo," +
                                            "BigSpecialTwo," +
                                            "Traction," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_LicenseMaster " +
                                     "WHERE StaffCode = " + staffCode + "";
            using (var sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    licenseMasterVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    licenseMasterVo.NameKana = _defaultValue.GetDefaultValue<string>(sqlDataReader["NameKana"]);
                    licenseMasterVo.Name = _defaultValue.GetDefaultValue<string>(sqlDataReader["Name"]);
                    licenseMasterVo.BirthDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["BirthDate"]);
                    licenseMasterVo.CurrentAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["CurrentAddress"]);
                    licenseMasterVo.DeliveryDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeliveryDate"]);
                    licenseMasterVo.ExpirationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ExpirationDate"]);
                    licenseMasterVo.LicenseCondition = _defaultValue.GetDefaultValue<string>(sqlDataReader["LicenseCondition"]);
                    licenseMasterVo.LicenseNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["LicenseNumber"]);
                    licenseMasterVo.GetDate1 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["GetDate1"]);
                    licenseMasterVo.GetDate2 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["GetDate2"]);
                    licenseMasterVo.GetDate3 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["GetDate3"]);
                    licenseMasterVo.PictureHead = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["PictureHead"]);
                    licenseMasterVo.PictureTail = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["PictureTail"]);
                    licenseMasterVo.Large = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Large"]);
                    licenseMasterVo.Medium = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Medium"]);
                    licenseMasterVo.QuasiMedium = _defaultValue.GetDefaultValue<bool>(sqlDataReader["QuasiMedium"]);
                    licenseMasterVo.Ordinary = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Ordinary"]);
                    licenseMasterVo.BigSpecial = _defaultValue.GetDefaultValue<bool>(sqlDataReader["BigSpecial"]);
                    licenseMasterVo.BigAutoBike = _defaultValue.GetDefaultValue<bool>(sqlDataReader["BigAutoBike"]);
                    licenseMasterVo.OrdinaryAutoBike = _defaultValue.GetDefaultValue<bool>(sqlDataReader["OrdinaryAutoBike"]);
                    licenseMasterVo.SmallSpecial = _defaultValue.GetDefaultValue<bool>(sqlDataReader["SmallSpecial"]);
                    licenseMasterVo.WithARaw = _defaultValue.GetDefaultValue<bool>(sqlDataReader["WithARaw"]);
                    licenseMasterVo.BigTwo = _defaultValue.GetDefaultValue<bool>(sqlDataReader["BigTwo"]);
                    licenseMasterVo.MediumTwo = _defaultValue.GetDefaultValue<bool>(sqlDataReader["MediumTwo"]);
                    licenseMasterVo.OrdinaryTwo = _defaultValue.GetDefaultValue<bool>(sqlDataReader["OrdinaryTwo"]);
                    licenseMasterVo.BigSpecialTwo = _defaultValue.GetDefaultValue<bool>(sqlDataReader["BigSpecialTwo"]);
                    licenseMasterVo.Traction = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Traction"]);
                    licenseMasterVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    licenseMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    licenseMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    licenseMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    licenseMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    licenseMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    licenseMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                }
            }
            return licenseMasterVo;
        }

        /// <summary>
        /// 免許証画像を抽出する
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public byte[] SelectOnePictureHead(int staffCode) {
            byte[] byteImage = Array.Empty<byte>();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT PictureHead " +
                                     "FROM H_LicenseMaster " +
                                     "WHERE StaffCode = " + staffCode + "";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    byteImage = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["PictureHead"]);
                }
            }
            return byteImage;
        }

        /// <summary>
        /// 免許証画像を抽出する
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public byte[] SelectOnePictureTail(int staffCode) {
            byte[] byteImage = Array.Empty<byte>();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT PictureTail " +
                                     "FROM H_LicenseMaster " +
                                     "WHERE StaffCode = " + staffCode + "";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    byteImage = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["PictureTail"]);
                }
            }
            return byteImage;
        }

        /// <summary>
        /// SelectAllHLicenseMaster
        /// PictureHead/PictureTail無し
        /// </summary>
        /// <returns></returns>
        public List<LicenseMasterVo> SelectAllLicenseMaster() {
            List<LicenseMasterVo> listLicenseMasterVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT H_LicenseMaster.StaffCode," +
                                            "H_LicenseMaster.NameKana," +
                                            "H_LicenseMaster.Name," +
                                            "H_LicenseMaster.BirthDate," +
                                            "H_LicenseMaster.CurrentAddress," +
                                            "H_LicenseMaster.DeliveryDate," +
                                            "H_LicenseMaster.ExpirationDate," +
                                            "H_LicenseMaster.LicenseCondition," +
                                            "H_LicenseMaster.LicenseNumber," +
                                            "H_LicenseMaster.GetDate1," +
                                            "H_LicenseMaster.GetDate2," +
                                            "H_LicenseMaster.GetDate3," +
                                            //"H_LicenseMaster.PictureHead," +
                                            //"H_LicenseMaster.PictureTail," +
                                            "H_LicenseMaster.Large," +
                                            "H_LicenseMaster.Medium," +
                                            "H_LicenseMaster.QuasiMedium," +
                                            "H_LicenseMaster.Ordinary," +
                                            "H_LicenseMaster.BigSpecial," +
                                            "H_LicenseMaster.BigAutoBike," +
                                            "H_LicenseMaster.OrdinaryAutoBike," +
                                            "H_LicenseMaster.SmallSpecial," +
                                            "H_LicenseMaster.WithARaw," +
                                            "H_LicenseMaster.BigTwo," +
                                            "H_LicenseMaster.MediumTwo," +
                                            "H_LicenseMaster.OrdinaryTwo," +
                                            "H_LicenseMaster.BigSpecialTwo," +
                                            "H_LicenseMaster.Traction," +
                                            "H_LicenseMaster.InsertPcName," +
                                            "H_LicenseMaster.InsertYmdHms," +
                                            "H_LicenseMaster.UpdatePcName," +
                                            "H_LicenseMaster.UpdateYmdHms," +
                                            "H_LicenseMaster.DeletePcName," +
                                            "H_LicenseMaster.DeleteYmdHms," +
                                            "H_LicenseMaster.DeleteFlag," +
                                            "H_StaffMaster.UnionCode," +                                                            // StaffMasterの組合コードを取得(LicenseListで使ってる)
                                            "H_StaffMaster.RetirementFlag " +                                                       // StaffMasterの退職フラグを取得(LicenseListで使ってる)
                                     "FROM H_LicenseMaster " +
                                     "LEFT OUTER JOIN H_StaffMaster ON H_LicenseMaster.StaffCode = H_StaffMaster.StaffCode";
            using (var sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    LicenseMasterVo licenseMasterVo = new();
                    licenseMasterVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    licenseMasterVo.NameKana = _defaultValue.GetDefaultValue<string>(sqlDataReader["NameKana"]);
                    licenseMasterVo.Name = _defaultValue.GetDefaultValue<string>(sqlDataReader["Name"]);
                    licenseMasterVo.BirthDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["BirthDate"]);
                    licenseMasterVo.CurrentAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["CurrentAddress"]);
                    licenseMasterVo.DeliveryDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeliveryDate"]);
                    licenseMasterVo.ExpirationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ExpirationDate"]);
                    licenseMasterVo.LicenseCondition = _defaultValue.GetDefaultValue<string>(sqlDataReader["LicenseCondition"]);
                    licenseMasterVo.LicenseNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["LicenseNumber"]);
                    licenseMasterVo.GetDate1 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["GetDate1"]);
                    licenseMasterVo.GetDate2 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["GetDate2"]);
                    licenseMasterVo.GetDate3 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["GetDate3"]);
                    //hLicenseMasterVo.PictureHead = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["PictureHead"]);
                    //hLicenseMasterVo.PictureTail = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["PictureTail"]);
                    licenseMasterVo.Large = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Large"]);
                    licenseMasterVo.Medium = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Medium"]);
                    licenseMasterVo.QuasiMedium = _defaultValue.GetDefaultValue<bool>(sqlDataReader["QuasiMedium"]);
                    licenseMasterVo.Ordinary = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Ordinary"]);
                    licenseMasterVo.BigSpecial = _defaultValue.GetDefaultValue<bool>(sqlDataReader["BigSpecial"]);
                    licenseMasterVo.BigAutoBike = _defaultValue.GetDefaultValue<bool>(sqlDataReader["BigAutoBike"]);
                    licenseMasterVo.OrdinaryAutoBike = _defaultValue.GetDefaultValue<bool>(sqlDataReader["OrdinaryAutoBike"]);
                    licenseMasterVo.SmallSpecial = _defaultValue.GetDefaultValue<bool>(sqlDataReader["SmallSpecial"]);
                    licenseMasterVo.WithARaw = _defaultValue.GetDefaultValue<bool>(sqlDataReader["WithARaw"]);
                    licenseMasterVo.BigTwo = _defaultValue.GetDefaultValue<bool>(sqlDataReader["BigTwo"]);
                    licenseMasterVo.MediumTwo = _defaultValue.GetDefaultValue<bool>(sqlDataReader["MediumTwo"]);
                    licenseMasterVo.OrdinaryTwo = _defaultValue.GetDefaultValue<bool>(sqlDataReader["OrdinaryTwo"]);
                    licenseMasterVo.BigSpecialTwo = _defaultValue.GetDefaultValue<bool>(sqlDataReader["BigSpecialTwo"]);
                    licenseMasterVo.Traction = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Traction"]);
                    licenseMasterVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    licenseMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    licenseMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    licenseMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    licenseMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    licenseMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    licenseMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    licenseMasterVo.UnionCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["UnionCode"]);
                    licenseMasterVo.RetirementFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["RetirementFlag"]);
                    listLicenseMasterVo.Add(licenseMasterVo);
                }
            }
            return listLicenseMasterVo;
        }

        /// <summary>
        /// InsertOneHLicenseMaster
        /// </summary>
        /// <param name="licenseMasterVo"></param>
        /// <returns></returns>
        public int InsertOneLicenseMaster(LicenseMasterVo licenseMasterVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_LicenseMaster(StaffCode," +
                                                                 "NameKana," +
                                                                 "Name," +
                                                                 "BirthDate," +
                                                                 "CurrentAddress," +
                                                                 "DeliveryDate," +
                                                                 "ExpirationDate," +
                                                                 "LicenseCondition," +
                                                                 "LicenseNumber," +
                                                                 "GetDate1," +
                                                                 "GetDate2," +
                                                                 "GetDate3," +
                                                                 "PictureHead," +
                                                                 "PictureTail," +
                                                                 "Large," +
                                                                 "Medium," +
                                                                 "QuasiMedium," +
                                                                 "Ordinary," +
                                                                 "BigSpecial," +
                                                                 "BigAutoBike," +
                                                                 "OrdinaryAutoBike," +
                                                                 "SmallSpecial," +
                                                                 "WithARaw," +
                                                                 "BigTwo," +
                                                                 "MediumTwo," +
                                                                 "OrdinaryTwo," +
                                                                 "BigSpecialTwo," +
                                                                 "Traction," +
                                                                 "InsertPcName," +
                                                                 "InsertYmdHms," +
                                                                 "UpdatePcName," +
                                                                 "UpdateYmdHms," +
                                                                 "DeletePcName," +
                                                                 "DeleteYmdHms," +
                                                                 "DeleteFlag) " +
                                     "VALUES (" + licenseMasterVo.StaffCode + "," +
                                            "'" + licenseMasterVo.NameKana + "'," +
                                            "'" + licenseMasterVo.Name + "'," +
                                            "'" + licenseMasterVo.BirthDate + "'," +
                                            "'" + licenseMasterVo.CurrentAddress + "'," +
                                            "'" + licenseMasterVo.DeliveryDate + "'," +
                                            "'" + licenseMasterVo.ExpirationDate + "'," +
                                            "'" + licenseMasterVo.LicenseCondition + "'," +
                                            "'" + licenseMasterVo.LicenseNumber + "'," +
                                            "'" + licenseMasterVo.GetDate1 + "'," +
                                            "'" + licenseMasterVo.GetDate2 + "'," +
                                            "'" + licenseMasterVo.GetDate3 + "'," +
                                            "@memberPictureHead," +
                                            "@memberPictureTail," +
                                            "'" + licenseMasterVo.Large + "'," +
                                            "'" + licenseMasterVo.Medium + "'," +
                                            "'" + licenseMasterVo.QuasiMedium + "'," +
                                            "'" + licenseMasterVo.Ordinary + "'," +
                                            "'" + licenseMasterVo.BigSpecial + "'," +
                                            "'" + licenseMasterVo.BigAutoBike + "'," +
                                            "'" + licenseMasterVo.OrdinaryAutoBike + "'," +
                                            "'" + licenseMasterVo.SmallSpecial + "'," +
                                            "'" + licenseMasterVo.WithARaw + "'," +
                                            "'" + licenseMasterVo.BigTwo + "'," +
                                            "'" + licenseMasterVo.MediumTwo + "'," +
                                            "'" + licenseMasterVo.OrdinaryTwo + "'," +
                                            "'" + licenseMasterVo.BigSpecialTwo + "'," +
                                            "'" + licenseMasterVo.Traction + "'," +
                                            "'" + Environment.MachineName + "'," +
                                            "'" + DateTime.Now + "'," +
                                            "'" + string.Empty + "'," +
                                            "'" + _defaultDateTime + "'," +
                                            "'" + string.Empty + "'," +
                                            "'" + _defaultDateTime + "'," +
                                            "'false'" +
                                            ");";
            try {
                sqlCommand.Parameters.Add("@memberPictureHead", SqlDbType.Image, licenseMasterVo.PictureHead.Length).Value = licenseMasterVo.PictureHead;
                sqlCommand.Parameters.Add("@memberPictureTail", SqlDbType.Image, licenseMasterVo.PictureTail.Length).Value = licenseMasterVo.PictureTail;
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// UpdateOneHLicenseLedger
        /// </summary>
        /// <param name="licenseMasterVo"></param>
        /// <returns></returns>
        public int UpdateOneLicenseLedger(LicenseMasterVo licenseMasterVo) {
            var sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_LicenseMaster " +
                                     "SET StaffCode = " + licenseMasterVo.StaffCode + "," +
                                         "NameKana = '" + licenseMasterVo.NameKana + "'," +
                                         "Name = '" + licenseMasterVo.Name + "'," +
                                         "BirthDate = '" + licenseMasterVo.BirthDate + "'," +
                                         "CurrentAddress = '" + licenseMasterVo.CurrentAddress + "'," +
                                         "DeliveryDate = '" + licenseMasterVo.DeliveryDate + "'," +
                                         "ExpirationDate = '" + licenseMasterVo.ExpirationDate + "'," +
                                         "LicenseCondition = '" + licenseMasterVo.LicenseCondition + "'," +
                                         "LicenseNumber = '" + licenseMasterVo.LicenseNumber + "'," +
                                         "GetDate1 = '" + licenseMasterVo.GetDate1 + "'," +
                                         "GetDate2 = '" + licenseMasterVo.GetDate2 + "'," +
                                         "GetDate3 = '" + licenseMasterVo.GetDate3 + "'," +
                                         "PictureHead = @memberPictureHead," +
                                         "PictureTail = @memberPictureTail," +
                                         "Large = '" + licenseMasterVo.Large + "'," +
                                         "Medium = '" + licenseMasterVo.Medium + "'," +
                                         "QuasiMedium = '" + licenseMasterVo.QuasiMedium + "'," +
                                         "Ordinary = '" + licenseMasterVo.Ordinary + "'," +
                                         "BigSpecial = '" + licenseMasterVo.BigSpecial + "'," +
                                         "BigAutoBike = '" + licenseMasterVo.BigAutoBike + "'," +
                                         "OrdinaryAutoBike = '" + licenseMasterVo.OrdinaryAutoBike + "'," +
                                         "SmallSpecial = '" + licenseMasterVo.SmallSpecial + "'," +
                                         "WithARaw = '" + licenseMasterVo.WithARaw + "'," +
                                         "BigTwo = '" + licenseMasterVo.BigTwo + "'," +
                                         "MediumTwo = '" + licenseMasterVo.MediumTwo + "'," +
                                         "OrdinaryTwo = '" + licenseMasterVo.OrdinaryTwo + "'," +
                                         "BigSpecialTwo = '" + licenseMasterVo.BigSpecialTwo + "'," +
                                         "Traction = '" + licenseMasterVo.Traction + "'," +
                                         "UpdatePcName = '" + Environment.MachineName + "'," +
                                         "UpdateYmdHms = '" + DateTime.Now + "' " +
                                     "WHERE StaffCode = " + licenseMasterVo.StaffCode;
            try {
                sqlCommand.Parameters.Add("@memberPictureHead", SqlDbType.Image, licenseMasterVo.PictureHead.Length).Value = licenseMasterVo.PictureHead;
                sqlCommand.Parameters.Add("@memberPictureTail", SqlDbType.Image, licenseMasterVo.PictureTail.Length).Value = licenseMasterVo.PictureTail;
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 条件等をグループ化して返す
        /// </summary>
        /// <returns></returns>
        public List<string> SelectGroupLicenseCondition() {
            List<string> listGroupLicenseCondition = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT LicenseCondition " +
                                     "FROM H_LicenseMaster " +
                                     "GROUP BY LicenseCondition";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    string licenseCondition = _defaultValue.GetDefaultValue<string>(sqlDataReader["LicenseCondition"]);
                    listGroupLicenseCondition.Add(licenseCondition);
                }
            }
            return listGroupLicenseCondition;
        }
    }
}
