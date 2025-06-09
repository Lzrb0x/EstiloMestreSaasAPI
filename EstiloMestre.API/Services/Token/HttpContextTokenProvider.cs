using EstiloMestre.Domain.Security.Tokens;

namespace EstiloMestre.API.Services.Token;

public class HttpContextTokenProvider(IHttpContextAccessor httpContextAccessor) : ITokenProvider
{
    public string Value()
    {
        var authentication = httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

        return authentication["Bearer ".Length..].Trim();
    }
}
