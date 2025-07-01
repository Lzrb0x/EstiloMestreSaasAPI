using AutoMapper;
using EstiloMestre.Communication.DTOs;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using EstiloMestre.Domain.Repositories.Barbershop;

namespace EstiloMestre.Application.UseCases.Dashboard.ClientDashboard;

public class GetClientDashboardUseCase(
    IBarbershopRepository barbershopRepository,
    IMapper mapper
) : IGetClientDashboardUseCase
{
    public async Task<ResponseBarbershopJson> Execute()
    {
        var barbershops = await barbershopRepository.GetForDashboard();

        return new ResponseBarbershopJson
        {
            Barbershops = mapper.Map<List<ShortBarbershopDto>>(barbershops)
        };
    }
}


//TODO: for now, only return the first five barbershops.
//After, return the first five barbershops that the user scheduled an appointment with OR Top 5 nearest barbershops