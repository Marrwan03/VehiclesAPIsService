using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle_Access;

namespace Vehicle_Business
{
    public class clsMakeModel
    {
        public enum enMode { Add, Update }
        public enMode Mode;
        public int? ModelID { get; set; }
        public int MakeID { get; set; }
        public string ModelName { get; set; }
        public MakeModelDTO mDTO { get { return new MakeModelDTO(ModelID, MakeID, ModelName); } }
        public clsMakeModel(MakeModelDTO mDTO, enMode mode = enMode.Add)
        {
            ModelID = mDTO.ModelID;
            MakeID = mDTO.MakeID;
            ModelName = mDTO.ModelName;
            Mode = mode;
        }
        private bool AddNewMakeModel()
        {
            this.ModelID = clsMakeModels.AddNewMakeModel(mDTO);
            return this.ModelID.HasValue;
        }
        private bool UpdatedMakeModel()
            => clsMakeModels.UpdatedMakeModel(this.mDTO);
        public static List<MakeModelDTO> GetAllMakeModels()
        {
            return clsMakeModels.GetAllMakeModels();
        }
        public static clsMakeModel GetMakeModel(int ModelID)
        {
            MakeModelDTO mDTO = clsMakeModels.GetMakeModel(ModelID);
            if (mDTO != null)
                return new clsMakeModel(mDTO, enMode.Update);
            return null;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    {
                        if (AddNewMakeModel())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        break;
                    }
                case enMode.Update:
                    {
                        return UpdatedMakeModel();
                    }

            }
            return false;
        }
        public static bool DeleteMakeModel(int ModelID)
            => clsMakeModels.DeleteMakeModel(ModelID);
        public static bool IsMakeModelExistsBy(int ModelID)
            => clsMakeModels.IsMakeModelExists(ModelID);
    }
}
