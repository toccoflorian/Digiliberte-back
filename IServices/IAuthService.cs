using DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IAuthService
    {
        public Task RegisterAsync(RegisterDTO registerDTO);
    }
}
