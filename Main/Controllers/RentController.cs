using DTO.Dates;
using DTO.Rent;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;
using System.Security.Claims;
using Utils.Constants;

namespace Main.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RentController : ControllerBase
    {
        private readonly IRentService _rentService;
        private readonly DatabaseContext _context;
        public RentController(IRentService rentService, DatabaseContext databaseContext)
        {
            this._rentService = rentService;
            this._context = databaseContext;
        }

        /// <summary>
        /// Create a new Rent
        /// </summary>
        /// <param name="createOneRentDTO"></param>
        /// <returns>the created rent formated with GetOneRentDTO</returns>
        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<GetOneRentDTO>> CreateOneRent(CreateRentDTO createOneRentDTO)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return BadRequest("Identifiez vous !");
            }
            createOneRentDTO.UserID = userId;
            try
            {
                return Ok(await this._rentService.CreateOneRentAsync(createOneRentDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //public void DeleteRentById(int rentID)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Get all rents 
        /// </summary>
        /// <returns>List of Rent formated with GetOneRentDTO</returns>
        [HttpGet]
        //[Authorize(Roles = ROLE.ADMIN)]
        public async Task<ActionResult<List<GetOneRentDTO>>> GetAllRent(int pageSize = 10, int pageIndex = 0)
        {
            try
            {
                return Ok(await this._rentService.GetAllRentAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<GetOneRentWithCarPoolDTO>> GetRentByCarPoolId(int carPoolId)
        {
            try
            {
                return Ok(await this._rentService.GetRentByCarPoolAsync(carPoolId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<GetOneRentWithCarPoolDTO>> GetRentById(int rentID)
        {
            try
            {
                return Ok(await this._rentService.GetRentByIdAsync(rentID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //public List<GetOneRentDTO> GetRentByVehicleId(int vehicleId)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<GetOneRentDTO> GetRentsByDateFork(DateForkDTO dateForkDTO)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<GetOneRentDTO> GetRentsByEndDate(DateTime date)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<GetOneRentDTO> GetRentsByStartDate(DateTime date)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<GetOneRentWithCarPoolDTO> GetRentsByUser(string userID)
        //{
        //    throw new NotImplementedException();
        //}
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> UpdateRentById(UpdateRentRequestDTO updateRentRequest)
        {
            try
            {
                return Ok(await this._rentService.UpdateRentByIdAsync(updateRentRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Retrieves rents within a date fork asynchronously.
        /// </summary>
        /// <param name="dateFork">The date fork DTO containing start and end dates.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
        /// <response code="200">Returns the list of rents.</response>
        /// <response code="400">If the request is invalid or an error occurs during retrieval.</response>

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetRentByDateFork(DateForkDTO dateFork)
        {
            try
            {
                return Ok(await this._rentService.GetRentsByDateForkAsync(dateFork));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Retrieves rents with associated car pools for the authenticated user or a specified user asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user. If null, retrieves rents for the authenticated user.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> representing the result of the action.
        /// </returns>
        /// <response code="200">Returns the list of rents with associated car pools.</response>
        /// <response code="400">If the request is invalid or an error occurs during retrieval.</response>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetRentByUserId()
        {
            try
            {
                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? throw new Exception("Merci de vous connecter");
                return Ok(await this._rentService.GetRentsByUserAsync(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
