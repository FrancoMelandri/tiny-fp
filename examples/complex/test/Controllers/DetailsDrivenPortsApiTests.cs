using FluentAssertions;
using NUnit.Framework;
using System.Net;
using Moq;
using TinyFp.Complex.Setup;
using static System.IO.File;
using static System.IO.Path;
using Newtonsoft.Json;
using TinyFpTest.Models;

namespace TinyFp.Complex.Contorllers;

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
        StubDetailsOk("prd", 200, ReadAllText(Combine("ApiStubs", "details-prd.json")));

        var response = Client.GetAsync("/details?productName=prd").Result;
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        TestStartup
            .Logger
            .Verify(_ => _.Error(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public void Search_Get_ReturnsProductDetails()
    {
        StubDetailsOk("prd", 200, ReadAllText(Combine("ApiStubs", "details-prd.json")));

        var response = Client.GetAsync("/details?productName=prd").Result;
        var responseContent = response.Content.ReadAsStringAsync().Result;
        var product = JsonConvert.DeserializeObject<ProductDetails>(responseContent);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        product.Should().BeEquivalentTo(new ProductDetails
        {
            Name = "prd",
            Sku = "sku",
            Description = "product description"
        });

        IntegrationTestHttpRequestHandler
            .RequestsReceived
            .Where(_ => _.RequestUri.ToString().Contains("/details/prd"))
            .Should().HaveCount(1);

        TestStartup
            .Logger
            .Verify(_ => _.Error(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public void Search_Get_ReturnsNotFound_AndLog()
    {
        StubDetailsOk("yyy", 404, string.Empty);

        var response = Client.GetAsync("/details?productName=yyy").Result;

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        IntegrationTestHttpRequestHandler
            .RequestsReceived
            .Where(_ => _.RequestUri.ToString().Contains("/details/yyy"))
            .Should().HaveCount(1);

        TestStartup
            .Logger
            .Verify(_ => _.Error("NotFound, , Not Found"));
    }
}