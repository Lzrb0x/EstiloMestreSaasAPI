using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories.User;
using EstiloMestre.Domain.Security.Cryptography;
using EstiloMestre.Domain.Security.Tokens;
using EstiloMestre.Exceptions.ExceptionsBase;

namespace EstiloMestre.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(
        IUserReadOnlyRepository userReadOnlyRepository, IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator accessTokenGenerator
    )
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }


    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var encryptedPassword = _passwordEncripter.Encrypt(request.Password!);

        var user = await _userReadOnlyRepository.GetByEmailAndPassword(request.Email, encryptedPassword)
                   ?? throw new InvalidLoginException();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokensJson { AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier) }
        };
    }
}
