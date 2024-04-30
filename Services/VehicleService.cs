using DTO.Vehicles;
using IRepositories;
using IServices;
using DTO.Dates;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Models;

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
        /// <exception cref="Exception"></exception>
        public async Task<GetOneVehicleDTO> CreateVehicleAsync(CreateVehicleDTO createVehicleDTO)
        {
            if (await this._vehicleRepository.GetVehicleByImmatAsync(createVehicleDTO.Immatriculation) != null)
            {   
                // si l'immatriculation existe
                throw new Exception("Vehicle already exist");
            }
            return await this._vehicleRepository.CreateVehicleAsync(createVehicleDTO);
        }


        /// <summary>
        /// Get a vehicle by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<GetOneVehicleDTO> GetVehicleByIdAsync(int id)
        {
            GetOneVehicleDTO? vehicleDTO = await this._vehicleRepository.GetVehicleByIdAsync(id);
            if (vehicleDTO == null)
            {
                throw new Exception("Vehicle with provided Id not found");
            }
            if(id < 0)
            {
                throw new Exception("A correct id must be provided");
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

        /// <summary>
        /// Get All vehicles
        /// </summary>
        /// <param name="paginationIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<GetOneVehicleDTO>> GetAllVehiclesAsync(int paginationIndex = 0, int pageSize = 10)
        {
            var vehicles = await _vehicleRepository.GetAllVehiclesAsync(paginationIndex, pageSize);
            if(vehicles==null)
            {
                throw new Exception("Not vehicle found");
            }
            if(paginationIndex <0)
            {
                throw new Exception("Pagination error");
            }
            if(pageSize <0)
            {
                throw new Exception("Page size error");
            }
                return vehicles;
        }

        public Task<List<GetOneVehicleDTO>> GetReservedVehicleByDatesAsync(DateForkDTO dateForkDTO)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneVehicleDTO>> GetUnreservedVehicleByDatesAsync(DateForkDTO dateForkDTO)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a vehicle by Id with rents , used to know if vehicle exists already
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<GetOneVehicleWithRentDTO>> GetVehicleByIdWithRentAsync(int vehicleId)
        {
            var vehicleDTO = await this._vehicleRepository.GetVehicleByIdWithRentAsync(vehicleId);
            if (vehicleDTO == null)
            {
                throw new Exception("Vehicle with id provided not found");
            }
            if (vehicleId == null)
            {
                throw new Exception("A vehicle Id must be provided");
            }
            if (vehicleId < 0)
            {
                throw new Exception("A correct id for the vehicle must be provided");
            }
            return vehicleDTO;
        }

        /// <summary>
        /// Get a vehicle by immat , used to know if immat exists already
        /// </summary>
        /// <param name="immat"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<GetOneVehicleDTO> GetVehicleByImmatAsync(string immat)
        {
            GetOneVehicleDTO? vehicleDTO = await this._vehicleRepository.GetVehicleByImmatAsync(immat);
            if(vehicleDTO == null)
            {
                throw new Exception("Vehicle with provided immatriculation not found");
            }
            if(immat == null)
            {
                throw new Exception("A immatriculation for the vehicle must be provided");
            }
            if(immat.Length == 0)
            {
                throw new Exception("A immatriculation for the vehicle must be provided");
            }
            return vehicleDTO;
        }

        /// <summary>
        /// Get a vehicle by brand , used to know if brand exists already
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="paginationIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<GetOneVehicleDTO>> GetVehiclesByBrandAsync(int brandId, int paginationIndex = 0, int pageSize = 10)
        {
             var vehicles = await _vehicleRepository.GetVehiclesByBrandAsync(brandId, paginationIndex, pageSize);
            if(vehicles == null && brandId < 0)
            {
                throw new Exception("Vehicle with provided brandId not found");
            }
            if(brandId <0)
            {
                throw new Exception("A correct brandID must be provided");
            }
            if (paginationIndex < 0) {
                throw new Exception("Pagination error");
            }
            if(pageSize < 0)
            {
                throw new Exception("Page size error");
            }
                return vehicles;
        }

        /// <summary>
        /// Get a vehicle by category , used to know if category exists already
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="paginationIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<GetOneVehicleDTO>> GetVehiclesByCategoryAsync(int categoryId, int paginationIndex = 0, int pageSize = 10)
        {
            List<GetOneVehicleDTO>? vehicles = await _vehicleRepository.GetVehiclesByCategoryAsync(categoryId, paginationIndex, pageSize);
            if(vehicles == null)
            {
                // Gérer les exceptions appropriées
                throw new Exception("Vehicle with provided categoryId not found");
            }
            if(categoryId < 0)
            {
                throw new Exception("A correct categoryID must be provided");
            }
            if(paginationIndex < 0)
            {
                throw new Exception("PaginationIndex Error");
            }
            if (pageSize < 0)
            {
                throw new Exception("PageSize Error");
            }
                return vehicles;
        }

        public Task<List<GetOneVehicleDTO>> GetVehiclesByLocalizationAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a vehicle by model , used modelId to know if model exists already
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="paginationIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<GetOneVehicleDTO>> GetVehiclesByModelAsync(int modelId, int paginationIndex = 0, int pageSize = 10)
        {
            var vehicles = await _vehicleRepository.GetVehiclesByModelAsync(modelId, paginationIndex, pageSize);
            if(vehicles == null)
            {
                throw new Exception("No Vehicle found");
            }
            if(modelId < 0)
            {
                throw new Exception("A correct modelId must be provided");
            }
            if (paginationIndex < 0) {
                throw new Exception("Problem with the pagination");
            }
            if(pageSize < 0)
            {
                throw new Exception("Page size probleme");
            }
                return vehicles;
        }

        /// <summary>
        /// Get a vehicle by motorization , used to know if motorization exists already
        /// </summary>
        /// <param name="motorizationId"></param>
        /// <param name="paginationIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<GetOneVehicleDTO>> GetVehiclesByMotorizationAsync(int motorizationId, int paginationIndex = 0, int pageSize = 10)
        {
            var vehicles = await _vehicleRepository.GetVehiclesByMotorizationAsync(motorizationId, paginationIndex, pageSize);
            if (vehicles == null)
            {
                throw new Exception("No Vehicle found");
            }
            if (motorizationId < 0)
            {
                throw new Exception("A correct motorizationID must be provided");
            }
            if (paginationIndex < 0)
            {
                throw new Exception("Problem with the pagination");
            }
            if (pageSize < 0)
            {
                throw new Exception("Page size probleme");
            }
            return vehicles;
        }

        /// <summary>
        /// Get a vehicle by state , used to know if state exists already
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="paginationIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<GetOneVehicleDTO>> GetVehiclesByStateAsync(int stateId, int paginationIndex = 0, int pageSize = 10)
        {
              var  vehicles = await _vehicleRepository.GetVehiclesByStateAsync(stateId, paginationIndex, pageSize);
            if (vehicles == null)
            {
                throw new Exception("No Vehicle found");
            }
            if (stateId < 0)
            {
                throw new Exception("Vehicle with provided modelId not found");
            }
            if (paginationIndex < 0)
            {
                throw new Exception("Problem with the pagination");
            }
            if (pageSize < 0)
            {
                throw new Exception("Page size probleme");
            }
            return vehicles;
        }
    

        public Task<List<GetOneVehicleDTO>> GetVehiclesByUserIdAsync(string userID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update a vehicle by state , used updateOneVehicleDTO to know if state exists already
        /// </summary>
        /// <param name="updateOneVehicleDTO"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<GetOneVehicleDTO> UpdateVehicleByIdAsync(UpdateOneVehicleDTO updateOneVehicleDTO)
        {
            // Vérifiez d'abord si un véhicule avec le même ID existe déjà dans la base de données
            GetOneVehicleDTO? existingVehicle = await _vehicleRepository.GetVehicleByIdAsync(updateOneVehicleDTO.VehicleId);

            // Si aucun véhicule avec le même ID n'existe, retournez null ou lancez une exception selon vos besoins
            if (existingVehicle == null )
            {
                throw new Exception("Vehicle with provided ID not found");
            }
            await this._vehicleRepository.UpdateVehicleByIdAsync(updateOneVehicleDTO);
            return (await this._vehicleRepository.GetVehicleByIdAsync(updateOneVehicleDTO.VehicleId))!;
        }
    }
}
