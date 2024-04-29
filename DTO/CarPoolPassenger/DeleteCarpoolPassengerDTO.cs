using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CarPoolPassenger
{
    public class DeleteCarpoolPassengerDTO
    {
        public int Id { get; set; }
        public string? ConnectedUserId { get; set; }
        public string? ConnectedUserRole { get; set; }
    }
}
