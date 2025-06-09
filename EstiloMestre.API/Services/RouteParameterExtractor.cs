using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.API.Services;

public class RouteParameterExtractor(IHttpContextAccessor context) : IRouteParameterExtractor
{
    public long BarbershopId()
    {
        if (!context.HttpContext!.Request.RouteValues.TryGetValue("barbershopId", out var barbershopIdValue)
         || barbershopIdValue == null)
            throw new EstiloMestreException(ResourceMessagesExceptions.BARBERSHOP_ID_NOT_FOUND_IN_ROUTE);
        if (long.TryParse(barbershopIdValue.ToString(), out var barbershopId))
        {
            return barbershopId;
        }

        throw new EstiloMestreException(ResourceMessagesExceptions.BARBERSHOP_ID_INVALID_IN_ROUTE);
    }
}
