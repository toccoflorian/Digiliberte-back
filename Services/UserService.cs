using DTO.User;
using IRepositories;
using IServices;

namespace Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository )
        {
            this._userRepository = userRepository;
        }

        /// <summary>
        /// Delete one User with referenced AppUser
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>void</returns>
        public async Task DeleteUserByIdAsync(string userId)                // delete user
        {
            await this._userRepository.DeleteUserByIdAsync(userId);
        }

        /// <summary>
        /// get all users registered
        /// </summary>
        /// <returns>List of users formated with GetOneUserDTO</returns>
        public async Task<List<GetOneUserDTO>> GetAllUsersAsync()           // get all users
        {
            return await this._userRepository.GetAllUsersAsync();
        }

        public async Task<List<GetOneUserDTO>> GetUserByCarPoolAsync(int carPoolId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get one User with User.Id
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>one user formated with GetOneUserDTO</returns>
        public async Task<GetOneUserDTO> GetUserByIdAsync(string userId)                // get user by Id
        {
            return await this._userRepository.GetUserByIdAsync(userId);
        }

        public Task<GetOneUserDTO> GetUserByRentAsync(int rentId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get list of user in the role
        /// </summary>
        /// <param name="role"></param>
        /// <returns>List of user formated with GetOneUserDTO</returns>
        public async Task<List<GetOneUserDTO?>> GetUserByRoleAsync(string role)                 // get user by role
        {
            return await this._userRepository.GetUserByRoleAsync(role);
        }

        public Task<List<GetOneUserDTO>> GetUsersByNameAsync(GetUserByNameDTO getUserByNameDTO)
        {
            throw new NotImplementedException();
        }

        public Task<GetOneUserDTO> UpdateUserByIdAsync(CreateUserDTO updateOneUserDTO)
        {
            throw new NotImplementedException();
        }
    }
}
