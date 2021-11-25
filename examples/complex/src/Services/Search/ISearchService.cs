using TinyFp;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;

namespace TinyFpTest.Services
{
    public interface ISearchService
    {
        Task<Either<ApiError, Product[]>> SearchProductsAsync(string forName);
    }
}
