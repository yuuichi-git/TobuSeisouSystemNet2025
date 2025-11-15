/*
 * 2025-11-11
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class CarWorkingDaysDao {
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public CarWorkingDaysDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// 指定日と車両をキーとしたデータを取得(期間)
        /// </summary>
        /// <param name="operationDate1"></param>
        /// <param name="operationDate2"></param>
        /// <returns></returns>
        public List<CarWorkingDaysVo> SelectCarWorkingDaysVo(DateTime operationDate1, DateTime operationDate2, int carCode) {
            List<CarWorkingDaysVo> listCarWorkingDaysVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT H_VehicleDispatchDetail.OperationDate," +
                                            "H_VehicleDispatchDetail.SetCode," +
                                            "H_SetMaster.SetName," +
                                            "H_VehicleDispatchDetail.CarCode," +
                                            "H_CarMaster.RegistrationNumber," +
                                            "H_CarMaster.DoorNumber," +
                                            "H_VehicleDispatchDetail.ClassificationCode," +
                                            "H_ClassificationMaster.Name AS ClassificationName," +
                                            "H_CarMaster.DisguiseKind2," +
                                            "H_VehicleDispatchDetail.StaffCode1," +
                                            "H_StaffMaster.DisplayName," +
                                            "H_VehicleDispatchDetail.StaffMemo1 " +
                                     "FROM H_VehicleDispatchDetail " +
                                     "LEFT OUTER JOIN H_SetMaster ON H_VehicleDispatchDetail.SetCode = H_SetMaster.SetCode " +
                                     "LEFT OUTER JOIN H_CarMaster ON H_VehicleDispatchDetail.CarCode = H_CarMaster.CarCode " +
                                     "LEFT OUTER JOIN H_StaffMaster ON H_VehicleDispatchDetail.StaffCode1 = H_StaffMaster.StaffCode " +
                                     "LEFT OUTER JOIN H_ClassificationMaster ON H_VehicleDispatchDetail.ClassificationCode = H_ClassificationMaster.Code " +
                                     "WHERE H_VehicleDispatchDetail.OperationDate BETWEEN '" + operationDate1.ToString("yyyy-MM-dd") + "' AND '" + operationDate2.ToString("yyyy-MM-dd") + "' " +
                                       "AND H_VehicleDispatchDetail.CarCode = " + carCode + " " +
                                       "AND H_VehicleDispatchDetail.OperationFlag = 'true' " +
                                       "AND H_VehicleDispatchDetail.VehicleDispatchFlag = 'true'";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    CarWorkingDaysVo carWorkingDaysVo = new();
                    carWorkingDaysVo.OperationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["OperationDate"]);
                    carWorkingDaysVo.SetCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["SetCode"]);
                    carWorkingDaysVo.SetName = _defaultValue.GetDefaultValue<string>(sqlDataReader["SetName"]);
                    carWorkingDaysVo.CarCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["CarCode"]);
                    carWorkingDaysVo.RegistrationNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["RegistrationNumber"]);
                    carWorkingDaysVo.DoorNumber = _defaultValue.GetDefaultValue<int>(sqlDataReader["DoorNumber"]).ToString();
                    carWorkingDaysVo.ClassificationCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["ClassificationCode"]);
                    carWorkingDaysVo.ClassificationName = _defaultValue.GetDefaultValue<string>(sqlDataReader["ClassificationName"]);
                    carWorkingDaysVo.DisguiseKind2 = _defaultValue.GetDefaultValue<string>(sqlDataReader["DisguiseKind2"]);
                    carWorkingDaysVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode1"]);
                    carWorkingDaysVo.StaffName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DisplayName"]);
                    carWorkingDaysVo.Remarks = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffMemo1"]);

                    listCarWorkingDaysVo.Add(carWorkingDaysVo);
                }
            }
            return listCarWorkingDaysVo;
        }
    }
}
