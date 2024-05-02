using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Rent
{
    public class GetOneRentWithCarPoolDTO : GetOneRentDTO
    {
        public List<GetOneRentDTO>? CarPools  {  get; set; }
    }
}
