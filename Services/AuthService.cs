using DTO.User;
using IRepositories;
using IServices;


namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        public AuthService(IAuthRepository authRepository)
        {
            this._authRepository = authRepository;
        }

        /// <summary>
        /// registration of a new user
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task RegisterAsync(CreateUserDTO createUserDTO)
        {
            if(createUserDTO.Password == null || createUserDTO.Password != createUserDTO.ConfirmPassword) 
            {
                throw new Exception("Le mot de passe et la confirmation du mot de passe doivent être identiques !");
            }
            if(createUserDTO.EmailLogin == null || createUserDTO.EmailLogin == "" || createUserDTO.EmailLogin.Length < 3)
            {
                throw new Exception("Le login est incorrect !");
            }
            await this._authRepository.RegisterAsync(createUserDTO);
        }
    }
}
