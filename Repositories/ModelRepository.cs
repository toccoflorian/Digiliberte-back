using DTO.Models;
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
    /// <summary>
    /// Class for all Model Tools
    /// </summary>
    public class ModelRepository : IModelRepository
    {
        private readonly DatabaseContext Context;
        public ModelRepository(DatabaseContext databaseContext)  // Dependancy injections
        {
            this.Context = databaseContext;
        }
        /// <summary>
        /// Create a model based on Creation DTO, Name string, Co2 double, Year Int
        /// </summary>
        /// <param name="createModelDTO"></param>
        /// <returns>GetOneModelDTO</returns>
        public async Task<GetOneModelDTO> CreateModelAsync(CreateOneModelDTO createModelDTO)
        {

            Model newModel = new Model
            {
                Label = createModelDTO.Name,
                CO2 = createModelDTO.Co2,
                Year = createModelDTO.Year
            };

            await Context.Models.AddAsync(newModel);
            await Context.SaveChangesAsync();

            return new GetOneModelDTO
            {
                Id = newModel.Id,
                Name = newModel.Label,
                Co2 = newModel.CO2,
                Year = newModel.Year
            };
        }
        /// <summary>
        /// Update one model by async
        /// </summary>
        /// <param name="updatedModelDTO"> Take the model to update it and get the corresponding ID</param>
        /// <returns></returns>
        /// <exception cref="Exception">Throwed if Id not found</exception>
        public async Task<GetOneModelDTO?> UpdateOneModelByIdAsync(GetOneModelDTO updatedModelDTO)
        {
            // Recherchez le modèle existant dans la base de données en fonction de son ID
            var existingModel = await Context.Models.FindAsync(updatedModelDTO.Id);

            if (existingModel == null)
            {
                // Si le modèle n'est pas trouvé, vous pouvez choisir de retourner null ou de lever une exception
                // Ici, je choisis de retourner null
                throw new Exception("Id not found");
            }

            // Mettez à jour les propriétés du modèle existant avec les nouvelles valeurs
            existingModel.Label = updatedModelDTO.Name;
            existingModel.CO2 = updatedModelDTO.Co2;
            existingModel.Year = updatedModelDTO.Year;

            // Enregistrez les modifications dans la base de données
            await Context.SaveChangesAsync();

            // Retournez le modèle mis à jour sous forme de DTO
            return new GetOneModelDTO
            {
                Id = existingModel.Id,
                Name = existingModel.Label,
                Year = existingModel.Year,
                Co2 = existingModel.CO2
            };
        }

        /// <summary>
        /// Delete a model by Id , used Id to know if  exists
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteOneModelByIdAsync(int modelId)
        {
            var modelToDelete = await Context.Models.FindAsync(modelId);

            if (modelToDelete != null)
            {
                Context.Models.Remove(modelToDelete);
                await Context.SaveChangesAsync();
            }
            // Si le modèle n'existe pas, il n'y a rien à supprimer
        }

        /// <summary>
        /// Get a model by Id , used Id to know if  exists already
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Model?> GetOneModelByIdAsync(int Id)
        {
            return await Context.Models.FirstOrDefaultAsync(c=>c.Id == Id);
        }


        /// <summary>
        /// Get all models
        /// </summary>
        /// <returns>List of model DTOs</returns>
        public async Task<List<GetOneModelDTO>> GetAllModelsAsync()
        {
            // Interrogez la base de données pour obtenir toutes les motorisations
            var models = await Context.Models.ToListAsync();

            // Convertissez les objets Model en DTOs
            var modelDTOs = models.Select(m => new GetOneModelDTO
            {
                Id = m.Id,
                Name = m.Label
            }).ToList();

            return modelDTOs;
        }

    }
}
