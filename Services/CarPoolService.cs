using DTO.CarPoolPassenger;
using DTO.CarPools;
using DTO.Dates;
using DTO.Localization;
using DTO.Rent;
using DTO.User;
using IRepositories;
using IServices;
using Models;

namespace Services
{
    public class CarPoolService : ICarPoolService
    {
        private readonly ICarPoolRepository _carPoolRepository;
        private readonly ICarPoolPassengerRepository _carPoolPassengerRepository;
        private readonly IRentRepository _rentRepository;
        private readonly ILocalizationRepository _localizationRepository;
        private readonly IDateRepository _dateRepository;
        private readonly IUserRepository _userRepository;
        public CarPoolService(
            ICarPoolRepository carPoolRepository,
            ICarPoolPassengerRepository carPoolPassengerRepository,
            IRentRepository rentRepository, 
            ILocalizationRepository localizationRepository, 
            IDateRepository dateRepository,
            IUserRepository userRepository)
        {
            this._carPoolRepository = carPoolRepository;
            this._carPoolPassengerRepository = carPoolPassengerRepository;
            this._rentRepository = rentRepository;
            this._localizationRepository = localizationRepository;
            this._dateRepository = dateRepository;
            this._userRepository = userRepository;
        }


        public async Task<GetOneCarPoolDTO> CreateCarpoolAsync(CreateOneCarPoolDTO createOneCarPoolDTO)
        {
            // vérification de la nullabilité des localisations
            if(createOneCarPoolDTO.StartLocalization.Logitude == 0 || createOneCarPoolDTO.StartLocalization.Latitude == 0)
            {
                throw new Exception("Merci de rensigner la localisation de départ du covoiturage !");
            }
            if (createOneCarPoolDTO.EndLocalization.Logitude == 0 || createOneCarPoolDTO.EndLocalization.Latitude == 0)
            {
                throw new Exception("Merci de rensigner la localisation d'arrivé du covoiturage !");
            }

            // recuperation et vérification de la location associée au covoiturage
            if(createOneCarPoolDTO.RentId == 0)
            {
                throw new Exception("Merci de renseigner une location existante !");
            }
            GetOneRentDTO? rentDTO = await this._rentRepository.GetRentByIdAsync(createOneCarPoolDTO.RentId);
            if(rentDTO == null)
            {
                throw new Exception("Aucune location ne correspond à cette id !");
            }
            
            // vérification que la location associée au covoiturage demandé appartient à l'utilisateur connecté 
            if(createOneCarPoolDTO.UserId == null || createOneCarPoolDTO.UserId != rentDTO.UserId)
            {
                throw new Exception("Vous n'êtes pas le propriétaire de la location :");
            }

            // vérification de la validité des dates
            if (createOneCarPoolDTO.StartDate.Date <= DateTime.Now)
            {
                throw new Exception("La date de début de covoiturage dois être dans le futur !");
            }
            if (createOneCarPoolDTO.StartDate.Date >= createOneCarPoolDTO.EndDate.Date)
            {
                throw new Exception("La date de fin de covoiturage dois être superieur à la date de début !");
            }
            if(createOneCarPoolDTO.StartDate.Date < rentDTO.StartDate.Date || createOneCarPoolDTO.StartDate.Date > rentDTO.ReturnDate.Date)
            {
                throw new Exception("La date de début du covoiturage dois se situer entre la date de début et de fin de la location associée !");
            }
            if (createOneCarPoolDTO.EndDate.Date < rentDTO.StartDate.Date || createOneCarPoolDTO.EndDate.Date > rentDTO.ReturnDate.Date)
            {
                throw new Exception("La date de fin du covoiturage dois se situer entre la date de début et de fin de la location associée !");
            }

            // création des localizations et recuperation de leurs id
            createOneCarPoolDTO.StartLocalizationId = (await this._localizationRepository.CreateOneLocalizationAsync(createOneCarPoolDTO.StartLocalization)).Id;
            createOneCarPoolDTO.EndLocalizationId = (await this._localizationRepository.CreateOneLocalizationAsync(createOneCarPoolDTO.EndLocalization)).Id;
            if(createOneCarPoolDTO.StartLocalizationId == null || createOneCarPoolDTO.EndLocalizationId == null)
            {
                throw new Exception("Erreur lors de l'enrgistrement des localisations !");
            }

            // création des dates et récuperation de leurs id
            createOneCarPoolDTO.StartDateId = (await this._dateRepository.CreateAsync(createOneCarPoolDTO.StartDate.Date)).Id;
            createOneCarPoolDTO.EndDateId = (await this._dateRepository.CreateAsync(createOneCarPoolDTO.EndDate.Date)).Id;

            // création du covoiturage
            int carpoolId = await this._carPoolRepository.CreateCarpoolAsync(createOneCarPoolDTO);

            CarPool carpool = await this._carPoolRepository.GetCarPoolByIdAsync(carpoolId);
            return new GetOneCarPoolWithPassengersDTO
            {
                CarPoolId = carpool.Id,
                RentId = carpool.RentId,
                UserId = carpool.Rent.UserID,
                VehicleId = carpool.Rent.VehicleId,
                VehicleBrand = carpool.Rent.Vehicle.Brand.Label,
                VehicleModel = carpool.Rent.Vehicle.Model.Label,
                VehicleMotorization = carpool.Rent.Vehicle.Motorization.Label,
                VehicleImmatriculation = carpool.Rent.Vehicle.Immatriculation,
                SeatsTotalNumber = carpool.Rent.Vehicle.Category.SeatsNumber,
                FreeSeats = carpool.Rent.Vehicle.Category.SeatsNumber - (carpool.carPoolPassengers != null ? carpool.carPoolPassengers.Count() : 0),
                CO2 = carpool.Rent.Vehicle.Model.CO2,
                StartDate = carpool.StartDate.Date,
                EndDate = carpool.EndDate.Date,
                StartLocalization = new LocalizationDTO
                {
                    Latitude = carpool.StartLocalization.Latitude,
                    Logitude = carpool.StartLocalization.Longitude,
                },
                EndLocalization = new LocalizationDTO
                {
                    Latitude = carpool.EndLocalization.Latitude,
                    Logitude = carpool.EndLocalization.Longitude,
                },
                Passengers = carpool.carPoolPassengers
                            .Select(passenger =>
                                new GetOneCarPoolPassengerDTO
                                {
                                    Id = passenger.Id,
                                    CarPoolId = carpool.Id,
                                    Description = passenger.Description,
                                    UserDTO = new GetOneUserDTO
                                    {
                                        Id = passenger.UserID,
                                        Firstname = passenger.User.Firstname,
                                        Lastname = passenger.User.Lastname,
                                        PictureURL = passenger.User.PictureURL
                                    }
                                })
                            .ToList()
            };
        }

