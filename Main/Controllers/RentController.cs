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
        [Authorize]
        public async Task<ActionResult<GetOneRentDTO>> CreateOneRent(CreateRentDTO createOneRentDTO)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null)
            {
                return BadRequest("Identifier vous !");
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
        public async Task<ActionResult<List<GetOneRentDTO>>> GetAllRent()
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

        //public GetOneRentDTO GetRentByCarPool(int carPoolID)
        //{
        //    throw new NotImplementedException();
        //}

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<GetOneRentWithCarPoolDTO>> GetRentById(int rentID)
        {
            try
            {
                return Ok(await this._rentService.GetRentByIdAsync(rentID));
            }
            catch(Exception ex)
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
        public async Task<IActionResult> UpdateRentById(UpdateRentRequestDTO updateRentRequest)
        {
            throw new NotImplementedException();
        }
    }
}
