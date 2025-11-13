using DTO;
using Vehicle_Access;

namespace Vehicle_Business
{
    public class clsBody
    {
        public enum enMode { Add, Update }
        public enMode Mode;
        public int? BodyID { get; set; }
        public string BodyName { get; set; }
        public BodyDTO bDTO { get { return new BodyDTO(BodyID, BodyName); } }
        public clsBody(BodyDTO bodyDTO, enMode mode= enMode.Add)
        {
            BodyID = bodyDTO.BodyID;
            BodyName = bodyDTO.BodyName;
            Mode = mode;
        }
        private bool AddNewBody()
        {
            this.BodyID = clsBodies.AddNewBody(bDTO);
            return this.BodyID.HasValue;
        }
        private bool UpdateBody()
            => clsBodies.UpdatedBody(this.bDTO);
        public static List<BodyDTO> GetAllBodies()
        {
            return clsBodies.GetAllBodies();
        }
        public static clsBody GetBodyBy(int BodyID)
        {
            BodyDTO bDTO = clsBodies.GetBody(BodyID);
            if (bDTO != null)
                return new clsBody(bDTO, enMode.Update);
            return null;
        }
        public static clsBody GetBodyBy(string BodyName)
        {
            BodyDTO bDTO = clsBodies.GetBody(BodyName);
            if (bDTO != null)
                return new clsBody(bDTO, enMode.Update);
            return null;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    {
                        if (AddNewBody())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        break;
                    }
                    case enMode.Update:
                    {
                        return UpdateBody();
                    }

            }
            return false;
        }
        public static bool DeleteBody(int BodyID)
            => clsBodies.DeleteBody(BodyID);
        public static bool IsBodyExistsBy(int BodyID)
            => clsBodies.IsBodyExistsBy(BodyID);
        public static bool IsBodyExistsBy(string BodyName)
            => clsBodies.IsBodyExistsBy(BodyName);

    }
}
