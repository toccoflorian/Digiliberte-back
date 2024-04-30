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
                UserId = createOneRentDTO.UserID,
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
                        UserId = rent.UserID,
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

        /// <summary>
        /// Asynchronously retrieves rental information by the specified rental ID.
        /// </summary>
        /// <param name="rentID">The ID of the rental to retrieve.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains a <see cref="GetOneRentDTO"/> object representing the rental information, or null if not found.
        /// </returns>
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

        public async Task<List<GetOneRentByVehicleIdDTO>> GetRentByVehicleIdAsync(int vehicleId)
        {
            
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
        /// Update Rend By Id using dto as entry to look for ID and edited values
        /// </summary>
        /// <param name="updateRentDTO"> Changes made on start date and return Date</param>
        /// <returns>Return GetOneRendDTO with the updated Rent</returns>
        public async Task<int?> UpdateRentByIdAsync(UpdateRentDTO updateRentDTO)
        {
            var updatingRent = await this._context.Rents.FindAsync(updateRentDTO.Id);

            updatingRent.ReturnDateID = updateRentDTO.ReturnDateId;
            updatingRent.StartDateID = updatingRent.StartDateID;
            updatingRent.StartDate = updatingRent.StartDate;
            updatingRent.ReturnDate = updatingRent.ReturnDate;

            _context.Update(updatingRent);

            return await _context.SaveChangesAsync(); ;
        }
    }
}
