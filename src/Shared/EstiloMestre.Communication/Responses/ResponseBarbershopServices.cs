using EstiloMestre.Communication.DTOs;

namespace EstiloMestre.Communication.Responses;

public class ResponseBarbershopServices
{
    public IList<BarbershopServiceDto> BarbershopServices { get; set; } = [];
}