using System.Threading.Tasks;
using TinyFp;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;

namespace TinyFpTest.Services.Details
{
    public interface IDetailsService
    {
        Task<Either<ApiError, ProductDetails>> GetDetailsAsync(string productName);
    }
}
