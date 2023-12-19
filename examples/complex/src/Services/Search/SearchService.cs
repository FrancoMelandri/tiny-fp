using TinyFp;
using TinyFp.Extensions;
using TinyFpTest.Configuration;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;
using static TinyFpTest.Constants.Errors;

namespace TinyFpTest.Services;

public class SearchService(
    IApiClient apiClient,
    ProductsApiConfiguration productsApiConfiguration)
    : ISearchService
{
    public Task<Either<ApiError, Product[]>> SearchProductsAsync(string forName)
        => ApiRequest
            .Create()
            .WithUrl($"{productsApiConfiguration.Url}")
            .Map(apiClient.GetAsync<Product[]>)
            .BindAsync(_ => FilterProducts(forName, _))
            .BindAsync(_ => _.ToEither(p => p, p => p.Length == 0, NotFoundError));

    private static Either<ApiError, Product[]> FilterProducts(string forName, Product[] products)
        => products.Filter(_ => _.Name.Contains(forName)).ToArray();
}