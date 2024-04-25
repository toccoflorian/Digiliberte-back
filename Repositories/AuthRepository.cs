using DTO.User;
using IRepositories;
using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<AppUser> _userManager;
        public AuthRepository(DatabaseContext context, UserManager<AppUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        /// <summary>
        ///  registration of a new user
        /// </summary>
        /// <param name="createUserDTO"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task RegisterAsync(CreateUserDTO createUserDTO)
        {
            AppUser appUser = new AppUser{ 
                UserName = createUserDTO.EmailLogin.ToUpper(), 
                NormalizedUserName = createUserDTO.EmailLogin.ToUpper(),
                Email = createUserDTO.EmailLogin,
                NormalizedEmail = createUserDTO.EmailLogin.ToUpper()};

            IdentityResult? identityResult = await this._userManager.CreateAsync(appUser, createUserDTO.Password);

            if (identityResult.Succeeded)
            {
                await this._context.Users.AddAsync(new User
                {
                    Id = appUser.Id,
                    Firstname = createUserDTO.Firstname,
                    Lastname = createUserDTO.Lastname,
                    AppUserId = appUser.Id,
                    PictureURL = "https://defaultPhoto.com"
                });
                await this._context.SaveChangesAsync();
        }
            else
            {
                throw new Exception(identityResult.Errors.ToString());
            }
        }
    }
}
