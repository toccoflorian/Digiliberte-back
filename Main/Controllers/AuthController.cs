using DTO.Auth;
using IRepositories;
using IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{
    [Route("api/[controller],[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            this._authRepository = authRepository;
        }

        /// <summary>
        /// registration of a new user
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Register(RegisterDTO registerDTO)
        {
            try
            {
                await this._authRepository.RegisterAsync(registerDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }   
    }
}
