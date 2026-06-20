/*
 * 2026-05-30
 */
using System.Data;
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class TimeOffMasterDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="connectionVo"></param>
        public TimeOffMasterDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>true:該当レコードあり false:該当レコードなし</returns>
        public bool ExistenceTimeOffMaster(DateTime date, int staffCode) {
            using SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT CASE WHEN EXISTS (SELECT 1 FROM H_TimeOffMaster " +
                                     "                         WHERE Date = @Date " +
                                     "                           AND StaffCode = @StaffCode) " +
                                     "THEN 1 ELSE 0 END";
            sqlCommand.Parameters.Add("@Date", SqlDbType.Date).Value = date.Date;
            sqlCommand.Parameters.Add("@StaffCode", SqlDbType.Int).Value = staffCode;

            try {
                return (int)sqlCommand.ExecuteScalar() != 0 ? true : false;
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public List<TimeOffMasterVo> SelectAllTimeOffMaster(DateTime date) {
            List<TimeOffMasterVo> listTimeOffMasterVo = new();

            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT Date," +
                                     "       BaseDate," +
                                     "       Code," +
                                     "       StaffCode," +
                                     "       Remarks," +
                                     "       ProcessedFlag," +
                                     "       InsertPcName," +
                                     "       InsertYmdHms," +
                                     "       UpdatePcName," +
                                     "       UpdateYmdHms," +
                                     "       DeletePcName," +
                                     "       DeleteYmdHms," +
                                     "       DeleteFlag " +
                                     "FROM   H_TimeOffMaster " +
                                     "WHERE  Date      = @Date " +
                                     "  AND  DeleteFlag = 0";

            sqlCommand.Parameters.Add("@Date", SqlDbType.Date).Value = date;

            using(SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while(sqlDataReader.Read() == true) {
                    TimeOffMasterVo timeOffMasterVo = new();
                    timeOffMasterVo.Date = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["Date"]);
                    timeOffMasterVo.BaseDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["BaseDate"]);
                    timeOffMasterVo.Code = _defaultValue.GetDefaultValue<int>(sqlDataReader["Code"]);
                    timeOffMasterVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    timeOffMasterVo.Remarks = _defaultValue.GetDefaultValue<string>(sqlDataReader["Remarks"]);
                    timeOffMasterVo.ProcessedFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["ProcessedFlag"]);
                    timeOffMasterVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    timeOffMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    timeOffMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    timeOffMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    timeOffMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    timeOffMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    timeOffMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listTimeOffMasterVo.Add(timeOffMasterVo);
                }
            }
            return listTimeOffMasterVo;
        }

        /// <summary>
        /// StaffCodeを基準として抽出する
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public List<TimeOffMasterVo> SelectAllTimeOffMaster(int staffCode) {
            List<TimeOffMasterVo> listTimeOffMasterVo = new();

            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT Date," +
                                     "       BaseDate," +
                                     "       Code," +
                                     "       StaffCode," +
                                     "       Remarks," +
                                     "       ProcessedFlag," +
                                     "       InsertPcName," +
                                     "       InsertYmdHms," +
                                     "       UpdatePcName," +
                                     "       UpdateYmdHms," +
                                     "       DeletePcName," +
                                     "       DeleteYmdHms," +
                                     "       DeleteFlag " +
                                     "FROM   H_TimeOffMaster " +
                                     "WHERE  StaffCode      = @StaffCode " +
                                     "  AND  DeleteFlag = 0";

            sqlCommand.Parameters.Add("@StaffCode", SqlDbType.Int).Value = staffCode;

            using(SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while(sqlDataReader.Read() == true) {
                    TimeOffMasterVo timeOffMasterVo = new();
                    timeOffMasterVo.Date = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["Date"]);
                    timeOffMasterVo.BaseDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["BaseDate"]);
                    timeOffMasterVo.Code = _defaultValue.GetDefaultValue<int>(sqlDataReader["Code"]);
                    timeOffMasterVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    timeOffMasterVo.Remarks = _defaultValue.GetDefaultValue<string>(sqlDataReader["Remarks"]);
                    timeOffMasterVo.ProcessedFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["ProcessedFlag"]);
                    timeOffMasterVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    timeOffMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    timeOffMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    timeOffMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    timeOffMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    timeOffMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    timeOffMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listTimeOffMasterVo.Add(timeOffMasterVo);
                }
            }
            return listTimeOffMasterVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="code"></param>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public TimeOffMasterVo? SelectOneTimeOffMaster(DateTime date, int code, int staffCode) {
            TimeOffMasterVo? timeOffMasterVo = null;

            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT Date," +
                                     "       BaseDate," +
                                     "       Code," +
                                     "       StaffCode," +
                                     "       Remarks," +
                                     "       ProcessedFlag," +
                                     "       InsertPcName," +
                                     "       InsertYmdHms," +
                                     "       UpdatePcName," +
                                     "       UpdateYmdHms," +
                                     "       DeletePcName," +
                                     "       DeleteYmdHms," +
                                     "       DeleteFlag " +
                                     "FROM   H_TimeOffMaster " +
                                     "WHERE  Date      = @Date " +
                                     "  AND  Code       = @Code " +
                                     "  AND  StaffCode  = @StaffCode " +
                                     "  AND  DeleteFlag = 0";

            sqlCommand.Parameters.Add("@Date", SqlDbType.Date).Value = date;
            sqlCommand.Parameters.Add("@Code", SqlDbType.Int).Value = code;
            sqlCommand.Parameters.Add("@StaffCode", SqlDbType.Int).Value = staffCode;

            using(SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while(sqlDataReader.Read() == true) {
                    timeOffMasterVo = new();
                    timeOffMasterVo.Date = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["Date"]);
                    timeOffMasterVo.BaseDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["BaseDate"]);
                    timeOffMasterVo.Code = _defaultValue.GetDefaultValue<int>(sqlDataReader["Code"]);
                    timeOffMasterVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    timeOffMasterVo.Remarks = _defaultValue.GetDefaultValue<string>(sqlDataReader["Remarks"]);
                    timeOffMasterVo.ProcessedFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["ProcessedFlag"]);
                    timeOffMasterVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    timeOffMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    timeOffMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    timeOffMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    timeOffMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    timeOffMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    timeOffMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                }
            }
            return timeOffMasterVo;
        }

        /// <summary>
        /// InsertOneTimeOffMaster
        /// </summary>
        /// <param name="timeOffMasterVo"></param>
        /// <returns></returns>
        public int InsertOneTimeOffMaster(DateTime date, DateTime baseDate, int code, int staffCode) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_TimeOffMaster(Date," +
                                     "                            BaseDate," +
                                     "                            Code," +
                                     "                            StaffCode," +
                                     "                            Remarks," +
                                     "                            ProcessedFlag," +
                                     "                            InsertPcName," +
                                     "                            InsertYmdHms," +
                                     "                            UpdatePcName," +
                                     "                            UpdateYmdHms," +
                                     "                            DeletePcName," +
                                     "                            DeleteYmdHms," +
                                     "                            DeleteFlag) " +
                                     "VALUES ('" + _defaultValue.GetDefaultValue<DateTime>(date).Date + "'," +
                                             "'" + _defaultValue.GetDefaultValue<DateTime>(baseDate).Date + "'," +
                                              "" + _defaultValue.GetDefaultValue<int>(code) + "," +
                                              "" + _defaultValue.GetDefaultValue<int>(staffCode) + "," +
                                             "'" + string.Empty + "'," +
                                             "'false'," +
                                             "'" + Environment.MachineName + "'," +
                                             "'" + DateTime.Now + "'," +
                                             "'" + string.Empty + "'," +
                                             "'" + _defaultDateTime + "'," +
                                             "'" + string.Empty + "'," +
                                             "'" + _defaultDateTime + "'," +
                                             "'False'" +
                                             ");";
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 対象レコードを更新する
        /// 種別
        /// </summary>
        /// <param name="date"></param>
        /// <param name="code"></param>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public int UpdateOneTimeOffMaster(DateTime date, int code, int staffCode) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_TimeOffMaster " +
                                     "SET Code = " + _defaultValue.GetDefaultValue<int>(code) + "," +
                                     "    UpdatePcName = '" + Environment.MachineName + "'," +
                                     "    UpdateYmdHms = '" + DateTime.Now + "' " +
                                     "WHERE Date = '" + date.ToString("yyyy-MM-dd") + "' AND StaffCode = " + staffCode;
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 対象レコードを更新する
        /// 備考
        /// </summary>
        /// <param name="date"></param>
        /// <param name="code"></param>
        /// <param name="staffCode"></param>
        /// <param name="remarks"></param>
        /// <returns></returns>
        public int UpdateOneRemarks(DateTime date, int code, int staffCode, string remarks) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_TimeOffMaster " +
                                     "SET Remarks      = @Remarks, " +
                                     "    UpdatePcName = @UpdatePcName, " +
                                     "    UpdateYmdHms = @UpdateYmdHms " +
                                     "WHERE Date       = @Date " +
                                     "  AND Code       = @Code " +
                                     "  AND StaffCode  = @StaffCode";

            sqlCommand.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = remarks;
            sqlCommand.Parameters.Add("@UpdatePcName", SqlDbType.NVarChar).Value = Environment.MachineName;
            sqlCommand.Parameters.Add("@UpdateYmdHms", SqlDbType.DateTime).Value = DateTime.Now;

            sqlCommand.Parameters.Add("@Date", SqlDbType.Date).Value = date;
            sqlCommand.Parameters.Add("@Code", SqlDbType.Int).Value = code;
            sqlCommand.Parameters.Add("@StaffCode", SqlDbType.Int).Value = staffCode;

            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 対象レコードを削除する
        /// 物理削除
        /// </summary>
        /// <param name="setCode"></param>
        /// <param name="operationDate"></param>
        /// <param name="lastRollCallYmdHms"></param>
        /// <returns></returns>
        public int DeleteOneTimeOffMaster(DateTime date, int staffCode) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM H_TimeOffMaster " +
                                     "       WHERE Date = @Date " +
                                     "         AND StaffCode = @StaffCode";
            sqlCommand.Parameters.Add("@Date", SqlDbType.Date).Value = date.Date;
            sqlCommand.Parameters.Add("@StaffCode", SqlDbType.Int).Value = staffCode;
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 指定した日付以降の有給消化日数を取得する
        /// </summary>
        /// <param name="PaidLeaveCommencementDate">起算日</param>
        /// <param name="staffCode">従事者コード</param>
        /// <returns></returns>
        public int GetPaidLeave(DateTime commencementDate, int staffCode) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(*) " +
                                     "FROM H_TimeOffMaster " +
                                     "WHERE StaffCode = @StaffCode " +
                                     "  AND Date >= @TargetDate " +
                                     "  AND DeleteFlag = 0";

            sqlCommand.Parameters.Add(new SqlParameter("@StaffCode", SqlDbType.Int) { Value = staffCode });
            sqlCommand.Parameters.Add(new SqlParameter("@TargetDate", SqlDbType.DateTime) { Value = commencementDate });

            try {
                return (int)sqlCommand.ExecuteScalar();
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

    public class TimeOffMasterVo {
        private DateTime _defaultDateTime = new(1900, 01, 01);

        private DateTime _date;                                 // 休暇取得日
        private DateTime _baseDate;                             // 基準起算日
        private int _code;                                      // 休暇コード
        private int _staffCode;                                 // 従事者コード
        private string _remarks;                                // 備考
        private bool _processedFlag;
        private string _insertPcName;
        private DateTime _insertYmdHms;
        private string _updatePcName;
        private DateTime _updateYmdHms;
        private string _deletePcName;
        private DateTime _deleteYmdHms;
        private bool _deleteFlag;

        public TimeOffMasterVo() {
            _date = _defaultDateTime;
            _code = 0;
            _staffCode = 0;
            _remarks = string.Empty;
            _insertPcName = string.Empty;
            _insertYmdHms = _defaultDateTime;
            _updatePcName = string.Empty;
            _updateYmdHms = _defaultDateTime;
            _deletePcName = string.Empty;
            _deleteYmdHms = _defaultDateTime;
            _deleteFlag = false;
        }

        /// <summary>
        /// 休暇日
        /// </summary>
        public DateTime Date {
            get => this._date; set => this._date = value;
        }
        /// <summary>
        /// 基準起算日(どの起算日に発生した有給を使用したか)
        /// </summary>
        public DateTime BaseDate {
            get => this._baseDate; set => this._baseDate = value;
        }
        /// <summary>
        /// 休暇Code
        /// </summary>
        public int Code {
            get => this._code; set => this._code = value;
        }
        /// <summary>
        /// StaffCode
        /// </summary>
        public int StaffCode {
            get => this._staffCode; set => this._staffCode = value;
        }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks {
            get => this._remarks; set => this._remarks = value;
        }
        /// <summary>
        /// true:有給期間消滅(2年) false:有給計算中
        /// </summary>
        public bool ProcessedFlag {
            get => this._processedFlag; set => this._processedFlag = value;
        }
        public string InsertPcName {
            get => this._insertPcName; set => this._insertPcName = value;
        }

        public DateTime InsertYmdHms {
            get => this._insertYmdHms; set => this._insertYmdHms = value;
        }
        public string UpdatePcName {
            get => this._updatePcName; set => this._updatePcName = value;
        }
        public DateTime UpdateYmdHms {
            get => this._updateYmdHms; set => this._updateYmdHms = value;
        }
        public string DeletePcName {
            get => this._deletePcName; set => this._deletePcName = value;
        }
        public DateTime DeleteYmdHms {
            get => this._deleteYmdHms; set => this._deleteYmdHms = value;
        }
        public bool DeleteFlag {
            get => this._deleteFlag; set => this._deleteFlag = value;
        }
    }
}
