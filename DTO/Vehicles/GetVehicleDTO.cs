
using DTO.Localization;
using Models;

namespace DTO.Vehicles
{
    public class GetVehicleDTO
    {
        public int VehicleId { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string CategoryName { get; set; }
        public string MotorizationName { get; set; }
        public string PictureUrl { get; set; }
        public string? Color { get; set; }
        public int SeatsNumber { get; set; }
        public double CO2 { get; set; }
        public int ModelYear { get; set; }
        public string Immatriculation { get; set; }

        public GetVehicleDTO(Vehicle vehicle) 
        {
            VehicleId = vehicle.Id;
            BrandName = vehicle.Brand.Label;
            ModelName = vehicle.Model.Label;
            CategoryName = vehicle.Category.Label;
            MotorizationName = vehicle.Motorization.Label;
            PictureUrl = vehicle.PictureURL;
            SeatsNumber = vehicle.Model.Year;
            CO2 = vehicle.Model.CO2;
            ModelYear = vehicle.Model.Year;
            Immatriculation = vehicle.Immatriculation;
        }
    }
}
