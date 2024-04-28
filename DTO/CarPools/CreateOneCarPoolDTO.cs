using DTO.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DTO.Dates;


namespace DTO.CarPools
{
    public class CreateOneCarPoolDTO
    {
        public string? UserId { get; set; }
        public int RentId { get; set; }
        public string CarpoolName { get; set; }
        public int? StartLocalizationId { get; set; }
        public LocalizationDTO StartLocalization { get; set; }
        public int? EndLocalizationId { get; set; }
        public LocalizationDTO EndLocalization { get; set; }
        public int? StartDateId { get; set; }
        public GetOneDateDTO StartDate { get; set; }
        public int? EndDateId { get; set; }
        public GetOneDateDTO EndDate { get; set; }
    }
}
