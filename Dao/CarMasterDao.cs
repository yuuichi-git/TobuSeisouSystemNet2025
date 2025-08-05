/*
 * 2023-11-10
 */
using System.Data;
using System.Data.SqlClient;

using Common;

using Vo;

namespace Dao {
    public class CarMasterDao {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private readonly DefaultValue _defaultValue = new();
        /*
         * Vo
         */
        private readonly ConnectionVo _connectionVo;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="connectionVo"></param>
        public CarMasterDao(ConnectionVo connectionVo) {
            /*
             * Vo
             */
            _connectionVo = connectionVo;
        }

        /// <summary>
        /// ExistenceHCarMasterRecord
        /// true:該当レコードあり false:該当レコードなし
        /// </summary>
        /// <param name="carCode"></param>
        /// <returns></returns>
        public bool ExistenceHCarMaster(int carCode) {
            int count;
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT COUNT(CarCode) " +
                                     "FROM H_CarMaster " +
                                     "WHERE CarCode = " + carCode;
            try {
                count = (int)sqlCommand.ExecuteScalar();
            } catch {
                throw;
            }
            return count != 0 ? true : false;
        }

        /// <summary>
        /// 新規CarCodeを採番
        /// </summary>
        /// <returns>CarCodeの最大値</returns>
        public int GetCarCode() {
            var sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT MAX(CarCode) " +
                                     "FROM H_CarMaster";
            try {
                return (int)sqlCommand.ExecuteScalar();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// SelectAllHCarMaster
        /// Picture無
        /// </summary>
        /// <returns></returns>
        public List<CarMasterVo> SelectAllCarMaster() {
            List<CarMasterVo> listCarMasterVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT CarCode," +
                                            "ClassificationCode," +
                                            "RegistrationNumber," +
                                            "RegistrationNumber1," +
                                            "RegistrationNumber2," +
                                            "RegistrationNumber3," +
                                            "RegistrationNumber4," +
                                            "GarageCode," +
                                            "DoorNumber," +
                                            "RegistrationDate," +
                                            "FirstRegistrationDate," +
                                            "CarKindCode," +
                                            "DisguiseKind1," +
                                            "DisguiseKind2," +
                                            "DisguiseKind3," +
                                            "CarUse," +
                                            "OtherCode," +
                                            "ShapeCode," +
                                            "ManufacturerCode," +
                                            "Capacity," +
                                            "MaximumLoadCapacity," +
                                            "VehicleWeight," +
                                            "TotalVehicleWeight," +
                                            "VehicleNumber," +
                                            "Length," +
                                            "Width," +
                                            "Height," +
                                            "FfAxisWeight," +
                                            "FrAxisWeight," +
                                            "RfAxisWeight," +
                                            "RrAxisWeight," +
                                            "Version," +
                                            "MotorVersion," +
                                            "TotalDisplacement," +
                                            "TypesOfFuel," +
                                            "VersionDesignateNumber," +
                                            "CategoryDistinguishNumber," +
                                            "OwnerName," +
                                            "OwnerAddress," +
                                            "UserName," +
                                            "UserAddress," +
                                            "BaseAddress," +
                                            "ExpirationDate," +
                                            "Remarks," +
                                            //"MainPicture," +
                                            //"SubPicture," +
                                            "EmergencyVehicleFlag," +
                                            "EmergencyVehicleDate," +
                                            "DigitalTachographFlag," +
                                            "DigitalTachographType," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_CarMaster";
            // "WHERE delete_flag = 'False' " + // 2022-07-08 delete_flagを入れると過去の配車に削除済のCarLabelが反映出来なくなる
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    CarMasterVo carMasterVo = new();
                    carMasterVo.CarCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["CarCode"]);
                    carMasterVo.ClassificationCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["ClassificationCode"]);
                    carMasterVo.RegistrationNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["RegistrationNumber"]);
                    carMasterVo.RegistrationNumber1 = _defaultValue.GetDefaultValue<string>(sqlDataReader["RegistrationNumber1"]);
                    carMasterVo.RegistrationNumber2 = _defaultValue.GetDefaultValue<string>(sqlDataReader["RegistrationNumber2"]);
                    carMasterVo.RegistrationNumber3 = _defaultValue.GetDefaultValue<string>(sqlDataReader["RegistrationNumber3"]);
                    carMasterVo.RegistrationNumber4 = _defaultValue.GetDefaultValue<string>(sqlDataReader["RegistrationNumber4"]);
                    carMasterVo.GarageCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["GarageCode"]);
                    carMasterVo.DoorNumber = _defaultValue.GetDefaultValue<int>(sqlDataReader["DoorNumber"]);
                    carMasterVo.RegistrationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["RegistrationDate"]);
                    carMasterVo.FirstRegistrationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["FirstRegistrationDate"]);
                    carMasterVo.CarKindCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["CarKindCode"]);
                    carMasterVo.DisguiseKind1 = _defaultValue.GetDefaultValue<string>(sqlDataReader["DisguiseKind1"]);
                    carMasterVo.DisguiseKind2 = _defaultValue.GetDefaultValue<string>(sqlDataReader["DisguiseKind2"]);
                    carMasterVo.DisguiseKind3 = _defaultValue.GetDefaultValue<string>(sqlDataReader["DisguiseKind3"]);
                    carMasterVo.CarUse = _defaultValue.GetDefaultValue<string>(sqlDataReader["CarUse"]);
                    carMasterVo.OtherCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["OtherCode"]);
                    carMasterVo.ShapeCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["ShapeCode"]);
                    carMasterVo.ManufacturerCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["ManufacturerCode"]);
                    carMasterVo.Capacity = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["Capacity"]);
                    carMasterVo.MaximumLoadCapacity = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["MaximumLoadCapacity"]);
                    carMasterVo.VehicleWeight = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["VehicleWeight"]);
                    carMasterVo.TotalVehicleWeight = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["TotalVehicleWeight"]);
                    carMasterVo.VehicleNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["VehicleNumber"]);
                    carMasterVo.Length = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["Length"]);
                    carMasterVo.Width = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["Width"]);
                    carMasterVo.Height = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["Height"]);
                    carMasterVo.FfAxisWeight = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["FfAxisWeight"]);
                    carMasterVo.FrAxisWeight = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["FrAxisWeight"]);
                    carMasterVo.RfAxisWeight = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["RfAxisWeight"]);
                    carMasterVo.RrAxisWeight = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["RrAxisWeight"]);
                    carMasterVo.Version = _defaultValue.GetDefaultValue<string>(sqlDataReader["Version"]);
                    carMasterVo.MotorVersion = _defaultValue.GetDefaultValue<string>(sqlDataReader["MotorVersion"]);
                    carMasterVo.TotalDisplacement = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["TotalDisplacement"]);
                    carMasterVo.TypesOfFuel = _defaultValue.GetDefaultValue<string>(sqlDataReader["TypesOfFuel"]);
                    carMasterVo.VersionDesignateNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["VersionDesignateNumber"]);
                    carMasterVo.CategoryDistinguishNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["CategoryDistinguishNumber"]);
                    carMasterVo.OwnerName = _defaultValue.GetDefaultValue<string>(sqlDataReader["OwnerName"]);
                    carMasterVo.OwnerAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["OwnerAddress"]);
                    carMasterVo.UserName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UserName"]);
                    carMasterVo.UserAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["UserAddress"]);
                    carMasterVo.BaseAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["BaseAddress"]);
                    carMasterVo.ExpirationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ExpirationDate"]);
                    carMasterVo.Remarks = _defaultValue.GetDefaultValue<string>(sqlDataReader["Remarks"]);
                    //hCarMasterVo.MainPicture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["MainPicture"]);
                    //hCarMasterVo.SubPicture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["SubPicture"]);
                    carMasterVo.EmergencyVehicleFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["EmergencyVehicleFlag"]);
                    carMasterVo.EmergencyVehicleDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["EmergencyVehicleDate"]);
                    carMasterVo.DigitalTachographFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DigitalTachographFlag"]);
                    carMasterVo.DigitalTachographType = _defaultValue.GetDefaultValue<string>(sqlDataReader["DigitalTachographType"]);
                    carMasterVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    carMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    carMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    carMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    carMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    carMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    carMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                    listCarMasterVo.Add(carMasterVo);
                }
            }
            return listCarMasterVo;
        }

        /// <summary>
        /// SelectOneMainPicture
        /// </summary>
        /// <param name="carCode"></param>
        /// <returns></returns>
        public byte[] SelectOneMainPicture(int carCode) {
            byte[] byteImage = Array.Empty<byte>();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT MainPicture " +
                                     "FROM H_CarMaster " +
                                     "WHERE CarCode = " + carCode + "";
            using (var sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    byteImage = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["MainPicture"]);
                }
            }
            return byteImage;
        }

        /// <summary>
        /// SelectOneSubPicture
        /// </summary>
        /// <param name="carCode"></param>
        /// <returns></returns>
        public byte[] SelectOneSubPicture(int carCode) {
            byte[] byteImage = Array.Empty<byte>();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT SubPicture " +
                                     "FROM H_CarMaster " +
                                     "WHERE CarCode = " + carCode + "";
            using (var sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    byteImage = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["SubPicture"]);
                }
            }
            return byteImage;
        }

        /// <summary>
        /// SelectOneHCarMaster
        /// Picture有
        /// </summary>
        /// <param name="carCode"></param>
        /// <returns></returns>
        public CarMasterVo SelectOneCarMasterP(int carCode) {
            CarMasterVo carMasterVo = new();
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "SELECT CarCode," +
                                            "ClassificationCode," +
                                            "RegistrationNumber," +
                                            "RegistrationNumber1," +
                                            "RegistrationNumber2," +
                                            "RegistrationNumber3," +
                                            "RegistrationNumber4," +
                                            "GarageCode," +
                                            "DoorNumber," +
                                            "RegistrationDate," +
                                            "FirstRegistrationDate," +
                                            "CarKindCode," +
                                            "DisguiseKind1," +
                                            "DisguiseKind2," +
                                            "DisguiseKind3," +
                                            "CarUse," +
                                            "OtherCode," +
                                            "ShapeCode," +
                                            "ManufacturerCode," +
                                            "Capacity," +
                                            "MaximumLoadCapacity," +
                                            "VehicleWeight," +
                                            "TotalVehicleWeight," +
                                            "VehicleNumber," +
                                            "Length," +
                                            "Width," +
                                            "Height," +
                                            "FfAxisWeight," +
                                            "FrAxisWeight," +
                                            "RfAxisWeight," +
                                            "RrAxisWeight," +
                                            "Version," +
                                            "MotorVersion," +
                                            "TotalDisplacement," +
                                            "TypesOfFuel," +
                                            "VersionDesignateNumber," +
                                            "CategoryDistinguishNumber," +
                                            "OwnerName," +
                                            "OwnerAddress," +
                                            "UserName," +
                                            "UserAddress," +
                                            "BaseAddress," +
                                            "ExpirationDate," +
                                            "Remarks," +
                                            "MainPicture," +
                                            "SubPicture," +
                                            "EmergencyVehicleFlag," +
                                            "EmergencyVehicleDate," +
                                            "DigitalTachographFlag," +
                                            "DigitalTachographType," +
                                            "InsertPcName," +
                                            "InsertYmdHms," +
                                            "UpdatePcName," +
                                            "UpdateYmdHms," +
                                            "DeletePcName," +
                                            "DeleteYmdHms," +
                                            "DeleteFlag " +
                                     "FROM H_CarMaster " +
                                     "WHERE CarCode = " + carCode + "";
            // "WHERE delete_flag = 'False' " + // 2022-07-08 delete_flagを入れると過去の配車に削除済のCarLabelが反映出来なくなる
            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader()) {
                while (sqlDataReader.Read() == true) {
                    carMasterVo.CarCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["CarCode"]);
                    carMasterVo.ClassificationCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["ClassificationCode"]);
                    carMasterVo.RegistrationNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["RegistrationNumber"]);
                    carMasterVo.RegistrationNumber1 = _defaultValue.GetDefaultValue<string>(sqlDataReader["RegistrationNumber1"]);
                    carMasterVo.RegistrationNumber2 = _defaultValue.GetDefaultValue<string>(sqlDataReader["RegistrationNumber2"]);
                    carMasterVo.RegistrationNumber3 = _defaultValue.GetDefaultValue<string>(sqlDataReader["RegistrationNumber3"]);
                    carMasterVo.RegistrationNumber4 = _defaultValue.GetDefaultValue<string>(sqlDataReader["RegistrationNumber4"]);
                    carMasterVo.GarageCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["GarageCode"]);
                    carMasterVo.DoorNumber = _defaultValue.GetDefaultValue<int>(sqlDataReader["DoorNumber"]);
                    carMasterVo.RegistrationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["RegistrationDate"]);
                    carMasterVo.FirstRegistrationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["FirstRegistrationDate"]);
                    carMasterVo.CarKindCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["CarKindCode"]);
                    carMasterVo.DisguiseKind1 = _defaultValue.GetDefaultValue<string>(sqlDataReader["DisguiseKind1"]);
                    carMasterVo.DisguiseKind2 = _defaultValue.GetDefaultValue<string>(sqlDataReader["DisguiseKind2"]);
                    carMasterVo.DisguiseKind3 = _defaultValue.GetDefaultValue<string>(sqlDataReader["DisguiseKind3"]);
                    carMasterVo.CarUse = _defaultValue.GetDefaultValue<string>(sqlDataReader["CarUse"]);
                    carMasterVo.OtherCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["OtherCode"]);
                    carMasterVo.ShapeCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["ShapeCode"]);
                    carMasterVo.ManufacturerCode = _defaultValue.GetDefaultValue<int>(sqlDataReader["ManufacturerCode"]);
                    carMasterVo.Capacity = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["Capacity"]);
                    carMasterVo.MaximumLoadCapacity = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["MaximumLoadCapacity"]);
                    carMasterVo.VehicleWeight = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["VehicleWeight"]);
                    carMasterVo.TotalVehicleWeight = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["TotalVehicleWeight"]);
                    carMasterVo.VehicleNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["VehicleNumber"]);
                    carMasterVo.Length = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["Length"]);
                    carMasterVo.Width = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["Width"]);
                    carMasterVo.Height = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["Height"]);
                    carMasterVo.FfAxisWeight = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["FfAxisWeight"]);
                    carMasterVo.FrAxisWeight = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["FrAxisWeight"]);
                    carMasterVo.RfAxisWeight = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["RfAxisWeight"]);
                    carMasterVo.RrAxisWeight = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["RrAxisWeight"]);
                    carMasterVo.Version = _defaultValue.GetDefaultValue<string>(sqlDataReader["Version"]);
                    carMasterVo.MotorVersion = _defaultValue.GetDefaultValue<string>(sqlDataReader["MotorVersion"]);
                    carMasterVo.TotalDisplacement = _defaultValue.GetDefaultValue<decimal>(sqlDataReader["TotalDisplacement"]);
                    carMasterVo.TypesOfFuel = _defaultValue.GetDefaultValue<string>(sqlDataReader["TypesOfFuel"]);
                    carMasterVo.VersionDesignateNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["VersionDesignateNumber"]);
                    carMasterVo.CategoryDistinguishNumber = _defaultValue.GetDefaultValue<string>(sqlDataReader["CategoryDistinguishNumber"]);
                    carMasterVo.OwnerName = _defaultValue.GetDefaultValue<string>(sqlDataReader["OwnerName"]);
                    carMasterVo.OwnerAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["OwnerAddress"]);
                    carMasterVo.UserName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UserName"]);
                    carMasterVo.UserAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["UserAddress"]);
                    carMasterVo.BaseAddress = _defaultValue.GetDefaultValue<string>(sqlDataReader["BaseAddress"]);
                    carMasterVo.ExpirationDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["ExpirationDate"]);
                    carMasterVo.Remarks = _defaultValue.GetDefaultValue<string>(sqlDataReader["Remarks"]);
                    carMasterVo.MainPicture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["MainPicture"]);
                    carMasterVo.SubPicture = _defaultValue.GetDefaultValue<byte[]>(sqlDataReader["SubPicture"]);
                    carMasterVo.EmergencyVehicleFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["EmergencyVehicleFlag"]);
                    carMasterVo.EmergencyVehicleDate = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["EmergencyVehicleDate"]);
                    carMasterVo.DigitalTachographFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DigitalTachographFlag"]);
                    carMasterVo.DigitalTachographType = _defaultValue.GetDefaultValue<string>(sqlDataReader["DigitalTachographType"]);
                    carMasterVo.InsertPcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["InsertPcName"]);
                    carMasterVo.InsertYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["InsertYmdHms"]);
                    carMasterVo.UpdatePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["UpdatePcName"]);
                    carMasterVo.UpdateYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["UpdateYmdHms"]);
                    carMasterVo.DeletePcName = _defaultValue.GetDefaultValue<string>(sqlDataReader["DeletePcName"]);
                    carMasterVo.DeleteYmdHms = _defaultValue.GetDefaultValue<DateTime>(sqlDataReader["DeleteYmdHms"]);
                    carMasterVo.DeleteFlag = _defaultValue.GetDefaultValue<bool>(sqlDataReader["DeleteFlag"]);
                }
            }
            return carMasterVo;
        }

        /// <summary>
        /// InsertOneHCarMaster
        /// </summary>
        /// <param name="carMasterVo"></param>
        public void InsertOneCarMaster(CarMasterVo carMasterVo) {
            var sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO H_CarMaster(CarCode," +
                                                             "ClassificationCode," +
                                                             "RegistrationNumber," +
                                                             "RegistrationNumber1," +
                                                             "RegistrationNumber2," +
                                                             "RegistrationNumber3," +
                                                             "RegistrationNumber4," +
                                                             "GarageCode," +
                                                             "DoorNumber," +
                                                             "RegistrationDate," +
                                                             "FirstRegistrationDate," +
                                                             "CarKindCode," +
                                                             "DisguiseKind1," +
                                                             "DisguiseKind2," +
                                                             "DisguiseKind3," +
                                                             "CarUse," +
                                                             "OtherCode," +
                                                             "ShapeCode," +
                                                             "ManufacturerCode," +
                                                             "Capacity," +
                                                             "MaximumLoadCapacity," +
                                                             "VehicleWeight," +
                                                             "TotalVehicleWeight," +
                                                             "VehicleNumber," +
                                                             "Length," +
                                                             "Width," +
                                                             "Height," +
                                                             "FfAxisWeight," +
                                                             "FrAxisWeight," +
                                                             "RfAxisWeight," +
                                                             "RrAxisWeight," +
                                                             "Version," +
                                                             "MotorVersion," +
                                                             "TotalDisplacement," +
                                                             "TypesOfFuel," +
                                                             "VersionDesignateNumber," +
                                                             "CategoryDistinguishNumber," +
                                                             "OwnerName," +
                                                             "OwnerAddress," +
                                                             "UserName," +
                                                             "UserAddress," +
                                                             "BaseAddress," +
                                                             "ExpirationDate," +
                                                             "Remarks," +
                                                             "MainPicture," +
                                                             "SubPicture," +
                                                             "EmergencyVehicleFlag," +
                                                             "EmergencyVehicleDate," +
                                                             "DigitalTachographFlag," +
                                                             "DigitalTachographType," +
                                                             "InsertPcName," +
                                                             "InsertYmdHms," +
                                                             "UpdatePcName," +
                                                             "UpdateYmdHms," +
                                                             "DeletePcName," +
                                                             "DeleteYmdHms," +
                                                             "DeleteFlag) " +
                                     "VALUES (" + carMasterVo.CarCode + "," +
                                             "" + carMasterVo.ClassificationCode + "," +
                                            "'" + carMasterVo.RegistrationNumber + "'," +
                                            "'" + carMasterVo.RegistrationNumber1 + "'," +
                                            "'" + carMasterVo.RegistrationNumber2 + "'," +
                                            "'" + carMasterVo.RegistrationNumber3 + "'," +
                                            "'" + carMasterVo.RegistrationNumber4 + "'," +
                                             "" + carMasterVo.GarageCode + "," +
                                             "" + carMasterVo.DoorNumber + "," +
                                            "'" + carMasterVo.RegistrationDate + "'," +
                                            "'" + carMasterVo.FirstRegistrationDate + "'," +
                                             "" + carMasterVo.CarKindCode + "," +
                                            "'" + carMasterVo.DisguiseKind1 + "'," +
                                            "'" + carMasterVo.DisguiseKind2 + "'," +
                                            "'" + carMasterVo.DisguiseKind3 + "'," +
                                            "'" + carMasterVo.CarUse + "'," +
                                             "" + carMasterVo.OtherCode + "," +
                                             "" + carMasterVo.ShapeCode + "," +
                                             "" + carMasterVo.ManufacturerCode + "," +
                                             "" + carMasterVo.Capacity + "," +
                                             "" + carMasterVo.MaximumLoadCapacity + "," +
                                             "" + carMasterVo.VehicleWeight + "," +
                                             "" + carMasterVo.TotalVehicleWeight + "," +
                                            "'" + carMasterVo.VehicleNumber + "'," +
                                             "" + carMasterVo.Length + "," +
                                             "" + carMasterVo.Width + "," +
                                             "" + carMasterVo.Height + "," +
                                             "" + carMasterVo.FfAxisWeight + "," +
                                             "" + carMasterVo.FrAxisWeight + "," +
                                             "" + carMasterVo.RfAxisWeight + "," +
                                             "" + carMasterVo.RrAxisWeight + "," +
                                            "'" + carMasterVo.Version + "'," +
                                            "'" + carMasterVo.MotorVersion + "'," +
                                             "" + carMasterVo.TotalDisplacement + "," +
                                            "'" + carMasterVo.TypesOfFuel + "'," +
                                            "'" + carMasterVo.VersionDesignateNumber + "'," +
                                            "'" + carMasterVo.CategoryDistinguishNumber + "'," +
                                            "'" + carMasterVo.OwnerName + "'," +
                                            "'" + carMasterVo.OwnerAddress + "'," +
                                            "'" + carMasterVo.UserName + "'," +
                                            "'" + carMasterVo.UserAddress + "'," +
                                            "'" + carMasterVo.BaseAddress + "'," +
                                            "'" + carMasterVo.ExpirationDate + "'," +
                                            "'" + carMasterVo.Remarks + "'," +
                                            "@member_MainPicture," +
                                            "@member_SubPicture," +
                                             "'false'," +
                                            "'" + _defaultDateTime + "'," +
                                            "'" + carMasterVo.DigitalTachographFlag + "'," +
                                            "'" + carMasterVo.DigitalTachographType + "'," +
                                            "'" + Environment.MachineName + "'," +
                                            "'" + DateTime.Now + "'," +
                                            "'" + string.Empty + "'," +
                                            "'" + _defaultDateTime + "'," +
                                            "'" + string.Empty + "'," +
                                            "'" + _defaultDateTime + "'," +
                                             "'false'" +
                                             ");";
            try {
                sqlCommand.Parameters.Add("@member_MainPicture", SqlDbType.Image, carMasterVo.MainPicture.Length).Value = carMasterVo.MainPicture;
                sqlCommand.Parameters.Add("@member_SubPicture", SqlDbType.Image, carMasterVo.SubPicture.Length).Value = carMasterVo.SubPicture;
                sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// UpdateOneHStaffMaster
        /// </summary>
        /// <param name="carMasterVo"></param>
        /// <returns></returns>
        public void UpdateOneCarMaster(CarMasterVo carMasterVo) {
            var sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_CarMaster " +
                                     "SET CarCode = " + carMasterVo.CarCode + "," +
                                         "ClassificationCode = " + carMasterVo.ClassificationCode + "," +
                                         "RegistrationNumber = '" + carMasterVo.RegistrationNumber + "'," +
                                         "RegistrationNumber1 = '" + carMasterVo.RegistrationNumber1 + "'," +
                                         "RegistrationNumber2 = '" + carMasterVo.RegistrationNumber2 + "'," +
                                         "RegistrationNumber3 = '" + carMasterVo.RegistrationNumber3 + "'," +
                                         "RegistrationNumber4 = '" + carMasterVo.RegistrationNumber4 + "'," +
                                         "GarageCode = " + carMasterVo.GarageCode + "," +
                                         "DoorNumber = " + carMasterVo.DoorNumber + "," +
                                         "RegistrationDate = '" + carMasterVo.RegistrationDate + "'," +
                                         "FirstRegistrationDate = '" + carMasterVo.FirstRegistrationDate + "'," +
                                         "CarKindCode = " + carMasterVo.CarKindCode + "," +
                                         "DisguiseKind1 = '" + carMasterVo.DisguiseKind1 + "'," +
                                         "DisguiseKind2 = '" + carMasterVo.DisguiseKind2 + "'," +
                                         "DisguiseKind3 = '" + carMasterVo.DisguiseKind3 + "'," +
                                         "CarUse = '" + carMasterVo.CarUse + "'," +
                                         "OtherCode = " + carMasterVo.OtherCode + "," +
                                         "ShapeCode = " + carMasterVo.ShapeCode + "," +
                                         "ManufacturerCode = " + carMasterVo.ManufacturerCode + "," +
                                         "Capacity = " + carMasterVo.Capacity + "," +
                                         "MaximumLoadCapacity = " + carMasterVo.MaximumLoadCapacity + "," +
                                         "VehicleWeight = " + carMasterVo.VehicleWeight + "," +
                                         "TotalVehicleWeight = " + carMasterVo.TotalVehicleWeight + "," +
                                         "VehicleNumber = '" + carMasterVo.VehicleNumber + "'," +
                                         "Length = " + carMasterVo.Length + "," +
                                         "Width = " + carMasterVo.Width + "," +
                                         "Height = " + carMasterVo.Height + "," +
                                         "FfAxisWeight = " + carMasterVo.FfAxisWeight + "," +
                                         "FrAxisWeight = " + carMasterVo.FrAxisWeight + "," +
                                         "RfAxisWeight = " + carMasterVo.RfAxisWeight + "," +
                                         "RrAxisWeight = " + carMasterVo.RrAxisWeight + "," +
                                         "Version = '" + carMasterVo.Version + "'," +
                                         "MotorVersion = '" + carMasterVo.MotorVersion + "'," +
                                         "TotalDisplacement = " + carMasterVo.TotalDisplacement + "," +
                                         "TypesOfFuel = '" + carMasterVo.TypesOfFuel + "'," +
                                         "VersionDesignateNumber = '" + carMasterVo.VersionDesignateNumber + "'," +
                                         "CategoryDistinguishNumber = '" + carMasterVo.CategoryDistinguishNumber + "'," +
                                         "OwnerName = '" + carMasterVo.OwnerName + "'," +
                                         "OwnerAddress = '" + carMasterVo.OwnerAddress + "'," +
                                         "UserName = '" + carMasterVo.UserName + "'," +
                                         "UserAddress = '" + carMasterVo.UserAddress + "'," +
                                         "BaseAddress = '" + carMasterVo.BaseAddress + "'," +
                                         "ExpirationDate = '" + carMasterVo.ExpirationDate + "'," +
                                         "Remarks = '" + carMasterVo.Remarks + "'," +
                                         "MainPicture = @member_MainPicture," +
                                         "SubPicture = @member_SubPicture," +
                                         "EmergencyVehicleFlag = '" + carMasterVo.EmergencyVehicleFlag + "'," +
                                         "EmergencyVehicleDate = '" + carMasterVo.EmergencyVehicleDate + "'," +
                                         "DigitalTachographFlag = '" + carMasterVo.DigitalTachographFlag + "'," +
                                         "DigitalTachographType = '" + carMasterVo.DigitalTachographType + "'," +
                                         "UpdatePcName = '" + Environment.MachineName + "'," +
                                         "UpdateYmdHms = '" + DateTime.Now + "' " +
                                     "WHERE CarCode = " + carMasterVo.CarCode;
            try {
                sqlCommand.Parameters.Add("@member_MainPicture", SqlDbType.Image, carMasterVo.MainPicture.Length).Value = carMasterVo.MainPicture;
                sqlCommand.Parameters.Add("@member_SubPicture", SqlDbType.Image, carMasterVo.SubPicture.Length).Value = carMasterVo.SubPicture;
                sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// 削除フラグを操作する
        /// </summary>
        /// <param name="carCode"></param>
        /// <param name="deleteFlag"></param>
        /// <returns></returns>
        public int DeleteOneCarMaster(int carCode, bool deleteFlag) {
            SqlCommand sqlCommand = _connectionVo.Connection.CreateCommand();
            sqlCommand.CommandText = "UPDATE H_CarMaster " +
                                     "SET DeletePcName = '" + Environment.MachineName + "'," +
                                         "DeleteYmdHms = '" + DateTime.Now + "'," +
                                         "DeleteFlag = '" + deleteFlag + "' " +
                                     "WHERE CarCode = " + carCode;
            try {
                return sqlCommand.ExecuteNonQuery();
            } catch {
                throw;
            }
        }
    }
}
