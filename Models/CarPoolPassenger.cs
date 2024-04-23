using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CarPoolPassenger
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CarPoolID { get; set; }
        public CarPool CarPool { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
    }
}
