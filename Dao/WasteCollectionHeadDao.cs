/*
 * 2026-01-26
 */
using System.Data;
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class WasteCollectionHeadDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        public WasteCollectionHeadDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// Idに等しいレコードの存在を確認する
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true:該当レコードあり false:該当レコードなし</returns>
        public bool ExistenceWasteCollectionHead(int id) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(Id) " +
                                     "FROM H_WasteCollectionHead " +
                                     "WHERE Id = " + id + "";
            try {
                return (int)sqlCommand.ExecuteScalar() != 0 ? true : false;
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetNewId() {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT MAX(Id) " +
                                     "FROM H_WasteCollectionHead";
            try {
                return sqlCommand.ExecuteScalar() is DBNull ? 1 : ((int)sqlCommand.ExecuteScalar() + 1);
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WasteCollectionHeadVo SelectOneWasteCollectionHead(int id) {
            WasteCollectionHeadVo wasteCollectionHeadVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT H_WasteCollectionHead.Id," +
                                            "H_WasteCollectionHead.OfficeQuotationDate," +
                                            "H_WasteCollectionHead.OfficeRequestWord," +
                                            "H_WordMaster.Name AS OfficeRequestWordName," +
                                            "H_WasteCollectionHead.OfficeCompanyName," +
                                            "H_WasteCollectionHead.OfficeContactPerson," +
                                            "H_WasteCollectionHead.OfficeAddress," +
                                            "H_WasteCollectionHead.OfficeTelephoneNumber," +
                                            "H_WasteCollectionHead.OfficeCellphoneNumber," +
                                            "H_WasteCollectionHead.WorkSiteLocation," +
                                            "H_WasteCollectionHead.WorkSiteAddress," +
                                            "H_WasteCollectionHead.PickupDate," +
                                            "H_WasteCollectionHead.Remarks," +
                                            "H_WasteCollectionHead.MainPicture," +
                                            "H_WasteCollectionHead.SubPicture," +
                                            "H_WasteCollectionHead.InsertPcName," +
                                            "H_WasteCollectionHead.InsertYmdHms," +
                                            "H_WasteCollectionHead.UpdatePcName," +
                                            "H_WasteCollectionHead.UpdateYmdHms," +
                                            "H_WasteCollectionHead.DeletePcName," +
                                            "H_WasteCollectionHead.DeleteYmdHms," +
                                            "H_WasteCollectionHead.DeleteFlag " +
                                     "FROM H_WasteCollectionHead " +
                                     "LEFT OUTER JOIN H_WordMaster ON H_WasteCollectionHead.OfficeRequestWord = H_WordMaster.Code " +
                                     "WHERE Id = " + id + "";

            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    wasteCollectionHeadVo.Id = _defaultValue.GetDefaultValue<int>(sqlDataReader["Id"]);
                    wasteCollectionHeadVo.OfficeQuotationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["OfficeQuotationDate"]);
                    wasteCollectionHeadVo.OfficeRequestWord = _defaultValue.GetDefaultValue<int>(sqlDataReader["OfficeRequestWord"]);
                    wasteCollectionHeadVo.OfficeRequestWordName = _defaultValue.GetDefaultValue<string>(sqlDataReader["OfficeRequestWordName"]);
                    wasteCollectionHeadVo.OfficeCompanyName = _defaultValue.GetDefaultValue<string>(sqlDataReader["OfficeCompanyName"]);
                    wasteCollectionHeadVo.OfficeContactPerson = _defaultValue.GetDefaultValue<string>(sqlDataReader["OfficeContactPerson"]);
                    wasteCollectionHeadVo.OfficeAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["OfficeAddress"]);
                    wasteCollectionHeadVo.OfficeTelephoneNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["OfficeTelephoneNumber"]);
                    wasteCollectionHeadVo.OfficeCellphoneNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["OfficeCellphoneNumber"]);
                    wasteCollectionHeadVo.WorkSiteLocation = _defaultValue.GetDefaultValue<string>(sqlDataReader["WorkSiteLocation"]);
                    wasteCollectionHeadVo.WorkSiteAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["WorkSiteAddress"]);
                    wasteCollectionHeadVo.PickupDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["PickupDate"]);
                    wasteCollectionHeadVo.Remarks = _defaultValue.GetDefaultValue<string>(sqlDataReader["Remarks"]);
                    wasteCollectionHeadVo.MainPicture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["MainPicture"]);
                    wasteCollectionHeadVo.SubPicture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["SubPicture"]);
                    wasteCollectionHeadVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    wasteCollectionHeadVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    wasteCollectionHeadVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    wasteCollectionHeadVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    wasteCollectionHeadVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    wasteCollectionHeadVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    wasteCollectionHeadVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                }
            }
            return wasteCollectionHeadVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WasteCollectionHeadVo> SelectAllWasteCollectionHead() {
            List<WasteCollectionHeadVo> listWasteCollectionHeadVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT H_WasteCollectionHead.Id," +
                                            "H_WasteCollectionHead.OfficeQuotationDate," +
                                            "H_WasteCollectionHead.OfficeRequestWord," +
                                            "H_WordMaster.Name AS OfficeRequestWordName," +
                                            "H_WasteCollectionHead.OfficeCompanyName," +
                                            "H_WasteCollectionHead.OfficeContactPerson," +
                                            "H_WasteCollectionHead.OfficeAddress," +
                                            "H_WasteCollectionHead.OfficeTelephoneNumber," +
                                            "H_WasteCollectionHead.OfficeCellphoneNumber," +
                                            "H_WasteCollectionHead.WorkSiteLocation," +
                                            "H_WasteCollectionHead.WorkSiteAddress," +
                                            "H_WasteCollectionHead.PickupDate," +
                                            "H_WasteCollectionHead.Remarks," +
                                            //"H_WasteCollectionHead.MainPicture," +
                                            //"H_WasteCollectionHead.SubPicture," +
                                            "H_WasteCollectionHead.InsertPcName," +
                                            "H_WasteCollectionHead.InsertYmdHms," +
                                            "H_WasteCollectionHead.UpdatePcName," +
                                            "H_WasteCollectionHead.UpdateYmdHms," +
                                            "H_WasteCollectionHead.DeletePcName," +
                                            "H_WasteCollectionHead.DeleteYmdHms," +
                                            "H_WasteCollectionHead.DeleteFlag " +
                                     "FROM H_WasteCollectionHead " +
                                     "LEFT OUTER JOIN H_WordMaster ON H_WasteCollectionHead.OfficeRequestWord = H_WordMaster.Code";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    WasteCollectionHeadVo wasteCollectionHeadVo = new();
                    wasteCollectionHeadVo.Id = _defaultValue.GetDefaultValue<int>(sqlDataReader["Id"]);
                    wasteCollectionHeadVo.OfficeQuotationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["OfficeQuotationDate"]);
                    wasteCollectionHeadVo.OfficeRequestWord = _defaultValue.GetDefaultValue<int>(sqlDataReader["OfficeRequestWord"]);
                    wasteCollectionHeadVo.OfficeRequestWordName = _defaultValue.GetDefaultValue<string>(sqlDataReader["OfficeRequestWordName"]);
                    wasteCollectionHeadVo.OfficeCompanyName = _defaultValue.GetDefaultValue<string>(sqlDataReader["OfficeCompanyName"]);
                    wasteCollectionHeadVo.OfficeContactPerson = _defaultValue.GetDefaultValue<string>(sqlDataReader["OfficeContactPerson"]);
                    wasteCollectionHeadVo.OfficeAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["OfficeAddress"]);
                    wasteCollectionHeadVo.OfficeTelephoneNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["OfficeTelephoneNumber"]);
                    wasteCollectionHeadVo.OfficeCellphoneNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["OfficeCellphoneNumber"]);
                    wasteCollectionHeadVo.WorkSiteLocation = _defaultValue.GetDefaultValue<string>(sqlDataReader["WorkSiteLocation"]);
                    wasteCollectionHeadVo.WorkSiteAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["WorkSiteAddress"]);
                    wasteCollectionHeadVo.PickupDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["PickupDate"]);
                    wasteCollectionHeadVo.Remarks = _defaultValue.GetDefaultValue<string>(sqlDataReader["Remarks"]);
                    //wasteCollectionHeadVo.MainPicture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["MainPicture"]);
                    //wasteCollectionHeadVo.SubPicture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["SubPicture"]);
                    wasteCollectionHeadVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    wasteCollectionHeadVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    wasteCollectionHeadVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    wasteCollectionHeadVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    wasteCollectionHeadVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    wasteCollectionHeadVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    wasteCollectionHeadVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listWasteCollectionHeadVo.Add(wasteCollectionHeadVo);
                }
            }
            return listWasteCollectionHeadVo;
        }

        /// <summary>
        /// InsertOneWasteCollectionHead
        /// </summary>
        /// <param name="wasteCollectionHeadVo"></param>
        public void InsertOneWasteCollectionHead(WasteCollectionHeadVo wasteCollectionHeadVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_WasteCollectionHead(Id," +
                                                                       "OfficeQuotationDate," +
                                                                       "OfficeRequestWord," +
                                                                       "OfficeCompanyName," +
                                                                       "OfficeContactPerson," +
                                                                       "OfficeAddress," +
                                                                       "OfficeTelephoneNumber," +
                                                                       "OfficeCellphoneNumber," +
                                                                       "WorkSiteLocation," +
                                                                       "WorkSiteAddress," +
                                                                       "PickupDate," +
                                                                       "Remarks," +
                                                                       "MainPicture," +
                                                                       "SubPicture," +
                                                                       "InsertPcName," +
                                                                       "InsertYmdHms," +
                                                                       "UpdatePcName," +
                                                                       "UpdateYmdHms," +
                                                                       "DeletePcName," +
                                                                       "DeleteYmdHms," +
                                                                       "DeleteFlag) " +
                                     "VALUES (" + wasteCollectionHeadVo.Id + "," +
                                            "'" + wasteCollectionHeadVo.OfficeQuotationDate + "'," +
                                             "" + wasteCollectionHeadVo.OfficeRequestWord + "," +
                                            "'" + wasteCollectionHeadVo.OfficeCompanyName + "'," +
                                            "'" + wasteCollectionHeadVo.OfficeContactPerson + "'," +
                                            "'" + wasteCollectionHeadVo.OfficeAddress + "'," +
                                            "'" + wasteCollectionHeadVo.OfficeTelephoneNumber + "'," +
                                            "'" + wasteCollectionHeadVo.OfficeCellphoneNumber + "'," +
                                            "'" + wasteCollectionHeadVo.WorkSiteLocation + "'," +
                                            "'" + wasteCollectionHeadVo.WorkSiteAddress + "'," +
                                            "'" + wasteCollectionHeadVo.PickupDate + "'," +
                                            "'" + wasteCollectionHeadVo.Remarks + "'," +
                                            "@member_MainPicture," +
                                            "@member_SubPicture," +
                                            "'" + Environment.MachineName + "'," +
                                            "'" + DateTime.Now + "'," +
                                            "'" + string.Empty + "'," +
                                            "'" + _defaultDateTime + "'," +
                                            "'" + string.Empty + "'," +
                                            "'" + _defaultDateTime + "'," +
                                             "'false'" +
                                             ");";
            try {
                sqlCommand.Parameters.Add("@member_MainPicture", SqlDbType.Image, wasteCollectionHeadVo.MainPicture.Length).Value = wasteCollectionHeadVo.MainPicture;
                sqlCommand.Parameters.Add("@member_SubPicture", SqlDbType.Image, wasteCollectionHeadVo.SubPicture.Length).Value = wasteCollectionHeadVo.SubPicture;
                sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// UpdateOneWasteCollectionHead
        /// </summary>
        /// <param name="wasteCollectionHeadVo"></param>
        public void UpdateOneWasteCollectionHead(WasteCollectionHeadVo wasteCollectionHeadVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_WasteCollectionHead " +
                                     "SET Id = " + wasteCollectionHeadVo.Id + "," +
                                         "OfficeQuotationDate = '" + wasteCollectionHeadVo.OfficeQuotationDate + "'," +
                                         "OfficeRequestWord = " + wasteCollectionHeadVo.OfficeRequestWord + "," +
                                         "OfficeCompanyName = '" + wasteCollectionHeadVo.OfficeCompanyName + "'," +
                                         "OfficeContactPerson = '" + wasteCollectionHeadVo.OfficeContactPerson + "'," +
                                         "OfficeAddress = '" + wasteCollectionHeadVo.OfficeAddress + "'," +
                                         "OfficeTelephoneNumber = '" + wasteCollectionHeadVo.OfficeTelephoneNumber + "'," +
                                         "OfficeCellphoneNumber = '" + wasteCollectionHeadVo.OfficeCellphoneNumber + "'," +
                                         "WorkSiteLocation = '" + wasteCollectionHeadVo.WorkSiteLocation + "'," +
                                         "WorkSiteAddress = '" + wasteCollectionHeadVo.WorkSiteAddress + "'," +
                                         "PickupDate = '" + wasteCollectionHeadVo.PickupDate + "'," +
                                         "Remarks = '" + wasteCollectionHeadVo.Remarks + "'," +
                                         "MainPicture = @member_MainPicture," +
                                         "SubPicture = @member_SubPicture," +
                                         "UpdatePcName = '" + Environment.MachineName + "'," +
                                         "UpdateYmdHms = '" + DateTime.Now + "' " +
                                     "WHERE Id = " + wasteCollectionHeadVo.Id + "";
            try {
                sqlCommand.Parameters.Add("@member_MainPicture", SqlDbType.Image, wasteCollectionHeadVo.MainPicture.Length).Value = wasteCollectionHeadVo.MainPicture;
                sqlCommand.Parameters.Add("@member_SubPicture", SqlDbType.Image, wasteCollectionHeadVo.SubPicture.Length).Value = wasteCollectionHeadVo.SubPicture;
                sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }
    }
}
