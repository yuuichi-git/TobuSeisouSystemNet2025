/*
 * 2023-11-06
 */
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class SetMasterDao {
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionVo"></param>
        public SetMasterDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// SelectAllSetMaster
        /// </summary>
        /// <returns></returns>
        public List<SetMasterVo> SelectAllSetMaster() {
            List<SetMasterVo> listSetMasterVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT SetCode," +
                                            "WordCode," +
                                            "SetName," +
                                            "SetName1," +
                                            "SetName2," +
                                            "FareCode," +
                                            "ManagedSpaceCode," +
                                            "ClassificationCode," +
                                            "ContactMethod," +
                                            "NumberOfPeople," +
                                            "SpareOfPeople," +
                                            "WorkingDays," +
                                            "FiveLap," +
                                            "MoveFlag," +
                                            "Remarks," +
                                            "TelephoneNumber," +
                                            "FaxNumber," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_SetMaster";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    SetMasterVo hSetMasterVo = new();
                    hSetMasterVo.SetCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["SetCode"]);
                    hSetMasterVo.WordCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["WordCode"]);
                    hSetMasterVo.SetName = _defaultValue.GetDefaultValue<string>(sqlDataReader["SetName"]);
                    hSetMasterVo.SetName1 = _defaultValue.GetDefaultValue<string>(sqlDataReader["SetName1"]);
                    hSetMasterVo.SetName2 = _defaultValue.GetDefaultValue<string>(sqlDataReader["SetName2"]);
                    hSetMasterVo.FareCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["FareCode"]);
                    hSetMasterVo.ManagedSpaceCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["ManagedSpaceCode"]);
                    hSetMasterVo.ClassificationCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["ClassificationCode"]);
                    hSetMasterVo.ContactMethod = _defaultValue.GetDefaultValue<int>(sqlDataReader["ContactMethod"]);
                    hSetMasterVo.NumberOfPeople = _defaultValue.GetDefaultValue<int>(sqlDataReader["NumberOfPeople"]);
                    hSetMasterVo.SpareOfPeople = _defaultValue.GetDefaultValue<bool>(sqlDataReader["SpareOfPeople"]);
                    hSetMasterVo.WorkingDays = _defaultValue.GetDefaultValue<string>(sqlDataReader["WorkingDays"]);
                    hSetMasterVo.FiveLap = _defaultValue.GetDefaultValue<bool>(sqlDataReader["FiveLap"]);
                    hSetMasterVo.MoveFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["MoveFlag"]);
                    hSetMasterVo.Remarks = _defaultValue.GetDefaultValue<string>(sqlDataReader["Remarks"]);
                    hSetMasterVo.TelephoneNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["TelephoneNumber"]);
                    hSetMasterVo.FaxNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["FaxNumber"]);
                    hSetMasterVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    hSetMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    hSetMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    hSetMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    hSetMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    hSetMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    hSetMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listSetMasterVo.Add(hSetMasterVo);
                }
            }
            return listSetMasterVo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setCode"></param>
        /// <returns></returns>
        public SetMasterVo SelectOneSetMaster(int setCode) {
            SetMasterVo setMasterVo = new();
            SqlCommand sqlCommand = _connectionVo.SqlServerConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT SetCode," +
                                            "WordCode," +
                                            "SetName," +
                                            "SetName1," +
                                            "SetName2," +
                                            "FareCode," +
                                            "ManagedSpaceCode," +
                                            "ClassificationCode," +
                                            "ContactMethod," +
                                            "NumberOfPeople," +
                                            "SpareOfPeople," +
                                            "WorkingDays," +
                                            "FiveLap," +
                                            "MoveFlag," +
                                            "Remarks," +
                                            "TelephoneNumber," +
                                            "FaxNumber," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_SetMaster " +
                                     "WHERE SetCode = '" + setCode + "'";
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    setMasterVo.SetCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["SetCode"]);
                    setMasterVo.WordCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["WordCode"]);
                    setMasterVo.SetName = _defaultValue.GetDefaultValue<string>(sqlDataReader["SetName"]);
                    setMasterVo.SetName1 = _defaultValue.GetDefaultValue<string>(sqlDataReader["SetName1"]);
                    setMasterVo.SetName2 = _defaultValue.GetDefaultValue<string>(sqlDataReader["SetName2"]);
                    setMasterVo.FareCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["FareCode"]);
                    setMasterVo.ManagedSpaceCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["ManagedSpaceCode"]);
                    setMasterVo.ClassificationCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["ClassificationCode"]);
                    setMasterVo.ContactMethod = _defaultValue.GetDefaultValue<int>(sqlDataReader["ContactMethod"]);
                    setMasterVo.NumberOfPeople = _defaultValue.GetDefaultValue<int>(sqlDataReader["NumberOfPeople"]);
                    setMasterVo.SpareOfPeople = _defaultValue.GetDefaultValue<bool>(sqlDataReader["SpareOfPeople"]);
                    setMasterVo.WorkingDays = _defaultValue.GetDefaultValue<string>(sqlDataReader["WorkingDays"]);
                    setMasterVo.FiveLap = _defaultValue.GetDefaultValue<bool>(sqlDataReader["FiveLap"]);
                    setMasterVo.MoveFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["MoveFlag"]);
                    setMasterVo.Remarks = _defaultValue.GetDefaultValue<string>(sqlDataReader["Remarks"]);
                    setMasterVo.TelephoneNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["TelephoneNumber"]);
                    setMasterVo.FaxNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["FaxNumber"]);
                    setMasterVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    setMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    setMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    setMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    setMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    setMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    setMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                }
            }
            return setMasterVo;
        }
    }
}
