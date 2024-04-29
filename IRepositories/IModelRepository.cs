using DTO.Models;
using Models;
using DTO.Dates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IModelRepository
    {
        public Task<GetOneModelDTO> CreateModelAsync(CreateOneModelDTO createModelDTO);
        public Task<GetOneModelDTO?> UpdateOneModelByIdAsync(GetOneModelDTO updatedModelDTO);
        //public Task DeleteOneModelByIdAsync(int modelId);
        public Task<Model?> GetOneModelByIdAsync(int Id);
        public Task<List<GetOneModelDTO>> GetAllModelsAsync(int paginationIndex = 0, int pageSize = 10);

    }
}
