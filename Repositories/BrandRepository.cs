﻿using DTO.Brands;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    /// <summary>
    /// Class for Brand Repositories
    /// </summary>
    public class BrandRepository
    {
        public DatabaseContext Context { get; set; }
        public BrandRepository(DatabaseContext databaseContext)  // Dependancy injections
        {
            this.Context = databaseContext;
        }

        /// <summary>
        /// Create a brand based on Creation DTO, Name string
        /// </summary>
        /// <param name="createBrandDTO"></param>
        /// <returns>GetOneBrandDTO</returns>
        public async Task<GetOneBrandDTO> CreateBrandAsync(CreateOneBrandDTO createBrandDTO)
        {
            Brand newBrand = new Brand
            {
                Label = createBrandDTO.Name
            };

            Context.Brands.Add(newBrand);
            await Context.SaveChangesAsync();

            return new GetOneBrandDTO
            {
                Id = newBrand.Id,
                Name = newBrand.Label
            };
        }

    }
}