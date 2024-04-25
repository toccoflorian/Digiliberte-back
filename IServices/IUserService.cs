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
        public Task<Task<GetOneUserDTO>> GetUserByIdAsync(string userID);
        public Task<List<GetOneUserDTO>> GetUserByRoleAsync(int rentID);
        public Task<GetOneUserDTO> GetUserByRentAsync(int rentID);
        public Task<List<GetOneUserDTO>> GetUserByCarPoolAsync(int carPoolID);
        public Task<List<GetOneUserDTO>> GetUsersByNameAsync(GetUserByNameDTO getUserByNameDTO);
        public Task<GetOneUserDTO> UpdateUserByIdAsync(CreateUserDTO updateOneUserDTO);
        public Task DeleteUserByIdAsync(int userID);
    }
}
