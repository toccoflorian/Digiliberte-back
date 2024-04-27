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
            GetOneVehicleDTO vehicleDTO = await this._vehicleRepository.GetVehicleByIdAsync(createOneRentDTO.VehiceId);
            createOneRentDTO.VehicleInfos = $"{vehicleDTO.CategoryName}, {vehicleDTO.SeatsNumber} places, {vehicleDTO.BrandName}, {vehicleDTO.ModelName}, {vehicleDTO.ModelYear}";
            createOneRentDTO.Immatriculation = vehicleDTO.Immatriculation;

            // récuperation des informations de User
            GetOneUserDTO? userDTO = await this._userRepository.GetUserByIdAsync(createOneRentDTO.UserID) 
                ?? 
                throw new Exception("L'utilisateur est introuvable !");
            
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

        public Task<GetOneRentWithCarPoolDTO> GetRentByIdAsync(int rentID)
        {
            throw new NotImplementedException();
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

        public Task<GetOneRentDTO> UpdateRentByIdAsync(int rentID)
        {
            throw new NotImplementedException();
        }
    }
}
