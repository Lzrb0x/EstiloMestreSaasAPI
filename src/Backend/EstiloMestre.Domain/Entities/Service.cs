using System.ComponentModel.DataAnnotations.Schema;

namespace EstiloMestre.Domain.Entities;

[Table("Services")]
public class Service : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

    public IList<BarbershopService> BarbershopServices { get; set; } = [];
}
