using EstiloMestre.API.Services;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories.Owner;
using EstiloMestre.Domain.Repositories.User;
using EstiloMestre.Domain.Security.Tokens;
using EstiloMestre.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace EstiloMestre.API.Filters;

public class OwnerFilter(
    IAccessTokenValidator tokenValidator,
    IOwnerRepository ownerRepository,
    IUserRepository userRepository,
    ITokenProvider tokenProvider,
    IRouteParameterExtractor routeParams
) : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = tokenProvider.Value();

            var userIdentifier = tokenValidator.ValidateAndGetUserIdentifier(token);
            var userExist = await userRepository.ExistActiveUserWithIdentifier(userIdentifier);
            if (userExist == null)
                throw new EstiloMestreException(ResourceMessagesExceptions.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);

            var userIsOwner = await ownerRepository.UserIsBarbershopOwner(userExist.Id, routeParams.BarbershopId());
            if (!userIsOwner)
                throw new EstiloMestreException(ResourceMessagesExceptions.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
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
