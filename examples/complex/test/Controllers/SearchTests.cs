using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Linq;
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

            IntegrationTestHttpRequestHandler
                .RequestsReceived
                .Where(_ => _.RequestUri.ToString().Contains("/products"))
                .Should().HaveCount(1);

        }

        [Test]
        public void Search_Get_TwoTimes_ReturnsProductListFiltered_AndCahcheValue()
        {
            StubProducts(200, ReadAllText(Combine("ApiStubs", "products.json")));

            var response = Client.GetAsync("/search?forName=prd").Result;
            response = Client.GetAsync("/search?forName=prd").Result;

            var responseContent = response.Content.ReadAsStringAsync().Result;
            var products = JsonConvert.DeserializeObject<Product[]>(responseContent);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            products.Should().HaveCount(2);


            response = Client.GetAsync("/search?forName=prd").Result;
            responseContent = response.Content.ReadAsStringAsync().Result;
            products = JsonConvert.DeserializeObject<Product[]>(responseContent);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            products.Should().HaveCount(2);

            IntegrationTestHttpRequestHandler
                .RequestsReceived
                .Where(_ => _.RequestUri.ToString().Contains("/products"))
                .Should().HaveCount(1);

            TestStartup
                .InMemoryRedisCache
                .ExistsAsync("products")
                .Result
                .Should().BeTrue();
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
