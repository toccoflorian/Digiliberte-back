using DTO.CarPoolPassenger;
using DTO.CarPools;
using IRepositories;
using IServices;

namespace Services
{
    public class CarPoolPassengerService : ICarPoolPassengerService
    {
        private readonly ICarPoolPassengerRepository _carPoolPassengerRepository;
        private readonly ICarPoolRepository _carPoolRepository;
        public CarPoolPassengerService(ICarPoolPassengerRepository carPoolPassengerRepository, ICarPoolRepository carPoolRepository)
        {
            this._carPoolPassengerRepository = carPoolPassengerRepository;
            this._carPoolRepository = carPoolRepository;
        }

        public async Task<GetOneCarPoolPassengerDTO> CreateCarPoolPassengerAsync(CreateCarPoolPassengerDTO createCarPoolPassengerDTO)
        {
            GetOneCarPoolWithPassengersDTO? carpoolDTO = await this._carPoolRepository.GetCarPoolByIdAsync(createCarPoolPassengerDTO.CarPoolId);
            if(carpoolDTO == null)
            {
                throw new Exception("Aucune location ne correspond !");
            }
            if(carpoolDTO.FreeSeats < 1)
            {
                throw new Exception("Plus de place libre !");
            }
            int passengerId = await this._carPoolPassengerRepository.CreateCarPoolPassengerAsync(createCarPoolPassengerDTO);
            GetOneCarPoolPassengerDTO? passenger = await this._carPoolPassengerRepository.GetPassengerByIdAsync(passengerId);
            if(passenger == null)
            {
                throw new Exception("Une erreur s'est produite, le passager de covoiturage n'a pas été enregistré !");
            }
            return passenger;
        }

        public Task DeleteCarPoolPassengerByIdAsync(int carPoolPassengerId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneCarPoolPassengerDTO>> GetAllPassengersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengerByDescriptionDateAsync(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengerByDescriptionLocalizationAsync(int localizationID)
        {
            throw new NotImplementedException();
        }

        public async Task<GetOneCarPoolPassengerDTO> GetPassengerByIdAsync(int carPoolPassengerID)
        {
            GetOneCarPoolPassengerDTO? carPoolPassengerDTO = await this._carPoolPassengerRepository.GetPassengerByIdAsync(carPoolPassengerID);
            if(carPoolPassengerDTO == null )
            {
                throw new Exception("Aucun covoiturage ne porte cette Id !");
            }
            return carPoolPassengerDTO;
        }

        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengersByCarPoolAsync(int carPoolID)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengersByUserAsync(string userID)
        {
            throw new NotImplementedException();
        }

        public Task<GetOneCarPoolPassengerDTO> UpdateCarPoolPassengerByIdAsync(int carPoolPassengerId)
        {
            throw new NotImplementedException();
        }
    }
}
