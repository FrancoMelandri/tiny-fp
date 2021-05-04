using FluentAssertions;
using NUnit.Framework;
using System.Net;
using TinyFp.Complex.Setup;

namespace TinyFp.Complex.Contorllers
{
    public class SearchTests : BaseIntegrationTest
    {
        [Test]
        public void Search_Get_ReturnsNotFound()
        {
            var response = Client.GetAsync("/search").Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
