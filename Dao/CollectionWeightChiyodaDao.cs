/*
 * 2024-03-21
 * 千代田区配車 set_code = '1310101' '1310102' '1310103'
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class CollectionWeightChiyodaDao {
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public CollectionWeightChiyodaDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;

        }

        public List<CollectionWeightChiyodaVo> SelectVehicleDispatchDetail(DateTime operationDate1, DateTime operationDate2) {
            List<CollectionWeightChiyodaVo> listCollectionWeightChiyodaVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT H_VehicleDispatchDetail.OperationDate," +
                                            "H_VehicleDispatchDetail.StaffCode1," +
                                            "H_StaffMaster1.DisplayName AS StaffDisplayName1," +
                                            "H_VehicleDispatchDetail.StaffCode2," +
                                            "H_StaffMaster2.DisplayName AS StaffDisplayName2," +
                                            "H_VehicleDispatchDetail.StaffCode3," +
                                            "H_StaffMaster3.DisplayName AS StaffDisplayName3 " +
                                     "FROM H_VehicleDispatchDetail " +
                                     "LEFT OUTER JOIN H_StaffMaster AS H_StaffMaster1 ON H_VehicleDispatchDetail.StaffCode1 = H_StaffMaster1.StaffCode " +
                                     "LEFT OUTER JOIN H_StaffMaster AS H_StaffMaster2 ON H_VehicleDispatchDetail.StaffCode2 = H_StaffMaster2.StaffCode " +
                                     "LEFT OUTER JOIN H_StaffMaster AS H_StaffMaster3 ON H_VehicleDispatchDetail.StaffCode3 = H_StaffMaster3.StaffCode " +
                                     "WHERE OperationDate BETWEEN '" + operationDate1.ToString("yyyy-MM-dd") + "' AND '" + operationDate2.ToString("yyyy-MM-dd") + "' " +
                                       "AND OperationFlag = 'True' " +
                                       "AND (H_VehicleDispatchDetail.SetCode = '1310101' OR H_VehicleDispatchDetail.SetCode = '1310102' OR H_VehicleDispatchDetail.SetCode = '1310103') " +
                                     "ORDER BY OperationDate ASC, CellNumber ASC";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    CollectionWeightChiyodaVo collectionWeightChiyodaVo = new();
                    collectionWeightChiyodaVo.OperationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["OperationDate"]);
                    collectionWeightChiyodaVo.StaffCode1 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode1"]);
                    collectionWeightChiyodaVo.StaffDisplayName1 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffDisplayName1"]);
                    collectionWeightChiyodaVo.StaffCode2 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode2"]);
                    collectionWeightChiyodaVo.StaffDisplayName2 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffDisplayName2"]);
                    collectionWeightChiyodaVo.StaffCode3 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode3"]);
                    collectionWeightChiyodaVo.StaffDisplayName3 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffDisplayName3"]);
                    listCollectionWeightChiyodaVo.Add(collectionWeightChiyodaVo);
                }
            }
            return listCollectionWeightChiyodaVo;
        }

        public List<CollectionWeightGroupChiyodaVo> SelectGroupByVehicleDispatchDetail(DateTime operationDate1, DateTime operationDate2) {
            List<CollectionWeightGroupChiyodaVo> listCollectionWeightGroupChiyodaVo = new();
            CollectionWeightGroupChiyodaVo collectionWeightGroupChiyodaVo;
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT H_VehicleDispatchDetail.OperationDate," +
                                            "H_VehicleDispatchDetail.StaffCode1," +
                                            "H_StaffMaster1.DisplayName AS StaffDisplayName1," +
                                            "H_VehicleDispatchDetail.StaffCode2," +
                                            "H_StaffMaster2.DisplayName AS StaffDisplayName2 " +
                                     "FROM H_VehicleDispatchDetail " +
                                     "LEFT OUTER JOIN H_StaffMaster AS H_StaffMaster1 ON H_VehicleDispatchDetail.StaffCode1 = H_StaffMaster1.StaffCode " +
                                     "LEFT OUTER JOIN H_StaffMaster AS H_StaffMaster2 ON H_VehicleDispatchDetail.StaffCode2 = H_StaffMaster2.StaffCode " +
                                     "WHERE OperationDate BETWEEN '" + operationDate1.ToString("yyyy-MM-dd") + "' AND '" + operationDate2.ToString("yyyy-MM-dd") + "' " +
                                       "AND OperationFlag = 'True' " +
                                       "AND (H_VehicleDispatchDetail.SetCode = '1310101' OR H_VehicleDispatchDetail.SetCode = '1310102' OR H_VehicleDispatchDetail.SetCode = '1310103')";

            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    /*
                     * 運転者を追加する
                     */
                    collectionWeightGroupChiyodaVo = new();
                    collectionWeightGroupChiyodaVo.OperationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["OperationDate"]);
                    collectionWeightGroupChiyodaVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode1"]);
                    collectionWeightGroupChiyodaVo.StaffDisplayName = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffDisplayName1"]);
                    collectionWeightGroupChiyodaVo.Occupation = "運転手";
                    listCollectionWeightGroupChiyodaVo.Add(collectionWeightGroupChiyodaVo);
                    /*
                     * 作業員１を追加する
                     */
                    collectionWeightGroupChiyodaVo = new();
                    collectionWeightGroupChiyodaVo.OperationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["OperationDate"]);
                    collectionWeightGroupChiyodaVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode2"]);
                    collectionWeightGroupChiyodaVo.StaffDisplayName = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffDisplayName2"]);
                    collectionWeightGroupChiyodaVo.Occupation = "作業員";
                    listCollectionWeightGroupChiyodaVo.Add(collectionWeightGroupChiyodaVo);
                }
            }
            return listCollectionWeightGroupChiyodaVo;
        }

        /// <summary>
        /// GetWorkingDaysForStaff
        /// </summary>
        /// <param name="staffCode"></param>
        /// <returns>従事者の期間内の出勤日数を返す</returns>
        public int GetWorkingDaysForStaff(DateTime operationDate1, DateTime operationDate2, int staffCode) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(CellNumber) " +
                                     "FROM H_VehicleDispatchDetail " +
                                     "WHERE OperationDate BETWEEN '" + operationDate1.ToString("yyyy-MM-dd") + "' AND '" + operationDate2.ToString("yyyy-MM-dd") + "' " +
                                       "AND OperationFlag = 'True' " +
                                       "AND (StaffCode1 = " + staffCode + " OR StaffCode2 = " + staffCode + " OR StaffCode3 = " + staffCode + " OR StaffCode4 = " + staffCode + ")";
            return (int)sqlCommand.ExecuteScalar();
        }
    }
}
