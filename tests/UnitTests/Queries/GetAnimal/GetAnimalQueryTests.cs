using Masny.QRAnimal.Application.CQRS.Queries.GetAnimal;
using Masny.QRAnimal.Application.DTO;
using Masny.QRAnimal.Domain.Enums;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Masny.QRAnimal.UnitTests.Queries.GetAnimal
{
    public class GetAnimalQueryTests : BaseTestsFixture
    {
        [Fact]
        public async Task Handle_WhenIsAnotherUser_ReturnsCorrectAnimalDTO()
        {
            // Arrange
            var animal = new AnimalDTO
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
                IsPublic = true
            };

            var query = new GetAnimalQuery
            {
                Id = 1,
                UserId = "QWERTY1234567890_One",
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

        [Fact]
        public async Task Handle_WhenIsNotAnotherUser_ReturnsCorrectAnimalDTO()
        {
            // Arrange
            var animal = new AnimalDTO
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
                IsPublic = true
            };

            var query = new GetAnimalQuery
            {
                Id = 1,
                UserId = string.Empty,
                AnotherUser = true
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

        [Fact]
        public async Task Handle_GivenInvalidId_ReturnsCorrectAnimalDTO()
        {
            // Arrange
            var query = new GetAnimalQuery
            {
                Id = 99,
                UserId = string.Empty,
                AnotherUser = true
            };

            // Act
            var handler = new GetAnimalQuery.GetAnimalQueryHandler(Context, Mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldBeNull();
        }
    }
}
