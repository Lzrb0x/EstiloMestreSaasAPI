using System.ComponentModel.DataAnnotations.Schema;

namespace EstiloMestre.Domain.Entities;

[Table("Employees")]
public class Employee : EntityBase
{
    public long UserId { get; set; }
    public long BarberShopId { get; set; }
    public Barbershop BarberShop { get; set; } = null!;
    public IList<ServiceEmployee> ServicesEmployee { get; set; } = [];
    public IList<EmployeeWorkingHour> EmployeeWorkingHours { get; set; } = [];
    public IList<EmployeeWorkingHourOverride> EmployeeWorkingHourOverrides { get; set; } = [];
    public IList<Booking> Bookings { get; set; } = [];
}