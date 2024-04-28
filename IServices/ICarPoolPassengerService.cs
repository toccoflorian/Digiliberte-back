using DTO.CarPoolPassenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface ICarPoolPassengerService
    {
        public Task<GetOneCarPoolPassengerDTO> CreateCarPoolPassengerAsync(CreateCarPoolPassengerDTO createCarPoolPassengerDTO);
        public Task DeleteCarPoolPassengerByIdAsync(int carPoolPassengerId);
        public Task<GetOneCarPoolPassengerDTO> UpdateCarPoolPassengerByIdAsync(int carPoolPassengerId);
        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengerByDescriptionLocalizationAsync(int localizationID);
        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengerByDescriptionDateAsync(DateTime dateTime);
        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengersByUserAsync(string userID);
        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengersByCarPoolAsync(int carPoolID);
        public Task<List<GetOneCarPoolPassengerDTO>> GetAllPassengersAsync();
        public Task<GetOneCarPoolPassengerDTO> GetPassengerByIdAsync(int carPoolPassengerID);
    }
}
