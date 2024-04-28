using DTO.User;
using IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;

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
        public async Task DeleteUserByIdAsync(AppUser appUser)                    // delete user
        {
                await this._userManager.DeleteAsync(appUser);
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


        /// <summary>
        /// Get one User with User.Id
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>one user formated with GetOneUserDTO</returns>
        public async Task<GetOneUserDTO?> GetUserByIdAsync(string userId)                // get user by id
        {
            return await this._context.Users.Select(user =>
                new GetOneUserDTO
                {
                    Id = user.Id,
                    Firstname= user.Firstname,
                    Lastname= user.Lastname,
                    PictureURL = user.PictureURL
                }).FirstOrDefaultAsync(user => user.Id == userId);
        }

        public async Task<List<GetOneUserDTO?>> GetUserByRoleAsync(string role)         
        {
            List<GetOneUserDTO?> userDTOs = new List<GetOneUserDTO?>();
            List<IdentityUserRole<string>>? userRoles = await this._context.UserRoles.ToListAsync();

            foreach (IdentityUserRole<string> userRole in userRoles)
            {
                AppUser? appUser = await this._userManager.FindByIdAsync(userRole.UserId);
                if (await this._userManager.IsInRoleAsync(appUser, role))
                {
                    userDTOs
                        .Add(await this._context.Users
                            .Select(user =>
                                new GetOneUserDTO
                                {
                                    Id = user.Id,
                                    Firstname = user.Firstname,
                                    Lastname = user.Lastname,
                                    PictureURL  = user.PictureURL
                                })
                            .FirstOrDefaultAsync(user => user.Id == userRole.UserId));
                }
            }
            return userDTOs;
        }


        /// <summary>
        /// get a list of users by name
        /// </summary>
        /// <param name="getUserByNameDTO"></param>
        /// <returns>List of users formated with GetUserByNameDTO</returns>
        public async Task<List<GetOneUserDTO>> GetUsersByNameAsync(GetUserByNameDTO getUserByNameDTO)     // get users by name
        {
            if (getUserByNameDTO.Firstname != null && getUserByNameDTO.Lastname == null)
            {
                return await this._context.Users
                .Select(user =>
                    new GetOneUserDTO
                    {
                        Id = user.Id,
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        PictureURL = user.PictureURL
                    })
                .Where(user => user.Firstname == getUserByNameDTO.Firstname)
                .ToListAsync();
            }
            else if (getUserByNameDTO.Firstname == null && getUserByNameDTO.Lastname != null)
            {
                return await this._context.Users
                .Select(user =>
                    new GetOneUserDTO
                    {
                        Id = user.Id,
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        PictureURL = user.PictureURL
                    })
                .Where(user => user.Lastname == getUserByNameDTO.Lastname)
                .ToListAsync();
            }
            else
            {
                return await this._context.Users
                .Select(user =>
                    new GetOneUserDTO
                    {
                        Id = user.Id,
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        PictureURL = user.PictureURL
                    })
                .Where(user => 
                    user.Firstname == getUserByNameDTO.Firstname
                    &&
                    user.Lastname == getUserByNameDTO.Lastname)
                .ToListAsync();
            }
            
        }

        public async Task UpdateUserByIdAsync(UpdateUserDTO updateOneUserDTO)
        {
            User user = (await this._context.Users.FindAsync(updateOneUserDTO.UserId))!;   
            if(updateOneUserDTO.Firstname != null)
            {
                user.Firstname = updateOneUserDTO.Firstname;
            }
            if(updateOneUserDTO.Lastname != null)
            {
                user.Lastname = updateOneUserDTO.Lastname;
            }
            if(updateOneUserDTO.PictureURL != null)
            {
                user.PictureURL = updateOneUserDTO.PictureURL;
            }
            await this._context.SaveChangesAsync();
        }
    }
}
