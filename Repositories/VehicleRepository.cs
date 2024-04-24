using DTO.Vehicles;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class VehicleRepository
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

            };
            await Context.Vehicles.AddAsync(newVehicle);
            await Context.SaveChangesAsync();

            // Maps all the values of the created vehicle to getOneVehicleDTO as output
            //var vehicleInfo = await Context.Vehicles
            //    .Where(v => v.Immatriculation == createVehicleDTO.Immatriculation)
            //    .Include(v => v.Brand)
            //    .Include(v => v.Model)
            //    .Include(v => v.Category)
            //    .Include(v => v.Motorization)
            //    .Include(v => v.State) // Assuming there's a navigation property for State
            //    .Select(v => new GetOneVehicleDTO
            //    {
            //        VehicleId = v.Id,
            //        BrandName = v.Brand.Label,
            //        ModelName = v.Model.Label,
            //        CategoryName = v.Category.Label,
            //        MotorizationName = v.Motorization.Label,
            //        StateName = v.State.Label, // This assumes there is a direct relation to State
            //        CO2 = v.Model.CO2,
            //        ModelYear = v.Model.Year
            //    })
            //    .FirstOrDefaultAsync();    
            
            
            return await Context.Vehicles
                .Where(v => v.Immatriculation == createVehicleDTO.Immatriculation)
                .Include(v => v.Brand)
                .Include(v => v.Model)
                .Include(v => v.Category)
                .Include(v => v.Motorization)
                .Include(v => v.State) // Assuming there's a navigation property for State
                .Select(v => new GetOneVehicleDTO
                {
                    VehicleId = v.Id,
                    BrandName = v.Brand.Label,
                    ModelName = v.Model.Label,
                    CategoryName = v.Category.Label,
                    MotorizationName = v.Motorization.Label,
                    StateName = v.State.Label, // This assumes there is a direct relation to State
                    CO2 = v.Model.CO2,
                    ModelYear = v.Model.Year
                })
                .FirstOrDefaultAsync();


            //new GetOneVehicleDTO
            //{
            //    VehicleId = (await Context.Vehicles.FirstOrDefaultAsync(v => v.Immatriculation == createVehicleDTO.Immatriculation)).Id,
            //    BrandName = (await Context.Brands.FirstOrDefaultAsync(b => b.Id == createVehicleDTO.BrandId)).Label,
            //    ModelName = (await Context.Models.FirstOrDefaultAsync(m => m.Id == createVehicleDTO.ModelId)).Label,
            //    CategoryName = (await Context.Categories.FirstOrDefaultAsync(c => c.Id == createVehicleDTO.CategoryId)).Label,
            //    MotorizationName = (await Context.Motorizations.FirstOrDefaultAsync(m => m.Id == createVehicleDTO.MotorizationId)).Label,
            //    StateName = (await Context.States.FirstOrDefaultAsync(c => c.Id == 1)).Label,
            //    CO2 = (await Context.Models.FirstOrDefaultAsync(c => c.Id == createVehicleDTO.ModelId)).CO2,
            //    ModelYear = (await Context.Models.FirstOrDefaultAsync(c => c.Id == createVehicleDTO.ModelId)).Year
            //};


        }

        /// <summary>
        /// Get a vehicle by immat , used to know if immat exists already
        /// </summary>
        /// <param name="Immatriculation">string</param>
        /// <returns></returns>
        public async Task<string> GetVehicleByImmat(string Immatriculation)
        {
            return (await Context.Vehicles.FirstOrDefaultAsync(c => c.Immatriculation == Immatriculation)).Immatriculation;
        }
    }
}
