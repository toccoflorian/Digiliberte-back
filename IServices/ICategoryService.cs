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
    }
}
