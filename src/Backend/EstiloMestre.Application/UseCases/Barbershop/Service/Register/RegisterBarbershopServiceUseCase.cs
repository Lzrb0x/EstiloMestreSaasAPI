using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories.BarbershopService;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Barbershop.Service.Register;

public class RegisterBarbershopServiceUseCase(IBarbershopServiceRepository barbershopServicesRepository)
    : IRegisterBarbershopServiceUseCase
{
    public async Task<ResponseRegisteredServiceJson> Execute(
        RequestRegisterBarbershopServiceJson request, long barbershopId
    )
    {
        await ValidateRequest(request);

        var barbershopServicesDto = request.BarbershopServices.Select(service => new Domain.DTOs.BarbershopServiceDto
        {
            Price = service.Price,
            Duration = service.Duration,
            DescriptionOverride = service.DescriptionOverride,
            BarbershopId = barbershopId,
            ServiceId = service.ServiceId
        }).ToList();

        
        return new ResponseRegisteredServiceJson();
    }

    private static async Task ValidateRequest(RequestRegisterBarbershopServiceJson request)
    {
        var result = await new RegisterBarbershopServiceValidator().ValidateAsync(request);

        if (result.IsValid is false)
            throw new OnValidationException(result.Errors.Select(error => error.ErrorMessage).ToList());
    }
}
