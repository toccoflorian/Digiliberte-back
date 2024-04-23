using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.User
{
    internal class GetOneUserDTO
    {
        public string Id {  get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? PictureURL { get; set; }
    }
}
