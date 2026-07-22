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
            sqlCommand.CommandText =
                "SELECT VehicleDispatchDetail.StaffCode, " +
                "       H_StaffMaster.UnionCode, " +
                "       H_BelongsMaster.Name AS BelongsName, " +
                "       H_StaffMaster.DisplayName, " +
                "       COUNT(*) AS HitCount " +
                "FROM (SELECT StaffCode1 AS StaffCode, StaffOccupation1 AS StaffOccupation, OperationDate, CarCode, ClassificationCode " +
                "      FROM H_VehicleDispatchDetail " +
                "      WHERE OperationFlag = 1 AND VehicleDispatchFlag = 1 AND (ClassificationCode = 10 OR ClassificationCode = 11 OR ClassificationCode = 12 OR ClassificationCode = 30) " +
                "UNION ALL " +
                "      SELECT StaffCode2 AS StaffCode, StaffOccupation2 AS StaffOccupation, OperationDate, CarCode, ClassificationCode " +
                "      FROM H_VehicleDispatchDetail " +
                "      WHERE OperationFlag = 1 AND VehicleDispatchFlag = 1 AND (ClassificationCode = 10 OR ClassificationCode = 11 OR ClassificationCode = 12 OR ClassificationCode = 30) " +
                "UNION ALL " +
                "      SELECT StaffCode3 AS StaffCode, StaffOccupation3 AS StaffOccupation, OperationDate, CarCode, ClassificationCode " +
                "      FROM H_VehicleDispatchDetail " +
                "      WHERE OperationFlag = 1 AND VehicleDispatchFlag = 1 AND (ClassificationCode = 10 OR ClassificationCode = 11 OR ClassificationCode = 12 OR ClassificationCode = 30) " +
                "UNION ALL " +
                "      SELECT StaffCode4 AS StaffCode, StaffOccupation4 AS StaffOccupation, OperationDate, CarCode, ClassificationCode " +
                "      FROM H_VehicleDispatchDetail " +
                "      WHERE OperationFlag = 1 AND VehicleDispatchFlag = 1 AND (ClassificationCode = 10 OR ClassificationCode = 11 OR ClassificationCode = 12 OR ClassificationCode = 30)) AS VehicleDispatchDetail " +
                "INNER JOIN H_StaffMaster ON VehicleDispatchDetail.StaffCode = H_StaffMaster.StaffCode " +
                "LEFT JOIN H_CarMaster ON VehicleDispatchDetail.CarCode = H_CarMaster.CarCode " +
                "LEFT JOIN H_BelongsMaster ON H_StaffMaster.Belongs = H_BelongsMaster.Code " +
                "WHERE VehicleDispatchDetail.OperationDate BETWEEN @OperationDate1 AND @OperationDate2 " +
                  "AND (H_StaffMaster.Belongs = 12 OR H_StaffMaster.Belongs = 22) " +
                  "AND (H_StaffMaster.JobForm = 20 OR H_StaffMaster.JobForm = 22 OR H_StaffMaster.JobForm = 99) " +
                  "AND (VehicleDispatchDetail.StaffOccupation = 11 OR (VehicleDispatchDetail.StaffOccupation = 10 AND (H_CarMaster.CarKindCode = 10 OR (H_CarMaster.CarKindCode = 11 AND H_CarMaster.ShapeCode = 10)))) " +
                  "AND DATEPART(WEEKDAY, VehicleDispatchDetail.OperationDate) <> 1 " +   // ★ 日曜日除外
                  "GROUP BY VehicleDispatchDetail.StaffCode, H_StaffMaster.UnionCode, H_StaffMaster.DisplayName, H_BelongsMaster.Name " +
                  "ORDER BY H_StaffMaster.UnionCode";

            sqlCommand.Parameters.Add("@OperationDate1", SqlDbType.Date).Value = operationDate1.Date;
            sqlCommand.Parameters.Add("@OperationDate2", SqlDbType.Date).Value = operationDate2.Date;

            using(SqlDataReader reader = sqlCommand.ExecuteReader()) {
                while(reader.Read()) {
                    PeakSeasonAllowanceVo peakSeasonAllowanceVo = new ();
                    peakSeasonAllowanceVo.UnionCode = _defaultValue.GetDefaultValue<int>(reader["UnionCode"]);
                    peakSeasonAllowanceVo.BelongsName = _defaultValue.GetDefaultValue<string>(reader["BelongsName"]);
                    peakSeasonAllowanceVo.DisplayName = _defaultValue.GetDefaultValue<string>(reader["DisplayName"]);
                    peakSeasonAllowanceVo.CountDays = _defaultValue.GetDefaultValue<int>(reader["HitCount"]);
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
