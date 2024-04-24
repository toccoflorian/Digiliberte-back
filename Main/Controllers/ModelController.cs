using DTO.Models;
using DTO.Vehicles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        public readonly ModelServices ModelServices;
        public ModelController(ModelServices ModelServices)
        {
            this.ModelServices = ModelServices;
        }
        /// <summary>
        /// Create a Model into the db, use a DTO for creations
        /// </summary>
        /// <param name="createOneModel">DTO of Model for creation</param>
        /// <returns>Returns a DTO of the created Vehicle</returns>
        [HttpPost]
        
        public async Task<ActionResult<GetOneModelDTO?>> CreateModel(CreateOneModelDTO createOneModel)
        {
            return Ok(await ModelServices.CreateModelAsync(createOneModel));
        }

        /// <summary>
        /// Put a Model into the db, use a DTO for update        /// </summary>
        /// <param name="updateOneModel">DTO of Model for update</param>
        /// <returns>Returns a DTO of the updated Vehicle</returns>
        [HttpPut]

        public async Task<ActionResult<GetOneModelDTO?>> UpdateModel(GetOneModelDTO getOneOneModel)
        {
            try
            {
            return Ok(await ModelServices.UpdateModelAsync(getOneOneModel));
            }catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
