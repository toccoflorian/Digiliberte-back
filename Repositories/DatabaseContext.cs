using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    /// <summary>
    /// Used for database context , all DB sets are done here
    /// </summary>
    public class DatabaseContext : IdentityDbContext<AppUser>
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<CarPool> CarPools { get; set; }
        public DbSet<CarPoolPassenger> CarPoolPassengers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DatesClass> Dates { get; set; }
        public DbSet<Localization> Localizations { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Motorization> Motorizations { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarPool>(entity =>
                entity
                    .HasOne(d => d.StartLocalization)
                    .WithMany(p => p.StartLocalizationCarPools)
                    .HasForeignKey(d => d.StartLocalizationID)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK__CarPool__StartLocalizationId__LocalizationId"));

            modelBuilder.Entity<CarPool>(entity =>
                entity
                    .HasOne(d => d.EndLocalization)
                    .WithMany(p => p.EndLocalizationCarPools)
                    .HasForeignKey(d => d.EndLocalizationID)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK__CarPool__EndLocalizationID__LocalizationId"));

            modelBuilder.Entity<CarPool>(entity =>
                entity
                    .HasOne(d => d.StartDate)
                    .WithMany(p => p.StartDateCarPools)
                    .HasForeignKey(d => d.StartDateID)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK__CarPool__StartDateID__DatesId"));

            modelBuilder.Entity<CarPool>(entity =>
                entity
                    .HasOne(d => d.EndDate)
                    .WithMany(p => p.EndDateCarPools)
                    .HasForeignKey(d => d.EndDateID)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK__CarPool__EndDateID__DatesId"));

            modelBuilder.Entity<Rent>(entity =>
                entity
                    .HasOne(d => d.StartDate)
                    .WithMany(p => p.StartDateRents)
                    .HasForeignKey(d => d.StartDateID)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK__Rent__StartDateID__DatesId"));

            modelBuilder.Entity<Rent>(entity =>
                entity
                    .HasOne(d => d.ReturnDate)
                    .WithMany(p => p.EndDateRents)
                    .HasForeignKey(d => d.ReturnDateID)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK__Rent__ReturnDateID__DatesId"));

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            base.OnConfiguring(optionsBuilder);
        }

        public DatabaseContext(DbContextOptions optionsBuilder) : base(optionsBuilder) { }
    }

}
