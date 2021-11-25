using TinyFp;

namespace TinyFpTest.Services.Api
{
    public interface IApiClient
    {
        Task<Either<ApiError, T>> GetAsync<T>(ApiRequest apiRequest);
    }
}
