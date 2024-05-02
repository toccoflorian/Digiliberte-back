using DTO.CarPools;
using DTO.Dates;
using DTO.Rent;
using DTO.User;
using DTO.Vehicles;
using IRepositories;
using IServices;
using Microsoft.AspNetCore.Identity;
using Models;
using Repositories;
using Repositories.Helper;
using System.Data;

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
        private readonly ICarPoolRepository _carPoolRepository;
        public RentService(
            IRentRepository rentRepository,
            IVehicleRepository vehicleRepository,
            IUserRepository userRepository,
            IDateRepository dateRepository,
            UserManager<AppUser> userManager,
            RentHelper rentHelper,
            ICarPoolRepository carPoolRepository)
        {
            this._rentRepository = rentRepository;
            this._vehicleRepository = vehicleRepository;
            this._userRepository = userRepository;
            this._dateRepository = dateRepository;
            this._userManager = userManager;
            this._rentHelper = rentHelper;
            this._carPoolRepository = carPoolRepository;
        }

        /// <summary>
        /// Create a new Rent
        /// </summary>
        /// <param name="createOneRentDTO"></param>
        /// <returns>the created rent formated with GetOneRentDTO</returns>
        public async Task<GetOneRentDTO> CreateOneRentAsync(CreateRentDTO createOneRentDTO)
        {
            if (createOneRentDTO.StartDate < DateTime.Now)
            {
                throw new Exception("La date de début de reservation dois être dans le futur !");
            }
            if (createOneRentDTO.ReturnDate <= createOneRentDTO.StartDate)
            {
                throw new Exception("La date de retour doit être superieur à la date de début !");
            }

            // récuperation des informations de Vehicle
            GetOneVehicleDTO? vehicleDTO = await this._vehicleRepository.GetVehicleByIdAsync(createOneRentDTO.VehiceId);
            if (vehicleDTO == null)
            {
                throw new Exception("Aucun véhicule ne correspond à l'id spécifier !");
            }
            createOneRentDTO.VehicleInfos = $"{vehicleDTO.CategoryName}, {vehicleDTO.SeatsNumber} places, {vehicleDTO.BrandName}, {vehicleDTO.ModelName}, {vehicleDTO.ModelYear}";
            createOneRentDTO.Immatriculation = vehicleDTO.Immatriculation;

            // récuperation des informations de User
            GetOneUserDTO? userDTO = await this._userRepository.GetUserByIdAsync(createOneRentDTO.UserID);
            if (userDTO == null)
            {
                throw new Exception("L'utilisateur est introuvable !");
            }
            createOneRentDTO.UserFirstname = userDTO.Firstname;
            createOneRentDTO.UserLastname = userDTO.Lastname;

            // création des DatesClass et récuperation des id
            GetOneDateDTO startDateDTO = await this._dateRepository.CreateAsync(createOneRentDTO.StartDate);
            GetOneDateDTO returnDateDTO = await this._dateRepository.CreateAsync(createOneRentDTO.ReturnDate);
            createOneRentDTO.StartDateId = startDateDTO.Id;
            createOneRentDTO.ReturnDateId = returnDateDTO.Id;

            return await this._rentRepository.CreateOneRentAsync(createOneRentDTO);
        }

        public Task DeleteRentByIdAsync(int rentID)
        {
            throw new NotImplementedException();
        } // TODO 

        /// <summary>
        /// Get all rents 
        /// </summary>
        /// <returns>List of Rent formated with GetOneRentDTO</returns>
        public async Task<List<GetOneRentDTO>> GetAllRentAsync(int pageSize = 10, int pageIndex = 0)
        {
            return await this._rentRepository.GetAllRentAsync(pageSize, pageIndex);
        }
        /// <summary>
        /// Get A rent with it's car Pools
        /// </summary>
        /// <param name="carPoolID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<GetOneRentWithCarPoolDTO> GetRentByCarPoolAsync(int carPoolID)
        {
            if (carPoolID < 0) { throw new Exception("Id can't be less than 0"); }
            int searchedRentId = await this._rentRepository.GetRentIdByCarPoolAsync(carPoolID);
            if (searchedRentId == 0)
            {
                throw new Exception("Rent not found for this carpoolId"); // MUST NEVER HAPPEN
            }

            GetOneRentDTO? rentDto = await this._rentRepository.GetRentByIdAsync(searchedRentId); // recupere le rent
            if (rentDto == null)
            {
                throw new Exception("No rent founds");
            }
            List<GetOneCarPoolWithPassengersDTO>? carpools = await this._carPoolRepository.GetCarPoolsByRentAsync(searchedRentId) ?? new List<GetOneCarPoolWithPassengersDTO>();


            //Mappe le rent DTO sur notre DTO de retour
            GetOneRentWithCarPoolDTO? rentWithCarPoolDTO = new GetOneRentWithCarPoolDTO
            {
                Id = rentDto.Id,
                UserId = rentDto.UserId,
                Immatriculation = rentDto.Immatriculation,
                StartDate = rentDto.StartDate,
                ReturnDate = rentDto.ReturnDate,
                UserFirstname = rentDto.UserFirstname,
                UserLastname = rentDto.UserLastname,
                VehiceId = rentDto.VehiceId,
                VehicleInfo = rentDto.VehicleInfo,
                CarPools = carpools // appelle un repo car pool pour afficher les carpools
            };

            return rentWithCarPoolDTO;

        }
        /// <summary>
        /// Get a Rent By Id 
        /// </summary>
        /// <param name="rentID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
            if (vehicleId < 0) { throw new Exception("Id can't be negative"); }

            List<GetOneRentByVehicleIdDTO> rentList = await _rentRepository.GetRentByVehicleIdAsync(vehicleId);
            // renvois erreur si liste vide
            if (rentList == null) { throw new Exception($"No rents found for this vehicle {vehicleId}"); }

            return rentList;
        }
        /// <summary>
        /// Retrieves rents within a date fork asynchronously.
        /// </summary>
        /// <param name="dateForkDTO">The date fork DTO containing start and end dates.</param>
        /// <returns>A list of <see cref="GetOneRentDTO"/> objects representing the rents.</returns>
        /// <exception cref="Exception">Thrown when <paramref name="dateForkDTO"/> is null or contains invalid dates.</exception>
        public async Task<List<GetOneRentDTO>> GetRentsByDateForkAsync(DateForkDTO dateForkDTO)
        {
            if (dateForkDTO == null)
            {
                throw new Exception("Can't be null");
            }
            if (dateForkDTO.StartDate.Date.Year <= 0) { throw new Exception("Date can't be less than 0"); }
            if (dateForkDTO.EndDate.Date.Year <= 0) { throw new Exception("Date can't be less than 0"); }
            if (dateForkDTO.EndDate.Id <= 0) { dateForkDTO.EndDate.Id = 0; }
            if (dateForkDTO.StartDate.Id <= 0) { dateForkDTO.StartDate.Id = 0; }
            if (dateForkDTO.EndDate.Id != 0) { dateForkDTO.EndDate.Id = 0; }
            if (dateForkDTO.StartDate.Id != 0) { dateForkDTO.StartDate.Id = 0; }
            var rents = await this._rentRepository.GetRentsByDateForkAsync(dateForkDTO);
            if (rents.Count == 0)
            {
                throw new Exception("No rents Founds");
            }

            return rents;
        }

        /// <summary>
        /// Retrieves rents with associated car pools by user ID asynchronously.
        /// </summary>
        /// <param name="userID">The ID of the user.</param>
        /// <returns>
        /// A list of <see cref="GetOneRentWithCarPoolDTO"/> objects representing the rents with associated car pools.
        /// </returns>
        /// <exception cref="Exception">
        /// Thrown when no rents are found for the user or when a rent or car pool is not found.
        /// </exception>
        public async Task<List<GetOneRentWithCarPoolDTO>> GetRentsByUserAsync(string userID)
        {
            var rentIds = await this._rentRepository.GetRentsByUserAsync(userID);
            if (rentIds == null || rentIds.Count == 0)
            {
                throw new Exception("No rents found for user.");
            }
            List<GetOneRentWithCarPoolDTO> rentsWithCarPools = new List<GetOneRentWithCarPoolDTO>();

            foreach (int rentId in rentIds)
            {
                // Fetch rent details synchronously within the loop to avoid concurrency issues
                GetOneRentDTO? rentDto = await this._rentRepository.GetRentByIdAsync(rentId);
                if (rentDto == null)
                {
                    throw new Exception("Rent not found for rentId: " + rentId);
                }

                // Fetch carpool details for the current rent synchronously within the loop
                List<GetOneCarPoolWithPassengersDTO>? carPools = await this._carPoolRepository.GetCarPoolsByRentAsync(rentId);

                // Create and add the combined DTO to the list
                rentsWithCarPools.Add(new GetOneRentWithCarPoolDTO
                {
                    Id = rentDto.Id,
                    UserId = rentDto.UserId,
                    Immatriculation = rentDto.Immatriculation,
                    StartDate = rentDto.StartDate,
                    ReturnDate = rentDto.ReturnDate,
                    UserFirstname = rentDto.UserFirstname,
                    UserLastname = rentDto.UserLastname,
                    VehiceId = rentDto.VehiceId,
                    VehicleInfo = rentDto.VehicleInfo,
                    CarPools = carPools ?? new List<GetOneCarPoolWithPassengersDTO>()
                });
            }

            return rentsWithCarPools;
        }
        /// <summary>
        /// Will check here all potentials errors before updating
        /// </summary>
        /// <param name="updateRentRequestDTO"></param>
        /// <returns></returns>
        public async Task<GetOneRentDTO> UpdateRentByIdAsync(UpdateRentRequestDTO updateRentRequestDTO)
        {
            // Check sur la requete et cohérence de la requete
            if (updateRentRequestDTO == null) { throw new Exception("Request cannot be null"); }
            if (updateRentRequestDTO.Id == 0) { throw new Exception("Id cannot be 0"); }
            if (updateRentRequestDTO.Id < 0) { throw new Exception("Id cannot be < 0"); }
            if (updateRentRequestDTO.ReturnDate < updateRentRequestDTO.StartDate) { throw new Exception("New Return Date cannot be less than new Start Date"); }
            if (updateRentRequestDTO.ReturnDate == updateRentRequestDTO.StartDate) { throw new Exception("New dates cannot be the sames"); }
            if ((updateRentRequestDTO.ReturnDate - updateRentRequestDTO.StartDate) > TimeSpan.FromDays(60)) { throw new Exception("Rent duration cannot exceed 2 months!"); }
            if ((updateRentRequestDTO.ReturnDate - updateRentRequestDTO.StartDate) < TimeSpan.FromMinutes(30)) { throw new Exception("Your rent must be at least 30 minutes"); }
            if (updateRentRequestDTO.StartDate <= DateTime.Now) { throw new Exception("The new Start Date must be in the future"); }
            if (updateRentRequestDTO.ReturnDate <= DateTime.Now) { throw new Exception("The new Return Date must be in the future"); }


            // check si le rentEntity est valide / pas nul
            GetOneRentDTO? getRentOld = await this._rentRepository.GetRentByIdAsync(updateRentRequestDTO.Id);
            if (getRentOld == null)
            {
                throw new Exception("No Rentals found for this Id!");
            }
            // check si les dates saisis ne coïncident pas avec dates de locations déjà existantes, auquel cas pas de modifications possible
            if (await this._rentHelper.isVehicleRentedAsync(getRentOld.VehiceId, updateRentRequestDTO.StartDate, updateRentRequestDTO.ReturnDate))
            {
                throw new Exception("The car is rented at the selected dates");
            }

            // check si les dates saisies existent dans la BDD
            int? getStartDateId = await this._dateRepository.GetCloseDateAsync(updateRentRequestDTO.StartDate);
            int? getReturnDateId = await this._dateRepository.GetCloseDateAsync(updateRentRequestDTO.ReturnDate);

            DateTime beginDateResult = updateRentRequestDTO.StartDate; // creation de date pour etre reutilisé plus tard dans isRented
            DateTime endDateResult = updateRentRequestDTO.ReturnDate;

            // check if the dates exists in DB within one minutes , if not creates them
            if (getStartDateId == null || getReturnDateId == null)
            {
                GetOneDateDTO startDateDTO = await this._dateRepository.CreateAsync(updateRentRequestDTO.StartDate);
                GetOneDateDTO returnDateDTO = await this._dateRepository.CreateAsync(updateRentRequestDTO.ReturnDate);
                beginDateResult = startDateDTO.Date;
                endDateResult = returnDateDTO.Date;
                getStartDateId = startDateDTO.Id;
                getReturnDateId = returnDateDTO.Id;
            }

            // check si le vehicule est loué aux dates choisis, si les dates existe, utilise les existante, sinon prend les nouvelles.
            bool isRented = await _rentHelper.isVehicleRentedAsync(getRentOld.VehiceId, beginDateResult, endDateResult);



            if (isRented) // si loué, retourne vrai, donc exception !
            {
                throw new Exception("Le véhicule est loué aux dates choisis !");
            }
            // SI TOUT EST BON , ALORS ON UPDATE !
            // Mapping sur UpdateRentDTO
            UpdateRentDTO UpdateRentDTO = new UpdateRentDTO
            {
                Id = updateRentRequestDTO.Id,
                StartDateId = (int)getStartDateId,
                ReturnDateId = (int)getReturnDateId,
                StartDate = beginDateResult,
                ReturnDate = endDateResult,
            };
            await this._rentRepository.UpdateRentByIdAsync(UpdateRentDTO);

            return await this._rentRepository.GetRentByIdAsync(updateRentRequestDTO.Id);

        }

        public Task<List<GetOneRentDTO>> GetRentsByEndDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneRentDTO>> GetRentsByStartDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneRentDTO>> GetRentByVehicleIdAsync(int vehicleId) { throw new NotImplementedException(); }
    }
}
