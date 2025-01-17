﻿using TinyFp.Extensions;

namespace TinyFp.Complex.Setup;

public class IntegrationTestHttpRequestHandler : DelegatingHandler
{
    private static readonly List<HttpRequestMessage> _requestsReceived = [];
    public static IEnumerable<HttpRequestMessage> RequestsReceived => _requestsReceived;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        => base.SendAsync(request, cancellationToken)
            .Tee(_ => _requestsReceived.Add(request));

    public static void Reset() => _requestsReceived.Clear();
}