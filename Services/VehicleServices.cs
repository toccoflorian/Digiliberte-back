using DTO.Dates;
using DTO.Vehicles;
using IRepositories;
using IServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositories;

namespace Services
{
    /// <summary>
    /// Class for Vehicles services used in Controllers dans repos
    /// </summary>
    public class VehicleServices : IVehicleService
    {
        // ----- Injection de dependances
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleServices(IVehicleRepository vehicleRepository)
        {
            this._vehicleRepository = vehicleRepository;
        }


        /// <summary>
        /// Create one vehicle 
        /// </summary>
        /// <param name="createOneVehicleDTO"></param>
        /// <returns>GetOneVehicle DTO </returns>
        public async Task<GetOneVehicleDTO> CreateOneVehicleAsync(CreateVehicleDTO createOneVehicleDTO)
        {
            return await this._vehicleRepository.CreateOneVehicleAsync(createOneVehicleDTO);
        }

        public async Task<GetOneVehicleDTO?> UpdateOneVehicleByIdAsync(int Id)
        {

            // Vérifiez d'abord si un categorie avec le même id existe déjà dans la base de données
            var existingVehicle = await _vehicleRepository.UpdateOneVehicleByIdAsync(Id);


            // Si aucun categorie avec le même id n'existe, vous pouvez procéder à la mise à jour
            return existingVehicle;
        }

       

    }
}
