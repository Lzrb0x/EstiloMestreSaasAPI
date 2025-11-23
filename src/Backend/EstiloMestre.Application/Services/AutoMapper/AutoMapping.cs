using AutoMapper;
using EstiloMestre.Communication.DTOs;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Entities;

namespace EstiloMestre.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        DomainToResponse();
    }

    private void RequestToDomain()
    {
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());
        CreateMap<RequestRegisterBarbershopJson, Barbershop>();
        CreateMap<RequestServiceJson, Service>();
        CreateMap<RequestBarbershopServiceJson, BarbershopService>()
            .ForMember(dest => dest.BarbershopId, opt => opt.Ignore());
        CreateMap<RequestRegisterServiceEmployeeJson, ServiceEmployee>()
            .ForMember(dest => dest.EmployeeId, opt => opt.Ignore());
        CreateMap<RequestWorkingHourJson, EmployeeWorkingHour>()
            .ForMember(dest => dest.EmployeeId, opt => opt.Ignore());
        CreateMap<RequestWorkingHourOverrideJson, EmployeeWorkingHourOverride>()
            .ForMember(dest => dest.EmployeeId, opt => opt.Ignore());
    }

    private void DomainToResponse()
    {
        CreateMap<Barbershop, ResponseRegisteredBarbershopJson>();
        CreateMap<Employee, ResponseRegisteredEmployeeJson>();
        CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.BarberShopId, opt => opt.MapFrom(src => src.BarberShopId));
        CreateMap<Service, ResponseRegisteredServiceJson>();
        CreateMap<BarbershopService, BarbershopServiceDto>()
            .ForMember(dest => dest.DescriptionOverride,
                opt => opt.MapFrom(src => src.DescriptionOverride ?? string.Empty))
            .ForMember(dest => dest.BarbershopServiceId, opt => opt.MapFrom(src => src.Id));
        CreateMap<Owner, ResponseRegisteredOwnerJson>();
        CreateMap<ServiceEmployee, ResponseRegisteredServiceEmployeeJson>();
        CreateMap<Service, GlobalServiceDto>();
        CreateMap<Barbershop, ShortBarbershopDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.BarbershopName))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
    }
}