        public async Task DeleteCarPoolByIdAsync(int carpoolId)
        {
            CarPool? carpool = await this._carPoolRepository.GetCarPoolByIdAsync(carpoolId);
            if (carpool == null)
            {
                throw new Exception("Aucun convoiturage ne porte cette id !");
            }
            int result = await this._carPoolRepository.DeleteCarPoolByIdAsync(carpool);
            if(result == 0)
            {
                throw new Exception("Une erreur s'est produite lors de la suppression, merci de contacter le support !");
            }
        }

        public async Task<GetOneCarPoolWithPassengersDTO> GetCarPoolByIdAsync(int carPoolID)
        {
            CarPool? carpool = await this._carPoolRepository.GetCarPoolByIdAsync(carPoolID);
            if(carpool == null)
            {
                throw new Exception("Aucun covoiturage ne porte cette id !");
            }
            return new GetOneCarPoolWithPassengersDTO
            {
                CarPoolId = carpool.Id,
                RentId = carpool.RentId,
                UserId = carpool.Rent.UserID,
                VehicleId = carpool.Rent.VehicleId,
                VehicleBrand = carpool.Rent.Vehicle.Brand.Label,
                VehicleModel = carpool.Rent.Vehicle.Model.Label,
                VehicleMotorization = carpool.Rent.Vehicle.Motorization.Label,
                VehicleImmatriculation = carpool.Rent.Vehicle.Immatriculation,
                SeatsTotalNumber = carpool.Rent.Vehicle.Category.SeatsNumber,
                FreeSeats = carpool.Rent.Vehicle.Category.SeatsNumber - (carpool.carPoolPassengers != null ? carpool.carPoolPassengers.Count() : 0),
                CO2 = carpool.Rent.Vehicle.Model.CO2,
                StartDate = carpool.StartDate.Date,
                EndDate = carpool.EndDate.Date,
                StartLocalization = new LocalizationDTO
                {
                    Latitude = carpool.StartLocalization.Latitude,
                    Logitude = carpool.StartLocalization.Longitude,
                },
                EndLocalization = new LocalizationDTO
                {
                    Latitude = carpool.EndLocalization.Latitude,
                    Logitude = carpool.EndLocalization.Longitude,
                },
                Passengers = carpool.carPoolPassengers
                            .Select(passenger =>
                                new GetOneCarPoolPassengerDTO
                                {
                                    Id = passenger.Id,
                                    CarPoolId = carpool.Id,
                                    Description = passenger.Description,
                                    UserDTO = new GetOneUserDTO
                                    {
                                        Id = passenger.UserID,
                                        Firstname = passenger.User.Firstname,
                                        Lastname = passenger.User.Lastname,
                                        PictureURL = passenger.User.PictureURL
                                    }
                                })
                            .ToList()
            };
        }

