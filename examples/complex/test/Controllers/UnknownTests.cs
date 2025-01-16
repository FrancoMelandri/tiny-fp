using System.Net;
using NUnit.Framework;
using Shouldly;
using TinyFp.Complex.Setup;

namespace TinyFp.Complex.Contorllers;

public class UnknownTests : BaseIntegrationTest
{
    [Test]
    public void Unknown_Get_ReturnsNotFound()
        => Client.GetAsync("/unknown").Result
            .StatusCode
            .ShouldBe(HttpStatusCode.NotFound);

}