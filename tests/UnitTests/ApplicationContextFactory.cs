using Masny.QRAnimal.Domain.Entities;
using Masny.QRAnimal.Domain.Enums;
using Masny.QRAnimal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

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

            var context = new ApplicationContext(options, true);

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

            context.Animals.Add(new Animal
            {
                Id = 1,
                UserId = "QWERTY1234567890",
                Kind = "Kind",
                Breed = "Breed",
                Gender = GenderTypes.None,
                Passport = "1234567890QWERTY",
                BirthDate = new DateTime(2000, 01, 01),
                Nickname = "Nickname",
                Features = "Features",
                IsDeleted = false,
                IsPublic = true
            });

            context.QRCodes.Add(new QRCode
            {
                Id = 1,
                Code = "Code",
                Created = new DateTime(2000, 01, 01),
                AnimalId = 1
            });

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
