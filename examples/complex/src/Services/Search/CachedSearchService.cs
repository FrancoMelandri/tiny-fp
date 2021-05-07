using System.Threading.Tasks;
using TinyFp;
using TinyFp.Extensions;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;

namespace TinyFpTest.Services
{
    public class CachedSearchService : ISearchService
    {
        private const string PRODUCTS_KEY = "products";

        private readonly ICache _cache;
        private readonly ISearchService _searchService;

        public CachedSearchService(ICache cache,
                                   ISearchService searchService)
        {
            _cache = cache;
            _searchService = searchService;
        }

        public Task<Either<ApiError, Product[]>> SearchProductsAsync(string forName)
            => _cache.GetAsync<Product[]>(PRODUCTS_KEY)
                .MatchAsync(
                    _ => _,
                    () => _searchService
                            .SearchProductsAsync(forName)
                            .BindAsync(_ => DoCache(_)));

        private Either<ApiError, Product[]> DoCache(Product[] products)
            => products.Tee(_ => _cache.SetAsync(PRODUCTS_KEY, products));
    }
}
