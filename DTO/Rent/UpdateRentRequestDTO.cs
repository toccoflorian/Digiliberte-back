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
        [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime StartDate { get; set; } = new DateTime(2000,01,01);
        public DateTime ReturnDate { get; set; } = new DateTime(2001, 01, 01);

    }
}
