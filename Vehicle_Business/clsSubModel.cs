using DTO;
using Vehicle_Access;

namespace Vehicle_Business
{
    public class clsSubModel
    {
        public enum enMode { Add, Update }
        public enMode Mode;
        public int? SubModelID { get; set; }
        public int ModelID { get; set; }
        public string SubModelName { get; set; }
        public SubModelsDTO sDTO { get { return new SubModelsDTO(SubModelID, ModelID, SubModelName); } }
        public clsSubModel(SubModelsDTO sDTO, enMode mode = enMode.Add)
        {
            SubModelID = sDTO.SubModelID;
            ModelID = sDTO.ModelID;
            SubModelName = sDTO.SubModelName;
            Mode = mode;
        }
        private bool AddNewSubModel()
        {
            this.SubModelID = clsSubModels.AddNewSubModel(sDTO);
            return this.SubModelID.HasValue;
        }
        private bool UpdatedSubModel()
            => clsSubModels.UpdatedSubModel(this.sDTO);
        public static List<SubModelsDTO> GetAllSubModels()
        {
            return clsSubModels.GetAllSubModels();
        }
        public static clsSubModel GetSubModelBy(int SubModelID)
        {
            SubModelsDTO sDTO = clsSubModels.GetSubModelBy(SubModelID);
            if (sDTO != null)
                return new clsSubModel(sDTO, enMode.Update);
            return null;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    {
                        if (AddNewSubModel())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        break;
                    }
                case enMode.Update:
                    {
                        return UpdatedSubModel();
                    }
            }
            return false;
        }
        public static bool DeleteSubModel(int SubModelID)
            => clsSubModels.DeleteSubModel(SubModelID);
        public static bool IsSubModelExists(int SubModelID)
            => clsSubModels.IsSubModelExists(SubModelID);
    }
}
