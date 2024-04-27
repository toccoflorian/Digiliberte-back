﻿using DTO.Dates;
using DTO.Vehicles;


namespace IRepositories
{
    public interface IVehicleRepository
    {
        public Task<GetOneVehicleDTO> CreateVehicleAsync(CreateVehicleDTO createVehicleDTO);
        public Task<UpdateOneVehicleDTO> UpdateVehicleByIdAsync(UpdateOneVehicleDTO updateOneVehicleDTO);
        public Task DeleteVehicleByIdAsync(int id);
        public Task<GetOneVehicleDTO?> GetVehicleByImmatAsync(string immat);
        public Task<List<GetOneVehicleDTO>> GetVehiclesByLocalizationAsync(int id);
        public Task<List<GetOneVehicleDTO>> GetVehiclesByUserIdAsync(string userID);
        public Task<List<GetOneVehicleDTO>> GetVehiclesByStateAsync(int stateId);
        public Task<List<GetOneVehicleDTO>> GetVehiclesByMotorizationAsync(int motorizationId);
        public Task<List<GetOneVehicleDTO>> GetVehiclesByCategoryAsync(int categoryId);
        public Task<List<GetOneVehicleDTO>> GetVehiclesByBrandAsync(int brandId);
        public Task<List<GetOneVehicleDTO>> GetVehiclesByModelAsync(int modelId);
        public Task<List<GetOneVehicleDTO>> GetAllUnreservedVehiclesAsync();
        public Task<List<GetOneVehicleWithRentDTO>> GetAllReservedVehiclesAsync();
        public Task<List<GetOneVehicleDTO>> GetReservedVehicleByDatesAsync(DateForkDTO dateForkDTO);
        public Task<List<GetOneVehicleDTO>> GetUnreservedVehicleByDatesAsync(DateForkDTO dateForkDTO);
        public Task<GetOneVehicleDTO> GetVehicleByIdAsync(int id);
        public Task<List<GetOneVehicleDTO>> GetAllVehiclesAsync();
        public Task<GetOneVehicleWithRentDTO> GetVehicleByIdWithRentAsync(int vehicleId);
    }
}