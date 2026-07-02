/*
 * 2026-06-12
 */
using System.Data;
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class PaidLeaveEntitlementDao {
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        public PaidLeaveEntitlementDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            this._connectionVo = connectionVo;
        }

        /// <summary>
        /// レコード存在チェック（StaffCode + StartDate）
        /// true:該当レコードあり false:該当レコードなし
        /// </summary>
        /// <param name="staffCode"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public bool ExistenceHPaidLeaveEntitlement(int staffCode, DateTime startDate) {
            using SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT CASE WHEN EXISTS (SELECT 1 FROM H_PaidLeaveEntitlement " +
                                     "                                  WHERE StaffCode = @StaffCode " +
                                     "                                    AND StartDate = @StartDate" +
                                     ") THEN 1 ELSE 0 END";
            sqlCommand.Parameters.Add("@StaffCode", SqlDbType.Int).Value = staffCode;
            sqlCommand.Parameters.Add("@StartDate", SqlDbType.Date).Value = startDate.Date;

            int result = (int)sqlCommand.ExecuteScalar();
            return result == 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PaidLeaveEntitlementV0> SelectAllPaidLeaveEntitlementV0() {
            List<PaidLeaveEntitlementV0> listPaidLeaveEntitlementV0 = [];
            SqlCommand sqlCommand = this._connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                     "       YearsOfService," +
                                     "       StartDate," +
                                     "       GrantedDays," +
                                     "       Remarks," +
                                     "       InsertPcName," +
                                     "       InsertYmdHms," +
                                     "       UpdatePcName," +
                                     "       UpdateYmdHms," +
                                     "       DeletePcName," +
                                     "       DeleteYmdHms," +
                                     "       DeleteFlag " +
                                     "FROM H_PaidLeaveEntitlement " +
                                     "ORDER BY StaffCode ASC";
            using(SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while(sqlDataReader.Read() == true) {
                    PaidLeaveEntitlementV0 paidLeaveEntitlementV0 = new() {
                        StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]),
                        YearsOfService = _defaultValue.GetDefaultValue<int>(sqlDataReader["YearsOfService"]),
                        StartDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StartDate"]),
                        GrantedDays = _defaultValue.GetDefaultValue<int>(sqlDataReader["GrantedDays"]),
                        Remarks = _defaultValue.GetDefaultValue<string>(sqlDataReader["Remarks"]),
                        InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]),
                        InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]),
                        UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]),
                        UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]),
                        DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]),
                        DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]),
                        DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"])
                    };
                    listPaidLeaveEntitlementV0.Add(paidLeaveEntitlementV0);
                }
            }
            return listPaidLeaveEntitlementV0;
        }

        /// <summary>
        /// InsertOnePaidLeaveEntitlementV0
        /// 有給付与日数を1件登録する
        /// </summary>
        /// <param name="paidLeaveEntitlementV0"></param>
        /// <returns>影響を受けた件数</returns>
        public int InsertOnePaidLeaveEntitlementV0(PaidLeaveEntitlementV0 paidLeaveEntitlementV0) {
            SqlCommand sqlCommand = this._connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_PaidLeaveEntitlement (StaffCode," +
                                     "                                    YearsOfService," +
                                     "                                    StartDate," +
                                     "                                    GrantedDays," +
                                     "                                    Remarks," +
                                     "                                    InsertPcName," +
                                     "                                    InsertYmdHms," +
                                     "                                    UpdatePcName," +
                                     "                                    UpdateYmdHms," +
                                     "                                    DeletePcName," +
                                     "                                    DeleteYmdHms," +
                                     "                                    DeleteFlag" +
                                     "                                    ) VALUES (" +
                                     "                                    @StaffCode," +
                                     "                                    @YearsOfService," +
                                     "                                    @StartDate," +
                                     "                                    @GrantedDays," +
                                     "                                    @Remarks," +
                                     "                                    @InsertPcName," +
                                     "                                    @InsertYmdHms," +
                                     "                                    @UpdatePcName," +
                                     "                                    @UpdateYmdHms," +
                                     "                                    @DeletePcName," +
                                     "                                    @DeleteYmdHms," +
                                     "                                    @DeleteFlag)";

            sqlCommand.Parameters.Add(new SqlParameter("@StaffCode", paidLeaveEntitlementV0.StaffCode));
            sqlCommand.Parameters.Add(new SqlParameter("@YearsOfService", paidLeaveEntitlementV0.YearsOfService));
            sqlCommand.Parameters.Add(new SqlParameter("@StartDate", paidLeaveEntitlementV0.StartDate));
            sqlCommand.Parameters.Add(new SqlParameter("@GrantedDays", paidLeaveEntitlementV0.GrantedDays));
            sqlCommand.Parameters.Add(new SqlParameter("@Remarks", paidLeaveEntitlementV0.Remarks));
            sqlCommand.Parameters.Add(new SqlParameter("@InsertPcName", paidLeaveEntitlementV0.InsertPcName));
            sqlCommand.Parameters.Add(new SqlParameter("@InsertYmdHms", paidLeaveEntitlementV0.InsertYmdHms));
            sqlCommand.Parameters.Add(new SqlParameter("@UpdatePcName", paidLeaveEntitlementV0.UpdatePcName));
            sqlCommand.Parameters.Add(new SqlParameter("@UpdateYmdHms", paidLeaveEntitlementV0.UpdateYmdHms));
            sqlCommand.Parameters.Add(new SqlParameter("@DeletePcName", paidLeaveEntitlementV0.DeletePcName));
            sqlCommand.Parameters.Add(new SqlParameter("@DeleteYmdHms", paidLeaveEntitlementV0.DeleteYmdHms));
            sqlCommand.Parameters.Add(new SqlParameter("@DeleteFlag", paidLeaveEntitlementV0.DeleteFlag));

            return sqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// UpdateOnePaidLeaveEntitlementV0
        /// </summary>
        /// <param name="paidLeaveEntitlementV0"></param>
        /// <returns></returns>
        public int UpdateOnePaidLeaveEntitlementV0(PaidLeaveEntitlementV0 paidLeaveEntitlementV0) {
            SqlCommand sqlCommand = this._connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_PaidLeaveEntitlement SET YearsOfService = @YearsOfService, " +
                                     "                                  GrantedDays    = @GrantedDays, " +
                                     "                                  Remarks        = @Remarks, " +
                                     "                                  UpdatePcName   = @UpdatePcName, " +
                                     "                                  UpdateYmdHms   = @UpdateYmdHms  " +
                                     "                                  WHERE StaffCode = @StaffCode " +
                                     "                                    AND StartDate = @StartDate";

            sqlCommand.Parameters.Add(new SqlParameter("@StaffCode", paidLeaveEntitlementV0.StaffCode));
            sqlCommand.Parameters.Add(new SqlParameter("@YearsOfService", paidLeaveEntitlementV0.YearsOfService));
            sqlCommand.Parameters.Add(new SqlParameter("@StartDate", paidLeaveEntitlementV0.StartDate));
            sqlCommand.Parameters.Add(new SqlParameter("@GrantedDays", paidLeaveEntitlementV0.GrantedDays));
            sqlCommand.Parameters.Add(new SqlParameter("@Remarks", paidLeaveEntitlementV0.Remarks));
            sqlCommand.Parameters.Add(new SqlParameter("@UpdatePcName", paidLeaveEntitlementV0.UpdatePcName));
            sqlCommand.Parameters.Add(new SqlParameter("@UpdateYmdHms", paidLeaveEntitlementV0.UpdateYmdHms));

            return sqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 有給付与日数を取得する
        /// </summary>
        /// <param name="staffCode">従事者コード</param>
        /// <param name="startDate">起算日</param>
        /// <returns></returns>
        public int GetGrantedDays(int staffCode, DateTime startDate) {
            int grantedDays = 0;

            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT GrantedDays " +
                                     "FROM H_PaidLeaveEntitlement " +
                                     "WHERE StaffCode = @StaffCode " +
                                     "  AND StartDate = @StartDate " +
                                     "  AND DeleteFlag = 0";

            sqlCommand.Parameters.Add(new SqlParameter("@StaffCode", staffCode));
            sqlCommand.Parameters.Add(new SqlParameter("@StartDate", startDate));

            using(SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                if(sqlDataReader.Read() == true) {
                    grantedDays = _defaultValue.GetDefaultValue<int>(sqlDataReader["GrantedDays"]);
                }
            }

            return grantedDays;
        }

        /// <summary>
        /// 勤続年数を取得する
        /// </summary>
        /// <param name="staffCode"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public int GetYearsOfService(int staffCode, DateTime startDate) {
            int grantedDays = 0;

            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT YearsOfService " +
                                     "FROM H_PaidLeaveEntitlement " +
                                     "WHERE StaffCode = @StaffCode " +
                                     "  AND StartDate = @StartDate " +
                                     "  AND DeleteFlag = 0";

            sqlCommand.Parameters.Add(new SqlParameter("@StaffCode", staffCode));
            sqlCommand.Parameters.Add(new SqlParameter("@StartDate", startDate));

            using(SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                if(sqlDataReader.Read() == true) {
                    grantedDays = _defaultValue.GetDefaultValue<int>(sqlDataReader["YearsOfService"]);
                }
            }

            return grantedDays;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endDate">指定期間の最終日</param>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public List<PaidLeaveBalanceVo> SelectRemainingDays(DateTime endDate, int staffCode) {
            List<PaidLeaveBalanceVo> listPaidLeaveBalanceVo = new ();
            try {
                using(SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand()) {
                    sqlCommand.CommandText = "SELECT PaidLeaveEntitlement.StaffCode," +                                                                     // 従事者コード
                                             "       PaidLeaveEntitlement.StartDate," +                                                                     // 起算日
                                             "       PaidLeaveEntitlement.GrantedDays," +                                                                   // 有給付与日数
                                             "       ISNULL(TimeOffMaster.UsedDays, 0) AS UsedDays," +                                                      // 使用済み日数
                                             "       (PaidLeaveEntitlement.GrantedDays - ISNULL(TimeOffMaster.UsedDays, 0)) AS RemainingDays " +            // 有給残日数
                                             "FROM H_PaidLeaveEntitlement AS PaidLeaveEntitlement " +
                                             "LEFT JOIN (SELECT StaffCode, BaseDate, COUNT(*) AS UsedDays " +
                                             "            FROM H_TimeOffMaster " +
                                             "            WHERE StaffCode = @StaffCode " +
                                             "              AND Date <= @EndDate " +
                                             "              AND Code = 1 " +
                                             "            GROUP BY StaffCode, BaseDate) AS TimeOffMaster " +
                                             "     ON  PaidLeaveEntitlement.StaffCode = TimeOffMaster.StaffCode " +
                                             "     AND PaidLeaveEntitlement.StartDate = TimeOffMaster.BaseDate " +
                                             "WHERE PaidLeaveEntitlement.StaffCode = @StaffCode " +
                                             "AND   PaidLeaveEntitlement.StartDate IN (" +
                                             "                                          (SELECT MAX(StartDate)                    FROM   H_PaidLeaveEntitlement WHERE StaffCode = @StaffCode)," +
                                             "                                          (SELECT DATEADD(year, -1, MAX(StartDate)) FROM   H_PaidLeaveEntitlement WHERE StaffCode = @StaffCode) " +
                                             "                                         ) " +
                                             "ORDER BY PaidLeaveEntitlement.StartDate ASC";

                    sqlCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate;
                    sqlCommand.Parameters.Add("@StaffCode", SqlDbType.Int).Value = staffCode;

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while(sqlDataReader.Read() == true) {
                        PaidLeaveBalanceVo paidLeaveBalanceVo = new ();
                        paidLeaveBalanceVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);                                  // 従事者コード
                        paidLeaveBalanceVo.StartDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StartDate"]);                             // 起算日
                        paidLeaveBalanceVo.GrantedDays = _defaultValue.GetDefaultValue<int>(sqlDataReader["GrantedDays"]);                              // 有給付与日数
                        paidLeaveBalanceVo.UsedDays = _defaultValue.GetDefaultValue<int>(sqlDataReader["UsedDays"]);                                    // 使用済み日数
                        paidLeaveBalanceVo.RemainingDays = _defaultValue.GetDefaultValue<int>(sqlDataReader["RemainingDays"]);                          // 有給残日数
                        listPaidLeaveBalanceVo.Add(paidLeaveBalanceVo);
                    }
                }
                return listPaidLeaveBalanceVo;
            } catch {
                throw;
            }
        }
    }

    /*
     * ----------------------------------------
     * Vo 内部クラス
     * ----------------------------------------
     */
    public class PaidLeaveEntitlementV0 {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);

        private int _staffCode;
        private DateTime _startDate;
        private int _yearsOfService;
        private int _grantedDays;
        private string _remarks;
        private string _insertPcName;
        private DateTime _insertYmdHms;
        private string _updatePcName;
        private DateTime _updateYmdHms;
        private string _deletePcName;
        private DateTime _deleteYmdHms;
        private bool _deleteFlag;

        public PaidLeaveEntitlementV0() {
            this._staffCode = 0;
            this._startDate = this._defaultDateTime;
            this._yearsOfService = 0;
            this._grantedDays = 0;
            this._remarks = string.Empty;
            this._insertPcName = string.Empty;
            this._insertYmdHms = this._defaultDateTime;
            this._updatePcName = string.Empty;
            this._updateYmdHms = this._defaultDateTime;
            this._deletePcName = string.Empty;
            this._deleteYmdHms = this._defaultDateTime;
            this._deleteFlag = false;
        }

        /// <summary>
        /// 従事者コード
        /// </summary>
        public int StaffCode {
            get {
                return this._staffCode;
            }

            set {
                this._staffCode = value;
            }
        }
        /// <summary>
        /// 起算日(毎年分)
        /// </summary>
        public DateTime StartDate {
            get {
                return this._startDate;
            }

            set {
                this._startDate = value;
            }
        }
        /// <summary>
        /// 勤続年数
        /// </summary>
        public int YearsOfService {
            get {
                return this._yearsOfService;
            }

            set {
                this._yearsOfService = value;
            }
        }
        /// <summary>
        /// 有給付与日数(毎年分)
        /// </summary>
        public int GrantedDays {
            get {
                return this._grantedDays;
            }

            set {
                this._grantedDays = value;
            }
        }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks {
            get {
                return this._remarks;
            }

            set {
                this._remarks = value;
            }
        }
        public string InsertPcName {
            get {
                return this._insertPcName;
            }

            set {
                this._insertPcName = value;
            }
        }
        public DateTime InsertYmdHms {
            get {
                return this._insertYmdHms;
            }

            set {
                this._insertYmdHms = value;
            }
        }
        public string UpdatePcName {
            get {
                return this._updatePcName;
            }

            set {
                this._updatePcName = value;
            }
        }
        public DateTime UpdateYmdHms {
            get {
                return this._updateYmdHms;
            }

            set {
                this._updateYmdHms = value;
            }
        }
        public string DeletePcName {
            get {
                return this._deletePcName;
            }

            set {
                this._deletePcName = value;
            }
        }
        public DateTime DeleteYmdHms {
            get {
                return this._deleteYmdHms;
            }

            set {
                this._deleteYmdHms = value;
            }
        }
        public bool DeleteFlag {
            get {
                return this._deleteFlag;
            }

            set {
                this._deleteFlag = value;
            }
        }
    }

    public class PaidLeaveBalanceVo {
        private int _staffCode;
        private DateTime _startDate;
        private int _GrantedDays;
        private int _UsedDays;
        private int _RemainingDays;

        /// <summary>
        /// 従事者コード
        /// </summary>
        public int StaffCode {
            get {
                return _staffCode;
            }

            set {
                _staffCode = value;
            }
        }
        /// <summary>
        /// 起算日
        /// </summary>
        public DateTime StartDate {
            get {
                return _startDate;
            }

            set {
                _startDate = value;
            }
        }
        /// <summary>
        /// 有給付与日数
        /// </summary>
        public int GrantedDays {
            get {
                return _GrantedDays;
            }

            set {
                _GrantedDays = value;
            }
        }
        /// <summary>
        /// 使用済み日数
        /// </summary>
        public int UsedDays {
            get {
                return _UsedDays;
            }

            set {
                _UsedDays = value;
            }
        }
        /// <summary>
        /// 有給残日数
        /// </summary>
        public int RemainingDays {
            get {
                return _RemainingDays;
            }

            set {
                _RemainingDays = value;
            }
        }
    }
}

