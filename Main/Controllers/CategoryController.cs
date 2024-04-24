using DTO.Dates;
using ICategoryServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) 
        {
            this._categoryService = categoryService;
        } 

        [HttpPost]
        public async Task<ActionResult<GetOneCategoryDTO>> CreateOneCategory(CreateOneCategoryDTO createOneCategoryDTO)
        {
            return await this._categoryService.CreateOneCategoryAsync(createOneCategoryDTO);
        }
    }
}
