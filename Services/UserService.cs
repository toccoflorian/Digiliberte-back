using DTO.Rent;
using DTO.User;
using IRepositories;
using IServices;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRentRepository _rentRepository;

        public UserService(IUserRepository userRepository, IRentRepository rentRepository)
        {
            this._userRepository = userRepository;
            this._rentRepository = rentRepository;
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

        public async Task<List<GetOneUserDTO>> GetUserByCarPoolAsync(int carPoolId)
        {
            throw new NotImplementedException();
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

        public Task<GetOneUserDTO> UpdateUserByIdAsync(CreateUserDTO updateOneUserDTO)
        {
            throw new NotImplementedException();
        }
    }
}
