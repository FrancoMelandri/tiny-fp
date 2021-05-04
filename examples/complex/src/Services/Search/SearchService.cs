using System;
using System.Threading.Tasks;
using TinyFp;
using TinyFpTest.Models;

namespace TinyFpTest.Services
{
    public class SearchService : ISearchService
    {
        public Task<Either<string, Product[]>> SearchProductsAsync(string forName)
            => Task.FromResult((Either<string, Product[]>)Array.Empty<Product>());
    }
}
