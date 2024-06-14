using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    /// <summary>
    /// Name , Co2, Year
    /// </summary>
    public class CreateOneModelDTO
    {
        public string Name { get; set; }
        public int Co2 { get; set; }
        public int Year {  get; set; }
    }
}
