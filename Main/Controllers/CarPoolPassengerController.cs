using DTO.CarPoolPassenger;
using IRepositories;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        //public Task DeleteCarPoolPassengerById(int carPoolPassengerId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<GetOneCarPoolPassengerDTO>> GetAllPassengers()
        //{
        //    throw new NotImplementedException();
        //}

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

        [HttpGet]
        public async Task<List<GetOneCarPoolPassengerDTO>> GetPassengersByCarPool(int carPoolID)
        {
            throw new NotImplementedException();
        }

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
