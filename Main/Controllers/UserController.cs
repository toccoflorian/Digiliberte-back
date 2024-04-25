using DTO.User;
using IRepositories;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

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
        public async Task<ActionResult> DeleteUserById(string userID)
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
        public async Task<ActionResult<GetOneUserDTO>> GetUserById(string userID)
        {
            return Ok(await this._userService.GetUserByIdAsync(userID));
        }

        //public Task<GetOneUserDTO> GetUserByRent(int rentID)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<GetOneUserDTO>> GetUserByRole(int rentID)
        //{
        //    throw new NotImplementedException();
        //}

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
