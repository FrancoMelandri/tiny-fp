using System.Threading.Tasks;
using TinyFp;
using TinyFpTest.Models;

namespace TinyFpTest.Services
{
    public class LoggedSearchService : ISearchService
    {
        private readonly ISearchService _searchService;

        public LoggedSearchService(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public Task<Either<string, Product[]>> SearchProductsAsync(string forName)
            => _searchService.SearchProductsAsync(forName);
    }
}
