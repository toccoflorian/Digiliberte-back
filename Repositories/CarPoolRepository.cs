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
        public Task DeleteCarPoolByIdAsync(int rentID)
        {
            throw new NotImplementedException();
        }
        public Task<GetOneRentDTO> UpdateCarPoolByIdAsync(int rentID)
        {
            throw new NotImplementedException();
        }
        public async Task<GetOneCarPoolWithPassengersDTO?> GetCarPoolByIdAsync(int carPoolID)
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
                .FirstOrDefaultAsync(carpool => carpool.CarPoolId == carPoolID);
        }

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

        public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByDriverIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByEndDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }


        public Task<List<GetOneCarPoolDTO>> GetCarPoolByPassengerAsync(int carPoolPassengerID)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByPassengerAsync(string userID)
        {
            throw new NotImplementedException();
        }

        public Task<GetOneCarPoolWithPassengersDTO> GetCarPoolByRentAsync(int rentID)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByStartDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolsByDateForkAsync(DateForkDTO dateForkDTO)
        {
            throw new NotImplementedException();
        }

    }
}
