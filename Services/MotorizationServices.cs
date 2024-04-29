using DTO.Dates;
using DTO.Motorization;
using IRepositories;
using IServices;
using Repositories;

namespace Services
{
    public class MotorizationServices : IMotorizationService
    {
        // ----- Injection de dependances
        public readonly IMotorizationRepository _motorizationRepository;

        public MotorizationServices(IMotorizationRepository motorizationRepository)
        {
            this._motorizationRepository = motorizationRepository;
        }


        public async Task<GetOneMotorizationDTO?> CreateOneMotorizationAsync(CreateOneMotorizationDTO createMotorizationDTO)
        {
            if (await _motorizationRepository.GetMotorizationByName(createMotorizationDTO.Name) == null)
            {
                return await _motorizationRepository.CreateOneMotorizationAsync(createMotorizationDTO);
            }
            throw new Exception("This Motorization already exists");
        }

        public async Task<GetOneMotorizationDTO?> UpdateOneMotorizationByIdAsync(GetOneMotorizationDTO getOneMotorizationDTO)
        {

            // Vérifiez d'abord si un motorization avec le même id existe déjà dans la base de données
            var existingMotorization = await _motorizationRepository.UpdateOneMotorizationByIdAsync(getOneMotorizationDTO);


            // Si aucun motorization avec le même id n'existe, vous pouvez procéder à la mise à jour
            return existingMotorization;
        }

        //public async Task<bool> DeleteOneMotorizationByIdAsync(int modelId)
        //{

        //    // Vérifiez d'abord si un categorie avec le même id existe déjà dans la base de données
        //    var existingMotorization = await _motorizationRepository.GetOneMotorizationByIdAsync(modelId);

        //    // Si le categorie existe, procédez à sa suppression
        //    if (existingMotorization != null)
        //    {
        //        await _motorizationRepository.DeleteOneMotorizationByIdAsync(modelId);
        //        return true; // Indique que la suppression a été effectuée avec succès
        //    }
        //    else
        //    {
        //        throw new Exception("Id Not Found"); // Indique que le categorie n'a pas été trouvé, donc la suppression n'a pas été effectuée
        //    }
        //}

        public async Task<GetOneMotorizationDTO?> GetOneMotorizationByIdAsync(int modelId)
        {
            // Utilisez le repository pour récupérer le categorie par son ID
            var model = await _motorizationRepository.GetOneMotorizationByIdAsync(modelId);

            // Si le categorie existe, mappez-le vers un DTO et retournez-le
            if (model != null)
            {
                return model;
            }
            else
            {
                // Si le categorie n'existe pas, retournez null
                throw new Exception("Motorization not found");
            }
        }
        public async Task<List<GetOneMotorizationDTO>> GetAllMotorizationsAsync()
        {
            try
            {
                var motorizations = await _motorizationRepository.GetAllMotorizationsAsync();
                return motorizations;
            }
            catch (Exception ex)
            {
                // Gérer les exceptions appropriées
                throw new Exception("Failed to retrieve motorizations", ex);
            }
        }
    }
}
