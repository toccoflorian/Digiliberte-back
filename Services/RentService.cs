using DTO.Dates;
using DTO.Rent;
using DTO.User;
using DTO.Vehicles;
using IRepositories;
using IServices;
using Microsoft.AspNetCore.Identity;
using Models;

namespace Services
{
    public class RentService : IRentService
    {
        private readonly IRentRepository _rentRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDateRepository _dateRepository;
        private readonly UserManager<AppUser> _userManager;
        public RentService(
            IRentRepository rentRepository, 
            IVehicleRepository vehicleRepository,
            IUserRepository userRepository,
            IDateRepository dateRepository,
            UserManager<AppUser> userManager)
        {
            this._rentRepository = rentRepository;
            this._vehicleRepository = vehicleRepository;
            this._userRepository = userRepository;
            this._dateRepository = dateRepository;
            this._userManager = userManager;
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

        public Task<List<GetOneRentDTO>> GetRentByVehicleIdAsync(int vehicleId)
        {
            throw new NotImplementedException();
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

            // check si le rentEntity est valide / pas nul
            GetOneRentDTO? getRent = await this._rentRepository.GetRentByIdAsync(updateRentRequestDTO.Id);
            if (getRent == null)
            {
                throw new Exception("Aucune location ne correspond à cette id !");
            }

            var getStartDate = await._this

            // check sur les propriétés saisis :



            throw new NotImplementedException();
        }
    }
}
