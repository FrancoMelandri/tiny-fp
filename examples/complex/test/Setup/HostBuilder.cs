using Microsoft.AspNetCore;
using Serilog;
using System.Diagnostics.CodeAnalysis;

namespace TinyFpTest.Complex
{
    [ExcludeFromCodeCoverage]
    public class HostBuilder<TStartup>
        where TStartup : class
    {
        protected Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureAppConfigurationHandler = (WebHostBuilderContext hostingContext, IConfigurationBuilder configurationBuilder) => { };

        public HostBuilder<TStartup> ConfigureAppConfiguration(Action<WebHostBuilderContext, IConfigurationBuilder> configureAppConfigurationHandler)
        {
            ConfigureAppConfigurationHandler = configureAppConfigurationHandler;
            return this;
        }

        public IWebHostBuilder CreateDefaultBuilder(string[] args)
            => WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) => config.AddJsonFile(args[0]))
                .ConfigureKestrel(options => options.AddServerHeader = false)
                .UseStartup<TStartup>()
                .UseSerilog();
    }
}
