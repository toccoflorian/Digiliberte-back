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

        //public Task DeleteUserById(int userID)
        //{
        //    throw new NotImplementedException();
        //}

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

        //public Task<Task<GetOneUserDTO>> GetUserById(string userID)
        //{
        //    throw new NotImplementedException();
        //}

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
