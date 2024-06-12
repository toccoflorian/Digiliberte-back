using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Rent
{
    /// <summary>
    /// DTO USED FOR CREATING A RENT , UserId, UserFirstName, UserLAstName, Vehicle Id , StartDate ,StartDateId, return date, ReturnDateId , à changer ?
    /// </summary>
    public class CreateRentDTO
    {
        public string? UserID { get; set; }
        public string? UserFirstname { get; set; }
        public string? UserLastname { get; set; }
        public int VehiceId { get; set; }
        public string? VehicleInfos { get; set; }
        public string? Immatriculation { get; set; }
        public DateTime StartDate { get; set; }
        public int? StartDateId { get; set; }
        public DateTime ReturnDate { get; set; }
        public int? ReturnDateId { get; set;}
    }
}
