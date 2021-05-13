using FluentAssertions;
using NUnit.Framework;
using System.Net;
using TinyFp.Complex.Setup;

namespace TinyFp.Complex.Contorllers
{
    [TestFixture]
    public class DetailsDrivenPortsApiTests : BaseIntegrationTest
    {
        public DetailsDrivenPortsApiTests()
        {
            AppSettings = () => "appsettings.json";
        }

        [Test]
        public void Search_Get_ReturnDetailsFromApi()
        {

            var response = Client.GetAsync("/details?productName=prd").Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
