using TinyFp;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;
using static TinyFpTest.Constants.Errors;

namespace TinyFpTest.Services.Details;

public class DetailsDrivenPortAdapterDb(IDetailsRepository detailsRepository) : IDetailsDrivenPort
{
    public Task<Either<ApiError, ProductDetails>> GetDetailsAsync(string productName)
        => detailsRepository
            .GetByProductName(productName)
            .MatchAsync(
                _ => Either<ApiError, ProductDetails>.Right(_),
                () => NotFoundOnDb);
}