using DTO.Vehicles;
using Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enum;
using Repositories;

namespace Services
{
    /// <summary>
    /// Class for Vehicles services used in Controllers dans repos
    /// </summary>
    public class VehicleServices
    {
        // ----- Injection de dependances
        public readonly VehicleRepository vehicleRepository;

        public VehicleServices(VehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        /// <summary>
        /// Used to create a new vehicle , check if immats already exists, need a rework and more errors check
        /// </summary>
        /// <param name="createVehicleDTO"></param>
        /// <returns></returns>
        public async Task<GetOneVehicleDTO?> CreateVehicleAsync(CreateVehicleDTO createVehicleDTO)
        {
            if (await vehicleRepository.GetVehicleByImmat(createVehicleDTO.Immatriculation) == null)
            {
                return await vehicleRepository.CreateVehicleAsync(createVehicleDTO);
            }
            throw new Exception("Vehicle Immatriculation already exists");
        }
    }
}
