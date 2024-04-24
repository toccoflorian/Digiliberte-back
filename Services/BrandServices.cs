using DTO.Brands;
using DTO.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Brand Services used in controllers
    /// </summary>
    public class BrandServices
    {

        // DEPENDANCY INJECTION
        public readonly BrandRepository brandRepository;
        public BrandServices(BrandRepository brandRepository) { this.brandRepository = brandRepository; }

        /// <summary>
        /// Service to call repository CreateBrandAsync , returns GetOneBrandDTO
        /// </summary>
        /// <param name="createBrandDTO"></param>
        /// <returns></returns>
        public async Task<GetOneBrandDTO?> CreateBrandAsync(CreateOneBrandDTO createBrandDTO)
        {
            return await brandRepository.CreateBrandAsync(createBrandDTO);
        }
    }
}
