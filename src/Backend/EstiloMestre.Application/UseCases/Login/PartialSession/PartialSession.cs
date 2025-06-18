using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.User;
using EstiloMestre.Domain.Security.Tokens;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Login.PartialSession;

public class PartialSession(
    IUserRepository userRepository,
    IAccessTokenGenerator tokenGenerator,
    IUnitOfWork unitOfWork)
    : IPartialSession
{
    public async Task<ResponseRegisteredUserJson> Execute(RequestPartialSessionUserJson request)
    {
        ValidateRequest(request);

        var user = await userRepository.GetUserByPhone(request.Phone);
        if (user is null)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new BusinessRuleException(ResourceMessagesExceptions.NAME_EMPTY);

            user = new Domain.Entities.User
            {
                Name = request.Name,
                Phone = request.Phone,
                IsComplete = false,
                UserIdentifier = Guid.NewGuid(),
            };

            await userRepository.Add(user);
            await unitOfWork.Commit();
        }
        else if (user.IsComplete)
            throw new BusinessRuleException(ResourceMessagesExceptions
                .USER_WITH_COMPLETE_PROFILE_ALREADY_EXISTS_WITH_PHONE);

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokensJson
            {
                AccessToken = tokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }

    private static void ValidateRequest(RequestPartialSessionUserJson request)
    {
        var validatorResult = new PartialSessionValidator().Validate(request);
        if (!validatorResult.IsValid)
            throw new OnValidationException(validatorResult.Errors.Select(x => x.ErrorMessage).ToList());
    }
}