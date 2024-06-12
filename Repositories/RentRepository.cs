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
        /// <param name="pageIndex">Page index to display default is 10</param>
        /// <returns>List of Rent formated with GetOneRentDTO</returns>
        public async Task<List<GetOneRentDTO>> GetAllRentAsync(int pageSize = 10, int pageIndex = 0)
        {
            return await this._context.Rents.Skip(pageSize*pageIndex).Take(pageSize)
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
        /// <summary>
        /// Return a car pool Id for a specified carPool
        /// </summary>
        /// <param name="carPoolID">carPoolId</param>
        /// <returns></returns>
        public async Task<int> GetRentIdByCarPoolAsync(int carPoolID)
        {
            CarPool? rentByCarPool = await this._context.CarPools
                .FirstOrDefaultAsync(carpool => carpool.Id == carPoolID);
            if (rentByCarPool == null)
            {
                return 0;
            }
            return rentByCarPool.RentId;
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
        }   // TODO : CHANGER LE RETOUR EN GETRENTWITHCARPOOL

        /// <summary>
        /// Asynchronously retrieves rental information for a vehicle based on the specified vehicle ID.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle for which rental information is to be retrieved.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains a list of <see cref="GetOneRentByVehicleIdDTO"/> objects representing the rental information for the specified vehicle.
        /// </returns>
        public async Task<List<GetOneRentByVehicleIdDTO>> GetRentByVehicleIdAsync(int vehicleId)
        {
            List<GetOneRentByVehicleIdDTO>? getRentsByVehicle = await this._context.Rents
                .Where(v => v.VehicleId == vehicleId)
                .Include(rent => rent.StartDate)
                .ThenInclude(startDate => startDate.Date)
                .Include (rent => rent.ReturnDate)
                .ThenInclude(returnDate => returnDate.Date)
                .Select(rent => 
                new GetOneRentByVehicleIdDTO
                {
                    Id = rent.Id,
                    UserId = rent.UserID,
                    VehiceId = rent.Vehicle.Id,
                    StartDate = rent.StartDate.Date,
                    ReturnDate= rent.ReturnDate.Date,
                }).ToListAsync();
            return getRentsByVehicle;
        }
        /// <summary>
        /// Update Rend By Id using dto as entry to look for ID and edited values
        /// </summary>
        /// <param name="updateRentDTO"> Changes made on start date and return Date</param>
        /// <returns>Return GetOneRendDTO with the updated Rent</returns>
        public async Task<int?> UpdateRentByIdAsync(UpdateRentDTO updateRentDTO)
        {
            Rent? updatingRent = await this._context.Rents.FindAsync(updateRentDTO.Id);

            updatingRent!.ReturnDateID = updateRentDTO.ReturnDateId;
            updatingRent.StartDateID = updateRentDTO.StartDateId;
            

            this._context.Rents.Update(updatingRent);

            return await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Get a rent by a Return date Id, might be more optimized then doing it by DateTime 
        /// </summary>
        /// <param name="id">the id of the searched date</param>
        /// <returns> List of GetOneRentDTO</returns>
        public async Task<List<GetOneRentDTO>> GetRentsByEndDateIdAsync(int id)
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
                .Where(rent => rent.ReturnDateID == id)
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
        /// <summary>
        /// Get a rent by a start date Id, might be more optimized then doing it by DateTime 
        /// </summary>
        /// <param name="id">the id of the searched date</param>
        /// <returns> List of GetOneRentDTO</returns>
        public async Task<List<GetOneRentDTO>> GetRentsByStartDateIdAsync(int id)
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
                .Where(rent=> rent.StartDateID == id)
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
        /// <summary>
        /// Asynchronously retrieves a list of rental transactions filtered by a specified date range.
        /// This method is optimized for read-only scenarios to enhance performance.
        /// </summary>
        /// <param name="dateForkDTO">An object containing the start and end dates used to define the date range for filtering the rentals.</param>
        /// <returns>A list of <see cref="GetOneRentDTO"/> objects that represent the rental transactions within the given date range. Each object contains detailed information about the rental, including identifiers and related user and vehicle details.</returns>
        /// <remarks>
        /// This method utilizes Entity Framework's AsNoTracking to improve query performance as the result set is only read. The rentals are filtered to include those where the start date is on or after the specified start date and the return date is on or before the specified end date.
        /// Each returned rental includes details such as the rental identifier, start and return dates, user ID, vehicle ID, user's firstname, user's lastname, and a formatted string containing the vehicle's brand, model, and year.
        /// </remarks>
        public async Task<List<GetOneRentDTO>> GetRentsByDateForkAsync(DateForkDTO dateForkDTO)
        {
            // Assume GetOneRentDTO contains all relevant fields you need
            var rentsList = await this._context.Rents
                .AsNoTracking() // Improve performance for read-only queries
                .Where(rent =>
                    rent.StartDate.Date >= dateForkDTO.StartDate.Date &&
                    rent.ReturnDate.Date <= dateForkDTO.EndDate.Date)
                .Select(rent => new GetOneRentDTO
                {
                    Id = rent.Id,
                    StartDate = rent.StartDate.Date, // Assuming StartDate is a complex type with a Date property
                    ReturnDate = rent.ReturnDate.Date, // Assuming ReturnDate is a complex type with a Date property
                    UserId = rent.UserID,
                    VehiceId = rent.VehicleId,
                    UserFirstname = rent.User.Firstname, // Assuming User details are needed
                    UserLastname = rent.User.Lastname,
                    VehicleInfo = $"{rent.Vehicle.Brand.Label} {rent.Vehicle.Model.Label} {rent.Vehicle.Model.Year}" // Assuming Vehicle details are 
                })
                .ToListAsync();

            return rentsList;
        }

        public Task<List<GetOneRentDTO>> GetRentsByEndDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        } // TODO : A FAIRE PLUS TARD

        public Task<List<GetOneRentDTO>> GetRentsByStartDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        } // TODO : A FAIRE PLUS TARD

        public async Task<List<int>?> GetRentsByUserAsync(string userID)
        {
            List<int>? myRents = await this._context.Rents
                .Where(rent => rent.UserID == userID)
                .Select(rent => rent.Id)
                .ToListAsync();

            return myRents;
        }

        public async Task<GetOneRentWithCarPoolDTO> GetRentByIdWithCarpoolAsync(int rentId)
        {
            throw new Exception();
        }

    }
}
