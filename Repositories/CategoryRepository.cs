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


        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        /// <summary>
        /// Create One category Async
        /// </summary>
        /// <param name="createOneCategoryDTO"></param>
        /// <returns></returns>
        public async Task<GetOneCategoryDTO> CreateOneCategoryAsync(CreateOneCategoryDTO createOneCategoryDTO)
        {
            //Category
            var newCategory = new Category
            {
                Label = createOneCategoryDTO.Name,
                SeatsNumber = createOneCategoryDTO.SeatsNumber,
            };
            await _context.Categories.AddAsync(newCategory);

            await _context.SaveChangesAsync();

            return new GetOneCategoryDTO
            {
                ID = newCategory.Id,
                Name = newCategory.Label,
                SeatsNumber = newCategory.SeatsNumber
            };
        }
        /// <summary>
        /// Update une categorie avec DTO en entree qui cherche sur l'id 
        /// </summary>
        /// <param name="updatedCategoryDTO"></param>
        /// <returns>renvois le DTO update si modifié</returns>
        /// <exception cref="Exception"> Renvois une exception si cat innexistante</exception>
        public async Task<GetOneCategoryDTO?> UpdateOneCategoryByIdAsync(GetOneCategoryDTO updatedCategoryDTO)
        {
            // Recherchez le categorie existant dans la base de données en fonction de son ID
            var existingCategory = await _context.Categories.FindAsync(updatedCategoryDTO.ID);

            if (existingCategory == null)
            {
                // Si le categorie n'est pas trouvé, vous pouvez choisir de retourner null ou de lever une exception
                // Ici, je choisis de retourner null
                throw new Exception("Id not found");
            }

            // Mettez à jour les propriétés du categorie existant avec les nouvelles valeurs
            existingCategory.Label = updatedCategoryDTO.Name;
            existingCategory.SeatsNumber = updatedCategoryDTO.SeatsNumber;

            // Enregistrez les modifications dans la base de données
            _context.Update(existingCategory);

            await _context.SaveChangesAsync();

            // Retournez le categorie mis à jour sous forme de DTO
            return new GetOneCategoryDTO
            {
                ID = existingCategory.Id,
                Name = existingCategory.Label,
                SeatsNumber = existingCategory.SeatsNumber,
            };
        }

        /// <summary>
        /// Delete a category by Id , used Id to know if  exists
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Bool , true si modifié, false si non modifié </returns>
        public async Task<bool> DeleteOneCategoryByIdAsync(int categoryId)
        {
            var categoryToDelete = await _context.Categories.FindAsync(categoryId);

            if (categoryToDelete != null)
            {
                _context.Categories.Remove(categoryToDelete);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
            // Si le modèle n'existe pas, il n'y a rien à supprimer
        }

        /// <summary>
        /// Get a category by Id , used Id to know if  exists already
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<GetOneCategoryDTO?> GetOneCategoryByIdAsync(int Id)
        {
            var getCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == Id);

            if (getCategory != null)
            {
                GetOneCategoryDTO? getOneCategoryDTO = new GetOneCategoryDTO
                {
                    ID = getCategory.Id,
                    Name = getCategory.Label,
                    SeatsNumber = getCategory.SeatsNumber,
                };
                return getOneCategoryDTO;
            }
            else
            {
                return null;
            }

        }
    }
}
