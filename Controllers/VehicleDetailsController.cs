using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vehicle_Business;

namespace VehicleAPIsService.Controllers
{
    [Route("api/VehicleDetails")]
    [ApiController]
    public class VehicleDetailsController : ControllerBase
    {
        [HttpGet("GetAllVehicleDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<VehicleDetailDTO>> GetAllVehicleDetails()
        {
            List<VehicleDetailDTO> vehicleDetails = clsVehicleDetail.GetAllVehicleDetails();
            if (vehicleDetails.Count == 0)
                return NotFound("There isn`t any VehicleDetails");
            return Ok(vehicleDetails);
        }

        [HttpGet("{ID}", Name = "GetVehicleDetail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VehicleDetailDTO> GetVehicleDetail(int ID)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This ID[{ID}] is not valid.");
            clsVehicleDetail vehicleDetail = clsVehicleDetail.GetVehicleDetail(ID);
            if (vehicleDetail == null)
                return NotFound($"Not Found: This VehicleDetailID[{ID}] is not found.");
            else
            {
                VehicleDetailDTO vDTO = vehicleDetail.vDTO;
                return Ok(vDTO);
            }
        }

        [HttpPost("AddNewVehicleDetail")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VehicleDetailDTO> AddNewVehicleDetail(VehicleDetailDTO NewVehicleDetailDTO)
        {
            if (NewVehicleDetailDTO == null ||
                string.IsNullOrEmpty(NewVehicleDetailDTO.Vehicle_Display_Name) ||
                string.IsNullOrEmpty(NewVehicleDetailDTO.Engine)||
                NewVehicleDetailDTO.BodyID <=0 ||
                NewVehicleDetailDTO.DriveTypeID <=0 ||
                NewVehicleDetailDTO.ModelID <=0 ||
                NewVehicleDetailDTO.FuelTypeID <=0 ||
                NewVehicleDetailDTO.MakeID <=0 ||
                NewVehicleDetailDTO.SubModelID <=0 ||
                NewVehicleDetailDTO.NumDoors <=0 ||
                NewVehicleDetailDTO.Engine_CC <=0 ||
                NewVehicleDetailDTO.Year >= 2000||
                NewVehicleDetailDTO.Engine_Cylinders <=0 ||
                NewVehicleDetailDTO.Engine_Liter_Display <= 0 ||
                NewVehicleDetailDTO.Engine.ToLower() == "string" ||
                NewVehicleDetailDTO.Vehicle_Display_Name.ToLower() == "string")
                return BadRequest("Bad request: Please enter valid data.");

            clsVehicleDetail VehicleDetail = new clsVehicleDetail(NewVehicleDetailDTO);
            if (!VehicleDetail.Save())
                return BadRequest(BadRequest("Bad request: The VehicleDetail`s data isn`t saved."));
            NewVehicleDetailDTO.VehicleDetailID = VehicleDetail.VehicleDetailID;
            return CreatedAtRoute("GetVehicleDetail", new { ID = NewVehicleDetailDTO.VehicleDetailID }, NewVehicleDetailDTO);
        }

        [HttpDelete("{ID}", Name = "DeleteVehicleDetail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteVehicleDetail(int ID)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This id[{ID}] isn`t valid.");
            if (!clsVehicleDetail.IsVehicleDetailExists(ID))
                return NotFound($"Not found: this ID[{ID}] isn`t exists.");
            if (clsVehicleDetail.DeleteVehicleDetail(ID))
                return Ok($"This VehicleDetail id[{ID}] is deleted.");
            return StatusCode(500, new { Message = "Error deleting VehicleDetail" });
        }

        [HttpPut("{ID}", Name = "UpdateVehicleDetail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VehicleDetailDTO> UpdateVehicleDetail(int ID, VehicleDetailDTO vDTO)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This id[{ID}] isn`t valid.");
            clsVehicleDetail VehicleDetail = clsVehicleDetail.GetVehicleDetail(ID);
            if (VehicleDetail == null)
                return NotFound($"Not found: this ID[{ID}] isn`t exists.");
            else
            {
                VehicleDetail.BodyID = vDTO.BodyID;
                VehicleDetail.DriveTypeID = vDTO.DriveTypeID;
                VehicleDetail.Engine = vDTO.Engine;
                VehicleDetail.Engine_CC = vDTO.Engine_CC;
                VehicleDetail.Engine_Cylinders = vDTO.Engine_Cylinders;
                VehicleDetail.Engine_Liter_Display = vDTO.Engine_Liter_Display;
                VehicleDetail.FuelTypeID = vDTO.FuelTypeID;
                VehicleDetail.MakeID = vDTO.MakeID;
                VehicleDetail.ModelID = vDTO.ModelID;
                VehicleDetail.NumDoors = vDTO.NumDoors;
                VehicleDetail.SubModelID = vDTO.SubModelID;
                VehicleDetail.VehicleDetailID = vDTO.VehicleDetailID;
                VehicleDetail.Vehicle_Display_Name = vDTO.Vehicle_Display_Name;
                VehicleDetail.Year = vDTO.Year;
                if (VehicleDetail.Save())
                    return Ok($"VehicleDetail with ID[{ID}] is updated successfuly");
                else
                    return StatusCode(500, new { Message = "Error updating VehicleDetail" });
            }
        }
    }
}
