using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Service.Register;

public interface IRegisterServiceUseCase
{
    Task<ResponseRegisteredServiceJson> Execute(RequestServiceJson request);
}
