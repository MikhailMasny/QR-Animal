using Masny.QRAnimal.Application.CQRS.Commands.UpdateAnimal;
using Masny.QRAnimal.Application.DTO;
using Masny.QRAnimal.Application.Exceptions;
using Masny.QRAnimal.Domain.Enums;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Masny.QRAnimal.UnitTests.Commands.Update
{
    public class UpdatedAnimalCommandTests : BaseTestsFixture
    {
        [Fact]
        public async Task Handle_GivenValidData_ShouldUpdatePersistedAnimal()
        {
            // Arrange
            var animal = new AnimalDTO
            {
                Id = 1,
                UserId = "QWERTY1234567890_One",
                Kind = "Kind_new_value",
                Breed = "Breed_new_value",
                Gender = GenderTypes.Male,
                Passport = "1234567890QWERTY_new_value",
                BirthDate = new DateTime(1999, 01, 01),
                Nickname = "Nickname_new_value",
                Features = "Features_new_value",
                IsPublic = true
            };

            var command = new UpdateAnimalCommand
            {
                Model = animal
            };

            // Act
            var handler = new UpdateAnimalCommand.UpdateAnimalCommandHandler(Context);

            await handler.Handle(command, CancellationToken.None);

            var entity = Context.Animals.Find(animal.Id);

            // Assert
            entity.ShouldNotBeNull();

            entity.UserId.ShouldBe(command.Model.UserId);
            entity.Kind.ShouldBe(command.Model.Kind);
            entity.Breed.ShouldBe(command.Model.Breed);
            entity.Passport.ShouldBe(command.Model.Passport);
            entity.Nickname.ShouldBe(command.Model.Nickname);
            entity.Features.ShouldBe(command.Model.Features);
            entity.IsPublic.ShouldBe(command.Model.IsPublic);

            entity.Gender.ShouldNotBe(command.Model.Gender);
            entity.BirthDate.ShouldNotBe(command.Model.BirthDate);
        }

        [Fact]
        public async Task Handle_GivenInvalidData_ThrowsException()
        {
            // Arrange
            var animal = new AnimalDTO
            {
                Id = 99,
                UserId = "QWERTY1234567890",
                Kind = "Kind_new_value",
                Breed = "Breed_new_value",
                Gender = GenderTypes.Male,
                Passport = "1234567890QWERTY_new_value",
                BirthDate = new DateTime(2001, 01, 01),
                Nickname = "Nickname_new_value",
                Features = "Features_new_value",
                IsPublic = true
            };

            var command = new UpdateAnimalCommand
            {
                Model = animal
            };

            // Act
            var handler = new UpdateAnimalCommand.UpdateAnimalCommandHandler(Context);

            // Assert
            await Should.ThrowAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
