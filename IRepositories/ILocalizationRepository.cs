using DTO.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    /// <summary>
    /// Interface for ILocalizationRepository
    /// </summary>
    public interface ILocalizationRepository
    {
        /// <summary>
        /// Get All Localizations with pagination parameters, default are 0 for index and 10 for pageSize
        /// </summary>
        /// <param name="paginationIndex">Index of starting, 0 will be 0 , 1 would be 10(pageSize), 2 would be 20(pageSize*index)</param>
        /// <param name="pageSize">Number of items to display, default is 10</param>
        /// <returns>A list of LocalizationDTO</returns>
        public Task<List<LocalizationDTO>> GetAllLocalizationsAsync(int paginationIndex = 0, int pageSize = 10);
        /// <summary>
        /// Get ALL localization with all Vehicles ID, with Pagination
        /// </summary>
        /// <param name="pageSize">Page size number, default is 10</param>
        /// <param name="paginationIndex">Index of starting, 0 will be 0 , 1 would be 10(pageSize), 2 would be 20(pageSize*index)</param>
        /// <returns>Return a Localization List with cars</returns>
        public Task<List<LocalizationWithCarDTO>> GetAllLocalizationsWithVehiclesAsync(int paginationIndex = 0, int pageSize = 10);
        /// <summary>
        /// Return all Localizations in Db with cars at this position or close by 500 Meters (radius = 0.0045)
        /// </summary>
        /// <param name="inputLongitude">Longitude To choose</param>
        /// <param name="inputLatitude">Latitude To choose</param>
        /// <param name="radius">Default value is 0.0009, approximatively 100 meters</param>
        /// <returns>Reutrn a List</returns>
        public Task<List<LocalizationWithCarDTO>?> GetLocalizationAndCarsByCoordinatesAsync(double inputLongitude, double inputLatitude, double radius = 0.0045);
        /// <summary>
        /// Helps to get localizations close to a certain point, might be used in Creating Localization process, to check if some exists , default area of  10 metters
        /// </summary>
        /// <param name="inputLongitude">Longitude</param>
        /// <param name="inputLatitude">Latitude</param>
        /// <param name="radius">default is 0.000135 around 10 meters</param>
        /// <returns>List of LocalizationIdDTO</returns>
        public Task<List<LocalizationIdDTO>?> GetLocalizationsByCoordinatesAsync(double inputLongitude, double inputLatitude, double radius = 0.0009);
        /// <summary>
        /// Create A new Localization, will be used when creating a car or Carpool, if the localization doesn't exists, it creates it, if not , it returns the closest localization, default is 100meters
        /// </summary>
        /// <param name="localizationDTO">Take inputs as Longitude and Latitudes</param>
        /// <returns></returns>
        public Task<LocalizationIdDTO> CreateOneLocalizationAsync(LocalizationDTO localizationDTO);
        /// <summary>
        /// Get Localizations List with their carpoools starting or ending here, giving pagination index and page size, starting at 0
        /// </summary
        /// <param name="pageSize">number of Items to display, default is 10</param>
        /// <param name="paginationIndex">Index of starting, 0 will be 0 , 1 would be 10(pageSize), 2 would be 20(pageSize*index)</param>
        /// <param name="localizationDTO">Coordinates Long and Lat</param>
        /// <param name="radius">double, radius around the coordinates , default is 0.0009 (around 100m)</param>
        /// <returns>Return a list that can be empty</returns>
        public Task<List<LocalizationWithCarpoolDTO>?> GetLocalizationAndCarpoolsAsync(LocalizationDTO localizationDTO, int paginationIndex = 0, int pageSize = 10, double radius = 0.0009);
    }
}
