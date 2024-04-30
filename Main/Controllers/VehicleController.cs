using DTO.Dates;
using DTO.Vehicles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IServices;
using Models;
using Repositories;
using Microsoft.AspNetCore.Authorization;
using Utils.Constants;
using Services;

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
                return BadRequest(ex.Message);

                }
        }

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


        //public Task<GetOneVehicleWithRentDTO> GetVehicleByIdWithRent(int vehicleId)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Put a Vehicule into the db, use a DTO for update        
        /// </summary>
        /// <param name="updateOneVehicle">DTO of Motorization for update</param>
        /// <returns>Returns a DTO of the updated vehicle</returns>
        [HttpPut]

        public async Task<ActionResult<GetOneVehicleDTO?>> UpdateVehicleByIdAsync(UpdateOneVehicleDTO UpdateOneVehicleDTO)
        {
            try
            {
                return Ok(await _vehicleService.UpdateVehicleByIdAsync(UpdateOneVehicleDTO));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="paginationIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<GetOneVehicleDTO>>> GetVehiclesByBrandAsync(int brandId, int paginationIndex = 0, int pageSize = 10)
        {
            try
            {
                return await this._vehicleService.GetVehiclesByBrandAsync(brandId, paginationIndex, pageSize);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="paginationIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<GetOneVehicleDTO>>> GetVehiclesByCategoryAsync(int categoryId, int paginationIndex = 0, int pageSize = 10)
        {
            try
            {
                return await this._vehicleService.GetVehiclesByCategoryAsync(categoryId, paginationIndex, pageSize);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //public Task<List<GetOneVehicleDTO>> GetVehiclesByLocalization(int id)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="paginationIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<GetOneVehicleDTO>>> GetVehiclesByModel(int modelId, int paginationIndex = 0, int pageSize = 10)
        {
            try
            {
                return await this._vehicleService.GetVehiclesByModelAsync(modelId, paginationIndex, pageSize);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="motorizationId"></param>
        /// <param name="paginationIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<GetOneVehicleDTO>>> GetVehiclesByMotorization(int motorizationId, int paginationIndex = 0, int pageSize = 10)
        {
            try
            {
                return await this._vehicleService.GetVehiclesByMotorizationAsync( motorizationId, paginationIndex, pageSize);
        }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="paginationIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<GetOneVehicleDTO>>> GetVehiclesByState(int stateId, int paginationIndex = 0, int pageSize = 10)
        {
            try
            {
                return await this._vehicleService.GetVehiclesByStateAsync(stateId, paginationIndex, pageSize);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //public Task<List<GetOneVehicleDTO>> GetVehiclesByUserId(string userID)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Get all vehicles
        /// </summary>
        /// <returns>List of vehicle DTOs</returns>
        [HttpGet("all")]
        public async Task<ActionResult<List<GetOneVehicleDTO>>> GetAllVehiclesAsync(int paginationIndex = 0, int pageSize = 10)
        {
            try
            {
                var vehicles = await _vehicleService.GetAllVehiclesAsync(paginationIndex, pageSize);
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
