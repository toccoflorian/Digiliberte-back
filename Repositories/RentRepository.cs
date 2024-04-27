using DTO.Dates;
using DTO.Rent;
using DTO.Vehicles;
using IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;


namespace Repositories
{
    public class RentRepository : IRentRepository
    {
        private readonly DatabaseContext _context;
        public RentRepository(DatabaseContext context)
        {
            this._context = context;

        }


        /// <summary>
        /// Create a new Rent
        /// </summary>
        /// <param name="createOneRentDTO"></param>
        /// <returns>the created rent formated with GetOneRentDTO</returns>
        public async Task<GetOneRentDTO> CreateOneRentAsync(CreateRentDTO createOneRentDTO)
        {
            EntityEntry<Rent> entityEntry = await this._context.AddAsync(new Rent
            {
                UserID = createOneRentDTO.UserID,
                VehicleId = createOneRentDTO.VehiceId,
                StartDateID = (int)createOneRentDTO.StartDateId!,
                ReturnDateID = (int)createOneRentDTO.ReturnDateId!,
            });
            await this._context.SaveChangesAsync();
            return new GetOneRentDTO
            {
                Id = entityEntry.Entity.Id,
                VehiceId = createOneRentDTO.VehiceId,
                VehicleInfo = createOneRentDTO.VehicleInfos!,
                Immatriculation = createOneRentDTO.Immatriculation!,
                UserFirstname = createOneRentDTO.UserFirstname!,
                UserLastname = createOneRentDTO.UserLastname!,
                StartDate = createOneRentDTO.StartDate,
                ReturnDate = createOneRentDTO.ReturnDate,
            };
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
            return await this._context.Rents
                .Include(rent => rent.Vehicle)
                .ThenInclude(vehicle => vehicle.Category)
                .Include(rent => rent.Vehicle)
                .ThenInclude(vehicle => vehicle.Brand)
                .Include(rent => rent.Vehicle)
                .ThenInclude(vehicle => vehicle.Model)
                .Include(rent => rent.User)
                .Include(rent => rent.StartDate)
                .Include(rent => rent.ReturnDate)
                .Select(rent =>
                    new GetOneRentDTO
                    {
                        Id = rent.Id,
                        VehiceId = rent.VehicleId,
                        VehicleInfo = $"{rent.Vehicle.Category.Label}, {rent.Vehicle.Category.SeatsNumber} places, {rent.Vehicle.Brand.Label}, {rent.Vehicle.Model.Label}, {rent.Vehicle.Model.Year}",
                        Immatriculation = rent.Vehicle.Immatriculation,
                        UserFirstname = rent.User.Firstname,
                        UserLastname = rent.User.Lastname,
                        StartDate = rent.StartDate.Date,
                        ReturnDate = rent.ReturnDate.Date
                    })
                .ToListAsync();
        }

        public Task<GetOneRentDTO> GetRentByCarPoolAsync(int carPoolID)
        {
            throw new NotImplementedException();
        }

        public Task<GetOneRentDTO?> GetRentByIdAsync(int rentID)
        {
            return this._context.Rents
                .Include(rent => rent.User)
                .Include(rent => rent.Vehicle)
                .ThenInclude(vehicle => vehicle.Brand)
                .Include(rent => rent.Vehicle)
                .ThenInclude(vehicle => vehicle.Model)
                .Include(rent => rent.StartDate)
                .Include(rent => rent.ReturnDate)
                .Select(rent =>
                    new GetOneRentDTO
                    {
                        Id = rent.Id,
                        UserId = rent.User.Id,
                        UserFirstname = rent.User.Firstname,
                        UserLastname = rent.User.Lastname,
                        StartDate = rent.StartDate.Date,
                        ReturnDate = rent.ReturnDate.Date,
                        VehiceId = rent.VehicleId,
                        VehicleInfo = $"{rent.Vehicle.Brand.Label} {rent.Vehicle.Model.Label} {rent.Vehicle.Model.Year}",
                        Immatriculation = rent.Vehicle.Immatriculation
                    })
                .FirstOrDefaultAsync(rentDTO => rentDTO.Id == rentID);
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
