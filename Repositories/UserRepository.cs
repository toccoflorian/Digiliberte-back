using DTO.User;
using IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        public UserRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public Task DeleteUserByIdAsync(int userID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// get all users registered
        /// </summary>
        /// <returns>List of users formated with GetOneUserDTO</returns>
        public async Task<List<GetOneUserDTO>> GetAllUsersAsync()           // get all users
        {
            return await this._context.Users.Select(user => 
                new GetOneUserDTO
                {
                    Id = user.Id,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    PictureURL = user.PictureURL,
                }).ToListAsync();
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
