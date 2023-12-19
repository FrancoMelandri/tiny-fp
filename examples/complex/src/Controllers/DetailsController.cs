using Microsoft.AspNetCore.Mvc;
using TinyFp;
using TinyFpTest.Services.Details;

namespace TinyFpTest.Controllers;

[ApiController]
[Route("[controller]")]
public class DetailsController(IDetailsDrivenPort detailsService) : BaseController
{
    [HttpGet]
    public Task<IActionResult> Details([FromQuery] string productName)
        => detailsService
            .GetDetailsAsync(productName)
            .MatchAsync(
                _ => FromResult(_),
                FromApiError
            );
}