using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TinyFpTest.Services.Details;

namespace TinyFpTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DetailsController : BaseController
    {
        private readonly IDetailsService _detailsService;

        public DetailsController(IDetailsService detailsService)
        {
            _detailsService = detailsService;
        }

        [HttpGet]
        public Task<IActionResult> Details([FromQuery] string productName)
            => Task.FromResult((IActionResult)new OkResult());
    }
}
