using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.User
{
    public class UpdateUserDTO
    {
        public string? UserId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? EmailLogin { get; set; }
        public string? OldPassword { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? PictureURL { get; set; }
        public string? UserToken { get; set; }
    }
}
