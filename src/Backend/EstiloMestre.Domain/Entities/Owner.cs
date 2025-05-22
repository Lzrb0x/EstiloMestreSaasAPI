using System.ComponentModel.DataAnnotations.Schema;

namespace EstiloMestre.Domain.Entities;

[Table("Owners")]
public class Owner : EntityBase
{
    public long UserId { get; set; }
    public IList<Barbershop> Barbershops { get; set; } = [];
}
