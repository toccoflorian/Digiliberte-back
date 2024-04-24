using DTO.Brands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandServices brandService;

        public BrandController(BrandServices brandService)
        {
            this.brandService = brandService;
        }

        /// <summary>
        /// Create a Brand into the db, use a DTO for creations
        /// </summary>
        /// <param name="createOneBrand">DTO of Brand for creation</param>
        /// <returns>Returns a DTO of the created Brand</returns>
        [HttpPost]
        public async Task<ActionResult<GetOneBrandDTO?>> CreateBrand(CreateOneBrandDTO createOneBrand)
        {
            var createdBrand = await brandService.CreateBrandAsync(createOneBrand);
            return Ok(createdBrand);
        }
    }
}
