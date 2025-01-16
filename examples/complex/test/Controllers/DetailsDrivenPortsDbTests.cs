using System.Net;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using TinyFp.Complex.Setup;
using TinyFpTest.Models;

namespace TinyFp.Complex.Contorllers;

[TestFixture]
public class DetailsDrivenPortsDbTests : DetailsDrivenPortsbaseTests
{
    public DetailsDrivenPortsDbTests()
    {
        AppSettings = () => "appsettings.override.db.json";
    }

    [Test]
    public void Search_Get_ReturnDetailsFromDb()
    {
        TestStartup
            .DetailsRepository
            .Setup(_ => _.GetByProductName("prd"))
            .ReturnsAsync(Option<ProductDetails>.Some(new ProductDetails
            {
                Sku = "sku",
                Name = "prd",
                Description = "description"
            }));

        var response = Client.GetAsync("/details?productName=prd").Result;
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseContent = response.Content.ReadAsStringAsync().Result;
        var products = JsonConvert.DeserializeObject<ProductDetails>(responseContent);

        products
            .Should()
            .BeEquivalentTo(new ProductDetails
            {
                Sku = "sku",
                Name = "prd",
                Description = "description"
            });

        TestStartup
            .DetailsRepository
            .Verify(_ => _.GetByProductName("prd"));

        TestStartup
            .Logger
            .Verify(_ => _.Error(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public void Search_Get_ReturnNotFounOnDb()
    {
        TestStartup
            .DetailsRepository
            .Setup(_ => _.GetByProductName("prd"))
            .ReturnsAsync(Option<ProductDetails>.None());

        var response = Client.GetAsync("/details?productName=prd").Result;
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        TestStartup
            .DetailsRepository
            .Verify(_ => _.GetByProductName("prd"));

        TestStartup
            .Logger
            .Verify(_ => _.Error("NotFound, not_found, product not found in db"));
    }
}