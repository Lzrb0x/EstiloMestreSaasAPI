using System.ComponentModel.DataAnnotations.Schema;

namespace EstiloMestre.Domain.Entities;

[Table("EmployeeWorkingHours")]
public class EmployeeWorkingHour : EntityBase
{
    public long EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    
    public DayOfWeek DayOfWeek { get; set; }
    
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    
    public bool IsDayOff { get; set; } = false;
}