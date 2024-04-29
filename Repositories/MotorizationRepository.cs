using DTO.Dates;
using DTO.Motorization;
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
    public class MotorizationRepository : IMotorizationRepository
    {
        public DatabaseContext _context { get; set; }
        public MotorizationRepository(DatabaseContext databaseContext) // Dependancy injections
        {
            this._context = databaseContext;
        }

        /// <summary>
        /// Create a Motorization Repository
        /// </summary>
        /// <param name="createOneMotorizationDTO">Gives a DTO as parameter with only needed values</param>
        /// <returns>Return Get One motorization DTO</returns>

        public async Task<GetOneMotorizationDTO?> CreateOneMotorizationAsync(CreateOneMotorizationDTO createOneMotorizationDTO)
        {
            Motorization newMotorization = new Motorization
            {
                Label = createOneMotorizationDTO.Name,
            };
            await _context.Motorizations.AddAsync(newMotorization);
            await _context.SaveChangesAsync();

            return new GetOneMotorizationDTO
            {
                Id = (await _context.Motorizations.FirstOrDefaultAsync(m=>m.Label == createOneMotorizationDTO.Name)).Id,
                Name = newMotorization.Label,
            };
        }

        /// <summary>
        /// Get a motorization by name , used Label to know if  exists already
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public async Task<string?> GetMotorizationByName(string Name)
        {
            var motorization = await _context.Motorizations.FirstOrDefaultAsync(c => c.Label.ToUpper() == Name.ToUpper());

            if (motorization == null)
            {
                return null;
            }
            return motorization.Label;
        }

        public async Task<GetOneMotorizationDTO?> UpdateOneMotorizationByIdAsync(GetOneMotorizationDTO updatedMotorizationDTO)
        {
            // Recherchez le motorization existant dans la base de données en fonction de son ID
            var existingMotorization = await _context.Motorizations.FindAsync(updatedMotorizationDTO.Id);

            if (existingMotorization == null)
            {
                // Si le motorization n'est pas trouvé, vous pouvez choisir de retourner null ou de lever une exception
                // Ici, je choisis de retourner null
                throw new Exception("Id not found");
            }

            // Mettez à jour les propriétés du motorization existant avec les nouvelles valeurs
            existingMotorization.Label = updatedMotorizationDTO.Name;

            // Enregistrez les modifications dans la base de données
            _context.Update(existingMotorization);

            await _context.SaveChangesAsync();

            // Retournez le motorization mis à jour sous forme de DTO
            return new GetOneMotorizationDTO
            {
                Id = existingMotorization.Id,
                Name = existingMotorization.Label,
            };
        }
        ///// <summary>
        ///// Delete a motorization by Id , used Id to know if  exists
        ///// </summary>
        ///// <param name="Id"></param>
        ///// <returns>Bool , true si modifié, false si non modifié </returns>
        //public async Task<bool> DeleteOneMotorizationByIdAsync(int motorizationId)
        //{
        //    var motorizationToDelete = await _context.Motorizations.FindAsync(motorizationId);

        //    if (motorizationToDelete != null)
        //    {
        //        _context.Motorizations.Remove(motorizationToDelete);
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }

        //    return false;
        //    // Si le modèle n'existe pas, il n'y a rien à supprimer
        //}

        /// <summary>
        /// Get a motorization by Id , used Id to know if  exists already
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<GetOneMotorizationDTO?> GetOneMotorizationByIdAsync(int MotorizationId)
        {
            var getMotorization = await _context.Motorizations.FirstOrDefaultAsync(c => c.Id == MotorizationId);

            if (getMotorization != null)
            {
                GetOneMotorizationDTO? getOneMotorizationDTO = new GetOneMotorizationDTO
                {
                    Id = getMotorization.Id,
                    Name = getMotorization.Label,
                };
                return getOneMotorizationDTO;
            }
            else
            {
                return null;
            }

        }
        // Autres méthodes existantes ...

        /// <summary>
        /// Get all motorizations
        /// </summary>
        /// <returns>List of motorization DTOs</returns>
        public async Task<List<GetOneMotorizationDTO>> GetAllMotorizationsAsync()
        {
            // Interrogez la base de données pour obtenir toutes les motorisations
            var motorizations = await _context.Motorizations.ToListAsync();

            // Convertissez les objets Motorization en DTOs
            var motorizationDTOs = motorizations.Select(m => new GetOneMotorizationDTO
            {
                Id = m.Id,
                Name = m.Label
            }).ToList();

            return motorizationDTOs;
        }


    }

}
