using System.Collections.Generic;
using System.Threading.Tasks;
using TinyFp;
using TinyFp.Extensions;
using TinyFpTest.Configuration;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;

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
                .WithUrl($"{_productsApiConfiguration.Url}/{forName}")
                .Map(_apiClient.GetAsync<Product[]>);
    }
}
