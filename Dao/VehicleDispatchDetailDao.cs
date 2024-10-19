/*
 * 2023-12-31 
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class VehicleDispatchDetailDao {
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();

        /// <summary>
        /// コンストラクター
        /// </summary>
        public VehicleDispatchDetailDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// 指定日のデータを取得
        /// </summary>
        /// <param name="operationDate">配車日</param>
        /// <returns></returns>
        public List<VehicleDispatchDetailVo> SelectAllVehicleDispatchDetail(DateTime operationDate) {
            List<VehicleDispatchDetailVo> listVehicleDispatchDetailVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT H_VehicleDispatchDetail.CellNumber," +
                                            "H_VehicleDispatchDetail.OperationDate," +
                                            "H_VehicleDispatchDetail.OperationFlag," +
                                            "H_VehicleDispatchDetail.VehicleDispatchFlag," +
                                            "H_VehicleDispatchDetail.PurposeFlag," +
                                            "H_VehicleDispatchDetail.SetCode," +
                                            "H_VehicleDispatchDetail.ManagedSpaceCode," +
                                            "H_VehicleDispatchDetail.ClassificationCode," +
                                            "H_VehicleDispatchDetail.LastRollCallFlag," +
                                            "H_VehicleDispatchDetail.LastRollCallYmdHms," +
                                            "H_VehicleDispatchDetail.SetMemoFlag," +
                                            "H_VehicleDispatchDetail.SetMemo," +
                                            "H_VehicleDispatchDetail.ShiftCode," +
                                            "H_VehicleDispatchDetail.StandByFlag," +
                                            "H_VehicleDispatchDetail.AddWorkerFlag," +
                                            "H_VehicleDispatchDetail.ContactInfomationFlag," +
                                            "H_VehicleDispatchDetail.FaxTransmissionFlag," +
                                            "H_VehicleDispatchDetail.CarCode," +
                                            "H_VehicleDispatchDetail.CarGarageCode," +
                                            "H_VehicleDispatchDetail.CarProxyFlag," +
                                            "H_VehicleDispatchDetail.CarMemoFlag," +
                                            "H_VehicleDispatchDetail.CarMemo," +
                                            "H_VehicleDispatchDetail.StaffCode1," +
                                            "H_VehicleDispatchDetail.StaffOccupation1," +
                                            "H_VehicleDispatchDetail.StaffProxyFlag1," +
                                            "H_VehicleDispatchDetail.StaffRollCallFlag1," +
                                            "H_VehicleDispatchDetail.StaffRollCallYmdHms1," +
                                            "H_VehicleDispatchDetail.StaffMemoFlag1," +
                                            "H_VehicleDispatchDetail.StaffMemo1," +
                                            "H_VehicleDispatchDetail.StaffCode2," +
                                            "H_VehicleDispatchDetail.StaffOccupation2," +
                                            "H_VehicleDispatchDetail.StaffProxyFlag2," +
                                            "H_VehicleDispatchDetail.StaffRollCallFlag2," +
                                            "H_VehicleDispatchDetail.StaffRollCallYmdHms2," +
                                            "H_VehicleDispatchDetail.StaffMemoFlag2," +
                                            "H_VehicleDispatchDetail.StaffMemo2," +
                                            "H_VehicleDispatchDetail.StaffCode3," +
                                            "H_VehicleDispatchDetail.StaffOccupation3," +
                                            "H_VehicleDispatchDetail.StaffProxyFlag3," +
                                            "H_VehicleDispatchDetail.StaffRollCallFlag3," +
                                            "H_VehicleDispatchDetail.StaffRollCallYmdHms3," +
                                            "H_VehicleDispatchDetail.StaffMemoFlag3," +
                                            "H_VehicleDispatchDetail.StaffMemo3," +
                                            "H_VehicleDispatchDetail.StaffCode4," +
                                            "H_VehicleDispatchDetail.StaffOccupation4," +
                                            "H_VehicleDispatchDetail.StaffProxyFlag4," +
                                            "H_VehicleDispatchDetail.StaffRollCallFlag4," +
                                            "H_VehicleDispatchDetail.StaffRollCallYmdHms4," +
                                            "H_VehicleDispatchDetail.StaffMemoFlag4," +
                                            "H_VehicleDispatchDetail.StaffMemo4," +
                                            "H_VehicleDispatchDetail.InsertPcName," +
                                            "H_VehicleDispatchDetail.InsertYmdHms," +
                                            "H_VehicleDispatchDetail.UpdatePcName," +
                                            "H_VehicleDispatchDetail.UpdateYmdHms," +
                                            "H_VehicleDispatchDetail.DeletePcName," +
                                            "H_VehicleDispatchDetail.DeleteYmdHms," +
                                            "H_VehicleDispatchDetail.DeleteFlag " +
                                     "FROM H_VehicleDispatchDetail " +
                                     "WHERE H_VehicleDispatchDetail.OperationDate = '" + operationDate.ToString("yyyy-MM-dd") + "'";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    VehicleDispatchDetailVo vehicleDispatchDetailVo = new();
                    vehicleDispatchDetailVo.CellNumber = _defaultValue.GetDefaultValue<int>(sqlDataReader["CellNumber"]);
                    vehicleDispatchDetailVo.OperationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["OperationDate"]);
                    vehicleDispatchDetailVo.OperationFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["OperationFlag"]);
                    vehicleDispatchDetailVo.VehicleDispatchFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["VehicleDispatchFlag"]);
                    vehicleDispatchDetailVo.PurposeFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["PurposeFlag"]);
                    vehicleDispatchDetailVo.SetCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["SetCode"]);
                    vehicleDispatchDetailVo.ManagedSpaceCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["ManagedSpaceCode"]);
                    vehicleDispatchDetailVo.ClassificationCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["ClassificationCode"]);
                    vehicleDispatchDetailVo.LastRollCallFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["LastRollCallFlag"]);
                    vehicleDispatchDetailVo.LastRollCallYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["LastRollCallYmdHms"]);
                    vehicleDispatchDetailVo.SetMemoFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["SetMemoFlag"]);
                    vehicleDispatchDetailVo.SetMemo = _defaultValue.GetDefaultValue<string>(sqlDataReader["SetMemo"]);
                    vehicleDispatchDetailVo.ShiftCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["ShiftCode"]);
                    vehicleDispatchDetailVo.StandByFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StandByFlag"]);
                    vehicleDispatchDetailVo.AddWorkerFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["AddWorkerFlag"]);
                    vehicleDispatchDetailVo.ContactInfomationFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["ContactInfomationFlag"]);
                    vehicleDispatchDetailVo.FaxTransmissionFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["FaxTransmissionFlag"]);
                    vehicleDispatchDetailVo.CarCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["CarCode"]);
                    vehicleDispatchDetailVo.CarGarageCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["CarGarageCode"]);
                    vehicleDispatchDetailVo.CarProxyFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["CarProxyFlag"]);
                    vehicleDispatchDetailVo.CarMemoFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["CarMemoFlag"]);
                    vehicleDispatchDetailVo.CarMemo = _defaultValue.GetDefaultValue<string>(sqlDataReader["CarMemo"]);
                    vehicleDispatchDetailVo.StaffCode1 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode1"]);
                    vehicleDispatchDetailVo.StaffOccupation1 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffOccupation1"]);
                    vehicleDispatchDetailVo.StaffProxyFlag1 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffProxyFlag1"]);
                    vehicleDispatchDetailVo.StaffRollCallFlag1 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffRollCallFlag1"]);
                    vehicleDispatchDetailVo.StaffRollCallYmdHms1 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StaffRollCallYmdHms1"]);
                    vehicleDispatchDetailVo.StaffMemoFlag1 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffMemoFlag1"]);
                    vehicleDispatchDetailVo.StaffMemo1 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffMemo1"]);
                    vehicleDispatchDetailVo.StaffCode2 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode2"]);
                    vehicleDispatchDetailVo.StaffOccupation2 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffOccupation2"]);
                    vehicleDispatchDetailVo.StaffProxyFlag2 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffProxyFlag2"]);
                    vehicleDispatchDetailVo.StaffRollCallFlag2 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffRollCallFlag2"]);
                    vehicleDispatchDetailVo.StaffRollCallYmdHms2 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StaffRollCallYmdHms2"]);
                    vehicleDispatchDetailVo.StaffMemoFlag2 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffMemoFlag2"]);
                    vehicleDispatchDetailVo.StaffMemo2 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffMemo2"]);
                    vehicleDispatchDetailVo.StaffCode3 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode3"]);
                    vehicleDispatchDetailVo.StaffOccupation3 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffOccupation3"]);
                    vehicleDispatchDetailVo.StaffProxyFlag3 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffProxyFlag3"]);
                    vehicleDispatchDetailVo.StaffRollCallFlag3 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffRollCallFlag3"]);
                    vehicleDispatchDetailVo.StaffRollCallYmdHms3 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StaffRollCallYmdHms3"]);
                    vehicleDispatchDetailVo.StaffMemoFlag3 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffMemoFlag3"]);
                    vehicleDispatchDetailVo.StaffMemo3 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffMemo3"]);
                    vehicleDispatchDetailVo.StaffCode4 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode4"]);
                    vehicleDispatchDetailVo.StaffOccupation4 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffOccupation4"]);
                    vehicleDispatchDetailVo.StaffProxyFlag4 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffProxyFlag4"]);
                    vehicleDispatchDetailVo.StaffRollCallFlag4 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffRollCallFlag4"]);
                    vehicleDispatchDetailVo.StaffRollCallYmdHms4 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StaffRollCallYmdHms4"]);
                    vehicleDispatchDetailVo.StaffMemoFlag4 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffMemoFlag4"]);
                    vehicleDispatchDetailVo.StaffMemo4 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffMemo4"]);
                    vehicleDispatchDetailVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    vehicleDispatchDetailVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    vehicleDispatchDetailVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    vehicleDispatchDetailVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    vehicleDispatchDetailVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    vehicleDispatchDetailVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    vehicleDispatchDetailVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listVehicleDispatchDetailVo.Add(vehicleDispatchDetailVo);
                }
            }
            return listVehicleDispatchDetailVo;
        }
    }
}
