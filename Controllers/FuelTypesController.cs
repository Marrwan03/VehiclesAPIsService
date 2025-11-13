using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vehicle_Access;
using Vehicle_Business;

namespace VehicleAPIsService.Controllers
{
    [Route("api/FuelTypes")]
    [ApiController]
    public class FuelTypesController : ControllerBase
    {
        [HttpGet("GetAllFuelTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<FuelTypeDTO>> GetAllFuelTypes()
        {
            List<FuelTypeDTO> fuelTypes = clsFuelTypes.GetAllFuelTypes();
            if (fuelTypes.Count == 0)
                return NotFound("There isn`t any fuelTypes");
            return Ok(fuelTypes);
        }

        [HttpGet("{ID}", Name = "GetFuelType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<FuelTypeDTO> GetFuelType(int ID)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This ID[{ID}] is not valid.");

            clsFuelType fuelType = clsFuelType.GetFuelTypeBy(ID);
            if (fuelType == null)
                return NotFound($"Not Found: This FuelTypeID[{ID}] is not found.");
            else
            {
                FuelTypeDTO fuelTypeDTO = fuelType.fDTO;
                return Ok(fuelTypeDTO);
            }
        }

        [HttpPost("AddNewFuelType")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<FuelTypeDTO> AddNewFuelType(FuelTypeDTO NewFuelTypeDTO)
        {
            if (NewFuelTypeDTO == null || string.IsNullOrEmpty(NewFuelTypeDTO.FuelTypeName) || NewFuelTypeDTO.FuelTypeName.ToLower() == "string")
                return BadRequest("Bad request: Please enter valid data.");

            clsFuelType fuelType = new clsFuelType(NewFuelTypeDTO);
            if (!fuelType.Save())
                return BadRequest(BadRequest("Bad request: The fuelType`s data isn`t saved."));

            NewFuelTypeDTO.FuelTypeID = fuelType.FuelTypeID;
            return CreatedAtRoute("GetFuelType", new { ID = NewFuelTypeDTO.FuelTypeID }, NewFuelTypeDTO);
        }

        [HttpDelete("{ID}", Name = "DeleteFuelType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteFuelType(int ID)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This id[{ID}] isn`t valid.");
            if (!clsFuelType.IsFuelTypeExistsBy(ID))
                return NotFound($"Not found: this ID[{ID}] isn`t exists.");
            if (clsFuelType.DeleteFuelType(ID))
                return Ok($"This fuel type id[{ID}] is deleted.");
            return StatusCode(500, new { Message = "Error deleting fuel type" });
        }

        [HttpPut("{ID}", Name = "UpdateFuelType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<FuelTypeDTO> UpdateFuelType(int ID, FuelTypeDTO fDTO)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This id[{ID}] isn`t valid.");
            clsFuelType fuelType = clsFuelType.GetFuelTypeBy(ID);
            if (fuelType == null)
                return NotFound($"Not found: this ID[{ID}] isn`t exists.");
            else
            {
                fuelType.FuelTypeName = fDTO.FuelTypeName;
                if (fuelType.Save())
                    return Ok($"fuel type with ID[{ID}] is updated successfuly");
                else
                    return StatusCode(500, new { Message = "Error updating fuel type" });
            }
        }
    }
}
