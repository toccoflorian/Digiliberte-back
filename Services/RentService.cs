using DTO.Dates;
using DTO.Rent;
using DTO.User;
using DTO.Vehicles;
using IRepositories;
using IServices;
using Microsoft.AspNetCore.Identity;
using Models;
using Repositories.Helper;

namespace Services
{
    public class RentService : IRentService
    {
        private readonly IRentRepository _rentRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDateRepository _dateRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly RentHelper _rentHelper;
        public RentService(
            IRentRepository rentRepository, 
            IVehicleRepository vehicleRepository,
            IUserRepository userRepository,
            IDateRepository dateRepository,
            UserManager<AppUser> userManager,
            RentHelper rentHelper)
        {
            this._rentRepository = rentRepository;
            this._vehicleRepository = vehicleRepository;
            this._userRepository = userRepository;
            this._dateRepository = dateRepository;
            this._userManager = userManager;
            this._rentHelper = rentHelper;
        }

        /// <summary>
        /// Create a new Rent
        /// </summary>
        /// <param name="createOneRentDTO"></param>
        /// <returns>the created rent formated with GetOneRentDTO</returns>
        public async Task<GetOneRentDTO> CreateOneRentAsync(CreateRentDTO createOneRentDTO)
        {
            if(createOneRentDTO.StartDate <  DateTime.Now)
            {
                throw new Exception("La date de début de reservation dois être dans le futur !");
            }
            if(createOneRentDTO.ReturnDate <= createOneRentDTO.StartDate)
            {
                throw new Exception("La date de retour doit être superieur à la date de début !");
            }

            // récuperation des informations de Vehicle
            GetOneVehicleDTO? vehicleDTO = await this._vehicleRepository.GetVehicleByIdAsync(createOneRentDTO.VehiceId);
            if(vehicleDTO == null)
            {
                throw new Exception("Aucun véhicule ne correspond à l'id spécifier !");
            }
            createOneRentDTO.VehicleInfos = $"{vehicleDTO.CategoryName}, {vehicleDTO.SeatsNumber} places, {vehicleDTO.BrandName}, {vehicleDTO.ModelName}, {vehicleDTO.ModelYear}";
            createOneRentDTO.Immatriculation = vehicleDTO.Immatriculation;

            // récuperation des informations de User
            GetOneUserDTO? userDTO = await this._userRepository.GetUserByIdAsync(createOneRentDTO.UserID);
            if(userDTO == null)
            {
                throw new Exception("L'utilisateur est introuvable !");
            }
            createOneRentDTO.UserFirstname = userDTO.Firstname;
            createOneRentDTO.UserLastname = userDTO.Lastname;

            // création des DatesClass et récuperation des id
            GetOneDateDTO startDateDTO = await this._dateRepository.CreateAsync(createOneRentDTO.StartDate);
            GetOneDateDTO returnDateDTO = await this._dateRepository.CreateAsync(createOneRentDTO.ReturnDate);
            createOneRentDTO.StartDateId  = startDateDTO.Id;
            createOneRentDTO.ReturnDateId = returnDateDTO.Id;

            return await this._rentRepository.CreateOneRentAsync(createOneRentDTO);
        }

