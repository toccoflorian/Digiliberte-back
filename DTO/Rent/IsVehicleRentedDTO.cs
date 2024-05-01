using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class IsVehicleRentedDTO
    {
        public int Id {get; set;}
        public int StartDateID {get; set;}
        public int ReturnDateID {get; set;}
    }
}
