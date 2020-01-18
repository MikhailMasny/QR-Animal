using Masny.QRAnimal.Application.CQRS.Queries.GetQRCode;
using Masny.QRAnimal.Application.DTO;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Masny.QRAnimal.UnitTests.Queries.GetQRCode
{
    public class GetQRCodeQueryTests : BaseTestsFixture
    {
        [Fact]
        public async Task Handle_ReturnsCorrectQRCodeDTO()
        {
            // Arrange
            var qrcode = new QRCodeDTO
            {
                Code = "Code",
                Created = new DateTime(2001, 01, 01),
                AnimalId = 1
            };

            var query = new GetQRCodeQuery
            {
                AnimalId = 1
            };

            // Act
            var handler = new GetQRCodeQuery.GetQRCodeQueryHandler(Context, Mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<QRCodeDTO>();

            result.ShouldNotBeNull();

            result.Code.ShouldBe(qrcode.Code);
            result.Created.ShouldBe(qrcode.Created);
            result.AnimalId.ShouldBe(qrcode.AnimalId);
        }
    }
}
