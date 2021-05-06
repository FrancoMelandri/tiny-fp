using System.Linq;
using System.Threading.Tasks;
using TinyFp;
using TinyFp.Extensions;
using TinyFpTest.Configuration;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;
using static TinyFp.Extensions.Functional;
using static TinyFpTest.Constants.Errors;

namespace TinyFpTest.Services
{
    public class SearchService : ISearchService
    {
        private readonly IApiClient _apiClient;
        private readonly ProductsApiConfiguration _productsApiConfiguration;

        public SearchService(IApiClient apiClient,
                             ProductsApiConfiguration productsApiConfiguration)
        {
            _apiClient = apiClient;
            _productsApiConfiguration = productsApiConfiguration;
        }

        public Task<Either<ApiError, Product[]>> SearchProductsAsync(string forName)
            => ApiRequest
                .Create()
                .WithUrl($"{_productsApiConfiguration.Url}")
                .Map(_apiClient.GetAsync<Product[]>)
                .BindAsync(_ => FilterProducts(forName, _))
                .BindAsync(_ => _.ToEither(p => p, p => p.Length == 0, NotFoundError));

        private static Either<ApiError, Product[]> FilterProducts(string forName, Product[] products)
            => products.Filter(_ => _.Name.Contains(forName)).ToArray();
    }
}
