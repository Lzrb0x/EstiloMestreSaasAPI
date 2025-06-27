using EstiloMestre.API.Services;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories.Barbershop;
using EstiloMestre.Domain.Repositories.User;
using EstiloMestre.Domain.Security.Tokens;
using EstiloMestre.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace EstiloMestre.API.Filters;

public class OwnerOrEmployeeByBarbershopFilter(
    IAccessTokenValidator tokenValidator,
    IBarbershopRepository barbershopRepository,
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

            var userIsOwnerOrEmployee = await barbershopRepository.UserIsOwnerOrEmployee(userExist.Id,
                routeParams.BarbershopId(), routeParams.EmployeeId());
            if (!userIsOwnerOrEmployee)
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

//TODO: aqui a uma vulnerabilidade:
// se o dono acessar esse authorize, ele terá acesso a funcionarios de outras barbearias,
// podera cadastrar horas de trabalho, serviços etc...
// + Isso deve ser corrigido, talvez no filtro de autorização, verificando se aquele funcionario pertence
// a barbearia do dono.
