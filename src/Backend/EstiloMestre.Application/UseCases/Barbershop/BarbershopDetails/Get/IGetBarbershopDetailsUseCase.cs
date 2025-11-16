using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Barbershop.BarbershopDetails.Get;

public interface IGetBarbershopDetailsUseCase
{
    Task<ResponseBarbershopDetailsJson> Execute(long barbershopId);
}