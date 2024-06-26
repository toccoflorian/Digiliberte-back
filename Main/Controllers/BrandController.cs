﻿using DTO.Brands;
using DTO.Brands;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Utils.Constants;

namespace Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService brandService;

        public BrandController(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        /// <summary>
        /// Create a Brand into the db, use a DTO for creations
        /// </summary>
        /// <param name="createOneBrand">DTO of Brand for creation</param>
        /// <returns>Returns a DTO of the created Brand</returns>
        /// 

        [Authorize(Roles = ROLE.ADMIN)]
        [HttpPost]
        public async Task<ActionResult<GetOneBrandDTO?>> CreateBrand(CreateOneBrandDTO createOneBrand)
        {
            var createdBrand = await brandService.CreateBrandAsync(createOneBrand);
            return Ok(createdBrand);
        }

        /// <summary>
        /// Put a Brand into the db, use a DTO for update        
        /// </summary>
        /// <param name="updateOneBrand">DTO of Brand for update</param>
        /// <returns>Returns a DTO of the updated Brand</returns>
        [HttpPut]
		[Authorize(Roles = ROLE.ADMIN)]
		public async Task<ActionResult<GetOneBrandDTO?>> UpdateBrand(GetOneBrandDTO getOneOneBrand)
        {
            try
            {
                return Ok(await brandService.UpdateBrandAsync(getOneOneBrand));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        /// <summary>
        /// Get a Brand By Id into the db, use a Id       
        /// </summary>
        /// <param name="GetOneBrandById">DTO of Brand for GetOneBrand</param>
        /// <returns>Returns a DTO of the GetOneBrandById Brand</returns>
        [HttpGet]
		[Authorize]

		public async Task<ActionResult<GetOneBrandDTO?>> GetOneBrandByIdAsync(int Id)
        {
            // Utilisez le service pour récupérer le modèle par son ID
            var brandDto = await brandService.GetOneBrandByIdAsync(Id);

            // Si le modèle est trouvé, retournez-le en tant que réponse HTTP 200 OK
            try
            {
                return Ok(brandDto);
            }
            catch
             (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all brands
        /// </summary>
        /// <returns>List of brand DTOs</returns>
        [HttpGet("all")]
		[Authorize(Roles = ROLE.ADMIN+","+ROLE.USER)]
		public async Task<ActionResult<List<GetOneBrandDTO>>> GetAllBrandsAsync()
        {
            try
            {
                var brands = await brandService.GetAllBrandsAsync();
                return Ok(brands);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
