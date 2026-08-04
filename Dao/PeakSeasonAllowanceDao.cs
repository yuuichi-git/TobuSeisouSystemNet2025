/*
 * 2026-07-23
 */
using System.Data;
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class PeakSeasonAllowanceDao {
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVO"></param>
        public PeakSeasonAllowanceDao(ConnectionVo connectionVO) {
            /*
             * Vo
             */
            _connectionVo = connectionVO;
        }

        /// <summary>
        /// 繁忙期割り増し費 対象者集計表用
        /// </summary>
        /// <param name="operationDate1"></param>
        /// <param name="operationDate2"></param>
        /// <returns></returns>
        public List<PeakSeasonAllowanceVo> SelectListPeakSeasonAllowanceVo(DateTime operationDate1, DateTime operationDate2) {
            List<PeakSeasonAllowanceVo> listPeakSeasonAllowanceVo = new ();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT VehicleDispatchDetail.StaffCode, " +
                                     "       H_StaffMaster.UnionCode, " +
                                     "       H_BelongsMaster.Name AS BelongsName, " +
                                     "       H_StaffMaster.DisplayName, " +
                                     "       COUNT(*) AS HitCount " +
                                     "FROM (SELECT StaffCode1 AS StaffCode, StaffOccupation1 AS StaffOccupation, OperationDate, CarCode, ClassificationCode " +     // StaffCode1
                                     "      FROM H_VehicleDispatchDetail " +
                                     "      WHERE OperationFlag = 1 AND VehicleDispatchFlag = 1 " +
                                     "        AND ClassificationCode IN (10, 11, 12, 30) " +                                                                        // 分類コード 10:雇上 11:区契 12:臨時 30:社内
                                     "        AND SetCode NOT IN (1312117) " +                                                                                      // 2026/8/3 家電を除くを追加
                                     "        AND DATEPART(WEEKDAY, OperationDate) <> 1 " +                                                                         // 2026/8/3 日曜日を除くを追加
                                     "UNION ALL " +
                                     "      SELECT StaffCode2 AS StaffCode, StaffOccupation2 AS StaffOccupation, OperationDate, CarCode, ClassificationCode " +     // StaffCode2
                                     "      FROM H_VehicleDispatchDetail " +
                                     "      WHERE OperationFlag = 1 AND VehicleDispatchFlag = 1 " +
                                     "        AND ClassificationCode IN (10, 11, 12, 30) " +                                                                        // 分類コード 10:雇上 11:区契 12:臨時 30:社内
                                     "        AND SetCode NOT IN (1312117) " +                                                                                      // 2026/8/3 家電を除くを追加
                                     "        AND DATEPART(WEEKDAY, OperationDate) <> 1 " +                                                                         // 2026/8/3 日曜日を除くを追加
                                     "UNION ALL " +
                                     "      SELECT StaffCode3 AS StaffCode, StaffOccupation3 AS StaffOccupation, OperationDate, CarCode, ClassificationCode " +     // StaffCode3
                                     "      FROM H_VehicleDispatchDetail " +
                                     "      WHERE OperationFlag = 1 AND VehicleDispatchFlag = 1 " +
                                     "        AND ClassificationCode IN (10, 11, 12, 30) " +                                                                        // 分類コード 10:雇上 11:区契 12:臨時 30:社内
                                     "        AND SetCode NOT IN (1312117) " +                                                                                      // 2026/8/3 家電を除くを追加
                                     "        AND DATEPART(WEEKDAY, OperationDate) <> 1 " +                                                                         // 2026/8/3 日曜日を除くを追加
                                     "UNION ALL " +
                                     "      SELECT StaffCode4 AS StaffCode, StaffOccupation4 AS StaffOccupation, OperationDate, CarCode, ClassificationCode " +     // StaffCode4
                                     "      FROM H_VehicleDispatchDetail " +
                                     "      WHERE OperationFlag = 1 AND VehicleDispatchFlag = 1 " +
                                     "        AND ClassificationCode IN (10, 11, 12, 30) " +                                                                        // 分類コード 10:雇上 11:区契 12:臨時 30:社内
                                     "        AND SetCode NOT IN (1312117) " +                                                                                      // 2026/8/3 家電を除くを追加
                                     "        AND DATEPART(WEEKDAY, OperationDate) <> 1) AS VehicleDispatchDetail " +                                               // 2026/8/3 日曜日を除くを追加
                                     "INNER JOIN H_StaffMaster ON VehicleDispatchDetail.StaffCode = H_StaffMaster.StaffCode " +
                                     "LEFT JOIN H_CarMaster ON VehicleDispatchDetail.CarCode = H_CarMaster.CarCode " +
                                     "LEFT JOIN H_BelongsMaster ON H_StaffMaster.Belongs = H_BelongsMaster.Code " +
                                     "WHERE VehicleDispatchDetail.OperationDate BETWEEN @OperationDate1 AND @OperationDate2 " +
                                     "  AND H_StaffMaster.Belongs IN (12, 22) " +
                                     "  AND H_StaffMaster.JobForm IN (20, 22, 99) " +
                                     "  AND (VehicleDispatchDetail.StaffOccupation = 11 OR (VehicleDispatchDetail.StaffOccupation = 10 AND (H_CarMaster.CarKindCode = 10 OR (H_CarMaster.CarKindCode = 11 AND H_CarMaster.ShapeCode = 10)))) " +
                                     "  AND DATENAME(WEEKDAY, VehicleDispatchDetail.OperationDate) <> 'Sunday' " +
                                     "GROUP BY VehicleDispatchDetail.StaffCode, H_StaffMaster.UnionCode, H_StaffMaster.DisplayName, H_BelongsMaster.Name " +
                                     "ORDER BY H_StaffMaster.UnionCode";

            sqlCommand.Parameters.Add("@OperationDate1", SqlDbType.Date).Value = operationDate1.Date;
            sqlCommand.Parameters.Add("@OperationDate2", SqlDbType.Date).Value = operationDate2.Date;

            using(SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while(sqlDataReader.Read()) {
                    PeakSeasonAllowanceVo peakSeasonAllowanceVo = new ();
                    peakSeasonAllowanceVo.UnionCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["UnionCode"]);
                    peakSeasonAllowanceVo.BelongsName = _defaultValue.GetDefaultValue<string>(sqlDataReader["BelongsName"]);
                    peakSeasonAllowanceVo.DisplayName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DisplayName"]);
                    peakSeasonAllowanceVo.CountDays = _defaultValue.GetDefaultValue<int>(sqlDataReader["HitCount"]);
                    listPeakSeasonAllowanceVo.Add(peakSeasonAllowanceVo);
                }
            }

            return listPeakSeasonAllowanceVo;
        }
    }

    /* 
     * ----------------------------------------
     * 
     * 内部クラス
     * 
     * ----------------------------------------
     */
    public class PeakSeasonAllowanceVo {
        private int _unionCode = 0;
        private string _belongsName = string.Empty;
        private string _displayName = string.Empty;
        private int _countDays = 0;
        /// <summary>
        /// 組合コード
        /// </summary>
        public int UnionCode {
            get {
                return _unionCode;
            }
            set {
                _unionCode = value;
            }
        }
        /// <summary>
        /// 職種
        /// </summary>
        public string BelongsName {
            get {
                return _belongsName;
            }
            set {
                _belongsName = value;
            }
        }
        /// <summary>
        /// 氏名
        /// </summary>
        public string DisplayName {
            get {
                return _displayName;
            }
            set {
                _displayName = value;
            }
        }
        /// <summary>
        /// 対象期間内の日数合計
        /// </summary>
        public int CountDays {
            get {
                return _countDays;
            }
            set {
                _countDays = value;
            }
        }
    }

}
