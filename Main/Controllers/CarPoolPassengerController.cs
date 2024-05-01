using DTO.CarPoolPassenger;
using DTO.Pagination;
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
    public class CarPoolPassengerController : ControllerBase
    {
        private readonly ICarPoolPassengerService _carPoolPassengerService;
        public CarPoolPassengerController(ICarPoolPassengerService carPoolPassengerService)
        {
            this._carPoolPassengerService = carPoolPassengerService;
        }

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
        /// Get All passengers
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

        [HttpGet]
        public async Task<ActionResult<GetOneCarPoolPassengerDTO>> GetPassengerById(int carPoolPassengerID)
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

        //[HttpGet]
        //public async Task<List<GetOneCarPoolPassengerDTO>> GetPassengersByCarPoolAsync(int carPoolID)
        //{
        //    try
        //    {
        //        return Ok(await this._carPoolPassengerService.GetPassengerByCarpoolAsync(carPoolID));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //public Task<List<GetOneCarPoolPassengerDTO>> GetPassengersByUser(string userID)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<GetOneCarPoolPassengerDTO> UpdateCarPoolPassengerById(int carPoolPassengerId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
