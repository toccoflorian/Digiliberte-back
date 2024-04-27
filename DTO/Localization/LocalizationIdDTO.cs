using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Localization
{
    /// <summary>
    /// Exact Same as Localization class , might not use 
    /// </summary>
    public class LocalizationIdDTO
    {
        public int Id {  get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
