/*
 * 2023-11-08
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class VehicleDispatchHeadDao {
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
        public VehicleDispatchHeadDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// SelectAllVehicleDispatchHeadVo
        /// 対象年度のレコードを抽出する
        /// </summary>
        /// <param name="financialYear"></param>
        /// <returns></returns>
        public List<VehicleDispatchHeadVo> SelectAllVehicleDispatchHeadVo(int financialYear) {
            List<VehicleDispatchHeadVo> listVehicleDispatchHeadVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT CellNumber," +
                                            "VehicleDispatchFlag," +
                                            "Purpose," +
                                            "SetCode," +
                                            "FinancialYear," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_VehicleDispatchHead " +
                                     "WHERE FinancialYear = " + financialYear + "";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    VehicleDispatchHeadVo vehicleDispatchHeadVo = new();
                    vehicleDispatchHeadVo.CellNumber = _defaultValue.GetDefaultValue<int>(sqlDataReader["CellNumber"]);
                    vehicleDispatchHeadVo.VehicleDispatchFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["VehicleDispatchFlag"]);
                    vehicleDispatchHeadVo.Purpose = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Purpose"]);
                    vehicleDispatchHeadVo.SetCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["SetCode"]);
                    vehicleDispatchHeadVo.FinancialYear = _defaultValue.GetDefaultValue<int>(sqlDataReader["FinancialYear"]);
                    vehicleDispatchHeadVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    vehicleDispatchHeadVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    vehicleDispatchHeadVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    vehicleDispatchHeadVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    vehicleDispatchHeadVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    vehicleDispatchHeadVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    vehicleDispatchHeadVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listVehicleDispatchHeadVo.Add(vehicleDispatchHeadVo);
                }
            }
            return listVehicleDispatchHeadVo;
        }
    }
}
