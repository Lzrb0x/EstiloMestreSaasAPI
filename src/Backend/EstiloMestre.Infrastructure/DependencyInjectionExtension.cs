using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.User;
using EstiloMestre.Domain.Security.Cryptography;
using EstiloMestre.Infrastructure.DataAccess;
using EstiloMestre.Infrastructure.DataAccess.Repositories;
using EstiloMestre.Infrastructure.Extensions;
using EstiloMestre.Infrastructure.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace EstiloMestre.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        AddRepositories(services);
        AddPasswordEncripter(services, config);
        AddDbContext(services, config);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.ConnectionString();
        var serverVersion = new MySqlServerVersion(new Version(9, 3, 0));
        services.AddDbContext<EstiloMestreDbContext>(dbOptions => { dbOptions.UseMySql(connectionString, serverVersion); });
    }

    private static void AddPasswordEncripter(IServiceCollection services, IConfiguration config)
    {
        var additionalKey = config.GetValue<string>("Settings:Password:AdditionalKey");

        services.AddScoped<IPasswordEncripter>(_ => new Sha512PasswordEncripter(additionalKey!));
    }
}