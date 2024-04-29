using DTO.Motorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IMotorizationService
    {
        public Task<GetOneMotorizationDTO?> CreateOneMotorizationAsync(CreateOneMotorizationDTO createMotorizationDTO);
        public Task<GetOneMotorizationDTO?> GetOneMotorizationByIdAsync(int modelId);
        public Task<GetOneMotorizationDTO?> UpdateOneMotorizationByIdAsync(GetOneMotorizationDTO getOneMotorizationDTO);
        //public Task<bool> DeleteOneMotorizationByIdAsync(int modelId);
        public Task<List<GetOneMotorizationDTO>> GetAllMotorizationsAsync(int paginationIndex = 0, int pageSize = 10);

    }
}
