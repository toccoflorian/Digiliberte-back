using DTO.Dates;
using DTO.Models;
using IServices;
using IRepositories;
using Repositories;
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

        /// <summary>
        /// Create one category 
        /// </summary>
        /// <param name="createOneCategoryDTO"></param>
        /// <returns>GetOneCategory DTO </returns>
        public async Task<GetOneCategoryDTO> CreateOneCategoryAsync(CreateOneCategoryDTO createOneCategoryDTO)
        {
            return await this._categoryRepository.CreateOneCategoryAsync(createOneCategoryDTO);
        }

        public async Task<GetOneCategoryDTO?> UpdateOneCategoryByIdAsync(GetOneCategoryDTO getOneCategoryDTO)
        {

            // Vérifiez d'abord si un categorie avec le même id existe déjà dans la base de données
            var existingCategory = await _categoryRepository.UpdateOneCategoryByIdAsync(getOneCategoryDTO);

            
            // Si aucun categorie avec le même id n'existe, vous pouvez procéder à la mise à jour
            return existingCategory;
        }

        public async Task<bool> DeleteOneCategoryByIdAsync(int modelId)
        {

            // Vérifiez d'abord si un categorie avec le même id existe déjà dans la base de données
            var existingCategory = await _categoryRepository.GetOneCategoryByIdAsync(modelId);

            // Si le categorie existe, procédez à sa suppression
            if (existingCategory != null)
            {
                await _categoryRepository.DeleteOneCategoryByIdAsync(modelId);
                return true; // Indique que la suppression a été effectuée avec succès
            }
            else
            {
                throw new Exception("Id Not Found"); // Indique que le categorie n'a pas été trouvé, donc la suppression n'a pas été effectuée
            }
        }

        public async Task<GetOneCategoryDTO?> GetOneCategoryByIdAsync(int modelId)
        {
            // Utilisez le repository pour récupérer le categorie par son ID
            var model = await _categoryRepository.GetOneCategoryByIdAsync(modelId);

            // Si le categorie existe, mappez-le vers un DTO et retournez-le
            if (model != null)
            {
                return model;
            }
            else
            {
                // Si le categorie n'existe pas, retournez null
                throw new Exception("Category not found");
            }
        }
    }
}
