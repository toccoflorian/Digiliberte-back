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
        public VehicleRepository(DatabaseContext databaseContext)
        {
            this.Context = databaseContext;
        }
        public async Task CreateVehicleAsync(CreateVehicleDTO createVehicleDTO)
        {
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


            GetOneVehicleDTO getOneVehicleDTO = new GetOneVehicleDTO
            {
                BrandName = (await Context.Brands.FirstOrDefaultAsync(b => b.Id == createVehicleDTO.BrandId)).Label,
                ModelName = (await Context.Models.FirstOrDefaultAsync(m => m.Id == createVehicleDTO.ModelId)).Label,
                CategoryName = (await Context.Categories.FirstOrDefaultAsync(c => c.Id == createVehicleDTO.CategoryId)).Label,
                MotorizationName = (await Context.Motorizations.FirstOrDefaultAsync(m => m.Id == createVehicleDTO.MotorizationId)).Label,
                StateName = (await Context.States.FirstOrDefaultAsync(c => c.Id == 1)).Label,
                CO2 = (await Context.Models.FirstOrDefaultAsync(c => c.Id == createVehicleDTO.ModelId)).CO2,
                //ModelYear = (await Context.Models.FirstOrDefaultAsync(c => c.Id == createVehicleDTO.ModelId));
            };
        }

        public async Task<string> GetVehicleByImmat(string Immatriculation)
        {
            return (await Context.Vehicles.FirstOrDefaultAsync(c=>c.Immatriculation==Immatriculation)).Immatriculation;
        }
    }
}
