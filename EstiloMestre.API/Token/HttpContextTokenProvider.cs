using EstiloMestre.Domain.Security.Tokens;

namespace EstiloMestre.API.Token;

public class HttpContextTokenProvider : ITokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextTokenProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string Value()
    {
        var authentication = _httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

        return authentication["Bearer ".Length..].Trim();
    }
}
