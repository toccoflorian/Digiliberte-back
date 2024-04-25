using DTO.User;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IUserRepository
    {
        public Task<List<GetOneUserDTO>> GetAllUsersAsync();
        public Task<Task<GetOneUserDTO>> GetUserByIdAsync(string userID);
        public Task<List<GetOneUserDTO>> GetUserByRoleAsync(int rentID);
        public Task<GetOneUserDTO> GetUserByRentAsync(int rentID);
        public Task<List<GetOneUserDTO>> GetUserByCarPoolAsync(int carPoolID);
        public Task<List<GetOneUserDTO>> GetUsersByNameAsync(GetUserByNameDTO getUserByNameDTO);
        public Task<GetOneUserDTO> UpdateUserByIdAsync(CreateUserDTO updateOneUserDTO);
        public Task DeleteUserByIdAsync(string userID);
    }
}
