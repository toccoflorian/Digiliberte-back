using DTO.CarPoolPassenger;
using DTO.Localization;
using DTO.User;
using Models;
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
        public int RentId { get; set; }
        public string UserId { get; set; }
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
        public virtual GetOneCarPoolDTO MapAsync(CarPool carpool)
        {
            this.CarPoolId = carpool.Id;
            this.RentId = carpool.RentId;
            this.UserId = carpool.Rent.UserID;
            this.VehicleId = carpool.Rent.Vehicle.Id;
            this.VehicleBrand = carpool.Rent.Vehicle.Brand.Label;
            this.VehicleModel = carpool.Rent.Vehicle.Model.Label;
            this.VehicleImmatriculation = carpool.Rent.Vehicle.Immatriculation;
            this.VehicleMotorization = carpool.Rent.Vehicle.Motorization.Label;
            this.SeatsTotalNumber = carpool.Rent.Vehicle.Category.SeatsNumber;
            this.CO2 = carpool.Rent.Vehicle.Model.CO2;
            this.StartDate = carpool.StartDate.Date;
            this.EndDate = carpool.EndDate.Date;
            this.StartLocalization = new LocalizationDTO
            {
                Latitude = carpool.StartLocalization.Latitude,
                Logitude = carpool.StartLocalization.Longitude,
            };
            this.EndLocalization = new LocalizationDTO
            {
                Latitude = carpool.EndLocalization.Latitude,
                Logitude = carpool.EndLocalization.Longitude,
            };
            return this;
        }
    }

    public class GetOneCarPoolWithPassengersDTO : GetOneCarPoolDTO
    {
        public int FreeSeats { get; set; }
        public List<GetOneCarPoolPassengerDTO>? Passengers { get; set; }
        public override GetOneCarPoolWithPassengersDTO MapAsync(CarPool carpool) 
        {
            GetOneCarPoolDTO? carpoolDTO = base.MapAsync(carpool);
            this.FreeSeats = carpool.Rent.Vehicle.Category.SeatsNumber - carpool.carPoolPassengers!.Count();
            this.Passengers = carpool.carPoolPassengers
                .Select(passenger =>
                    new GetOneCarPoolPassengerDTO
                    {
                        Id = passenger.Id,
                        CarPoolId = carpool.Id,
                        Description = passenger.Description,
                        UserDTO = new GetOneUserDTO
                        {
                            Id = passenger.UserID,
                            Firstname = passenger.User.Firstname,
                            Lastname = passenger.User.Lastname,
                            PictureURL = passenger.User.PictureURL
                        }
                    })
                .ToList();
            return this;
        }
    }
}
