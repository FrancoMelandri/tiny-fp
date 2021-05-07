using Serilog;
using System.Threading.Tasks;
using TinyFp;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;
using static TinyFp.Extensions.Functional;

namespace TinyFpTest.Services
{
    public class LoggedSearchService : ISearchService
    {
        private readonly ISearchService _searchService;
        private readonly ILogger _logger;

        public LoggedSearchService(ISearchService searchService,
                                   ILogger logger)
        {
            _searchService = searchService;
            _logger = logger;
        }

        public Task<Either<ApiError, Product[]>> SearchProductsAsync(string forName)
            => _searchService
                .SearchProductsAsync(forName)
                .BindLeftAsync(_ => LogError(_));

        private Either<ApiError, Product[]> LogError(ApiError error)
            => error.Tee(_ => _logger.Error($"{_.StatusCode}, {_.Code}, {_.Description}"));
    }
}
