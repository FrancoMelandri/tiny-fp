using Moq;
using NUnit.Framework;
using Shouldly;
using TinyFp;

namespace TinyFpTest.Examples.Basics.Catalog;

[TestFixture]
public class CachedCatalogExampleTests
{
    private CachedCatalogService _sut;
    private Mock<ICatalogCache> _catalogCache;
    private Mock<ICatalogService> _catalogService;
    private Catalog _catalog;

    [SetUp]
    public void SetUp()
    {
        _catalogCache = new Mock<ICatalogCache>();
        _catalogService = new Mock<ICatalogService>();

        _sut = new CachedCatalogService(_catalogService.Object,
            _catalogCache.Object);

        _catalog = new Catalog
        {
            Products =
            [
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
            ]
        };
    }

    [Test]
    public void Get_WhenCatalogInCache_ReturnCache()
    {
        _catalogCache
            .Setup(m => m.Get())
            .Returns(_catalog);

        var result = _sut.Get();

        result.IsRight.ShouldBeTrue();
        _catalogCache
            .Verify(m => m.Set(It.IsAny<Catalog>()), Times.Never);
        _catalogService
            .Verify(m => m.Get(), Times.Never);
    }

    [Test]
    public void Get_WhenCatalogNotInCache_ReturnService_SetInCache()
    {
        _catalogCache
            .Setup(m => m.Get())
            .Returns("error");
        _catalogService
            .Setup(m => m.Get())
            .Returns(_catalog);

        var result = _sut.Get();

        result.IsRight.ShouldBeTrue();
        _catalogCache
            .Verify(m => m.Set(It.IsAny<Catalog>()), Times.Once);
        _catalogService
            .Verify(m => m.Get(), Times.Once);
    }

    [Test]
    public void Get_WhenCatalogNotInCache_AndError_ReturnError_NotSetInCache()
    {
        _catalogCache
            .Setup(m => m.Get())
            .Returns("error");
        _catalogService
            .Setup(m => m.Get())
            .Returns("error");

        var result = _sut.Get();

        result.IsLeft.ShouldBeTrue();
        _catalogCache
            .Verify(m => m.Set(It.IsAny<Catalog>()), Times.Never);
        _catalogService
            .Verify(m => m.Get(), Times.Once);
    }

    [Test]
    public void Get_WhenErrorInGetFromCache_ReturnError()
    {
        var empty = Option<Catalog>.None();
        _catalogCache
            .Setup(m => m.Get())
            .Throws(new Exception("error"));

        var result = _sut.Get();

        result.IsLeft.ShouldBeTrue();
        result.OnLeft(_ => _.ShouldBe("error"));
        _catalogCache
            .Verify(m => m.Set(It.IsAny<Catalog>()), Times.Never);
        _catalogService
            .Verify(m => m.Get(), Times.Never);
    }
}