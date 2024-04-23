using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Vehicles
{
    public class CreateVehicleDTO
    {
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int ModelId { get; set; }
        public int MotorizationId { get; set; }
        public string Color { get; set; }
        public string Immatriculation { get; set; }
        public string PictureUrl { get; set; }
    }
}
