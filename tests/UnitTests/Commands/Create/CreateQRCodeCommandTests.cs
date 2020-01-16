using Masny.QRAnimal.Application.CQRS.Commands.CreateQRCode;
using Masny.QRAnimal.Application.DTO;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Masny.QRAnimal.UnitTests.Commands.Create
{
    public class CreateQRCodeCommandTests : BaseTestsFixture
    {
        [Fact]
        public async Task Handle_ShouldPersistQRCode()
        {
            // Arrange
            var qrcode = new QRCodeDTO
            {
                Code = "Code_test",
                Created = new DateTime(2004, 01, 01),
                AnimalId = 3
            };

            var command = new CreateQRCodeCommand
            {
                Model = qrcode
            };

            // Act
            var handler = new CreateQRCodeCommand.CreateQRCodeCommandHandler(Context, Mapper);

            var result = await handler.Handle(command, CancellationToken.None);

            var entity = Context.QRCodes.Find(result.Id);

            // Assert
            entity.ShouldNotBeNull();

            entity.Code.ShouldBe(command.Model.Code);
            entity.Created.ShouldBe(command.Model.Created);
            entity.AnimalId.ShouldBe(command.Model.AnimalId);
        }
    }
}
