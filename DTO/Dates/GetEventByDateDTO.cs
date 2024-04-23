using DTO.CarPools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Dates
{
    public class GetEventByDateDTO
    {
        public List<GetOneRentDTO> CurrentRents {  get; set; }
        public List<GetOneCarPoolDTO> CurrentCarPool {  get; set; }
    }
}
