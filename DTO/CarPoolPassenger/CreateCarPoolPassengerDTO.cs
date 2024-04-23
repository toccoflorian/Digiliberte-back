using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CarPoolPassenger
{
    public class CreateCarPoolPassengerDTO
    {
        public int CarPoolId { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
