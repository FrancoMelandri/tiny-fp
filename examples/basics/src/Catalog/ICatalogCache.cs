using TinyFp;

namespace TinyFpTest.Examples.Basics.Catalog
{
    public interface ICatalogCache
    {
        Either<string, Catalog> Get();
        void Set(Catalog catalog);
    }
}
