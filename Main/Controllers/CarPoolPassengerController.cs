using DTO.CarPoolPassenger;
using DTO.Pagination;
using IRepositories;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using System.Security.Claims;

namespace Main.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarPoolPassengerController : ControllerBase
    {
        private readonly ICarPoolPassengerService _carPoolPassengerService;
        public CarPoolPassengerController(ICarPoolPassengerService carPoolPassengerService)
        {
            this._carPoolPassengerService = carPoolPassengerService;
        }

        /// <summary>
        /// Create a Carpool passenger
        /// </summary>
        /// <param name="createCarPoolPassengerDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<GetOneCarPoolPassengerDTO>> CreateCarPoolPassenger(CreateCarPoolPassengerDTO createCarPoolPassengerDTO)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null)
            {
                return BadRequest("Identifiez-vous !");
            }
            createCarPoolPassengerDTO.UserId = userId;
            try
            {
                return Ok(await this._carPoolPassengerService.CreateCarPoolPassengerAsync(createCarPoolPassengerDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete a passenger of a carpool by the passenger Id
        /// </summary>
        /// <param name="deleteCarPoolPassengerDTO"></param>
        /// <returns></returns>
        [HttpDelete]
        //[Authorize]
        public async Task<ActionResult> DeleteCarPoolPassengerById(DeleteCarpoolPassengerDTO deleteCarPoolPassengerDTO)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string? userRole = User.FindFirstValue(ClaimTypes.Role);
            deleteCarPoolPassengerDTO.ConnectedUserId = userId;
            deleteCarPoolPassengerDTO.ConnectedUserRole = userRole;
            try
            {
                await this._carPoolPassengerService.DeleteCarPoolPassengerByIdAsync(deleteCarPoolPassengerDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Get All passengers of a carpool
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<GetOneCarPoolPassengerDTO>>> GetAllPassengers(int? pageIndex, int? pageSize)
        {
            try
            {
                return Ok(await this._carPoolPassengerService.GetAllPassengersAsync(new PageForkDTO
                {
                    PageIndex = pageIndex != null ? (int)pageIndex : 0,
                    PageSize = pageSize != null ? (int)pageSize : 10
                }));
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        //public Task<List<GetOneCarPoolPassengerDTO>> GetPassengerByDescriptionDate(DateTime dateTime)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<GetOneCarPoolPassengerDTO>> GetPassengerByDescriptionLocalization(int localizationID)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Get passengers of a carpool by a carpool passenger Id
        /// </summary>
        /// <param name="carPoolPassengerID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<GetOneCarPoolPassengerDTO>> GetPassengerById(int carPoolPassengerID, int? pageIndex, int? pageSize)
        {
            try
            {
                return Ok(await this._carPoolPassengerService.GetPassengerByIdAsync(carPoolPassengerID));

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get passengers by carPool id
        /// </summary>
        /// <param name="carPoolID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<GetOneCarPoolPassengerDTO>>> GetPassengersByCarPoolAsync(int carPoolID, int paginationIndex = 0, int pageSize = 10)
        {
            try
            {
                return Ok(await this._carPoolPassengerService.GetPassengersByCarPoolAsync(carPoolID, paginationIndex, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get passengers of a carpool by user Id (driver)
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<GetOneCarPoolPassengerDTO>>> GetPassengersByUserAsync(string userID)
        {
            try
            {
                return Ok(await this._carPoolPassengerService.GetPassengersByUserAsync(userID));
    }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
}
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<GetOneCarPoolPassengerDTO>> UpdateCarPoolPassengerById(UpdateCarPoolPassengerControllerDTO updateCarpoolDTO)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            string userRole = User.FindFirstValue(ClaimTypes.Role)!;
            try
            {
                return Ok(await this._carPoolPassengerService.UpdateCarPoolPassengerByIdAsync(new UpdateCarPoolPassengerDTO
                {
                    Id = updateCarpoolDTO.Id,
                    CarpoolId = updateCarpoolDTO.CarpoolId,
                    Description = updateCarpoolDTO.Description,
                    UserId = userId,
                    UserRole = userRole
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
