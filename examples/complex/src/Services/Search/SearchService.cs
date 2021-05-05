using System;
using System.Threading.Tasks;
using TinyFp;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;

namespace TinyFpTest.Services
{
    public class SearchService : ISearchService
    {
        private readonly IApiClient _apiClient;

        public SearchService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public Task<Either<string, Product[]>> SearchProductsAsync(string forName)
            => Task.FromResult((Either<string, Product[]>)Array.Empty<Product>());
    }
}
