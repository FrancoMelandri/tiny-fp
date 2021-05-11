using FluentAssertions;
using Moq;
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
        public void Search_Get_NotFound_WhenEmptyProducts_AndLog()
        {
            StubProducts(200, "[]");

            var response = Client.GetAsync("/search?forName=prd").Result;
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            IntegrationTestHttpRequestHandler
                .RequestsReceived
                .Where(_ => _.RequestUri.ToString().Contains("/products"))
                .Should().HaveCount(1);

            TestStartup
                .Logger
                .Verify(_ => _.Error("NotFound, not_found, product not found"));
        }

        [Test]
        public void Search_Get_ReturnBadRequest_DueToValidation()
        {
            StubProducts(200, ReadAllText(Combine("ApiStubs", "products.json")));

            var response = Client.GetAsync("/search?forName=prd prd").Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            IntegrationTestHttpRequestHandler
                .RequestsReceived
                .Where(_ => _.RequestUri.ToString().Contains("/products"))
                .Should().HaveCount(0);

            TestStartup
                .Logger
                .Verify(_ => _.Error("BadRequest, bad_request, input is not valid"));
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

            TestStartup
                .Logger
                .Verify(_ => _.Error(It.IsAny<string>()), Times.Never);
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
                .ExistsAsync("products:prd")
                .Result
                .Should().BeTrue();

            TestStartup
                .Logger
                .Verify(_ => _.Error(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void Search_Get_ReturnsNotFound_AndLog()
        {
            StubProducts(200, ReadAllText(Combine("ApiStubs", "products.json")));

            var response = Client.GetAsync("/search?forName=yyy").Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            IntegrationTestHttpRequestHandler
                .RequestsReceived
                .Where(_ => _.RequestUri.ToString().Contains("/products"))
                .Should().HaveCount(1);

            TestStartup
                .Logger
                .Verify(_ => _.Error("NotFound, not_found, product not found"));
        }
    }
}
