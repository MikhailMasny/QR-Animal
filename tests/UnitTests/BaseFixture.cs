using AutoMapper;
using Masny.QRAnimal.Application.Mapping;
using Masny.QRAnimal.Infrastructure.Persistence;
using Masny.QRAnimal.UnitTests;
using System;

namespace UnitTests
{
    /// <summary>
    /// Реализация базового приспособления.
    /// </summary>
    public class BaseFixture : IDisposable
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public BaseFixture()
        {
            Context = ApplicationContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AnimalProfile>();
                cfg.AddProfile<QRCodeProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }

        /// <summary>
        /// Контекст тестовой базы данных (InMemory).
        /// </summary>
        public ApplicationContext Context { get; }

        /// <summary>
        /// AutoMapper для DTO и основных моделей.
        /// </summary>
        public IMapper Mapper { get; }

        /// <summary>
        /// Разрушить контекст.
        /// </summary>
        public void Dispose()
        {
            ApplicationContextFactory.Destroy(Context);
        }
    }
}
