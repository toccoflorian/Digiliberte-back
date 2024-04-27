using DTO.Dates;
using Models;


namespace IRepositories
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAllCategoriesAsync();
        public Task<GetOneCategoryDTO> CreateOneCategoryAsync(CreateOneCategoryDTO createOneCategoryDTO);
        public Task<GetOneCategoryDTO?> UpdateOneCategoryByIdAsync(GetOneCategoryDTO updatedCategoryDTO);
        public Task<bool> DeleteOneCategoryByIdAsync(int categoryId);
        public Task<GetOneCategoryDTO> GetOneCategoryByIdAsync(int Id);
    }
}
