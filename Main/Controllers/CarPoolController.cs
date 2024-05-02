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
        /// Creates a new carpool asynchronously.
        /// </summary>
        /// <param name="createRequestCarPoolDTO">The request DTO containing information about the carpool to create.</param>
        /// <returns>An action result containing a <see cref="GetOneCarPoolDTO"/> object representing the created carpool.</returns>
        /// <response code="200">Returns the newly created carpool.</response>
        /// <response code="400">If the request is invalid or an error occurs during creation.</response>

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
        /// Retrieves a carpool by its ID asynchronously.
        /// </summary>
        /// <param name="carPoolID">The ID of the carpool to retrieve.</param>
        /// <returns>An action result containing a <see cref="GetOneCarPoolWithPassengersDTO"/> object representing the carpool.</returns>
        /// <response code="200">Returns the requested carpool.</response>
        /// <response code="400">If the request is invalid or an error occurs during retrieval.</response>

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
        /// Deletes a carpool by its ID asynchronously.
        /// </summary>
        /// <param name="carpoolId">The ID of the carpool to delete.</param>
        /// <returns>An action result.</returns>
        /// <response code="200">If the carpool is successfully deleted.</response>
        /// <response code="400">If the request is invalid or an error occurs during deletion.</response>

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
        /// Retrieves all car pools asynchronously.
        /// </summary>
        /// <returns>An action result containing a list of <see cref="GetOneCarPoolDTO"/> objects representing the car pools.</returns>
        /// <response code="200">Returns the list of car pools.</response>
        /// <response code="400">If the request is invalid or an error occurs during retrieval.</response>

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
        /// Retrieves car pools by the driver's ID asynchronously.
        /// </summary>
        /// <returns>An action result containing a list of <see cref="GetOneCarPoolWithPassengersDTO"/> objects representing the car pools.</returns>
        /// <response code="200">Returns the list of car pools.</response>
        /// <response code="400">If the request is invalid or an error occurs during retrieval.</response>

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
