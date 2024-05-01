using DTO.Localization;

namespace DTO.CarPools
{
    public class UpdateOneCarPoolDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LocalizationDTO StartLocalization { get; set; }
        public LocalizationDTO EndLocalization { get; set; }
    }
}
