using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Rent
{
    /// <summary>
    /// DTO made to be used when look for a rent for a car, will be used in GetRentByVehicleId
    /// </summary>
    public class GetOneRentByVehicleIdDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int VehiceId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ReturnDate { get; set; }

    }
}
