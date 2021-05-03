using TinyFp;
using TinyFp.Extensions;
using static TinyFp.Prelude;

namespace TinyFpTest.Examples.Basics.Catalog
{
    public class CachedCatalogService : ICatalogService
    {
        private readonly ICatalogCache _cache;
        private readonly ICatalogService _catalog;

        public CachedCatalogService(ICatalogService catalog,
                                    ICatalogCache cache)
        {
            _catalog = catalog;
            _cache = cache;
        }

        public Either<string, Catalog> Get()
            => Try(() => _cache
                            .Get()
                            .BindLeft(
                                _ => _catalog
                                        .Get()
                                        .Map(_ => _.Tee(__ => _cache.Set(__))))
                )
                .Match(
                    _ => _,
                    ex => ex.Message
                );
    }
}
