using DTO.Vehicles;
using IRepositories;
using IServices;
using DTO.Dates;

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
            return await this.GetVehicleByIdAsync(id)
                ??
                throw new Exception("Aucun véhicule ne correspont à cette id !");
        }

        public Task<List<GetOneVehicleWithRentDTO>> GetAllReservedVehiclesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneVehicleDTO>> GetAllUnreservedVehiclesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneVehicleDTO>> GetAllVehiclesAsync()
        {
            throw new NotImplementedException();
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
            return await this._vehicleRepository.GetVehicleByImmatAsync(immat)
                ??
                throw new Exception("Acun véhicule ne porte cette immatriculation !");
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

        public Task<List<GetOneVehicleDTO>> GetVehiclesByStateAsync(int stateId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneVehicleDTO>> GetVehiclesByUserIdAsync(string userID)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateOneVehicleDTO> UpdateVehicleByIdAsync(UpdateOneVehicleDTO updateOneVehicleDTO)
        {
            throw new NotImplementedException();
        }
    }
}
