using EstiloMestre.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EstiloMestre.API.Filters;

public abstract class TokenOnRequest
{
    protected static string Token(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString(); //token

        if (string.IsNullOrWhiteSpace(authentication))
            throw new EstiloMestreException(ResourceMessagesExceptions.NO_TOKEN);

        return authentication["Bearer ".Length..].Trim();
    }
}
