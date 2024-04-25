using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Localization
{
    /// <summary>
    /// DTO WITH LONGITUDE AND LATITUDE, used to create and display them ? 
    /// double latitude , double longitude
    /// </summary>
    public class LocalizationDTO
    {
        public double Logitude {  get; set; }
        public double Latitude { get; set; }
        //public string City { get; set; }
        //public string RoadName { get; set; }
        //public int RoadNumber { get; set; }
    }
}
