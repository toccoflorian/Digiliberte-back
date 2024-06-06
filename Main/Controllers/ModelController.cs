using DTO.Dates;
using DTO.Models;

using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;
using System.Threading.Tasks;
using Utils.Constants;

namespace Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelServices;
        public ModelController(IModelService modelServices)
        {
            this._modelServices = modelServices;
        }
        /// <summary>
        /// Create a Model into the db, use a DTO for creations
        /// </summary>
        /// <param name="createOneModel">DTO of Model for creation</param>
        /// <returns>Returns a DTO of the created Vehicle</returns>
        [HttpPost]
		//[Authorize(Roles = ROLE.ADMIN)]

		public async Task<ActionResult<GetOneModelDTO?>> CreateModel(CreateOneModelDTO createOneModel)
        {
            try
            {
                return Ok(await _modelServices.CreateModelAsync(createOneModel));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Put a Model into the db, use a DTO for update        
        /// </summary>
        /// <param name="updateOneModel">DTO of Model for update</param>
        /// <returns>Returns a DTO of the updated Vehicle</returns>
        [HttpPut]
		//[Authorize(Roles = ROLE.ADMIN)]

		public async Task<ActionResult<GetOneModelDTO?>> UpdateModel(GetOneModelDTO getOneModel)
        {
            try
            {
            return Ok(await _modelServices.UpdateModelAsync(getOneModel));
            }catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Get a Model By Id into the db, use a Id       
        /// </summary>
        /// <param name="GetOneModelById">DTO of Model for GetOneModel</param>
        /// <returns>Returns a DTO of the GetOneModelById Model</returns>
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<GetOneModelDTO?>> GetOneModelByIdAsync(int Id)
        {
            // Si le modèle est trouvé, retournez-le en tant que réponse HTTP 200 OK
            try
            {
                var modelDto = await _modelServices.GetOneModelByIdAsync(Id);
                return Ok(modelDto);
            }catch(Exception ex) 
            {
                // Si le modèle n'est pas trouvé, retournez une réponse HTTP 404 Not Found
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all models
        /// </summary>
        /// <returns>List of model DTOs</returns>
        [HttpGet("all")]
        //[Authorize]
        public async Task<ActionResult<List<GetOneModelDTO>>> GetAllModelsAsync(int paginationIndex = 0, int pageSize = 10)
        {
            try
            {
                var models = await _modelServices.GetAllModelsAsync(paginationIndex,pageSize) ;
                return Ok(models);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
