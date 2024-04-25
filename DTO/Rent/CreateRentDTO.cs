using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Rent
{
    public class CreateRentDTO
    {
        public int VehiceId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string UserID { get; set; }
    }
}
