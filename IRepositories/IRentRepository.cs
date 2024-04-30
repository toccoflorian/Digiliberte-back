using DTO.Rent;
using DTO.Dates;

namespace IRepositories
{
    public interface IRentRepository
    {
        /// <summary>
        /// Creates a new rental asynchronously based on the provided DTO.
        /// </summary>
        /// <param name="createOneRentDTO">The DTO containing information for creating a rental.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains a <see cref="GetOneRentDTO"/> object representing the newly created rental.
        /// </returns>
        public Task<GetOneRentDTO> CreateOneRentAsync(CreateRentDTO createOneRentDTO);

        public Task<int?> UpdateRentByIdAsync(UpdateRentDTO updateRentDTO);
        /// <summary>
        /// Deletes a rental by its ID asynchronously.
        /// </summary>
        /// <param name="rentID">The ID of the rental to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task DeleteRentByIdAsync(int rentID);
        /// <summary>
        /// Retrieves all rentals asynchronously.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains a list of <see cref="GetOneRentDTO"/> objects representing all rentals.
        /// </returns>
        public Task<List<GetOneRentDTO>> GetAllRentAsync();
        /// <summary>
        /// Retrieves rental information for a carpool based on the specified carpool ID asynchronously.
        /// </summary>
        /// <param name="carPoolID">The ID of the carpool for which rental information is to be retrieved.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains a <see cref="GetOneRentDTO"/> object representing the rental information for the specified carpool.
        /// </returns>
        public Task<GetOneRentDTO> GetRentByCarPoolAsync(int carPoolID);
        public Task<List<GetOneRentWithCarPoolDTO>> GetRentsByUserAsync(string userID);
        public Task<List<GetOneRentDTO>> GetRentsByDateForkAsync(DateForkDTO dateForkDTO);
        public Task<List<GetOneRentDTO>> GetRentsByEndDateAsync(DateTime date);
        public Task<List<GetOneRentDTO>> GetRentsByStartDateAsync(DateTime date);
        public Task<GetOneRentDTO?> GetRentByIdAsync(int rentID);
        /// <summary>
        /// Retrieves rental information for a vehicle based on the specified vehicle ID asynchronously.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle for which rental information is to be retrieved.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains a list of <see cref="GetOneRentByVehicleIdDTO"/> objects representing the rental information for the specified vehicle.
        /// </returns>
        public Task<List<GetOneRentByVehicleIdDTO>> GetRentByVehicleIdAsync(int vehicleId);

        public Task<List<GetOneRentDTO>> GetRentsByEndDateIdAsync(int id);

        public Task<List<GetOneRentDTO>> GetRentsByStartDateIdAsync(int id);
    }
}
