using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FuelTypeDTO
    {
        public int? FuelTypeID {  get; set; }
        public string FuelTypeName { get; set; }

        public FuelTypeDTO(int? fuelTypeID, string fuelTypeName)
        {
            FuelTypeID = fuelTypeID;
            FuelTypeName = fuelTypeName;
        }
    }
}
