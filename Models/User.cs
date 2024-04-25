using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// Id, Firstname, Lastname, PictureURL, AppUserId
    /// </summary>
    public class User
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PictureURL { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<Rent>? Rents { get; set; }
        public List<CarPoolPassenger>? CarPoolsPassengers { get; set; }
    }
}
