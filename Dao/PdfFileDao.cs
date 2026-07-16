/*
 * 2026-07-11
 */
using System.Data;
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class PdfFileDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
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
        public PdfFileDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        // ============================================================
        //  レコード存在チェック
        // ============================================================
        public bool ExistsById(string id) {
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT CASE WHEN EXISTS(SELECT 1 FROM H_PdfFile " +
                                     "                        WHERE Id = @Id) " +
                                     "THEN 1 ELSE 0 END";
            sqlCommand.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
            int result = (int)sqlCommand.ExecuteScalar();
            return result == 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PdfFileVo? SelectOnePdfFile(string id) {
            using SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();

            sqlCommand.CommandText = "SELECT Id," +
                                     "       PdfImage," +
                                     "       InsertPcName," +
                                     "       InsertYmdHms," +
                                     "       UpdatePcName," +
                                     "       UpdateYmdHms," +
                                     "       DeletePcName," +
                                     "       DeleteYmdHms," +
                                     "       DeleteFlag " +
                                     "FROM H_PdfFile " +
                                     "WHERE Id = @ID " +
                                     "AND DeleteFlag = 'false'";

            sqlCommand.Parameters.Add("@ID", SqlDbType.VarChar).Value = id;

            PdfFileVo? pdfFileVo = null;
            using(SqlDataReader aqlDataReader = sqlCommand.ExecuteReader()) {
                if(aqlDataReader.Read()) {
                    pdfFileVo = new();
                    pdfFileVo.Id = _defaultValue.GetDefaultValue<string>(aqlDataReader["Id"]);
                    pdfFileVo.PdfImage = _defaultValue.GetDefaultValue<byte[]>(aqlDataReader["PdfImage"]);
                    pdfFileVo.InsertPcName = _defaultValue.GetDefaultValue<string>(aqlDataReader["InsertPcName"]);
                    pdfFileVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(aqlDataReader["InsertYmdHms"]);
                    pdfFileVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(aqlDataReader["UpdatePcName"]);
                    pdfFileVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(aqlDataReader["UpdateYmdHms"]);
                    pdfFileVo.DeletePcName = _defaultValue.GetDefaultValue<string>(aqlDataReader["DeletePcName"]);
                    pdfFileVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(aqlDataReader["DeleteYmdHms"]);
                    pdfFileVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(aqlDataReader["DeleteFlag"]);
                }
            }
            return pdfFileVo;
        }

        /// <summary>
        /// PDF ファイルを新規登録する
        /// </summary>
        /// <param name="pdfFileVo"></param>
        public int InsertOnePdfFile(PdfFileVo pdfFileVo) {
            using SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();

            sqlCommand.CommandText =
                "INSERT INTO H_PdfFile (Id, " +
                "                       PdfImage, " +
                "                       InsertPcName, " +
                "                       InsertYmdHms, " +
                "                       UpdatePcName, " +
                "                       UpdateYmdHms, " +
                "                       DeletePcName, " +
                "                       DeleteYmdHms, " +
                "                       DeleteFlag" +
                ") VALUES (" +
                "                       @Id, " +
                "                       @PdfImage, " +
                "                       @InsertPcName, " +
                "                       GETDATE(), " +
                "                       '" + string.Empty + "'," +
                "                       '" + _defaultDateTime + "'," +
                "                       '" + string.Empty + "'," +
                "                       '" + _defaultDateTime + "'," +
                "                       'false')";
            sqlCommand.Parameters.Add("@Id", SqlDbType.VarChar).Value = pdfFileVo.Id;
            sqlCommand.Parameters.Add("@PdfImage", SqlDbType.VarBinary).Value = (object?)pdfFileVo.PdfImage ?? DBNull.Value;
            sqlCommand.Parameters.Add("@InsertPcName", SqlDbType.VarChar).Value = Environment.MachineName;
            return sqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// PDF ファイルを更新する
        /// </summary>
        /// <param name="pdfFileVo"></param>
        public int UpdateOnePdfFile(PdfFileVo pdfFileVo) {
            using SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();

            sqlCommand.CommandText =
                "UPDATE H_PdfFile SET " +
                "       PdfImage       = @PdfImage, " +
                "       UpdatePcName   = @UpdatePcName, " +
                "       UpdateYmdHms   = GETDATE() " +
                "WHERE  Id             = @Id";

            sqlCommand.Parameters.Add("@Id", SqlDbType.VarChar).Value = pdfFileVo.Id;
            sqlCommand.Parameters.Add("@PdfImage", SqlDbType.VarBinary).Value = (object?)pdfFileVo.PdfImage ?? DBNull.Value;
            sqlCommand.Parameters.Add("@UpdatePcName", SqlDbType.VarChar).Value = Environment.MachineName;
            return sqlCommand.ExecuteNonQuery();
        }

    }
}
