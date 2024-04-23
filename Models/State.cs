using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class State
    {
        public int Id { get; set; }
        public string Label { get; set; }

        // Navigation For EF

        public List<Vehicle>? Vehicles { get; set; }
    }
}
