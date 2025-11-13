using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vehicle_Business;

namespace VehicleAPIsService.Controllers
{
    [Route("api/DriveTypes")]
    [ApiController]
    public class DriveTypesController : ControllerBase
    {
        [HttpGet("GetAllDriveTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DriveTypeDTO>> GetAllDriveTypes()
        {
            List<DriveTypeDTO> driveTypes = clsDriveType.GetAllDriveTypes();
            if (driveTypes.Count == 0)
                return NotFound("There isn`t any driveTypes");
            return Ok(driveTypes);
        }

        [HttpGet("{ID}", Name = "GetDriveType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DriveType> GetDriveType(int ID)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This ID[{ID}] is not valid.");

            clsDriveType driveType = clsDriveType.GetDriveTypeBy(ID);
            if (driveType == null)
                return NotFound($"Not Found: This DriveTypeID[{ID}] is not found.");
            else
            {
                DriveTypeDTO driveTypeDTO = driveType.dDTO;
                return Ok(driveTypeDTO);
            }
        }

        [HttpPost("AddNewDriveType")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DriveTypeDTO> AddNewDriveType(DriveTypeDTO NewDriveTypeDTO)
        {
            if (NewDriveTypeDTO == null || string.IsNullOrEmpty(NewDriveTypeDTO.DriveTypeName) || NewDriveTypeDTO.DriveTypeName.ToLower() == "string")
                return BadRequest("Bad request: Please enter valid data.");

            if (clsDriveType.IsDriveTypeExistsBy(NewDriveTypeDTO.DriveTypeName))
                return BadRequest($"This Drive type[{NewDriveTypeDTO.DriveTypeName}] is exists, please set another name.");

            clsDriveType driveType = new clsDriveType(NewDriveTypeDTO);
            if (!driveType.Save())
                return BadRequest(BadRequest("Bad request: The driveType`s data isn`t saved."));

            NewDriveTypeDTO.DriveTypeID = driveType.DriveTypeID;
            return CreatedAtRoute("GetDriveType", new { ID = NewDriveTypeDTO.DriveTypeID }, NewDriveTypeDTO);
        }

        [HttpDelete("{ID}", Name = "DeleteDriveType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteDriveType(int ID)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This id[{ID}] isn`t valid.");
            if (!clsDriveType.IsDriveTypeExistsBy(ID))
                return NotFound($"Not found: this ID[{ID}] isn`t exists.");
            if (clsDriveType.DeleteDriveType(ID))
                return Ok($"This drive type id[{ID}] is deleted.");
            return StatusCode(500, new { Message = "Error deleting drive type" });
        }

        [HttpPut("{ID}", Name = "UpdateDriveType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DriveTypeDTO> UpdateDriveType(int ID, DriveTypeDTO dDTO)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This id[{ID}] isn`t valid.");
            clsDriveType driveType = clsDriveType.GetDriveTypeBy(ID);
            if (driveType == null)
                return NotFound($"Not found: this ID[{ID}] isn`t exists.");
            else
            {
                driveType.DriveTypeName = dDTO.DriveTypeName;
                if (driveType.Save())
                    return Ok($"drive type with ID[{ID}] is updated successfuly");
                else
                    return StatusCode(500, new { Message = "Error updating drive type" });
            }
        }
    }
}
