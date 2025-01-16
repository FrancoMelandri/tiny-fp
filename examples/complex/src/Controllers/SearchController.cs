using Microsoft.AspNetCore.Mvc;
using TinyFp;
using TinyFpTest.Services;

namespace TinyFpTest.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController(ISearchService searchService) : BaseController
{
    [HttpGet]
    public Task<IActionResult> Search([FromQuery]string forName)
        => searchService
            .SearchProductsAsync(forName)
            .MatchAsync(
                FromResult,
                FromApiError
            );
}