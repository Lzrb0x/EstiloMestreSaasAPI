using Microsoft.Extensions.Configuration;

namespace EstiloMestre.Infrastructure.Extensions;

public static class ConfigurationExtension
{
    public static string ConnectionString(this IConfiguration config)
    {
        return config.GetConnectionString("Connection")!;
    }
}