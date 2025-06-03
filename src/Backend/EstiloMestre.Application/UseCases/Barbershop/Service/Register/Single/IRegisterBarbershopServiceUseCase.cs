using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Barbershop.Service.Register.Single;

public interface IRegisterBarbershopServiceUseCase
{
    Task<ResponseRegisteredBarbershopServiceJson> Execute(RequestBarbershopServiceJson request, long barbershopId);
}
