using DTO.Dates;
using IServices;
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
        /// <param name="createOneCategoryDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GetOneCategoryDTO>> CreateOneCategory(CreateOneCategoryDTO createOneCategoryDTO)
        {
            try
            {
                return await this._categoryServices.CreateOneCategoryAsync(createOneCategoryDTO);
            }
            catch (Exception ex)
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
        /// Get a Category By Id into the db, use a Id       
        /// </summary>
        /// <param name="GetOneCategoryById">DTO of Category for GetOneCategory</param>
        /// <returns>Returns a DTO of the GetOneCategoryById Category</returns>
        [HttpGet]
        public async Task<ActionResult<GetOneCategoryDTO?>> GetOneCategoryByIdAsync(int Id)
        {
            // Utilisez le service pour récupérer le modèle par son ID
            try
            {
                return Ok(await _categoryServices.GetOneCategoryByIdAsync(Id));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Get all categorys
        /// </summary>
        /// <returns>List of category DTOs</returns>
        [HttpGet("all")]
        public async Task<ActionResult<List<GetOneCategoryDTO>>> GetAllCategorysAsync(int paginationIndex = 0, int pageSize = 10)
        {
            try
            {
                var categorys = await _categoryServices.GetAllCategorysAsync(paginationIndex, pageSize);
                return Ok(categorys);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
