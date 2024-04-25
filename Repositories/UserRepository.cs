using DTO.User;
using IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<AppUser> _userManager;
        public UserRepository(DatabaseContext context, UserManager<AppUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        /// <summary>
        /// Delete one User with referenced AppUser
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>void</returns>
        public async Task DeleteUserByIdAsync(string userID)
        {
            AppUser? appUser = await this._userManager.FindByIdAsync(userID);
            if (appUser == null)
            {
                throw new Exception("Utilisateur introuvable ! Aucune suppression n'a été éffectuée !");
            }
            else
            {
                await this._userManager.DeleteAsync(appUser);
            }
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
