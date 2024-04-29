using DTO.Dates;
using DTO.Models;

using IServices;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;
using System.Threading.Tasks;

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
        /// Put a Model into the db, use a DTO for update        /// </summary>
        /// <param name="updateOneModel">DTO of Model for update</param>
        /// <returns>Returns a DTO of the updated Vehicle</returns>
        [HttpPut]

        public async Task<ActionResult<GetOneModelDTO?>> UpdateModel(GetOneModelDTO getOneModel)
        {
            try
            {
            return Ok(await _modelServices.UpdateModelAsync(getOneModel));
            }catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Dlete a Model into the db, use a DTO for delete        
        /// 
        /// /// </summary>
        /// <param name="deleteOneModel">DTO of Model for delete</param>
        /// <returns>Returns a DTO of the deleted Vehicle</returns>
        [HttpDelete]
        public async Task<ActionResult<GetOneModelDTO?>> DeleteOneModelByIdAsync(int Id)
        {
            var model = await _modelServices.GetOneModelByIdAsync(Id);

            if (model != null)
            {
                await _modelServices.DeleteOneModelByIdAsync(Id); 
                return Ok($"Le modèle avec le id : {Id} à été supprimé ");
            }
            else
            {
                return null; // Indique que le modèle n'a pas été trouvé, donc la suppression n'a pas été effectuée
            }
        }

        /// <summary>
        /// Get a Model By Id into the db, use a Id       
        /// /// </summary>
        /// <param name="GetOneModelById">DTO of Model for GetOneModel</param>
        /// <returns>Returns a DTO of the GetOneModelById Model</returns>
        [HttpGet]
        public async Task<ActionResult<GetOneModelDTO?>> GetOneModelByIdAsync(int Id)
        {
            // Utilisez le service pour récupérer le modèle par son ID
            var modelDto = await _modelServices.GetOneModelByIdAsync(Id);

            // Si le modèle est trouvé, retournez-le en tant que réponse HTTP 200 OK
            if (modelDto != null)
            {
                return Ok(modelDto);
            }
            else
            {
                // Si le modèle n'est pas trouvé, retournez une réponse HTTP 404 Not Found
                return NotFound();
            }
        }

        /// <summary>
        /// Get all models
        /// </summary>
        /// <returns>List of model DTOs</returns>
        [HttpGet("all")]
        public async Task<ActionResult<List<GetOneModelDTO>>> GetAllModelsAsync()
        {
            try
            {
                var models = await _modelServices.GetAllModelsAsync();
                return Ok(models);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
