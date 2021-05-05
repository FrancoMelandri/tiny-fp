using System.Net;

namespace TinyFpTest.Services.Api
{
    public class ApiError
    {
        public HttpStatusCode StatusCode { get; private set; }
        public string Code { get; private set; }
        public string Description { get; private set; }

        public ApiError()
        {

        }

        public static ApiError Create(HttpStatusCode statusCode, string code, string description)
        => new()
            {
                StatusCode = statusCode,
                Code = code,
                Description = description,
            };
    }
}
