namespace EstiloMestre.Communication.Requests;

public class RequestWorkingHourJson
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public bool IsDayOff { get; set; } = false;
}