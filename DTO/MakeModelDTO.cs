using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MakeModelDTO
    {
        public int? ModelID { get; set; }
        public int MakeID { get; set; }
        public string ModelName { get; set; }
        public MakeModelDTO(int? modelID, int makeID, string modelName)
        {
            ModelID = modelID;
            MakeID = makeID;
            ModelName = modelName;
        }
    }
}
