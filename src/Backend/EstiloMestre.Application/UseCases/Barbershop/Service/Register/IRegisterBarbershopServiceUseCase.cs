using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Barbershop.Service.Register;

public interface IRegisterBarbershopServiceUseCase
{
    Task<ResponseRegisteredBarbershopServiceJson> Execute(RequestRegisterBarbershopServiceJson request, long barbershopId);
}
