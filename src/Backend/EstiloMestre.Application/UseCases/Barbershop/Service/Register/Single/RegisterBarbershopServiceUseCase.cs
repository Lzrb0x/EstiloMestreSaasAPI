using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Barbershop.Service.Register.Single;

public class RegisterBarbershopServiceUseCase : IRegisterBarbershopServiceUseCase
{
    public async Task<ResponseRegisteredBarbershopServiceJson> Execute(
        RequestBarbershopServiceJson request, long barbershopId
    )
    {
        ValidadeRequest(request);
        
        

        return new ResponseRegisteredBarbershopServiceJson();
    }

    private static void ValidadeRequest(RequestBarbershopServiceJson request)
    {
        var result = new BarbershopServiceValidator().Validate(request);

        if (!result.IsValid) 
            throw new OnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
    }
}
