using DTO.Motorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Main.Controllers
{
        [Route("api/[controller]/[action]")]
        [ApiController]
        public class MotorizationController : ControllerBase
        {
            public readonly MotorizationServices MotorizationServices;
            public MotorizationController(MotorizationServices vehicleServices)
            {
                MotorizationServices = vehicleServices;
            }


            [HttpPost]
            public async Task<ActionResult<GetOneMotorizationDTO?>> CreateMotorization(CreateOneMotorizationDTO createMotorizationDTO)
            {
                return Ok(await MotorizationServices.CreateMotorizationAsync(createMotorizationDTO));
            }
        }
    }

