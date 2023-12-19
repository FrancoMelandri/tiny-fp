using TinyFp;
using TinyFp.Extensions;
using TinyFpTest.Configuration;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;

namespace TinyFpTest.Services.Details;

public class DetailsDrivenPortAdapterApi(
    IApiClient apiClient,
    ProductDetailsApiConfiguration productDetailsApiConfiguration)
    : IDetailsDrivenPort
{
    public Task<Either<ApiError, ProductDetails>> GetDetailsAsync(string productName)
        => ApiRequest
            .Create()
            .WithUrl($"{productDetailsApiConfiguration.Url}/{productName}")
            .Map(apiClient.GetAsync<ProductDetails>);
}