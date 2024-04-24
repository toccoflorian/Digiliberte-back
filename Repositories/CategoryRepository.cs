using DTO.Dates;
using IRepositories;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext _context;
        public CategoryRepository(DatabaseContext context)
        {
            this._context = context;
        }
        /// <summary>
        /// Create One category Async
        /// </summary>
        /// <param name="createOneCategoryDTO"></param>
        /// <returns></returns>
        public async Task<GetOneCategoryDTO> CreateOneCategoryAsync(CreateOneCategoryDTO createOneCategoryDTO)
        {
            //Category
            await _context.Categories.AddAsync(new Category
            {
                Label = createOneCategoryDTO.Name,
                SeatsNumber = createOneCategoryDTO.SeatsNumber,
            });

            await _context.SaveChangesAsync();
            return new GetOneCategoryDTO
            {
                ID = (await this._context.Categories.FirstOrDefaultAsync(c => c.Label == createOneCategoryDTO.Name)).Id,
                Name = createOneCategoryDTO.Name,
                SeatsNumber = createOneCategoryDTO.SeatsNumber
            };
        }
    }
}
