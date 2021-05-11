using System.Net;
using TinyFpTest.Services.Api;

namespace TinyFpTest.Constants
{
    public static class Errors
    {
        public static readonly ApiError NotFoundError = ApiError.Create(HttpStatusCode.NotFound, "not_found", "product not found");
        public static readonly ApiError EmptyServerError = ApiError.Create(HttpStatusCode.BadRequest, "no_content", "no content from server");
        public static readonly ApiError ExceptionCallingService = ApiError.Create(HttpStatusCode.InternalServerError, "exception_on_Service", "exception calling external service");
        public static readonly ApiError InvalidInput = ApiError.Create(HttpStatusCode.BadRequest, "bad_request", "input is not valid");
    }
}
