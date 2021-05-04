using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;
using TinyFp.Extensions;

namespace TinyFpTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VersionController : ControllerBase
    {
        private readonly ILogger _logger;

        public VersionController(ILogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Task<string> Get()
            => Task.FromResult("Hello world")
                .Tee(_ => _logger.Debug("Hello world"));
    }
}
