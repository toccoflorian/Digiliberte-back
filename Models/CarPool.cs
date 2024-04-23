using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CarPool
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StartLocalizationID { get; set; }
        public Localization StartLocalization { get; set; }
        public int EndLocalizationID { get; set; }
        public Localization EndLocalization { get; set; }
        public int StartDateID { get; set; }
        public Dates StartDate { get; set; }
        public int EndDateID { get; set; }
        public Dates EndDate { get; set; }
        public int RentId { get; set; }
        public Rent Rent { get; set; }
        public List<CarPoolPassenger>? carPoolPassengers { get; set; }
    }
}
