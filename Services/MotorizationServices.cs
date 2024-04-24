using DTO.Motorization;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return await motorizationRepository.CreateOneMotorizationAsync(createMotorizationDTO);
            }
            throw new Exception("This Motorization already exists");
        }
    }
}
