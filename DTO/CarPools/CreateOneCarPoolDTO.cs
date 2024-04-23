using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DTO.CarPools
{
    public class CreateOneCarPoolDTO
    {
        public int RentId { get; set; }
        public LocalizationDTO StartLocalization { get; set; }
        public LocalizationDTO EndLocalization { get; set; }
        public Dates StartDate { get; set; }
        public Dates EndDate { get; set; }
    }
}
