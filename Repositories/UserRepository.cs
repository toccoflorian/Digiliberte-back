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
        public async Task DeleteUserByIdAsync(string userId)
        {
            AppUser? appUser = await this._userManager.FindByIdAsync(userId);
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

        public Task<List<GetOneUserDTO>> GetUserByCarPoolAsync(int carPoolId)
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
            GetOneUserDTO? userDTO = await this._context.Users.Select(user =>
                new GetOneUserDTO
                {
                    Id = user.Id,
                    Firstname= user.Firstname,
                    Lastname= user.Lastname,
                    PictureURL = user.PictureURL
                }).FirstOrDefaultAsync(user => user.Id == userId);
            if(userDTO == null)
            {
                throw new Exception("L'utilisateur est introuvable !");
            }
            else
            {
                return userDTO;
            }
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
