using TinyFp;
using TinyFp.Extensions;
using TinyFpTest.Configuration;
using TinyFpTest.Models;
using TinyFpTest.Services.Api;

namespace TinyFpTest.Services.Details
{
    public class DetailsDrivenPortAdapterApi : IDetailsDrivenPort
    {
        private readonly IApiClient _apiClient;
        private readonly ProductDetailsApiConfiguration _productDetailsApiConfiguration;

        public DetailsDrivenPortAdapterApi(IApiClient apiClient,
                                           ProductDetailsApiConfiguration productDetailsApiConfiguration)
        {
            _apiClient = apiClient;
            _productDetailsApiConfiguration = productDetailsApiConfiguration;
        }

        public Task<Either<ApiError, ProductDetails>> GetDetailsAsync(string productName)
            => ApiRequest
                .Create()
                .WithUrl($"{_productDetailsApiConfiguration.Url}/{productName}")
                .Map(_apiClient.GetAsync<ProductDetails>);
    }
}
