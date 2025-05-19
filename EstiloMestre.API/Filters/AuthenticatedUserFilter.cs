using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories.User;
using EstiloMestre.Domain.Security.Tokens;
using EstiloMestre.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace EstiloMestre.API.Filters;

public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
{
    private readonly IAccessTokenValidator _tokenValidator;
    private readonly IUserReadOnlyRepository _repository;

    public AuthenticatedUserFilter(IAccessTokenValidator tokenValidator, IUserReadOnlyRepository repository)
    {
        _tokenValidator = tokenValidator;
        _repository = repository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context);

            var userIdentifier = _tokenValidator.ValidateAndGetUserIdentifier(token);
            var userExist = await _repository.ExistActiveUserWithIdentifier(userIdentifier);
            if (!userExist) throw new EstiloMestreException(ResourceMessagesExceptions.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
        }
        catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson("Token is expired") { TokenIsExpired = true });
        }
        catch (EstiloMestreException ex)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Message));
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(
                new ResponseErrorJson(ResourceMessagesExceptions.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
        }
    }

    private static string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString(); //token

        if (string.IsNullOrWhiteSpace(authentication)) throw new EstiloMestreException(ResourceMessagesExceptions.NO_TOKEN);

        return authentication["Bearer ".Length..].Trim();
    }
}
