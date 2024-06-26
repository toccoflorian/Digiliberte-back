﻿using DTO.Dates;
using DTO.Vehicles;

namespace IServices
{
    public interface IVehicleService
    {
        public Task<GetOneVehicleDTO?> CreateVehicleAsync(CreateVehicleDTO createVehicleDTO);
        public Task<GetOneVehicleDTO> UpdateVehicleByIdAsync(UpdateOneVehicleDTO updateOneVehicleDTO);
        public Task<GetOneVehicleDTO?> GetVehicleByImmatAsync(string immat);
        public Task<List<GetOneVehicleDTO>> GetVehiclesByLocalizationAsync(int id);
        public Task<List<GetOneVehicleDTO>> GetVehiclesByUserIdAsync(string userID);
        public Task<List<GetOneVehicleDTO>> GetVehiclesByStateAsync(int stateId, int paginationIndex = 0, int pageSize = 10);
        public Task<List<GetOneVehicleDTO>> GetVehiclesByMotorizationAsync(int motorizationId, int paginationIndex = 0, int pageSize = 10);
        public Task<List<GetOneVehicleDTO>> GetVehiclesByCategoryAsync(int categoryId, int paginationIndex = 0, int pageSize = 10);
        public Task<List<GetOneVehicleDTO>> GetVehiclesByBrandAsync(int brandId, int paginationIndex = 0, int pageSize = 10);
        public Task<List<GetOneVehicleDTO>> GetVehiclesByModelAsync(int modelId, int paginationIndex = 0, int pageSize = 10);
        public Task<List<GetOneVehicleDTO>> GetAllUnreservedVehiclesAsync();
        public Task<List<GetOneVehicleWithRentDTO>> GetAllReservedVehiclesAsync();
        public Task<List<GetOneVehicleDTO>> GetReservedVehicleByDatesAsync(DateForkDTO dateForkDTO);
        public Task<List<GetOneVehicleDTO>> GetUnreservedVehicleByDatesAsync(DateForkDTO dateForkDTO);
        public Task<GetOneVehicleDTO> GetVehicleByIdAsync(int id);
        public Task<List<GetOneVehicleDTO>> GetAllVehiclesAsync(int paginationIndex = 0, int pageSize = 10);
        public Task<List<GetOneVehicleWithRentDTO>> GetVehicleByIdWithRentAsync(int vehicleId);
    }
}
