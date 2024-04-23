using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Vehicles
{
    public class GetOneVehicleDTO
    {
        public int VehicleId { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string CategoryName { get; set; }
        public string MotorizationName { get; set; }
        public string StateName { get; set; }
        public string PictureUrl { get; set; }
        public string Localization { get; set; }
        public string Color { get; set; }
        public string SeatsNumber { get; set; }
        public double CO2 { get; set; }
        public int ModelYear { get; set; }
        public string Immatriculation { get; set; }
    }
    public class GetOneVehicleWithRentDTO : GetOneVehicleDTO
    {
        public List<GetOneRentDTO> Rents { get; set; }
    }
}
