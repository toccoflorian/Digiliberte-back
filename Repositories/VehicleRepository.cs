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
        public DatabaseContext Context { get; set; }
        public VehicleRepository(DatabaseContext databaseContext)  // Dependancy injections
        {
            this.Context = databaseContext;
        }

        /// <summary>
        /// Create a Vehicle Repository
        /// </summary>
        /// <param name="createVehicleDTO">Gives a DTO as parameter with only needed values</param>
        /// <returns>Return Get One vehicle DTO</returns>
        public async Task<GetOneVehicleDTO> CreateVehicleAsync(CreateVehicleDTO createVehicleDTO)
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

            EntityEntry<Vehicle> entityEntry = await Context.Vehicles.AddAsync(newVehicle);
            Vehicle? vehicle = entityEntry.Entity;
            await Context.SaveChangesAsync();

            return await this.GetVehicleByImmatAsync(createVehicleDTO.Immatriculation);
        }


        /// <summary>
        /// Get a vehicle by immat , used to know if immat exists already
        /// </summary>
        /// <param name="immat">string</param>
        /// <returns> null or one Vehicle formated with GetOneVehicleDTO</returns>
        public async Task<GetOneVehicleDTO?> GetVehicleByImmatAsync(string immat)
        {
            GetOneVehicleDTO? vehicleDTO = await Context.Vehicles
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
                        Localization = new LocalizationDTO { Latitude = 1, Logitude = 2},
                        SeatsNumber = vehicle.Category.SeatsNumber,
                        Color = vehicle.ColorId.ToString(),
                        CO2 = vehicle.Model.CO2,
                        ModelYear = vehicle.Model.Year,
                        Immatriculation = vehicle.Immatriculation
                    })
                .FirstOrDefaultAsync(vehicle => vehicle.Immatriculation.ToUpper() == immat.ToUpper());
            // recupreration du nom de la couleur - Important
            vehicleDTO.Color = Enum.GetName(typeof(ColorEnum), int.Parse(vehicleDTO.Color));
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
        public async Task<GetOneVehicleDTO> GetVehicleByIdAsync(int id)
        {
            GetOneVehicleDTO? vehicleDTO = await Context.Vehicles
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
                        Localization = new LocalizationDTO { Latitude = 1, Logitude = 2 },
                        SeatsNumber = vehicle.Category.SeatsNumber,
                        Color = vehicle.ColorId.ToString(),
                        CO2 = vehicle.Model.CO2,
                        ModelYear = vehicle.Model.Year,
                        Immatriculation = vehicle.Immatriculation
                    })
                .FirstOrDefaultAsync(vehicle => vehicle.VehicleId == id);

            if(vehicleDTO == null)
            {
                throw new Exception("Aucun véhicule touvé avec cette id !");
            }
            // recupreration du nom de la couleur - Important
            vehicleDTO.Color = Enum.GetName(typeof(ColorEnum), int.Parse(vehicleDTO.Color));
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

        public Task<List<GetOneVehicleDTO>> GetUnreservedVehicleByDatesAsync(DateForkDTO dateForkDTO)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOneVehicleDTO>> GetAllVehiclesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetOneVehicleWithRentDTO> GetVehicleByIdWithRentAsync(int vehicleId)
        {
            throw new NotImplementedException();
        }
    }
}
