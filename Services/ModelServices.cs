using DTO.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{

    /// <summary>
    /// Class for all models services
    /// </summary>
    public class ModelServices
    {
        // ----- Injection de dependances
        public readonly ModelRepository ModelRepository;

        public ModelServices(ModelRepository modelRepository)
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
    }
}
