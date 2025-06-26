using EstiloMestre.Communication.Requests;

namespace EstiloMestre.Communication.Responses;

public class ResponseBarbershopJson
{
    public IList<ResponseShortBarbershopJson> Barbershops { get; set; } = [];
}

