using TinyFp;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;
using static TinyFp.Extensions.Functional;

namespace TinyFpTest.Services;

public class LoggedSearchService(
    ISearchService searchService,
    Serilog.ILogger logger) : ISearchService
{
    public Task<Either<ApiError, Product[]>> SearchProductsAsync(string forName)
        => searchService
            .SearchProductsAsync(forName)
            .BindLeftAsync(LogError);

    private Either<ApiError, Product[]> LogError(ApiError error)
        => error.Tee(_ => logger.Error($"{_.StatusCode}, {_.Code}, {_.Description}"));
}