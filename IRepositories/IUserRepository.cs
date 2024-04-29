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
        public Task<List<GetOneUserDTO>> GetUsersByNameAsync(GetUserByNameDTO getUserByNameDTO);
        public Task UpdateUserByIdAsync(UpdateUserDTO updateOneUserDTO);
        public Task DeleteUserByIdAsync(AppUser appUser);
    }
}
