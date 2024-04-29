using DTO.Brands;
using Repositories;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRepositories;

namespace Services
{
    /// <summary>
    /// Brand Services used in controllers
    /// </summary>
    public class BrandServices : IBrandService
    {

        // DEPENDANCY INJECTION

        private readonly IBrandRepository brandRepository;
        public BrandServices(IBrandRepository brandRepository) { this.brandRepository = brandRepository; }

        /// <summary>
        /// Service to call repository CreateBrandAsync , returns GetOneBrandDTO
        /// </summary>
        /// <param name="createBrandDTO"></param>
        /// <returns></returns>
        public async Task<GetOneBrandDTO?> CreateBrandAsync(CreateOneBrandDTO createBrandDTO)
        {
            return await brandRepository.CreateBrandAsync(createBrandDTO);
        }

        public async Task<GetOneBrandDTO?> UpdateBrandAsync(GetOneBrandDTO getOneBrandDTO)
        {

            // Vérifiez d'abord si un modèle avec le même id existe déjà dans la base de données
            var existingBrand = await brandRepository.UpdateOneBrandByIdAsync(getOneBrandDTO);

            // Si aucun modèle avec le même id n'existe, vous pouvez procéder à la mise à jour
            return existingBrand;
        }

        public async Task<bool> DeleteOneBrandByIdAsync(int brandId)
        {

            // Vérifiez d'abord si un modèle avec le même id existe déjà dans la base de données
            var existingBrand = await brandRepository.GetOneBrandByIdAsync(brandId);

            // Si le brand existe, procédez à sa suppression
            if (existingBrand != null)
            {
                await brandRepository.DeleteOneBrandByIdAsync(brandId);
                return true; // Indique que la suppression a été effectuée avec succès
            }
            else
            {
                return false; // Indique que le modèle n'a pas été trouvé, donc la suppression n'a pas été effectuée
            }
        }

        public async Task<GetOneBrandDTO?> GetOneBrandByIdAsync(int brandId)
        {
            // Utilisez le repository pour récupérer le modèle par son ID
            var brand = await brandRepository.GetOneBrandByIdAsync(brandId);

            // Si le modèle existe, mappez-le vers un DTO et retournez-le
            if (brand != null)
            {
                return new GetOneBrandDTO
                {
                    Id = brand.Id,
                    Name = brand.Label
                };
            }
            else
            {
                // Si le modèle n'existe pas, retournez null
                return null;
            }
        }

        public async Task<List<GetOneBrandDTO>> GetAllBrandsAsync(int paginationIndex = 0, int pageSize = 10)
        {
            try
            {
                var brands = await brandRepository.GetAllBrandsAsync(paginationIndex, pageSize);
                return brands;
            }
            catch (Exception ex)
            {
                // Gérer les exceptions appropriées
                throw new Exception("Failed to retrieve brands", ex);
            }
        }
    }
}
