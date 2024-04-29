using DTO.Vehicles;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IVehicleRepository
    {
        public Task<GetOneVehicleDTO?> CreateOneVehicleAsync(CreateVehicleDTO createVehicleDTO);
        //public Task<GetOneVehicleDTO?> GetOneVehicleByIdAsync(int Id);
        public Task<GetOneVehicleDTO?> UpdateOneVehicleByIdAsync(int Id);
    }
}
