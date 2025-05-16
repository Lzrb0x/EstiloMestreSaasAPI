using System.ComponentModel.DataAnnotations.Schema;

namespace EstiloMestre.Domain.Entities;

[Table("Owners")]
public class Owner : EntityBase
{
    public IList<Barbershops> Barbershops { get; set; } = [];
    public long UserId { get; set; }
}