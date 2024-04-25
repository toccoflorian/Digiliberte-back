using DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IUserService
    {
        public Task<List<GetOneUserDTO>> GetAllUsersAsync();
        public Task<GetOneUserDTO> GetUserByIdAsync(string userId);
        public Task<List<GetOneUserDTO>> GetUserByRoleAsync(int rentId);
        public Task<GetOneUserDTO> GetUserByRentAsync(int rentId);
        public Task<List<GetOneUserDTO>> GetUserByCarPoolAsync(int carPoolId);
        public Task<List<GetOneUserDTO>> GetUsersByNameAsync(GetUserByNameDTO getUserByNameDTO);
        public Task<GetOneUserDTO> UpdateUserByIdAsync(CreateUserDTO updateOneUserDTO);
        public Task DeleteUserByIdAsync(string userId);
    }
}
