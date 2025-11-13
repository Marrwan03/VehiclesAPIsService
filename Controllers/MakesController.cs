using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vehicle_Access;
using Vehicle_Business;

namespace VehicleAPIsService.Controllers
{
    [Route("api/Makes")]
    [ApiController]
    public class MakesController : ControllerBase
    {
        [HttpGet("GetAllMakes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<MakeDTO>> GetAllMakes()
        {
            List<MakeDTO> makes = clsMake.GetAllMakes();
            if (makes.Count == 0)
                return NotFound("There isn`t any makes");
            return Ok(makes);
        }

        [HttpGet("{ID}", Name = "GetMake")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MakeDTO> GetMake(int ID)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This ID[{ID}] is not valid.");

            clsMake make = clsMake.GetMake(ID);
            if (make == null)
                return NotFound($"Not Found: This MakeID[{ID}] is not found.");
            else
            {
                MakeDTO makeDTO = make.mDTO;
                return Ok(makeDTO);
            }
        }

        [HttpPost("AddNewMake")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MakeDTO> AddNewMake(MakeDTO NewMakeDTO)
        {
            if (NewMakeDTO == null || string.IsNullOrEmpty(NewMakeDTO.Make) || NewMakeDTO.Make.ToLower() == "string")
                return BadRequest("Bad request: Please enter valid data.");
            clsMake make = new clsMake(NewMakeDTO);
            if (!make.Save())
                return BadRequest(BadRequest("Bad request: The make`s data isn`t saved."));

            NewMakeDTO.MakeID = make.MakeID;
            return CreatedAtRoute("GetMake", new { ID = NewMakeDTO.MakeID }, NewMakeDTO);
        }

        [HttpDelete("{ID}", Name = "DeleteMake")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteMake(int ID)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This id[{ID}] isn`t valid.");
            if (!clsMake.IsMakeExistsBy(ID))
                return NotFound($"Not found: this ID[{ID}] isn`t exists.");
            if (clsMake.DeleteMake(ID))
                return Ok($"This make id[{ID}] is deleted.");
            return StatusCode(500, new { Message = "Error deleting make" });
        }

        [HttpPut("{ID}", Name = "UpdateMake")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MakeDTO> UpdateMake(int ID, MakeDTO mDTO)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This id[{ID}] isn`t valid.");
            clsMake make = clsMake.GetMake(ID);
            if (make == null)
                return NotFound($"Not found: this ID[{ID}] isn`t exists.");
            else
            {
                make.Make = mDTO.Make;
                if (make.Save())
                    return Ok($"make with ID[{ID}] is updated successfuly");
                else
                    return StatusCode(500, new { Message = "Error updating make" });
            }
        }
    }
}
