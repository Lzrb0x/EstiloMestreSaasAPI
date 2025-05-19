using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EstiloMestre.Infrastructure.Security.Tokens;

public abstract class JwtTokenHandler
{
    protected static SymmetricSecurityKey SecurityKey(string key)
    {
        var bytes = Encoding.UTF8.GetBytes(key);
        return new SymmetricSecurityKey(bytes);
    }
}
