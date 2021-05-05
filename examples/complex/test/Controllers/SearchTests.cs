using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using TinyFp.Complex.Setup;
using TinyFpTest.Models;

namespace TinyFp.Complex.Contorllers
{
    public class SearchTests : BaseIntegrationTest
    {
        [Test]
        public void Search_Get_ReturnsEmptyProductList()
        {
            StubProducts("prd", 200, "[]");

            var response = Client.GetAsync("/search?forName=prd").Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var products = JsonConvert.DeserializeObject<Product[]>(responseContent);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            products.Should().BeEmpty();
        }
    }
}
