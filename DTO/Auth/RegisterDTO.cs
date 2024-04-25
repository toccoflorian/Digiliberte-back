using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Auth
{
    /// <summary>
    /// Login, Password, ConfirmPassword
    /// </summary>
    public class RegisterDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
