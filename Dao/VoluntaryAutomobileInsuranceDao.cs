/*
 * 2026-04-04
 */
using System.Data;
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class VoluntaryAutomobileInsuranceDao {
        /*
         * インスタンス
         */
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public VoluntaryAutomobileInsuranceDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        public List<(StaffMasterVo Staff, VoluntaryAutomobileInsuranceVo Insurance, string BelongsName, string OccupationName, string JobFormName)> SelectStaffWithVoluntaryInsurance(List<int>? sqlBelongs, List<int>? sqlJobForm, List<int>? sqlOccupation, bool? sqlRetirementFlag) {
            var list = new List<(StaffMasterVo, VoluntaryAutomobileInsuranceVo, string, string, string)>();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();

            sqlCommand.CommandText =
                "SELECT " +
                "   S.StaffCode," +
                "   S.UnionCode," +
                "   S.Belongs," +
                "   B.Name AS BelongsName," +
                "   S.JobForm," +
                "   J.Name AS JobFormName," +
                "   S.Occupation," +
                "   O.Name AS OccupationName," +
                "   S.NameKana," +
                "   S.Name," +
                "   S.DisplayName," +
                "   S.BirthDate," +
                "   S.EmploymentDate," +

                "   V.Id AS V_Id," +
                "   V.VehicleType AS V_VehicleType," +
                "   V.CompanyName AS V_CompanyName," +
                "   V.StartDate AS V_StartDate," +
                "   V.EndDate AS V_EndDate," +

                "   CASE WHEN V.Image1 IS NULL THEN 0 ELSE 1 END AS HasImage1," +
                "   CASE WHEN V.Image2 IS NULL THEN 0 ELSE 1 END AS HasImage2," +
                "   CASE WHEN V.Image3 IS NULL THEN 0 ELSE 1 END AS HasImage3 " +
                "   CASE WHEN V.Image4 IS NULL THEN 0 ELSE 1 END AS HasImage4 " +

                "FROM H_StaffMaster S " +
                "LEFT JOIN H_VoluntaryAutomobileInsurance V ON S.StaffCode = V.StaffCode " +
                "LEFT JOIN H_BelongsMaster B ON S.Belongs = B.Code " +
                "LEFT JOIN H_OccupationMaster O ON S.Occupation = O.Code " +
                "LEFT JOIN H_JobFormMaster J ON S.JobForm = J.Code " +
                "WHERE S.DeleteFlag = 'false' " +
                      CreateSqlBelongs(sqlBelongs) +
                      CreateSqlJobForm(sqlJobForm) +
                      CreateSqlOccupation(sqlOccupation) +
                      CreateSqlRetirementFlag(sqlRetirementFlag) +
                "ORDER BY S.Belongs ASC,S.Occupation ASC,S.UnionCode ASC";

            using (SqlDataReader reader = sqlCommand.ExecuteReader()) {
                while (reader.Read()) {
                    // --- StaffMasterVo ---
                    StaffMasterVo staff = new();
                    staff.StaffCode = _defaultValue.GetDefaultValue<int>(reader["StaffCode"]);
                    staff.UnionCode = _defaultValue.GetDefaultValue<int>(reader["UnionCode"]);
                    staff.Belongs = _defaultValue.GetDefaultValue<int>(reader["Belongs"]);
                    staff.JobForm = _defaultValue.GetDefaultValue<int>(reader["JobForm"]);
                    staff.Occupation = _defaultValue.GetDefaultValue<int>(reader["Occupation"]);
                    staff.NameKana = _defaultValue.GetDefaultValue<string>(reader["NameKana"]);
                    staff.Name = _defaultValue.GetDefaultValue<string>(reader["Name"]);
                    staff.DisplayName = _defaultValue.GetDefaultValue<string>(reader["DisplayName"]);
                    staff.BirthDate = _defaultValue.GetDefaultValue<DateTime>(reader["BirthDate"]);
                    staff.EmploymentDate = _defaultValue.GetDefaultValue<DateTime>(reader["EmploymentDate"]);

                    // --- Insurance ---
                    VoluntaryAutomobileInsuranceVo insurance = new();
                    insurance.Id = _defaultValue.GetDefaultValue<string>(reader["V_Id"]);
                    insurance.StaffCode = staff.StaffCode.ToString();
                    insurance.VehicleType = _defaultValue.GetDefaultValue<string>(reader["V_VehicleType"]);
                    insurance.CompanyName = _defaultValue.GetDefaultValue<string>(reader["V_CompanyName"]);
                    insurance.StartDate = _defaultValue.GetDefaultValue<string>(reader["V_StartDate"]);
                    insurance.EndDate = _defaultValue.GetDefaultValue<string>(reader["V_EndDate"]);
                    insurance.HasImage1 = _defaultValue.GetDefaultValue<bool>(reader["HasImage1"]);
                    insurance.HasImage2 = _defaultValue.GetDefaultValue<bool>(reader["HasImage2"]);
                    insurance.HasImage3 = _defaultValue.GetDefaultValue<bool>(reader["HasImage3"]);
                    insurance.HasImage4 = _defaultValue.GetDefaultValue<bool>(reader["HasImage4"]);

                    // --- JOIN 名称 ---
                    string belongsName = _defaultValue.GetDefaultValue<string>(reader["BelongsName"]);
                    string occupationName = _defaultValue.GetDefaultValue<string>(reader["OccupationName"]);
                    string jobFormName = _defaultValue.GetDefaultValue<string>(reader["JobFormName"]);

                    list.Add((staff, insurance, belongsName, occupationName, jobFormName));
                }
            }

            return list;
        }

        // ============================================================
        //  INSERT
        // ============================================================
        public void InsertOneVoluntaryAutomobileInsuranceVo(VoluntaryAutomobileInsuranceVo vo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();

            sqlCommand.CommandText =
                "INSERT INTO H_VoluntaryAutomobileInsurance (" +
                "       Id," +
                "       StaffCode," +
                "       VehicleType," +
                "       CompanyName," +
                "       StartDate," +
                "       EndDate," +
                "       Image1," +
                "       Image2," +
                "       Image3," +
                "       Image4," +
                "       InsertPcName," +
                "       InsertYmdHms," +
                "       DeleteFlag" +
                ") VALUES (" +
                "       @Id," +
                "       @StaffCode," +
                "       @VehicleType," +
                "       @CompanyName," +
                "       @StartDate," +
                "       @EndDate," +
                "       @Image1," +
                "       @Image2," +
                "       @Image3," +
                "       @Image4," +
                "       @PcName," +
                "       GETDATE()," +
                "       'false'" +
                ")";

            sqlCommand.Parameters.Add("@Id", SqlDbType.VarChar).Value = vo.Id;
            sqlCommand.Parameters.Add("@StaffCode", SqlDbType.Int).Value = vo.StaffCode;
            sqlCommand.Parameters.Add("@VehicleType", SqlDbType.VarChar).Value = vo.VehicleType;
            sqlCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = vo.CompanyName;
            sqlCommand.Parameters.Add("@StartDate", SqlDbType.Date).Value = vo.StartDate;
            sqlCommand.Parameters.Add("@EndDate", SqlDbType.Date).Value = vo.EndDate;

            sqlCommand.Parameters.Add("@Image1", SqlDbType.VarBinary).Value = (object?)vo.Image1 ?? DBNull.Value;
            sqlCommand.Parameters.Add("@Image2", SqlDbType.VarBinary).Value = (object?)vo.Image2 ?? DBNull.Value;
            sqlCommand.Parameters.Add("@Image3", SqlDbType.VarBinary).Value = (object?)vo.Image3 ?? DBNull.Value;
            sqlCommand.Parameters.Add("@Image4", SqlDbType.VarBinary).Value = (object?)vo.Image4 ?? DBNull.Value;

            sqlCommand.Parameters.Add("@PcName", SqlDbType.VarChar).Value = Environment.MachineName;

            sqlCommand.ExecuteNonQuery();
        }

        // ============================================================
        //  UPDATE
        // ============================================================
        public void UpdateOneVoluntaryAutomobileInsuranceVo(VoluntaryAutomobileInsuranceVo vo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();

            sqlCommand.CommandText =
                "UPDATE H_VoluntaryAutomobileInsurance SET " +
                "       StaffCode      = @StaffCode," +
                "       VehicleType    = @VehicleType," +
                "       CompanyName    = @CompanyName," +
                "       StartDate      = @StartDate," +
                "       EndDate        = @EndDate," +
                "       Image1         = @Image1," +
                "       Image2         = @Image2," +
                "       Image3         = @Image3," +
                "       Image4         = @Image4," +
                "       UpdatePcName   = @PcName," +
                "       UpdateYmdHms   = GETDATE() " +
                "WHERE  Id             = @Id";

            sqlCommand.Parameters.Add("@Id", SqlDbType.VarChar).Value = vo.Id;
            sqlCommand.Parameters.Add("@StaffCode", SqlDbType.Int).Value = vo.StaffCode;
            sqlCommand.Parameters.Add("@VehicleType", SqlDbType.VarChar).Value = vo.VehicleType;
            sqlCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = vo.CompanyName;
            sqlCommand.Parameters.Add("@StartDate", SqlDbType.Date).Value = vo.StartDate;
            sqlCommand.Parameters.Add("@EndDate", SqlDbType.Date).Value = vo.EndDate;

            sqlCommand.Parameters.Add("@Image1", SqlDbType.VarBinary).Value = (object?)vo.Image1 ?? DBNull.Value;
            sqlCommand.Parameters.Add("@Image2", SqlDbType.VarBinary).Value = (object?)vo.Image2 ?? DBNull.Value;
            sqlCommand.Parameters.Add("@Image3", SqlDbType.VarBinary).Value = (object?)vo.Image3 ?? DBNull.Value;
            sqlCommand.Parameters.Add("@Image4", SqlDbType.VarBinary).Value = (object?)vo.Image4 ?? DBNull.Value;

            sqlCommand.Parameters.Add("@PcName", SqlDbType.VarChar).Value = Environment.MachineName;

            sqlCommand.ExecuteNonQuery();
        }

        // ============================================================
        //  DELETE（論理削除）
        // ============================================================
        public void DeleteOneVoluntaryAutomobileInsuranceVo(string id) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();

            sqlCommand.CommandText =
                "UPDATE H_VoluntaryAutomobileInsurance SET " +
                "    DeletePcName = @PcName, " +
                "    DeleteYmdHms = GETDATE(), " +
                "    DeleteFlag = 'true' " +
                "WHERE Id = @Id";

            sqlCommand.Parameters.Add("@PcName", SqlDbType.VarChar).Value = Environment.MachineName;
            sqlCommand.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;

            sqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// SQL Belongsを作成する
        /// </summary>
        /// <param name="sqlBelongs"></param>
        /// <returns></returns>
        private string CreateSqlBelongs(List<int>? sqlBelongs) {
            string sql = string.Empty;
            if (sqlBelongs is not null) {
                string codes = string.Empty;
                int i = 0;
                foreach (int code in sqlBelongs) {
                    codes += string.Concat(code.ToString(), i < sqlBelongs.Count - 1 ? "," : "");
                    i++;
                }
                sql = " AND Belongs IN (" + codes + ")";
                return sql;
            } else {
                return sql;
            }
        }

        /// <summary>
        /// SQL JobFormを作成する
        /// </summary>
        /// <param name="sqlJobForm"></param>
        /// <returns></returns>
        private string CreateSqlJobForm(List<int>? sqlJobForm) {
            string sql = string.Empty;
            if (sqlJobForm is not null) {
                string codes = string.Empty;
                int i = 0;
                foreach (int code in sqlJobForm) {
                    codes += string.Concat(code.ToString(), i < sqlJobForm.Count - 1 ? "," : "");
                    i++;
                }
                sql = " AND JobForm IN (" + codes + ")";
                return sql;
            } else {
                return sql;
            }
        }

        /// <summary>
        /// SQL Occupationを作成する
        /// </summary>
        /// <param name="sqlOccupation"></param>
        /// <returns></returns>
        private string CreateSqlOccupation(List<int>? sqlOccupation) {
            string sql = string.Empty;
            if (sqlOccupation is not null) {
                string codes = string.Empty;
                int i = 0;
                foreach (int code in sqlOccupation) {
                    codes += string.Concat(code.ToString(), i < sqlOccupation.Count - 1 ? "," : "");
                    i++;
                }
                sql = " AND Occupation IN (" + codes + ")";
                return sql;
            } else {
                return sql;
            }
        }

        /// <summary>
        /// SQL RetirementFlagを作成する
        /// </summary>
        /// <param name="sqlRetirementFlag"></param>
        /// <returns></returns>
        private string CreateSqlRetirementFlag(bool? sqlRetirementFlag) {
            string sql = string.Empty;
            if (sqlRetirementFlag is not null) {
                if (sqlRetirementFlag == false) {
                    return " AND RetirementFlag = 'false'";
                } else {
                    return sql;
                }
            } else {
                return sql;
            }
        }

        /// <summary>
        /// Image1〜4 の PDF を取得する
        /// </summary>
        public byte[]? SelectPdf(string id, int imageNo) {
            if (imageNo < 1 || imageNo > 4)
                throw new ArgumentOutOfRangeException(nameof(imageNo));

            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText =
                $"SELECT Image{imageNo} " +
                "FROM H_VoluntaryAutomobileInsurance " +
                "WHERE Id = @Id";

            sqlCommand.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;

            using (SqlDataReader reader = sqlCommand.ExecuteReader()) {
                if (reader.Read()) {
                    if (reader[0] == DBNull.Value)
                        return null;

                    return (byte[])reader[0];
                }
            }
            return null;
        }

        /// <summary>
        /// Image1〜4 に PDF を保存する
        /// </summary>
        public void UpdatePdf(string id, int imageNo, byte[]? pdfBytes) {
            if (imageNo < 1 || imageNo > 4)
                throw new ArgumentOutOfRangeException(nameof(imageNo));

            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText =
                $"UPDATE H_VoluntaryAutomobileInsurance " +
                $"SET Image{imageNo} = @Pdf, " +
                "    UpdatePcName = @PcName, " +
                "    UpdateYmdHms = GETDATE() " +
                "WHERE Id = @Id";

            sqlCommand.Parameters.Add("@Pdf", SqlDbType.VarBinary).Value =
                (object?)pdfBytes ?? DBNull.Value;

            sqlCommand.Parameters.Add("@PcName", SqlDbType.VarChar).Value =
                Environment.MachineName;

            sqlCommand.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;

            sqlCommand.ExecuteNonQuery();
        }

    }
}
