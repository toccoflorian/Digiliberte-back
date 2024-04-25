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
        /// <param name="userID"></param>
        /// <returns>void</returns>
        public async Task DeleteUserByIdAsync(string userID)
        {
            await this._userRepository.DeleteUserByIdAsync(userID);
        }

        /// <summary>
        /// get all users registered
        /// </summary>
        /// <returns>List of users formated with GetOneUserDTO</returns>
        public async Task<List<GetOneUserDTO>> GetAllUsersAsync()           // get all users
        {
            return await this._userRepository.GetAllUsersAsync();
        }

        public Task<List<GetOneUserDTO>> GetUserByCarPoolAsync(int carPoolID)
        {
            throw new NotImplementedException();
        }

        public Task<Task<GetOneUserDTO>> GetUserByIdAsync(string userID)
        {
            throw new NotImplementedException();
        }

        public Task<GetOneUserDTO> GetUserByRentAsync(int rentID)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneUserDTO>> GetUserByRoleAsync(int rentID)
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