        public Task DeleteRentByIdAsync(int rentID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all rents 
        /// </summary>
        /// <returns>List of Rent formated with GetOneRentDTO</returns>
        public async Task<List<GetOneRentDTO>> GetAllRentAsync()
        {
            return await this._rentRepository.GetAllRentAsync();
        }

        public Task<GetOneRentDTO> GetRentByCarPoolAsync(int carPoolID)
        {
            throw new NotImplementedException();
        }

        public async Task<GetOneRentDTO> GetRentByIdAsync(int rentID)
        {
            GetOneRentDTO? rentDTO = await this._rentRepository.GetRentByIdAsync(rentID);
            if (rentDTO == null)
            {
                throw new Exception("Aucune location ne correspond à cette id !");
            }
            return rentDTO;
        }
        /// <summary>
        /// Asynchronous service to getRentSimpleByVehicleId , almost the same as 
        /// <see cref="GetRentByVehicleIdAsync(int)"/> but returns a Different DTO
        /// </summary>
        /// <param name="vehicleId">Id of the looked vehicle</param>
        /// <returns>Return a List of <see cref="GetOneRentByVehicleIdDTO"/></returns>
        /// <exception cref="Exception">Exception for id == 0 or <0 , or if no vehicles found</exception>
        public async Task<List<GetOneRentByVehicleIdDTO>> GetRentSimpleByVehicleIdAsync(int vehicleId)
        {
            if (vehicleId == 0) { throw new Exception("Id can not be 0"); }
            if(vehicleId < 0) { throw new Exception("Id can't be negative"); }

            List<GetOneRentByVehicleIdDTO> rentList = await _rentRepository.GetRentByVehicleIdAsync(vehicleId);
            // renvois erreur si liste vide
            if (rentList == null) { throw new Exception($"No rents found for this vehicle {vehicleId}"); }

            return rentList;
        }

        public Task<List<GetOneRentDTO>> GetRentsByDateForkAsync(DateForkDTO dateForkDTO)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneRentDTO>> GetRentsByEndDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneRentDTO>> GetRentsByStartDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneRentWithCarPoolDTO>> GetRentsByUserAsync(string userID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Will check here all potentials errors before updating
        /// </summary>
        /// <param name="updateRentRequestDTO"></param>
        /// <returns></returns>
        public async Task<GetOneRentDTO> UpdateRentByIdAsync(UpdateRentRequestDTO updateRentRequestDTO)
        {
            // Check sur la requete et cohérence de la requete
            if(updateRentRequestDTO == null) { throw new Exception("Request cannot be null"); }
            if(updateRentRequestDTO.Id == 0) { throw new Exception("Id cannot be 0"); }
            if(updateRentRequestDTO.Id < 0) { throw new Exception("Id cannot be < 0");  }
            if(updateRentRequestDTO.ReturnDate > updateRentRequestDTO.StartDate) { throw new Exception("New Return Date cannot be less than new Start Date"); }
            if(updateRentRequestDTO.ReturnDate == updateRentRequestDTO.StartDate) { throw new Exception("New dates cannot be the sames"); }
            if((updateRentRequestDTO.ReturnDate - updateRentRequestDTO.StartDate) > TimeSpan.FromDays(60)) { throw new Exception("Rent duration cannot exceed 2 months!"); }
            if((updateRentRequestDTO.ReturnDate - updateRentRequestDTO.StartDate) < TimeSpan.FromMinutes(30)) { throw new Exception("Your rent must be at least 30 minutes"); }
            if(updateRentRequestDTO.StartDate <= DateTime.Now) { throw new Exception("The new Start Date must be in the future"); }
            if(updateRentRequestDTO.ReturnDate <= DateTime.Now) { throw new Exception("The new Return Date must be in the future"); }


            // check si le rentEntity est valide / pas nul
            GetOneRentDTO? getRent = await this._rentRepository.GetRentByIdAsync(updateRentRequestDTO.Id);
            if (getRent == null)
            {
                throw new Exception("No Rentals found for this Id!");
            }
            // check si les dates saisis ne coïncident pas avec dates de locations déjà existantes, auquel cas pas de modifications possible
            if (await this._rentHelper.isVehicleRentedAsync(getRent.VehiceId, updateRentRequestDTO.StartDate, updateRentRequestDTO.ReturnDate))
            {
                throw new Exception("The car is rented at the selected dates");
            }

            // check si les dates saisies existent dans la BDD
            int? getStartDateId = await this._dateRepository.GetCloseDateAsync(updateRentRequestDTO.StartDate);
            int? getReturnDateId = await this._dateRepository.GetCloseDateAsync(updateRentRequestDTO.ReturnDate);

            // check if the dates exists in DB within one minutes , if not
            GetOneDateDTO startDateDTO;
            GetOneDateDTO returnDateDTO;
            if (getStartDateId == null || getReturnDateId == null)
            {
                startDateDTO = await this._dateRepository.CreateAsync(updateRentRequestDTO.StartDate); 
                returnDateDTO = await this._dateRepository.CreateAsync(updateRentRequestDTO.ReturnDate);
            }



            throw new NotImplementedException();
        }

        public Task<List<GetOneRentDTO>> GetRentByVehicleIdAsync(int vehicleId) {  throw new NotImplementedException(); }
    }
}
