using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Moq;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using TinyFpTest.Complex;
using TinyFpTest.Services;

namespace TinyFp.Complex.Setup
{
    [ExcludeFromCodeCoverage]
    public class TestStartup : Startup
    {
        public static InMemoryRedisCache InMemoryRedisCache { get; } = new InMemoryRedisCache();
        public static Mock<ILogger> Logger { get; } = new Mock<ILogger>();

        public TestStartup(IConfiguration configuration)
                : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddSingleton<ICache>(InMemoryRedisCache);
            services.AddSingleton(Logger.Object);

            services.ConfigureAll<HttpClientFactoryOptions>(options =>
            {
                options.HttpMessageHandlerBuilderActions.Add(builder =>
                {
                    builder.AdditionalHandlers.Add(new IntegrationTestHttpRequestHandler());
                });
            });
        }
    }
}
