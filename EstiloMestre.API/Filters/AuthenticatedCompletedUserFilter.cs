using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories.User;
using EstiloMestre.Domain.Security.Tokens;
using EstiloMestre.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace EstiloMestre.API.Filters;

public class AuthenticatedCompletedUserFilter(
    IAccessTokenValidator tokenValidator,
    IUserRepository repository,
    ITokenProvider tokenProvider
) : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = tokenProvider.Value();

            var userIdentifier = tokenValidator.ValidateAndGetUserIdentifier(token);
            var userExist = await repository.ExistActiveUserWithIdentifier(userIdentifier);
            if (userExist == null)
                throw new EstiloMestreException(ResourceMessagesExceptions.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
            if(!await repository.UserProfileIsComplete(userIdentifier))
                throw new EstiloMestreException(ResourceMessagesExceptions.USER_PROFILE_NOT_COMPLETE);
        } catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(
                new ResponseErrorJson(ResourceMessagesExceptions.TOKEN_EXPIRED)
                {
                    TokenIsExpired = true
                });
        } catch (EstiloMestreException ex)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Message));
        } catch
        {
            context.Result = new UnauthorizedObjectResult(
                new ResponseErrorJson(ResourceMessagesExceptions.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
        }
    }
}
