using DTO.User;


namespace DTO.CarPoolPassenger
{
    public class GetOneCarPoolPassengerDTO
    {
        public int Id { get; set; }
        public int CarPoolId { get; set; }
        public GetOneUserDTO UserDTO { get; set; }
        public string Description { get; set; }
    }
}
