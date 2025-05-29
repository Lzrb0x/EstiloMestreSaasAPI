namespace EstiloMestre.Communication.Requests;

public class RequestRegisterBarbershopServiceJson
{
    public IList<RequestBarbershopServiceJson> BarbershopServices { get; set; } = [];
}
