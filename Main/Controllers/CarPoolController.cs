using DTO.CarPools;
using DTO.Dates;
using DTO.Rent;
using IRepositories;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Security.Claims;

namespace Main.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarPoolController : ControllerBase
    {
        private readonly ICarPoolService _carPoolService;
        public CarPoolController(ICarPoolService carPoolService)
        {
            this._carPoolService = carPoolService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<GetOneCarPoolDTO>> CreateCarpool(CreateOneCarPoolDTO createOneCarPoolDTO)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            createOneCarPoolDTO.UserId = userId;
            try
            {
                return Ok(await this._carPoolService.CreateCarpoolAsync(createOneCarPoolDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<GetOneCarPoolWithPassengersDTO>> GetCarPoolByIdAsync(int carPoolID)
        {
            try
            {
                return Ok(await this._carPoolService.GetCarPoolByIdAsync(carPoolID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //public Task DeleteCarPoolByIdAsync(int rentID)
        //{
        //    throw new NotImplementedException();
        //}

        [HttpGet]
        //[Authorize]     // ADMIN
        public async Task<ActionResult<List<GetOneCarPoolDTO>>> GetAllCarPoolAsync()
        {
            try
            {
                return Ok(await this._carPoolService.GetAllCarPoolAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<GetOneCarPoolWithPassengersDTO>>> GetCarPoolByDriverIdAsync()
        {
            string? currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                return Ok(await this._carPoolService.GetCarPoolByDriverIdAsync(currentUser));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByEndDateAsync(DateTime date)
        //{
        //    throw new NotImplementedException();
        //}


        //public Task<List<GetOneCarPoolDTO>> GetCarPoolByPassengerAsync(int carPoolPassengerID)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByPassengerAsync(string userID)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<GetOneCarPoolWithPassengersDTO> GetCarPoolByRentAsync(int rentID)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolByStartDateAsync(DateTime date)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<GetOneCarPoolWithPassengersDTO>> GetCarPoolsByDateForkAsync(DateForkDTO dateForkDTO)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<GetOneRentDTO> UpdateCarPoolByIdAsync(int rentID)
        //{
        //    throw new NotImplementedException();
        //} public    
    }
}
