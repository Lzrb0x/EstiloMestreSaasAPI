using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;

namespace EstiloMestre.Application.UseCases.Owner.Register;

public interface IRegisterOwnerUseCase
{
    Task<ResponseRegisteredOwnerJson> Execute(long userId);
}
