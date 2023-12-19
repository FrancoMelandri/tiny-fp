using TinyFp;
using TinyFp.Extensions;
using static TinyFp.Prelude;

namespace TinyFpTest.Examples.Basics.Catalog;

public class CachedCatalogService(
    ICatalogService catalog,
    ICatalogCache cache) : ICatalogService
{
    public Either<string, Catalog> Get()
        => Try(() => cache
                .Get()
                .BindLeft(
                    _ => catalog
                        .Get()
                        .Map(_ => _.Tee(cache.Set))))
            .OnFail(_ => _.Message);
}