using DTO.User;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utils.Constants;


namespace Main.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        /// <summary>
        /// Delete one User with referenced AppUser
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>void</returns>
        [HttpDelete]
        //[Authorize]
        public async Task<ActionResult> DeleteUserById(string userID)           // delete user
        {
            try
            {
                await this._userService.DeleteUserByIdAsync(userID);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// get all users registered
        /// </summary>
        /// <returns>List of users formated with GetOneUserDTO</returns>
        [HttpGet]
        //[Authorize(Roles = ROLE.ADMIN)]
        public async Task<ActionResult<List<GetOneUserDTO>>> GetAllUsers()         // get all users
        {
            try
            {
                return Ok(await this._userService.GetAllUsersAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //public Task<List<GetOneUserDTO>> GetUserByCarPool(int carPoolID)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Get one User with User.Id
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>one user formated with GetOneUserDTO</returns>
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<GetOneUserDTO>> GetUserById(string userID)           // get user by Id
        {
            return Ok(await this._userService.GetUserByIdAsync(userID));
        }

        //public Task<GetOneUserDTO> GetUserByRent(int rentID)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Get list of user in the role
        /// </summary>
        /// <param name="role"></param>
        /// <returns>List of user formated with GetOneUserDTO</returns>
        [HttpGet]
        //[Authorize(Roles = ROLE.ADMIN)]
        public async Task<ActionResult<List<GetOneUserDTO>>> GetUserByRole(string role)         // get user by role
        {
            return Ok(await this._userService.GetUserByRoleAsync(role));
        }

        //public Task<List<GetOneUserDTO>> GetUsersByName(GetUserByNameDTO getUserByNameDTO)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<GetOneUserDTO> UpdateUserById(CreateUserDTO updateOneUserDTO)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
