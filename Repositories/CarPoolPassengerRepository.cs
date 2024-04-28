using DTO.CarPoolPassenger;
using DTO.User;
using IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Repositories
{
    public class CarPoolPassengerRepository : ICarPoolPassengerRepository
    {
        private readonly DatabaseContext _context;
        public CarPoolPassengerRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<int> CreateCarPoolPassengerAsync(CreateCarPoolPassengerDTO createCarPoolPassengerDTO)
        {
            EntityEntry<CarPoolPassenger> entityEntry = await this._context.CarPoolPassengers.AddAsync(
                new CarPoolPassenger
                {
                    UserID = createCarPoolPassengerDTO.UserId!,
                    CarPoolID = createCarPoolPassengerDTO.CarPoolId,
                    Description = createCarPoolPassengerDTO.Description
                });
            await this._context.SaveChangesAsync();
            return entityEntry.Entity.Id;
        }

        public Task DeleteCarPoolPassengerByIdAsync(int carPoolPassengerId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneCarPoolPassengerDTO>> GetAllPassengersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengerByDescriptionDateAsync(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengerByDescriptionLocalizationAsync(int localizationID)
        {
            throw new NotImplementedException();
        }

        public async Task<GetOneCarPoolPassengerDTO?> GetPassengerByIdAsync(int carPoolPassengerID)
        {
            return await this._context.CarPoolPassengers
                .Include(passenger => passenger.User)
                .Select( passenger => 
                    new GetOneCarPoolPassengerDTO
                    {
                        Id = passenger.Id,
                        CarPoolId = passenger.CarPoolID,
                        Description = passenger.Description,
                        UserDTO = new GetOneUserDTO
                        {
                            Id = passenger.UserID,
                            Firstname = passenger.User.Firstname,
                            Lastname = passenger.User.Lastname,
                            PictureURL = passenger.User.PictureURL
                        },
                    })
                .FirstOrDefaultAsync(passenger => passenger.Id == carPoolPassengerID);
        }

        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengersByCarPoolAsync(int carPoolID)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengersByUserAsync(string userID)
        {
            throw new NotImplementedException();
        }

        public Task<GetOneCarPoolPassengerDTO> UpdateCarPoolPassengerByIdAsync(int carPoolPassengerId)
        {
            throw new NotImplementedException();
        }
    }
}
