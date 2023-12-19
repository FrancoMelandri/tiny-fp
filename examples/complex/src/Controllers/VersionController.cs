using Microsoft.AspNetCore.Mvc;
using TinyFp.Extensions;

namespace TinyFpTest.Controllers;

[ApiController]
[Route("[controller]")]
public class VersionController(Serilog.ILogger logger) : ControllerBase
{
    [HttpGet]
    public Task<string> Get()
        => Task.FromResult("Hello world")
            .Tee(_ => logger.Debug("Hello world"));
}