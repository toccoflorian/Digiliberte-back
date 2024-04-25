using DTO.Brands;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    /// <summary>
    /// Class for Brand Repositories
    /// </summary>
    public class BrandRepository
    {
        public DatabaseContext Context { get; set; }
        public BrandRepository(DatabaseContext databaseContext)  // Dependancy injections
        {
            this.Context = databaseContext;
        }

        /// <summary>
        /// Create a brand based on Creation DTO, Name string
        /// </summary>
        /// <param name="createBrandDTO"></param>
        /// <returns>GetOneBrandDTO</returns>
        public async Task<GetOneBrandDTO> CreateBrandAsync(CreateOneBrandDTO createBrandDTO)
        {
            Brand newBrand = new Brand
            {
                Label = createBrandDTO.Name
            };

            Context.Brands.Add(newBrand);
            await Context.SaveChangesAsync();

            return new GetOneBrandDTO
            {
                Id = newBrand.Id,
                Name = newBrand.Label
            };
        }

        public async Task<GetOneBrandDTO?> UpdateOneBrandByIdAsync(GetOneBrandDTO updatedBrandDTO)
        {
            // Recherchez le brand existant dans la base de données en fonction de son ID
            var existingBrand = await Context.Brands.FindAsync(updatedBrandDTO.Id);

            if (existingBrand == null)
            {
                // Si le Brnad n'est pas trouvé, vous pouvez choisir de retourner null ou de lever une exception
                // Ici, je choisis de retourner null
                throw new Exception("Id not found");
            }

            // Mettez à jour les propriétés du brand existant avec les nouvelles valeurs
            existingBrand.Label = updatedBrandDTO.Name;


            // Enregistrez les modifications dans la base de données
            await Context.SaveChangesAsync();

            // Retournez le brand mis à jour sous forme de DTO
            return new GetOneBrandDTO
            {
                Id = existingBrand.Id,
                Name = existingBrand.Label,
            };
        }

        /// <summary>
        /// Delete a brand by Id , used Id to know if  exists
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteOneBrandByIdAsync(int brandId)
        {
            var brandToDelete = await Context.Brands.FindAsync(brandId);

            if (brandToDelete != null)
            {
                Context.Brands.Remove(brandToDelete);
                await Context.SaveChangesAsync();
            }
            // Si le brand n'existe pas, il n'y a rien à supprimer
        }

        /// <summary>
        /// Get a brand by Id , used Id to know if  exists already
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Brand?> GetOneBrandByIdAsync(int Id)
        {
            return await Context.Brands.FirstOrDefaultAsync(c => c.Id == Id);
        }

    }
}
