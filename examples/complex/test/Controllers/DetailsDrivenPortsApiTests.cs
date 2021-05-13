using FluentAssertions;
using NUnit.Framework;
using System.Net;

namespace TinyFp.Complex.Contorllers
{
    [TestFixture]
    public class DetailsDrivenPortsApiTests : DetailsDrivenPortsbaseTests
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
