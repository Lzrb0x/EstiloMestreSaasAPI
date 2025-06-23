using EstiloMestre.Communication.Requests;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.User;
using EstiloMestre.Domain.Security.Cryptography;
using EstiloMestre.Domain.Services.ILoggedUser;
using EstiloMestre.Exceptions.ExceptionsBase;
using FluentValidation.Results;

namespace EstiloMestre.Application.UseCases.User.CompleteProfile;

public class CompletePartialUserProfileUseCase(
    IUserRepository userRepository,
    ILoggedUser loggedUser,
    IPasswordEncripter passwordEncripter,
    IUnitOfWork unitOfWork)
    : ICompletePartialUserProfileUseCase
{
    public async Task Execute(RequestCompletePartialUserProfileJson request)
    {
        await ValidateRequest(request);

        var partialUser = await loggedUser.User();
        var user = await userRepository.GetUserByIdentifier(partialUser.UserIdentifier);

        if (user is not { IsComplete: false })
            throw new BusinessRuleException(ResourceMessagesExceptions.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);

        user.Email = request.Email;
        user.Password = passwordEncripter.Encrypt(request.Password);
        user.IsComplete = true;

        userRepository.Update(user);
        await unitOfWork.Commit();
    }

    private async Task ValidateRequest(RequestCompletePartialUserProfileJson request)
    {
        var validatorResult = await new CompletePartialUserProfileValidator().ValidateAsync(request);

        var emailAlreadyExists = await userRepository.ExistActiveUserWithEmail(request.Email);
        if (emailAlreadyExists)
            validatorResult.Errors.Add(new ValidationFailure(string.Empty,
                ResourceMessagesExceptions.EMAIL_ALREADY_EXISTS));

        if (!validatorResult.IsValid)
            throw new OnValidationException(validatorResult.Errors.Select(x => x.ErrorMessage).ToList());
    }
}