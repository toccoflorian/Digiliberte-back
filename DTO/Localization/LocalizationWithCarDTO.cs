using DTO.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Localization
{
    public class LocalizationWithCarDTO
    {
        public double Longitude {  get; set; }
        public double Latitude { get; set; }

        public List<VehicleSimpleIdDTO>? Vehicles { get; set; }
    }
}
