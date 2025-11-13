using DTO;
using Vehicle_Access;

namespace Vehicle_Business
{
    public class clsMake
    {
        public enum enMode { Add, Update }
        public enMode Mode;
        public int? MakeID { get; set; }
        public string Make { get; set; }
        public MakeDTO mDTO { get { return new MakeDTO(MakeID, Make); } }
        public clsMake(MakeDTO mDTO, enMode mode = enMode.Add)
        {
            MakeID = mDTO.MakeID;
            Make = mDTO.Make;
            Mode = mode;
        }
        private bool AddNewMake()
        {
            this.MakeID = clsMakes.AddNewMake(mDTO);
            return this.MakeID.HasValue;
        }
        private bool UpdatedMake()
            => clsMakes.UpdatedMake(this.mDTO);
        public static List<MakeDTO> GetAllMakes()
        {
            return clsMakes.GetAllMakes();
        }
        public static clsMake GetMake(int MakeID)
        {
            MakeDTO mDTO = clsMakes.GetMake(MakeID);
            if (mDTO != null)
                return new clsMake(mDTO, enMode.Update);
            return null;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    {
                        if (AddNewMake())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        break;
                    }
                case enMode.Update:
                    {
                        return UpdatedMake();
                    }

            }
            return false;
        }
        public static bool DeleteMake(int MakeID)
            => clsMakes.DeleteMake(MakeID);
        public static bool IsMakeExistsBy(int MakeID)
            => clsMakes.IsMakeExistsBy(MakeID);
    }
}
