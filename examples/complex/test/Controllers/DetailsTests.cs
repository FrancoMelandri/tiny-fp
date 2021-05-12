using FluentAssertions;
using NUnit.Framework;
using System.Net;
using TinyFp.Complex.Setup;

namespace TinyFp.Complex.Contorllers
{
    [TestFixture]
    public class DetailsTests : BaseIntegrationTest
    {
        [Test]
        public void Search_Get_NotFound_WhenEmptyProducts_AndLog()
        {
            StubProducts(200, "[]");


            var response = Client.GetAsync("/details?productName=prd").Result;
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
