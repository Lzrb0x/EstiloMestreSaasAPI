using AutoMapper;
using EstiloMestre.Communication.DTOs;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories.BarbershopService;

namespace EstiloMestre.Application.UseCases.Barbershop.Service.Get;

public class GetBarbershopServicesUseCase(IBarbershopServiceRepository barbershopServiceRepository, IMapper mapper)
    : IGetBarbershopServicesUseCase
{
    public async Task<ResponseBarbershopServices> Execute(long barbershopId)
    {
        var barbershopServices = await barbershopServiceRepository.GetBarbershopServicesByBarbershop(barbershopId);

        return new ResponseBarbershopServices
        {
            BarbershopServices = barbershopServices.Select(mapper.Map<BarbershopServiceDto>).ToList()
        };
    }
}