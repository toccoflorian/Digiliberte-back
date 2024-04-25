using DTO.Auth;
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

        public async Task RegisterAsync(RegisterDTO registerDTO)
        {
            AppUser appUser = new AppUser{ 
                UserName = registerDTO.EMail.ToUpper(), 
                NormalizedUserName = registerDTO.EMail.ToUpper(),
                Email = registerDTO.EMail,
                NormalizedEmail = registerDTO.EMail.ToUpper()};

            IdentityResult? identityResult = await this._userManager.CreateAsync(appUser, registerDTO.Password);

            if (identityResult.Succeeded)
            {
                await this._context.Users.AddAsync(new User
                {
                    Id = appUser.Id,
                    Firstname = registerDTO.Firstname,
                    Lastname = registerDTO.Lastname,
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
