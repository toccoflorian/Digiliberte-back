using DTO.Motorization;
using Repositories;

namespace Services
{
    public class MotorizationServices
    {
        // ----- Injection de dependances
        public readonly MotorizationRepository motorizationRepository;

        public MotorizationServices(MotorizationRepository motorizationRepository)
        {
            this.motorizationRepository = motorizationRepository;
        }


        public async Task<GetOneMotorizationDTO?> CreateMotorizationAsync(CreateOneMotorizationDTO createMotorizationDTO)
        {
            if (await motorizationRepository.GetMotorizationByName(createMotorizationDTO.Name) == null)
            {
                return null;
            }
            return await motorizationRepository.CreateOneMotorizationAsync(createMotorizationDTO);
        }
    }
}
