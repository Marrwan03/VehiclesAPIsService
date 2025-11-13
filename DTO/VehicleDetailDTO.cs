using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class VehicleDetailDTO
    {
        public int? VehicleDetailID { get; set; }
        public int MakeID { get; set; }
        public int ModelID { get; set; }
        public int SubModelID { get; set; }
        public int BodyID { get; set; }
        public string Vehicle_Display_Name { get; set; }
        public short Year { get; set; }
        public int DriveTypeID { get; set; }
        public string Engine { get; set; }
        public short Engine_CC { get; set; }
        public byte Engine_Cylinders { get; set; }
        public decimal Engine_Liter_Display { get; set; }
        public int FuelTypeID { get; set; }
        public byte NumDoors { get; set; }

        public VehicleDetailDTO(int? vehicleDetailID, int makeID, int modelID, int subModelID, int bodyID,
            string vehicle_Display_Name, short year, int driveTypeID, string engine, short engine_CC,
            byte engine_Cylinders, decimal engine_Liter_Display, int fuelTypeID, byte numDoors)
        {
            VehicleDetailID = vehicleDetailID;
            MakeID = makeID;
            ModelID = modelID;
            SubModelID = subModelID;
            BodyID = bodyID;
            Vehicle_Display_Name = vehicle_Display_Name;
            Year = year;
            DriveTypeID = driveTypeID;
            Engine = engine;
            Engine_CC = engine_CC;
            Engine_Cylinders = engine_Cylinders;
            Engine_Liter_Display = engine_Liter_Display;
            FuelTypeID = fuelTypeID;
            NumDoors = numDoors;
        }
    }
}
