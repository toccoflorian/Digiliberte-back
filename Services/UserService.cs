using DTO.CarPools;
using DTO.Rent;
using DTO.User;
using IRepositories;
using IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRentRepository _rentRepository;
        private readonly ICarPoolRepository _carPoolRepository;
        private readonly UserManager<AppUser> _userManager;
        public UserService(
            IUserRepository userRepository, 
            IRentRepository rentRepository,
            ICarPoolRepository carPoolRepository,
            UserManager<AppUser> userManager)
        {
            this._userRepository = userRepository;
            this._rentRepository = rentRepository;
            this._carPoolRepository = carPoolRepository;
            this._userManager = userManager;
        }

        /// <summary>
        /// Delete one User with referenced AppUser
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>void</returns>
        public async Task DeleteUserByIdAsync(string userId)                // delete user
        {
            await this._userRepository.DeleteUserByIdAsync(userId);
        }

        /// <summary>
        /// get all users registered
        /// </summary>
        /// <returns>List of users formated with GetOneUserDTO</returns>
        public async Task<List<GetOneUserDTO>> GetAllUsersAsync()           // get all users
        {
            return await this._userRepository.GetAllUsersAsync();
        }

        /// <summary>
        /// Get the user in origin of the carpool
        /// </summary>
        /// <param name="carPoolID"></param>
        /// <returns>one user formated with GetOneUserDTO</returns>
        public async Task<GetOneUserDTO> GetUserByCarPoolAsync(int carPoolId)
        {
            if(carPoolId == 0)
            {
                throw new ArgumentException("Merci de renseigner un id valide");
            }
            GetOneCarPoolWithPassengersDTO? carpool = await this._carPoolRepository.GetCarPoolByIdAsync(carPoolId);
            if(carpool == null)
            {
                throw new ArgumentException("Le covoiturage n'existe pas !");
            }
            GetOneUserDTO? user = await this._userRepository.GetUserByIdAsync(carpool.UserId);
            if (user == null)
            {   // une location devrais forcement avoir un user createur
                throw new Exception("Une erreur s'est produite, merci de contacter le développeur back-end");
            }
            return user;
        }

        /// <summary>
        /// Get one User with User.Id
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>one user formated with GetOneUserDTO</returns>
        public async Task<GetOneUserDTO> GetUserByIdAsync(string userId)                // get user by Id
        {
            GetOneUserDTO? userDTO = await this._userRepository.GetUserByIdAsync(userId);
            if(userDTO == null)
            {
                throw new Exception("L'utilisateur est introuvable !");
            }
            return userDTO;
        }

        /// <summary>
        /// Get the user of one rent
        /// </summary>
        /// <param name="rentId"></param>
        /// <returns>one user formated with GetOneUserDTO</returns>
        public async Task<GetOneUserDTO> GetUserByRentAsync(int rentId)
        {
            GetOneRentDTO? rentDTO = await this._rentRepository.GetRentByIdAsync(rentId);
            if(rentDTO == null)
            {
                throw new Exception("Aucune location avec cette id !");
            }
            GetOneUserDTO? userDTO = await this._userRepository.GetUserByIdAsync(rentDTO.UserId);
            if(userDTO == null)
            {
                throw new Exception("Une erreur s'est produite, location sans utilisateur referencé, merci de contater le dev back-end !");
            }
            return userDTO;
        }

        /// <summary>
        /// Get list of user in the role
        /// </summary>
        /// <param name="role"></param>
        /// <returns>List of user formated with GetOneUserDTO</returns>
        public async Task<List<GetOneUserDTO?>> GetUserByRoleAsync(string role)                 // get user by role
        {
            return await this._userRepository.GetUserByRoleAsync(role);
        }


        /// <summary>
        /// get a list of users by name
        /// </summary>
        /// <param name="getUserByNameDTO"></param>
        /// <returns>List of users formated with GetUserByNameDTO</returns>
        public async Task<List<GetOneUserDTO>> GetUsersByNameAsync(GetUserByNameDTO getUserByNameDTO)     // get users by name
        {
            return await this._userRepository.GetUsersByNameAsync(getUserByNameDTO);
        }

        public async Task<GetOneUserDTO> UpdateUserByIdAsync(UpdateUserDTO updateOneUserDTO)
        {
            AppUser appUser = (await this._userManager.Users
                .Include(appuser => appuser.User)
                .FirstOrDefaultAsync(appuser => appuser.Id == updateOneUserDTO.UserId))!;
            if (updateOneUserDTO.EmailLogin != null && updateOneUserDTO.EmailLogin != "" && updateOneUserDTO.EmailLogin != "string")
            {
                string? emailToken = await this._userManager.GenerateChangeEmailTokenAsync(appUser, updateOneUserDTO.EmailLogin);
                await this._userManager.ChangeEmailAsync(appUser, updateOneUserDTO.EmailLogin, emailToken);
                appUser.NormalizedUserName = updateOneUserDTO.EmailLogin.ToUpper();
                appUser.UserName = updateOneUserDTO.EmailLogin;
                await this._userManager.UpdateAsync(appUser);
            }
            if (updateOneUserDTO.Password != null && updateOneUserDTO.Password != "" && updateOneUserDTO.Password != "string") 
            {
                if(updateOneUserDTO.Password != updateOneUserDTO.ConfirmPassword)
                {
                    throw new ArgumentException("Le mot de passe et la confirmation de mot de passe doivent être identiques !");
                }
                await this._userManager.ChangePasswordAsync(appUser, updateOneUserDTO.OldPassword, updateOneUserDTO.Password);
            }
            if(updateOneUserDTO.Firstname != null && updateOneUserDTO.Firstname != "" && updateOneUserDTO.Firstname != "string"
                ||
                updateOneUserDTO.Lastname != null && updateOneUserDTO.Lastname != "" && updateOneUserDTO.Lastname != "string"
                ||
                updateOneUserDTO.PictureURL != null && updateOneUserDTO.PictureURL != "" && updateOneUserDTO.PictureURL != "string")
            {
                await this._userRepository.UpdateUserByIdAsync(updateOneUserDTO);
            }
            return (await this._userRepository.GetUserByIdAsync(appUser.Id))!;
        }
    }
}
