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
    }

    private void DomainToResponse()
    {
        CreateMap<Barbershop, ResponseRegisteredBarbershopJson>();
        CreateMap<Employee, ResponseRegisteredEmployeeJson>();
        CreateMap<Service, ResponseRegisteredServiceJson>();
    }
}
