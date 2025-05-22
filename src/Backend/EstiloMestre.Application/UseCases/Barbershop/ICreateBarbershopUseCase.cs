using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Barbershop;

public interface ICreateBarbershopUseCase
{
    Task<ResponseRegisteredBarbershopJson> Execute(RequestRegisterBarbershopJson request);
}
