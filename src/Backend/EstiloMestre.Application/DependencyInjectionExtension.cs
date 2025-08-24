using EstiloMestre.Application.Services.AutoMapper;
using EstiloMestre.Application.Services.GetEmployeeWorkingBlocks;
using EstiloMestre.Application.UseCases.Barbershop.Employee.BusinessHour.WorkingHour;
using EstiloMestre.Application.UseCases.Barbershop.Employee.BusinessHour.WorkingHour.Set;
using EstiloMestre.Application.UseCases.Barbershop.Employee.BusinessHour.WorkingHourOverride.Set;
using EstiloMestre.Application.UseCases.Barbershop.Employee.Get.GetByService;
using EstiloMestre.Application.UseCases.Barbershop.Employee.Register;
using EstiloMestre.Application.UseCases.Barbershop.Employee.Register.OwnerAsEmployee;
using EstiloMestre.Application.UseCases.Barbershop.Employee.Register.UserAsEmployee;
using EstiloMestre.Application.UseCases.Barbershop.Employee.ServiceEmployee.Register;
using EstiloMestre.Application.UseCases.Barbershop.Employee.Slots;
using EstiloMestre.Application.UseCases.Barbershop.Register;
using EstiloMestre.Application.UseCases.Barbershop.Service.Get;
using EstiloMestre.Application.UseCases.Barbershop.Service.Register.List;
using EstiloMestre.Application.UseCases.Barbershop.Service.Register.Single;
using EstiloMestre.Application.UseCases.Booking.Create;
using EstiloMestre.Application.UseCases.Dashboard.ClientDashboard;
using EstiloMestre.Application.UseCases.Login.DoLogin;
using EstiloMestre.Application.UseCases.Login.PartialSession;
using EstiloMestre.Application.UseCases.Owner.Register;
using EstiloMestre.Application.UseCases.Service.Get;
using EstiloMestre.Application.UseCases.Service.Register;
using EstiloMestre.Application.UseCases.User.CompleteProfile;
using EstiloMestre.Application.UseCases.User.Register;
using Microsoft.Extensions.DependencyInjection;

namespace EstiloMestre.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
        AddAutoMapper(services);
        AddHelpers(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IRegisterBarbershopUseCase, RegisterBarbershopUseCase>();
        services.AddScoped<IRegisterOwnerUseCase, RegisterOwnerUseCase>();
        services.AddScoped<IRegisterEmployeeUseCase, RegisterEmployeeUseCase>();
        services.AddScoped<IRegisterServiceUseCase, RegisterServiceUseCase>();
        services.AddScoped<IRegisterBarbershopServiceListUseCase, RegisterBarbershopServiceListUseCase>();
        services.AddScoped<IRegisterBarbershopServiceUseCase, RegisterBarbershopServiceUseCase>();
        services.AddScoped<IRegisterServiceEmployeeUseCase, RegisterServiceEmployeeUseCase>();
        services.AddScoped<IGetAllGlobalServicesUseCase, GetAllGlobalServicesUseCase>();
        services.AddScoped<IRegisterOwnerAsEmployeeUseCase, RegisterOwnerAsEmployeeUseCase>();
        services.AddScoped<IGetClientDashboardUseCase, GetClientDashboardUseCase>();
        services.AddScoped<IPartialSession, PartialSession>();
        services.AddScoped<ICompletePartialUserProfileUseCase, CompletePartialUserProfileUseCase>();
        services.AddScoped<ISetWorkingHourUseCase, SetWorkingHourUseCase>();
        services.AddScoped<ISetWorkingHourOverrideUseCase, SetWorkingHourOverrideUseCase>();
        services.AddScoped<IGetEmployeeSlotsUseCase, GetEmployeeSlotsUseCase>();
        services.AddScoped<ICreateBookingUseCase, CreateBookingUseCase>();
        services.AddScoped<IGetBarbershopServicesUseCase, GetBarbershopServicesUseCase>();
        services.AddScoped<IGetEmployeesByService, GetEmployeesByService>();
    }

    private static void AddHelpers(IServiceCollection services)
    {
        services.AddScoped<EmployeeRegistrationHandler>();
        services.AddScoped<GetEmployeeWorkingBlocks>();
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddScoped(_ =>
            new AutoMapper.MapperConfiguration(options => { options.AddProfile(new AutoMapping()); }).CreateMapper());
    }
}