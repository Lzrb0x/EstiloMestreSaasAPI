using System.ComponentModel.DataAnnotations.Schema;

namespace EstiloMestre.Domain.Entities;

[Table("Barbershops")]
public class Barbershop : EntityBase
{
    public string BarbershopName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public long UserId { get; set; }
}
