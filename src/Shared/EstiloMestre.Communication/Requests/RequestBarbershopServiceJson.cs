namespace EstiloMestre.Communication.Requests;

public class RequestBarbershopServiceJson
{
    public long ServiceId { get; set; }
    public decimal Price { get; set; }
    public TimeSpan Duration { get; set; }
    public string? DescriptionOverride { get; set; } = string.Empty;
}
