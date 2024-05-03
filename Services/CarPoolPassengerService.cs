using DTO.CarPoolPassenger;
using DTO.CarPools;
using DTO.Pagination;
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
            CarPool? carpoolDTO = await this._carPoolRepository.GetCarPoolTypeByIdAsync(createCarPoolPassengerDTO.CarPoolId);
            if(carpoolDTO == null)
            {
                throw new Exception("Aucune location ne correspond !");
            }
            if(carpoolDTO.Rent.Vehicle.Category.SeatsNumber - carpoolDTO.carPoolPassengers?.Count() < 1)
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
            CarPool? carpool = await this._carPoolRepository.GetCarPoolTypeByIdAsync(carpoolPassenger.CarPoolId);
            if(deleteCarPoolPassengerDTO.ConnectedUserId != carpoolPassenger.UserDTO.Id
                &&
                deleteCarPoolPassengerDTO.ConnectedUserId != carpool!.Rent.UserID
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

        /// <summary>
        /// Get all passengers
        /// </summary>
        /// <param name="pageKorkDTO"></param>
        /// <param name="paginationIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<List<GetOneCarPoolPassengerDTO>> GetAllPassengersAsync(PageForkDTO pageKorkDTO, int paginationIndex = 0, int pageSize = 10)
        {
            if (pageKorkDTO == null) { throw new Exception("No passengers were found"); }
            if (paginationIndex < 0) { throw new Exception("Pagination index Error"); }
            if (pageSize < 0) { throw new Exception("Page size Error"); }
            return await this._carPoolPassengerRepository.GetAllPassengersAsync(pageKorkDTO);
        }

        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengerByDescriptionDateAsync(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengerByDescriptionLocalizationAsync(int localizationID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a Passenger by id
        /// </summary>
        /// <param name="carPoolPassengerID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<GetOneCarPoolPassengerDTO> GetPassengerByIdAsync(int carPoolPassengerID)
        {
            if(carPoolPassengerID == ' '){throw new Exception("A carPool passenger iD must be provided ! ");}
            if(carPoolPassengerID < 0){throw new Exception("A valid carPool passenger Id sould be provided ! ");}
            GetOneCarPoolPassengerDTO? carPoolPassengerDTO = await this._carPoolPassengerRepository.GetPassengerByIdAsync(carPoolPassengerID);
            if(carPoolPassengerDTO == null ){throw new Exception("No passengers was found!");}
            return carPoolPassengerDTO;
        }

        /// <summary>
        /// Get passengers of a carpool by carpoolID
        /// </summary>
        /// <param name="carPoolID"></param>
        /// <param name="paginationIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<GetOneCarPoolPassengerDTO>> GetPassengersByCarPoolAsync(int carPoolID, int paginationIndex = 0, int pageSize = 10)
        {
            if (paginationIndex < 0){throw new Exception("PaginationIndex Error");}
            if (pageSize < 0){throw new Exception("PageSize Error");}
            if (carPoolID < 0){throw new Exception("A valid carPoolID sould be provided");}
            if(carPoolID == ' '){throw new Exception("A corPoolID sould be provided");}
            List<GetOneCarPoolPassengerDTO>? carPoolPassengerDTO = await this._carPoolPassengerRepository.GetPassengersByCarPoolAsync(carPoolID);
            if (carPoolPassengerDTO == null){throw new Exception("No Carpool with this id was found");}
            return carPoolPassengerDTO;
        }

        /// <summary>
        /// Get passengers by user Id
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="paginationIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<GetOneCarPoolPassengerDTO>> GetPassengersByUserAsync(string userID, int paginationIndex = 0, int pageSize = 10)
        {
            if (paginationIndex < 0){throw new Exception("PaginationIndex Error");}
            if (pageSize < 0){throw new Exception("PageSize Error");}
            if (userID == null){throw new Exception("A user Id must be provided");}
            if(userID.Length == 0){throw new Exception("A correct userId should be provided");}

            List<GetOneCarPoolPassengerDTO>? carPoolPassengerDTO = await this._carPoolPassengerRepository.GetPassengersByUserAsync(userID);
            if (carPoolPassengerDTO == null){throw new Exception("No passenger with this id was found");}
            return carPoolPassengerDTO;
        }

        public async Task<CarPoolPassenger> GetCarpoolPassengerTypeAsync(int carpoolPassengerId)
        {
            CarPoolPassenger? carpoolPassenger = await this._carPoolPassengerRepository.GetCarpoolPassengerTypeAsync(carpoolPassengerId);
            if (carpoolPassenger == null)
            {
                throw new Exception("Le passager de covoiturage est introuvable !");
            }
            return carpoolPassenger;
        }

        public async Task<GetOneCarPoolPassengerDTO> UpdateCarPoolPassengerByIdAsync(UpdateCarPoolPassengerDTO updateCarpoolPassengerDTO)
        {
            CarPoolPassenger carpoolPassenger = await this.GetCarpoolPassengerTypeAsync(updateCarpoolPassengerDTO.Id);
            if(updateCarpoolPassengerDTO.CarpoolId != carpoolPassenger.CarPoolID  && updateCarpoolPassengerDTO.CarpoolId != null
                || 
                updateCarpoolPassengerDTO.Description != carpoolPassenger.Description && updateCarpoolPassengerDTO.Description != null)
            {
                await this.DeleteCarPoolPassengerByIdAsync(new DeleteCarpoolPassengerDTO
                {
                    Id = updateCarpoolPassengerDTO.Id,
                    ConnectedUserId = updateCarpoolPassengerDTO.UserId,
                    ConnectedUserRole = updateCarpoolPassengerDTO.UserRole
                });
                return await this.CreateCarPoolPassengerAsync(new CreateCarPoolPassengerDTO
                {
                    CarPoolId = updateCarpoolPassengerDTO.CarpoolId != null ? (int)updateCarpoolPassengerDTO.CarpoolId : carpoolPassenger.Id,
                    Description = updateCarpoolPassengerDTO.Description != null ? updateCarpoolPassengerDTO.Description : carpoolPassenger.Description,
                    UserId  = updateCarpoolPassengerDTO.UserId,
                });
                
            }
            throw new Exception("Aucune modification à effectuer !");
        }
    }
}
