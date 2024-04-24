using DTO.Vehicles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Main.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        public readonly VehicleServices VehicleServices;
        public VehicleController(VehicleServices vehicleServices)
        {
            VehicleServices = vehicleServices;
        }


        [HttpPost]
        public async Task<ActionResult<GetOneVehicleDTO?>> CreateVehicle(CreateVehicleDTO createVehicleDTO)
        {
            return Ok(await VehicleServices.CreateVehicleAsync(createVehicleDTO));
        }
    }
}
