using System.Net;
using NUnit.Framework;
using Shouldly;
using TinyFp.Complex.Setup;

namespace TinyFp.Complex.Contorllers;

[TestFixture]
public class DetailsDrivenPortsbaseTests : BaseIntegrationTest
{
    public DetailsDrivenPortsbaseTests()
    {
        AppSettings = () => "appsettings.json";
    }

    [TestCase("prd prd")]
    [TestCase(null)]
    [TestCase("")]
    public void Search_Get_ReturnBadRequest(string productName)
    {
        var response = Client.GetAsync($"/details?productName={productName}").Result;
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        TestStartup
            .Logger
            .Verify(_ => _.Error("BadRequest, bad_request, input is not valid"));
    }
}