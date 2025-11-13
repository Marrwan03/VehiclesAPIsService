using DTO;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle_Access;

namespace Vehicle_Business
{
    public class clsFuelType
    {
        public enum enMode { Add, Update }
        public enMode Mode;
        public int? FuelTypeID { get; set; }
        public string FuelTypeName { get; set; }
        public FuelTypeDTO fDTO { get { return new FuelTypeDTO(FuelTypeID, FuelTypeName); } }
        public clsFuelType(FuelTypeDTO fDTO, enMode mode = enMode.Add)
        {
            FuelTypeID = fDTO.FuelTypeID;
            FuelTypeName = fDTO.FuelTypeName;
            Mode = mode;
        }
        private bool AddNewFuelType()
        {
            this.FuelTypeID = clsFuelTypes.AddNewFuelType(fDTO);
            return this.FuelTypeID.HasValue;
        }
        private bool UpdateFuelType()
            => clsFuelTypes.UpdatedFuelType(this.fDTO);
        public static List<FuelTypeDTO> GetAllFuelTypes()
        {
            return clsFuelTypes.GetAllFuelTypes();
        }
        public static clsFuelType GetFuelTypeBy(int FuelTypeID)
        {
            FuelTypeDTO fDTO = clsFuelTypes.GetFuelType(FuelTypeID);
            if (fDTO != null)
                return new clsFuelType(fDTO, enMode.Update);
            return null;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    {
                        if (AddNewFuelType())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        break;
                    }
                case enMode.Update:
                    {
                        return UpdateFuelType();
                    }

            }
            return false;
        }
        public static bool DeleteFuelType(int FuelTypeID)
            => clsFuelTypes.DeletedFuelType(FuelTypeID);
        public static bool IsFuelTypeExistsBy(int FuelTypeID)
            => clsFuelTypes.IsFuelTypeExistsBy(FuelTypeID);
    }
}
