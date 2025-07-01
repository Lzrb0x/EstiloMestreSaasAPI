using AutoMapper;
using EstiloMestre.Communication.DTOs;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories.Service;

namespace EstiloMestre.Application.UseCases.Service.Get;

public class GetAllGlobalServicesUseCase(IServiceRepository serviceRepository, IMapper mapper)
    : IGetAllGlobalServicesUseCase
{
    public async Task<ResponseGlobalServicesList> Execute()
    {
        var services = await serviceRepository.GetAllGlobalServices();

        return new ResponseGlobalServicesList
        {
            GlobalServices = services.Select(service => mapper.Map<GlobalServiceDto>(service)).ToList()
        };
    }
}