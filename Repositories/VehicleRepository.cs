using DTO.Dates;
using DTO.Vehicles;
using IRepositories;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        public DatabaseContext _context { get; set; }
        public VehicleRepository(DatabaseContext databaseContext)  // Dependancy injections
        {
            this._context = databaseContext;
        }
        /// <summary>
        /// Get all Vehicles Repository
        /// </summary>
        /// <param name="getAllVehicleDTO">Gives a DTO as parameter with no needed values</param>
        /// <returns>Return Get One vehicle DTO</returns>
        public async Task<List<Vehicle>> GetAllVehiclesAsync()
        {
            return await _context.Vehicles.ToListAsync();
        }

        /// <summary>
        /// Create a Vehicle Repository
        /// </summary>
        /// <param name="createVehicleDTO">Gives a DTO as parameter with only needed values</param>
        /// <returns>Return Get One vehicle DTO</returns>
        public async Task<GetOneVehicleDTO?> CreateOneVehicleAsync(CreateVehicleDTO createVehicleDTO)
        {
            //Create the vehicle Based on CreateDTO
            Vehicle newVehicle = new Vehicle
            {
                BrandID = createVehicleDTO.BrandId,
                //VehicleID = createVehicleDTO.VehicleId,
                ModelID = createVehicleDTO.ModelId,
                MotorizationID = createVehicleDTO.MotorizationId,
                ColorId = createVehicleDTO.ColorId,
                Immatriculation = createVehicleDTO.Immatriculation,
                PictureURL = createVehicleDTO.PictureUrl,
                StateID = 1,
                LocalizationID = createVehicleDTO.LocalizationId,

            };
            await _context.Vehicles.AddAsync(newVehicle);
            await _context.SaveChangesAsync();
          
            return await _context.Vehicles
                .Where(v => v.Immatriculation == createVehicleDTO.Immatriculation)
                .Include(v => v.Brand)
                .Include(v => v.Model)
                //.Include(v => v.Vehicle)
                .Include(v => v.Motorization)
                .Include(v => v.Localization)
                .Include(v => v.ColorId)
                .Include(v => v.Category)
                .Include(v => v.Rents)
                .Include(v => v.PictureURL)
                .Include(v => v.State) // Assuming there's a navigation property for State
                .Select(v => new GetOneVehicleDTO
                {
                    VehicleId = v.Id,
                    BrandName = v.Brand.Label,
                    ModelName = v.Model.Label,
                    MotorizationName = v.Motorization.Label,
                    //Localization = v.Localization.Latitude,
                    //Color = v.ColorId,
                    //Rents = v.Rents,
                    StateName = v.State.Label, // This assumes there is a direct relation to State
                    CO2 = v.Model.CO2,
                    ModelYear = v.Model.Year
                })
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get a vehicle by immat , used to know if immat exists already
        /// </summary>
        /// <param name="Immatriculation">string</param>
        /// <returns></returns>
        public async Task<string?> GetOneVehicleByImatAsync(string Immat)
        {
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(c => c.Immatriculation.ToUpper() == Immat.ToUpper());

            if (vehicle == null)
            {
                return null;
            }
            return vehicle.Immatriculation;
        }
        
        /// <summary>
        /// Update une vehicle avec DTO en entree qui cherche sur l'id 
        /// </summary>
        /// <param name="updatedVehicleDTO"></param>
        /// <returns>renvois le DTO update si modifié</returns>
        /// <exception cref="Exception"> Renvois une exception si cat innexistante</exception>
        public async Task<GetOneVehicleDTO?> UpdateOneVehicleByIdAsync(int Id)
        {
            // Recherchez le vehicle existant dans la base de données en fonction de son ID
            var existingVehicle = await _context.Vehicles.FindAsync(Id);

            if (existingVehicle == null)
            {
                // Si le vehicle n'est pas trouvé, vous pouvez choisir de retourner null ou de lever une exception
                // Ici, je choisis de retourner null
                throw new Exception("Id not found");
            }

            // Mettez à jour les propriétés du vehicle existant avec les nouvelles valeurs
            //existingVehicle.Label = updatedVehicleDTO.Lab;
            //existingVehicle.SeatsNumber = updatedVehicleDTO.SeatsNumber;

            // Enregistrez les modifications dans la base de données
            _context.Update(existingVehicle);

            await _context.SaveChangesAsync();

            // Retournez le vehicle mis à jour sous forme de DTO
            return new GetOneVehicleDTO
            {
                //ID = existingVehicle.Id,
                //Name = existingVehicle.Label,
                //SeatsNumber = existingVehicle.SeatsNumber,
            };
        }
        public async Task<bool> DeleteOneVehicleByIdAsync(int Id)
        {
            var vehicleToDelete = await _context.Vehicles.FindAsync(Id);

            if (vehicleToDelete != null)
            {
                _context.Vehicles.Remove(vehicleToDelete);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
            // Si le modèle n'existe pas, il n'y a rien à supprimer
        }
    }


}
