using Microsoft.Extensions.Options;

namespace me.Helpers;

public interface IHttpFactory
{
    HttpClient GetClient();
}
public class HttpFactory : IHttpFactory
{
    private readonly HttpClient _client;

    public HttpFactory(IOptions<HttpOptions> options)
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri(options.Value.BaseUrl)
        };
    }

    public HttpClient GetClient()
    {
        return _client;
    }
}

public class HttpOptions
{
    public string BaseUrl { get; set; }
}