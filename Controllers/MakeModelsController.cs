using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vehicle_Business;

namespace VehicleAPIsService.Controllers
{
    [Route("api/MakeModels")]
    [ApiController]
    public class MakeModelsController : ControllerBase
    {
        [HttpGet("GetAllMakeModels")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<MakeModelDTO>> GetAllMakeModels()
        {
            List<MakeModelDTO> makemodels = clsMakeModel.GetAllMakeModels();
            if (makemodels.Count == 0)
                return NotFound("There isn`t any MakeModels");
            return Ok(makemodels);
        }

        [HttpGet("{ID}", Name = "GetMakeModel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MakeModelDTO> GetMakeModel(int ID)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This ID[{ID}] is not valid.");
            clsMakeModel makemodel = clsMakeModel.GetMakeModel(ID);
            if (makemodel == null)
                return NotFound($"Not Found: This MakeModelID[{ID}] is not found.");
            else
            {
                MakeModelDTO makemodelDTO = makemodel.mDTO;
                return Ok(makemodelDTO);
            }
        }

        [HttpPost("AddNewMakeModel")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MakeModelDTO> AddNewMakeModel(MakeModelDTO NewMakeModelDTO)
        {
            if (NewMakeModelDTO == null || string.IsNullOrEmpty(NewMakeModelDTO.ModelName) || NewMakeModelDTO.ModelName.ToLower() == "string")
                return BadRequest("Bad request: Please enter valid data.");
            clsMakeModel makemodel = new clsMakeModel(NewMakeModelDTO);
            if (!makemodel.Save())
                return BadRequest(BadRequest("Bad request: The MakeModel`s data isn`t saved."));
            NewMakeModelDTO.ModelID = makemodel.ModelID;
            return CreatedAtRoute("GetMakeModel", new { ID = NewMakeModelDTO.ModelID }, NewMakeModelDTO);
        }

        [HttpDelete("{ID}", Name = "DeleteMakeModel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteMakeModel (int ID)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This id[{ID}] isn`t valid.");
            if (!clsMakeModel.IsMakeModelExistsBy(ID))
                return NotFound($"Not found: this ID[{ID}] isn`t exists.");
            if (clsMakeModel.DeleteMakeModel(ID))
                return Ok($"This MakeModel id[{ID}] is deleted.");
            return StatusCode(500, new { Message = "Error deleting MakeModel"});
        }

        [HttpPut("{ID}", Name = "UpdateMakeModel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MakeModelDTO> UpdateMakeModel(int ID, MakeModelDTO mDTO)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This id[{ID}] isn`t valid.");
            clsMakeModel makemodel = clsMakeModel.GetMakeModel(ID);
            if (makemodel == null)
                return NotFound($"Not found: this ID[{ID}] isn`t exists.");
            else
            {
                makemodel.ModelName = mDTO.ModelName;
                makemodel.MakeID = mDTO.MakeID;
                if (makemodel.Save())
                    return Ok($"MakeModel with ID[{ID}] is updated successfuly");
                else
                    return StatusCode(500, new { Message = "Error updating MakeModel" });
            }
        }
    }
}
