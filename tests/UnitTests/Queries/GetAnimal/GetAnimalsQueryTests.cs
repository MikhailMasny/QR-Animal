using Masny.QRAnimal.Application.CQRS.Queries.GetAnimal;
using Masny.QRAnimal.Application.DTO;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Masny.QRAnimal.UnitTests.Queries.GetAnimal
{
    public class GetAnimalsQueryTests : BaseTestsFixture
    {
        [Fact]
        public async Task Handle_ReturnsAnimalDTOCollection()
        {
            // Arrange
            var query = new GetAnimalsQuery();

            // Act
            var handler = new GetAnimalsQuery.GetAnimalsQueryHandler(Context, Mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<List<AnimalDTO>>();

            result.ShouldNotBeNull();
        }
    }
}
