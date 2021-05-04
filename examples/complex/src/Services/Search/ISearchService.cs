using System.Threading.Tasks;
using TinyFp;
using TinyFpTest.Models;

namespace TinyFpTest.Services
{
    public interface ISearchService
    {
        Task<Either<string, Product[]>> SearchProductsAsync(string forName);
    }
}
