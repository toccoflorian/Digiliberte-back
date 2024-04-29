using DTO.Dates;
using DTO.Vehicles;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Main.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        public readonly IVehicleService _vehicleServices;
        public VehicleController(IVehicleService vehicleServices)
        {
            _vehicleServices = vehicleServices;
        }

        /// <summary>
        /// get all vehicle
        /// </summary>
        /// <returns>List of Vheicless formated with GetOneVhehicleDTO</returns>
        //[HttpGet]
        //public async Task<ActionResult<List<GetOneVehicleDTO>>> GetAllVehicles()         // get all users
        //{
        //    try
        //    {
        //        return Ok(await this._vehicleServices.GetAllVehiclesAsync());
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult<GetOneVehicleDTO?>> CreateOneVehicleAsync(CreateVehicleDTO createVehicleDTO)
        {
            try
            {
                return Ok(await _vehicleServices.CreateOneVehicleAsync(createVehicleDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Put a Vehicle into the db, use a DTO for update        
        /// </summary>
        /// <param name="updateOneVehicle">DTO of Vehicle for update</param>
        /// <returns>Returns a DTO of the updated vehicle</returns>
        [HttpPut]

        public async Task<ActionResult<GetOneVehicleDTO?>> UpdateOneVehicle(int Id)
        {
            try
            {
                return Ok(await _vehicleServices.UpdateOneVehicleByIdAsync(Id));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

    }
}
