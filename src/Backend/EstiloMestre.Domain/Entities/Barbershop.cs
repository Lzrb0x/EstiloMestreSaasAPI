using System.ComponentModel.DataAnnotations.Schema;

namespace EstiloMestre.Domain.Entities;

[Table("Barbershops")]
public class Barbershop : EntityBase
{
    public string BarbershopName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public IList<Employee> Employees { get; set; } = [];

    public long OwnerId { get; set; }
    public Owner Owner { get; set; } = null!;
}
