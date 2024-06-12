using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CarPoolPassenger
{
    public class UpdateCarPoolPassengerDTO
    {
        public int Id { get; set; }
        public int? CarpoolId { get; set; }
        public string? Description { get; set; }
        public string UserId { get; set; }
        public string UserRole { get; set; }
    }
}
