using System.ComponentModel.DataAnnotations.Schema;

namespace EstiloMestre.Domain.Entities;

[Table("EmployeeWorkingHourOverrides")]
public class EmployeeWorkingHourOverride : EntityBase
{
    public long EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;

    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public bool IsDayOff { get; set; } = false;
}