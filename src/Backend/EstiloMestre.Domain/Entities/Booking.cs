using System.ComponentModel.DataAnnotations.Schema;

namespace EstiloMestre.Domain.Entities;

[Table("Bookings")]
public class Booking : EntityBase
{
    public long CustomerId { get; set; }
    public User Customer { get; set; } = null!;

    public long EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;

    public long BarbershopId { get; set; }
    public Barbershop Barbershop { get; set; } = null!;

    public long BarbershopServiceId { get; set; }
    public BarbershopService BarbershopService { get; set; } = null!;

    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}