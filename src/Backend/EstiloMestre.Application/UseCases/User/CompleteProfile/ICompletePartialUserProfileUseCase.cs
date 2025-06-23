using EstiloMestre.Communication.Requests;

namespace EstiloMestre.Application.UseCases.User.CompleteProfile;

public interface ICompletePartialUserProfileUseCase
{
    Task Execute(RequestCompletePartialUserProfileJson request);
}