using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CarPools
{
    /// <summary>
    /// DTO made to get the carpool id and Localization ID, Start And End Coordinates 
    /// </summary>
    public class CarpoolWithLocalizationIdDTO
    {
        public int Id { get; set; }
        public int StartLocalizationId { get; set; }
        public double StartLatitude { get; set; }
        public double StartLongitude { get; set; }
        public int EndLocalizationId { get; set; }
        public double EndLatitude { get; set; }
        public double EndLongitude { get; set; }
    }
}
