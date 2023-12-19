using TinyFp;
using TinyFp.Extensions;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;

namespace TinyFpTest.Services;

public class CachedSearchService(
    ICache cache,
    ISearchService searchService) : ISearchService
{
    private const string PRODUCTS_KEY = "products";

    public Task<Either<ApiError, Product[]>> SearchProductsAsync(string forName)
        => cache.GetAsync<Product[]>(GetKey(forName))
            .MatchAsync(
                _ => _,
                () => searchService
                    .SearchProductsAsync(forName)
                    .BindAsync(_ => DoCache(_, forName)));

    private Either<ApiError, Product[]> DoCache(Product[] products, string forName)
        => products.Tee(_ => cache.SetAsync(GetKey(forName), products));

    private static string GetKey(string forName)
        => $"{PRODUCTS_KEY}:{forName}";
}