using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTO.Rent
{
    /// <summary>
    /// Class used to update Rent Client Side
    /// </summary>
    public class UpdateRentRequestDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ReturnDate { get; set; }

    }
}
