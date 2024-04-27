﻿using DTO.Dates;
using IRepositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class DateRepository : IDateRepository
    {
        private readonly DatabaseContext _context;
        public DateRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public async Task<GetOneDateDTO> CreateAsync(DateTime date)
        {
            EntityEntry<DatesClass> entityEntry = await this._context.AddAsync(new DatesClass{Date = date});
            await this._context.SaveChangesAsync();
            return new GetOneDateDTO{ Id = entityEntry.Entity.Id, Date = entityEntry.Entity.Date};
        }

        public async Task<GetOneDateDTO> GetDateByIdAsync(int id)
        {
            DatesClass? date = await this._context.Dates.FindAsync(id);
            if (date == null)
            {
                throw new Exception("Cette Id de date n'est pas enregistré !");
            }
            return new GetOneDateDTO { Id = date.Id, Date = date.Date };
        }

        public async Task<GetOneDateDTO> GetDateWithRentsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}