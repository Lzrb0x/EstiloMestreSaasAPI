using System.ComponentModel.DataAnnotations.Schema;

namespace EstiloMestre.Domain.Entities;

[Table("Owners")]
public class Owner : EntityBase
{
    public long UserId { get; set; }
    public User User { get; set; } = null!;
    public IList<Barbershop> Barbershops { get; set; } = [];
}
