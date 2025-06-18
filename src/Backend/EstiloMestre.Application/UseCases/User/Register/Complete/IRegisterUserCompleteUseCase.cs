using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.User.Register.Complete;

public interface IRegisterUserCompleteUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterCompleteUserJson request);
}