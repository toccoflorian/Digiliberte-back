using DTO.User;
using IRepositories;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        /// <summary>
        /// Delete one User with referenced AppUser
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>void</returns>
        public async Task DeleteUserByIdAsync(string userId)
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
        public async Task<GetOneUserDTO> GetUserByIdAsync(string userId)
        {
            return await this._userRepository.GetUserByIdAsync(userId);
        }

        public Task<GetOneUserDTO> GetUserByRentAsync(int rentId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneUserDTO>> GetUserByRoleAsync(int rentId)
        {
            throw new NotImplementedException();
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
