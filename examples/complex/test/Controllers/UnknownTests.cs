using FluentAssertions;
using NUnit.Framework;
using System.Net;
using TinyFp.Complex.Setup;

namespace TinyFp.Complex.Contorllers
{
    public class UnknownTests : BaseIntegrationTest
    {
        [Test]
        public void Unknown_Get_ReturnsNotFound()
            => Client.GetAsync("/unknown").Result
                .StatusCode
                .Should().Be(HttpStatusCode.NotFound);

    }
}
