using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Localization
    {
        public int Id { get; set; }
        public double Latitude {  get; set; }
        public double Longitude { get; set; }
        // EF navigation

        public List<Vehicle>? Vehicles { get; set; }
        public List<CarPool>? StartLocalizationCarPools { get; set; }
        public List<CarPool>? EndLocalizationCarPools { get; set; }
    }
}
