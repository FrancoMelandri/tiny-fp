using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace TinyFp.Complex.Setup
{
    public class IntegrationTestServer
    {
        public TestServer TestServer { get; private set; }
        public HttpClient Client { get; private set; }

        public IntegrationTestServer(IWebHostBuilder webHostBuilder)
        {
            TestServer = new TestServer(webHostBuilder);
            Client = TestServer.CreateClient();
        }
    }
}
