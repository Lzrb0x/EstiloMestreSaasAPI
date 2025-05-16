using System.Security.Cryptography;
using System.Text;
using EstiloMestre.Domain.Security.Cryptography;

namespace EstiloMestre.Infrastructure.Security.Cryptography;

public class Sha512PasswordEncripter : IPasswordEncripter
{
    private readonly string _additionalKey;
    public Sha512PasswordEncripter(string additionalKey)
    {
        _additionalKey = additionalKey;
    }
    
    public string Encrypt(string? password)
    {
        var newPassword = $"{password}{_additionalKey}";
        var bytes = Encoding.UTF8.GetBytes(newPassword);
        var hashBytes = SHA256.HashData(bytes);
        
        return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (var b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        
        return sb.ToString();
    }
}