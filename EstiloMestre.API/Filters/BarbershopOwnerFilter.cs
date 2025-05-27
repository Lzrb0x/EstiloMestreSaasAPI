using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories.Owner;
using EstiloMestre.Domain.Repositories.User;
using EstiloMestre.Domain.Security.Tokens;
using EstiloMestre.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace EstiloMestre.API.Filters;

public class BarbershopOwnerFilter : TokenOnRequest, IAsyncAuthorizationFilter
{
    private readonly IAccessTokenValidator _tokenValidator;
    private readonly IOwnerRepository _ownerRepository;
    private readonly IUserRepository _userRepository;

    public BarbershopOwnerFilter(
        IAccessTokenValidator tokenValidator,
        IOwnerRepository ownerRepository,
        IUserRepository userRepository
    )
    {
        _tokenValidator = tokenValidator;
        _ownerRepository = ownerRepository;
        _userRepository = userRepository;
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

            var userIsOwner = await _ownerRepository.UserIsBarbershopOwner(userExist.Id, GetBarbershopId(context));
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

    private static long GetBarbershopId(AuthorizationFilterContext context)
    {
        var routeData = context.RouteData;

        if (!routeData.Values.TryGetValue("barbershopId", out var barbershopIdValue) 
            || barbershopIdValue == null)
            throw new EstiloMestreException(ResourceMessagesExceptions.BARBERSHOP_ID_NOT_FOUND_IN_ROUTE);
        if (long.TryParse(barbershopIdValue.ToString(), out var barbershopId))
        {
            return barbershopId;
        }

        throw new EstiloMestreException(ResourceMessagesExceptions.BARBERSHOP_ID_INVALID_IN_ROUTE);
    }
}
