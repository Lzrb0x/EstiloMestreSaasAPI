using AutoMapper;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.User;
using EstiloMestre.Domain.Security.Cryptography;
using EstiloMestre.Domain.Security.Tokens;
using EstiloMestre.Exceptions.ExceptionsBase;
using FluentValidation.Results;

namespace EstiloMestre.Application.UseCases.User.Register;

public class RegisterUserUseCase(
    IUserRepository repository,
    IUnitOfWork uof,
    IMapper mapper,
    IPasswordEncripter passwordEncripter,
    IAccessTokenGenerator tokenGenerator)
    : IRegisterUserUseCase
{
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
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
            Tokens = new TokensDto
            {
                AccessToken = tokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }

    private async Task ValidateRequest(RequestRegisterUserJson request)
    {
        var validatorResult = await new RegisterUserValidator().ValidateAsync(request);


        var emailAlreadyExists = await repository.ExistActiveUserWithEmail(request.Email);
        if (emailAlreadyExists)
        {
            validatorResult.Errors.Add(new ValidationFailure(string.Empty,
                ResourceMessagesExceptions.EMAIL_ALREADY_EXISTS));
        }

        if (!validatorResult.IsValid)
        {
            throw new OnValidationException(validatorResult.Errors.Select(x => x.ErrorMessage).ToList());
        }
    }
}