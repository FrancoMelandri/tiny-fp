using Microsoft.Extensions.Http;
using Moq;
using System.Diagnostics.CodeAnalysis;
using TinyFpTest.Complex;
using TinyFpTest.Services;
using TinyFpTest.Services.Details;

namespace TinyFp.Complex.Setup
{
    [ExcludeFromCodeCoverage]
    public class TestStartup : Startup
    {
        public static InMemoryRedisCache InMemoryRedisCache { get; } = new InMemoryRedisCache();
        public static Mock<Serilog.ILogger> Logger { get; } = new Mock<Serilog.ILogger>();
        public static Mock<IDetailsRepository> DetailsRepository { get; } = new Mock<IDetailsRepository>();

        public TestStartup(IConfiguration configuration)
                : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddSingleton<ICache>(InMemoryRedisCache);
            services.AddSingleton(Logger.Object);
            services.AddSingleton(DetailsRepository.Object);

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
