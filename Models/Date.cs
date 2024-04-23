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
        public List<Rent>? StartDateRents {  get; set; }
        public List<Rent>? EndDateRents { get; set; }
        public List<CarPool>? StartDateCarPools {  get; set; }
        public List<CarPool>? EndDateCarPools {  get; set; }
    }
}
