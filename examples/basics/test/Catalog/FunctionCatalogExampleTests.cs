using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace TinyFpTest.Examples.Basics.Catalog
{

    [TestFixture]
    public class FunctionCatalogExampleTests
    {
        private FunctionalCatalogService _sut;
        private Mock<IApiClient> _apiClient;
        private Catalog _catalog;

        [SetUp]
        public void SetUp()
        {
            _apiClient = new Mock<IApiClient>();

            _sut = new FunctionalCatalogService(_apiClient.Object);

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
        public void Get_WhenNoError_ReturnCatalog()
        {
            _apiClient
                .Setup(m => m.Get())
                .Returns(_catalog);

            var result = _sut.Get();

            result.IsRight.Should().BeTrue();
            result.OnRight(_ => _.Products.Length.Should().Be(2));
        }

        [Test]
        public void Get_WhenExcpetion_ReturnMessage ()
        {
            _apiClient
                .Setup(m => m.Get())
                .Throws(new Exception("error"));

            var result = _sut.Get();

            result.IsLeft.Should().BeTrue();
            result.OnLeft(_ => _.Should().Be("error"));
        }
    }
}
