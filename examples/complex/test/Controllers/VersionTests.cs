using System.Net;
using NUnit.Framework;
using Shouldly;
using TinyFp.Complex.Setup;

namespace TinyFp.Complex.Contorllers;

public class VersionTests : BaseIntegrationTest
{
    [Test]
    public void Version_Get_ReturnsOk_And_RigtContent()
    {
        var response = Client.GetAsync("/version").Result;
        var responseContent = response.Content.ReadAsStringAsync().Result;

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        responseContent.ShouldBe("Hello world");
    }
}