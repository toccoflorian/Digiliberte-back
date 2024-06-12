using DTO.Dates;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Helper;
using System.Security.Claims;

namespace Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly RentHelper _rentHelper;
        private readonly DatabaseContext _context;
        public TestController(RentHelper rentHelper, DatabaseContext databaseContext)
        {
            this._rentHelper = rentHelper;
            this._context = databaseContext;
        }
        /// <summary>
        /// Test
        /// </summary>
        /// <param name="id"></param>
        /// <param name="forkDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> isCarRented(int id, DateForkDTO forkDTO)
        {
            var test = await this._rentHelper.isVehicleRentedAsync(id,forkDTO.StartDate.Date,forkDTO.EndDate.Date);

            if (test)
            {
                return Ok("LE VEHICULE EST LOUE A CES DATES");
            }
            return Ok("LE VEHICULE EST LIBRE");
        }

        [HttpGet]
        public async Task<IActionResult> getUserRoles()
        {
            var user = HttpContext.User;
			var roles = user.FindAll(ClaimTypes.Role).Select(r => r.Value);
			return Ok(new { UserRoles = roles });
		}

    }
}
