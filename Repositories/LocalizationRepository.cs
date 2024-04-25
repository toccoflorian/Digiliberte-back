using DTO.Localization;
using DTO.Vehicles;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    /// <summary>
    /// Repository to create localizations
    /// </summary>
    public class LocalizationRepository
    {
        public readonly DatabaseContext Context;
        public LocalizationRepository(DatabaseContext context) { this.Context = context; } // Dependency injections

        /// <summary>
        /// Outputs all localization with their DTO , no futher informations 
        /// </summary>
        /// <returns></returns>
        public async Task<List<LocalizationDTO>> GetAllLocalizationsAsync()
        {
            List<LocalizationDTO> localizationsList = await Context.Localizations.Select(loc => new LocalizationDTO
            {
                Logitude = loc.Longitude,
                Latitude = loc.Latitude,
            }).ToListAsync();

            return localizationsList;
        }
        /// <summary>
        /// Get ALL localization with all Vehicles ID, would be good to add pagination
        /// </summary>
        /// <returns>Return a Localization List with cars</returns>
        public async Task<List<LocalizationWithCarDTO>> GetAllLocalizationsWithVehiclesAsync()
        {
            List<LocalizationWithCarDTO> localizationWithCars = await Context.Localizations
                .Select(localization => new LocalizationWithCarDTO
                {
                    Longitude = localization.Longitude,
                    Latitude = localization.Latitude,
                    Vehicles = Context.Vehicles.Select(v => new VehicleSimpleIdDTO
                    {
                        Id = v.Id,
                    }
                    ).ToList(),
                }).ToListAsync();

            return localizationWithCars;
        }

        /// <summary>
        /// Return all Localizations in Db with cars at this position or close by 100 Meters (radius = 0.0009)
        /// </summary>
        /// <param name="inputLongitude">Longitude To choose</param>
        /// <param name="inputLatitude">Latitude To choose</param>
        /// <param name="radius">Default value is 0.0009, approximatively 100 meters</param>
        /// <returns>Reutrn a List</returns>
        public async Task<List<LocalizationWithCarDTO>?> GetLocalizationAndCarsByCoordinatesAsync(double inputLongitude, double inputLatitude, double radius = 0.0009)
        {
            // Check les localizations en fonction du radius 
            var localizationWithCars = await Context.Localizations
            .Where(localization =>
                Math.Abs(localization.Longitude - inputLongitude) <= radius &&
                Math.Abs(localization.Latitude - inputLatitude) <= radius)   // condition on the radius, will output the ones 100 meters arounds only 
            .Select(localization => new LocalizationWithCarDTO
            {
                Longitude = localization.Longitude,
                Latitude = localization.Latitude,
                Vehicles = localization.Vehicles != null ? localization.Vehicles.Select(v => new VehicleSimpleIdDTO    // LONG CODE POUR EVITER LE RENVOIS DE NULL QUI
                {                                                                                                      // qui pourrait faire planter tout      
                    Id = v.Id,
                }).ToList() : new List<VehicleSimpleIdDTO>(),
            }).ToListAsync();

            return localizationWithCars;
        }

        /// <summary>
        /// Helps to get localizations close to a certain point, might be used in Creating Localization process, to check if some exists , default area of  10 metters
        /// </summary>
        /// <param name="inputLongitude">Longitude</param>
        /// <param name="inputLatitude">Latitude</param>
        /// <param name="radius">default is 0.000135 around 10 meters</param>
        /// <returns>List of LocalizationIdDTO</returns>
        public async Task<List<LocalizationIdDTO>?> GetLocalizationsByCoordinatesAsync(double inputLongitude, double inputLatitude, double radius = 0.000135)
        {
            List<LocalizationIdDTO> localizations = await Context.Localizations
                .Where(localization => 
                Math.Abs(localization.Longitude - inputLongitude) <= radius && 
                Math.Abs(localization.Latitude -inputLatitude) <= radius)   // Check the localization on the radius ! 
                .Select(dto => new LocalizationIdDTO
                {
                    Id= dto.Id,
                    Latitude = dto.Latitude,
                    Longitude = dto.Longitude,
                }).ToListAsync();

            return localizations; // Returning the list (might often be only one) of localizations that are really close to the selected point ! 
        }

        /// <summary>
        /// Create A new Localization, will be used when creating a car or Carpool, if the localization doesn't exists, it creates it, if not , it returns the closest localization
        /// </summary>
        /// <param name="localizationDTO">Take inputs as Longitude and Latitudes</param>
        /// <returns></returns>
        public async Task<LocalizationIdDTO> CreateOneLocalizationAsync(LocalizationDTO localizationDTO)
        {
            // Needs to check if the Localization exists already : 
            // if this makes no sense we can change radius default or manually ! 
            List<LocalizationIdDTO>? closeLocalizationsList = await GetLocalizationsByCoordinatesAsync(inputLongitude: localizationDTO.Logitude, inputLatitude: localizationDTO.Latitude);


            //if A lot of close Localizations we want to take the closest one, and assign our new localization to it 
            //if no existing localizations, we create a new one !

            if (closeLocalizationsList == null || closeLocalizationsList.Count == 0) // check if list is empty or null, is the same but handle errors
            {
                Localization newLocalization = new Localization
                {
                    Longitude = localizationDTO.Logitude,
                    Latitude = localizationDTO.Latitude,
                };

                await Context.Localizations.AddAsync(newLocalization);
                await Context.SaveChangesAsync();

                List<LocalizationIdDTO>? outputList = await GetLocalizationsByCoordinatesAsync(inputLongitude: localizationDTO.Logitude, inputLatitude: localizationDTO.Latitude, 0.00005);
                LocalizationIdDTO? outPut = outputList.FirstOrDefault();

                return outPut;
            }
            else   // Now we need to ouput the closest one if not null ! 
            {
                List<double> distances = new List<double>(); // Create a list of averagedistances
                foreach (var item in closeLocalizationsList)
                {
                    distances.Add((Math.Abs(item.Longitude - localizationDTO.Logitude) + Math.Abs(item.Latitude - localizationDTO.Latitude)) / 2); // calcule distance moyenne 
                }

                double minDistance = distances.Min(); // check the minimum distance in this list
                int minIndex = distances.IndexOf(minDistance); // Get its index

                var closestLocalizationDTO = closeLocalizationsList[minIndex]; // Get the corresponding item

                return closestLocalizationDTO; // return the item
            }

           


        }
    }
}
