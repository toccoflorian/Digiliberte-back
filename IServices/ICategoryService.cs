using DTO.Dates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICategoryServices
{
    public interface ICategoryService
    {
        public Task<GetOneCategoryDTO> CreateOneCategoryAsync(CreateOneCategoryDTO createOneCategoryDTO);
        public Task<GetOneCategoryDTO?> GetOneCategoryByIdAsync(int modelId);
        public Task<GetOneCategoryDTO?> UpdateOneCategoryByIdAsync(GetOneCategoryDTO getOneCategoryDTO);
        public Task<bool> DeleteOneCategoryByIdAsync(int modelId);
    }
}
