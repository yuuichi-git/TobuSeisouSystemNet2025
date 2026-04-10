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

        // ============================================================
        //  レコード存在チェック
        // ============================================================
        public bool ExistsById(string id) {
            SqlCommand cmd = _connectionVo.SqlServerConnection.CreateCommand();
            cmd.CommandText =
                "SELECT CASE WHEN EXISTS (" +
                "    SELECT 1 FROM H_VoluntaryAutomobileInsurance " +
                "    WHERE Id = @Id" +
                ") THEN 1 ELSE 0 END";

            cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;

            int result = (int)cmd.ExecuteScalar();
            return result == 1;
        }

        // ============================================================
        //  スタッフコード存在チェック（int版）
        // ============================================================
        public bool ExistsByStaffCode(int staffCode) {
            using SqlCommand cmd = _connectionVo.SqlServerConnection.CreateCommand();
            cmd.CommandText =
                "SELECT CASE WHEN EXISTS (" +
                "    SELECT 1 FROM H_VoluntaryAutomobileInsurance " +
                "    WHERE StaffCode = @StaffCode" +
                ") THEN 1 ELSE 0 END";

            cmd.Parameters.Add("@StaffCode", SqlDbType.Int).Value = staffCode;

            int result = (int)cmd.ExecuteScalar();
            return result == 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public VoluntaryAutomobileInsuranceVo SelectOneByStaffCode(int staffCode) {
            using SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();

            sqlCommand.CommandText = "SELECT" +
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
                                     "       UpdatePcName," +
                                     "       UpdateYmdHms," +
                                     "       DeletePcName," +
                                     "       DeleteYmdHms," +
                                     "       DeleteFlag" +
                                     "  FROM H_VoluntaryAutomobileInsurance" +
                                     " WHERE StaffCode = @StaffCode" +
                                     "   AND DeleteFlag = 'false'";

            sqlCommand.Parameters.Add("@StaffCode", SqlDbType.Int).Value = staffCode;

            using SqlDataReader reader = sqlCommand.ExecuteReader();

            if (!reader.Read())
                return null;

            VoluntaryAutomobileInsuranceVo vo = new();

            /*
             * 文字列系
             */
            vo.Id = reader["Id"].ToString() ?? "";
            vo.StaffCode = (int)reader["StaffCode"];
            vo.VehicleType = reader["VehicleType"].ToString() ?? "";
            vo.CompanyName = reader["CompanyName"].ToString() ?? "";
            vo.StartDate = reader["StartDate"].ToString() ?? "";
            vo.EndDate = reader["EndDate"].ToString() ?? "";

            /*
             * PDF（byte[]）
             */
            vo.Image1 = reader["Image1"] as byte[] ?? Array.Empty<byte>();
            vo.Image2 = reader["Image2"] as byte[] ?? Array.Empty<byte>();
            vo.Image3 = reader["Image3"] as byte[] ?? Array.Empty<byte>();
            vo.Image4 = reader["Image4"] as byte[] ?? Array.Empty<byte>();

            /*
             * DateTime 系
             */
            vo.InsertYmdHms = reader.GetDateTime(reader.GetOrdinal("InsertYmdHms"));
            vo.UpdateYmdHms = reader.GetDateTime(reader.GetOrdinal("UpdateYmdHms"));
            vo.DeleteYmdHms = reader.GetDateTime(reader.GetOrdinal("DeleteYmdHms"));

            /*
             * PC 名
             */
            vo.InsertPcName = reader["InsertPcName"].ToString() ?? "";
            vo.UpdatePcName = reader["UpdatePcName"].ToString() ?? "";
            vo.DeletePcName = reader["DeletePcName"].ToString() ?? "";

            /*
             * 削除フラグ（bool）
             */
            vo.DeleteFlag = (bool)reader["DeleteFlag"];

            /*
             * HasImageX のセット
             */
            vo.HasImage1 = vo.Image1.Length > 0;
            vo.HasImage2 = vo.Image2.Length > 0;
            vo.HasImage3 = vo.Image3.Length > 0;
            vo.HasImage4 = vo.Image4.Length > 0;

            return vo;
        }


        /// <summary>
        /// スタッフ情報と任意保険情報を JOIN して取得する。
        /// ・スタッフ基本情報（H_StaffMaster）
        /// ・任意保険情報（H_VoluntaryAutomobileInsurance）
        /// ・所属名 / 職種名 / 雇用形態名（各マスタ）
        ///
        /// 条件：所属 / 雇用形態 / 職種 / 退職フラグ を可変条件で絞り込む。
        /// 戻り値：
        ///   (StaffMasterVo Staff,
        ///    VoluntaryAutomobileInsuranceVo Insurance,
        ///    string BelongsName,
        ///    string OccupationName,
        ///    string JobFormName)
        /// のタプルリスト。
        /// </summary>
        public List<(StaffMasterVo Staff, VoluntaryAutomobileInsuranceVo Insurance, string BelongsName, string OccupationName, string JobFormName)>
            SelectStaffWithVoluntaryInsurance(List<int>? sqlBelongs, List<int>? sqlJobForm, List<int>? sqlOccupation, bool? sqlRetirementFlag) {

            // ▼ JOIN 結果を格納するリスト
            var list = new List<(StaffMasterVo, VoluntaryAutomobileInsuranceVo, string, string, string)>();

            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();

            /*
             * ▼ SQL 作成
             * スタッフ情報を基点に LEFT JOIN で任意保険情報を結合する。
             * → 任意保険が未登録のスタッフも一覧に表示するため LEFT JOIN を使用。
             */
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

                // ▼ 任意保険情報（V_ で別名を付けて区別）
                "   V.Id AS V_Id," +
                "   V.VehicleType AS V_VehicleType," +
                "   V.CompanyName AS V_CompanyName," +
                "   V.StartDate AS V_StartDate," +
                "   V.EndDate AS V_EndDate," +

                // ▼ PDF の有無（NULL ではなく 0 バイトも考慮）
                //   → DATALENGTH() = 0 なら PDF 無し
                "   CASE WHEN DATALENGTH(V.Image1) IS NULL OR DATALENGTH(V.Image1) = 0 THEN 0 ELSE 1 END AS HasImage1," +
                "   CASE WHEN DATALENGTH(V.Image2) IS NULL OR DATALENGTH(V.Image2) = 0 THEN 0 ELSE 1 END AS HasImage2," +
                "   CASE WHEN DATALENGTH(V.Image3) IS NULL OR DATALENGTH(V.Image3) = 0 THEN 0 ELSE 1 END AS HasImage3," +
                "   CASE WHEN DATALENGTH(V.Image4) IS NULL OR DATALENGTH(V.Image4) = 0 THEN 0 ELSE 1 END AS HasImage4 " +

                "FROM H_StaffMaster S " +
                "LEFT JOIN H_VoluntaryAutomobileInsurance V ON S.StaffCode = V.StaffCode " +
                "LEFT JOIN H_BelongsMaster B ON S.Belongs = B.Code " +
                "LEFT JOIN H_OccupationMaster O ON S.Occupation = O.Code " +
                "LEFT JOIN H_JobFormMaster J ON S.JobForm = J.Code " +
                "WHERE S.DeleteFlag = 'false' " +   // スタッフが削除されていないものだけ
                      CreateSqlBelongs(sqlBelongs) +      // 所属フィルタ
                      CreateSqlJobForm(sqlJobForm) +      // 雇用形態フィルタ
                      CreateSqlOccupation(sqlOccupation) +// 職種フィルタ
                      CreateSqlRetirementFlag(sqlRetirementFlag) + // 退職フラグフィルタ
                "ORDER BY S.Belongs ASC, S.Occupation ASC, S.UnionCode ASC";

            /*
             * ▼ SQL 実行
             */
            using (SqlDataReader reader = sqlCommand.ExecuteReader()) {
                while (reader.Read()) {

                    /*
                     * ▼ StaffMasterVo の生成
                     * スタッフ基本情報を VO に詰める。
                     */
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

                    /*
                     * ▼ VoluntaryAutomobileInsuranceVo の生成
                     * LEFT JOIN のため、保険未登録の場合は NULL または 0 バイトが返る。
                     * _defaultValue により安全に初期化される。
                     */
                    VoluntaryAutomobileInsuranceVo insurance = new();
                    insurance.Id = _defaultValue.GetDefaultValue<string>(reader["V_Id"]);
                    insurance.StaffCode = staff.StaffCode; // StaffCode は StaffMaster と同じ
                    insurance.VehicleType = _defaultValue.GetDefaultValue<string>(reader["V_VehicleType"]);
                    insurance.CompanyName = _defaultValue.GetDefaultValue<string>(reader["V_CompanyName"]);
                    insurance.StartDate = _defaultValue.GetDefaultValue<string>(reader["V_StartDate"]);
                    insurance.EndDate = _defaultValue.GetDefaultValue<string>(reader["V_EndDate"]);

                    // ▼ PDF の有無（HasImageX）
                    insurance.HasImage1 = _defaultValue.GetDefaultValue<bool>(reader["HasImage1"]);
                    insurance.HasImage2 = _defaultValue.GetDefaultValue<bool>(reader["HasImage2"]);
                    insurance.HasImage3 = _defaultValue.GetDefaultValue<bool>(reader["HasImage3"]);
                    insurance.HasImage4 = _defaultValue.GetDefaultValue<bool>(reader["HasImage4"]);

                    /*
                     * ▼ JOIN 名称（所属名・職種名・雇用形態名）
                     */
                    string belongsName = _defaultValue.GetDefaultValue<string>(reader["BelongsName"]);
                    string occupationName = _defaultValue.GetDefaultValue<string>(reader["OccupationName"]);
                    string jobFormName = _defaultValue.GetDefaultValue<string>(reader["JobFormName"]);

                    /*
                     * ▼ タプルとしてリストに追加
                     */
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

            sqlCommand.CommandText = "INSERT INTO H_VoluntaryAutomobileInsurance (" +
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
                                     "       UpdatePcName," +
                                     "       UpdateYmdHms," +
                                     "       DeletePcName," +
                                     "       DeleteYmdHms," +
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
                                     "       ''," +
                                     "       '1900-01-01'," +
                                     "       ''," +
                                     "       '1900-01-01'," +
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
        //  Id と StaffCode は更新しない（WHERE 句の条件にする）
        // ============================================================
        public void UpdateOneVoluntaryAutomobileInsuranceVo(VoluntaryAutomobileInsuranceVo vo) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();

            sqlCommand.CommandText =
                "UPDATE H_VoluntaryAutomobileInsurance SET " +
                //"       Id             = @Id," +
                //"       StaffCode      = @StaffCode," +
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
                "WHERE  StaffCode      = @StaffCode";

            //sqlCommand.Parameters.Add("@Id", SqlDbType.VarChar).Value = vo.Id;
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
        public void DeleteOneVoluntaryAutomobileInsuranceVo(int staffCode) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();

            sqlCommand.CommandText =
                "UPDATE H_VoluntaryAutomobileInsurance SET " +
                "    DeletePcName = @PcName, " +
                "    DeleteYmdHms = GETDATE(), " +
                "    DeleteFlag = 'true' " +
                "WHERE StaffCode = @StaffCode";

            sqlCommand.Parameters.Add("@PcName", SqlDbType.VarChar).Value = Environment.MachineName;
            sqlCommand.Parameters.Add("@StaffCode", SqlDbType.VarChar).Value = staffCode;

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

        // ============================================================
        //  PDF 取得（Image1〜4）
        // ============================================================
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

        // ============================================================
        //  PDF INSERT（Image1〜4）
        // ============================================================
        public void InsertPdf(string id, int imageNo, byte[]? pdfBytes) {
            if (imageNo < 1 || imageNo > 4)
                throw new ArgumentOutOfRangeException(nameof(imageNo));

            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText =
                $"INSERT INTO H_VoluntaryAutomobileInsurance (" +
                $"    Id, Image{imageNo}, UpdatePcName, UpdateYmdHms" +
                $") VALUES (" +
                $"    @Id, @Pdf, @PcName, GETDATE()" +
                $")";

            sqlCommand.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
            sqlCommand.Parameters.Add("@Pdf", SqlDbType.VarBinary).Value = (object?)pdfBytes ?? DBNull.Value;
            sqlCommand.Parameters.Add("@PcName", SqlDbType.VarChar).Value = Environment.MachineName;
            sqlCommand.ExecuteNonQuery();
        }


        // ============================================================
        //  PDF 保存（Image1〜4）
        // ============================================================
        public void UpdatePdf(string id, int imageNo, byte[]? pdfBytes) {
            if (imageNo < 1 || imageNo > 4)
                throw new ArgumentOutOfRangeException(nameof(imageNo));

            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText =
                $"UPDATE H_VoluntaryAutomobileInsurance SET " +
                $"    Image{imageNo}   = @Pdf, " +
                $"    UpdatePcName     = @PcName, " +
                $"    UpdateYmdHms     = GETDATE() " +
                $"WHERE Id            = @Id";

            sqlCommand.Parameters.Add("@Pdf", SqlDbType.VarBinary).Value = (object?)pdfBytes ?? DBNull.Value;
            sqlCommand.Parameters.Add("@PcName", SqlDbType.VarChar).Value = Environment.MachineName;
            sqlCommand.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
            sqlCommand.ExecuteNonQuery();
        }

        // ============================================================
        //  PDF 削除（Image1〜4 を NULL にする）
        // ============================================================
        public void DeletePdf(string id, int imageNo) {
            if (imageNo < 1 || imageNo > 4)
                throw new ArgumentOutOfRangeException(nameof(imageNo));

            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText =
                $"UPDATE H_VoluntaryAutomobileInsurance SET " +
                $"    Image{imageNo}   = NULL, " +
                $"    UpdatePcName     = @PcName, " +
                $"    UpdateYmdHms     = GETDATE() " +
                $"WHERE Id            = @Id";

            sqlCommand.Parameters.Add("@PcName", SqlDbType.VarChar).Value = Environment.MachineName;
            sqlCommand.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
            sqlCommand.ExecuteNonQuery();
        }

        public List<string> SelectGroupVehicleType() {
            List<string> listGroupVehicleType = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT VehicleType " +
                                     "FROM H_VoluntaryAutomobileInsurance " +
                                     "GROUP BY VehicleType " +
                                     "ORDER BY VehicleType ASC";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    string vehicleType = _defaultValue.GetDefaultValue<string>(sqlDataReader["VehicleType"]);
                    listGroupVehicleType.Add(vehicleType);
                }
            }
            return listGroupVehicleType;
        }

        public List<string> SelectGroupCompanyName() {
            List<string> listGroupCompanyName = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT CompanyName " +
                                     "FROM H_VoluntaryAutomobileInsurance " +
                                     "GROUP BY CompanyName " +
                                     "ORDER BY CompanyName ASC";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    string vehicleType = _defaultValue.GetDefaultValue<string>(sqlDataReader["CompanyName"]);
                    listGroupCompanyName.Add(vehicleType);
                }
            }
            return listGroupCompanyName;
        }
    }
}
