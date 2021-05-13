using FluentAssertions;
using NUnit.Framework;
using System.Net;
using TinyFp.Complex.Setup;

namespace TinyFp.Complex.Contorllers
{
    [TestFixture]
    public class DetailsDrivenPortsbaseTests : BaseIntegrationTest
    {
        public DetailsDrivenPortsbaseTests()
        {
            AppSettings = () => "appsettings.json";
        }

        [TestCase("prd prd")]
        [TestCase(null)]
        [TestCase("")]
        public void Search_Get_ReturnBadRequest(string productName)
        {
            var response = Client.GetAsync($"/details?productName={productName}").Result;
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
