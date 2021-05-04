using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using TinyFpTest.Configuration;

namespace TinyFpTest.Complex
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        protected IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            InitializeSerilog(services);
        }

        private void InitializeSerilog(IServiceCollection services)
        {
            var config = Configuration
                .GetSection(typeof(SerilogConfiguration).Name).Get<SerilogConfiguration>();
            var logConfig = new LoggerConfiguration();
            var serilog = logConfig.CreateLogger();
            services.AddSingleton<ILogger>(serilog);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
