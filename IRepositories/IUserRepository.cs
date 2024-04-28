using DTO.User;
using Microsoft.AspNetCore.Identity;
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
        public Task<GetOneUserDTO?> GetUserByIdAsync(string userId);
        public Task<List<GetOneUserDTO?>> GetUserByRoleAsync(string role);
        public Task<List<GetOneUserDTO>> GetUserByCarPoolAsync(int carPoolId);
        public Task<List<GetOneUserDTO>> GetUsersByNameAsync(GetUserByNameDTO getUserByNameDTO);
        public Task<GetOneUserDTO> UpdateUserByIdAsync(CreateUserDTO updateOneUserDTO);
        public Task DeleteUserByIdAsync(string userId);
    }
}
