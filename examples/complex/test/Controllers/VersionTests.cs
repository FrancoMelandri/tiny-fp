using FluentAssertions;
using NUnit.Framework;
using System.Net;
using TinyFp.Complex.Setup;

namespace TinyFp.Complex.Contorllers
{
    public class VersionTests : BaseIntegrationTest
    {
        [Test]
        public void Version_Get_ReturnsOk_And_RigtContent()
        {
            var response = Client.GetAsync("/version").Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            responseContent.Should().Be("Hello world");
        }
    }
}
