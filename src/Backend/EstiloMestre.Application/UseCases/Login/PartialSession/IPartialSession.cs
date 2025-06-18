using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Login.PartialSession;

public interface IPartialSession
{
    Task<ResponseRegisteredUserJson> Execute(RequestPartialSessionUserJson request);
}