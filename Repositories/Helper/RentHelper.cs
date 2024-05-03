using DTO;
using DTO.Dates;
using DTO.Rent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Repositories.Helper
{
    /// <summary>
    /// Helper for rent, will be used in RentRepository
    /// </summary>
    public class RentHelper
    {
        private readonly DatabaseContext _context;
        public RentHelper(DatabaseContext context)
        {
            this._context = context;
        }
        /// <summary>
        /// Determines asynchronously if a specific vehicle is rented during a given time period.
        /// </summary>
        /// <param name="vehicleId">The unique identifier of the vehicle to check.</param>
        /// <param name="beginDate">The start date of the period to check for vehicle availability.</param>
        /// <param name="returnDate">The end date of the period to check for vehicle availability.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result is <c>true</c> if the vehicle is currently rented
        /// within the specified period, otherwise <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method checks for any rental records that overlap with the given date range. It considers various scenarios:
        /// an existing rental starting or ending during the specified period, an existing rental encompassing the entire period,
        /// or the specified period encompassing an existing rental.
        /// </remarks>
        public async Task<bool> isVehicleRentedAsync(int vehicleId, DateTime beginDate, DateTime returnDate)
        {
            // Get all rents for the specified vehicle
            bool isRented = await _context.Rents
                .AnyAsync(rent => rent.VehicleId == vehicleId
                               && (
                                   // Check for any overlap scenarios:
                                   // Existing rent starts during the new rent period
                                   (rent.StartDate.Date <= returnDate && rent.StartDate.Date >= beginDate) ||
                                   // Existing rent ends during the new rent period
                                   (rent.ReturnDate.Date >= beginDate && rent.ReturnDate.Date <= returnDate) ||
                                   // Existing rent envelops the new rent period
                                   (rent.StartDate.Date <= beginDate && rent.ReturnDate.Date >= returnDate) ||
                                   // New rent period envelops the existing rent
                                   (beginDate <= rent.StartDate.Date && returnDate >= rent.ReturnDate.Date)
                                  )
                       )
                ;
            //return true if rented, false if not
            return isRented;

        }

    }
}
