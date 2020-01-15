using Masny.QRAnimal.Application.CQRS.Commands.CreateAnimal;
using Masny.QRAnimal.Application.DTO;
using Masny.QRAnimal.Domain.Enums;
using Masny.QRAnimal.UnitTests;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Commands.Create
{
    public class CreateAnimalCommandTests : BaseTestsFixture
    {
        [Fact]
        public async Task Handle_ShouldPersistAnimal()
        {
            // Arrange
            var animal = new AnimalDTO
            {
                Id = 2,
                UserId = "QWERTY1234567890_test",
                Kind = "Kind_test",
                Breed = "Breed_test",
                Gender = GenderTypes.None,
                Passport = "1234567890QWERTY_test",
                BirthDate = new DateTime(2000, 01, 01),
                Nickname = "Nickname_test",
                Features = "Features_test",
                IsPublic = false
            };

            var command = new CreateAnimalCommand
            {
                Model = animal
            };

            // Act
            var handler = new CreateAnimalCommand.CreateAnimalCommandHandler(Context, Mapper);

            var result = await handler.Handle(command, CancellationToken.None);

            var entity = Context.Animals.Find(result);

            // Assert
            entity.ShouldNotBeNull();

            entity.Id.ShouldBe(command.Model.Id);
            entity.UserId.ShouldBe(command.Model.UserId);
            entity.Kind.ShouldBe(command.Model.Kind);
            entity.Breed.ShouldBe(command.Model.Breed);
            entity.Gender.ShouldBe(command.Model.Gender);
            entity.Passport.ShouldBe(command.Model.Passport);
            entity.BirthDate.ShouldBe(command.Model.BirthDate);
            entity.Nickname.ShouldBe(command.Model.Nickname);
            entity.Features.ShouldBe(command.Model.Features);
            entity.IsPublic.ShouldBe(command.Model.IsPublic);
        }
    }
}
