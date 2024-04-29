using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IModelService
    {
        public Task<GetOneModelDTO?> CreateModelAsync(CreateOneModelDTO createOneModel);
        public Task<GetOneModelDTO?> UpdateModelAsync(GetOneModelDTO getOneModel);
        public Task<bool> DeleteOneModelByIdAsync(int modelId);
        public Task<GetOneModelDTO?> GetOneModelByIdAsync(int Id);
        public Task<List<GetOneModelDTO>> GetAllModelsAsync();
    }
}
