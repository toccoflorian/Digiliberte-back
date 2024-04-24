using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Models;
using Repositories;
using System;

namespace Main
{
    public class InitializeUser
    {
        //sdqdqsdqsd
        public async static Task adminInit(DatabaseContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if ((await roleManager.FindByNameAsync("ADMIN")) == null) // creer un role admin si null
            {
                string adminPassword = "Azerty1+";
                AppUser userAdmin = new AppUser { Email = "admin@admin.fr", NormalizedEmail = "admin@admin.fr", UserName = "admin@admin.fr" };
                IdentityRole roleAdmin = new IdentityRole { Name = "admin", NormalizedName = "ADMIN" };

                User userClass = new User { Id= userAdmin.Id , AppUserId = userAdmin.Id, Firstname = "admin", Lastname="ADMIN", AppUser = userAdmin,PictureURL="PHOTO" };

                IdentityRole? roleAdminCheck = await context.Roles.FindAsync(roleAdmin.Id);
                User? userAdminCheck = await context.Users.FindAsync(userClass.Id);

                if (roleAdminCheck == null && userAdminCheck == null)
                {
                    await userManager.CreateAsync(userAdmin, adminPassword);
                    await roleManager.CreateAsync(roleAdmin);

                    await userManager.AddToRoleAsync(userAdmin, "ADMIN");

                    await context.AddAsync(userClass);
                    await context.SaveChangesAsync();
                }
            }
            
            
        }
        public async static Task UserInit(DatabaseContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if ((await roleManager.FindByNameAsync("USER")) == null) // Creer un role user si null
            {
                string userPassword = "Azerty1+";
                AppUser newUser = new AppUser { Email = "user@user.fr", NormalizedEmail = "user@user.fr", UserName = "user@user.fr" };
                IdentityRole roleUser = new IdentityRole { Name = "user", NormalizedName = "USER" };

                User userClass1 = new User { Id=newUser.Id, AppUserId = newUser.Id, Firstname = "user",Lastname="USER", AppUser = newUser, PictureURL = "PHOTO" };

                IdentityRole? roleUserCheck = await context.Roles.FindAsync(roleUser.Id);
                User? newUserCheck = await context.Users.FindAsync(userClass1.Id);

                if (roleUserCheck == null && newUserCheck == null)
                {
                    await userManager.CreateAsync(newUser, userPassword);
                    await roleManager.CreateAsync(roleUser);

                    await userManager.AddToRoleAsync(newUser, "USER");

                    await context.AddAsync(userClass1);
                    await context.SaveChangesAsync();
                }
            }
        }


    }
}
