using AutoMapper;
using Masny.QRAnimal.Application.Mapping;
using Masny.QRAnimal.Infrastructure.Persistence;
using System;

namespace Masny.QRAnimal.UnitTests
{
    /// <summary>
    /// Реализация базового приспособления.
    /// </summary>
    public class BaseTestsFixture : IDisposable
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public BaseTestsFixture()
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
