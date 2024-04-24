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
            try
            {
                return Ok(await ModelServices.CreateModelAsync(createOneModel));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
