using System.Reflection;
using EstiloMestre.Domain.Repositories;
using EstiloMestre.Domain.Repositories.Barbershop;
using EstiloMestre.Domain.Repositories.BarbershopService;
using EstiloMestre.Domain.Repositories.Booking;
using EstiloMestre.Domain.Repositories.Employee;
using EstiloMestre.Domain.Repositories.Employee.BusinessHour;
using EstiloMestre.Domain.Repositories.Owner;
using EstiloMestre.Domain.Repositories.Service;
using EstiloMestre.Domain.Repositories.ServiceEmployee;
using EstiloMestre.Domain.Repositories.User;
using EstiloMestre.Domain.Security.Cryptography;
using EstiloMestre.Domain.Security.Tokens;
using EstiloMestre.Domain.Services.ILoggedUser;
using EstiloMestre.Infrastructure.DataAccess;
using EstiloMestre.Infrastructure.DataAccess.Repositories;
using EstiloMestre.Infrastructure.Extensions;
using EstiloMestre.Infrastructure.Security.Cryptography;
using EstiloMestre.Infrastructure.Security.Tokens.Access.Generator;
using EstiloMestre.Infrastructure.Security.Tokens.Access.Validator;
using EstiloMestre.Infrastructure.Services.LoggedUser;
using FluentMigrator.Runner;
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
        AddFluentMigrator(services, config);
        AddTokens(services, config);
        AddLoggedUser(services);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBarbershopRepository, BarbershopRepository>();
        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IBarbershopServiceRepository, BarbershopServiceRepository>();
        services.AddScoped<IServiceEmployeeRepository, ServiceEmployeeRepository>();
        services.AddScoped<IWorkingHourRepository, WorkingHourRepository>();
        services.AddScoped<IWorkingHourOverrideRepository, WorkingHourOverrideRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.ConnectionString();
        var serverVersion = new MySqlServerVersion(new Version(9, 3, 0));
        services.AddDbContext<EstiloMestreDbContext>(dbOptions =>
        {
            dbOptions.UseMySql(connectionString, serverVersion);
        });
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.ConnectionString();
        services.AddFluentMigratorCore()
            .ConfigureRunner(runner =>
            {
                runner.AddMySql5()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.Load("EstiloMestre.Infrastructure"))
                    .For
                    .All();
            });
    }

    private static void AddTokens(IServiceCollection services, IConfiguration config)
    {
        var expirationTime = config.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
        var signingKey = config.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(_ => new JwtTokenGenerator(expirationTime, signingKey!));
        services.AddScoped<IAccessTokenValidator>(_ => new JwtTokenValidator(signingKey!));
    }

    private static void AddPasswordEncripter(IServiceCollection services, IConfiguration config)
    {
        var additionalKey = config.GetValue<string>("Settings:Password:AdditionalKey");

        services.AddScoped<IPasswordEncripter>(_ => new Sha512PasswordEncripter(additionalKey!));
    }

    private static void AddLoggedUser(IServiceCollection services)
    {
        services.AddScoped<ILoggedUser, LoggedUser>();
    }
}
