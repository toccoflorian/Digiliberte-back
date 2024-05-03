using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Rent
{
    /// <summary>
    /// Class used to update Rent the check on this will be done on service
    /// </summary>
    public class UpdateRentDTO
    {
        public int Id { get; set; }
        public int StartDateId { get; set; }
        public DateTime StartDate { get; set; }
        public int ReturnDateId { get; set; }
        public DateTime ReturnDate { get; set; }

    }
}
