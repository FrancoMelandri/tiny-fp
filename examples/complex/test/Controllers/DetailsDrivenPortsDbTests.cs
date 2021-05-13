using FluentAssertions;
using NUnit.Framework;
using System.Net;
using TinyFp.Complex.Setup;

namespace TinyFp.Complex.Contorllers
{
    [TestFixture]
    public class DetailsDrivenPortsDbTests : BaseIntegrationTest
    {
        public DetailsDrivenPortsDbTests()
        {
            AppSettings = () => "appsettings.override.db.json";
        }

        [Test]
        public void Search_Get_ReturnDetailsFromDb()
        {
            var response = Client.GetAsync("/details?productName=prd").Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
