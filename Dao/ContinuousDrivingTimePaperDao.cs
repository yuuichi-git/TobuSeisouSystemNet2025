/*
 * 2026-05-08
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class ContinuousDrivingTimePaperDao {
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public ContinuousDrivingTimePaperDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationStartDate"></param>
        /// <param name="operationEndDate"></param>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public List<ContinuousDrivingTimePaperVo> SelectContinuousDrivingTimePaperVo(DateTime operationStartDate, DateTime operationEndDate, int staffCode) {
            List<ContinuousDrivingTimePaperVo> listContinuousDrivingTimePaperVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText =
                "SELECT H_VehicleDispatchDetail.OperationDate," +
                "       H_StaffMaster.DisplayName AS StaffDisplayName," +
                "       H_JobFormMaster.Name AS JobFormName," +
                "       H_SetMaster.SetName," +
                "       H_CarMaster.RegistrationNumber," +
                "       H_LastRollCall.FirstRollCallYmdHms," +
                "       H_LastRollCall.LastRollCallYmdHms," +
                "       H_LastRollCall.ContinuousDrivingTime," +
                "       H_VehicleDispatchDetail.StaffMemo1 AS Remarks " +
                "FROM H_VehicleDispatchDetail " +
                "LEFT OUTER JOIN H_SetMaster ON H_SetMaster.SetCode = H_VehicleDispatchDetail.SetCode " +
                "LEFT OUTER JOIN H_CarMaster ON H_CarMaster.CarCode = H_VehicleDispatchDetail.CarCode " +
                "LEFT OUTER JOIN H_StaffMaster ON H_StaffMaster.StaffCode = H_VehicleDispatchDetail.StaffCode1 " +
                "LEFT OUTER JOIN H_JobFormMaster ON H_JobFormMaster.Code = H_StaffMaster.JobForm " +
                "LEFT OUTER JOIN H_LastRollCall ON H_LastRollCall.OperationDate = H_VehicleDispatchDetail.OperationDate AND H_LastRollCall.SetCode = H_VehicleDispatchDetail.SetCode " +
                "WHERE H_VehicleDispatchDetail.OperationDate BETWEEN @OperationStartDate AND @OperationEndDate " +
                "  AND H_VehicleDispatchDetail.StaffCode1 = @StaffCode " +
                "  AND H_VehicleDispatchDetail.VehicleDispatchFlag = 'true' " +
                "ORDER BY H_VehicleDispatchDetail.OperationDate";

            sqlCommand.Parameters.Add(new SqlParameter("@OperationStartDate", operationStartDate));
            sqlCommand.Parameters.Add(new SqlParameter("@OperationEndDate", operationEndDate));
            sqlCommand.Parameters.Add(new SqlParameter("@StaffCode", staffCode));

            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    ContinuousDrivingTimePaperVo continuousDrivingTimePaperVo = new();
                    // 運行日
                    continuousDrivingTimePaperVo.OperationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["OperationDate"]);
                    // 運転者氏名
                    continuousDrivingTimePaperVo.StaffDisplayName = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffDisplayName"]);
                    // 職務形態
                    continuousDrivingTimePaperVo.JobForm = _defaultValue.GetDefaultValue<string>(sqlDataReader["JobFormName"]);
                    // 配車先
                    continuousDrivingTimePaperVo.SetName = _defaultValue.GetDefaultValue<string>(sqlDataReader["SetName"]);
                    // 車両の車両登録番号
                    string carRegistrationNumberRaw = _defaultValue.GetDefaultValue<string>(sqlDataReader["RegistrationNumber"]);
                    string carRegistrationNumber = String.IsNullOrEmpty(carRegistrationNumberRaw) ? "----" : carRegistrationNumberRaw;
                    continuousDrivingTimePaperVo.CarRegistrationNumber = carRegistrationNumber;
                    // 始業点呼時刻
                    string firstRollCallRaw = _defaultValue.GetDefaultValue<string>(sqlDataReader["FirstRollCallYmdHms"]);
                    string firstRollCall;
                    if (String.IsNullOrEmpty(firstRollCallRaw)) {                                       // NULL の場合は 00:00
                        firstRollCall = "00:00";
                    } else {
                        // 値がある場合は DateTime に変換して HH:mm に整形
                        DateTime dt;
                        if (DateTime.TryParse(firstRollCallRaw, out dt)) {
                            firstRollCall = dt.ToString("HH:mm");
                        } else {
                            firstRollCall = "00:00";                                                    // パースできない異常値の場合も 00:00 にフォールバック
                        }
                    }
                    continuousDrivingTimePaperVo.FirstLollCallHms = firstRollCall;
                    // 終業点呼時刻
                    string lastRollCallRaw = _defaultValue.GetDefaultValue<string>(sqlDataReader["LastRollCallYmdHms"]);
                    string lastRollCall;
                    if (String.IsNullOrEmpty(lastRollCallRaw)) {                                        // NULL の場合は 00:00
                        lastRollCall = "00:00";
                    } else {
                        // 値がある場合は DateTime に変換して HH:mm に整形
                        DateTime dt;
                        if (DateTime.TryParse(lastRollCallRaw, out dt)) {
                            lastRollCall = dt.ToString("HH:mm");
                        } else {
                            lastRollCall = "00:00";                                                     // パースできない異常値の場合も 00:00 にフォールバック
                        }
                    }
                    continuousDrivingTimePaperVo.LastLollCallHms = lastRollCall;
                    // 継続運転時間
                    continuousDrivingTimePaperVo.ContinuosDrivingTime = _defaultValue.GetDefaultValue<TimeSpan>(sqlDataReader["ContinuousDrivingTime"]);
                    // 備考
                    continuousDrivingTimePaperVo.Remarks = _defaultValue.GetDefaultValue<string>(sqlDataReader["Remarks"]);
                    listContinuousDrivingTimePaperVo.Add(continuousDrivingTimePaperVo);
                }
            }
            return listContinuousDrivingTimePaperVo;
        }

    }
}
