namespace EstiloMestre.Communication.Requests;

public class RequestRegisterBarbershopJson
{
    public string BarbershopName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
}
