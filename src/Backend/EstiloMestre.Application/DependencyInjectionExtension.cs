using EstiloMestre.Application.Services.AutoMapper;
using EstiloMestre.Application.UseCases.Barbershop.Employee.Register;
using EstiloMestre.Application.UseCases.Barbershop.Register;
using EstiloMestre.Application.UseCases.Barbershop.Service.Register;
using EstiloMestre.Application.UseCases.Login.DoLogin;
using EstiloMestre.Application.UseCases.Owner.Register;
using EstiloMestre.Application.UseCases.Service.Register;
using EstiloMestre.Application.UseCases.User.Register;
using Microsoft.Extensions.DependencyInjection;

namespace EstiloMestre.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
        AddAutoMapper(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IRegisterBarbershopUseCase, RegisterBarbershopUseCase>();
        services.AddScoped<IRegisterOwnerUseCase, RegisterOwnerUseCase>();
        services.AddScoped<IRegisterEmployeeUseCase, RegisterEmployeeUseCase>();
        services.AddScoped<IRegisterServiceUseCase, RegisterServiceUseCase>();
        services.AddScoped<IRegisterBarbershopServiceUseCase, RegisterBarbershopServiceUseCase>();
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddScoped(_ => new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper());
    }
}
