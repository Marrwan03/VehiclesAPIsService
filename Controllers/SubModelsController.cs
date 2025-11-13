using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vehicle_Business;

namespace VehicleAPIsService.Controllers
{
    [Route("api/SubModels")]
    [ApiController]
    public class SubModelsController : ControllerBase
    {
        [HttpGet("GetAllSubModels")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<SubModelsDTO>> GetAllSubModels()
        {
            List<SubModelsDTO> submodels = clsSubModel.GetAllSubModels();
            if (submodels.Count == 0)
                return NotFound("There isn`t any SubModels");
            return Ok(submodels);
        }

        [HttpGet("{ID}", Name = "GetSubModel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SubModelsDTO> GetSubModel(int ID)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This ID[{ID}] is not valid.");
            clsSubModel submodel = clsSubModel.GetSubModelBy(ID);
            if (submodel == null)
                return NotFound($"Not Found: This SubModelID[{ID}] is not found.");
            else
            {
                SubModelsDTO sDTO = submodel.sDTO;
                return Ok(sDTO);
            }
        }

        [HttpPost("AddNewSubModel")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SubModelsDTO> AddNewSubModel(SubModelsDTO NewSubModelDTO)
        {
            if (NewSubModelDTO == null || string.IsNullOrEmpty(NewSubModelDTO.SubModelName) || NewSubModelDTO.SubModelName.ToLower() == "string")
                return BadRequest("Bad request: Please enter valid data.");
            clsSubModel submodel = new clsSubModel(NewSubModelDTO);
            if (!submodel.Save())
                return BadRequest(BadRequest("Bad request: The SubModel`s data isn`t saved."));
            NewSubModelDTO.SubModelID = submodel.SubModelID;
            return CreatedAtRoute("GetSubModel", new { ID = NewSubModelDTO.SubModelID }, NewSubModelDTO);
        }

        [HttpDelete("{ID}", Name = "DeleteSubModel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteSubModel(int ID)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This id[{ID}] isn`t valid.");
            if (!clsSubModel.IsSubModelExists(ID))
                return NotFound($"Not found: this ID[{ID}] isn`t exists.");
            if (clsSubModel.DeleteSubModel(ID))
                return Ok($"This SubModel id[{ID}] is deleted.");
            return StatusCode(500, new { Message = "Error deleting SubModel" });
        }

        [HttpPut("{ID}", Name = "UpdateSubModel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SubModelsDTO> UpdateSubModel(int ID, SubModelsDTO sDTO)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This id[{ID}] isn`t valid.");
            clsSubModel submodel = clsSubModel.GetSubModelBy(ID);
            if (submodel == null)
                return NotFound($"Not found: this ID[{ID}] isn`t exists.");
            else
            {
                submodel.ModelID = sDTO.ModelID;
                submodel.SubModelName = sDTO.SubModelName;
                if (submodel.Save())
                    return Ok($"SubModel with ID[{ID}] is updated successfuly");
                else
                    return StatusCode(500, new { Message = "Error updating SubModel" });
            }
        }
    }
}
