using DTO.CarPools;
using DTO.Dates;
using DTO.Rent;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Create a carPool
        /// </summary>
        /// <param name="createRequestCarPoolDTO"></param>
        /// <returns></returns>
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

        /// <summary>
        /// GET a carPool by Id
        /// </summary>
        /// <param name="carPoolID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// DELETE a carPoll by a carpool Id
        /// </summary>
        /// <param name="carpoolId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteCarPoolByIdAsync(int carpoolId)
        {
            try
            {
                await this._carPoolService.DeleteCarPoolByIdAsync(carpoolId);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// GET all carPool
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// GET a carPool by the driver id
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// GET a crPool by the carPool EndDate
        /// </summary>
        /// <param name="date"></param>
        /// <param name="marge"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get a carPoll by user Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<GetOneCarPoolWithPassengersDTO>>> GetCarPoolByUserAsync()
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

        /// <summary>
        /// Get a carPool by a rent id
        /// </summary>
        /// <param name="rentID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// GET a carPool by StartDate
        /// </summary>
        /// <param name="date"></param>
        /// <param name="marge"></param>
        /// <returns></returns>
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

        /// <summary>
        /// GET a carPool by Date
        /// </summary>
        /// <param name="beginFork"></param>
        /// <param name="endFork"></param>
        /// <returns></returns>
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

        /// <summary>
        /// UPDATE a carPool by carPoolDTO
        /// </summary>
        /// <param name="carpoolDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<GetOneRentDTO>> UpdateCarPoolByIdAsync(UpdateOneCarPoolDTO carpoolDTO)
        {
            try
            {
                return Ok(await this._carPoolService.UpdateCarPoolByIdAsync(carpoolDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
