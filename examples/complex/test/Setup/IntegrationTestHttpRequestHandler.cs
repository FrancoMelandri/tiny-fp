using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TinyFp.Complex.Setup
{
    public class IntegrationTestHttpRequestHandler : DelegatingHandler
    {
        private static readonly List<HttpRequestMessage> _requestsReceived = new List<HttpRequestMessage>();
        public static IEnumerable<HttpRequestMessage> RequestsReceived => _requestsReceived;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _requestsReceived.Add(request);
            return base.SendAsync(request, cancellationToken);
        }

        public static void Reset() => _requestsReceived.Clear();
    }
}
