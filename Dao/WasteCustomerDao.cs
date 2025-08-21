/*
 * 2025-06-16
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class WasteCustomerDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public WasteCustomerDao(ConnectionVo connectionVo) {
            /*
             * 
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// Idに等しいレコードの存在を確認する
        /// </summary>
        /// <param name="targetId"></param>
        /// <returns>true:該当レコードあり false:該当レコードなし</returns>
        public bool ExistenceWasteCustomerVo(int targetId) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(Id) " +
                                     "FROM H_WasteCustomer " +
                                     "WHERE Id = " + targetId + "";
            try {
                return (int)sqlCommand.ExecuteScalar() != 0 ? true : false;
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 新規でIdを取得する
        /// </summary>
        /// <returns></returns>
        public int GetId() {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT MAX(Id) " +
                                     "FROM H_WasteCustomer";
            try {
                if (sqlCommand.ExecuteScalar() is not DBNull) {
                    return (int)sqlCommand.ExecuteScalar() + 1;
                } else {
                    return 1;
                }
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetId"></param>
        /// <returns></returns>
        public WasteCustomerVo SelectOneWasteCustomerVo(int targetId) {
            WasteCustomerVo wasteCustomerVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT Id," +
                                            "ConcludedDate," +
                                            "ConcludedDetail," +
                                            "TransportationCompany," +
                                            "EmissionCompanyKana," +
                                            "EmissionCompanyName," +
                                            "PostNumber," +
                                            "Address," +
                                            "TelephoneNumber," +
                                            "FaxNumber," +
                                            "EmissionPlaceName," +
                                            "EmissionPlaceAddress," +
                                            "UnitPriceFlammable," +
                                            "UnitPriceCollection," +
                                            "UnitPriceDisposal," +
                                            "UnitPriceResources," +
                                            "UnitPriceTransportationCosts," +
                                            "UnitPriceManifestCosts," +
                                            "UnitPriceOtherCosts," +
                                            "UnitPriceBulkyTransportationCosts," +
                                            "UnitPriceBulkyDisposal," +
                                            "Remarks," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_WasteCustomer " +
                                     "WHERE Id = " + targetId + "";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    wasteCustomerVo.Id = _defaultValue.GetDefaultValue<int>(sqlDataReader["Id"]);
                    wasteCustomerVo.ConcludedDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ConcludedDate"]);
                    wasteCustomerVo.ConcludedDetail = _defaultValue.GetDefaultValue<string>(sqlDataReader["ConcludedDetail"]);
                    wasteCustomerVo.TransportationCompany = _defaultValue.GetDefaultValue<string>(sqlDataReader["TransportationCompany"]);
                    wasteCustomerVo.EmissionCompanyKana = _defaultValue.GetDefaultValue<string>(sqlDataReader["EmissionCompanyKana"]);
                    wasteCustomerVo.EmissionCompanyName = _defaultValue.GetDefaultValue<string>(sqlDataReader["EmissionCompanyName"]);
                    wasteCustomerVo.PostNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["PostNumber"]);
                    wasteCustomerVo.Address = _defaultValue.GetDefaultValue<string>(sqlDataReader["Address"]);
                    wasteCustomerVo.TelephoneNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["TelephoneNumber"]);
                    wasteCustomerVo.FaxNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["FaxNumber"]);
                    wasteCustomerVo.EmissionPlaceName = _defaultValue.GetDefaultValue<string>(sqlDataReader["EmissionPlaceName"]);
                    wasteCustomerVo.EmissionPlaceAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["EmissionPlaceAddress"]);
                    wasteCustomerVo.UnitPriceFlammable = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["UnitPriceFlammable"]);
                    wasteCustomerVo.UnitPriceCollection = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["UnitPriceCollection"]);
                    wasteCustomerVo.UnitPriceDisposal = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["UnitPriceDisposal"]);
                    wasteCustomerVo.UnitPriceResources = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["UnitPriceResources"]);
                    wasteCustomerVo.UnitPriceTransportationCosts = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["UnitPriceTransportationCosts"]);
                    wasteCustomerVo.UnitPriceManifestCosts = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["UnitPriceManifestCosts"]);
                    wasteCustomerVo.UnitPriceOtherCosts = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["UnitPriceOtherCosts"]);
                    wasteCustomerVo.UnitPriceBulkyTransportationCosts = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["UnitPriceBulkyTransportationCosts"]);
                    wasteCustomerVo.UnitPriceBulkyDisposal = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["UnitPriceBulkyDisposal"]);
                    wasteCustomerVo.Remarks = _defaultValue.GetDefaultValue<string>(sqlDataReader["Remarks"]);
                    wasteCustomerVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    wasteCustomerVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    wasteCustomerVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    wasteCustomerVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    wasteCustomerVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    wasteCustomerVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    wasteCustomerVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                }
            }
            return wasteCustomerVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WasteCustomerVo> SelectAllWasteCustomerVo() {
            List<WasteCustomerVo> listWasteCustomerVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT Id," +
                                            "ConcludedDate," +
                                            "ConcludedDetail," +
                                            "TransportationCompany," +
                                            "EmissionCompanyKana," +
                                            "EmissionCompanyName," +
                                            "PostNumber," +
                                            "Address," +
                                            "TelephoneNumber," +
                                            "FaxNumber," +
                                            "EmissionPlaceName," +
                                            "EmissionPlaceAddress," +
                                            "UnitPriceFlammable," +
                                            "UnitPriceCollection," +
                                            "UnitPriceDisposal," +
                                            "UnitPriceResources," +
                                            "UnitPriceTransportationCosts," +
                                            "UnitPriceManifestCosts," +
                                            "UnitPriceOtherCosts," +
                                            "UnitPriceBulkyTransportationCosts," +
                                            "UnitPriceBulkyDisposal," +
                                            "Remarks," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_WasteCustomer";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    WasteCustomerVo wasteCustomerVo = new();
                    wasteCustomerVo.Id = _defaultValue.GetDefaultValue<int>(sqlDataReader["Id"]);
                    wasteCustomerVo.ConcludedDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ConcludedDate"]);
                    wasteCustomerVo.ConcludedDetail = _defaultValue.GetDefaultValue<string>(sqlDataReader["ConcludedDetail"]);
                    wasteCustomerVo.TransportationCompany = _defaultValue.GetDefaultValue<string>(sqlDataReader["TransportationCompany"]);
                    wasteCustomerVo.EmissionCompanyKana = _defaultValue.GetDefaultValue<string>(sqlDataReader["EmissionCompanyKana"]);
                    wasteCustomerVo.EmissionCompanyName = _defaultValue.GetDefaultValue<string>(sqlDataReader["EmissionCompanyName"]);
                    wasteCustomerVo.PostNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["PostNumber"]);
                    wasteCustomerVo.Address = _defaultValue.GetDefaultValue<string>(sqlDataReader["Address"]);
                    wasteCustomerVo.TelephoneNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["TelephoneNumber"]);
                    wasteCustomerVo.FaxNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["FaxNumber"]);
                    wasteCustomerVo.EmissionPlaceName = _defaultValue.GetDefaultValue<string>(sqlDataReader["EmissionPlaceName"]);
                    wasteCustomerVo.EmissionPlaceAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["EmissionPlaceAddress"]);
                    wasteCustomerVo.UnitPriceFlammable = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["UnitPriceFlammable"]);
                    wasteCustomerVo.UnitPriceCollection = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["UnitPriceCollection"]);
                    wasteCustomerVo.UnitPriceDisposal = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["UnitPriceDisposal"]);
                    wasteCustomerVo.UnitPriceResources = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["UnitPriceResources"]);
                    wasteCustomerVo.UnitPriceTransportationCosts = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["UnitPriceTransportationCosts"]);
                    wasteCustomerVo.UnitPriceManifestCosts = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["UnitPriceManifestCosts"]);
                    wasteCustomerVo.UnitPriceOtherCosts = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["UnitPriceOtherCosts"]);
                    wasteCustomerVo.UnitPriceBulkyTransportationCosts = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["UnitPriceBulkyTransportationCosts"]);
                    wasteCustomerVo.UnitPriceBulkyDisposal = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["UnitPriceBulkyDisposal"]);
                    wasteCustomerVo.Remarks = _defaultValue.GetDefaultValue<string>(sqlDataReader["Remarks"]);
                    wasteCustomerVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    wasteCustomerVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    wasteCustomerVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    wasteCustomerVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    wasteCustomerVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    wasteCustomerVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    wasteCustomerVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listWasteCustomerVo.Add(wasteCustomerVo);
                }
            }
            return listWasteCustomerVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wasteCustomerVo"></param>
        public void InsertOneWasteCustomerVo(WasteCustomerVo wasteCustomerVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_WasteCustomer(Id," +
                                                                 "ConcludedDate," +
                                                                 "ConcludedDetail," +
                                                                 "TransportationCompany," +
                                                                 "EmissionCompanyKana," +
                                                                 "EmissionCompanyName," +
                                                                 "PostNumber," +
                                                                 "Address," +
                                                                 "TelephoneNumber," +
                                                                 "FaxNumber," +
                                                                 "EmissionPlaceName," +
                                                                 "EmissionPlaceAddress," +
                                                                 "UnitPriceFlammable," +
                                                                 "UnitPriceCollection," +
                                                                 "UnitPriceDisposal," +
                                                                 "UnitPriceResources," +
                                                                 "UnitPriceTransportationCosts," +
                                                                 "UnitPriceManifestCosts," +
                                                                 "UnitPriceOtherCosts," +
                                                                 "UnitPriceBulkyTransportationCosts," +
                                                                 "UnitPriceBulkyDisposal," +
                                                                 "Remarks," +
                                                                 "InsertPcName," +
                                                                 "InsertYmdHms," +
                                                                 "UpdatePcName," +
                                                                 "UpdateYmdHms," +
                                                                 "DeletePcName," +
                                                                 "DeleteYmdHms," +
                                                                 "DeleteFlag) " +
                                     "VALUES (" + wasteCustomerVo.Id + "," +
                                            "'" + wasteCustomerVo.ConcludedDate + "'," +
                                            "'" + wasteCustomerVo.ConcludedDetail + "'," +
                                            "'" + wasteCustomerVo.TransportationCompany + "'," +
                                            "'" + wasteCustomerVo.EmissionCompanyKana + "'," +
                                            "'" + wasteCustomerVo.EmissionCompanyName + "'," +
                                            "'" + wasteCustomerVo.PostNumber + "'," +
                                            "'" + wasteCustomerVo.Address + "'," +
                                            "'" + wasteCustomerVo.TelephoneNumber + "'," +
                                            "'" + wasteCustomerVo.FaxNumber + "'," +
                                            "'" + wasteCustomerVo.EmissionPlaceName + "'," +
                                            "'" + wasteCustomerVo.EmissionPlaceAddress + "'," +
                                             "" + wasteCustomerVo.UnitPriceFlammable + "," +
                                             "" + wasteCustomerVo.UnitPriceCollection + "," +
                                             "" + wasteCustomerVo.UnitPriceDisposal + "," +
                                             "" + wasteCustomerVo.UnitPriceResources + "," +
                                             "" + wasteCustomerVo.UnitPriceTransportationCosts + "," +
                                             "" + wasteCustomerVo.UnitPriceManifestCosts + "," +
                                             "" + wasteCustomerVo.UnitPriceOtherCosts + "," +
                                             "" + wasteCustomerVo.UnitPriceBulkyTransportationCosts + "," +
                                             "" + wasteCustomerVo.UnitPriceBulkyDisposal + "," +
                                            "'" + wasteCustomerVo.Remarks + "'," +
                                            "'" + wasteCustomerVo.InsertPcName + "'," +
                                            "'" + wasteCustomerVo.InsertYmdHms + "'," +
                                            "'" + string.Empty + "'," +
                                            "'" + _defaultDateTime + "'," +
                                            "'" + string.Empty + "'," +
                                            "'" + _defaultDateTime + "'," +
                                             "'false'" +
                                             ");";
            try {
                sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wasteCustomerVo"></param>
        public void UpdateOneWasteCustomerVo(WasteCustomerVo wasteCustomerVo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_WasteCustomer " +
                                     "SET ConcludedDate = '" + _defaultValue.GetDefaultValue<DateTime>(wasteCustomerVo.ConcludedDate) + "'," +
                                         "ConcludedDetail = '" + _defaultValue.GetDefaultValue<string>(wasteCustomerVo.ConcludedDetail) + "'," +
                                         "TransportationCompany = '" + _defaultValue.GetDefaultValue<string>(wasteCustomerVo.TransportationCompany) + "'," +
                                         "EmissionCompanyKana = '" + _defaultValue.GetDefaultValue<string>(wasteCustomerVo.EmissionCompanyKana) + "'," +
                                         "EmissionCompanyName = '" + _defaultValue.GetDefaultValue<string>(wasteCustomerVo.EmissionCompanyName) + "'," +
                                         "PostNumber = '" + _defaultValue.GetDefaultValue<string>(wasteCustomerVo.PostNumber) + "'," +
                                         "Address = '" + _defaultValue.GetDefaultValue<string>(wasteCustomerVo.Address) + "'," +
                                         "TelephoneNumber = '" + _defaultValue.GetDefaultValue<string>(wasteCustomerVo.TelephoneNumber) + "'," +
                                         "FaxNumber = '" + _defaultValue.GetDefaultValue<string>(wasteCustomerVo.FaxNumber) + "'," +
                                         "EmissionPlaceName = '" + _defaultValue.GetDefaultValue<string>(wasteCustomerVo.EmissionPlaceName) + "'," +
                                         "EmissionPlaceAddress = '" + _defaultValue.GetDefaultValue<string>(wasteCustomerVo.EmissionPlaceAddress) + "'," +
                                         "UnitPriceFlammable = " + _defaultValue.GetDefaultValue<decimal>(wasteCustomerVo.UnitPriceFlammable) + "," +
                                         "UnitPriceCollection = " + _defaultValue.GetDefaultValue<decimal>(wasteCustomerVo.UnitPriceCollection) + "," +
                                         "UnitPriceDisposal = " + _defaultValue.GetDefaultValue<decimal>(wasteCustomerVo.UnitPriceDisposal) + "," +
                                         "UnitPriceResources = " + _defaultValue.GetDefaultValue<decimal>(wasteCustomerVo.UnitPriceResources) + "," +
                                         "UnitPriceTransportationCosts = " + _defaultValue.GetDefaultValue<decimal>(wasteCustomerVo.UnitPriceTransportationCosts) + "," +
                                         "UnitPriceManifestCosts = " + _defaultValue.GetDefaultValue<decimal>(wasteCustomerVo.UnitPriceManifestCosts) + "," +
                                         "UnitPriceOtherCosts = " + _defaultValue.GetDefaultValue<decimal>(wasteCustomerVo.UnitPriceOtherCosts) + "," +
                                         "UnitPriceBulkyTransportationCosts = " + _defaultValue.GetDefaultValue<decimal>(wasteCustomerVo.UnitPriceBulkyTransportationCosts) + "," +
                                         "UnitPriceBulkyDisposal = '" + _defaultValue.GetDefaultValue<decimal>(wasteCustomerVo.UnitPriceBulkyDisposal) + "'," +
                                         "Remarks = '" + _defaultValue.GetDefaultValue<string>(wasteCustomerVo.Remarks) + "'," +
                                         "UpdatePcName = '" + Environment.MachineName + "'," +
                                         "UpdateYmdHms = '" + DateTime.Now + "' " +
                                     "WHERE Id = " + wasteCustomerVo.Id + "";
            try {
                sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

    }
}
