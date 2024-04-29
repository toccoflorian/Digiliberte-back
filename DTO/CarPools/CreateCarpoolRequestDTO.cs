using DTO.Dates;
using DTO.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CarPools
{
    /// <summary>
    /// Carpool used in controller for carpoool creation
    /// </summary>
    public class CreateCarpoolRequestDTO
    {
        public int RentId { get; set; }
        public string CarpoolName { get; set; }
        public LocalizationDTO StartLocalization { get; set; }
        public LocalizationDTO EndLocalization { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
