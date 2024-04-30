using DTO.Dates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IDateRepository
    {
        /// <summary>
        /// Asynchronously creates a new date record in the repository and returns details of the created date.
        /// </summary>
        /// <param name="date">The date to create in the repository.</param>
        /// <returns>A task that represents the asynchronous create operation, which, upon completion,
        /// returns a <see cref="GetOneDateDTO"/> object containing the details of the newly created date.</returns>

        public Task<GetOneDateDTO> CreateAsync(DateTime date);
        /// <summary>
        /// Asynchronously retrieves a date by its unique identifier from the repository.
        /// </summary>
        /// <param name="id">The identifier of the date to retrieve.</param>
        /// <returns>A task that represents the asynchronous fetch operation, which, upon completion,
        /// returns a <see cref="GetOneDateDTO"/> object containing the details of the specified date.
        /// If no date with the specified ID exists, the method result is null.</returns>

        public Task<GetOneDateDTO> GetDateByIdAsync(int id);
        /// <summary>
        /// Asynchronously retrieves a date along with its associated rental details by the date's ID from the repository.
        /// </summary>
        /// <param name="id">The identifier of the date to retrieve, including rental details.</param>
        /// <returns>A task that represents the asynchronous fetch operation, which, upon completion,
        /// returns a <see cref="GetOneDateDTO"/> object containing the details of the date and its associated rents.
        /// If no such date exists, the method result is null.</returns>

        public Task<GetOneDateDTO> GetDateWithRentsAsync(int id);
        /// <summary>
        /// Searches for the closest date within one minute (before or after) of a specified target date and retrieves its ID.
        /// </summary>
        /// <param name="targetDate">The date to compare against existing dates in the repository.</param>
        /// <returns>A task that represents the asynchronous search operation, which, upon completion,
        /// returns the ID of the closest date if such a date is found within one minute of <paramref name="targetDate"/>;
        /// otherwise, returns null if no close date is found.</returns>

        public Task<int?> GetCloseDate(DateTime targetDate);
    }
}
