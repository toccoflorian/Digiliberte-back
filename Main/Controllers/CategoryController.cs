using DTO.Dates;
using DTO.Models;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryServices;
        public CategoryController(ICategoryService categoryService) 
        {
            this._categoryServices = categoryService;
        }

        /// <summary>
        /// Create a Category into the db, use a DTO for Create        
        /// </summary>
        /// <param name="updateOneCategory">DTO of Category for update</param>
        /// <returns>Returns a DTO of the updated category</returns>
        [HttpPost]
        public async Task<ActionResult<GetOneCategoryDTO>> CreateOneCategory(CreateOneCategoryDTO createOneCategoryDTO)
        {
            try
            {
            return await this._categoryServices.CreateOneCategoryAsync(createOneCategoryDTO);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Put a Category into the db, use a DTO for update        
        /// </summary>
        /// <param name="updateOneCategory">DTO of Category for update</param>
        /// <returns>Returns a DTO of the updated category</returns>
        [HttpPut]

        public async Task<ActionResult<GetOneCategoryDTO?>> UpdateCategory(GetOneCategoryDTO getOneCategory)
        {
            try
            {
                return Ok(await _categoryServices.UpdateOneCategoryByIdAsync(getOneCategory));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Dlete a Category into the db, use a DTO for delete        
        /// 
        /// /// </summary>
        /// <param name="deleteOneCategory">DTO of Category for delete</param>
        /// <returns>Returns a DTO of the deleted category</returns>
        [HttpDelete]
        public async Task<ActionResult<GetOneCategoryDTO?>> DeleteOneCategoryByIdAsync(int Id)
        {
            var model = await _categoryServices.GetOneCategoryByIdAsync(Id);

            if (model != null)
            {
                await _categoryServices.DeleteOneCategoryByIdAsync(Id);
                return Ok($"La catégorie avec le id : {Id} delete ");
            }
            else
            {
                return null; // Indique que le modèle n'a pas été trouvé, donc la suppression n'a pas été effectuée
            }
        }

        /// <summary>
        /// Get a Category By Id into the db, use a Id       
        /// /// </summary>
        /// <param name="GetOneCategoryById">DTO of Category for GetOneCategory</param>
        /// <returns>Returns a DTO of the GetOneCategoryById Category</returns>
        [HttpGet]
        public async Task<ActionResult<GetOneCategoryDTO?>> GetOneCategoryByIdAsync(int Id)
        {
            // Utilisez le service pour récupérer le modèle par son ID
            var modelDto = await _categoryServices.GetOneCategoryByIdAsync(Id);

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
    }
}
