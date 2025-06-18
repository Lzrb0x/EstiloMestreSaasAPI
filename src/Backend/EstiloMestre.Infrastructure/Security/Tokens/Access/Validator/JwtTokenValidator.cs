using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EstiloMestre.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;

namespace EstiloMestre.Infrastructure.Security.Tokens.Access.Validator;

public class JwtTokenValidator(string signingKey) : JwtTokenHandler, IAccessTokenValidator
{
    public Guid ValidateAndGetUserIdentifier(string token)
    {
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = SecurityKey(signingKey),
            ClockSkew = new TimeSpan(0)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

        var userIdentifier = principal.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        return Guid.Parse(userIdentifier);
    }
}