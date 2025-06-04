using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Barbershop.Service.Register.List;

public interface IRegisterBarbershopServiceListUseCase
{
    Task<ResponseRegisteredBarbershopServiceListJson> Execute(
        RequestRegisterBarbershopServiceListJson request, long barbershopId
    );
}
