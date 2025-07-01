using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories.User;
using EstiloMestre.Domain.Security.Cryptography;
using EstiloMestre.Domain.Security.Tokens;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase(
    IUserRepository userRepository,
    IPasswordEncripter passwordEncripter,
    IAccessTokenGenerator accessTokenGenerator)
    : IDoLoginUseCase
{
    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var encryptedPassword = passwordEncripter.Encrypt(request.Password!);

        var user = await userRepository.GetByEmailAndPassword(request.Email, encryptedPassword)
                   ?? throw new InvalidLoginException();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new TokensDto { AccessToken = accessTokenGenerator.Generate(user.UserIdentifier) }
        };
    }
}
