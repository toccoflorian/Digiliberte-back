using DTO.CarPoolPassenger;
using DTO.CarPools;
using DTO.Dates;
using DTO.Localization;
using DTO.Rent;
using DTO.User;
using IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Repositories
{
    public class CarPoolRepository : ICarPoolRepository
    {
        private readonly DatabaseContext _context;
        public CarPoolRepository(DatabaseContext context)
        {
            _context = context;
        }


        public async Task<int> CreateCarpoolAsync(CreateOneCarPoolDTO createOneCarPoolDTO)
        {
            EntityEntry<CarPool> entityEntry = await this._context.CarPools.AddAsync(
                new CarPool
                {
                    RentId = createOneCarPoolDTO.RentId,
                    Name = createOneCarPoolDTO.CarpoolName,
                    StartDateID = (int)createOneCarPoolDTO.StartDateId!,
                    EndDateID = (int)createOneCarPoolDTO.EndDateId!,
                    StartLocalizationID = (int)createOneCarPoolDTO.StartLocalizationId!,
                    EndLocalizationID = (int)createOneCarPoolDTO.EndLocalizationId!,
                });
            await this._context.SaveChangesAsync();
            return entityEntry.Entity.Id;
        }

        public async Task<int> DeleteCarPoolByIdAsync(CarPool carpool)
        {
            this._context.CarPools.Remove(carpool);
            return await this._context.SaveChangesAsync();
        }
        /// <summary>
        /// Updates a carpool in the database asynchronously.
        /// </summary>
        /// <param name="carpool">The carpool object to update.</param>
        /// <returns>The number of state entries written to the database.</returns>

        public async Task<int> UpdateCarPoolByIdAsync(CarPool carpool)
        {
            this._context.Update(carpool);
            return await this._context.SaveChangesAsync();
        }
        /// <summary>
        /// Retrieves a carpool with passengers by its ID asynchronously.
        /// </summary>
        /// <param name="carPoolID">The ID of the carpool to retrieve.</param>
        /// <returns>A <see cref="GetOneCarPoolWithPassengersDTO"/> object representing the carpool with passengers, or null if not found.</returns>

        public async Task<GetOneCarPoolWithPassengersDTO?> GetCarPoolByIdAsync(int carPoolID)
        {
            CarPool? carpool =  await this._context.CarPools
                .FirstOrDefaultAsync(carpool => carpool.Id == carPoolID);
            return carpool == null ? null : new GetOneCarPoolWithPassengersDTO().MapAsync(carpool);
        }
        /// <summary>
        /// Retrieves a carpool's type by its ID asynchronously.
        /// </summary>
        /// <param name="carPoolID">The ID of the carpool to retrieve.</param>
        /// <returns>A <see cref="CarPool"/> object representing the carpool's type, or null if not found.</returns>

        public async Task<CarPool?> GetCarPoolTypeByIdAsync(int carPoolID)
        {
            return await this._context.CarPools
                .Include(carpool => carpool.Rent)
                .ThenInclude(rent=>rent.Vehicle)
                .ThenInclude(vehicle=>vehicle.Brand)
                .Include(carpool => carpool.Rent)
                .ThenInclude(rent => rent.Vehicle)
                .ThenInclude(vehicle => vehicle.Model)
                .Include(carpool => carpool.Rent)
                .ThenInclude(rent => rent.Vehicle)
                .ThenInclude(vehicle => vehicle.Motorization)
                .Include(carpool => carpool.Rent)
                .ThenInclude(rent => rent.Vehicle)
                .ThenInclude(vehicle => vehicle.Category)
                .Include(carpool => carpool.Rent)
                .ThenInclude(rent => rent.StartDate)
                .Include(carpool => carpool.Rent)
                .ThenInclude(rent => rent.ReturnDate)
                .Include(carpool => carpool.StartDate)
                .Include(carpool => carpool.EndDate)
                .Include(carpool => carpool.StartLocalization)
                .Include(carpool => carpool.EndLocalization)
                .Include(carpool => carpool.carPoolPassengers)!
                .ThenInclude(p=>p.User)
                .FirstOrDefaultAsync(carpool => carpool.Id == carPoolID);
        }
        /// <summary>
        /// Retrieves all car pools asynchronously.
        /// </summary>
        /// <returns>A list of <see cref="GetOneCarPoolDTO"/> objects representing the car pools.</returns>

        public async Task<List<GetOneCarPoolDTO>> GetAllCarPoolAsync()
        {
            return await this._context.CarPools
                .Select(carpool => new GetOneCarPoolDTO
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
                })
                .ToListAsync();
        }
        /// <summary>
        /// Retrieves car pools by driver ID asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the driver.</param>
        /// <returns>A list of <see cref="GetOneCarPoolWithPassengersDTO"/> objects representing car pools.</returns>

        public async Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByDriverIdAsync(string userId)
        {
            var carPoolsByDriverId = await this._context.CarPools
                .Where(carpoolUser => carpoolUser.Rent.UserID == userId)
                .Select(carpool =>
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
                }).ToListAsync();

            return carPoolsByDriverId;
        }
        /// <summary>
        /// Retrieves car pools by end date asynchronously, within a specified margin of time.
        /// </summary>
        /// <param name="dateDTO">The date and margin DTO.</param>
        /// <returns>A list of <see cref="GetOneCarPoolWithPassengersDTO"/> objects representing car pools.</returns>

        public async Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByEndDateAsync(GetCarpoolByDateDTO dateDTO)
        {
            return await this._context.CarPools
                .Where(carpool => 
                    dateDTO.Date > carpool.EndDate.Date.AddMinutes(- dateDTO.Marge * 60) 
                    && 
                    dateDTO.Date < carpool.EndDate.Date.AddMinutes(dateDTO.Marge * 60))
                .Select(carpool =>
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
                    })
                    .ToListAsync();
        }
        /// <summary>
        /// Retrieves car pools by passenger ID asynchronously.
        /// </summary>
        /// <param name="userID">The ID of the passenger.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="NotImplementedException">Thrown if the method is not implemented.</exception>

        public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByPassengerAsync(string userID)
        {
            throw new NotImplementedException();
        }

        public async Task<GetOneCarPoolWithPassengersDTO?> GetCarPoolByRentAsync(int rentID)
        {
            return await this._context.CarPools
                .Select(carpool =>
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
                    })
                .FirstOrDefaultAsync(carpool => carpool.RentId == rentID);
        }

        public async Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByStartDateAsync(GetCarpoolByDateDTO dateDTO)
        {
            return await this._context.CarPools
                .Where(carpool =>
                    dateDTO.Date > carpool.StartDate.Date.AddMinutes(-dateDTO.Marge * 60)
                    &&
                    dateDTO.Date < carpool.StartDate.Date.AddMinutes(dateDTO.Marge * 60))
                .Select(carpool =>
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
                    })
                    .ToListAsync();
        }

        public async Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolsByDateForkAsync(DateForkDTO dateForkDTO)
        {
            return await this._context.CarPools
                .Where(carpool =>
                    carpool.StartDate.Date >= dateForkDTO.StartDate.Date
                    &&
                    carpool.StartDate.Date <= dateForkDTO.EndDate.Date)
                .Select(carpool =>
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
                    })
                    .ToListAsync();
        }
        /// <summary>
        /// Return a list instead of a single CarPool
        /// </summary>
        /// <param name="rentID"></param>
        /// <returns></returns>
        public async Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolsByRentAsync(int rentID)
        {
            return await this._context.CarPools
                .Include(carpool => carpool.carPoolPassengers)  // Assure le chargement des passagers
                .ThenInclude(passenger => passenger.User)       // Et leurs utilisateurs associés si nécessaire
                .Where(carpool => carpool.RentId == rentID)
                .Select(carpool =>
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
                    })
                .ToListAsync();
        }

    }
}
