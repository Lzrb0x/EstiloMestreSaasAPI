using System.ComponentModel.DataAnnotations.Schema;

namespace EstiloMestre.Domain.Entities;

[Table("ServicesEmployee")]
public class ServiceEmployee : EntityBase
{
    public long EmployeeId { get; set; }
    public long BarbershopServiceId { get; set; }
}
