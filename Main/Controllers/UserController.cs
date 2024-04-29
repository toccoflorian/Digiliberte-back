using DTO.User;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        /// <summary>
        /// Get the user in origin of the carpool
        /// </summary>
        /// <param name="carPoolID"></param>
        /// <returns>one user formated with GetOneUserDTO</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<GetOneUserDTO>>> GetUserByCarPool(int carPoolID)
        {
            try
            {   
                return Ok(await this._userService.GetUserByCarPoolAsync(carPoolID));
            }
            catch (Exception ex)    
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get one User with User.Id
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>one user formated with GetOneUserDTO</returns>
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<GetOneUserDTO>> GetUserById(string userID)           // get user by Id
        {
            try
            {
                return Ok(await this._userService.GetUserByIdAsync(userID));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Get the user of one rent
        /// </summary>
        /// <param name="rentId"></param>
        /// <returns>one user formated with GetOneUserDTO</returns>
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<GetOneUserDTO>> GetUserByRent(int rentId)
        {
            try
            {
                return Ok(await this._userService.GetUserByRentAsync(rentId));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get list of user in the role
        /// </summary>
        /// <param name="role"></param>
        /// <returns>List of user formated with GetOneUserDTO</returns>
        [HttpGet]
        //[Authorize(Roles = ROLE.ADMIN)]
        public async Task<ActionResult<List<GetOneUserDTO>>> GetUserByRole(string role)         // get user by role
        {
            try
            {
                return Ok(await this._userService.GetUserByRoleAsync(role));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            }

        /// <summary>
        /// get a list of users by name
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <returns>List of users formated with GetUserByNameDTO</returns>
        [HttpGet]
        //[Authorize(Roles = ROLE.ADMIN)]
        public async Task<ActionResult<List<GetOneUserDTO>>> GetUsersByName(string? firstname, string? lastname)     // get users by name
        {
            try
            {
                return await this._userService.GetUsersByNameAsync(new GetUserByNameDTO
                {
                    Firstname = firstname,
                    Lastname = lastname
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<GetOneUserDTO>> UpdateUserById(UpdateUserDTO updateOneUserDTO)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            updateOneUserDTO.UserId = userId;
            try
            {
                return await this._userService.UpdateUserByIdAsync(updateOneUserDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
