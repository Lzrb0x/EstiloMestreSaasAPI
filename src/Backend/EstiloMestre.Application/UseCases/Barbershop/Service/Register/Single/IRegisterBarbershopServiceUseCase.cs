using EstiloMestre.Communication.DTOs;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Barbershop.Service.Register.Single;

public interface IRegisterBarbershopServiceUseCase
{
    Task<BarbershopServiceDto> Execute(RequestBarbershopServiceJson request, long barbershopId);
}
