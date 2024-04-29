using DTO.Brands;
using DTO.Dates;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IBrandRepository
    {
        public Task<GetOneBrandDTO> CreateBrandAsync(CreateOneBrandDTO createBrandDTO);
        public Task<GetOneBrandDTO?> UpdateOneBrandByIdAsync(GetOneBrandDTO updatedBrandDTO);
        public Task DeleteOneBrandByIdAsync(int brandId);
        public Task<Brand?> GetOneBrandByIdAsync(int Id);
        public Task<List<GetOneBrandDTO>> GetAllBrandsAsync(int paginationIndex = 0, int pageSize = 10);
    }
}
