/*
 * 2025-2-1
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class StaffWorkingHoursDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;
        private List<StaffWorkingHoursVo> _listStaffWorkingHoursVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public StaffWorkingHoursDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            _listStaffWorkingHoursVo = new();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationDate1"></param>
        /// <param name="operationDate2"></param>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public List<StaffWorkingHoursVo> SelectAllStaffWorkingHoursVo(DateTime operationDate1, DateTime operationDate2, int staffCode) {
            List<StaffWorkingHoursVo> listStaffWorkingHoursVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT H_VehicleDispatchDetail.OperationDate," +
                                            "H_SetMaster.SetName," +
                                            "CASE " +
                                                "WHEN H_VehicleDispatchDetail.StaffCode1 = " + staffCode + " THEN H_VehicleDispatchDetail.StaffRollCallYmdHms1 " +
                                                "WHEN H_VehicleDispatchDetail.StaffCode2 = " + staffCode + " THEN H_VehicleDispatchDetail.StaffRollCallYmdHms2 " +
                                                "WHEN H_VehicleDispatchDetail.StaffCode3 = " + staffCode + " THEN H_VehicleDispatchDetail.StaffRollCallYmdHms3 " +
                                                "WHEN H_VehicleDispatchDetail.StaffCode4 = " + staffCode + " THEN H_VehicleDispatchDetail.StaffRollCallYmdHms4 " +
                                            "END AS StaffRollCallYmdHms," +
                                            "H_VehicleDispatchDetail.LastRollCallYmdHms " +
                                     "FROM H_VehicleDispatchDetail " +
                                     "LEFT OUTER JOIN H_SetMaster ON H_VehicleDispatchDetail.SetCode = H_SetMaster.SetCode " +
                                     "WHERE OperationDate BETWEEN '" + operationDate1.ToString("yyyy-MM-dd") + "' AND '" + operationDate2.ToString("yyyy-MM-dd") + "' " +
                                       "AND (StaffCode1 = " + staffCode + " OR StaffCode2 = " + staffCode + " OR StaffCode3 = " + staffCode + " OR StaffCode4 = " + staffCode + ") " +
                                       "AND H_VehicleDispatchDetail.ClassificationCode IN(10,11,12,20)";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    StaffWorkingHoursVo staffWorkingHoursVo = new();
                    staffWorkingHoursVo.OperationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["OperationDate"]);
                    staffWorkingHoursVo.SetName = _defaultValue.GetDefaultValue<string>(sqlDataReader["SetName"]);
                    staffWorkingHoursVo.FirstRollCallTime = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StaffRollCallYmdHms"]);
                    staffWorkingHoursVo.LastRollCallTime = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["LastRollCallYmdHms"]);
                    listStaffWorkingHoursVo.Add(staffWorkingHoursVo);
                }
            }
            return listStaffWorkingHoursVo;
        }

    }
}
