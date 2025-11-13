using DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle_Access;

namespace Vehicle_Business
{
    public class clsVehicleDetail
    {
        public enum enMode { Add, Update }
        public enMode Mode;
        public int? VehicleDetailID { get; set; }
        public int MakeID { get; set; }
        public int ModelID { get; set; }
        public int SubModelID { get; set; }
        public int BodyID { get; set; }
        public string Vehicle_Display_Name { get; set; }
        public short Year { get; set; }
        public int DriveTypeID { get; set; }
        public string Engine { get; set; }
        public short Engine_CC { get; set; }
        public byte Engine_Cylinders { get; set; }
        public decimal Engine_Liter_Display { get; set; }
        public int FuelTypeID { get; set; }
        public byte NumDoors { get; set; }
        public VehicleDetailDTO vDTO { get { return new VehicleDetailDTO(VehicleDetailID, MakeID,ModelID, SubModelID
            ,BodyID, Vehicle_Display_Name, Year, DriveTypeID, Engine, Engine_CC, Engine_Cylinders, Engine_Liter_Display,
            FuelTypeID, NumDoors); } }
        public clsVehicleDetail(VehicleDetailDTO vDTO, enMode mode = enMode.Add)
        {
            VehicleDetailID = vDTO.VehicleDetailID;
            MakeID = vDTO.MakeID;
            ModelID = vDTO.ModelID;
            SubModelID = vDTO.SubModelID;
            BodyID = vDTO.BodyID;
            Vehicle_Display_Name = vDTO.Vehicle_Display_Name;
            Year = vDTO.Year;
            DriveTypeID = vDTO.DriveTypeID;
            Engine = vDTO.Engine;
            Engine_CC = vDTO.Engine_CC;
            Engine_Cylinders = vDTO.Engine_Cylinders;
            Engine_Liter_Display = vDTO.Engine_Liter_Display;
            FuelTypeID = vDTO.FuelTypeID;
            NumDoors = vDTO.NumDoors;
            Mode = mode;
        }
        private bool AddNewVehicleDetail()
        {
            this.VehicleDetailID = clsVehicleDetails.AddNewVehicleDetail(vDTO);
            return this.VehicleDetailID.HasValue;
        }
        private bool UpdatedVehicleDetail()
            => clsVehicleDetails.UpdatedVehicleDetail(this.vDTO);
        public static List<VehicleDetailDTO> GetAllVehicleDetails()
        {
            return clsVehicleDetails.GetAllVehicleDetails();
        }
        public static clsVehicleDetail GetVehicleDetail(int VehicleDetailID)
        {
            VehicleDetailDTO vDTO = clsVehicleDetails.GetVehicleDetail(VehicleDetailID);
            if (vDTO != null)
                return new clsVehicleDetail(vDTO, enMode.Update);
            return null;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    {
                        if (AddNewVehicleDetail())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        break;
                    }
                case enMode.Update:
                    {
                        return UpdatedVehicleDetail();
                    }
            }
            return false;
        }
        public static bool DeleteVehicleDetail(int VehicleDetailID)
            => clsVehicleDetails.DeleteVehicleDetail(VehicleDetailID);
        public static bool IsVehicleDetailExists(int VehicleDetailID)
            => clsVehicleDetails.IsVehicleDetailExists(VehicleDetailID);
    }
}
