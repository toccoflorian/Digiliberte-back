using DTO.Dates;
using DTO.Models;
using ICategoryServices;
using IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository) 
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task<GetOneCategoryDTO> CreateOneCategoryAsync(CreateOneCategoryDTO createOneCategoryDTO)
        {
            return await this._categoryRepository.CreateOneCategoryAsync(createOneCategoryDTO);
        }
    }
}
