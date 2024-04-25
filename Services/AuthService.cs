using DTO.Auth;
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


        public async Task RegisterAsync(RegisterDTO registerDTO)
        {
            if(registerDTO.Password == null || registerDTO.Password != registerDTO.ConfirmPassword)
            {
                throw new Exception("Le mot de passe et la confirmation du mot de passe doivent être identiques !");
            }
            if(registerDTO.EMail == null || registerDTO.EMail == "" || registerDTO.EMail.Length < 3)
            {
                throw new Exception("Le login est incorrect !");
            }
            await this._authRepository.RegisterAsync(registerDTO);
        }
    }
}
