using DTO.User;
using IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Security.Claims;
using Utils.Constants;

namespace Main.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<AppUser> _userManager;
        public AuthController(IAuthService authService, UserManager<AppUser> userManager)
        {
            this._authService = authService;
            this._userManager = userManager;
        }

        /// <summary>
        /// registration of a new user
        /// </summary>
        /// <param name="createUserDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Register(CreateUserDTO createUserDTO)
        {
            try
            {
                await this._authService.RegisterAsync(createUserDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<string>>> GetRole()
        {
            AppUser? appUser = await this._userManager.GetUserAsync(User);
            if (appUser == null)
            {
                return Unauthorized();
            }
            return Ok(await this._userManager.GetRolesAsync(appUser));
        }
    }
}
