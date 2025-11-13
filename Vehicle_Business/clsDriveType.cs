using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle_Access;

namespace Vehicle_Business
{
    public class clsDriveType
    {
        public enum enMode { Add, Update }
        public enMode Mode;
        public int? DriveTypeID { get; set; }
        public string DriveTypeName { get; set; }
        public DriveTypeDTO dDTO { get { return new DriveTypeDTO(DriveTypeID, DriveTypeName); } }
        public clsDriveType(DriveTypeDTO dDTO, enMode mode = enMode.Add)
        {
            DriveTypeID = dDTO.DriveTypeID;
            DriveTypeName = dDTO.DriveTypeName;
            Mode = mode;
        }
        private bool AddNewDriveType()
        {
            this.DriveTypeID = clsDriveTypes.AddNewDriveType(dDTO);
            return this.DriveTypeID.HasValue;
        }
        private bool UpdateDriveType()
            => clsDriveTypes.UpdatedDriveType(this.dDTO);
        public static List<DriveTypeDTO> GetAllDriveTypes()
        {
            return clsDriveTypes.GetAllDriveTypes();
        }
        public static clsDriveType GetDriveTypeBy(int DriveTypeID)
        {
            DriveTypeDTO dDTO = clsDriveTypes.GetDriveType(DriveTypeID);
            if (dDTO != null)
                return new clsDriveType(dDTO, enMode.Update);
            return null;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    {
                        if (AddNewDriveType())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        break;
                    }
                case enMode.Update:
                    {
                        return UpdateDriveType();
                    }

            }
            return false;
        }
        public static bool DeleteDriveType(int DriveTypeID)
            => clsDriveTypes.DeleteDriveType(DriveTypeID);
        public static bool IsDriveTypeExistsBy(int DriveTypeID)
            => clsDriveTypes.IsDriveTypeExistsBy(DriveTypeID);
        public static bool IsDriveTypeExistsBy(string DriveTypeName)
            => clsDriveTypes.IsDriveTypeExistsBy(DriveTypeName);
    }
}
