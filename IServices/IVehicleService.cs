using DTO.Vehicles;

namespace IServices
{
    public interface IVehicleService
    {
        public Task<GetOneVehicleDTO?> CreateOneVehicleAsync(CreateVehicleDTO createVehicleDTO);
        public Task<GetOneVehicleDTO?> UpdateOneVehicleByIdAsync(int Id);
    }
}
