using System.ComponentModel.DataAnnotations.Schema;

namespace EstiloMestre.Domain.Entities;

[Table("BarbershopServices")]
public class BarbershopService : EntityBase
{
    public decimal Price { get; set; }
    
    public TimeSpan Duration { get; set; }
    
    public string? DescriptionOverride { get; set; } = string.Empty;
    
    public long BarbershopId { get; set; }
    
    public long ServiceId { get; set; }
    
    public IList<ServiceEmployee> ServicesEmployees { get; set; } = [];

    public Barbershop Barbershop { get; set; } = null!;
    public Service Service { get; set; } = null!;
}
