using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;

namespace TinyFpTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Version : ControllerBase
    {
        private readonly ILogger _logger;

        public Version(ILogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Task<string> Get()
            => Task.FromResult("Hello world");
    }
}
