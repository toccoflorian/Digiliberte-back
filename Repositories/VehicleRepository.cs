using DTO.Dates;
using DTO.Localization;
using DTO.Vehicles;
using IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enum;

namespace Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly DatabaseContext _context;
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
        public async Task<GetOneVehicleDTO?> CreateVehicleAsync(CreateVehicleDTO createVehicleDTO)
        {
            //Create the vehicle Based on CreateDTO
            Vehicle newVehicle = new Vehicle
            {
                BrandID = createVehicleDTO.BrandId,
                CategoryID = createVehicleDTO.CategoryId,
                ModelID = createVehicleDTO.ModelId,
                MotorizationID = createVehicleDTO.MotorizationId,
                ColorId = createVehicleDTO.ColorId,
                Immatriculation = createVehicleDTO.Immatriculation,
                PictureURL = createVehicleDTO.PictureUrl,
                StateID = 1,
                LocalizationID = createVehicleDTO.LocalizationId,

            };

            EntityEntry<Vehicle> entityEntry = await _context.Vehicles.AddAsync(newVehicle);
            Vehicle? vehicle = entityEntry.Entity;
            await _context.SaveChangesAsync();

            return await this.GetVehicleByImmatAsync(createVehicleDTO.Immatriculation);
        }


        /// <summary>
        /// Get a vehicle by immat , used to know if immat exists already
        /// </summary>
        /// <param name="immat">string</param>
        /// <returns> null or one Vehicle formated with GetOneVehicleDTO</returns>
        public async Task<GetOneVehicleDTO?> GetVehicleByImmatAsync(string immat)
        {
            GetOneVehicleDTO? vehicleDTO = await _context.Vehicles
                .Select(vehicle => 
                    new GetOneVehicleDTO
                    {
                        VehicleId = vehicle.Id,
                        BrandName = vehicle.Brand.Label,
                        ModelName = vehicle.Model.Label,
                        CategoryName=vehicle.Category.Label,
                        MotorizationName=vehicle.Motorization.Label,
                        StateName = vehicle.State.Label,
                        PictureUrl = vehicle.PictureURL,
                        Localization = new LocalizationDTO { Latitude = 1, Logitude = 2},       // données en dur !!!
                        SeatsNumber = vehicle.Category.SeatsNumber,
                        Color = vehicle.ColorId.ToString(),
                        CO2 = vehicle.Model.CO2,
                        ModelYear = vehicle.Model.Year,
                        Immatriculation = vehicle.Immatriculation
                    })
                .FirstOrDefaultAsync(vehicle => vehicle.Immatriculation.ToUpper() == immat.ToUpper());
            if(vehicleDTO != null)
            {
                // recupreration du nom de la couleur - Important
                vehicleDTO.Color = Enum.GetName(typeof(ColorEnum), int.Parse(vehicleDTO.Color));
            }
            return vehicleDTO;
        }


        public Task<UpdateOneVehicleDTO> UpdateVehicleByIdAsync(UpdateOneVehicleDTO updateOneVehicleDTO)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a vehicle by id
        /// </summary>
        /// <param name="id">string</param>
        /// <returns> null or one Vehicle formated with GetOneVehicleDTO</returns>
        public async Task<GetOneVehicleDTO?> GetVehicleByIdAsync(int id)
        {
            GetOneVehicleDTO? vehicleDTO = await this._context.Vehicles
                .Select(vehicle =>
                    new GetOneVehicleDTO
                    {
                        VehicleId = vehicle.Id,
                        BrandName = vehicle.Brand.Label,
                        ModelName = vehicle.Model.Label,
                        CategoryName = vehicle.Category.Label,
                        MotorizationName = vehicle.Motorization.Label,
                        StateName = vehicle.State.Label,
                        PictureUrl = vehicle.PictureURL,
                        Localization = new LocalizationDTO 
                        { 
                            Latitude = 1.5484584,        // données en dur !!!
                            Logitude = 2.4949445 
                        },
                        SeatsNumber = vehicle.Category.SeatsNumber,
                        Color = vehicle.ColorId.ToString(),
                        CO2 = vehicle.Model.CO2,
                        ModelYear = vehicle.Model.Year,
                        Immatriculation = vehicle.Immatriculation
                    })
                .FirstOrDefaultAsync(vehicle => vehicle.VehicleId == id);
                

            if(vehicleDTO != null)
            {
                // recupreration du nom de la couleur - Important
                vehicleDTO.Color = Enum.GetName(typeof(ColorEnum), int.Parse(vehicleDTO.Color));
            }
            return vehicleDTO;
        }

        public Task<List<GetOneVehicleDTO>> GetVehiclesByLocalizationAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteVehicleByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneVehicleDTO>> GetVehiclesByUserIdAsync(string userID)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneVehicleDTO>> GetVehiclesByStateAsync(int stateId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneVehicleDTO>> GetVehiclesByMotorizationAsync(int motorizationId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneVehicleDTO>> GetVehiclesByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneVehicleDTO>> GetVehiclesByBrandAsync(int brandId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneVehicleDTO>> GetVehiclesByModelAsync(int modelId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneVehicleDTO>> GetAllUnreservedVehiclesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneVehicleWithRentDTO>> GetAllReservedVehiclesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneVehicleDTO>> GetReservedVehicleByDatesAsync(DateForkDTO dateForkDTO)
        {
            throw new NotImplementedException();
        }

        
        }
        public Task<List<GetOneVehicleDTO>> GetAllVehiclesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetOneVehicleWithRentDTO> GetVehicleByIdWithRentAsync(int vehicleId)
        {
            throw new NotImplementedException();
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
