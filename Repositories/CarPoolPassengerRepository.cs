﻿using DTO.CarPoolPassenger;
using DTO.Pagination;
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

        public async Task<int> DeleteCarPoolPassengerByIdAsync(int carpoolPassengerId)
        {
            this._context.CarPoolPassengers.Remove((await this._context.CarPoolPassengers.FindAsync(carpoolPassengerId))!);
            return await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Get all passengers of a carpool
        /// </summary>
        /// <param name="pageForkDTO"></param>
        /// <returns></returns>
        public async Task<List<GetOneCarPoolPassengerDTO>> GetAllPassengersAsync(PageForkDTO pageForkDTO)
        {
            return await this._context.CarPoolPassengers
                .Skip(pageForkDTO.PageIndex * pageForkDTO.PageSize)
                .Take(pageForkDTO.PageSize)
                .Select(carpoolPassenger => 
                    new GetOneCarPoolPassengerDTO
                    {
                        Id = carpoolPassenger.Id,
                        CarPoolId = carpoolPassenger.CarPoolID,
                        Description = carpoolPassenger.Description,
                        UserDTO = 
                            new GetOneUserDTO 
                            {
                                Id = carpoolPassenger.UserID,
                                Firstname = carpoolPassenger.User.Firstname,
                                Lastname = carpoolPassenger.User.Lastname,
                                PictureURL = carpoolPassenger.User.PictureURL
                            }})
                .ToListAsync();
        }

        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengerByDescriptionDateAsync(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneCarPoolPassengerDTO>> GetPassengerByDescriptionLocalizationAsync(int localizationID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get passengers of a carpool by a carpool passenger Id
        /// </summary>
        /// <param name="carPoolPassengerID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get passengers of a carpool by Carpool Id
        /// </summary>
        /// <param name="carPoolID"></param>
        /// <returns></returns>
        public async Task<List<GetOneCarPoolPassengerDTO>> GetPassengersByCarPoolAsync(int carPoolID)
        {
            return await this._context.CarPoolPassengers
                .Where(passenger => passenger.CarPoolID == carPoolID)
                .Select(passenger =>
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
                .ToListAsync();
        }

        /// <summary>
        /// Get carpool passengers by user Id (driver) 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<List<GetOneCarPoolPassengerDTO>> GetPassengersByUserAsync(string userID)
        {
            return await this._context.CarPoolPassengers
                .Where(passenger => passenger.UserID == userID)
                .Select(passenger =>
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
                .ToListAsync();
        }


        public async Task<CarPoolPassenger?> GetCarpoolPassengerTypeAsync(int carpoolPassengerId)
        {
            return await this._context.CarPoolPassengers.FindAsync(carpoolPassengerId);
        }
    }
}
