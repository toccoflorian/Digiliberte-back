using DTO.Localization;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Repositories;
using RepositoriesTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Tests
{
    [TestClass()]
    public class LocalizationRepositoryTests
    {
        private DbContextOptions<DatabaseContext> _options;
        [TestInitialize]
        public void Init()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            _options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseSqlite(connection)
               .Options;

            using (var context = new DatabaseContext(_options))
            {
                context.Database.EnsureCreated();
            }
        }


        [TestMethod()]
        public async Task GetAllLocalizationsAsyncTest1()
        {
            using (var context = new DatabaseContext(_options))
            {
                LocalizationRepository localizationRepo = new LocalizationRepository(context);
                // cree un faux Localization
                Localization localizationTest = new Localization
                {
                    Id = 1,
                    Latitude = 0.10000,
                    Longitude = 0.00001
                };
                Localization localizationTest1 = new Localization
                {
                    Id = 2,
                    Latitude = 0.10000,
                    Longitude = 0.00001
                };
                Localization localizationTest2 = new Localization
                {
                    Id = 3,
                    Latitude = 0.10000,
                    Longitude = 0.00001
                };
                Localization localizationTest3 = new Localization
                {
                    Id = 4,
                    Latitude = 0.10000,
                    Longitude = 0.00001
                };
                await context.Localizations.AddAsync(localizationTest);
                await context.Localizations.AddAsync(localizationTest1);
                await context.Localizations.AddAsync(localizationTest2);
                await context.Localizations.AddAsync(localizationTest3);

                context.SaveChanges();

                var localizationExpected = await localizationRepo.GetAllLocalizationsAsync();

                Assert.AreEqual(4, localizationExpected.Count);
            }
        }

        [TestMethod()]
        public async Task GetLocalizationsByCoordinatesAsyncTest1()
        {
            using (var context = new DatabaseContext(_options))
            {
                LocalizationRepository localizationRepo = new LocalizationRepository(context);
                // cree un faux Localization
                Localization localizationTest = new Localization
                {
                    Id = 1,
                    Latitude = 0.10000,
                    Longitude = 0.10001
                };
                Localization localizationTest1 = new Localization
                {
                    Id = 2,
                    Latitude = 0.10000,
                    Longitude = 0.20001
                };
                Localization localizationTest2 = new Localization
                {
                    Id = 3,
                    Latitude = 0.10000,
                    Longitude = 0.30001
                };
                Localization localizationTest3 = new Localization
                {
                    Id = 4,
                    Latitude = 43.631680288806564,
                    Longitude = 3.85614302780279
                };
                Localization localizationTest4 = new Localization
                {
                    Id = 5,
                    Latitude = 43.63283453445857,
                    Longitude = 3.8576543054404784
                };
                Localization localizationTest5 = new Localization
                {
                    Id = 6,
                    Latitude = 43.63214552954521,
                    Longitude = 3.856044335082078,
                };
                await context.Localizations.AddAsync(localizationTest);
                await context.Localizations.AddAsync(localizationTest1);
                await context.Localizations.AddAsync(localizationTest2);
                await context.Localizations.AddAsync(localizationTest3);
                await context.Localizations.AddAsync(localizationTest4);
                await context.Localizations.AddAsync(localizationTest5);


                context.SaveChanges();
                // 43.63283453445857, 3.8576543054404784
                // 43.631680288806564, 3.85614302780279
                var localizationExpected = await localizationRepo.GetLocalizationsByCoordinatesAsync(3.85614302780279, 43.631680288806564);


                Assert.AreEqual(2, localizationExpected.Count());
            }
        }

        [TestMethod()]
        public async Task CreateOneLocalizationAsyncTest2()
        {
            using (var context = new DatabaseContext(_options))
            {
                LocalizationRepository localizationRepo = new LocalizationRepository(context);
                // cree un faux Localization
                Localization localizationTest = new Localization
                {
                    Id = 1,
                    Latitude = 0.10000,
                    Longitude = 0.10001
                };
                Localization localizationTest1 = new Localization
                {
                    Id = 2,
                    Latitude = 0.10000,
                    Longitude = 0.20001
                };
                Localization localizationTest2 = new Localization
                {
                    Id = 3,
                    Latitude = 0.10000,
                    Longitude = 0.30001
                };
                Localization localizationTest3 = new Localization
                {
                    Id = 4,
                    Latitude = 43.631680288806564,
                    Longitude = 3.85614302780279
                };
                Localization localizationTest4 = new Localization
                {
                    Id = 5,
                    Latitude = 43.63209012326303,
                    Longitude = 3.85673969199671,
                };
                Localization localizationTest5 = new Localization
                {
                    Id = 6,
                    Latitude = 43.63214552954521,
                    Longitude = 3.856044335082078,
                };
                await context.Localizations.AddAsync(localizationTest);
                await context.Localizations.AddAsync(localizationTest1);
                await context.Localizations.AddAsync(localizationTest2);
                await context.Localizations.AddAsync(localizationTest3);
                await context.Localizations.AddAsync(localizationTest4);
                await context.Localizations.AddAsync(localizationTest5);

                context.SaveChanges();
                // 43.63283453445857, 3.8576543054404784
                // 43.631680288806564, 3.85614302780279
                LocalizationDTO localizationDTO = new LocalizationDTO
                {
                    Latitude = 43.63212012972853,
                    Logitude = 3.85400610630012,
                };

                var localizationExpected = await localizationRepo.CreateOneLocalizationAsync(localizationDTO);


                Assert.AreEqual(7, localizationExpected.Id);
            }
        }
        [TestMethod()]
        public async Task TestCoordinates()
        {
            double inputLatitude = 43.631680288806564; // My maison
            double inputLongitude = 3.85614302780279;
            double radius100 = 0.0009 / 1.5; // 100 m apparement
            double radius500 = 0.0055;
            double testLatitude = 43.63283453445857; // la rue d'a coté 100 m ?
            double testLongitude = 3.8576543054404784;

            double testLatitudeProche = 43.63289763377764; // a 50 metre ? 
            double testLongitudeProche = 3.8576280792099067;

            var distanceLongitude = Math.Abs(testLongitudeProche - testLongitude);
            var distanceLatitude = Math.Abs(testLatitudeProche - testLatitude);


            Assert.IsTrue(distanceLongitude <= radius100 && distanceLatitude <= radius100);



        }
        [TestMethod()]
        public void LocalizationRepositoryTest1()
        {
            throw new NotImplementedException();
        }


        [TestMethod()]
        public void GetAllLocalizationsWithVehiclesAsyncTest1()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetLocalizationAndCarsByCoordinatesAsyncTest1()
        {
            throw new NotImplementedException();
        }



        [TestMethod()]
        public void GetLocalizationAndCarpoolsAsyncTest1()
        {
            throw new NotImplementedException();
        }
    }
}

