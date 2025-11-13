using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SubModelsDTO
    {
        public int? SubModelID { get; set; }
        public int ModelID { get; set; }
        public string SubModelName { get; set; }

        public SubModelsDTO(int? subModelID, int modelID, string subModelName)
        {
            SubModelID = subModelID;
            ModelID = modelID;
            SubModelName = subModelName;
        }
    }
}
