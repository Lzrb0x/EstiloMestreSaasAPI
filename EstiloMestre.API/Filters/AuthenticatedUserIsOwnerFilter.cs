using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories.Owner;
using EstiloMestre.Domain.Repositories.User;
using EstiloMestre.Domain.Security.Tokens;
using EstiloMestre.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace EstiloMestre.API.Filters;

public class AuthenticatedUserIsOwnerFilter : TokenOnRequest, IAsyncAuthorizationFilter
{
    private readonly IOwnerRepository _ownerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAccessTokenValidator _tokenValidator;

    public AuthenticatedUserIsOwnerFilter(
        IAccessTokenValidator tokenValidator,
        IOwnerRepository ownerRepository,
        IUserRepository userRepository
    )
    {
        _ownerRepository = ownerRepository;
        _userRepository = userRepository;
        _tokenValidator = tokenValidator;
    }


    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = Token(context);

            var userIdentifier = _tokenValidator.ValidateAndGetUserIdentifier(token);
            var userExist = await _userRepository.ExistActiveUserWithIdentifier(userIdentifier);
            if (userExist == null)
                throw new EstiloMestreException(ResourceMessagesExceptions.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);

            var userIsOwner = await _ownerRepository.ExistActiveOwnerWithUserId(userExist.Id);
            if (userIsOwner == false)
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
