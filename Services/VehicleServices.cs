using DTO.Vehicles;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Class for Vehicles services used in Controllers dans repos
    /// </summary>
    public class VehicleServices
    {
        public async Task CreateVehicle(CreateVehicleDTO createVehicleDTO)
        {
            Vehicle newVehicle = new Vehicle
            {
                BrandID = createVehicleDTO.BrandId,
                CategoryID = createVehicleDTO.CategoryId,
                ModelID = createVehicleDTO.ModelId,
                MotorizationID = createVehicleDTO.MotorizationId,
                ColorId = createVehicleDTO.

            }
        }
    }
}
