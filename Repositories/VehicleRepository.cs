using DTO.Dates;
using DTO.Localization;
using DTO.Motorization;
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

        /// <summary>
        /// Get a vehicle by motorization , used to know if immat exists already
        /// </summary>
        /// <param name="immat">string</param>
        /// <returns> null or one Vehicle formated with GetOneVehicleDTO</returns>
        public async Task<List<GetOneVehicleDTO>> GetVehiclesByMotorizationAsync(int motorizationId, int paginationIndex = 0, int pageSize = 10)
        {
            try
            {
                // Interrogez la base de données pour obtenir les véhicules avec l'ID de l'état spécifié
                var vehicles = await _context.Vehicles
                    .Where(v => v.MotorizationID == motorizationId)
                    .Include(v => v.Brand)
                    .Include(v => v.Model)
                    .Include(v => v.Category)
                    .Include(v => v.Motorization)
                    .Include(v => v.State)
                    .Skip(pageSize * paginationIndex)
                    .Take(pageSize)
                    .ToListAsync();

                // Convertissez les objets Vehicle en DTOs
                var vehiclesDTOs = vehicles.Select(v => new GetOneVehicleDTO
                {
                    VehicleId = v.Id,
                    BrandName = v.Brand?.Label,
                    ModelName = v.Model?.Label,
                    CategoryName = v.Category?.Label,
                    MotorizationName = v.Motorization?.Label,
                    StateName = v.State?.Label,
                    PictureUrl = v.PictureURL,
                    Localization = new LocalizationDTO
                    {
                        Latitude = 1.5484584,        // données en dur !!!
                        Logitude = 2.4949445
                    },
                    SeatsNumber = v.Category.SeatsNumber,
                    Color = v.ColorId.ToString(),
                    CO2 = v.Model.CO2,
                    ModelYear = v.Model.Year,
                    Immatriculation = v.Immatriculation

                }).ToList();

                return vehiclesDTOs;
            }
            catch (Exception ex)
            {
                // Gérer les exceptions appropriées
                throw new Exception("Failed to retrieve vehicles by state", ex);
            }
        }

        /// <summary>
        /// Get a vehicle by id , used to know if immat exists already
        /// </summary>
        /// <param name="immat">string</param>
        /// <returns> null or one Vehicle formated with GetOneVehicleDTO</returns>
        public async Task UpdateVehicleByIdAsync(UpdateOneVehicleDTO updateOneVehicleDTO)
        {
            Vehicle vehicle= (await this._context.Vehicles.FindAsync(updateOneVehicleDTO.VehicleId))!;
            if(updateOneVehicleDTO.StateId != null)
            {
                vehicle.StateID = (int) updateOneVehicleDTO.StateId;
            }
            if (updateOneVehicleDTO.PictureURL != null)
            {
                vehicle.PictureURL = (string)updateOneVehicleDTO.PictureURL;
            }
            await this._context.SaveChangesAsync();
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

        /// <summary>
        /// Get a vehicle by state , used to know if immat exists already
        /// </summary>
        /// <param name="immat">string</param>
        /// <returns> null or one Vehicle formated with GetOneVehicleDTO</returns>
        public async Task<List<GetOneVehicleDTO>> GetVehiclesByStateAsync(int stateId, int paginationIndex = 0, int pageSize = 10)
        {
            try
            {
                // Interrogez la base de données pour obtenir les véhicules avec l'ID de l'état spécifié
                var vehicles = await _context.Vehicles
                    .Where(v => v.StateID == stateId)
                    .Include(v => v.Brand)
                    .Include(v => v.Model)
                    .Include(v => v.Category)
                    .Include(v => v.Motorization)
                    .Include(v => v.State)
                    .Skip(pageSize * paginationIndex)
                    .Take(pageSize)
                    .ToListAsync();

                // Convertissez les objets Vehicle en DTOs
                var vehiclesDTOs = vehicles.Select(v => new GetOneVehicleDTO
                {
                    VehicleId = v.Id,
                    BrandName = v.Brand?.Label,
                    ModelName = v.Model?.Label,
                    CategoryName = v.Category?.Label,
                    MotorizationName = v.Motorization?.Label,
                    StateName = v.State?.Label,
                    PictureUrl = v.PictureURL,
                    Localization = new LocalizationDTO
                    {
                        Latitude = 1.5484584,        // données en dur !!!
                        Logitude = 2.4949445
                    },
                    SeatsNumber = v.Category.SeatsNumber,
                    Color = v.ColorId.ToString(),
                    CO2 = v.Model.CO2,
                    ModelYear = v.Model.Year,
                    Immatriculation = v.Immatriculation

                }).ToList();

                return vehiclesDTOs;
            }
            catch (Exception ex)
            {
                // Gérer les exceptions appropriées
                throw new Exception("Failed to retrieve vehicles by state", ex);
            }
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

        public Task<List<GetOneVehicleDTO>> GetUnreservedVehicleByDatesAsync(DateForkDTO dateForkDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetOneVehicleDTO>> GetAllVehiclesAsync(int paginationIndex = 0, int pageSize = 10)
        {
            try
            {
                // Interrogez la base de données pour obtenir toutes les vehicles
                List<Vehicle> vehicles = await _context.Vehicles
                    .Include(v => v.Brand)
                    .Include(v => v.Model)
                    .Include(v => v.Category)
                    .Include(v => v.Motorization)
                    .Include(v => v.State)
                    .Skip(pageSize * paginationIndex)
                    .Take(pageSize)
                    .ToListAsync();

                // Convertissez les objets Vehicle en DTOs
                List<GetOneVehicleDTO> vehiclesDTOs = vehicles.Select(v => new GetOneVehicleDTO
                {
                    VehicleId = v.Id,
                    BrandName = v.Brand.Label,
                    ModelName = v.Model.Label,
                    CategoryName = v.Category.Label,
                    MotorizationName = v.Motorization.Label,
                    StateName = v.State.Label,
                    PictureUrl = v.PictureURL,
                    Localization = new LocalizationDTO
                    {
                        Latitude = 1.5484584,        // données en dur !!!
                        Logitude = 2.4949445
                    },
                    SeatsNumber = v.Category.SeatsNumber,
                    Color = v.ColorId.ToString(),
                    CO2 = v.Model.CO2,
                    ModelYear = v.Model.Year,
                    Immatriculation = v.Immatriculation

                }).ToList();

                return vehiclesDTOs;
            }
            catch (Exception ex)
            {
                // Gérer les exceptions appropriées
                throw new Exception("Failed to retrieve vehicles", ex);
            }
        }

        public Task<GetOneVehicleWithRentDTO> GetVehicleByIdWithRentAsync(int vehicleId)
        {
            throw new NotImplementedException();
        }
    }
}
