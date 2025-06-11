using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Service.Get;

public interface IGetAllGlobalServicesUseCase
{
    Task<ResponseGlobalServicesList> Execute();
}