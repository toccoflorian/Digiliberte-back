using DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CarPoolPassenger
{
    public class GetOneCarPoolPassengerDTO
    {
        public int Id { get; set; }
        public GetOneUserDTO UserDTO { get; set; }
        public int CarPoolId { get; set; }
        public string Description { get; set; }
    }
}
