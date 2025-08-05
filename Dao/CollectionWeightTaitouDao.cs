/*
 * 2024-03-16
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class CollectionTaitouDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public CollectionTaitouDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// ExistenceCollectionWeightTaitou
        /// true:該当レコードあり false:該当レコードなし
        /// </summary>
        /// <param name="operationDate"></param>
        /// <returns></returns>
        public bool ExistenceCollectionWeightTaitou(DateTime operationDate) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(OperationDate) " +
                                     "FROM H_CollectionWeightTaitou " +
                                     "WHERE OperationDate = '" + operationDate.Date + "'";
            try {
                return (int)sqlCommand.ExecuteScalar() > 0 ? true : false;
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationDate"></param>
        /// <returns></returns>
        public CollectionWeightTaitouVo SelectOneCollectionWeightTaitou(DateTime operationDate) {
            CollectionWeightTaitouVo hCollectionWeightTaitouVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT OperationDate," +
                                            "Weight1Total," +
                                            "Weight2Total," +
                                            "Weight3Total," +
                                            "Weight4Total," +
                                            "Weight5Total," +
                                            "Weight6Total," +
                                            "Weight7Total," +
                                            "Weight8Total," +
                                            "Weight9Total," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_CollectionWeightTaitou " +
                                     "WHERE OperationDate = '" + operationDate.Date + "'";
            try {
                using (var sqlDataReader = sqlCommand.ExecuteReader()) {
                    while (sqlDataReader.Read() == true) {
                        hCollectionWeightTaitouVo = new();
                        hCollectionWeightTaitouVo.OperationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["OperationDate"]);
                        hCollectionWeightTaitouVo.Weight1Total = _defaultValue.GetDefaultValue<int>(sqlDataReader["Weight1Total"]);
                        hCollectionWeightTaitouVo.Weight2Total = _defaultValue.GetDefaultValue<int>(sqlDataReader["Weight2Total"]);
                        hCollectionWeightTaitouVo.Weight3Total = _defaultValue.GetDefaultValue<int>(sqlDataReader["Weight3Total"]);
                        hCollectionWeightTaitouVo.Weight4Total = _defaultValue.GetDefaultValue<int>(sqlDataReader["Weight4Total"]);
                        hCollectionWeightTaitouVo.Weight5Total = _defaultValue.GetDefaultValue<int>(sqlDataReader["Weight5Total"]);
                        hCollectionWeightTaitouVo.Weight6Total = _defaultValue.GetDefaultValue<int>(sqlDataReader["Weight6Total"]);
                        hCollectionWeightTaitouVo.Weight7Total = _defaultValue.GetDefaultValue<int>(sqlDataReader["Weight7Total"]);
                        hCollectionWeightTaitouVo.Weight8Total = _defaultValue.GetDefaultValue<int>(sqlDataReader["Weight8Total"]);
                        hCollectionWeightTaitouVo.Weight9Total = _defaultValue.GetDefaultValue<int>(sqlDataReader["Weight9Total"]);
                        hCollectionWeightTaitouVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                        hCollectionWeightTaitouVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                        hCollectionWeightTaitouVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                        hCollectionWeightTaitouVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                        hCollectionWeightTaitouVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                        hCollectionWeightTaitouVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                        hCollectionWeightTaitouVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    }
                }
                return hCollectionWeightTaitouVo;
            } catch {
                throw;
            }
        }

        /// <summary>
        /// SelectListHCollectionWeightTaitou
        /// </summary>
        /// <param name="operationDate1"></param>
        /// <param name="operationDate2"></param>
        /// <returns></returns>
        public List<CollectionWeightTaitouVo> SelectCollectionWeightTaitou(DateTime operationDate1, DateTime operationDate2) {
            List<CollectionWeightTaitouVo> listCollectionWeightTaitouVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT OperationDate," +
                                            "Weight1Total," +
                                            "Weight2Total," +
                                            "Weight3Total," +
                                            "Weight4Total," +
                                            "Weight5Total," +
                                            "Weight6Total," +
                                            "Weight7Total," +
                                            "Weight8Total," +
                                            "Weight9Total," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_CollectionWeightTaitou " +
                                     "WHERE OperationDate BETWEEN '" + operationDate1.Date + "' AND '" + operationDate2.Date + "'";
            try {
                using (var sqlDataReader = sqlCommand.ExecuteReader()) {
                    while (sqlDataReader.Read() == true) {
                        CollectionWeightTaitouVo hCollectionWeightTaitouVo = new();
                        hCollectionWeightTaitouVo.OperationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["OperationDate"]);
                        hCollectionWeightTaitouVo.Weight1Total = _defaultValue.GetDefaultValue<int>(sqlDataReader["Weight1Total"]);
                        hCollectionWeightTaitouVo.Weight2Total = _defaultValue.GetDefaultValue<int>(sqlDataReader["Weight2Total"]);
                        hCollectionWeightTaitouVo.Weight3Total = _defaultValue.GetDefaultValue<int>(sqlDataReader["Weight3Total"]);
                        hCollectionWeightTaitouVo.Weight4Total = _defaultValue.GetDefaultValue<int>(sqlDataReader["Weight4Total"]);
                        hCollectionWeightTaitouVo.Weight5Total = _defaultValue.GetDefaultValue<int>(sqlDataReader["Weight5Total"]);
                        hCollectionWeightTaitouVo.Weight6Total = _defaultValue.GetDefaultValue<int>(sqlDataReader["Weight6Total"]);
                        hCollectionWeightTaitouVo.Weight7Total = _defaultValue.GetDefaultValue<int>(sqlDataReader["Weight7Total"]);
                        hCollectionWeightTaitouVo.Weight8Total = _defaultValue.GetDefaultValue<int>(sqlDataReader["Weight8Total"]);
                        hCollectionWeightTaitouVo.Weight9Total = _defaultValue.GetDefaultValue<int>(sqlDataReader["Weight9Total"]);
                        hCollectionWeightTaitouVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                        hCollectionWeightTaitouVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                        hCollectionWeightTaitouVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                        hCollectionWeightTaitouVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                        hCollectionWeightTaitouVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                        hCollectionWeightTaitouVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                        hCollectionWeightTaitouVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                        listCollectionWeightTaitouVo.Add(hCollectionWeightTaitouVo);
                    }
                }
                return listCollectionWeightTaitouVo;
            } catch {
                throw;
            }
        }

        /// <summary>
        /// InsertCollectionWeightTaitou
        /// </summary>
        /// <param name="collectionWeightTaitouVo"></param>
        /// <returns></returns>
        public int InsertOneCollectionWeightTaitou(CollectionWeightTaitouVo collectionWeightTaitouVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_CollectionWeightTaitou(OperationDate," +
                                                                          "Weight1Total," +
                                                                          "Weight2Total," +
                                                                          "Weight3Total," +
                                                                          "Weight4Total," +
                                                                          "Weight5Total," +
                                                                          "Weight6Total," +
                                                                          "Weight7Total," +
                                                                          "Weight8Total," +
                                                                          "Weight9Total," +
                                                                          "InsertPcName," +
                                                                          "InsertYmdHms," +
                                                                          "UpdatePcName," +
                                                                          "UpdateYmdHms," +
                                                                          "DeletePcName," +
                                                                          "DeleteYmdHms," +
                                                                          "DeleteFlag) " +
                                     "VALUES ('" + _defaultValue.GetDefaultValue<DateTime>(collectionWeightTaitouVo.OperationDate) + "'," +
                                              "" + _defaultValue.GetDefaultValue<int>(collectionWeightTaitouVo.Weight1Total) + "," +
                                              "" + _defaultValue.GetDefaultValue<int>(collectionWeightTaitouVo.Weight2Total) + "," +
                                              "" + _defaultValue.GetDefaultValue<int>(collectionWeightTaitouVo.Weight3Total) + "," +
                                              "" + _defaultValue.GetDefaultValue<int>(collectionWeightTaitouVo.Weight4Total) + "," +
                                              "" + _defaultValue.GetDefaultValue<int>(collectionWeightTaitouVo.Weight5Total) + "," +
                                              "" + _defaultValue.GetDefaultValue<int>(collectionWeightTaitouVo.Weight6Total) + "," +
                                              "" + _defaultValue.GetDefaultValue<int>(collectionWeightTaitouVo.Weight7Total) + "," +
                                              "" + _defaultValue.GetDefaultValue<int>(collectionWeightTaitouVo.Weight8Total) + "," +
                                              "" + _defaultValue.GetDefaultValue<int>(collectionWeightTaitouVo.Weight9Total) + "," +
                                             "'" + Environment.MachineName + "'," +
                                             "'" + DateTime.Today + "'," +
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
        /// UpdateCollectionWeightTaitou
        /// </summary>
        /// <param name="collectionWeightTaitouVo"></param>
        /// <returns></returns>
        public int UpdateOneCollectionWeightTaitou(CollectionWeightTaitouVo collectionWeightTaitouVo) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_CollectionWeightTaitou " +
                                     "SET OperationDate = '" + _defaultValue.GetDefaultValue<DateTime>(collectionWeightTaitouVo.OperationDate) + "'," +
                                         "Weight1Total = " + _defaultValue.GetDefaultValue<int>(collectionWeightTaitouVo.Weight1Total) + "," +
                                         "Weight2Total = " + _defaultValue.GetDefaultValue<int>(collectionWeightTaitouVo.Weight2Total) + "," +
                                         "Weight3Total = " + _defaultValue.GetDefaultValue<int>(collectionWeightTaitouVo.Weight3Total) + "," +
                                         "Weight4Total = " + _defaultValue.GetDefaultValue<int>(collectionWeightTaitouVo.Weight4Total) + "," +
                                         "Weight5Total = " + _defaultValue.GetDefaultValue<int>(collectionWeightTaitouVo.Weight5Total) + "," +
                                         "Weight6Total = " + _defaultValue.GetDefaultValue<int>(collectionWeightTaitouVo.Weight6Total) + "," +
                                         "Weight7Total = " + _defaultValue.GetDefaultValue<int>(collectionWeightTaitouVo.Weight7Total) + "," +
                                         "Weight8Total = " + _defaultValue.GetDefaultValue<int>(collectionWeightTaitouVo.Weight8Total) + "," +
                                         "Weight9Total = " + _defaultValue.GetDefaultValue<int>(collectionWeightTaitouVo.Weight9Total) + "," +
                                         "UpdatePcName = '" + Environment.MachineName + "'," +
                                         "UpdateYmdHms = '" + DateTime.Today + "' " +
                                     "WHERE OperationDate = '" + collectionWeightTaitouVo.OperationDate.Date + "'";
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// SelectOperationDaysVehicleDispatchDetail
        /// 稼働日数を取得
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="setCode"></param>
        /// <returns>稼働日数を返す</returns>
        public int GetCollectionOperationDays(int year, int month, int setCode) {
            /*
             * 1310602 台東資源1
             * 1310603 台東資源2
             * 1310604 台東資源4
             * 1310606 台東資源臨
             */
            DateTime targetYmd = new DateTime(year, month, 01);
            string staYmd = string.Concat(targetYmd.Year, "/", targetYmd.Month, "/01");
            string endYmd = string.Concat(targetYmd.Year, "/", targetYmd.Month, "/", targetYmd.AddMonths(1).AddDays(-1).Day);

            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(SetCode) " +
                                     "FROM H_VehicleDispatchDetail " +
                                     "WHERE OperationDate BETWEEN '" + staYmd + "' AND '" + endYmd + "' " +
                                       "AND OperationFlag = 'true' " +
                                       "AND SetCode = " + setCode + " " +
                                     "GROUP BY SetCode";
            int? operationDays = (int?)sqlCommand.ExecuteScalar();
            if (operationDays.HasValue) {
                return (int)operationDays;
            } else {
                return 0;
            }
        }

        /// <summary>
        /// ３人目の人数を取得
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="setCode"></param>
        /// <returns></returns>
        public List<CollectionStaffsTaitouVo> GetCollectionStaffs(int year, int month, int setCode) {
            /*
             * 1310602 台東資源1
             * 1310603 台東資源2
             * 1310604 台東資源4
             * 1310606 台東資源臨
             */
            DateTime targetYmd = new DateTime(year, month, 01);
            string staYmd = string.Concat(targetYmd.Year, "/", targetYmd.Month, "/01");
            string endYmd = string.Concat(targetYmd.Year, "/", targetYmd.Month, "/", targetYmd.AddMonths(1).AddDays(-1).Day);

            List<CollectionStaffsTaitouVo> listCollectionStaffsTaitouVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT DATEPART(WEEKDAY,OperationDate) AS WeekDay," +
                                            "StaffCode3 " +
                                     "FROM H_VehicleDispatchDetail " +
                                     "WHERE OperationDate BETWEEN '" + staYmd + "' AND '" + endYmd + "' " +
                                       "AND OperationFlag = 'true' " +
                                       "AND SetCode = " + setCode + " " +
                                       "AND StaffCode3 > 0";
            using (var sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    CollectionStaffsTaitouVo collectionStaffsTaitouVo = new();
                    collectionStaffsTaitouVo.OperationWeekDay = _defaultValue.GetDefaultValue<int>(sqlDataReader["WeekDay"]);
                    collectionStaffsTaitouVo.StaffCode3 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode3"]);
                    listCollectionStaffsTaitouVo.Add(collectionStaffsTaitouVo);
                }
            }
            return listCollectionStaffsTaitouVo;
        }
    }
}
