using Microsoft.AspNetCore.Mvc;
using TinyFp.Extensions;

namespace TinyFpTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VersionController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;

        public VersionController(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Task<string> Get()
            => Task.FromResult("Hello world")
                .Tee(_ => _logger.Debug("Hello world"));
    }
}
