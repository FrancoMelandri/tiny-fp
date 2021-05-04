using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using Serilog;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using WireMock.Server;

namespace TinyFp.Complex.Setup
{
    [ExcludeFromCodeCoverage]
    public class BaseIntegrationTest : IDisposable
    {
        protected WireMockServer SearchServer { get; private set; }

        protected IntegrationTestServer TestServer;
        protected HttpClient Client => TestServer.Client;

        public IWebHostBuilder GetWebHostBuilder()
            => WebHost.CreateDefaultBuilder(Array.Empty<string>())
                .ConfigureKestrel(options => options.AddServerHeader = false)
                .UseStartup<TestStartup>()
                .UseSerilog();

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            SearchServer = WireMockServer.Start(port: 5002);
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            SearchServer.Stop();
            TestStartup.InMemoryRedisCache.ClearCache();
        }

        protected BaseIntegrationTest()
        {
            InitTestServer();
        }

        public void InitTestServer()
        {
            TestServer = new IntegrationTestServer(GetWebHostBuilder());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            TestServer.TestServer.Dispose();
            TestServer.Client.Dispose();            
        }
    }
}
