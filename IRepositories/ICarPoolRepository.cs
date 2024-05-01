using DTO.CarPools;
using DTO.Dates;
using DTO.Rent;

namespace IRepositories
{
    public interface ICarPoolRepository
    {
        public Task<int> CreateCarpoolAsync(CreateOneCarPoolDTO createOneCarPoolDTO);
        public Task<GetOneRentDTO> UpdateCarPoolByIdAsync(int rentID);
        public Task DeleteCarPoolByIdAsync(int rentID);
        public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByEndDateAsync(GetCarpoolByDateDTO dateDTO);
        public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolsByDateForkAsync(DateForkDTO dateForkDTO);
        public Task<GetOneCarPoolWithPassengersDTO?> GetCarPoolByRentAsync(int rentID);
        public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByStartDateAsync(GetCarpoolByDateDTO dateDTO);
        public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByDriverIdAsync(string userId);
        public Task<List<GetOneCarPoolDTO>> GetAllCarPoolAsync();
        public Task<GetOneCarPoolWithPassengersDTO> GetCarPoolByIdAsync(int carPoolID);
    }
}
