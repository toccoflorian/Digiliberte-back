using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Rent
    {
        [Key]
        public int Id { get; set; }
        public int StartDateID { get; set; }
        public DatesClass StartDate { get; set; }
        public int ReturnDateID { get; set; }
        public DatesClass ReturnDate { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public Vehicle Vehicle {  get; set; }
        public List<CarPool>? CarPools {  get; set; }

    }
}
