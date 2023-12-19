using Microsoft.AspNetCore.Mvc;
using TinyFpTest.Services.Api;

namespace TinyFpTest.Controllers;

public class BaseController : ControllerBase
{
    protected static IActionResult FromApiError(ApiError error)
        => new ContentResult
        {
            StatusCode = (int)error.StatusCode,
            Content = error.Code
        };

    protected static IActionResult FromResult(object result)
        => new JsonResult(result);
}