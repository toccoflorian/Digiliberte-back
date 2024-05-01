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
        public async Task<ActionResult<GetOneCarPoolDTO>> CreateCarpool(CreateCarpoolRequestDTO createRequestCarPoolDTO)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CreateOneCarPoolDTO createOneCarPoolDTO = new CreateOneCarPoolDTO
            {
                CarpoolName = createRequestCarPoolDTO.CarpoolName,
                UserId = userId,
                StartDate = new GetOneDateDTO { Date = createRequestCarPoolDTO.StartDate },
                EndDate = new GetOneDateDTO { Date = createRequestCarPoolDTO.EndDate },
                StartLocalization = createRequestCarPoolDTO.StartLocalization,
                EndLocalization = createRequestCarPoolDTO.EndLocalization,
                RentId = createRequestCarPoolDTO.RentId,
            };
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
        [Authorize]
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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<GetOneCarPoolWithPassengersDTO>>> GetCarPoolByEndDateAsync(DateTime date, float? marge)
        {
            try
            {
                return Ok(await this._carPoolService.GetCarPoolByEndDateAsync(
                    new GetCarpoolByDateDTO
                    {
                        Date = date,
                        Marge = marge == (float)0 ? (float)0.1 : (marge ?? (float)0.1)
                    }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //public Task<List<GetOneCarPoolDTO>> GetCarPoolByPassengerAsync(int carPoolPassengerID)
        //{
        //    throw new NotImplementedException();
        //}

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<GetOneCarPoolWithPassengersDTO>>> GetCarPoolByPassengerAsync()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            try
            {
                return Ok(await this._carPoolService.GetCarPoolByPassengerAsync(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<GetOneCarPoolWithPassengersDTO>> GetCarPoolByRentAsync(int rentID)
        {
            try
            {
                return Ok(await this._carPoolService.GetCarPoolByRentAsync(rentID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<GetOneCarPoolWithPassengersDTO>>> GetCarPoolByStartDateAsync(DateTime date, float? marge)
        {
            try
            {
                return Ok(await this._carPoolService.GetCarPoolByStartDateAsync(
                    new GetCarpoolByDateDTO
                    {
                        Date = date,
                        Marge = marge == (float)0 ? (float)0.1 : (marge ?? (float)0.1)
                    }));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<GetOneCarPoolWithPassengersDTO>>> GetCarPoolsByDateForkAsync(DateTime beginFork, DateTime endFork)
        {
            try
            {
                return Ok(await this._carPoolService.GetCarPoolsByDateForkAsync(
                    new DateForkDTO
                    {
                        StartDate = new GetOneDateDTO { Id = 1, Date = beginFork },
                        EndDate = new GetOneDateDTO { Id = 1, Date = endFork }
                    }));
            }
            catch( Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //public Task<GetOneRentDTO> UpdateCarPoolByIdAsync(int rentID)
        //{
        //    throw new NotImplementedException();
        //} public    
    }
}
