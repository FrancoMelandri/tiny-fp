using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TinyFp;
using Newtonsoft.Json;
using System.Net;
using static TinyFp.Prelude;
using static TinyFp.Extensions.Functional;
using static System.TimeSpan;
using static TinyFpTest.Constants.Errors;

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
            => UsingAsync(CreateGetRequest(apiRequest),
                          _ => SendAsync<T>(_, apiRequest.Timeout));

        private static HttpRequestMessage CreateGetRequest(ApiRequest apiRequest)
            => CreateRequest(new StringContent(string.Empty), apiRequest, HttpMethod.Get);

        private async Task<Either<ApiError, T>> SendAsync<T>(HttpRequestMessage httpRequest, int apiRequestTimeout)
            => (await SendAsyncWithStringResponse(httpRequest, apiRequestTimeout))
                .Map(JsonConvert.DeserializeObject<T>);

        private Task<Either<ApiError, string>> SendAsyncWithStringResponse(
            HttpRequestMessage httpRequest,
            int apiRequestTimeout)
             => TryAsync(() => SendRequest(httpRequest, apiRequestTimeout))
                .OnFail(_ => ExceptionCallingService);

        private Task<Either<ApiError, string>> SendRequest(HttpRequestMessage httpRequest, int apiRequestTimeout)
            => new CancellationTokenSource()
                .Tee(_ => _.CancelAfter(FromMilliseconds(apiRequestTimeout)))
                .Map(_ => _httpClientProvider().SendAsync(httpRequest, _.Token))
                .MapAsync(IsValidResponse)
                .BindAsync(GetResponseContent);

        private Task<Either<ApiError, HttpResponseMessage>> IsValidResponse(HttpResponseMessage response)
            => response.StatusCode == HttpStatusCode.OK ?
                    Either<ApiError, HttpResponseMessage>.Right(response).AsTask() :
                    Either<ApiError, HttpResponseMessage>.Left(ApiError.Create(response.StatusCode, string.Empty, response.ReasonPhrase)).AsTask();

        private async Task<Either<ApiError, string>> GetResponseContent(HttpResponseMessage response)
            => Either<ApiError, string>.Right(await response.Content.ReadAsStringAsync());

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
