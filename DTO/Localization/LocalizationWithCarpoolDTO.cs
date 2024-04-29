using DTO.CarPools;
using DTO.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Localization
{
    /// <summary>
    /// double longitude , double latitude, List CarPoolDTO
    /// </summary>
    public class LocalizationWithCarpoolDTO
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public List<CarpoolWithLocalizationIdDTO>? CarPoolsStartingAt { get; set; }
        public List<CarpoolWithLocalizationIdDTO>? CarPoolsEndingAt { get; set; }
    }
}
