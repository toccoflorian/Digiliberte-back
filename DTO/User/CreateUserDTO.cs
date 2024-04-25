using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.User
{
    public class CreateUserDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password1 { get; set; }
        public string EmailLogin { get; set; }
        public string? PictureURL { get; set; }
    }
}
