using TinyFp;

namespace TinyFpTest.Examples.Basics.Catalog
{
    public interface ICatalogService
    {
        Either<string, Catalog> Get();
    }
}
