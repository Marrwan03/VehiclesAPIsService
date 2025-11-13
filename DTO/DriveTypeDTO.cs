using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DriveTypeDTO
    {
        public int? DriveTypeID {  get; set; }
        public string DriveTypeName { get; set; }

        public DriveTypeDTO(int? driveTypeID, string driveTypeName)
        {
            DriveTypeID = driveTypeID;
            DriveTypeName = driveTypeName;
        }
    }
}
