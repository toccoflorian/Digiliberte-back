using DTO.CarPoolPassenger;
using DTO.Pagination;
using Models;

namespace IServices
{
    public interface ICarPoolPassengerService
    {
        public Task<GetOneCarPoolPassengerDTO> CreateCarPoolPassengerAsync(CreateCarPoolPassengerDTO createCarPoolPassengerDTO);
        public Task DeleteCarPoolPassengerByIdAsync(DeleteCarpoolPassengerDTO deleteCarpoolPassengerDTO);
        public Task<GetOneCarPoolPassengerDTO> UpdateCarPoolPassengerByIdAsync(UpdateCarPoolPassengerDTO updateCarpoolDTO);
        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengerByDescriptionLocalizationAsync(int localizationID);
        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengerByDescriptionDateAsync(DateTime dateTime);
        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengersByUserAsync(string userID);
        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengersByCarPoolAsync(int carPoolID);
        public Task<List<GetOneCarPoolPassengerDTO>> GetAllPassengersAsync(PageForkDTO pageKorkDTO);
        public Task<GetOneCarPoolPassengerDTO> GetPassengerByIdAsync(int carPoolPassengerID);
        public Task<CarPoolPassenger> GetCarpoolPassengerTypeAsync(int carpoolPassengerId);
    }
}
