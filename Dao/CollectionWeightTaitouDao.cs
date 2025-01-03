/*
 * 2024-03-16
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class CollectionWeightTaitouDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public CollectionWeightTaitouDao(ConnectionVo connectionVo) {
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
        public List<CollectionWeightTaitouVo> SelectListCollectionWeightTaitou(DateTime operationDate1, DateTime operationDate2) {
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
    }
}
