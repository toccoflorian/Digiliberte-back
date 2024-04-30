using DTO.Vehicles;
using IRepositories;
using IServices;
using DTO.Dates;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Services
{
    /// <summary>
    /// Class for Vehicles services used in Controllers dans repos
    /// </summary>
    public class VehicleService : IVehicleService
    {
        // ----- Injection de dependances
        public readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            this._vehicleRepository = vehicleRepository;
        }

        /// <summary>
        /// Used to create a new vehicle , check if immats already exists, need a rework and more errors check
        /// </summary>
        /// <param name="createVehicleDTO"></param>
        /// <returns></returns>
        public async Task<GetOneVehicleDTO> CreateVehicleAsync(CreateVehicleDTO createVehicleDTO)
        {
            if (await this._vehicleRepository.GetVehicleByImmatAsync(createVehicleDTO.Immatriculation) != null)
            {   
                // si l'immatriculation existe
                throw new Exception("Cette immatriculation existe déjà !");
            }
            return await this._vehicleRepository.CreateVehicleAsync(createVehicleDTO);
        }

        public Task DeleteVehicleByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a vehicle by id
        /// </summary>
        /// <param name="id">string</param>
        /// <returns> null or one Vehicle formated with GetOneVehicleDTO</returns>
        public async Task<GetOneVehicleDTO> GetVehicleByIdAsync(int id)
        {
            GetOneVehicleDTO? vehicleDTO = await this._vehicleRepository.GetVehicleByIdAsync(id);
            if (vehicleDTO == null)
            {
                throw new Exception("Aucun véhicule ne correspont à cette id !");
            }
            return vehicleDTO;
        }

        public Task<List<GetOneVehicleWithRentDTO>> GetAllReservedVehiclesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneVehicleDTO>> GetAllUnreservedVehiclesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetOneVehicleDTO>> GetAllVehiclesAsync(int paginationIndex = 0, int pageSize = 10)
        {
            try
            {
                var vehicles = await _vehicleRepository.GetAllVehiclesAsync(paginationIndex, pageSize);
                return vehicles;
            }
            catch (Exception ex)
            {
                // Gérer les exceptions appropriées
                throw new Exception("Failed to retrieve vehicles", ex);
            }
        }

        public Task<List<GetOneVehicleDTO>> GetReservedVehicleByDatesAsync(DateForkDTO dateForkDTO)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneVehicleDTO>> GetUnreservedVehicleByDatesAsync(DateForkDTO dateForkDTO)
        {
            throw new NotImplementedException();
        }

        public Task<GetOneVehicleWithRentDTO> GetVehicleByIdWithRentAsync(int vehicleId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a vehicle by immat , used to know if immat exists already
        /// </summary>
        /// <param name="immat">string</param>
        /// <returns> null or one Vehicle formated with GetOneVehicleDTO</returns>
        public async Task<GetOneVehicleDTO> GetVehicleByImmatAsync(string immat)
        {
            GetOneVehicleDTO? vehicleDTO = await this._vehicleRepository.GetVehicleByImmatAsync(immat);
            if(vehicleDTO == null)
            {
                throw new Exception("Acun véhicule ne porte cette immatriculation !");
            }
            return vehicleDTO;
        }

        public Task<List<GetOneVehicleDTO>> GetVehiclesByBrandAsync(int brandId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneVehicleDTO>> GetVehiclesByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneVehicleDTO>> GetVehiclesByLocalizationAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneVehicleDTO>> GetVehiclesByModelAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneVehicleDTO>> GetVehiclesByMotorizationAsync(int motorizationId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetOneVehicleDTO>> GetVehiclesByStateAsync(int stateId, int paginationIndex = 0, int pageSize = 10)
        {
            try
            {
                var vehicles = await _vehicleRepository.GetVehiclesByStateAsync(stateId, paginationIndex, pageSize);
                return vehicles;
            }
            catch (Exception ex)
            {
                // Gérer les exceptions appropriées
                throw new Exception("Failed to retrieve vehicles", ex);
            }
        }
    

        public Task<List<GetOneVehicleDTO>> GetVehiclesByUserIdAsync(string userID)
        {
            throw new NotImplementedException();
        }

        public async Task<GetOneVehicleDTO> UpdateVehicleByIdAsync(UpdateOneVehicleDTO updateOneVehicleDTO)
        {
            // Vérifiez d'abord si un véhicule avec le même ID existe déjà dans la base de données
            GetOneVehicleDTO? existingVehicle = await _vehicleRepository.GetVehicleByIdAsync(updateOneVehicleDTO.VehicleId);

            // Si aucun véhicule avec le même ID n'existe, retournez null ou lancez une exception selon vos besoins
            if (existingVehicle == null)
            {
                throw new Exception("Vehicle with provided ID not found");
            }
            await this._vehicleRepository.UpdateVehicleByIdAsync(updateOneVehicleDTO);
            return (await this._vehicleRepository.GetVehicleByIdAsync(updateOneVehicleDTO.VehicleId))!;
        }
    }
}
