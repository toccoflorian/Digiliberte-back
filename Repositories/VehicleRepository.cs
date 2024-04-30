using DTO.Dates;
using DTO.Localization;
using DTO.Motorization;
using DTO.Rent;
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
using static System.Net.Mime.MediaTypeNames;

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
        /// <param name="createVehicleDTO"></param>
        /// <returns></returns>
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
        /// <param name="immat"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<GetOneVehicleDTO?> GetVehicleByImmatAsync(string immat)
        {
            GetOneVehicleDTO? vehicleDTO = await _context.Vehicles
                .Select(v =>
                    new GetOneVehicleDTO
                    {
                        VehicleId = v.Id,
                        BrandName = v.Brand.Label,
                        ModelName = v.Model.Label,
                        CategoryName = v.Category.Label,
                        MotorizationName = v.Motorization.Label,
                        StateName = v.State.Label,
                        PictureUrl = v.PictureURL,
                        Localization = new LocalizationDTO { Latitude = 1, Logitude = 2 },       // données en dur !!!
                        SeatsNumber = v.Category.SeatsNumber,
                        Color = v.ColorId.ToString(),
                        CO2 = v.Model.CO2,
                        ModelYear = v.Model.Year,
                        Immatriculation = v.Immatriculation
                    })
                .FirstOrDefaultAsync(vehicle => vehicle.Immatriculation.ToUpper() == immat.ToUpper());
            if (vehicleDTO != null)
            {
                // recupreration du nom de la couleur - Important
                vehicleDTO.Color = Enum.GetName(typeof(ColorEnum), int.Parse(vehicleDTO.Color));
            }
            if(immat == null)
            {
                 throw new Exception("A correct immatriculation must be provided");
            }
            if(immat == "")
            {
                throw new Exception("An immatriculation must be provided");
            }
            return vehicleDTO;
        }

        /// <summary>
        /// Get a vehicle by motorization , used to know if immat exists already
        /// </summary>
        /// <param name="motorizationId"></param>
        /// <param name="paginationIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<GetOneVehicleDTO>> GetVehiclesByMotorizationAsync(int motorizationId, int paginationIndex = 0, int pageSize = 10)
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
            if (vehiclesDTOs != null  && paginationIndex < 0)
            {
                // recupreration du nom de la couleur - Important
                //vehiclesDTOs.Color = Enum.GetName(typeof(ColorEnum), int.Parse(vehiclesDTOs.Color));
            }
            if(motorizationId == ' ')
            {
                throw new Exception("A correct motorizationId must be provided");
            }
            if(motorizationId < 0)
            {
                throw new Exception("A correct motorizationId must be provided");
            }
            if(paginationIndex < 0)
            {
                throw new Exception("PaginationIndex Error");
            }
            if (pageSize < 0)
            {
                throw new Exception("PageSize Error");
            }
            return vehiclesDTOs;
        }

        /// <summary>
        /// Update a vehicle by id
        /// </summary>
        /// <param name="updateOneVehicleDTO"></param>
        /// <returns></returns>
        public async Task UpdateVehicleByIdAsync(UpdateOneVehicleDTO updateOneVehicleDTO)
        {
            Vehicle vehicle = (await this._context.Vehicles.FindAsync(updateOneVehicleDTO.VehicleId))!;
            if (updateOneVehicleDTO.StateId != null)
            {
                vehicle.StateID = (int)updateOneVehicleDTO.StateId;
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
        /// <param name="id"></param>
        /// <returns></returns>
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


            if (vehicleDTO != null && id < 0 && id == null)
            {
                // recupreration du nom de la couleur - Important
                vehicleDTO.Color = Enum.GetName(typeof(ColorEnum), int.Parse(vehicleDTO.Color));
            }
            if (id < 0)
            {
                throw new Exception("A correct Id should be provided");
            }
            if (id == ' ')
            {
                throw new Exception("A Id should be provided");
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
        /// Get a vehicle by state , used to know if state exists already
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="paginationIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<GetOneVehicleDTO>> GetVehiclesByStateAsync(int stateId, int paginationIndex = 0, int pageSize = 10)
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
            var vehicleDTO = vehicles.Select(v => new GetOneVehicleDTO
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
            if (vehicleDTO == null && stateId < 0 && stateId == null)
            {
                throw new NotImplementedException();
            }
            if (stateId < 0)
            {
                throw new Exception("A correct stateId should be provided");
            }
            if (stateId == ' ')
            {
                throw new Exception("A correct stateId should be provided");
            }
            if (paginationIndex < 0)
            {
                throw new Exception("PaginationIndex Error");
            }
            if (pageSize < 0)
            {
                throw new Exception("PageSige Error");
            }
            return vehicleDTO;
        }


        public Task<List<GetOneVehicleDTO>> GetVehiclesByMotorizationAsync(int motorizationId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a vehicle by category , used to know if category exists already
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="paginationIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<GetOneVehicleDTO>> GetVehiclesByCategoryAsync(int categoryId, int paginationIndex = 0, int pageSize = 10)
        {
                // Interrogez la base de données pour obtenir les véhicules avec l'ID de l'état spécifié
                var vehicles = await _context.Vehicles
                    .Where(v => v.CategoryID == categoryId)
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

            if(vehiclesDTOs == null && categoryId < 0 && categoryId == null) { 
                throw new NotImplementedException();
            }
            if (categoryId < 0)
            {
                throw new Exception("A correct categoryId should be provided");
            }
            if (categoryId == ' ')
            {
                throw new Exception("A categoryId should be provided");
            }
            if (paginationIndex < 0)
            {
                throw new Exception("PaginationIndex Error");
            }
            if (pageSize < 0)
            {
                throw new Exception("PageSige Error");
            }
            return vehiclesDTOs;
        }

        /// <summary>
        /// Get a vehicle by Brand , used to know if brand exists already
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="paginationIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<GetOneVehicleDTO>> GetVehiclesByBrandAsync(int brandId, int paginationIndex = 0, int pageSize = 10)
        {
            // Interrogez la base de données pour obtenir les véhicules avec l'ID de l'état spécifié
            var vehicles = await _context.Vehicles
                .Where(v => v.BrandID == brandId)
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

            if(vehiclesDTOs == null)
            {
                throw new NotImplementedException();
            }
            if (brandId <0)
            {
                throw new Exception("A correct brandId should be provided");
            }
            if (brandId ==' ')
            {
                throw new Exception("A brandlId should be provided");
            }
            if (paginationIndex < 0)
            {
                throw new Exception("PaginationIndex Error");
            }
            if (pageSize < 0)
            {
                throw new Exception("PageSige Error");
            }
            return vehiclesDTOs;
        }

        /// <summary>
        /// Get a vehicle by model , used to know if model exists already
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="paginationIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<GetOneVehicleDTO>> GetVehiclesByModelAsync(int modelId, int paginationIndex = 0, int pageSize = 10)
        {

            // Interrogez la base de données pour obtenir les véhicules avec l'ID de l'état spécifié
            var vehicles = await _context.Vehicles
                .Where(v => v.ModelID == modelId)
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
            if (vehiclesDTOs== null &&  paginationIndex <0)
            {
                throw new NotImplementedException();
            }
            if(modelId < 0)
            {
                throw new Exception("A correct modelId should be provided");
            }
            if (modelId == ' ')
            {
                throw new Exception("A modelId should be provided");
            }
            if (paginationIndex < 0)
            {
                throw new Exception("PaginationIndex Error");
            }
            if (pageSize < 0)
            {
                throw new Exception("PageSige Error");
            }
            return vehiclesDTOs;
            
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

        /// <summary>
        /// Get All Vehicles
        /// </summary>
        /// <param name="paginationIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<GetOneVehicleDTO>> GetAllVehiclesAsync(int paginationIndex = 0, int pageSize = 10)
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
            if(vehiclesDTOs == null)
            {
                throw new Exception("No Vehicle found");
            }
            if(paginationIndex < 0 )
            {
                throw new Exception( "PaginationIndex Error");
            }
            if(pageSize < 0)
            {
                throw new Exception("PageSige Error");
            }
            return vehiclesDTOs;
            
        }

        //public async Task<List<GetOneVehicleDTO>> GetVehicleByIdWithRentAsync(int vehicleId)
        //{
        //        var vehiclesWithRent = await _context.Vehicles
        //     .Select(v => new GetOneVehicleWithRentDTO
        //     {
        //         VehicleId = v.Id,
        //         BrandName = v.Brand.Label,
        //         ModelName = v.Model.Label,
        //         CategoryName = v.Category.Label,
        //         MotorizationName = v.Motorization.Label,
        //         StateName = v.State.Label,
        //         PictureUrl = v.PictureURL,
        //         Localization = new LocalizationDTO
        //         {
        //             Latitude = 1.5484584,        // données en dur !!!
        //             Logitude = 2.4949445
        //         },
        //         SeatsNumber = v.Category.SeatsNumber,
        //         Color = v.ColorId.ToString(),
        //         CO2 = v.Model.CO2,
        //         ModelYear = v.Model.Year,
        //         Immatriculation = v.Immatriculation,
                 
        //         Rents = v.Rents.Select(r => new GetOneRentDTO
        //         {
        //             Id = r.Id,
        //             UserId=r.User.Id,
        //             VehiceId=r.VehicleId,
        //             VehicleInfo=r.Vehicle.State.ToString(),
        //             Immatriculation=r.Vehicle.Immatriculation,
        //             StartDate=r.StartDate.Date,
        //             ReturnDate=r.ReturnDate.Date,
        //             UserFirstname=r.User.Firstname,
        //             UserLastname=r.User.Lastname,
        //         }).ToList()
        //     })
        //             .ToListAsync();

        //        return vehiclesWithRent;
        }
    }

