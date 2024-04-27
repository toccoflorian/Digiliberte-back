using DTO.Models;
using IRepositories;
using IServices;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services
{

    /// <summary>
    /// Class for all models services
    /// </summary>
    public class ModelServices : IModelService
    {
        // ----- Injection de dependances
        private readonly IModelRepository ModelRepository;

        public ModelServices(IModelRepository modelRepository)
        {
            this.ModelRepository = modelRepository;
        }
        public async Task<GetOneModelDTO?> CreateModelAsync(CreateOneModelDTO createModelDTO)
        {
            return await ModelRepository.CreateModelAsync(createModelDTO);
        }

        public async Task<GetOneModelDTO?> UpdateModelAsync(GetOneModelDTO getOneModelDTO)
        {

            // Vérifiez d'abord si un modèle avec le même id existe déjà dans la base de données
            var existingModel = await ModelRepository.UpdateOneModelByIdAsync(getOneModelDTO);

            // Si aucun modèle avec le même id n'existe, vous pouvez procéder à la mise à jour
            return existingModel;
        }

        public async Task<bool> DeleteOneModelByIdAsync(int modelId)
        {

            // Vérifiez d'abord si un modèle avec le même id existe déjà dans la base de données
            var existingModel = await ModelRepository.GetOneModelByIdAsync(modelId);

            // Si le modèle existe, procédez à sa suppression
            if (existingModel != null)
            {
                await ModelRepository.DeleteOneModelByIdAsync(modelId);
                return true; // Indique que la suppression a été effectuée avec succès
            }
            else
            {
                return false; // Indique que le modèle n'a pas été trouvé, donc la suppression n'a pas été effectuée
            }
        }

        public async Task<GetOneModelDTO?> GetOneModelByIdAsync(int modelId)
        {
            // Utilisez le repository pour récupérer le modèle par son ID
            var model = await ModelRepository.GetOneModelByIdAsync(modelId);

            // Si le modèle existe, mappez-le vers un DTO et retournez-le
            if (model != null)
            {
                return new GetOneModelDTO
                {
                    Id = model.Id,
                    Name = model.Label,
                    Year = model.Year,
                    Co2 = model.CO2
                };
            }
            else
            {
                // Si le modèle n'existe pas, retournez null
                return null;
            }
        }

    }
}

