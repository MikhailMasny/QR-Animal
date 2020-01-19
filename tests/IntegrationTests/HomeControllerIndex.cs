using FluentAssertions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Masny.QRAnimal.IntegrationTests
{
    [Collection("Sequential")]
    public class HomeControllerIndex : IClassFixture<WebTestFixture>
    {
        public HomeControllerIndex(WebTestFixture factory)
        {
            factory = factory ?? throw new ArgumentNullException(nameof(factory));

            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task Return_HomePage_WithFooterInformation()
        {
            // Arrange
            HttpResponseMessage response = await Client.GetAsync("/");

            // Act
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            stringResponse.Should().Contain("QR Animals &copy; 2019-2020");
        }
    }
}
