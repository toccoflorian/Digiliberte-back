﻿using DTO.CarPools;
using DTO.Dates;
using DTO.Rent;

namespace IServices
{
    public interface ICarPoolService
    {
        public Task<GetOneCarPoolDTO> CreateCarpoolAsync(CreateOneCarPoolDTO createOneCarPoolDTO);
        public Task DeleteCarPoolByIdAsync(int rentID);
        public Task<List<GetOneCarPoolDTO>> GetCarPoolByPassengerAsync(int carPoolPassengerID);
        public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByEndDateAsync(DateTime date);
        public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByPassengerAsync(string userID);
        public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolsByDateForkAsync(DateForkDTO dateForkDTO);
        public Task<GetOneCarPoolWithPassengersDTO> GetCarPoolByRentAsync(int rentID);
        public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByStartDateAsync(DateTime date);
        public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByDriverIdAsync(string userId);
        public Task<List<GetOneCarPoolDTO>> GetAllCarPoolAsync();
        public Task<GetOneCarPoolWithPassengersDTO> GetCarPoolByIdAsync(int carPoolID);
        public Task<GetOneRentDTO> UpdateCarPoolByIdAsync(int rentID);
    }
}