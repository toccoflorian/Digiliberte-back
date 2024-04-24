using DTO.Dates;


namespace IRepositories
{
    public interface ICategoryRepository
    {
        public Task<GetOneCategoryDTO> CreateOneCategoryAsync(CreateOneCategoryDTO createOneCategoryDTO);
    }
}
