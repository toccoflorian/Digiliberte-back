using DTO.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CarPools
{
    public class UpdateOneCarPoolDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LocalizationDTO StartLocalization { get; set; }
        public LocalizationDTO EndLocalization { get; set; }
        public int BlockedSeats { get; set; }
    }
}
