using DTO.CarPools;
using DTO.Dates;
using DTO.Rent;
using Models;

namespace IRepositories
{
    public interface ICarPoolRepository
    {
        public Task<int> CreateCarpoolAsync(CreateOneCarPoolDTO createOneCarPoolDTO);
        public Task<GetOneRentDTO> UpdateCarPoolByIdAsync(int rentID);
        public Task<int> DeleteCarPoolByIdAsync(CarPool carpool);
        public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByEndDateAsync(GetCarpoolByDateDTO dateDTO);
        public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolsByDateForkAsync(DateForkDTO dateForkDTO);
        public Task<GetOneCarPoolWithPassengersDTO?> GetCarPoolByRentAsync(int rentID);
        public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByStartDateAsync(GetCarpoolByDateDTO dateDTO);
        public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByDriverIdAsync(string userId);
        public Task<List<GetOneCarPoolDTO>> GetAllCarPoolAsync();
        public Task<CarPool?> GetCarPoolByIdAsync(int carPoolID);
    }
}
