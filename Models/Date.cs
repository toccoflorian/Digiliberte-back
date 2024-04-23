using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Date
    {
        public int Id;

        public DateTime Date {  get; set; }
        public StartDateRents List<Rent> {  get; set; }
        public EndDateRent List<Rent> { get; set; }
        public StartDateCarPools List<CarPool>{  get; set; }
        public EndDateCarPool List<CarPool>{  get; set; }
    }
}
