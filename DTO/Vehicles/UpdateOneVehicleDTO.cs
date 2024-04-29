using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Vehicles
{
    public class UpdateOneVehicleDTO
    {
        //public int VehicleId { get; set; }
        //public int LocalizationId { get; set; }
        public int? StateId { get; set; }
        public string? PictureURL { get; set; }
    }
}
