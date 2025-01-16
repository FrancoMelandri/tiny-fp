namespace TinyFpTest.Services.Api;

public struct ApiRequest
{
    public string Url { get; }
    public int Timeout { get; }
    public List<(string name, string value)> Headers { get; }

    private ApiRequest(string baseUrl, List<(string name, string value)> headers, int timeout)
    {
        Url = baseUrl;
        Headers = headers;
        Timeout = timeout;                 
    }

    public static ApiRequest Create()
        => new(string.Empty, [], 5000);

    public ApiRequest WithUrl(string url)
        => new(url, Headers, Timeout);

    public ApiRequest WithHeaders(List<(string name, string value)> headers)
        => new(Url, headers, Timeout);

    public ApiRequest WithTimeout(int timeout)
        => new(Url, Headers, timeout);
}