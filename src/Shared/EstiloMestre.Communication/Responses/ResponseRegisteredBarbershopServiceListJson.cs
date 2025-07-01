using EstiloMestre.Communication.DTOs;

namespace EstiloMestre.Communication.Responses;

public class ResponseRegisteredBarbershopServiceListJson
{
    public IList<BarbershopServiceDto> BarbershopServices { get; set; } = [];
}
