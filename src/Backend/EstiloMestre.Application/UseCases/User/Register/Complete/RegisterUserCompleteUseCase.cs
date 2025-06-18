using AutoMapper;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.User;
using EstiloMestre.Domain.Security.Cryptography;
using EstiloMestre.Domain.Security.Tokens;
using EstiloMestre.Exceptions.ExceptionsBase;
using FluentValidation.Results;

namespace EstiloMestre.Application.UseCases.User.Register.Complete;

public class RegisterUserCompleteUseCase(
    IUserRepository repository,
    IUnitOfWork uof,
    IMapper mapper,
    IPasswordEncripter passwordEncripter,
    IAccessTokenGenerator tokenGenerator)
    : IRegisterUserCompleteUseCase
{
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterCompleteUserJson request)
    {
        await ValidateRequest(request);

        var user = mapper.Map<Domain.Entities.User>(request);
        user.Password = passwordEncripter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();
        user.IsComplete = true;

        await repository.Add(user);
        await uof.Commit();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokensJson
            {
                AccessToken = tokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }

    private async Task ValidateRequest(RequestRegisterCompleteUserJson request)
    {
        var validator = new RegisterUserCompleteValidator();
        var result = await validator.ValidateAsync(request);

        var emailAlreadyExists = await repository.ExistActiveUserWithEmail(request.Email);
        if (emailAlreadyExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesExceptions.EMAIL_ALREADY_EXISTS));
        }

        if (!result.IsValid)
        {
            throw new OnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
        }
    }
}
