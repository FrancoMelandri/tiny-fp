using Microsoft.AspNetCore.Mvc;
using TinyFpTest.Services.Api;

namespace TinyFpTest.Controllers
{
    public class BaseController : ControllerBase
    {
        public static IActionResult FromApiError(ApiError _)
            => new ContentResult
            {
                StatusCode = (int)_.StatusCode,
                Content = _.Code
            };
    }
}
