/*
 * 2024-12-19
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class StaffDestinationDao {
        /*
         * インスタンス
         */
        private readonly DateTime _defaultDateTime = new DateTime(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        public StaffDestinationDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<StaffMasterVo> SelectAllStaffMasterVo() {
            List<StaffMasterVo> listStaffMasterVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT StaffCode," +
                                            "Belongs," +
                                            "JobForm," +
                                            "Occupation," +
                                            "NameKana," +
                                            "Name," +
                                            "DisplayName " +
                                     "FROM H_StaffMaster " +
                                     "WHERE Occupation IN(10,11) AND RetirementFlag = 'false' AND DeleteFlag = 'false'";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    StaffMasterVo staffMasterVo = new();
                    staffMasterVo.StaffCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode"]);
                    staffMasterVo.Belongs = _defaultValue.GetDefaultValue<int>(sqlDataReader["Belongs"]);
                    staffMasterVo.JobForm = _defaultValue.GetDefaultValue<int>(sqlDataReader["JobForm"]);
                    staffMasterVo.Occupation = _defaultValue.GetDefaultValue<int>(sqlDataReader["Occupation"]);
                    staffMasterVo.NameKana = _defaultValue.GetDefaultValue<string>(sqlDataReader["NameKana"]);
                    staffMasterVo.Name = _defaultValue.GetDefaultValue<string>(sqlDataReader["Name"]);
                    staffMasterVo.DisplayName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DisplayName"]);
                    listStaffMasterVo.Add(staffMasterVo);
                }
            }
            return listStaffMasterVo;
        }

        public List<StaffDestinationVo> SelectAllStaffDestinationVo(DateTime operationDate1, DateTime operationDate2) {
            List<StaffDestinationVo> listStaffDestinationVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT H_VehicleDispatchDetail.OperationDate," +
                                            "H_SetMaster.SetCode," +
                                            "H_SetMaster.SetName," +
                                            "H_VehicleDispatchDetail.LastRollCallYmdHms," +
                                            "H_VehicleDispatchDetail.StaffCode1," +
                                            "H_StaffMaster1.DisplayName AS StaffName1," +
                                            "H_StaffMaster1.Belongs AS StaffBelongs1," +
                                            "H_StaffMaster1.JobForm AS StaffJobform1," +
                                            "H_VehicleDispatchDetail.StaffOccupation1," +
                                            "H_VehicleDispatchDetail.StaffProxyFlag1," +
                                            "H_VehicleDispatchDetail.StaffRollCallFlag1," +
                                            "H_VehicleDispatchDetail.StaffRollCallYmdHms1," +
                                            "H_VehicleDispatchDetail.StaffMemoFlag1," +
                                            "H_VehicleDispatchDetail.StaffMemo1," +
                                            "H_VehicleDispatchDetail.StaffCode2," +
                                            "H_StaffMaster2.DisplayName AS StaffName2," +
                                            "H_StaffMaster2.Belongs AS StaffBelongs2," +
                                            "H_StaffMaster2.JobForm AS StaffJobform2," +
                                            "H_VehicleDispatchDetail.StaffOccupation2," +
                                            "H_VehicleDispatchDetail.StaffProxyFlag2," +
                                            "H_VehicleDispatchDetail.StaffRollCallFlag2," +
                                            "H_VehicleDispatchDetail.StaffRollCallYmdHms2," +
                                            "H_VehicleDispatchDetail.StaffMemoFlag2," +
                                            "H_VehicleDispatchDetail.StaffMemo2," +
                                            "H_VehicleDispatchDetail.StaffCode3," +
                                            "H_StaffMaster3.DisplayName AS StaffName3," +
                                            "H_StaffMaster3.Belongs AS StaffBelongs3," +
                                            "H_StaffMaster3.JobForm AS StaffJobform3," +
                                            "H_VehicleDispatchDetail.StaffOccupation3," +
                                            "H_VehicleDispatchDetail.StaffProxyFlag3," +
                                            "H_VehicleDispatchDetail.StaffRollCallFlag3," +
                                            "H_VehicleDispatchDetail.StaffRollCallYmdHms3," +
                                            "H_VehicleDispatchDetail.StaffMemoFlag3," +
                                            "H_VehicleDispatchDetail.StaffMemo3," +
                                            "H_VehicleDispatchDetail.StaffCode4," +
                                            "H_StaffMaster4.DisplayName AS StaffName4," +
                                            "H_StaffMaster4.Belongs AS StaffBelongs4," +
                                            "H_StaffMaster4.JobForm AS StaffJobform4," +
                                            "H_VehicleDispatchDetail.StaffOccupation4," +
                                            "H_VehicleDispatchDetail.StaffProxyFlag4," +
                                            "H_VehicleDispatchDetail.StaffRollCallFlag4," +
                                            "H_VehicleDispatchDetail.StaffRollCallYmdHms4," +
                                            "H_VehicleDispatchDetail.StaffMemoFlag4," +
                                            "H_VehicleDispatchDetail.StaffMemo4 " +
                                     "FROM H_VehicleDispatchDetail " +
                                     "LEFT OUTER JOIN H_SetMaster ON H_VehicleDispatchDetail.SetCode = H_SetMaster.SetCode " +
                                     "LEFT OUTER JOIN H_StaffMaster AS H_StaffMaster1 ON H_VehicleDispatchDetail.StaffCode1 = H_StaffMaster1.StaffCode " +
                                     "LEFT OUTER JOIN H_StaffMaster AS H_StaffMaster2 ON H_VehicleDispatchDetail.StaffCode2 = H_StaffMaster2.StaffCode " +
                                     "LEFT OUTER JOIN H_StaffMaster AS H_StaffMaster3 ON H_VehicleDispatchDetail.StaffCode3 = H_StaffMaster3.StaffCode " +
                                     "LEFT OUTER JOIN H_StaffMaster AS H_StaffMaster4 ON H_VehicleDispatchDetail.StaffCode4 = H_StaffMaster4.StaffCode " +
                                     "WHERE H_VehicleDispatchDetail.OperationDate BETWEEN '" + operationDate1.ToString("yyyy-MM-dd") + "' AND '" + operationDate2.ToString("yyyy-MM-dd") + "' " +
                                       "AND H_VehicleDispatchDetail.OperationFlag = 'true'";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    StaffDestinationVo staffDestinationVo = new();
                    staffDestinationVo.OperationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["OperationDate"]);
                    staffDestinationVo.SetCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["SetCode"]);
                    staffDestinationVo.SetName = _defaultValue.GetDefaultValue<string>(sqlDataReader["SetName"]);
                    staffDestinationVo.StaffCode1 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode1"]);
                    staffDestinationVo.StaffName1 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffName1"]);
                    staffDestinationVo.StaffBelongs1 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffBelongs1"]);
                    staffDestinationVo.StaffJobForm1 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffJobform1"]);
                    staffDestinationVo.StaffOccupation1 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffOccupation1"]);
                    staffDestinationVo.StaffProxyFlag1 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffProxyFlag1"]);
                    staffDestinationVo.StaffRollCallFlag1 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffRollCallFlag1"]);
                    staffDestinationVo.StaffRollCallYmdHms1 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StaffRollCallYmdHms1"]);
                    staffDestinationVo.StaffMemoFlag1 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffMemoFlag1"]);
                    staffDestinationVo.StaffMemo1 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffMemo1"]);
                    staffDestinationVo.StaffCode2 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode2"]);
                    staffDestinationVo.StaffName2 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffName2"]);
                    staffDestinationVo.StaffBelongs2 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffBelongs2"]);
                    staffDestinationVo.StaffJobForm2 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffJobform2"]);
                    staffDestinationVo.StaffOccupation2 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffOccupation2"]);
                    staffDestinationVo.StaffProxyFlag2 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffProxyFlag2"]);
                    staffDestinationVo.StaffRollCallFlag2 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffRollCallFlag2"]);
                    staffDestinationVo.StaffRollCallYmdHms2 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StaffRollCallYmdHms2"]);
                    staffDestinationVo.StaffMemoFlag2 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffMemoFlag2"]);
                    staffDestinationVo.StaffMemo2 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffMemo2"]);
                    staffDestinationVo.StaffCode3 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode3"]);
                    staffDestinationVo.StaffName3 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffName3"]);
                    staffDestinationVo.StaffBelongs3 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffBelongs3"]);
                    staffDestinationVo.StaffJobForm3 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffJobform3"]);
                    staffDestinationVo.StaffOccupation3 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffOccupation3"]);
                    staffDestinationVo.StaffProxyFlag3 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffProxyFlag3"]);
                    staffDestinationVo.StaffRollCallFlag3 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffRollCallFlag3"]);
                    staffDestinationVo.StaffRollCallYmdHms3 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StaffRollCallYmdHms3"]);
                    staffDestinationVo.StaffMemoFlag3 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffMemoFlag3"]);
                    staffDestinationVo.StaffMemo3 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffMemo3"]);
                    staffDestinationVo.StaffCode4 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode4"]);
                    staffDestinationVo.StaffName4 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffName4"]);
                    staffDestinationVo.StaffBelongs4 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffBelongs4"]);
                    staffDestinationVo.StaffJobForm4 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffJobform4"]);
                    staffDestinationVo.StaffOccupation4 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffOccupation4"]);
                    staffDestinationVo.StaffProxyFlag4 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffProxyFlag4"]);
                    staffDestinationVo.StaffRollCallFlag4 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffRollCallFlag4"]);
                    staffDestinationVo.StaffRollCallYmdHms4 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StaffRollCallYmdHms4"]);
                    staffDestinationVo.StaffMemoFlag4 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffMemoFlag4"]);
                    staffDestinationVo.StaffMemo4 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffMemo4"]);
                    listStaffDestinationVo.Add(staffDestinationVo);
                }
            }
            return listStaffDestinationVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationDate1"></param>
        /// <param name="operationDate2"></param>
        /// <param name="staffCode"></param>
        /// <returns></returns>
        public List<StaffDestinationVo> SelectOneStaffDestinationVo(DateTime operationDate1, DateTime operationDate2, int staffCode) {
            List<StaffDestinationVo> listStaffDestinationVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT H_VehicleDispatchDetail.OperationDate," +
                                            "H_SetMaster.SetCode," +
                                            "H_SetMaster.SetName," +
                                            "H_VehicleDispatchDetail.LastRollCallYmdHms," +
                                            "H_VehicleDispatchDetail.StaffCode1," +
                                            "H_StaffMaster1.DisplayName AS StaffName1," +
                                            "H_StaffMaster1.Belongs AS StaffBelongs1," +
                                            "H_StaffMaster1.JobForm AS StaffJobform1," +
                                            "H_VehicleDispatchDetail.StaffOccupation1," +
                                            "H_VehicleDispatchDetail.StaffProxyFlag1," +
                                            "H_VehicleDispatchDetail.StaffRollCallFlag1," +
                                            "H_VehicleDispatchDetail.StaffRollCallYmdHms1," +
                                            "H_VehicleDispatchDetail.StaffMemoFlag1," +
                                            "H_VehicleDispatchDetail.StaffMemo1," +
                                            "H_VehicleDispatchDetail.StaffCode2," +
                                            "H_StaffMaster2.DisplayName AS StaffName2," +
                                            "H_StaffMaster2.Belongs AS StaffBelongs2," +
                                            "H_StaffMaster2.JobForm AS StaffJobform2," +
                                            "H_VehicleDispatchDetail.StaffOccupation2," +
                                            "H_VehicleDispatchDetail.StaffProxyFlag2," +
                                            "H_VehicleDispatchDetail.StaffRollCallFlag2," +
                                            "H_VehicleDispatchDetail.StaffRollCallYmdHms2," +
                                            "H_VehicleDispatchDetail.StaffMemoFlag2," +
                                            "H_VehicleDispatchDetail.StaffMemo2," +
                                            "H_VehicleDispatchDetail.StaffCode3," +
                                            "H_StaffMaster3.DisplayName AS StaffName3," +
                                            "H_StaffMaster3.Belongs AS StaffBelongs3," +
                                            "H_StaffMaster3.JobForm AS StaffJobform3," +
                                            "H_VehicleDispatchDetail.StaffOccupation3," +
                                            "H_VehicleDispatchDetail.StaffProxyFlag3," +
                                            "H_VehicleDispatchDetail.StaffRollCallFlag3," +
                                            "H_VehicleDispatchDetail.StaffRollCallYmdHms3," +
                                            "H_VehicleDispatchDetail.StaffMemoFlag3," +
                                            "H_VehicleDispatchDetail.StaffMemo3," +
                                            "H_VehicleDispatchDetail.StaffCode4," +
                                            "H_StaffMaster4.DisplayName AS StaffName4," +
                                            "H_StaffMaster4.Belongs AS StaffBelongs4," +
                                            "H_StaffMaster4.JobForm AS StaffJobform4," +
                                            "H_VehicleDispatchDetail.StaffOccupation4," +
                                            "H_VehicleDispatchDetail.StaffProxyFlag4," +
                                            "H_VehicleDispatchDetail.StaffRollCallFlag4," +
                                            "H_VehicleDispatchDetail.StaffRollCallYmdHms4," +
                                            "H_VehicleDispatchDetail.StaffMemoFlag4," +
                                            "H_VehicleDispatchDetail.StaffMemo4 " +
                                     "FROM H_VehicleDispatchDetail " +
                                     "LEFT OUTER JOIN H_SetMaster ON H_VehicleDispatchDetail.SetCode = H_SetMaster.SetCode " +
                                     "WHERE H_VehicleDispatchDetail.OperationDate BETWEEN '" + operationDate1.ToString("yyyy-MM-dd") + "' AND '" + operationDate2.ToString("yyyy-MM-dd") + "' " +
                                       "AND " + staffCode.ToString("####0") + "IN (H_VehicleDispatchDetail.StaffCode1,H_VehicleDispatchDetail.StaffCode2,H_VehicleDispatchDetail.StaffCode3,H_VehicleDispatchDetail.StaffCode4) " +
                                       "AND H_VehicleDispatchDetail.OperationFlag = 'true'";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    StaffDestinationVo staffDestinationVo = new();
                    staffDestinationVo.OperationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["OperationDate"]);
                    staffDestinationVo.SetCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["SetCode"]);
                    staffDestinationVo.SetName = _defaultValue.GetDefaultValue<string>(sqlDataReader["SetName"]);
                    staffDestinationVo.StaffCode1 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode1"]);
                    staffDestinationVo.StaffName1 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffName1"]);
                    staffDestinationVo.StaffBelongs1 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffBelongs1"]);
                    staffDestinationVo.StaffJobForm1 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffJobform1"]);
                    staffDestinationVo.StaffOccupation1 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffOccupation1"]);
                    staffDestinationVo.StaffProxyFlag1 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffProxyFlag1"]);
                    staffDestinationVo.StaffRollCallFlag1 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffRollCallFlag1"]);
                    staffDestinationVo.StaffRollCallYmdHms1 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StaffRollCallYmdHms1"]);
                    staffDestinationVo.StaffMemoFlag1 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffMemoFlag1"]);
                    staffDestinationVo.StaffMemo1 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffMemo1"]);
                    staffDestinationVo.StaffCode2 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode2"]);
                    staffDestinationVo.StaffName2 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffName2"]);
                    staffDestinationVo.StaffBelongs2 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffBelongs2"]);
                    staffDestinationVo.StaffJobForm2 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffJobform2"]);
                    staffDestinationVo.StaffOccupation2 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffOccupation2"]);
                    staffDestinationVo.StaffProxyFlag2 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffProxyFlag2"]);
                    staffDestinationVo.StaffRollCallFlag2 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffRollCallFlag2"]);
                    staffDestinationVo.StaffRollCallYmdHms2 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StaffRollCallYmdHms2"]);
                    staffDestinationVo.StaffMemoFlag2 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffMemoFlag2"]);
                    staffDestinationVo.StaffMemo2 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffMemo2"]);
                    staffDestinationVo.StaffCode3 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode3"]);
                    staffDestinationVo.StaffName3 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffName3"]);
                    staffDestinationVo.StaffBelongs3 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffBelongs3"]);
                    staffDestinationVo.StaffJobForm3 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffJobform3"]);
                    staffDestinationVo.StaffOccupation3 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffOccupation3"]);
                    staffDestinationVo.StaffProxyFlag3 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffProxyFlag3"]);
                    staffDestinationVo.StaffRollCallFlag3 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffRollCallFlag3"]);
                    staffDestinationVo.StaffRollCallYmdHms3 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StaffRollCallYmdHms3"]);
                    staffDestinationVo.StaffMemoFlag3 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffMemoFlag3"]);
                    staffDestinationVo.StaffMemo3 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffMemo3"]);
                    staffDestinationVo.StaffCode4 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode4"]);
                    staffDestinationVo.StaffName4 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffName4"]);
                    staffDestinationVo.StaffBelongs4 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffBelongs4"]);
                    staffDestinationVo.StaffJobForm4 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffJobform4"]);
                    staffDestinationVo.StaffOccupation4 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffOccupation4"]);
                    staffDestinationVo.StaffProxyFlag4 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffProxyFlag4"]);
                    staffDestinationVo.StaffRollCallFlag4 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffRollCallFlag4"]);
                    staffDestinationVo.StaffRollCallYmdHms4 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StaffRollCallYmdHms4"]);
                    staffDestinationVo.StaffMemoFlag4 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffMemoFlag4"]);
                    staffDestinationVo.StaffMemo4 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffMemo4"]);
                    listStaffDestinationVo.Add(staffDestinationVo);
                }
            }
            return listStaffDestinationVo;
        }

        /// <summary>
        /// StaffDestinationで使用(画面のColumnに合わせてある)
        /// </summary>
        /// <returns></returns>
        public List<StaffDestinationVo> SelectAllStaffDestinationVo(DateTime operationDate1, DateTime operationDate2, string sqlBelongs, string sqlJobForm, string sqlOccupation) {
            List<StaffDestinationVo> listStaffDestinationVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT H_VehicleDispatchDetail.OperationDate," +
                                            "H_SetMaster.SetCode," +
                                            "H_SetMaster.SetName," +
                                            "H_VehicleDispatchDetail.LastRollCallYmdHms," +
                                            "H_VehicleDispatchDetail.StaffCode1," +
                                            "H_StaffMaster1.DisplayName AS StaffName1," +
                                            "H_StaffMaster1.Belongs AS StaffBelongs1," +
                                            "H_StaffMaster1.JobForm AS StaffJobform1," +
                                            "H_VehicleDispatchDetail.StaffOccupation1," +
                                            "H_VehicleDispatchDetail.StaffProxyFlag1," +
                                            "H_VehicleDispatchDetail.StaffRollCallFlag1," +
                                            "H_VehicleDispatchDetail.StaffRollCallYmdHms1," +
                                            "H_VehicleDispatchDetail.StaffMemoFlag1," +
                                            "H_VehicleDispatchDetail.StaffMemo1," +
                                            "H_VehicleDispatchDetail.StaffCode2," +
                                            "H_StaffMaster2.DisplayName AS StaffName2," +
                                            "H_StaffMaster2.Belongs AS StaffBelongs2," +
                                            "H_StaffMaster2.JobForm AS StaffJobform2," +
                                            "H_VehicleDispatchDetail.StaffOccupation2," +
                                            "H_VehicleDispatchDetail.StaffProxyFlag2," +
                                            "H_VehicleDispatchDetail.StaffRollCallFlag2," +
                                            "H_VehicleDispatchDetail.StaffRollCallYmdHms2," +
                                            "H_VehicleDispatchDetail.StaffMemoFlag2," +
                                            "H_VehicleDispatchDetail.StaffMemo2," +
                                            "H_VehicleDispatchDetail.StaffCode3," +
                                            "H_StaffMaster3.DisplayName AS StaffName3," +
                                            "H_StaffMaster3.Belongs AS StaffBelongs3," +
                                            "H_StaffMaster3.JobForm AS StaffJobform3," +
                                            "H_VehicleDispatchDetail.StaffOccupation3," +
                                            "H_VehicleDispatchDetail.StaffProxyFlag3," +
                                            "H_VehicleDispatchDetail.StaffRollCallFlag3," +
                                            "H_VehicleDispatchDetail.StaffRollCallYmdHms3," +
                                            "H_VehicleDispatchDetail.StaffMemoFlag3," +
                                            "H_VehicleDispatchDetail.StaffMemo3," +
                                            "H_VehicleDispatchDetail.StaffCode4," +
                                            "H_StaffMaster4.DisplayName AS StaffName4," +
                                            "H_StaffMaster4.Belongs AS StaffBelongs4," +
                                            "H_StaffMaster4.JobForm AS StaffJobform4," +
                                            "H_VehicleDispatchDetail.StaffOccupation4," +
                                            "H_VehicleDispatchDetail.StaffProxyFlag4," +
                                            "H_VehicleDispatchDetail.StaffRollCallFlag4," +
                                            "H_VehicleDispatchDetail.StaffRollCallYmdHms4," +
                                            "H_VehicleDispatchDetail.StaffMemoFlag4," +
                                            "H_VehicleDispatchDetail.StaffMemo4 " +
                                     "FROM H_VehicleDispatchDetail " +
                                     "LEFT OUTER JOIN H_SetMaster ON H_VehicleDispatchDetail.SetCode = H_SetMaster.SetCode " +
                                     "LEFT OUTER JOIN H_StaffMaster AS H_StaffMaster1 ON H_VehicleDispatchDetail.StaffCode1 = H_StaffMaster1.StaffCode " +
                                     "LEFT OUTER JOIN H_StaffMaster AS H_StaffMaster2 ON H_VehicleDispatchDetail.StaffCode2 = H_StaffMaster2.StaffCode " +
                                     "LEFT OUTER JOIN H_StaffMaster AS H_StaffMaster3 ON H_VehicleDispatchDetail.StaffCode3 = H_StaffMaster3.StaffCode " +
                                     "LEFT OUTER JOIN H_StaffMaster AS H_StaffMaster4 ON H_VehicleDispatchDetail.StaffCode4 = H_StaffMaster4.StaffCode " +
                                     "WHERE H_VehicleDispatchDetail.OperationDate BETWEEN '" + operationDate1.ToString("yyyy-MM-dd") + "' AND '" + operationDate2.ToString("yyyy-MM-dd") + "' " +
                                       "AND H_VehicleDispatchDetail.OperationFlag = 'true' AND H_VehicleDispatchDetail.VehicleDispatchFlag = 'true' " +
                                       "AND H_StaffMaster1.Belongs IN (" + sqlBelongs + ") AND H_StaffMaster1.JobForm IN (" + sqlJobForm + ") AND H_StaffMaster1.Occupation IN (" + sqlOccupation + ") " +
                                       "AND H_StaffMaster2.Belongs IN (" + sqlBelongs + ") AND H_StaffMaster2.JobForm IN (" + sqlJobForm + ") AND H_StaffMaster2.Occupation IN (" + sqlOccupation + ") " +
                                       "AND H_StaffMaster3.Belongs IN (" + sqlBelongs + ") AND H_StaffMaster3.JobForm IN (" + sqlJobForm + ") AND H_StaffMaster3.Occupation IN (" + sqlOccupation + ") " +
                                       "AND H_StaffMaster4.Belongs IN (" + sqlBelongs + ") AND H_StaffMaster4.JobForm IN (" + sqlJobForm + ") AND H_StaffMaster4.Occupation IN (" + sqlOccupation + ")";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    StaffDestinationVo staffDestinationVo = new();
                    staffDestinationVo.OperationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["OperationDate"]);
                    staffDestinationVo.SetCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["SetCode"]);
                    staffDestinationVo.SetName = _defaultValue.GetDefaultValue<string>(sqlDataReader["SetName"]);
                    staffDestinationVo.StaffCode1 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode1"]);
                    staffDestinationVo.StaffName1 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffName1"]);
                    staffDestinationVo.StaffBelongs1 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffBelongs1"]);
                    staffDestinationVo.StaffJobForm1 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffJobform1"]);
                    staffDestinationVo.StaffOccupation1 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffOccupation1"]);
                    staffDestinationVo.StaffProxyFlag1 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffProxyFlag1"]);
                    staffDestinationVo.StaffRollCallFlag1 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffRollCallFlag1"]);
                    staffDestinationVo.StaffRollCallYmdHms1 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StaffRollCallYmdHms1"]);
                    staffDestinationVo.StaffMemoFlag1 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffMemoFlag1"]);
                    staffDestinationVo.StaffMemo1 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffMemo1"]);
                    staffDestinationVo.StaffCode2 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode2"]);
                    staffDestinationVo.StaffName2 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffName2"]);
                    staffDestinationVo.StaffBelongs2 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffBelongs2"]);
                    staffDestinationVo.StaffJobForm2 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffJobform2"]);
                    staffDestinationVo.StaffOccupation2 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffOccupation2"]);
                    staffDestinationVo.StaffProxyFlag2 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffProxyFlag2"]);
                    staffDestinationVo.StaffRollCallFlag2 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffRollCallFlag2"]);
                    staffDestinationVo.StaffRollCallYmdHms2 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StaffRollCallYmdHms2"]);
                    staffDestinationVo.StaffMemoFlag2 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffMemoFlag2"]);
                    staffDestinationVo.StaffMemo2 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffMemo2"]);
                    staffDestinationVo.StaffCode3 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode3"]);
                    staffDestinationVo.StaffName3 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffName3"]);
                    staffDestinationVo.StaffBelongs3 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffBelongs3"]);
                    staffDestinationVo.StaffJobForm3 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffJobform3"]);
                    staffDestinationVo.StaffOccupation3 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffOccupation3"]);
                    staffDestinationVo.StaffProxyFlag3 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffProxyFlag3"]);
                    staffDestinationVo.StaffRollCallFlag3 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffRollCallFlag3"]);
                    staffDestinationVo.StaffRollCallYmdHms3 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StaffRollCallYmdHms3"]);
                    staffDestinationVo.StaffMemoFlag3 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffMemoFlag3"]);
                    staffDestinationVo.StaffMemo3 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffMemo3"]);
                    staffDestinationVo.StaffCode4 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffCode4"]);
                    staffDestinationVo.StaffName4 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffName4"]);
                    staffDestinationVo.StaffBelongs4 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffBelongs4"]);
                    staffDestinationVo.StaffJobForm4 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffJobform4"]);
                    staffDestinationVo.StaffOccupation4 = _defaultValue.GetDefaultValue<int>(sqlDataReader["StaffOccupation4"]);
                    staffDestinationVo.StaffProxyFlag4 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffProxyFlag4"]);
                    staffDestinationVo.StaffRollCallFlag4 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffRollCallFlag4"]);
                    staffDestinationVo.StaffRollCallYmdHms4 = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["StaffRollCallYmdHms4"]);
                    staffDestinationVo.StaffMemoFlag4 = _defaultValue.GetDefaultValue<bool>(sqlDataReader["StaffMemoFlag4"]);
                    staffDestinationVo.StaffMemo4 = _defaultValue.GetDefaultValue<string>(sqlDataReader["StaffMemo4"]);
                    listStaffDestinationVo.Add(staffDestinationVo);
                }
            }
            return listStaffDestinationVo;
        }

        
    }
}
