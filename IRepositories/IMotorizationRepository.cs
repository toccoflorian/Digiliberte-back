using DTO.Motorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IMotorizationRepository
    {
        public Task<GetOneMotorizationDTO?> CreateOneMotorizationAsync(CreateOneMotorizationDTO createMotorizationDTO);
        public Task<string?> GetMotorizationByName(string Name);
        public Task<GetOneMotorizationDTO?> UpdateOneMotorizationByIdAsync(GetOneMotorizationDTO updatedMotorizationDTO);
        public Task<bool> DeleteOneMotorizationByIdAsync(int motorizationId);
        public Task<GetOneMotorizationDTO?> GetOneMotorizationByIdAsync(int Id);
    }
}
