using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Barbershop.Register;

public interface IRegisterBarbershopUseCase
{
    Task<ResponseRegisteredBarbershopJson> Execute(RequestRegisterBarbershopJson request);
}
