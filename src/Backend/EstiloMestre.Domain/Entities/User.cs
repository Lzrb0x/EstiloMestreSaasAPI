using System.ComponentModel.DataAnnotations.Schema;

namespace EstiloMestre.Domain.Entities;

[Table("Users")]
public class User : EntityBase
{
  public Guid UserIdentifier { get; set; }
  public string Name { get; set; } = string.Empty;
  public string? Email { get; set; } = string.Empty;
  public string? Password { get; set; } = string.Empty;
  
  public string Phone { get; set; } = string.Empty;
  
  public bool IsComplete { get; set; }
  
  public IList<Booking> Bookings { get; set; } = [];
}
