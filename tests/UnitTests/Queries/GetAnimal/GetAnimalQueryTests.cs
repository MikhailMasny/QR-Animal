using Masny.QRAnimal.Application.CQRS.Queries.GetAnimal;
using Masny.QRAnimal.Application.DTO;
using Masny.QRAnimal.Domain.Enums;
using Masny.QRAnimal.UnitTests;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Queries.GetAnimal
{
    public class GetAnimalQueryTests : BaseTestsFixture
    {
        [Fact]
        public async Task Handle_ReturnsCorrectAnimalDTO()
        {
            // Arrange
            var animal = new AnimalDTO
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
                IsPublic = true
            };

            var query = new GetAnimalQuery
            {
                Id = 1,
                UserId = "QWERTY1234567890",
                AnotherUser = false
            };

            // Act
            var handler = new GetAnimalQuery.GetAnimalQueryHandler(Context, Mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<AnimalDTO>();

            result.ShouldNotBeNull();

            result.Id.ShouldBe(animal.Id);
            result.UserId.ShouldBe(animal.UserId);
            result.Kind.ShouldBe(animal.Kind);
            result.Breed.ShouldBe(animal.Breed);
            result.Gender.ShouldBe(animal.Gender);
            result.Passport.ShouldBe(animal.Passport);
            result.BirthDate.ShouldBe(animal.BirthDate);
            result.Nickname.ShouldBe(animal.Nickname);
            result.Features.ShouldBe(animal.Features);
            result.IsPublic.ShouldBe(animal.IsPublic);
        }
    }
}
