﻿using DTO.Dates;
using DTO.Rent;

namespace IServices
{
    public interface IRentService
    {
        public Task<GetOneRentDTO> CreateOneRentAsync(CreateRentDTO createOneRentDTO);
        public Task<GetOneRentDTO> UpdateRentByIdAsync(int rentID);
        public Task DeleteRentByIdAsync(int rentID);
        public Task<List<GetOneRentDTO>> GetAllRentAsync();
        public Task<GetOneRentDTO> GetRentByCarPoolAsync(int carPoolID);
        public Task<List<GetOneRentWithCarPoolDTO>> GetRentsByUserAsync(string userID);
        public Task<List<GetOneRentDTO>> GetRentsByDateForkAsync(DateForkDTO dateForkDTO);
        public Task<List<GetOneRentDTO>> GetRentsByEndDateAsync(DateTime date);
        public Task<List<GetOneRentDTO>> GetRentsByStartDateAsync(DateTime date);
        public Task<GetOneRentDTO> GetRentByIdAsync(int rentID);
        public Task<List<GetOneRentDTO>> GetRentByVehicleIdAsync(int vehicleId);
    }
}
