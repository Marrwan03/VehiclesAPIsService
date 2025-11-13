using DTO;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vehicle_Business;

namespace VehicleAPIsService.Controllers
{
    [Route("api/Bodies")]
    [ApiController]
    public class BodiesController : ControllerBase
    {
        [HttpGet("GetAllBodies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<BodyDTO>> GetAllBodies()
        {
            List<BodyDTO> bodies = clsBody.GetAllBodies();
            if(bodies.Count == 0)
                return NotFound("There isn`t any VehicleBody");
            return Ok(bodies);
        }

        [HttpGet("{ID}", Name="GetBodyByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BodyDTO> GetBodyByID(int ID)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This ID[{ID}] is not valid.");

            clsBody body = clsBody.GetBodyBy(ID);
            if (body == null)
                return NotFound($"Not Found: This BodyID[{ID}] is not found.");
            else
            {
                BodyDTO bodyDTO = body.bDTO;
                return Ok(bodyDTO);
            }
        }

        [HttpGet("'{Name}'", Name = "GetBodyByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BodyDTO> GetBodyByName(string Name)
        {
            if (string.IsNullOrEmpty(Name) || Name.ToLower()=="string")
                return BadRequest($"Bad request: This BodyName[{Name}] is not valid.");

            clsBody body = clsBody.GetBodyBy(Name);
            if (body == null)
                return NotFound($"Not Found: This BodyName[{Name}] is not found.");
            else
            {
                BodyDTO bodyDTO = body.bDTO;
                return Ok(bodyDTO);
            }
        }

        [HttpPost("AddNewBody")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BodyDTO> AddNewBody(BodyDTO NewbodyDTO)
        {
            if (NewbodyDTO == null || string.IsNullOrEmpty(NewbodyDTO.BodyName) || NewbodyDTO.BodyName.ToLower() == "string")
                return BadRequest("Bad request: Please enter valid data.");

            if (clsBody.IsBodyExistsBy(NewbodyDTO.BodyName))
                return BadRequest($"Bad request: This BodyName[{NewbodyDTO.BodyName}] is exists, please set another name.");

            clsBody body = new clsBody(NewbodyDTO);
            if (!body.Save())
                return BadRequest(BadRequest("Bad request: The body`s data isn`t saved."));

            NewbodyDTO.BodyID = body.BodyID;
            return CreatedAtRoute("GetBodyByID", new { ID = NewbodyDTO.BodyID }, NewbodyDTO);
        }

        [HttpDelete("{ID}", Name ="DeleteBody")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteBody(int ID)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This id[{ID}] isn`t valid.");
            if (!clsBody.IsBodyExistsBy(ID))
                return NotFound($"Not found: this ID[{ID}] isn`t exists.");
            if (clsBody.DeleteBody(ID))
                return Ok($"This body id[{ID}] is deleted.");
            return StatusCode(500, new { Message = "Error deleting Body" });
        }

        [HttpPut("{ID}", Name ="UpdateBody")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BodyDTO> UpdateBody(int ID, BodyDTO bDTO)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This id[{ID}] isn`t valid.");
            clsBody body = clsBody.GetBodyBy(ID);
            if(body == null)
                return NotFound($"Not found: this ID[{ID}] isn`t exists.");
            else
            {
                body.BodyName = bDTO.BodyName;
                if (body.Save())
                    return Ok($"Body with ID[{ID}] is updated successfuly");
                else
                    return StatusCode(500, new { Message = "Error updating Body" });
            }
        }

    }
}
