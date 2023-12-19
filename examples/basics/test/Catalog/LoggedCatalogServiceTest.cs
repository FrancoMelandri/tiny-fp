using FluentAssertions;
using Moq;
using NUnit.Framework;
using TinyFp;

namespace TinyFpTest.Examples.Basics.Catalog;

[TestFixture]
public class LoggedCatalogServiceTest
{
    private LoggedCatalogService _sut;
    private Mock<ICatalogService> _catalogService;
    private Mock<ILogger> _logger;
    private Catalog _catalog;

    [SetUp]
    public void SetUp()
    {
        _logger = new Mock<ILogger>();
        _catalogService = new Mock<ICatalogService>();

        _sut = new LoggedCatalogService(_catalogService.Object,
            _logger.Object);

        _catalog = new Catalog
        {
            Products = new[]
            {
                new Product
                {
                    Name = "Name1",
                    Description = "Description1"
                },
                new Product
                {
                    Name = "Name2",
                    Description = "Description2"
                }
            }
        };
    }

    [Test]
    public void Get_HappyPath_ReturnCatalog()
    {
        _catalogService
            .Setup(m => m.Get())
            .Returns(_catalog);

        var result = _sut.Get();

        result.IsRight.Should().BeTrue();
        _logger
            .Verify(m => m.Log(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public void Get_WhenError_Log()
    {
        Either<string, Catalog> error = "error";

        _catalogService
            .Setup(m => m.Get())
            .Returns(error);

        var result = _sut.Get();

        result.IsLeft.Should().BeTrue();
        _logger
            .Verify(m => m.Log("error"), Times.Once);
    }
}