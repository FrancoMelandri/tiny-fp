using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TinyFp;
using Newtonsoft.Json;
using static TinyFp.Prelude;
using static TinyFp.Extensions.FunctionalExtension;
using static System.TimeSpan;

namespace TinyFpTest.Services.Api
{
    public class ApiClient : IApiClient
    {
        private readonly Func<HttpClient> _httpClientProvider;

        public ApiClient(Func<HttpClient> httpClientProvider)
        {
            _httpClientProvider = httpClientProvider;
        }

        public Task<Either<ApiError, T>> GetAsync<T>(ApiRequest apiRequest)
            => Using(CreateGetRequest(apiRequest),
                     _ => SendAsync<T>(_, apiRequest.Timeout));        

        private static HttpRequestMessage CreateGetRequest(ApiRequest apiRequest)
            => CreateRequest(new StringContent(string.Empty), apiRequest, HttpMethod.Get);

        private async Task<Either<ApiError, T>> SendAsync<T>(HttpRequestMessage httpRequest, int apiRequestTimeout)
            => (await SendAsyncWithStringResponse(httpRequest, apiRequestTimeout))
                .Map(_ => JsonConvert.DeserializeObject<T>(_));

        private Task<Either<ApiError, string>> SendAsyncWithStringResponse(
            HttpRequestMessage httpRequest,
            int apiRequestTimeout)
             => TryAsync(async () => await SendRequest(httpRequest, apiRequestTimeout))
                .OnFail(_ => new ApiError());

        private Task<Either<ApiError, string>> SendRequest(HttpRequestMessage httpRequest, int apiRequestTimeout)
            => new CancellationTokenSource()
                .Tee(_ => _.CancelAfter(FromMilliseconds(apiRequestTimeout)))
                .Map(async _ => await _httpClientProvider().SendAsync(httpRequest, _.Token))
                .MapAsync(async _ => await _.Content.ReadAsStringAsync())
                .Map(_ => _.ToEitherAsync(new ApiError()));

        private static HttpRequestMessage CreateRequest(HttpContent httpContent, ApiRequest apiRequest, HttpMethod method)
            => new HttpRequestMessage
                {
                    RequestUri = new Uri(apiRequest.Url),
                    Method = method,
                    Content = httpContent
                }
                .Tee(_ => apiRequest.Headers.ForEach(x => _.Headers.Add(x.name, x.value)));
    }
}
