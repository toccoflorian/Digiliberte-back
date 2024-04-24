using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    /// <summary>
    /// Id, name , Co2, Year
    /// </summary>
    public class GetOneModelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public double Co2 { get; set; }
    }
}
