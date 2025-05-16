using AutoMapper;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Domain.Entities;

namespace EstiloMestre.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
    }

    private void RequestToDomain()
    {
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore()); //ignore, because it will be hashed  
    }

    private static void DomainToResponse()
    {
    }
}