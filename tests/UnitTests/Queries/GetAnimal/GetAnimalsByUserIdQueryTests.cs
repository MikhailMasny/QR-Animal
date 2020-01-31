using Masny.QRAnimal.Application.CQRS.Queries.GetAnimal;
using Masny.QRAnimal.Application.DTO;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Masny.QRAnimal.UnitTests.Queries.GetAnimal
{
    public class GetAnimalsByUserIdQueryTests : BaseTestsFixture
    {
        [Fact]
        public async Task Handle_ReturnsNotEmptyAnimalDTOCollection()
        {
            // Arrange
            var query = new GetAnimalsByUserIdQuery
            {
                UserId = "QWERTY1234567890_One"
            };

            // Act
            var handler = new GetAnimalsByUserIdQuery.GetAnimalsByUserIdQueryHandler(Context, Mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<List<AnimalDTO>>();
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task Handle_ReturnsEmptyAnimalDTOCollection()
        {
            // Arrange
            var query = new GetAnimalsByUserIdQuery
            {
                UserId = "QWERTY1234567890_Test"
            };

            // Act
            var handler = new GetAnimalsByUserIdQuery.GetAnimalsByUserIdQueryHandler(Context, Mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldBeEmpty();
        }
    }
}
