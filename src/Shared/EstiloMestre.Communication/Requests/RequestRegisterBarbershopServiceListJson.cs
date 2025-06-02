namespace EstiloMestre.Communication.Requests;

public class RequestRegisterBarbershopServiceListJson
{
    public IList<RequestBarbershopServiceJson> BarbershopServices { get; set; } = [];
}
