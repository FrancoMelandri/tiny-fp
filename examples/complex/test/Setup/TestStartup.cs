using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using TinyFpTest.Complex;
using TinyFpTest.Services;

namespace TinyFp.Complex.Setup
{
    [ExcludeFromCodeCoverage]
    public class TestStartup : Startup
    {
        public static InMemoryRedisCache InMemoryRedisCache { get; } = new InMemoryRedisCache();

        public TestStartup(IConfiguration configuration)
                : base(configuration)
        {
        }
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.AddSingleton<ICache>(InMemoryRedisCache);
        }
    }
}
