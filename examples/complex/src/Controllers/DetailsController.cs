using Microsoft.AspNetCore.Mvc;
using TinyFp;
using TinyFpTest.Services.Details;

namespace TinyFpTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DetailsController : BaseController
    {
        private readonly IDetailsDrivenPort _detailsService;

        public DetailsController(IDetailsDrivenPort detailsService)
        {
            _detailsService = detailsService;
        }

        [HttpGet]
        public Task<IActionResult> Details([FromQuery] string productName)
            => _detailsService
                .GetDetailsAsync(productName)
                .MatchAsync(
                    _ => new JsonResult(_),
                    FromApiError
                );
    }
}
