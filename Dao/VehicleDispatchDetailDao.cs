/*
 * 2023-12-31 
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class VehicleDispatchDetailDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

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
        /// true:該当レコードあり 
        /// false:該当レコードなし
        /// </summary>
        /// <param name="cellNumber"></param>
        /// <param name="operationDate"></param>
        /// <returns></returns>
        public bool ExistenceEmploymentAgreement(int cellNumber, DateTime operationDate) {
            int count;
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(CellNumber) " +
                                     "FROM H_VehicleDispatchDetail " +
                                     "WHERE CellNumber = " + cellNumber + " AND OperationDate = '" + operationDate.ToString("yyyy-MM-dd") + "'";
            try {
                count = (int)sqlCommand.ExecuteScalar();
            } catch {
                throw;
            }
            return count != 0 ? true : false;
        }

        /// <summary>
        /// true:該当レコードあり
        /// false:該当レコードなし
        /// </summary>
        /// <param name="operationDate"></param>
        /// <returns></returns>
        public bool ExistenceVehicleDispatchDetail(DateTime operationDate) {
            int count;
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(CellNumber) " +
                                     "FROM H_VehicleDispatchDetail " +
                                     "WHERE OperationDate = '" + operationDate.ToString("yyyy-MM-dd") + "'";
            try {
                count = (int)sqlCommand.ExecuteScalar();
            } catch {
                throw;
            }
            return count != 0 ? true : false;
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

        /// <summary>
        /// 指定日のデータを取得(期間)
        /// </summary>
        /// <param name="operationDate1"></param>
        /// <param name="operationDate2"></param>
        /// <returns></returns>
        public List<VehicleDispatchDetailVo> SelectAllVehicleDispatchDetail(DateTime operationDate1, DateTime operationDate2) {
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
                                     "WHERE OperationDate BETWEEN '" + operationDate1.ToString("yyyy-MM-dd") + "' AND '" + operationDate2.ToString("yyyy-MM-dd") + "'";
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellNumber"></param>
        /// <param name="operationDate"></param>
        /// <returns></returns>
        public VehicleDispatchDetailVo SelectOneVehicleDispatchDetail(int cellNumber, DateTime operationDate) {
            VehicleDispatchDetailVo vehicleDispatchDetailVo = new();
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
                                     "WHERE CellNumber = " + cellNumber + " AND OperationDate = '" + operationDate.ToString("yyyy-MM-dd") + "'";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    vehicleDispatchDetailVo = new();
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
                }
            }
            return vehicleDispatchDetailVo;
        }

        /// <summary>
        /// 運転手の出庫点呼日時を取得
        /// </summary>
        /// <param name="cellNumber"></param>
        /// <param name="operationDate"></param>
        /// <returns>StaffRollCallYmdHms1</returns>
        public DateTime GetStaffRollCallYmdHms1(int cellNumber, DateTime operationDate) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffRollCallYmdHms1 " +
                                     "FROM H_VehicleDispatchDetail " +
                                     "WHERE CellNumber = " + cellNumber + " AND OperationDate = '" + operationDate.ToString("yyyy-MM-dd") + "'";
            try {
                return (DateTime)sqlCommand.ExecuteScalar();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// InsertOneVehicleDispatchDetail
        /// </summary>
        /// <param name="vehicleDispatchDetailVo"></param>
        public int InsertOneVehicleDispatchDetail(VehicleDispatchDetailVo vehicleDispatchDetailVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_VehicleDispatchDetail(CellNumber," +
                                                                         "OperationDate," +
                                                                         "OperationFlag," +
                                                                         "VehicleDispatchFlag," +
                                                                         "PurposeFlag," +
                                                                         "SetCode," +
                                                                         "ManagedSpaceCode," +
                                                                         "ClassificationCode," +
                                                                         "LastRollCallFlag," +
                                                                         "LastRollCallYmdHms," +
                                                                         "SetMemoFlag," +
                                                                         "SetMemo," +
                                                                         "ShiftCode," +
                                                                         "StandByFlag," +
                                                                         "AddWorkerFlag," +
                                                                         "ContactInfomationFlag," +
                                                                         "FaxTransmissionFlag," +
                                                                         "CarCode," +
                                                                         "CarGarageCode," +
                                                                         "CarProxyFlag," +
                                                                         "CarMemoFlag," +
                                                                         "CarMemo," +
                                                                         "StaffCode1," +
                                                                         "StaffOccupation1," +
                                                                         "StaffProxyFlag1," +
                                                                         "StaffRollCallFlag1," +
                                                                         "StaffRollCallYmdHms1," +
                                                                         "StaffMemoFlag1," +
                                                                         "StaffMemo1," +
                                                                         "StaffCode2," +
                                                                         "StaffOccupation2," +
                                                                         "StaffProxyFlag2," +
                                                                         "StaffRollCallFlag2," +
                                                                         "StaffRollCallYmdHms2," +
                                                                         "StaffMemoFlag2," +
                                                                         "StaffMemo2," +
                                                                         "StaffCode3," +
                                                                         "StaffOccupation3," +
                                                                         "StaffProxyFlag3," +
                                                                         "StaffRollCallFlag3," +
                                                                         "StaffRollCallYmdHms3," +
                                                                         "StaffMemoFlag3," +
                                                                         "StaffMemo3," +
                                                                         "StaffCode4," +
                                                                         "StaffOccupation4," +
                                                                         "StaffProxyFlag4," +
                                                                         "StaffRollCallFlag4," +
                                                                         "StaffRollCallYmdHms4," +
                                                                         "StaffMemoFlag4," +
                                                                         "StaffMemo4," +
                                                                         "InsertPcName," +
                                                                         "InsertYmdHms," +
                                                                         "UpdatePcName," +
                                                                         "UpdateYmdHms," +
                                                                         "DeletePcName," +
                                                                         "DeleteYmdHms," +
                                                                         "DeleteFlag) " +
                                     "VALUES (" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.CellNumber) + ", " +
                                            "'" + _defaultValue.GetDefaultValue<DateTime>(vehicleDispatchDetailVo.OperationDate) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.OperationFlag) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.VehicleDispatchFlag) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.PurposeFlag) + "'," +
                                             "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.SetCode) + "," +
                                             "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.ManagedSpaceCode) + "," +
                                             "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.ClassificationCode) + "," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.LastRollCallFlag) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<DateTime>(vehicleDispatchDetailVo.LastRollCallYmdHms) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.SetMemoFlag) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<string>(vehicleDispatchDetailVo.SetMemo) + "'," +
                                             "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.ShiftCode) + "," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StandByFlag) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.AddWorkerFlag) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.ContactInfomationFlag) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.FaxTransmissionFlag) + "'," +
                                             "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.CarCode) + "," +
                                             "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.CarGarageCode) + "," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.CarProxyFlag) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.CarMemoFlag) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<string>(vehicleDispatchDetailVo.CarMemo) + "'," +
                                             "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.StaffCode1) + "," +
                                             "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.StaffOccupation1) + "," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffProxyFlag1) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffRollCallFlag1) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<DateTime>(vehicleDispatchDetailVo.StaffRollCallYmdHms1) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffMemoFlag1) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<string>(vehicleDispatchDetailVo.StaffMemo1) + "'," +
                                             "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.StaffCode2) + "," +
                                             "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.StaffOccupation2) + "," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffProxyFlag2) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffRollCallFlag2) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<DateTime>(vehicleDispatchDetailVo.StaffRollCallYmdHms2) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffMemoFlag2) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<string>(vehicleDispatchDetailVo.StaffMemo2) + "'," +
                                             "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.StaffCode3) + "," +
                                             "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.StaffOccupation3) + "," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffProxyFlag3) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffRollCallFlag3) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<DateTime>(vehicleDispatchDetailVo.StaffRollCallYmdHms3) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffMemoFlag3) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<string>(vehicleDispatchDetailVo.StaffMemo3) + "'," +
                                             "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.StaffCode4) + "," +
                                             "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.StaffOccupation4) + "," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffProxyFlag4) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffRollCallFlag4) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<DateTime>(vehicleDispatchDetailVo.StaffRollCallYmdHms4) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffMemoFlag4) + "'," +
                                            "'" + _defaultValue.GetDefaultValue<string>(vehicleDispatchDetailVo.StaffMemo4) + "'," +
                                            "'" + Environment.MachineName + "'," +
                                            "'" + DateTime.Now + "'," +
                                            "'" + string.Empty + "'," +
                                            "'" + _defaultDateTime + "'," +
                                            "'" + string.Empty + "'," +
                                            "'" + _defaultDateTime + "'," +
                                            "'false')";
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// InsertVehicleDispatchDetail
        /// </summary>
        /// <param name="listVehicleDispatchDetailVo"></param>
        public int InsertVehicleDispatchDetail(List<VehicleDispatchDetailVo> listVehicleDispatchDetailVo) {
            int count = 1;
            string sqlString = string.Empty;
            foreach (VehicleDispatchDetailVo vehicleDispatchDetailVo in listVehicleDispatchDetailVo) {
                sqlString += "(" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.CellNumber) + "," +
                             "'" + _defaultValue.GetDefaultValue<DateTime>(vehicleDispatchDetailVo.OperationDate) + "'," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.OperationFlag) + "'," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.VehicleDispatchFlag) + "'," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.PurposeFlag) + "'," +
                              "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.SetCode) + "," +
                              "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.ManagedSpaceCode) + "," +
                              "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.ClassificationCode) + "," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.LastRollCallFlag) + "'," +
                             "'" + _defaultValue.GetDefaultValue<DateTime>(vehicleDispatchDetailVo.LastRollCallYmdHms) + "'," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.SetMemoFlag) + "'," +
                             "'" + _defaultValue.GetDefaultValue<string>(vehicleDispatchDetailVo.SetMemo) + "'," +
                              "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.ShiftCode) + "," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StandByFlag) + "'," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.AddWorkerFlag) + "'," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.ContactInfomationFlag) + "'," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.FaxTransmissionFlag) + "'," +
                              "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.CarCode) + "," +
                              "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.CarGarageCode) + "," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.CarProxyFlag) + "'," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.CarMemoFlag) + "'," +
                             "'" + _defaultValue.GetDefaultValue<string>(vehicleDispatchDetailVo.CarMemo) + "'," +
                              "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.StaffCode1) + "," +
                              "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.StaffOccupation1) + "," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffProxyFlag1) + "'," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffRollCallFlag1) + "'," +
                             "'" + _defaultValue.GetDefaultValue<DateTime>(vehicleDispatchDetailVo.StaffRollCallYmdHms1) + "'," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffMemoFlag1) + "'," +
                             "'" + _defaultValue.GetDefaultValue<string>(vehicleDispatchDetailVo.StaffMemo1) + "'," +
                              "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.StaffCode2) + "," +
                              "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.StaffOccupation2) + "," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffProxyFlag2) + "'," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffRollCallFlag2) + "'," +
                             "'" + _defaultValue.GetDefaultValue<DateTime>(vehicleDispatchDetailVo.StaffRollCallYmdHms2) + "'," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffMemoFlag2) + "'," +
                             "'" + _defaultValue.GetDefaultValue<string>(vehicleDispatchDetailVo.StaffMemo2) + "'," +
                              "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.StaffCode3) + "," +
                              "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.StaffOccupation3) + "," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffProxyFlag3) + "'," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffRollCallFlag3) + "'," +
                             "'" + _defaultValue.GetDefaultValue<DateTime>(vehicleDispatchDetailVo.StaffRollCallYmdHms3) + "'," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffMemoFlag3) + "'," +
                             "'" + _defaultValue.GetDefaultValue<string>(vehicleDispatchDetailVo.StaffMemo3) + "'," +
                              "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.StaffCode4) + "," +
                              "" + _defaultValue.GetDefaultValue<int>(vehicleDispatchDetailVo.StaffOccupation4) + "," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffProxyFlag4) + "'," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffRollCallFlag4) + "'," +
                             "'" + _defaultValue.GetDefaultValue<DateTime>(vehicleDispatchDetailVo.StaffRollCallYmdHms4) + "'," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.StaffMemoFlag4) + "'," +
                             "'" + _defaultValue.GetDefaultValue<string>(vehicleDispatchDetailVo.StaffMemo4) + "'," +
                             "'" + _defaultValue.GetDefaultValue<string>(vehicleDispatchDetailVo.InsertPcName) + "'," +
                             "'" + _defaultValue.GetDefaultValue<DateTime>(vehicleDispatchDetailVo.InsertYmdHms) + "'," +
                             "'" + _defaultValue.GetDefaultValue<string>(vehicleDispatchDetailVo.UpdatePcName) + "'," +
                             "'" + _defaultValue.GetDefaultValue<DateTime>(vehicleDispatchDetailVo.UpdateYmdHms) + "'," +
                             "'" + _defaultValue.GetDefaultValue<string>(vehicleDispatchDetailVo.DeletePcName) + "'," +
                             "'" + _defaultValue.GetDefaultValue<DateTime>(vehicleDispatchDetailVo.DeleteYmdHms) + "'," +
                             "'" + _defaultValue.GetDefaultValue<bool>(vehicleDispatchDetailVo.DeleteFlag) + "')";
                if (count < listVehicleDispatchDetailVo.Count)
                    sqlString += ",";
                count++;
            }
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_VehicleDispatchDetail(CellNumber," +
                                                                         "OperationDate," +
                                                                         "OperationFlag," +
                                                                         "VehicleDispatchFlag," +
                                                                         "PurposeFlag," +
                                                                         "SetCode," +
                                                                         "ManagedSpaceCode," +
                                                                         "ClassificationCode," +
                                                                         "LastRollCallFlag," +
                                                                         "LastRollCallYmdHms," +
                                                                         "SetMemoFlag," +
                                                                         "SetMemo," +
                                                                         "ShiftCode," +
                                                                         "StandByFlag," +
                                                                         "AddWorkerFlag," +
                                                                         "ContactInfomationFlag," +
                                                                         "FaxTransmissionFlag," +
                                                                         "CarCode," +
                                                                         "CarGarageCode," +
                                                                         "CarProxyFlag," +
                                                                         "CarMemoFlag," +
                                                                         "CarMemo," +
                                                                         "StaffCode1," +
                                                                         "StaffOccupation1," +
                                                                         "StaffProxyFlag1," +
                                                                         "StaffRollCallFlag1," +
                                                                         "StaffRollCallYmdHms1," +
                                                                         "StaffMemoFlag1," +
                                                                         "StaffMemo1," +
                                                                         "StaffCode2," +
                                                                         "StaffOccupation2," +
                                                                         "StaffProxyFlag2," +
                                                                         "StaffRollCallFlag2," +
                                                                         "StaffRollCallYmdHms2," +
                                                                         "StaffMemoFlag2," +
                                                                         "StaffMemo2," +
                                                                         "StaffCode3," +
                                                                         "StaffOccupation3," +
                                                                         "StaffProxyFlag3," +
                                                                         "StaffRollCallFlag3," +
                                                                         "StaffRollCallYmdHms3," +
                                                                         "StaffMemoFlag3," +
                                                                         "StaffMemo3," +
                                                                         "StaffCode4," +
                                                                         "StaffOccupation4," +
                                                                         "StaffProxyFlag4," +
                                                                         "StaffRollCallFlag4," +
                                                                         "StaffRollCallYmdHms4," +
                                                                         "StaffMemoFlag4," +
                                                                         "StaffMemo4," +
                                                                         "InsertPcName," +
                                                                         "InsertYmdHms," +
                                                                         "UpdatePcName," +
                                                                         "UpdateYmdHms," +
                                                                         "DeletePcName," +
                                                                         "DeleteYmdHms," +
                                                                         "DeleteFlag) " +
                                     "VALUES " + sqlString;
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// UpdateOneVehicleDispatchDetail
        /// </summary>
        /// <param name="vehicleDispatchDetailVo"></param>
        public int UpdateOneVehicleDispatchDetail(VehicleDispatchDetailVo vehicleDispatchDetailVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_VehicleDispatchDetail " +
                                     "Set CellNumber = " + vehicleDispatchDetailVo.CellNumber + "," +
                                         "OperationDate = '" + vehicleDispatchDetailVo.OperationDate + "'," +
                                         "OperationFlag = '" + vehicleDispatchDetailVo.OperationFlag + "'," +
                                         "VehicleDispatchFlag = '" + vehicleDispatchDetailVo.VehicleDispatchFlag + "'," +
                                         "PurposeFlag = '" + vehicleDispatchDetailVo.PurposeFlag + "'," +
                                         "SetCode = " + vehicleDispatchDetailVo.SetCode + "," +
                                         "ManagedSpaceCode = " + vehicleDispatchDetailVo.ManagedSpaceCode + "," +
                                         "ClassificationCode = " + vehicleDispatchDetailVo.ClassificationCode + "," +
                                         "LastRollCallFlag = '" + vehicleDispatchDetailVo.LastRollCallFlag + "'," +
                                         "LastRollCallYmdHms = '" + vehicleDispatchDetailVo.LastRollCallYmdHms + "'," +
                                         "SetMemoFlag = '" + vehicleDispatchDetailVo.SetMemoFlag + "'," +
                                         "SetMemo = '" + vehicleDispatchDetailVo.SetMemo + "'," +
                                         "ShiftCode = " + vehicleDispatchDetailVo.ShiftCode + "," +
                                         "StandByFlag = '" + vehicleDispatchDetailVo.StandByFlag + "'," +
                                         "AddWorkerFlag = '" + vehicleDispatchDetailVo.AddWorkerFlag + "'," +
                                         "ContactInfomationFlag = '" + vehicleDispatchDetailVo.ContactInfomationFlag + "'," +
                                         "FaxTransmissionFlag = '" + vehicleDispatchDetailVo.FaxTransmissionFlag + "'," +
                                         "CarCode = " + vehicleDispatchDetailVo.CarCode + "," +
                                         "CarGarageCode = " + vehicleDispatchDetailVo.CarGarageCode + "," +
                                         "CarProxyFlag = '" + vehicleDispatchDetailVo.CarProxyFlag + "'," +
                                         "CarMemoFlag = '" + vehicleDispatchDetailVo.CarMemoFlag + "'," +
                                         "CarMemo = '" + vehicleDispatchDetailVo.CarMemo + "'," +
                                         "StaffCode1 = " + vehicleDispatchDetailVo.StaffCode1 + "," +
                                         "StaffOccupation1 = " + vehicleDispatchDetailVo.StaffOccupation1 + "," +
                                         "StaffProxyFlag1 = '" + vehicleDispatchDetailVo.StaffProxyFlag1 + "'," +
                                         "StaffRollCallFlag1 = '" + vehicleDispatchDetailVo.StaffRollCallFlag1 + "'," +
                                         "StaffRollCallYmdHms1 = '" + vehicleDispatchDetailVo.StaffRollCallYmdHms1 + "'," +
                                         "StaffMemoFlag1 = '" + vehicleDispatchDetailVo.StaffMemoFlag1 + "'," +
                                         "StaffMemo1 = '" + vehicleDispatchDetailVo.StaffMemo1 + "'," +
                                         "StaffCode2 = " + vehicleDispatchDetailVo.StaffCode2 + "," +
                                         "StaffOccupation2 = " + vehicleDispatchDetailVo.StaffOccupation2 + "," +
                                         "StaffProxyFlag2 = '" + vehicleDispatchDetailVo.StaffProxyFlag2 + "'," +
                                         "StaffRollCallFlag2 = '" + vehicleDispatchDetailVo.StaffRollCallFlag2 + "'," +
                                         "StaffRollCallYmdHms2 = '" + vehicleDispatchDetailVo.StaffRollCallYmdHms2 + "'," +
                                         "StaffMemoFlag2 = '" + vehicleDispatchDetailVo.StaffMemoFlag2 + "'," +
                                         "StaffMemo2 = '" + vehicleDispatchDetailVo.StaffMemo2 + "'," +
                                         "StaffCode3 = " + vehicleDispatchDetailVo.StaffCode3 + "," +
                                         "StaffOccupation3 = " + vehicleDispatchDetailVo.StaffOccupation3 + "," +
                                         "StaffProxyFlag3 = '" + vehicleDispatchDetailVo.StaffProxyFlag3 + "'," +
                                         "StaffRollCallFlag3 = '" + vehicleDispatchDetailVo.StaffRollCallFlag3 + "'," +
                                         "StaffRollCallYmdHms3 = '" + vehicleDispatchDetailVo.StaffRollCallYmdHms3 + "'," +
                                         "StaffMemoFlag3 = '" + vehicleDispatchDetailVo.StaffMemoFlag3 + "'," +
                                         "StaffMemo3 = '" + vehicleDispatchDetailVo.StaffMemo3 + "'," +
                                         "StaffCode4 = " + vehicleDispatchDetailVo.StaffCode4 + "," +
                                         "StaffOccupation4 = " + vehicleDispatchDetailVo.StaffOccupation4 + "," +
                                         "StaffProxyFlag4 = '" + vehicleDispatchDetailVo.StaffProxyFlag4 + "'," +
                                         "StaffRollCallFlag4 = '" + vehicleDispatchDetailVo.StaffRollCallFlag4 + "'," +
                                         "StaffRollCallYmdHms4 = '" + vehicleDispatchDetailVo.StaffRollCallYmdHms4 + "'," +
                                         "StaffMemoFlag4 = '" + vehicleDispatchDetailVo.StaffMemoFlag4 + "'," +
                                         "StaffMemo4 = '" + vehicleDispatchDetailVo.StaffMemo4 + "'," +
                                         "UpdatePcName = '" + Environment.MachineName + "'," +
                                         "UpdateYmdHms = '" + DateTime.Now + "' " +
                                     "WHERE CellNumber = " + vehicleDispatchDetailVo.CellNumber + " AND OperationDate = '" + vehicleDispatchDetailVo.OperationDate.ToString("yyyy-MM-dd") + "'";
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// SetControlの可変
        /// </summary>
        /// <param name="beforeCellNumber">Update対象のCellNumber</param>
        /// <param name="vehicleDispatchDetailVo">Update後のVehicleDispatchDetailVo</param>
        /// <returns></returns>
        public int UpdateOneVehicleDispatchDetail(int beforeCellNumber, VehicleDispatchDetailVo vehicleDispatchDetailVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_VehicleDispatchDetail " +
                                     "Set CellNumber = " + vehicleDispatchDetailVo.CellNumber + "," +
                                         "OperationDate = '" + vehicleDispatchDetailVo.OperationDate + "'," +
                                         "OperationFlag = '" + vehicleDispatchDetailVo.OperationFlag + "'," +
                                         "VehicleDispatchFlag = '" + vehicleDispatchDetailVo.VehicleDispatchFlag + "'," +
                                         "PurposeFlag = '" + vehicleDispatchDetailVo.PurposeFlag + "'," +
                                         "SetCode = " + vehicleDispatchDetailVo.SetCode + "," +
                                         "ManagedSpaceCode = " + vehicleDispatchDetailVo.ManagedSpaceCode + "," +
                                         "ClassificationCode = " + vehicleDispatchDetailVo.ClassificationCode + "," +
                                         "LastRollCallFlag = '" + vehicleDispatchDetailVo.LastRollCallFlag + "'," +
                                         "LastRollCallYmdHms = '" + vehicleDispatchDetailVo.LastRollCallYmdHms + "'," +
                                         "SetMemoFlag = '" + vehicleDispatchDetailVo.SetMemoFlag + "'," +
                                         "SetMemo = '" + vehicleDispatchDetailVo.SetMemo + "'," +
                                         "ShiftCode = " + vehicleDispatchDetailVo.ShiftCode + "," +
                                         "StandByFlag = '" + vehicleDispatchDetailVo.StandByFlag + "'," +
                                         "AddWorkerFlag = '" + vehicleDispatchDetailVo.AddWorkerFlag + "'," +
                                         "ContactInfomationFlag = '" + vehicleDispatchDetailVo.ContactInfomationFlag + "'," +
                                         "FaxTransmissionFlag = '" + vehicleDispatchDetailVo.FaxTransmissionFlag + "'," +
                                         "CarCode = " + vehicleDispatchDetailVo.CarCode + "," +
                                         "CarGarageCode = " + vehicleDispatchDetailVo.CarGarageCode + "," +
                                         "CarProxyFlag = '" + vehicleDispatchDetailVo.CarProxyFlag + "'," +
                                         "CarMemoFlag = '" + vehicleDispatchDetailVo.CarMemoFlag + "'," +
                                         "CarMemo = '" + vehicleDispatchDetailVo.CarMemo + "'," +
                                         "StaffCode1 = " + vehicleDispatchDetailVo.StaffCode1 + "," +
                                         "StaffOccupation1 = " + vehicleDispatchDetailVo.StaffOccupation1 + "," +
                                         "StaffProxyFlag1 = '" + vehicleDispatchDetailVo.StaffProxyFlag1 + "'," +
                                         "StaffRollCallFlag1 = '" + vehicleDispatchDetailVo.StaffRollCallFlag1 + "'," +
                                         "StaffRollCallYmdHms1 = '" + vehicleDispatchDetailVo.StaffRollCallYmdHms1 + "'," +
                                         "StaffMemoFlag1 = '" + vehicleDispatchDetailVo.StaffMemoFlag1 + "'," +
                                         "StaffMemo1 = '" + vehicleDispatchDetailVo.StaffMemo1 + "'," +
                                         "StaffCode2 = " + vehicleDispatchDetailVo.StaffCode2 + "," +
                                         "StaffOccupation2 = " + vehicleDispatchDetailVo.StaffOccupation2 + "," +
                                         "StaffProxyFlag2 = '" + vehicleDispatchDetailVo.StaffProxyFlag2 + "'," +
                                         "StaffRollCallFlag2 = '" + vehicleDispatchDetailVo.StaffRollCallFlag2 + "'," +
                                         "StaffRollCallYmdHms2 = '" + vehicleDispatchDetailVo.StaffRollCallYmdHms2 + "'," +
                                         "StaffMemoFlag2 = '" + vehicleDispatchDetailVo.StaffMemoFlag2 + "'," +
                                         "StaffMemo2 = '" + vehicleDispatchDetailVo.StaffMemo2 + "'," +
                                         "StaffCode3 = " + vehicleDispatchDetailVo.StaffCode3 + "," +
                                         "StaffOccupation3 = " + vehicleDispatchDetailVo.StaffOccupation3 + "," +
                                         "StaffProxyFlag3 = '" + vehicleDispatchDetailVo.StaffProxyFlag3 + "'," +
                                         "StaffRollCallFlag3 = '" + vehicleDispatchDetailVo.StaffRollCallFlag3 + "'," +
                                         "StaffRollCallYmdHms3 = '" + vehicleDispatchDetailVo.StaffRollCallYmdHms3 + "'," +
                                         "StaffMemoFlag3 = '" + vehicleDispatchDetailVo.StaffMemoFlag3 + "'," +
                                         "StaffMemo3 = '" + vehicleDispatchDetailVo.StaffMemo3 + "'," +
                                         "StaffCode4 = " + vehicleDispatchDetailVo.StaffCode4 + "," +
                                         "StaffOccupation4 = " + vehicleDispatchDetailVo.StaffOccupation4 + "," +
                                         "StaffProxyFlag4 = '" + vehicleDispatchDetailVo.StaffProxyFlag4 + "'," +
                                         "StaffRollCallFlag4 = '" + vehicleDispatchDetailVo.StaffRollCallFlag4 + "'," +
                                         "StaffRollCallYmdHms4 = '" + vehicleDispatchDetailVo.StaffRollCallYmdHms4 + "'," +
                                         "StaffMemoFlag4 = '" + vehicleDispatchDetailVo.StaffMemoFlag4 + "'," +
                                         "StaffMemo4 = '" + vehicleDispatchDetailVo.StaffMemo4 + "'," +
                                         "UpdatePcName = '" + Environment.MachineName + "'," +
                                         "UpdateYmdHms = '" + DateTime.Now + "' " +
                                     "WHERE CellNumber = " + beforeCellNumber + " AND OperationDate = '" + vehicleDispatchDetailVo.OperationDate.ToString("yyyy-MM-dd") + "'";
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lastRollCallFlag"></param>
        /// <param name="cellNumber"></param>
        /// <param name="lastRollCallVo"></param>
        /// <returns></returns>
        public int UpdateOneLastRollCall(bool lastRollCallFlag, int cellNumber, LastRollCallVo lastRollCallVo) {
            /*
             * DB更新
             */
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_VehicleDispatchDetail " +
                                     "SET StaffRollCallFlag1 = 'true'," +
                                         "StaffRollCallYmdHms1 = '" + lastRollCallVo.FirstRollCallYmdHms + "'," +
                                         "LastRollCallFlag = '" + lastRollCallFlag + "'," +
                                         "LastRollCallYmdHms = '" + lastRollCallVo.LastRollCallYmdHms + "'," +
                                         "UpdatePcName = '" + Environment.MachineName + "'," +
                                         "UpdateYmdHms = '" + DateTime.Now + "' " +
                                     "WHERE CellNumber = " + cellNumber + " AND OperationDate = '" + lastRollCallVo.OperationDate.ToString("yyyy-MM-dd") + "'";
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellNumber"></param>
        /// <param name="lastRollCallVo"></param>
        /// <returns></returns>
        public int DeleteOneLastRollCall(int cellNumber, LastRollCallVo lastRollCallVo) {
            /*
             * DB更新
             */
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_VehicleDispatchDetail " +
                                     "SET LastRollCallFlag = 'false'," +
                                         "LastRollCallYmdHms = '" + _defaultDateTime + "'," +
                                         "UpdatePcName = '" + Environment.MachineName + "'," +
                                         "UpdateYmdHms = '" + DateTime.Now + "' " +
                                     "WHERE CellNumber = " + cellNumber + " AND OperationDate = '" + lastRollCallVo.OperationDate.ToString("yyyy-MM-dd") + "'";
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 1行削除
        /// </summary>
        /// <param name="cellNumber"></param>
        /// <param name="operationDate"></param>
        /// <returns></returns>
        public int DeleteOneVehicleDispatchDetail(int cellNumber, DateTime operationDate) {
            /*
             * DB更新
             */
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM H_VehicleDispatchDetail " +
                                     "WHERE CellNumber = " + cellNumber + " " +
                                       "AND OperationDate = '" + operationDate.ToString("yyyy-MM-dd") + "' " +
                                       "AND PurposeFlag = 'false' " +
                                       "AND SetCode = 0 " +
                                       "AND CarCode = 0 " +
                                       "AND StaffCode1 = 0 " +
                                       "AND StaffCode2 = 0";
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationDate"></param>
        public void DeleteVehicleDispatchDetail(DateTime operationDate) {
            var sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM H_VehicleDispatchDetail " +
                                     "WHERE OperationDate = '" + operationDate.ToString("yyyy-MM-dd") + "'";
            try {
                sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// H_VehicleDispatchHead/H_VehicleDispatchBodyからVehicleDispatchDetailVoを作成する
        /// </summary>
        /// <param name="financialYear"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public List<VehicleDispatchDetailVo> SelectVehicleDispatchDetailVo(int financialYear, string dayOfWeek) {
            List<VehicleDispatchDetailVo> listVehicleDispatchDetailVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT H_VehicleDispatchHead.CellNumber," +
                                            "H_VehicleDispatchHead.VehicleDispatchFlag," +
                                            "H_VehicleDispatchHead.Purpose," +
                                            "H_VehicleDispatchHead.SetCode," +
                                            "H_VehicleDispatchBody.CarCode," +
                                            "H_VehicleDispatchBody.StaffCode1," +
                                            "H_VehicleDispatchBody.StaffCode2," +
                                            "H_VehicleDispatchBody.StaffCode3," +
                                            "H_VehicleDispatchBody.StaffCode4 " +
                                     "FROM H_VehicleDispatchHead " +
                                     "LEFT OUTER JOIN H_VehicleDispatchBody ON H_VehicleDispatchHead.SetCode = H_VehicleDispatchBody.SetCode " +
                                                                          "AND H_VehicleDispatchHead.FinancialYear = H_VehicleDispatchBody.FinancialYear " +
                                     "WHERE H_VehicleDispatchHead.FinancialYear = " + financialYear + " " +
                                       "AND H_VehicleDispatchBody.DayOfWeek = '" + dayOfWeek + "'";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    VehicleDispatchDetailVo vehicleDispatchDetailVo = new();
                    vehicleDispatchDetailVo.CellNumber = _defaultValue.GetDefaultValue<int>(sqlDataReader["CellNumber"]);
                    vehicleDispatchDetailVo.VehicleDispatchFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["VehicleDispatchFlag"]);
                    vehicleDispatchDetailVo.PurposeFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["Purpose"]);
                    vehicleDispatchDetailVo.SetCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["SetCode"]);
                    vehicleDispatchDetailVo.CarCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["CarCode"]);
                    vehicleDispatchDetailVo.StaffCode1 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode1"]);
                    vehicleDispatchDetailVo.StaffCode2 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode2"]);
                    vehicleDispatchDetailVo.StaffCode3 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode3"]);
                    vehicleDispatchDetailVo.StaffCode4 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode4"]);
                    listVehicleDispatchDetailVo.Add(vehicleDispatchDetailVo);
                }
            }
            return listVehicleDispatchDetailVo;
        }
    }
}
