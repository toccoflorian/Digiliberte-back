using DTO.Dates;
using DTO.Vehicles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IServices;
using Models;
using Repositories;
using Microsoft.AspNetCore.Authorization;
using Utils.Constants;

namespace Main.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        public readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        /// <summary>
        ///  a new vehicle and return this formated with GetOneVehicleDTO
        /// </summary>
        /// <param name="createVehicleDTO"></param>
        /// <returns>one Vehicle formated with GetOneVehicleDTO</returns>
        [HttpPost]
        //[Authorize(Roles = ROLE.ADMIN)]
        public async Task<ActionResult<GetOneVehicleDTO>> CreateVehicle(CreateVehicleDTO createVehicleDTO)
        {
            try
            {
                return Ok(await this._vehicleService.CreateVehicleAsync(createVehicleDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpDelete]
        //public async Task<ActionResult> DeleteVehicleById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<UpdateOneVehicleDTO> UpdateVehicleById(UpdateOneVehicleDTO updateOneVehicleDTO)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Get a vehicle by id
        /// </summary>
        /// <param name="id">string</param>
        /// <returns> null or one Vehicle formated with GetOneVehicleDTO</returns>
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<GetOneVehicleDTO>> GetVehicleById(int id)
        {
            try
            {
                return Ok(await this._vehicleService.GetVehicleByIdAsync(id));
        }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
    }
}

        //public Task<List<GetOneVehicleDTO>> GetAllVehicles()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<GetOneVehicleWithRentDTO>> GetAllReservedVehicles()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<GetOneVehicleDTO>> GetAllUnreservedVehicles()
        //{
        //    throw new NotImplementedException();
        //}


        //public Task<List<GetOneVehicleDTO>> GetReservedVehicleByDates(DateForkDTO dateForkDTO)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<GetOneVehicleDTO>> GetUnreservedVehicleByDates(DateForkDTO dateForkDTO)
        //{
        //    throw new NotImplementedException();
        //}


        //public Task<GetOneVehicleWithRentDTO> GetVehicleByIdWithRent(int vehicleId)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Get a vehicle by immat , used to know if immat exists already
        /// </summary>
        /// <param name="GetOneVehicleDTO">string</param>
        /// <returns> null or one Vehicle formated with GetOneVehicleDTO</returns>
        [HttpGet]
        public async Task<ActionResult<GetOneVehicleDTO?>> GetVehicleByImmat(string immat)
        {
            try
            {
                return await this._vehicleService.GetVehicleByImmatAsync(immat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //public Task<List<GetOneVehicleDTO>> GetVehiclesByBrand(int brandId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<GetOneVehicleDTO>> GetVehiclesByCategory(int categoryId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<GetOneVehicleDTO>> GetVehiclesByLocalization(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<GetOneVehicleDTO>> GetVehiclesByModel(int modelId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<GetOneVehicleDTO>> GetVehiclesByMotorization(int motorizationId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<GetOneVehicleDTO>> GetVehiclesByState(int stateId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<GetOneVehicleDTO>> GetVehiclesByUserId(string userID)
        //{
        //    throw new NotImplementedException();
        //}


    }
}
