using DTO.Dates;
using IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class DateRepository : IDateRepository
    {
        private readonly DatabaseContext _context;
        public DateRepository(DatabaseContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Asynchronously creates a new date entry in the database.
        /// </summary>
        /// <param name="date">The date to be added to the database.</param>
        /// <returns>A task that represents the asynchronous operation, returning the newly created date information encapsulated in a <see cref="GetOneDateDTO"/> object.</returns>

        public async Task<GetOneDateDTO> CreateAsync(DateTime date)
        {
            EntityEntry<DatesClass> entityEntry = await this._context.Dates.AddAsync(new DatesClass{Date = date});
            await this._context.SaveChangesAsync();
            return new GetOneDateDTO{ Id = entityEntry.Entity.Id, Date = entityEntry.Entity.Date};
        }
        /// <summary>
        /// Retrieves a specific date by its ID asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the date to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation, returning the date information in a <see cref="GetOneDateDTO"/> object. Throws an exception if no date is found with the provided ID.</returns>
        /// <exception cref="Exception">Thrown when no date is found with the specified ID.</exception>

        public async Task<GetOneDateDTO?> GetDateByIdAsync(int id)
        {
            DatesClass? date = await this._context.Dates.FindAsync(id);
            if (date == null)
            {
                return null;
            }
            return new GetOneDateDTO { Id = date.Id, Date = date.Date };
        }
        /// <summary>
        /// Retrieves a date and its associated rental information by the date's ID asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the date for which to retrieve associated rents.</param>
        /// <returns>A task that represents the asynchronous operation. The implementation is not yet provided and will throw <see cref="NotImplementedException"/>.</returns>
        /// <exception cref="NotImplementedException">Indicates the method is not implemented.</exception>

        public async Task<GetOneDateDTO> GetDateWithRentsAsync(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Used to check if a date already exists in table , by 1 minutes
        /// </summary>
        /// <param name="targetDate">The date to check</param>
        /// <returns>the id of the date found or null</returns>
        public async Task<int?> GetCloseDateAsync(DateTime targetDate)
        {
            // Define the time range: one minute before and one minute after the target date
            var minDate = targetDate.AddMinutes(-0.5);
            var maxDate = targetDate.AddMinutes(0.5);

            // Query the database to find a close date
            var closeDate = await _context.Dates
                                          .Where(d => d.Date >= minDate && d.Date <= maxDate)
                                          .OrderBy(d => Math.Abs((d.Date - targetDate).Ticks)) // Order by closest date
                                          .FirstOrDefaultAsync();

            // Return the ID of the date if found, otherwise return null
            return closeDate?.Id;
        }
    }
}
