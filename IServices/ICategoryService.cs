using DTO.Dates;

namespace IServices
{
    public interface ICategoryService
    {
        public Task<GetOneCategoryDTO> CreateOneCategoryAsync(CreateOneCategoryDTO createOneCategoryDTO);
        public Task<GetOneCategoryDTO?> GetOneCategoryByIdAsync(int modelId);
        public Task<GetOneCategoryDTO?> UpdateOneCategoryByIdAsync(GetOneCategoryDTO getOneCategoryDTO);
        public Task<bool> DeleteOneCategoryByIdAsync(int modelId);
        public Task<List<GetOneCategoryDTO>> GetAllCategorysAsync(int paginationIndex = 0, int pageSize = 10);
    }
}
