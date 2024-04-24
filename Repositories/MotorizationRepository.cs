﻿using DTO.Motorization;
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
    public class MotorizationRepository
    {
        public DatabaseContext Context { get; set; }
        public MotorizationRepository(DatabaseContext databaseContext)
        {
            this.Context = databaseContext;
        }
        /// <summary>
        /// Create a Motorization Repository
        /// </summary>
        /// <param name="createOneMotorizationDTO">Gives a DTO as parameter with only needed values</param>
        /// <returns>Return Get One motorization DTO</returns>
        public async Task<GetOneMotorizationDTO> CreateOneMotorizationAsync(CreateOneMotorizationDTO createOneMotorizationDTO)
        {
            Motorization newMotorization = new Motorization
            {
                Label = createOneMotorizationDTO.Name,


            };
            await Context.Motorizations.AddAsync(newMotorization);
            await Context.SaveChangesAsync();


            return new GetOneMotorizationDTO
            {
                Id = (await Context.Motorizations.FirstOrDefaultAsync(m=>m.Label == createOneMotorizationDTO.Name)).Id,
                Name = (await Context.Motorizations.FirstOrDefaultAsync(v => v.Label == createOneMotorizationDTO.Name)).Label,
            };
        }

        /// <summary>
        /// Get a motorization by name , used to know if name exists already
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public async Task<string> GetMotorizationByName(string Name)
        {
            return (await Context.Motorizations.FirstOrDefaultAsync(c => c.Label == Name)).Label;
        }
    }

}
