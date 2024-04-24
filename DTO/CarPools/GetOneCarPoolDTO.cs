using DTO.CarPoolPassenger;
using DTO.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CarPools
{
    public class GetOneCarPoolDTO
    {
        public int CarPoolId { get; set; }
        public int VehicleId { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleImmatriculation { get; set; }
        public string VehicleMotorization { get; set; }
        public int SeatsTotalNumber { get; set; }
        public double CO2 { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LocalizationDTO StartLocalization { get; set; }
        public LocalizationDTO EndLocalization { get; set; }
    }

    public class GetOneCarPoolWithPassengersDTO : GetOneCarPoolDTO
    {
        public int FreeSeats { get; set; }
        public List<GetOneCarPoolPassengerDTO> Passengers { get; set; }
    }
}
