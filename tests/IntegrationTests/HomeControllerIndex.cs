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
            // Arrange & Act
            var response = await Client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains("QR Animals &copy; 2019-2020", stringResponse);
        }
    }
}
