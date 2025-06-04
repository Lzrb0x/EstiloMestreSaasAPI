using AutoMapper;
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
        CreateMap<RequestRegisterUserJson, User>().ForMember(dest => dest.Password, opt => opt.Ignore());
        CreateMap<RequestRegisterBarbershopJson, Barbershop>();
        CreateMap<RequestServiceJson, Service>();
        CreateMap<RequestBarbershopServiceJson, BarbershopService>()
           .ForMember(dest => dest.BarbershopId, opt => opt.Ignore());
    }

    private void DomainToResponse()
    {
        CreateMap<Barbershop, ResponseRegisteredBarbershopJson>();
        CreateMap<Employee, ResponseRegisteredEmployeeJson>();
        CreateMap<Service, ResponseRegisteredServiceJson>();
        CreateMap<BarbershopService, ResponseRegisteredBarbershopServiceJson>()
           .ForMember(dest => dest.DescriptionOverride,
                opt => opt.MapFrom(src => src.DescriptionOverride ?? string.Empty))
           .ForMember(dest => dest.BarbershopServiceId, opt => opt.MapFrom(src => src.Id));
    }
}
