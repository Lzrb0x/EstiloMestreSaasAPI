using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Login.DoLogin;

public interface IDoLoginUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}
