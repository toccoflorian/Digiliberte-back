using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Model
    {

        public int Id { get; set; }
        public string Label { get; set; }
        public double CO2 { get; set; }
        public int Year { get; set; }

        // Navigation For EF

        public List<Vehicle>? Vehicles { get; set; }

    }
}
