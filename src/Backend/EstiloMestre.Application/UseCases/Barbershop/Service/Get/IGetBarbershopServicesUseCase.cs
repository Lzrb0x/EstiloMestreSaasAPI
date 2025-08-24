using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Barbershop.Service.Get;

public interface IGetBarbershopServicesUseCase
{
    Task<ResponseBarbershopServices> Execute(long barbershopId);
}