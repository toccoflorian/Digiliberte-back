using DTO.Dates;
using DTO.Rent;

namespace IServices
{
    public interface IRentService
    {
        /// <summary>
        /// Creating One rent, using DTO for the request, might change createRentDTO to CreateRentREQUEST
        /// </summary>
        /// <param name="createOneRentDTO"></param>
        /// <returns></returns>
        public Task<GetOneRentDTO> CreateOneRentAsync(CreateRentDTO createOneRentDTO);
        /// <summary>
        /// Updating Rent by Id, using requestDTO Id.
        /// </summary>
        /// <param name="updateRentRequest">Take the DTO used in request/controller and gives it to the repository, also take the id to search the good entity</param>
        /// <see cref="GetRentByIdAsync(int)"/>
        /// <returns><see cref="GetRentByIdAsync(int)"/>
        /// Return a getOneRentDTO using the method GetRentByIdAsync</returns>
        public Task<GetOneRentDTO> UpdateRentByIdAsync(UpdateRentRequestDTO updateRentRequest);
        public Task DeleteRentByIdAsync(int rentID);
        public Task<List<GetOneRentDTO>> GetAllRentAsync();
        public Task<GetOneRentDTO> GetRentByCarPoolAsync(int carPoolID);
        public Task<List<GetOneRentWithCarPoolDTO>> GetRentsByUserAsync(string userID);
        public Task<List<GetOneRentDTO>> GetRentsByDateForkAsync(DateForkDTO dateForkDTO);
        public Task<List<GetOneRentDTO>> GetRentsByEndDateAsync(DateTime date);
        public Task<List<GetOneRentDTO>> GetRentsByStartDateAsync(DateTime date);
        public Task<GetOneRentDTO> GetRentByIdAsync(int rentID);
        public Task<List<GetOneRentDTO>> GetRentByVehicleIdAsync(int vehicleId);
        public Task<List<GetOneRentByVehicleIdDTO>> GetRentSimpleByVehicleIdAsync(int vehicleId);
    }
}
