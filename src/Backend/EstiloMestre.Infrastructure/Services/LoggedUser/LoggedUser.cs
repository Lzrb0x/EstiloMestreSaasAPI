using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Security.Tokens;
using EstiloMestre.Domain.Services.ILoggedUser;
using EstiloMestre.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly EstiloMestreDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(EstiloMestreDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> User()
    {
        var token = _tokenProvider.Value();

        var tokenHandle = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandle.ReadJwtToken(token);

        var userIdentifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid)?.Value;

        var userIdentifierToGuid = Guid.Parse(userIdentifier!);

        return await _dbContext.Users.AsNoTracking().FirstAsync(u => u.UserIdentifier.Equals(userIdentifierToGuid));
    }
}
