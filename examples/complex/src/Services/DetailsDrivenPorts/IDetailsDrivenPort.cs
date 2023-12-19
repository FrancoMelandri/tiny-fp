using TinyFp;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;

namespace TinyFpTest.Services.Details;

public interface IDetailsDrivenPort
{
    Task<Either<ApiError, ProductDetails>> GetDetailsAsync(string productName);
}