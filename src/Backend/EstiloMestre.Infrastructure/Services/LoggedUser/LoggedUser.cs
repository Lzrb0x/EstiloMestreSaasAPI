using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EstiloMestre.Domain.Entities;
using EstiloMestre.Domain.Security.Tokens;
using EstiloMestre.Domain.Services.ILoggedUser;
using EstiloMestre.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace EstiloMestre.Infrastructure.Services.LoggedUser;

public class LoggedUser(EstiloMestreDbContext dbContext, ITokenProvider tokenProvider) : ILoggedUser
{
    public async Task<User> User()
    {
        var token = tokenProvider.Value();

        var tokenHandle = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandle.ReadJwtToken(token);

        var userIdentifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid)?.Value;

        var userIdentifierToGuid = Guid.Parse(userIdentifier!);

        return await dbContext.Users.AsNoTracking().FirstAsync(u => u.UserIdentifier.Equals(userIdentifierToGuid));
    }
}
