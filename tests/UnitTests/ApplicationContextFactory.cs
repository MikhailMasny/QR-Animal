using Masny.QRAnimal.Domain.Entities;
using Masny.QRAnimal.Domain.Enums;
using Masny.QRAnimal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Masny.QRAnimal.UnitTests
{
    /// <summary>
    /// Фабрика для заполнения контекста тестовой базы данных (InMemory) данными.
    /// </summary>
    public static class ApplicationContextFactory
    {
        /// <summary>
        /// Создать базу данных.
        /// </summary>
        /// <returns>Контекст тестовой базы данных (InMemory).</returns>
        public static ApplicationContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationContext(options);

            context.Database.EnsureCreated();

            SeedSampleData(context);

            return context;
        }

        /// <summary>
        /// Заполнить БД начальными данными.
        /// </summary>
        /// <param name="context">Контекст тестовой базы данных (InMemory).</param>
        public static void SeedSampleData(ApplicationContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));

            var animals = new List<Animal>
            {
                new Animal
                {
                    Id = 1,
                    UserId = "QWERTY1234567890_One",
                    Kind = "Kind_One",
                    Breed = "Breed_One",
                    Gender = GenderTypes.None,
                    Passport = "1234567890QWERTY_One",
                    BirthDate = new DateTime(2001, 01, 01),
                    Nickname = "Nickname_One",
                    Features = "Features_One",
                    IsDeleted = false,
                    IsPublic = true
                },

                new Animal
                {
                    Id = 2,
                    UserId = "QWERTY1234567890_Two",
                    Kind = "Kind_Two",
                    Breed = "Breed_Two",
                    Gender = GenderTypes.None,
                    Passport = "1234567890QWERTY_Two",
                    BirthDate = new DateTime(2002, 01, 01),
                    Nickname = "Nickname_Two",
                    Features = "Features_Two",
                    IsDeleted = false,
                    IsPublic = false
                },

                new Animal
                {
                    Id = 3,
                    UserId = "QWERTY1234567890_Three",
                    Kind = "Kind_Three",
                    Breed = "Breed_Three",
                    Gender = GenderTypes.None,
                    Passport = "1234567890QWERTY_Three",
                    BirthDate = new DateTime(2003, 01, 01),
                    Nickname = "Nickname_Three",
                    Features = "Features_Three",
                    IsDeleted = true,
                    IsPublic = false
                }
            };

            var qrcodes = new List<QRCode>
            {
                new QRCode
                {
                    Id = 1,
                    Code = "Code",
                    Created = new DateTime(2001, 01, 01),
                    AnimalId = 1
                },

                new QRCode
                {
                    Id = 2,
                    Code = "Code_Two",
                    Created = new DateTime(2002, 01, 01),
                    AnimalId = 2
                },

                new QRCode
                {
                    Id = 3,
                    Code = "Code_Three",
                    Created = new DateTime(2003, 01, 01),
                    AnimalId = 3
                }
            };

            context.Animals.AddRange(animals);
            context.QRCodes.AddRange(qrcodes);

            context.SaveChanges();
        }

        /// <summary>
        /// Разрушить контекст БД.
        /// </summary>
        /// <param name="context">Контекст тестовой БД (InMemory).</param>
        public static void Destroy(ApplicationContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));

            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
