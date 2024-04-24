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


        public async Task CreateVehicle(CreateVehicleDTO createVehicleDTO)
        {
            if(vehicleRepository.GetVehicleByImmat(createVehicleDTO.Immatriculation) == null) 
            { 
                return;
            }
            vehicleRepository.CreateVehicleAsync(createVehicleDTO);
        }
    }
}