        public async Task<List<GetOneCarPoolDTO>> GetAllCarPoolAsync()
        {
            return await this._carPoolRepository.GetAllCarPoolAsync();
        }

        public async Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByDriverIdAsync(string userId)
        {
            if ((await _userRepository.GetUserByIdAsync(userId)) == null)
            {
                throw new Exception($"Could not find {userId}");
            }
            var carPools = await _carPoolRepository.GetCarPoolByDriverIdAsync(userId);
            if( carPools == null || carPools.Count() == 0)
            {
                throw new Exception("Could not find any CarPools for this user");
            }

            return carPools;
        }

        public async Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByEndDateAsync(GetCarpoolByDateDTO dateDTO)
        {
            return await this._carPoolRepository.GetCarPoolByEndDateAsync(dateDTO);
        }

        public async Task<List<GetOneCarPoolDTO>> GetCarPoolByPassengerAsync(string userId)
        {
            List<GetOneCarPoolPassengerDTO>? passengers =  await this._carPoolPassengerRepository.GetPassengersByUserAsync(userId);
            List<GetOneCarPoolDTO> carpools = new List<GetOneCarPoolDTO>();
            foreach (GetOneCarPoolPassengerDTO passenger in passengers)
            {
                CarPool carpool = await this._carPoolRepository.GetCarPoolByIdAsync(passenger.CarPoolId);
                carpools.Add(
                    new GetOneCarPoolWithPassengersDTO
                    {
                        CarPoolId = carpool.Id,
                        RentId = carpool.RentId,
                        UserId = carpool.Rent.UserID,
                        VehicleId = carpool.Rent.VehicleId,
                        VehicleBrand = carpool.Rent.Vehicle.Brand.Label,
                        VehicleModel = carpool.Rent.Vehicle.Model.Label,
                        VehicleMotorization = carpool.Rent.Vehicle.Motorization.Label,
                        VehicleImmatriculation = carpool.Rent.Vehicle.Immatriculation,
                        SeatsTotalNumber = carpool.Rent.Vehicle.Category.SeatsNumber,
                        FreeSeats = carpool.Rent.Vehicle.Category.SeatsNumber - (carpool.carPoolPassengers != null ? carpool.carPoolPassengers.Count() : 0),
                        CO2 = carpool.Rent.Vehicle.Model.CO2,
                        StartDate = carpool.StartDate.Date,
                        EndDate = carpool.EndDate.Date,
                        StartLocalization = new LocalizationDTO
                        {
                            Latitude = carpool.StartLocalization.Latitude,
                            Logitude = carpool.StartLocalization.Longitude,
                        },
                        EndLocalization = new LocalizationDTO
                        {
                            Latitude = carpool.EndLocalization.Latitude,
                            Logitude = carpool.EndLocalization.Longitude,
                        },
                        Passengers = carpool.carPoolPassengers
                            .Select(passenger =>
                                new GetOneCarPoolPassengerDTO
                                {
                                    Id = passenger.Id,
                                    CarPoolId = carpool.Id,
                                    Description = passenger.Description,
                                    UserDTO = new GetOneUserDTO
                                    {
                                        Id = passenger.UserID,
                                        Firstname = passenger.User.Firstname,
                                        Lastname = passenger.User.Lastname,
                                        PictureURL = passenger.User.PictureURL
                                    }
                                })
                            .ToList()
                    });
            }
            return carpools;
        }

        public async Task<GetOneCarPoolWithPassengersDTO> GetCarPoolByRentAsync(int rentID)
        {
            GetOneCarPoolWithPassengersDTO? carpool = await this._carPoolRepository.GetCarPoolByRentAsync(rentID);
            if(carpool == null)
            {
                throw new Exception("Aucun covoiturage ne correspond à cette location !");
            }
            return carpool;
        }

        public async Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByStartDateAsync(GetCarpoolByDateDTO dateDTO)
        {
            return await this._carPoolRepository.GetCarPoolByStartDateAsync(dateDTO);
        }

        public async Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolsByDateForkAsync(DateForkDTO dateForkDTO)
        {
            return await this._carPoolRepository.GetCarPoolsByDateForkAsync (dateForkDTO);
        }

        public Task<GetOneRentDTO> UpdateCarPoolByIdAsync(int rentID)
        {
            throw new NotImplementedException();
        }
    }
}
