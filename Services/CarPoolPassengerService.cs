using DTO.CarPoolPassenger;
using DTO.CarPools;
using IRepositories;
using IServices;
using Models;
using Utils.Constants;

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

        public async Task DeleteCarPoolPassengerByIdAsync(DeleteCarpoolPassengerDTO deleteCarPoolPassengerDTO)
        {

            //  recuperation du carpoolPassenger et verification que le carpoolPassenger correspond à l'utilisateur connecté
            GetOneCarPoolPassengerDTO? carpoolPassenger = await this._carPoolPassengerRepository.GetPassengerByIdAsync(deleteCarPoolPassengerDTO.Id);
            if(carpoolPassenger == null)
            {
                throw new Exception("Aucun covoiturage ne correspond à cette id !");
            }
            GetOneCarPoolWithPassengersDTO? carpool = await this._carPoolRepository.GetCarPoolByIdAsync(carpoolPassenger.CarPoolId);
            if(deleteCarPoolPassengerDTO.ConnectedUserId != carpoolPassenger.UserDTO.Id
                &&
                deleteCarPoolPassengerDTO.ConnectedUserId != carpool.UserId
                &&
                deleteCarPoolPassengerDTO.ConnectedUserRole?.ToUpper() != ROLE.ADMIN)
            {
                throw new Exception("Vous devez être le conducteur du covoiturage, l'auteur de la reservation ou ADMIN pour pouvoir la supprimer");
            }
            //verification du carpoolPassenger
            if (carpoolPassenger == null)
            {
                throw new Exception("Aucun passager de covoiturage ne correspond à cette id !");
            }
            // suppression et verification de la suppression du carpoolPassenger
            if (await this._carPoolPassengerRepository.DeleteCarPoolPassengerByIdAsync(deleteCarPoolPassengerDTO.Id) < 1)
            {
                throw new Exception("Une erreur est survenue, suppression non effectuée !");
            }
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
