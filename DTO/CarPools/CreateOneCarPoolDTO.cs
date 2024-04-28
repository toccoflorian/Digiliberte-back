using DTO.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;


namespace DTO.CarPools
{
    public class CreateOneCarPoolDTO
    {
        public int RentId { get; set; }
        public LocalizationDTO StartLocalization { get; set; }
        public LocalizationDTO EndLocalization { get; set; }
        public DatesClass StartDate { get; set; }
        public DatesClass EndDate { get; set; }
    }
}
