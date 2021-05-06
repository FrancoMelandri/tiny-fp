using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using TinyFp.Complex.Setup;
using TinyFpTest.Models;
using static System.IO.File;
using static System.IO.Path;

namespace TinyFp.Complex.Contorllers
{
    public class SearchTests : BaseIntegrationTest
    {
        [Test]
        public void Search_Get_NotFound_WhenEmptyProducts()
        {
            StubProducts(200, "[]");

            var response = Client.GetAsync("/search?forName=prd").Result;
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public void Search_Get_ReturnsProductListFiltered()
        {
            StubProducts(200, ReadAllText(Combine("ApiStubs", "products.json")));

            var response = Client.GetAsync("/search?forName=prd").Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var products = JsonConvert.DeserializeObject<Product[]>(responseContent);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            products.Should().HaveCount(2);
        }

        [Test]
        public void Search_Get_ReturnsNotFound()
        {
            StubProducts(200, ReadAllText(Combine("ApiStubs", "products.json")));

            var response = Client.GetAsync("/search?forName=yyy").Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
