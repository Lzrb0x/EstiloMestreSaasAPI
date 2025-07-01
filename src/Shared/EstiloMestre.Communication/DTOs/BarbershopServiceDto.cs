namespace EstiloMestre.Communication.DTOs;

public class BarbershopServiceDto
{
    public long BarbershopServiceId { get; init; }
    public decimal Price { get; init; }
    public TimeSpan Duration { get; init; }
    public string? DescriptionOverride { get; init; } = string.Empty;

    public long BarbershopId { get; init; }
    public long ServiceId { get; init; }
}
