using DTO.CarPoolPassenger;
using DTO.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CarPools
{
    /// <summary>
    /// simple DTO , used in rent Repository
    /// </summary>
    public class GetCarPoolSimpleWithPassengerDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LocalizationDTO StartLocalization { get; set; }
        public LocalizationDTO EndLocalization { get; set; }
        public List<GetOneCarPoolPassengerDTO> CarPoolPassenger { get; set; }
    }
}
