using EstiloMestre.Communication.DTOs;
using EstiloMestre.Communication.Requests;

namespace EstiloMestre.Communication.Responses;

public class ResponseBarbershopJson
{
    public IList<ShortBarbershopDto> Barbershops { get; set; } = [];
}

