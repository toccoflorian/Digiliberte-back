using DTO.CarPools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Rent
{
    public class GetOneRentWithCarPoolDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int VehiceId { get; set; }
        public string VehicleInfo { get; set; }
        public string Immatriculation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string UserFirstname { get; set; }
        public string UserLastname { get; set; }
        public List<GetOneCarPoolWithPassengersDTO> CarPools  {  get; set; }
    }
}
