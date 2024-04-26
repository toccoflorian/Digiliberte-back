using DTO.Brands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IBrandService
    {
        public Task<GetOneBrandDTO?> CreateBrandAsync(CreateOneBrandDTO createBrandDTO);
        public Task<GetOneBrandDTO?> UpdateBrandAsync(GetOneBrandDTO getOneBrandDTO);
        public Task<bool> DeleteOneBrandByIdAsync(int brandId);
        public Task<GetOneBrandDTO?> GetOneBrandByIdAsync(int brandId);
    }
}
