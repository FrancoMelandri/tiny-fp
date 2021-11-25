using Microsoft.AspNetCore.Mvc;
using TinyFpTest.Services;
using TinyFp;

namespace TinyFpTest.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SearchController : BaseController
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public Task<IActionResult> Search([FromQuery]string forName)
            => _searchService
                .SearchProductsAsync(forName)
                .MatchAsync(
                    _ => new JsonResult(_), 
                    FromApiError
                );
    }
}
